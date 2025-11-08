using UnityEngine;
using UnityEngine.UIElements;
public class ClassPIckUI : MonoBehaviour
{
    private UIDocument doc;
    private VisualElement ClassMenu;
    private Button buttonArcher;
    private Button buttonMage;
    [SerializeField] private PlayerController PlayerCon;
    private void Awake()
    {
        //PlayerCon = GetComponent<PlayerController>();
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClassMenu.visible = false;
    }

    public void ShowMenu()
    {
        ClassMenu.visible = true;
    }
}
