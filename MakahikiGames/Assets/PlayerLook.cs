using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    public GameObject player;
    public ThrowSpear throwSpear;
    public bool isAiming;
    public bool isCharging;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            player.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //  player.transform.rotation = Quaternion.LookRotation(0, 0, 0); 
        }
        if (isAiming)
        {
            throwSpear.isAiming = true;
        }
        else
        {
            throwSpear.isAiming = false;
        }
        if (isCharging)
        {
            throwSpear.isCharging = true;
        }
        else
        {
            throwSpear.isCharging = false;
        }

    }
    public void OnAiming(InputValue value)
    {
        isAiming = value.isPressed;
    }
        public void OnChargeThrow(InputValue value)
    {
        isCharging = value.isPressed;
    }
}
