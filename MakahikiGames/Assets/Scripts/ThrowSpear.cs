using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
using NUnit.Framework;
using UnityEngine.Rendering;

public class ThrowSpear : MonoBehaviour
{

    public bool isPracticeMode = true;
    public GameObject spear;
    public Rigidbody rb;
    public Vector3 startPos;
    public Transform spearPos;
    public LayerMask tree;
    public Camera cam;

    public float strength = 0f;
    public float strengthMult = 4f;
    public float maxCharge = 50f;
    private float timer = 0.0f;
    private int seconds = 0;

    bool isMoving;
    public bool isAiming;
    public bool isCharging;
    public bool readyThrow;
    public bool isThrown;
    public bool isWin = false;
    public SpearUI SpearUI;
    public SpearCollision spearCollision;
    public UIManager uiManager;
    public CameraSwitch CameraSwitch;
    public int ammoRemaining = 0;
    public int maxAmmo = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spear != null)
        {
            isMoving = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        readyThrow = false;
        isThrown = false;
        if (!isPracticeMode)
        {
            Debug.Log("NOT PRACTICE MODE");
            ammoRemaining = maxAmmo;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isWin)
        {
            if (!isMoving)
            {
                if (isAiming && !isThrown)
                {
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;


                    spear.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward) * Quaternion.Euler(90, 0, 0);
                    if (isCharging)
                    {
                        chargeSpear();
                    }
                    if (!isCharging && readyThrow)
                    {
                        rb.constraints = RigidbodyConstraints.None;
                        Throw();
                        timer = 0f;
                        strength = 0f;
                        isMoving = false;
                        readyThrow = false;
                        isThrown = true;
                        spearCollision.isThrown = isThrown;
                        ammoRemaining--;

                    }

                }
            }
            //To reset spear for testing delete when three charges are implemented
            if (Input.GetKeyDown(KeyCode.R))
            {
                resetSpear();
            }
        }
        if (isWin)
            {
            Debug.Log("WINNER");
            CameraSwitch.CamReset();

            }
    }
    void Throw()
    {
        isMoving = true;

        if (rb != null)
        {
            // Apply force
            rb.AddForce(transform.forward * strength, ForceMode.Impulse);

            //For Spawn in version
            // GameObject spear = Instantiate(spearObject, spearPos.position, transform.rotation);
            // spear.GetComponent<Rigidbody>().AddForce(transform.forward * strength, ForceMode.Impulse);
        }
    }

    void chargeSpear()
    {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        if (strength != maxCharge || strength < maxCharge)
        {
            strength = timer * strengthMult;
            SpearUI.SpearCharge(strength);
        }
        if (strength >= maxCharge)
        {
            strength = maxCharge;
        }
        if (strength == 50)
        {
            Debug.Log(seconds);
        }
        readyThrow = true;
        if (seconds % 1 == 0)
        {
            //Debug.Log("power: " + strength);
        }
    }

    void resetSpear()
    {
        if (isPracticeMode || (ammoRemaining < maxAmmo && ammoRemaining > 0 && !isPracticeMode))
            {
                spear.transform.position = startPos;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                spear.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                isThrown = false;
                Debug.Log("resetSpear");
                if (!isPracticeMode && (ammoRemaining != 0 || ammoRemaining > 0))
                {
                    uiManager.ammoRemaining(ammoRemaining);
                }
            }
            if (ammoRemaining == 0 && !isPracticeMode)
            {
                Debug.Log("No More Ammo");
            }
    }
    

    #region input bool
    public void OnAiming(InputValue value)
    {
        isAiming = value.isPressed;
    }

    public void OnChargeThrow(InputValue value)
    {
        isCharging = value.isPressed;
    }

    #endregion
}