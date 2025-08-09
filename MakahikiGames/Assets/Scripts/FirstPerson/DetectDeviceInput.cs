using UnityEngine;
using UnityEngine.InputSystem;

public class DetectDeviceInput : MonoBehaviour
{
    public GameObject keyboardUI;  // UI or object to enable if keyboard is used
    public GameObject gamepadUI;   // UI or object to enable if gamepad is used

    private string currentInputMethod = "Keyboard";

    void Start()
    {
        SwitchToKeyboard();
    }
    void Update()
    {
        // Detect keyboard key press
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (currentInputMethod != "Keyboard")
            {
                currentInputMethod = "Keyboard";
                SwitchToKeyboard();
            }
        }

        // Detect gamepad input
        if (Gamepad.current != null && (
            Gamepad.current.leftStick.ReadValue().magnitude > 0.1f ||
            Gamepad.current.buttonSouth.wasPressedThisFrame ||
            Gamepad.current.dpad.ReadValue() != Vector2.zero))
        {
            if (currentInputMethod != "Gamepad")
            {
                currentInputMethod = "Gamepad";
                SwitchToGamepad();
            }
        }
    }

    void SwitchToKeyboard()
    {
        keyboardUI?.SetActive(true);
        gamepadUI?.SetActive(false);
        Debug.Log("Switched to Keyboard");
    }

    void SwitchToGamepad()
    {
        keyboardUI?.SetActive(false);
        gamepadUI?.SetActive(true);
        Debug.Log("Switched to Gamepad");
    }
}
