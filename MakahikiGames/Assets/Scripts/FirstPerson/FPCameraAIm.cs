using UnityEngine;
using UnityEngine.InputSystem;


public class FPCameraAim : MonoBehaviour
{
    [SerializeField] private FPPlayerInputHandler FPinputHandler;
    public bool isInputEnabled = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        FPinputHandler.OnFPLookInput += HandleLook;

    }

    private void OnDisable()
    {
        FPinputHandler.OnFPLookInput -= HandleLook;

    }

    private void HandleLook(Vector2 input)
    {
        if (!isInputEnabled) return;
        var device = Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero
            ? Mouse.current
            : (InputDevice)Gamepad.current;

        FPinputHandler.HandleLook(input, device);
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
