using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ThrowSpear : MonoBehaviour
{

    public GameObject spear;
    public Rigidbody rb;
    public Vector3 startPos;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spear != null)
        {
            isMoving = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        readyThrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (isAiming)
            {
                //Camera.Aim();
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
                }

            }
        }
        //To reset spear for testing delete when three charges are implemented
            if (Input.GetKeyDown(KeyCode.R))
            {
                spear.transform.position = startPos;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                spear.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                isMoving = false;
            }

    }
    void Throw()
    {
        isMoving = true;

        if (rb != null)
        {
            // Convert mouse position to world coordinates
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 direction;

            if (Physics.Raycast(ray, out hit))
            {
                direction = (hit.point - transform.position).normalized;
            }
            else
            {
                direction = ray.direction; // If no hit, use ray direction
            }

            // Apply force
            rb.AddForce(direction * strength, ForceMode.Impulse);
            readyThrow = false;
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