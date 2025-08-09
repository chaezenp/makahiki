using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Talkto2People : MonoBehaviour, IInteractable
{
    public GameObject dialogueBox;
    public MenuManager menuManager;
    public GameObject[] dialogueText;
    public GameObject[] Names;
    public bool interact;
    public bool next;
    public bool isInteract = false;
    bool wasPressed = false;

    public int nextText = 0;
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference nextAction;


    public void Interact()
    {
        //Debug.Log("interact: " + Random.Range(0, 100));
        Time.timeScale = 0;
        dialogueBox.SetActive(true);
        dialogueText[nextText].SetActive(true);
        Names[nextText +1].SetActive(false);
        Names[nextText].SetActive(true);


        isInteract = true;
        menuManager.inDialogue = true;

        Debug.Log("Start Dialogue");
    }
    public void Update()
    {
        if (isInteract)
        {
            //interact = interactAction.action.IsPressed();
            next = nextAction.action.IsPressed();


            if (next && !wasPressed)
            {
                Debug.Log(dialogueText.Length);
                if (nextText + 1 < dialogueText.Length)
                {
                    nextText = nextText + 1;
                    if (dialogueText[nextText] != null)
                    {
                        dialogueText[nextText - 1].SetActive(false);
                        dialogueText[nextText].SetActive(true);
                        // Line up dialogue with their name
                        Names[nextText - 1].SetActive(false);
                        Names[nextText].SetActive(true);
                    }
                }
                else
                {
                    Debug.Log("End of Dialogue");
                    isInteract = false;
                    dialogueBox.SetActive(false);
                    Time.timeScale = 1;
                    nextText = 0;
                    for (int i = 0; i < dialogueText.Length; i++)
                    {
                        dialogueText[i].SetActive(false);
                    }
                }
            }
            wasPressed = next;
        }
    }

}
