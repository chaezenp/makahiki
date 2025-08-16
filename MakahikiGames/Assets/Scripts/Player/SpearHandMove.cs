using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearHandMove : MonoBehaviour
{
    public float moveDistance = 3f;     
    public float moveSpeed = 6f;         
    public bool moveBackward = false;
    private bool wasCharging = false;
    public GameObject spearModel;
    public GameObject spearCollider;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 startPositionSpear;
    private Vector3 targetPositionSpear;
    private Vector3 lastPositionSpear;
    private bool initialized = false;
    [SerializeField] private InputActionReference chargeAction;
    [SerializeField] private InputActionReference aimAction;



    void Start()
    {
        startPosition = transform.position;
        lastPositionSpear = spearModel.transform.position;
        //startPositionSpear = spearModel.transform.position;
        initialized = true;
    }

    void Update()
    {
        startPositionSpear = spearCollider.transform.position;
        bool isAiming = aimAction.action.IsPressed();
        bool isCharging = chargeAction.action.IsPressed();
        moveBackward = isAiming && isCharging;

    if (!initialized) return;

    if (moveBackward)
    {
        targetPosition = startPosition - transform.forward * moveDistance;
        targetPositionSpear = startPositionSpear + spearModel.transform.up * moveDistance;
    }
    else
    {
        targetPosition = startPosition;
        targetPositionSpear = startPositionSpear;
    }

    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    spearModel.transform.position = Vector3.MoveTowards(spearModel.transform.position, targetPositionSpear, moveSpeed * Time.deltaTime);

    if (wasCharging && !isCharging)
    {
        transform.position = startPosition;
        spearModel.transform.position = spearCollider.transform.position;
    }

    wasCharging = isCharging;
      
    }
}
