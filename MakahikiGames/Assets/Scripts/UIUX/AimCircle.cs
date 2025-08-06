using UnityEngine;
using UnityEngine.InputSystem;

public class AimCircle : MonoBehaviour
{
    [SerializeField] private InputActionReference aimAction;
    public GameObject AimingCircle;

    void Start()
    {
        AimingCircle.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
            bool isAiming = aimAction.action.IsPressed();
            if (isAiming)
            {
                AimingCircle.SetActive(true);
            }
            else
            {
                AimingCircle.SetActive(false);
            }
    }
}
