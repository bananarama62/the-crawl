using UnityEngine;
using UnityEngine.UIElements;
public class MainMenuScript : MonoBehaviour
{
    private UIDocument doc;
    private VisualElement menu;
    private Button buttonQuit;
    private Button buttonPlay;
    [SerializeField] private ClassPIckUI wow;
    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        //wow = GetComponent<ClassPIckUI>();
        menu = doc.rootVisualElement;
        buttonQuit = menu.Q<Button>("Exit");
        buttonQuit.clicked += QuitGame;
        buttonPlay = menu.Q<Button>("Play");
        buttonPlay.clicked += PlayGame;
    }
    // quits the game
    public void PlayGame()
    {
        Debug.Log("Starting game...");
        wow.ShowMenu();
        menu.visible = false;
    }
    // quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
