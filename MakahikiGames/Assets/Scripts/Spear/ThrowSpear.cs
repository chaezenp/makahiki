using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
using NUnit.Framework;
using UnityEngine.Rendering;

public class ThrowSpear : MonoBehaviour
{
[SerializeField] private GameObject spearObject;

    public bool isPracticeMode = true;
    public GameObject spear;
    public Rigidbody rb;
    public Vector3 startPos;
    public Transform spearPos;
    public LayerMask tree;
    public Camera Maincam;
    //public ArcPreview drawArc;

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
    public bool canReset = false;
    public bool isWin = false;
    public SpearUI SpearUI;
    public SpearCollision spearCollision;
    public UIManager uiManager;
    public ScoreSystem pointSystem;
    public CameraSwitch CameraSwitch;
    public GameObject reloadIndicator;
    public wind wind;
    [SerializeField] private InputActionReference aimAction;
    [SerializeField] private InputActionReference chargeAction;
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
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        reloadIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isAiming = aimAction.action.IsPressed();
        isCharging = chargeAction.action.IsPressed();
        canReset = spearCollision.canReset;

        if (!isWin)
        {
            if (!isMoving)
            {
                reloadIndicator.SetActive(canReset);
                if (!isAiming && strength > 0)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    Throw();
                    timer = 0f;
                    strength = 0f;
                    isMoving = false;
                    readyThrow = false;
                    isThrown = true;
                    spearCollision.isThrown = isThrown;
                    wind.isThrown = isThrown;
                    ammoRemaining--;
                    uiManager.ammoRemaining(ammoRemaining);
                    pointSystem.ammoRemaining = ammoRemaining;
                    pointSystem.isThrown = isThrown;
                }
                if (isAiming && !isThrown)
                {

                    spear.transform.rotation = Quaternion.LookRotation(Maincam.transform.forward) * Quaternion.Euler(90, 0, 0);
                    if (isCharging)
                    {
                        chargeSpear();
                    }
                    if (!isCharging && readyThrow)
                    {
                        SoundManager.PlaySound(SoundType.SPEARTHROW);
                        rb.constraints = RigidbodyConstraints.None;
                        Throw();
                        timer = 0f;
                        strength = 0f;
                        isMoving = false;
                        readyThrow = false;
                        isThrown = true;
                        spearCollision.isThrown = isThrown;
                        wind.isThrown = isThrown;
                        ammoRemaining--;
                        uiManager.ammoRemaining(ammoRemaining);
                        pointSystem.ammoRemaining = ammoRemaining;
                        pointSystem.isThrown = isThrown;
                        //drawArc.isThrown = isThrown;
                        if(isThrown) SoundManager.PlaySound(SoundType.SPEARWIND);
                    }

                }
            }
            //To reset spear for testing delete when three charges are implemented
            if (canReset && Input.GetButtonDown("Respawn"))
            {
                resetSpear();
            }
        }
        if (isWin)
        {
            Debug.Log("WINNER");
            CameraSwitch.CamReset();
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        if (ammoRemaining == 0)
        {
            reloadIndicator.SetActive(false);
        }
    }
    void Throw()
    {
        isMoving = true;

        if (rb != null)
        {
            // center of screen aim
            // Vector3 aimDir = Maincam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)).direction;

            // rb.AddForce(aimDir.normalized * strength, ForceMode.Impulse);

            //Camera forward aim
            rb.AddForce(Maincam.transform.forward * strength, ForceMode.Impulse);
            //For Spawn in version

            //GameObject spear = Instantiate(spearObject, spearPos.position, transform.rotation);
            //spear.GetComponent<Rigidbody>().AddForce(Maincam.transform.forward * strength, ForceMode.Impulse);
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
            //drawArc.launchForce = strength;
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
        rb.Sleep();
        if (isPracticeMode || (ammoRemaining < maxAmmo && ammoRemaining > 0 && !isPracticeMode))
        {
            spear.transform.position = startPos;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            spear.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            isThrown = false;
            wind.isThrown = isThrown;
            pointSystem.isThrown = isThrown;

           //drawArc.isThrown = isThrown;
            Debug.Log("resetSpear");
        }
            if (ammoRemaining == 0 && !isPracticeMode)
            {
                pointSystem.isThrown = false;
                Debug.Log("No More Ammo");
            }
    }

}