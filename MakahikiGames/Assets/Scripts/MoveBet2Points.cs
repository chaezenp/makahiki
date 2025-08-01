using UnityEngine;

public class MoveBet2Points : MonoBehaviour
{
    public float speed = 2f;

    public Transform pointA;
    public Transform pointB;
    public Transform target;

    void Update()
    {
        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If we've reached the target, switch to the other point
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
