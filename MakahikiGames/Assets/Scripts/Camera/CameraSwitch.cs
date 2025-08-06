using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera MainCam;
    public Camera SpearCam;
    public Vector3 cam2StartPos;
    public Vector3 MainCamSetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCam.gameObject.SetActive(true);
    }
    public void SetTargetCamera(Camera cam)
    {
        SpearCam = cam;
        if (SpearCam != null)
        {
            SpearCam.gameObject.SetActive(false);
        }
    }
    public void SpearCamSwitch()
    {
        MainCam.gameObject.SetActive(false);
        SpearCam.gameObject.SetActive(true);
        //MainCam.transform.SetParent(Parentspear.transform, true);
    }

    public void CamReset()
    {
        MainCam.gameObject.SetActive(true);
        if (SpearCam != null)
        {
            SpearCam.gameObject.SetActive(false);
        }
        Debug.Log("RESET-CAM");
    }
    
}
