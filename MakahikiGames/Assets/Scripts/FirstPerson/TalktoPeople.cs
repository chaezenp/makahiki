using UnityEngine;

public class TalktoPeople : MonoBehaviour, IInteractable
{
    public GameObject dialogueBox;

    public void Interact()
    {
        //Debug.Log("interact: " + Random.Range(0, 100));
        Time.timeScale = 0;
        dialogueBox.SetActive(true);
    }

}
