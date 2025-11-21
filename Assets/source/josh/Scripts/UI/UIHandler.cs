/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the UIHandler class, which handles the HUD of the player.
 * Elements affected are the player's health bar and portrait, the inventory
 * icons, and the boss info bar. The class should only be interacted with 
 * with `UIHandler.instance.<function>`.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
   // Handles the HUD for the player.
{
   public static UIHandler instance { get; private set; }
   private VisualElement healthBar;// The player's health bar
   // Inventory icons
   private VisualElement icon1;
   private VisualElement icon2;
   private VisualElement icon3;

   // Boss information
   private VisualElement BossInfo;
   private VisualElement BossHealthBar;
   private Label BossName;

   private void Awake()
      // Finds and stores the various elements that need to be modified.
   {
      instance = this;
      UIDocument uiDocument = GetComponent<UIDocument>();

      // Player information elements
      healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
      icon1 = uiDocument.rootVisualElement.Q<VisualElement>("ItemSlot1").Q<VisualElement>("Icon");
      icon2 = uiDocument.rootVisualElement.Q<VisualElement>("ItemSlot2").Q<VisualElement>("Icon");
      icon3 = uiDocument.rootVisualElement.Q<VisualElement>("ItemSlot3").Q<VisualElement>("Icon");

      // Boss information elements
      BossInfo = uiDocument.rootVisualElement.Q<VisualElement>("BossInfo");
      BossHealthBar = BossInfo.Q<VisualElement>("HealthBar");
      BossName = BossInfo.Q<Label>("BossName");
      BossInfo.visible = false; // Hides boss information by default
   }


   // Sets the health bar to a percentage of full
   public void setHealthValue(float value){
      healthBar.style.width = Length.Percent(value * 100.0f);
   }

   // Calculates the health bar percentage, given a numerator and denominator
   public void setHealthValue(int current, int outof){
      setHealthValue((float)current/outof);
   }

   // Sets the icon of a hotbar slot. Supports icons 1-3.
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

   public void EnterBossEncounter(string name, float health = 1.0f){
      // Shows boss information, including the name and health bar
      BossName.text = name;
      SetBossHealth(health);
      BossInfo.visible = true;
      Debug.Log("Entering boss encounter");
   }

   public void ExitBossEncounter(){
      // Hides boss information
      BossInfo.visible = false;
      Debug.Log("Exiting boss encounter");
   }

   public void SetBossHealth(float value){
      // Updates the health bar for the boss.
      BossHealthBar.style.width = Length.Percent(value * 100.0f);
   }
}
