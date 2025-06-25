using UnityEngine;

public class ThrowSpear : MonoBehaviour
{

    public GameObject spear;
    public Rigidbody rb;
    public GameObject target;
    public float spearSpeed = 2f;
    public float strength = 0f;
    bool isMoving;
    public Vector3 startPos;
    public LayerMask tree;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spear != null)
        {
            isMoving = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     isMoving = true;
        // }
        // if (isMoving && spear.transform.position != target.transform.position)
        // {
        //     spear.transform.position = Vector3.MoveTowards(spear.transform.position, target.transform.position, spearSpeed);

        // }
        // else if (isMoving && spear.transform.position == target.transform.position)
        // {
        //     isMoving = false;
        // }
        if (Input.GetButtonDown("Fire1"))
        {
            rb.constraints = RigidbodyConstraints.None;
            Throw();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            spear.transform.position = startPos;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        }

    void Throw()
    {
        Camera cam = Camera.main;
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
        }
    }
}

