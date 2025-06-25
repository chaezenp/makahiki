using UnityEditor.Callbacks;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the colliding object's layer is included in the freezeLayer
        if (collision.CompareTag("BananTree"))
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
