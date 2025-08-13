using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class SliderSaved : MonoBehaviour
{
    [Header("Setting Key (used in PlayerPrefs)")]
    public string settingKey = "SettingName"; 

    [Header("Default Value")]
    [Range(0f, 4f)]
    public float defaultValue = 1f;

    [Header("Optional Event on Value Change")]
    public UnityEvent<float> onValueChanged;

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();

        float savedValue = PlayerPrefs.GetFloat(settingKey, defaultValue);
        slider.value = savedValue;

        slider.onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        PlayerPrefs.SetFloat(settingKey, value);
        PlayerPrefs.Save();

        onValueChanged?.Invoke(value);
    }
}
