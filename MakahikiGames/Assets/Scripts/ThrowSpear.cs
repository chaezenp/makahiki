using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ThrowSpear : MonoBehaviour
{

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (isAiming && !isThrown)
            {
                
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
                }

            }
        }
        //To reset spear for testing delete when three charges are implemented
        if (Input.GetKeyDown(KeyCode.R))
        {
            spear.transform.position = startPos;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            spear.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            isThrown = false;
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
            Debug.Log("power: " + strength);
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