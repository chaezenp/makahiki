using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}
public class Interact : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    public bool interact = false;
    public bool isTalking = false;
    public Transform InteractionSource;
    public float InteractRange;
    public bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        interact = interactAction.action.IsPressed();
        if (Time.timeScale == 0f)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
        if (!isPaused)
        {
            if (interact && !isTalking)
            {
                Ray r = new Ray(InteractionSource.position, InteractionSource.forward);
                if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
                {
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        isTalking = true;
                        interactObj.Interact();
                    }
                }
            }
            if (Time.timeScale > 0f)
            {
                isTalking = false;
            }

        }
    }

}
