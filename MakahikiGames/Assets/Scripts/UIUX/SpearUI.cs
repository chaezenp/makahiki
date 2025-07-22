using UnityEngine;
using UnityEngine.UI;

public class SpearUI : MonoBehaviour
{
    public Slider powerBar;
    public float powerVal;
    public float powerValMax;

    public void SpearCharge(float charge)
    {
        powerVal = charge;
        powerBar.value = powerVal;
        powerBar.maxValue = powerValMax;
    }
}
