using Unity.VisualScripting;
//using UnityEditor.Callbacks;
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
    private bool inTree;
    private float timer = 0.0f;
    private int seconds = 0;
    public bool isThrown;
    public bool timerOn;
    public int waitime = 5;
    public int savedWait;
    public bool canReset;
    public bool upDraft;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onGround = false;
        inTree = false;
        isThrown = false;
        timerOn = false;
        savedWait = waitime;
        canReset = false;
        upDraft = false;
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
                scoreSystem.isLose = true;
                //NextSpear.Next();
                timerOn = false;
                onGround = false;
                canReset = true;
                timer = 0;
                waitime = savedWait;
                inTree = false;
            }
        }
        scoreSystem.onGround = onGround;
        scoreSystem.inTree = inTree;

    }

    void FixedUpdate()
    {
        //Spear Rotation
        //Where its facing (looks better?)
        if (rb.linearVelocity != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(rb.linearVelocity) * Quaternion.Euler(89, 0, 0);
        }
        if (upDraft)
        {
            rb.AddForce(-transform.forward * 2, ForceMode.Force);
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
            scoreSystem.onGround = true;

        }
        if (collision.CompareTag("Out"))
        {
            timerOn = true;
            scoreSystem.onGround = true;
            waitime = 0;
        }
        if (collision.CompareTag("Water"))
        {
            SoundManager.StopSound();
            SoundManager.PlayOneShot(SoundType.SPEARSPLASH, 0.5f);
        }
        if (collision.CompareTag("Wind"))
        {
            Debug.Log("IN Updraft");
            upDraft = true;
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
                if (collision.CompareTag("Wind"))
        {
            Debug.Log("OUT Updraft");
            upDraft = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SoundManager.StopSound();
            SoundManager.PlayOneShot(SoundType.SPEARMISS);
            Vector3 hitPoint = collision.contacts[0].point;
            Debug.Log("Spear hit ground at: " + hitPoint);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.DrawRay(hitPoint, Vector3.up * 2, Color.red, 5f); // Red marker lasts 5 sec
            onGround = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("BanaTree"))
        {
            SoundManager.StopSound();
            SoundManager.PlayOneShot(SoundType.SPEARHIT);
            ContactPoint contact = collision.contacts[0];
            Debug.Log("Spear hit target at: " + contact.point);
            Vector3 spearForward = transform.up;
            Vector3 contactNormal = -contact.normal;
            float dot = Vector3.Dot(spearForward, contactNormal);

            float hitangleThreshold = 0.5f;

            if (dot > hitangleThreshold) // Only stick if tip hits target
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                scoreSystem.Hit(contact.point);

            }
            else
            {
                rb.linearVelocity = rb.linearVelocity * 0.1f;
                rb.angularVelocity = Vector3.zero;
            }

            // Speed based embed
            //float speed = rb.linearVelocity.magnitude;
            //float embedDepth = Mathf.Clamp(speed * 0.00025f, 0.005f, 0.025f);

            // static embed
            //float embedDepth = 0.00025f;

            //transform.position = contact.point - transform.up * embedDepth;

            Debug.DrawRay(contact.point, Vector3.up * 2, Color.cyan, 5f);

            timerOn = true;
            inTree = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, TheLine);
    }
}
