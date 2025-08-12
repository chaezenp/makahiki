using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 1;
    public float jumpForce = 5;
    public Vector3 startPos;
    public bool canWalk = false;

    public void CanWalk()
    {
        if (!canWalk)
        {
            canWalk = true;
        }
        
        if (canWalk)
        {
            canWalk = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canWalk)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
            }

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            movementDirection.Normalize();

            transform.Translate(movementDirection * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = startPos;
        }
    }
}
