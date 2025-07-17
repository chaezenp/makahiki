using Unity.VisualScripting;
using UnityEngine;

public class wind : MonoBehaviour
{
    public float windSpeed = 0f;
    public Vector3 windDir = new Vector3(1f, 0f, 0f);
    private Rigidbody rb;
    private bool inAir = false;
    void Start()
    {
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

    void FixedUpdate()
    {
        if (inAir)
        {
            Vector3 flatWind = new Vector3(windDir.x, 0f, windDir.z).normalized;

            rb.AddForce(flatWind.normalized * windSpeed, ForceMode.Force);
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
