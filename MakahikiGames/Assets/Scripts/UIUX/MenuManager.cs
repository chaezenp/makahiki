using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject OptionsMenu;
    public GameObject firstButtonInOptions;
    public GameObject firstButtonInPauseMenu;
    public PlayerInputHandler playerInputHandler;
    public FPPlayerInputHandler FPplayerInputHandler;
    public PlayerInputHandler overlayCameraInputHandler;
    public FPMove fpMove;

    public static bool gameIsPaused = false; // Static variable to track pause state


    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                optionBack();
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        if (playerInputHandler != null)
        {
            playerInputHandler.isInputEnabled = false;
        }
        if (FPplayerInputHandler != null)
        {
            FPplayerInputHandler.isInputEnabled = false;
        }
        if (overlayCameraInputHandler != null)
        {
            overlayCameraInputHandler.isInputEnabled = false;
        }
        if (fpMove != null)
        {
            fpMove.isPaused = true;
        }
    }

    public void ResumeGame()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        if (playerInputHandler != null)
        {
            playerInputHandler.isInputEnabled = true;
        }
        if (FPplayerInputHandler != null)
        {
            FPplayerInputHandler.isInputEnabled = true;
        }
        if (overlayCameraInputHandler != null)
        {
            overlayCameraInputHandler.isInputEnabled = true;
        }
        if (fpMove != null)
        {
            fpMove.isPaused = false;
        }
    }

    public void optionMenu()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        optionsFirstButton();
    }
    public void optionBack()
    {
        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        PauseFirstButton();
    }
    public void QuitGame()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }

    void optionsFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(firstButtonInOptions.gameObject);
    }
            void PauseFirstButton()
        {
            EventSystem.current.SetSelectedGameObject(null); 

            EventSystem.current.SetSelectedGameObject(firstButtonInPauseMenu.gameObject);
        }
}
