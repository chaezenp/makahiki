using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class wind : MonoBehaviour
{
    public float windSpeed = 0f;
    public Vector3 windDir = new Vector3(1f, 0f, 0f);
    private Rigidbody rb;
    private bool inAir = false;
    public GameObject WindArrow;
    public float xv = -90f;
    public float yv = 0;
    public float zv = 90;
    [SerializeField] private InputActionReference aimAction;
    public bool isThrown;

    void Start()
    {
        isThrown = false;
        rb = GetComponent<Rigidbody>();
        if (windSpeed <= 1)
        {
            Debug.Log("Light Breeze");
        }
        if (windSpeed <= 3 && windSpeed >= 2)
        {
            Debug.Log("Moderate Wind");
        }
        if (windSpeed <= 5 && windSpeed >= 4)
        {
            Debug.Log("Strong Wind");
        }
        if (windSpeed >= 6)
        {
            Debug.Log("Extreme Winds");
        }
        if (windSpeed > 10)
        {
            windSpeed = 10;
        }
    }

    void Update()
    {
        bool isAiming = aimAction.action.IsPressed();
        if (isAiming || isThrown)
        {
            WindArrow.SetActive(false);
        }
        else
        {
            WindArrow.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 flatWind = new Vector3(windDir.x, 0f, windDir.z).normalized;
        if (inAir)
        {
            rb.AddForce(flatWind.normalized * windSpeed, ForceMode.Force);
        }
        //Temp arrow for direction
        Vector3 horizontalWindDir = new Vector3(windDir.x, 0, windDir.z).normalized;


        // If the wind direction has no horizontal component (e.g., vertical wind), do nothing
        if (horizontalWindDir.magnitude > 0 && windDir.x > 0 && windDir.z > 0)
        {
            // Calculate the target rotation to align the object’s forward with the wind direction horizontally
            Quaternion targetRotation = Quaternion.LookRotation(horizontalWindDir, Vector3.back) * Quaternion.Euler(xv, yv, zv);
            WindArrow.transform.rotation = targetRotation;
        }
        if (horizontalWindDir.magnitude > 0 && windDir.x < 0)
        {
            // Calculate the target rotation to align the object’s forward with the wind direction horizontally
            Quaternion targetRotation = Quaternion.LookRotation(horizontalWindDir, Vector3.back) * Quaternion.Euler(xv, yv + 180, zv + 180);
            WindArrow.transform.rotation = targetRotation;
        }
        if (horizontalWindDir.magnitude > 0 && windDir.z < 0)
        {
            // Calculate the target rotation to align the object’s forward with the wind direction horizontally
            Quaternion targetRotation = Quaternion.LookRotation(horizontalWindDir, Vector3.back) * Quaternion.Euler(-1 * xv, yv, zv);
            WindArrow.transform.rotation = targetRotation;
        }
        if (horizontalWindDir.magnitude > 0 && windDir.x < 0 && windDir.z < 0)
        {
            // Calculate the target rotation to align the object’s forward with the wind direction horizontally
            Quaternion targetRotation = Quaternion.LookRotation(horizontalWindDir, Vector3.back) * Quaternion.Euler(xv - 180, yv, zv + 180 + 180);
            WindArrow.transform.rotation = targetRotation;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SpearHoldBox"))
        {
            if (rb != null)
            {
                inAir = true;
            }
        }
    }
    void OnDrawGizmos()
{
    Gizmos.color = Color.cyan;
    Gizmos.DrawRay(transform.position, windDir.normalized * windSpeed);
}

}
