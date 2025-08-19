using UnityEngine;

public class TalkPeopleLight : MonoBehaviour, InteractLight
{
    public GameObject Personlight;
    public void InteractLight(bool Talked2Before)
    {
        if (Talked2Before)
        {
            Personlight.SetActive(false);
        }
    }
}