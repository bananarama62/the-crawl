using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores UI script for class picker ui
/// </summary>
public class ClassPIckUI : MonoBehaviour
{
    private UIDocument doc;
    private VisualElement ClassMenu;
    private Button buttonArcher;
    private Button buttonMage;
    [SerializeField] private PlayerController PlayerCon;
    /// <summary>
    /// sets necessary components and establishes what function each button calls when pressed
    /// </summary>
    private void Awake()
    {
        doc = GetComponent<UIDocument>();
        ClassMenu = doc.rootVisualElement;
        buttonArcher = ClassMenu.Q<Button>("Archer");
        buttonArcher.clicked += () =>
            {
                PlayerCon.setClass(new Archer());
                ClassMenu.visible = false;
            };
            buttonMage = ClassMenu.Q<Button>("Mage");
            buttonMage.clicked += () =>
            {
                PlayerCon.setClass(new Mage());
                ClassMenu.visible = false;
            };
        }
    /// <summary>
    /// sets classmenu visibility to false on startup, if not done it will draw over other menus
    /// </summary>
    void Start()
    {
        ClassMenu.visible = false;
    }
    /// <summary>
    /// function necessary for inter menu communication
    /// </summary>
    public void ShowMenu()
    {
        ClassMenu.visible = true;
    }
}
