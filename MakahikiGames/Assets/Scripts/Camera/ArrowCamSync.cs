using UnityEngine;

public class ArrowCamSync : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject ArrowCam;

    void Update()
    {
        if (mainCam == null) return;

        // Sync this object's active state with the mainCam
        if (ArrowCam.activeSelf != mainCam.activeSelf)
        {
            ArrowCam.SetActive(mainCam.activeSelf);
        }
    }
}
