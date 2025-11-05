using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
   public static UIHandler instance { get; private set; }
   private VisualElement healthBar;
   private VisualElement icon1;
   private VisualElement icon2;
   private VisualElement icon3;

   private void Awake()
   {
      instance = this;
      UIDocument uiDocument = GetComponent<UIDocument>();
      healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
      icon1 = uiDocument.rootVisualElement.Q<VisualElement>("ItemSlot1").Q<VisualElement>("Icon");
      icon2 = uiDocument.rootVisualElement.Q<VisualElement>("ItemSlot2").Q<VisualElement>("Icon");
      icon3 = uiDocument.rootVisualElement.Q<VisualElement>("ItemSlot3").Q<VisualElement>("Icon");
   }


   // Sets the health bar to a percentage of full
   public void setHealthValue(float value){
      healthBar.style.width = Length.Percent(value * 100.0f);
   }

   // Calculates the health bar percentage, given a numerator and denominator
   public void setHealthValue(int current, int outof){
      setHealthValue((float)current/outof);
   }

   // Sets the icon of a hotbar slot
   public int setIcon(int IconNumber, Sprite image){
      VisualElement icon_to_modify = null;
      if(IconNumber == 1){
         icon_to_modify = icon1;
      }
      else if(IconNumber == 2){
         icon_to_modify = icon2;
      }
      else if(IconNumber == 3){
         icon_to_modify = icon3;
      }
      if (icon_to_modify != null){
         icon_to_modify.style.backgroundImage = new StyleBackground(image);
         return 0;
      }
      return 1;
   }
}
