using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject OptionsMenu;
    public GameObject ControlsMenu;
    public GameObject SoundMenu;
    public GameObject keyboardCont;
    public GameObject controllerCont;
    public GameObject firstButtonInOptions;
    public GameObject firstButtonInPauseMenu;
    public GameObject firstButtonInControlsMenu;
    public GameObject firstButtonInMusicMenu;
    public PlayerInputHandler playerInputHandler;
    public FPPlayerInputHandler FPplayerInputHandler;
    public PlayerInputHandler overlayCameraInputHandler;
    public FPMove fpMove;
    public bool inDialogue;

    public static bool gameIsPaused = false; // Static variable to track pause state


    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        SoundMenu.SetActive(false);
        inDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inDialogue)
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
        controlsBack();
        optionBack();
        MusicBack();
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
    public void ControlMenu()
    {
        PauseMenu.SetActive(false);
        ControlsMenu.SetActive(true);
        ControlsFirstButton();
    }
    public void controlsBack()
    {
        ControlsMenu.SetActive(false);
        PauseMenu.SetActive(true);
        PauseFirstButton();
    }
    public void MusicMenu()
    {
        PauseMenu.SetActive(false);
        SoundMenu.SetActive(true);
        MusicFirstButton();
    }
    public void MusicBack()
    {
        SoundMenu.SetActive(false);
        PauseMenu.SetActive(true);
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

    void ControlsFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(firstButtonInControlsMenu.gameObject);
    }
    public void KeyBoardControlsSwitch()
    {
        keyboardCont.SetActive(true);
        controllerCont.SetActive(false);
    }
    public void ControllerControlsSwitch()
    {
        keyboardCont.SetActive(false);
        controllerCont.SetActive(true);
    }
    
        void MusicFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(firstButtonInMusicMenu.gameObject);
    }
}
