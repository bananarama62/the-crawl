using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement healthBar;

    private void Awake()
    {
        instance = this;
        UIDocument uiDocument = GetComponent<UIDocument>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
    }


    // Sets the health bar to a percentage of full
    public void setHealthValue(float value){
        healthBar.style.width = Length.Percent(value * 100.0f);
    }

    // Calculates the health bar percentage, given a numerator and denominator
    public void setHealthValue(int current, int outof){
      setHealthValue((float)current/outof);
    }

}
