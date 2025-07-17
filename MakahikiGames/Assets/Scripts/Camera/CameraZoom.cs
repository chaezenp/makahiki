using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoomOnAim : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float aimFOV = 40f;
    [SerializeField] private float zoomSpeed = 10f;

    [Header("Input Settings")]
    [SerializeField] private InputActionReference aimAction; // Drag your 'Aim' Input Action here

    private bool isAiming = false;

    private void OnEnable()
    {
        aimAction.action.Enable();
        aimAction.action.performed += OnAimPerformed;
        aimAction.action.canceled += OnAimCanceled;
    }

    private void OnDisable()
    {
        aimAction.action.performed -= OnAimPerformed;
        aimAction.action.canceled -= OnAimCanceled;
        aimAction.action.Disable();
    }

    private void Update()
    {
    bool isPressed = aimAction.action.IsPressed(); // works for mouse/gamepad
    float targetFOV = isPressed ? aimFOV : normalFOV;
    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        isAiming = true;
    }

    private void OnAimCanceled(InputAction.CallbackContext context)
    {
        isAiming = false;
    }
}
