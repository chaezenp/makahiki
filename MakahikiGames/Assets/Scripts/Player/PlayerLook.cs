using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    public GameObject player;
    public Camera maincam;
    public ThrowSpear throwSpear;
    public bool isAiming;
    [SerializeField] private InputActionReference aimAction;

    public bool isCharging;
    [SerializeField] private InputActionReference chargeAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            isAiming = aimAction.action.IsPressed(); 
            isCharging = chargeAction.action.IsPressed();
        if (isAiming)
        {
            player.transform.rotation = Quaternion.LookRotation(maincam.transform.forward);
        }
        if (Input.GetButtonDown("Respawn"))
        {
            player.transform.rotation = Quaternion.LookRotation(Vector3.zero); 
        }
        // if (isAiming)
        // {
        //     throwSpear.isAiming = true;
        // }
        // else
        // {
        //     throwSpear.isAiming = false;
        // }
        // if (isCharging)
        // {
        //     throwSpear.isCharging = true;
        // }
        // else
        // {
        //     throwSpear.isCharging = false;
        // }

    }

}
