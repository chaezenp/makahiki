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
    public bool canReset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onGround = false;
        isThrown = false;
        timerOn = false;
        savedWait = waitime;
        canReset = false;
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
                canReset = true;
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

    public void OnTriggerExit(Collider collision)
    {
        // Check if the colliding object's layer is included in the freezeLayer
        if (collision.CompareTag("SpearHoldBox"))
        {
            if (rb != null && isThrown)
            {
                canReset = false;
                CameraSwitch.SpearCamSwitch();
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 hitPoint = collision.contacts[0].point;
            Debug.Log("Spear hit ground at: " + hitPoint);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.DrawRay(hitPoint, Vector3.up * 2, Color.red, 5f); // Red marker lasts 5 sec

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("BanaTree"))
        {
            ContactPoint contact = collision.contacts[0];
            Debug.Log("Spear hit target at: " + contact.point);
            Vector3 spearForward = transform.up;
            Vector3 contactNormal = -contact.normal;
            float dot = Vector3.Dot(spearForward, contactNormal);

            if (dot > 0.85f) // Only stick if tip hits target
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                scoreSystem.Hit(contact.point);

            }

            // Speed based embed
            //float speed = rb.linearVelocity.magnitude;
            //float embedDepth = Mathf.Clamp(speed * 0.00025f, 0.005f, 0.025f);

            // static embed
            //float embedDepth = 0.00025f;

            //transform.position = contact.point - transform.up * embedDepth;

            Debug.DrawRay(contact.point, Vector3.up * 2, Color.cyan, 5f);

            timerOn = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, TheLine);
    }
}
