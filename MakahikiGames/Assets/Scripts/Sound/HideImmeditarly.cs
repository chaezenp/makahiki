using UnityEngine;

public class HideImmeditarly : MonoBehaviour
{
public GameObject ObjectToTurnOff;
void Start()
    {
        ObjectToTurnOff.SetActive(false);
    }
}
