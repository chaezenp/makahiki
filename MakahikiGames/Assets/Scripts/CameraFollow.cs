using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object the camera will follow
    public Vector3 offset; // The desired offset from the target
    public float smoothSpeed = 0.125f; // Smoothness of the follow
    private GameObject self;
    public Vector3 origin;

    void Start()
    {
        self = gameObject;
    }
    void Update()
    {
        // Calculate the desired position with the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Apply the smoothed position to the camera's transform
        transform.position = smoothedPosition;
        //transform.rotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, -25, 0);

        // Optionally, make the camera look at the target
        //transform.LookAt(target); 
        if (Input.GetKeyDown(KeyCode.R) && !(self.transform.position == origin))
        {
            self.transform.position = origin;
        }
    }
}
