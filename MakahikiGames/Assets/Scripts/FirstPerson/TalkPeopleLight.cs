using UnityEngine;

public class TalkPeopleLight : MonoBehaviour, InteractLight
{
    public GameObject Personlight;
    public string uniqueID; // Set in Inspector or dynamically
    public bool Talked2Before;
    private void Start()
    {
        LoadBool();
    }
    public void InteractLight(bool Talked2Before)
    {
        LoadBool();
        SetBool(Talked2Before);
        if (Talked2Before)
        {
            Personlight.SetActive(false);
        }
    }

    public void SetBool(bool value)
    {
        Talked2Before = value;
        SaveBool();
    }

    void SaveBool()
    {
        PlayerPrefs.SetInt("Bool_" + uniqueID, Talked2Before ? 1 : 0);
    }

    void LoadBool()
    {
        if (PlayerPrefs.HasKey("Bool_" + uniqueID))
        {
            Talked2Before = PlayerPrefs.GetInt("Bool_" + uniqueID) == 1;
        }
    }
}