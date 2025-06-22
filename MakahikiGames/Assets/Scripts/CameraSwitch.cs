using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject Camera1; 
    public GameObject Camera2;
    bool cam1, cam2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera1.SetActive(true);
        cam1 = true;
        Camera2.SetActive(false);
        cam2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam1 && Input.GetKeyDown(KeyCode.Tab))
        {
            Camera1.SetActive(false);
            cam1 = false;
            Camera2.SetActive(true);
            cam2 = true;
        }
        else if (cam2 && Input.GetKeyDown(KeyCode.Tab))
        {
            Camera1.SetActive(true);
            cam1 = true;
            Camera2.SetActive(false);
            cam2 = false;
        }
    }
}
