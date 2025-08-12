using UnityEngine;
using UnityEngine.UI; 

public class LockToggleHandler : MonoBehaviour
{
    public GameObject objectToLock;
    private Toggle lockToggle;

    void Start()
    {
        lockToggle = GetComponent<Toggle>(); // Get the Toggle component attached to this GameObject
        lockToggle.onValueChanged.AddListener(OnLockToggleValueChanged); // Add a listener for when the toggle's value changes
    }

    void OnLockToggleValueChanged(bool isLocked)
    {
        // Perform actions based on whether the toggle is locked or unlocked
        if (isLocked)
        {
            Debug.Log("Object locked: " + objectToLock.name);
            //  Add your "lock selected" functionality here.
            //  For example, you could disable its movement, prevent interaction, etc.
        }
        else
        {
            Debug.Log("Object unlocked: " + objectToLock.name);
            //  Add your "unlock" functionality here
        }
    }
}
