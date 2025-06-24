using UnityEngine;

public class ThrowSpear : MonoBehaviour
{

    public GameObject spear;
    public GameObject target;
    public float spearSpeed = 2f;
    bool isMoving;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spear != null)
        {
            isMoving = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            isMoving = true;
        }
        if (isMoving && spear.transform.position != target.transform.position)
        {
            spear.transform.position = Vector3.MoveTowards(spear.transform.position, target.transform.position, spearSpeed);

        }
        else if (isMoving && spear.transform.position == target.transform.position)
        {
            isMoving = false;
        }
    }
}
