using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}
interface IInteractText
{
    public void InteractIndicator(bool isIN);
}
public class Interact : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    public bool interact = false;
    public bool isTalking = false;
    public Transform InteractionSource;
    public float InteractRange;
    public bool isPaused = false;
    private IInteractText lastInteractText;



    // Update is called once per frame
    void Update()
    {
        interact = interactAction.action.IsPressed();
        isPaused = Time.timeScale == 0f;

        if (isPaused)
        {
            if (lastInteractText != null)
        {
            lastInteractText.InteractIndicator(false);
            lastInteractText = null;
        }
        return;
        }

            Ray r = new Ray(InteractionSource.position, InteractionSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
                {
                    // Try to get both interfaces
                var interactable = hitInfo.collider.GetComponent<IInteractable>();
                var interactText = hitInfo.collider.GetComponent<IInteractText>();

                // Handle UI: if looking at a new interactable, hide previous and show new
                if (interactText != null && interactText != lastInteractText)
                {
                    // Hide previous
                    if (lastInteractText != null)
                        lastInteractText.InteractIndicator(false);

                    // Show current
                    interactText.InteractIndicator(true);
                    lastInteractText = interactText;
                }

                // If interact button is pressed and not talking yet
                if (interactable != null && interact && !isTalking)
                {
                    isTalking = true;
                    interactable.Interact();
                }
            }
            else
            {
                // Nothing hit → hide last interact UI
                if (lastInteractText != null)
                {
                    lastInteractText.InteractIndicator(false);
                    lastInteractText = null;
                }
            }
            if (Time.timeScale > 0f)
            {
                isTalking = false;
            }

        }
    }
