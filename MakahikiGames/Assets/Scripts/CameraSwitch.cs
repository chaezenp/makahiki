using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras;
    public Vector3 cam2StartPos;
    bool cam1, cam2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if (cameras.Length > 1)
        // {
        //     for (int i = 1; i < cameras.Length; i++)
        //     {
        //         cameras[i].gameObject.SetActive(false);
        //     }
        // }

        cameras[0].gameObject.SetActive(true);
        cam1 = true;
        cameras[1].gameObject.SetActive(false);
        cam2 = false;
    }
    // if (Input.GetKeyDown(KeyCode.JoystickButton4))

    // Update is called once per frame
    void Update()
    {
        // if (cam1 && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5)))
        // {
        //     cameras[0].gameObject.SetActive(false);
        //     cam1 = false;
        //     cameras[1].gameObject.SetActive(true);
        //     cam2 = true;
        // }
        // else if (cam2 && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5)))
        // {
        //     cameras[0].gameObject.SetActive(true);
        //     cam1 = true;
        //     cameras[1].gameObject.SetActive(false);
        //     cam2 = false;
        // }
    }

    public void CamSwitch()
    {
        cameras[0].gameObject.SetActive(false);
        cam1 = false;
        cameras[1].gameObject.SetActive(true);
        cam2 = true;
    }

    public void CamReset()
    {
        cameras[0].gameObject.SetActive(true);
        cam1 = true;
        cameras[1].gameObject.SetActive(false);
        cam2 = false;
        cameras[1].transform.position = cam2StartPos;
    }
    
}
