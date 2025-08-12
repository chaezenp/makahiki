using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    public GameObject crosshair;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CrosshairActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0)
        {
            CrosshairDeactive();
        }
        else
        {
            CrosshairActive();
        }
    }

    public void CrosshairActive()
    {
        crosshair.SetActive(true);
    }
    public void CrosshairDeactive()
    {
        crosshair.SetActive(false);
    }
}
