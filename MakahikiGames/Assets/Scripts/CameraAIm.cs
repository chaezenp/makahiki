using UnityEngine;
using UnityEngine.InputSystem;


public class CameraAim : MonoBehaviour
{
    [Header("Camera Settings")]
    public float mouseSensitivity = 2f;
    public float gamepadSensitivity = 3f; // Can be different for gamepad
    public float verticalLookLimit = 90f; // Limit how far up/down the camera can look

    private float rotationX = 0f;
    private float rotationY = 0f;

    // Reference to the generated Input Actions class
    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();

        // This method will be called whenever the "Look" action is performed
        playerInputActions.Player.Look.performed += OnLookPerformed;
    }

    private void OnEnable()
    {
        playerInputActions.Enable(); // Enable the action map when the script is enabled
    }

    private void OnDisable()
    {
        playerInputActions.Disable(); // Disable the action map when the script is disabled
    }

    // This method is called when the "Look" action is performed by either mouse or gamepad
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        // Determine which device is active and adjust sensitivity accordingly
        if (context.control.device is Mouse)
        {
            rotationY += lookInput.x * mouseSensitivity;
            rotationX -= lookInput.y * mouseSensitivity; // Invert Y for typical camera behavior
        }
        else if (context.control.device is Gamepad)
        {
            rotationY += lookInput.x * gamepadSensitivity;
            rotationX -= lookInput.y * gamepadSensitivity; // Invert Y for typical camera behavior
        }

        // Clamp vertical rotation to prevent the camera from flipping over
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);

        // Apply the rotation to the camera
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}

//     float rotationX = 0f;
//     float rotationY = 0f;
//     public float sensitivity = 5f;

//     void Update()
//     {
//         rotationY += Input.GetAxis("Mouse X") * sensitivity;
//         rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity;

//         transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
//     }


// }
