using UnityEngine;
using UnityEngine.InputSystem;


public class CameraAim : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    public bool isAiming = false;

    private void OnEnable()
    {
        inputHandler.OnLookInput += HandleLook;
        inputHandler.OnAimChanged += HandleAim;

    }

    private void OnDisable()
    {
        inputHandler.OnLookInput -= HandleLook;
        inputHandler.OnAimChanged -= HandleAim;

    }

    private void HandleAim(bool aiming)
{
    isAiming = aiming;
}

    private void HandleLook(Vector2 input)
    {
        var device = Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero
            ? Mouse.current
            : (InputDevice)Gamepad.current;

        inputHandler.HandleLook(input, device);
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
