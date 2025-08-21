using UnityEngine;

public class InvisibleUIFix : MonoBehaviour
{
[SerializeField] private GameObject invisibleObject;

    private void OnEnable()
    {
        if (invisibleObject != null)
        {
            invisibleObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Invisible UI Object not assigned in DisableInvisibleUI script on " + gameObject.name);
        }
    }
}
