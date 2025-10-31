using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    private VisualElement menu;
    private bool isPaused = false;
    private InputAction Cancel;
    private Button buttonQuitGame;
    private Button buttonResumeGame;

    [SerializeField] private AudioSource backgroundMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        menu = uiDocument.rootVisualElement;
        buttonQuitGame = menu.Q<Button>("ExitGame");
        buttonQuitGame.clicked += QuitGame;
        buttonResumeGame = menu.Q<Button>("ResumeGame");
        buttonResumeGame.clicked += Resume;
        Cancel = InputSystem.actions.FindAction("Cancel");

    }

    void Start()
    {
        menu.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cancel.WasPressedThisFrame())
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming...");
        menu.visible = false;
        Time.timeScale = 1f; // Resume game time
        backgroundMusic.UnPause();
        isPaused = false;
    }

    void Pause()
    {
        Debug.Log("Pausing...");
        menu.visible = true;
        Time.timeScale = 0f; // Freeze game time
        backgroundMusic.Pause();
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
