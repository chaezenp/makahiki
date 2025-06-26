using UnityEditor.Callbacks;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{

    private Rigidbody rb;
    public CameraSwitch CameraSwitch;
    private bool onGround;
    private float timer = 0.0f;
    private int seconds = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            if (seconds > 5)
            {
                CameraSwitch.CamReset();
                //NextSpear.Next();
            }
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        // Check if the colliding object's layer is included in the freezeLayer
        if (collision.CompareTag("BananTree"))
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        if (collision.CompareTag("Respawn"))
        {
            onGround = true;
        }
            if (collision.CompareTag("Out") || collision.CompareTag("BananTree"))
            {
                CameraSwitch.CamReset();
            }
        }
        
        private void OnTriggerExit(Collider collision)
{
    // Check if the colliding object's layer is included in the freezeLayer
    if (collision.CompareTag("SpearHoldBox"))
    {
        if (rb != null)
        {
            CameraSwitch.CamSwitch();
        }
    }
}
}
