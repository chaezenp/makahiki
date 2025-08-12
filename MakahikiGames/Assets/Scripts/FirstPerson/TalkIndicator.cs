using UnityEngine;
using UnityEngine.InputSystem;

public class TalkIndicator : MonoBehaviour, IInteractText
{
    public GameObject indicator;
    public void Start()
    {
        indicator.SetActive(false);
    }
    public void InteractIndicator(bool isIn)
    {
        indicator.SetActive(isIn);
    }
}
