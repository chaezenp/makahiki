using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float mouseSensitivity = 0.2f;
    public float gamepadSensitivity = 3f;
    public float verticalLookLimit = 90f;
    public float horizontalLookLimit = 180f;

    public bool isInputEnabled = true;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public event Action<Vector2> OnLookInput;
    public event Action<bool> OnAimChanged;


    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();

        playerInputActions.Player.Look.performed += OnLookPerformed;
        playerInputActions.Player.Aiming.performed += ctx => OnAimChanged?.Invoke(true);
        playerInputActions.Player.Aiming.canceled += ctx => OnAimChanged?.Invoke(false);

    }

    private void OnEnable() => playerInputActions.Enable();
    private void OnDisable() => playerInputActions.Disable();

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        Vector2 lookInput = context.ReadValue<Vector2>();
//        Debug.Log("Look Input: " + lookInput);
        OnLookInput?.Invoke(lookInput);
    }

    public void HandleLook(Vector2 input, InputDevice device)
    {
        
        float sensitivity = (device is Mouse) ? mouseSensitivity : gamepadSensitivity;

        rotationY += input.x * sensitivity;
        rotationX -= input.y * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);
        rotationY = Mathf.Clamp(rotationY, -horizontalLookLimit, horizontalLookLimit);

        //transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        Quaternion xRotation = Quaternion.AngleAxis(rotationX, Vector3.right);
        Quaternion yRotation = Quaternion.AngleAxis(rotationY, Vector3.up);
        transform.localRotation = yRotation * xRotation;

    }
}
