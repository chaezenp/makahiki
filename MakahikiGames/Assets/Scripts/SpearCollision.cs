using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{
    [SerializeField] private Transform spearHead;
    private Rigidbody rb;
    public CameraSwitch CameraSwitch;
    public ScoreSystem scoreSystem;
    public GameObject LinePoint;
    public Vector3 TheLine;

    private bool onGround;
    private float timer = 0.0f;
    private int seconds = 0;
    public bool isThrown;
    public bool timerOn;
    public int waitime = 5;
    public int savedWait;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onGround = false;
        isThrown = false;
        timerOn = false;
        savedWait = waitime;
    }

    // Update is called once per frame
    void Update()
    {
        TheLine = LinePoint.transform.position;
        if (onGround || timerOn)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            if (seconds > waitime)
            {
                CameraSwitch.CamReset();
                //NextSpear.Next();
                timerOn = false;
                onGround = false;
                timer = 0;
                waitime = savedWait;
            }
        }
    }

    void FixedUpdate()
    {
        //Spear Rotation
        //Where its facing (looks better?)
        if (rb.linearVelocity != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(rb.linearVelocity) * Quaternion.Euler(89, 0, 0);
        }
        //Math Way with center of mass
        // rb.AddForceAtPosition(rb.linearVelocity * -0.1f, transform.TransformPoint(0,-.5f,0));   
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the colliding object's layer is included in the freezeLayer
        if (collision.CompareTag("BananTree"))
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                timerOn = true;

                Vector3 impactPoint = spearHead.position;
                Debug.Log("Impact Point: " + impactPoint);


                // Calculate distance from impact point to center of the target
                scoreSystem.Hit(impactPoint);
            }
        }

        if (collision.CompareTag("Respawn"))
        {
            onGround = true;
            rb.linearVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        if (collision.CompareTag("Out"))
        {
            timerOn = true;
            waitime = 0;
        }
    }

    // public void CollisionEnter(Collision collision)
    // {
    //     foreach (ContactPoint contact in collision.contacts)
    //     {
    //         Debug.Log("Spear hit at point: " + contact.point);
    //         scoreSystem.Hit();
    //     }
    // }

    public void OnTriggerExit(Collider collision)
    {
        // Check if the colliding object's layer is included in the freezeLayer
        if (collision.CompareTag("SpearHoldBox"))
        {
            if (rb != null && isThrown)
            {
                CameraSwitch.SpearCamSwitch();
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, TheLine);
    }
}
