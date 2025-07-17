using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcPreview : MonoBehaviour
{
    [Header("References")]
    public Transform spearSpawnPoint;  // Where the spear will spawn/launch from
    public Transform aimDirection;     // Usually the camera or a forward pointer

    [Header("Arc Settings")]
    public float launchForce = 50f;
    public int arcResolution = 30;     // How many segments in the arc
    public float timeStep = 0.1f;      // Time between each point
    public float maxTime = 3f;         // How far to simulate

    public bool isThrown = false;

    private LineRenderer lineRenderer;
    private Vector3 gravity;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gravity = Physics.gravity;
    }

    void Update()
    {
        Vector3 startPos = spearSpawnPoint.position;
        Vector3 startVel = aimDirection.forward * launchForce;

        DrawArc(startPos, startVel);
        if (isThrown)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.enabled = true;
        }
    }

    void DrawArc(Vector3 startPosition, Vector3 startVelocity)
    {
        Vector3[] arcPoints = new Vector3[arcResolution];

        for (int i = 0; i < arcResolution; i++)
        {
            float t = i * (maxTime / arcResolution);
            Vector3 point = startPosition + startVelocity * t + 0.5f * gravity * t * t;
            arcPoints[i] = point;
        }

        lineRenderer.positionCount = arcResolution;
        lineRenderer.SetPositions(arcPoints);
    }
}
