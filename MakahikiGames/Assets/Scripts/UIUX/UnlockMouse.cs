using UnityEngine;

public class UnlockMouse : MonoBehaviour
{
    void Awake()
    {
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
    }
}
