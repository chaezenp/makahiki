using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPMove : MonoBehaviour
{

    private float moveSpeed;
    public float sprintSpeed;
    public float walkspeed;

    public float groundDrag;

    public float playerHeight;
    public LayerMask Ground;
    public bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    [SerializeField] private InputActionReference runAction;
    public bool isRun;
    public bool isPaused;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        isRun = runAction.action.IsPressed();
        float rayLength = playerHeight * 0.5f + 0.2f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, Ground);
        MyInput(); Color rayColor = isGrounded ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector3.down * rayLength, rayColor);

        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        if (!isPaused)
        {
            if (isRun)
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = walkspeed;
            }
            transform.rotation = orientation.rotation;
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
