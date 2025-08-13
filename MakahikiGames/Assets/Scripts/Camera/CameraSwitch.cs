using UnityEngine;
using UnityEngine.Animations;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras;
    public Camera MainCam;
    public Vector3 cam2StartPos;
    public Vector3 MainCamSetPos;
    public GameObject Parentspear;
    public Transform target;

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
        cameras[1].gameObject.SetActive(false);
    }
    public void SpearCamSwitch()
    {
        cameras[0].gameObject.SetActive(false);
        cameras[1].gameObject.SetActive(true);
        //MainCam.transform.SetParent(Parentspear.transform, true);
    }

    public void CamReset()
    {
        cameras[0].gameObject.SetActive(true);
        cameras[1].gameObject.SetActive(false);
        cameras[0].transform.LookAt(target, Vector3.forward);
        Debug.Log("RESET-CAM");
        // MainCam.transform.SetParent(Parentspear.transform, false);
        // MainCam.transform.position = MainCamSetPos;
        //cameras[1].transform.position = cam2StartPos;
    }
    
}
