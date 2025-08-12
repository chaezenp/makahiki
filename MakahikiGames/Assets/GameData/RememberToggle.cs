using UnityEngine;
using UnityEngine.UI;

public class RememberToggle : MonoBehaviour
{
    public Toggle toggle;
    private const string TogglePrefKey = "CrosshairToggleState";

    private void Start()
    {
        // Load saved value (default to ON = 1)
        bool savedState = PlayerPrefs.GetInt(TogglePrefKey, 1) == 1;

        // Set the toggle value, which also updates its internal image
        toggle.isOn = savedState;

        // Listen for changes to save new state
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(TogglePrefKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
