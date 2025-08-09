using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DetectDeviceInput : MonoBehaviour
{
    public GameObject keyboardUI;  // UI or object to enable if keyboard or mouse is used
    public GameObject gamepadUI;   // UI or object to enable if gamepad is used

    [Header("Toggle input detection")]
    public bool useMouseInsteadOfKeyboard = false;

    private string currentInputMethod = "Keyboard"; // Or "Mouse" or "Gamepad"
    [SerializeField] private string sceneToForceGamepad= "TalkArea";

    void Start()
    {
        SwitchToGamepad();
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == sceneToForceGamepad)
        {
            currentInputMethod = "Keyboard";
            SwitchToKeyboardOrMouse();
        }
    }

    void Update()
    {
        if (useMouseInsteadOfKeyboard)
        {
            DetectMouseInput();
        }
        else
        {
            DetectKeyboardInput();
        }

        DetectGamepadInput();
    }

    void DetectKeyboardInput()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (currentInputMethod != "Keyboard")
            {
                currentInputMethod = "Keyboard";
                SwitchToKeyboardOrMouse(); // shared UI logic
            }
        }
    }

    void DetectMouseInput()
    {
        if (Mouse.current != null && (
            Mouse.current.leftButton.wasPressedThisFrame ||
            Mouse.current.rightButton.wasPressedThisFrame ||
            Mouse.current.middleButton.wasPressedThisFrame ||
            Mouse.current.delta.ReadValue() != Vector2.zero ||
            Mouse.current.scroll.ReadValue().y != 0))
        {
            if (currentInputMethod != "Mouse")
            {
                currentInputMethod = "Mouse";
                SwitchToKeyboardOrMouse(); // shared UI logic
            }
        }
    }

    void DetectGamepadInput()
    {
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

    void SwitchToKeyboardOrMouse()
    {
        keyboardUI?.SetActive(true);
        gamepadUI?.SetActive(false);
        Debug.Log($"Switched to {(useMouseInsteadOfKeyboard ? "Mouse" : "Keyboard")}");
    }

    void SwitchToGamepad()
    {
        keyboardUI?.SetActive(false);
        gamepadUI?.SetActive(true);
        Debug.Log("Switched to Gamepad");
    }
}
