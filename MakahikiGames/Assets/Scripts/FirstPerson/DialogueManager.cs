using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public GameObject Dialoguebox;

    public void Start()
    {
        Dialoguebox.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Dialoguebox.activeSelf)
        {
            Time.timeScale = 1;
            Dialoguebox.SetActive(false);
        }
    }

}
