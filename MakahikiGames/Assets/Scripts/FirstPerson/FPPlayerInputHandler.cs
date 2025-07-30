using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPPlayerInputHandler : MonoBehaviour
{
    public float mouseSensitivity = 0.2f;
    public float gamepadSensitivity = 3f;
    public float verticalLookLimit = 90f;
    public bool isInputEnabled = true;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public event Action<Vector2> OnFPLookInput;
    public Transform orientation;


    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();

        playerInputActions.Player.FPLook.performed += OnFPLookPerformed;


    }

    private void OnEnable() => playerInputActions.Enable();
    private void OnDisable() => playerInputActions.Disable();

    private void OnFPLookPerformed(InputAction.CallbackContext context)
    {
        if (!isInputEnabled) return;

        Vector2 lookInput = context.ReadValue<Vector2>();
        //        Debug.Log("Look Input: " + lookInput); // 👈 add this
        OnFPLookInput?.Invoke(lookInput);
    }

    public void HandleLook(Vector2 input, InputDevice device)
    {
        float sensitivity = (device is Mouse) ? mouseSensitivity : gamepadSensitivity;

        rotationY += input.x * sensitivity;
        rotationX -= input.y * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }

    public void Update()
    {
    orientation.rotation = Quaternion.Euler(0, rotationY, 0);

    }

}
