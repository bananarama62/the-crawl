using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores UI script for Mainmenu ui
/// </summary>
public class MainMenuScript : MonoBehaviour
{
    private UIDocument doc;
    private VisualElement menu;
    private Button buttonQuit;
    private Button buttonPlay;
    [SerializeField] private ClassPIckUI wow;
    /// <summary>
    /// sets necessary components and establishes what function each button calls when pressed
    /// </summary>
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
    /// <summary>
    /// starts game, and disables main menu and then enables class pick ui
    /// </summary>
    public void PlayGame()
    {
        Debug.Log("Starting game...");
        wow.ShowMenu();
        menu.visible = false;
    }
    /// <summary>
    /// quits game, only works in exe form
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    /// <summary>
    /// function necessary for inter menu communication
    /// </summary>
    /// <param name="input"></param>
    public void setScene(bool input)
    {
        menu.visible = input;
    }
}
