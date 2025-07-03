using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    public GameObject player;
    public bool isAiming;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            player.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
          //  player.transform.rotation = Quaternion.LookRotation(0, 0, 0); 
        }

    }
    public void OnAiming(InputValue value)
    {
        isAiming = value.isPressed;
    }
}
