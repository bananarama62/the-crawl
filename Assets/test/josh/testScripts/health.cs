using UnityEngine;
using System;

// Testable version of the health functions in the Character class
public class Health{

  public void die() {
    ;
  }


  // Sets the current health to the specified value (Must be 0 or greater)
  // Returns true on error (attempted to set to negative value)
  public bool setHealth(int new_health){
    int current_health = 0;
    if (new_health >= 0) {
      current_health = new_health;
      if (current_health == 0){
        die();
      }
      return false;
    } else {
      return true;
    }
  }

  // Modifies the current health by an integer. Positive values
  // increase health and negative values decrease it.
  // Returns the current health after modification
  public int modifyHealth(int modify_health_by, int current_health){
    current_health += modify_health_by;
    if (current_health < 0){
      current_health = 0;
    }
    if (current_health == 0){
      die();
    }
    return current_health;
  }

  // Modifies the current health by a percentage. Default is to modify by a 
  // percentage of the max health. Setting ofMaxHealth to false will use
  // the max health for the percentage calculation. Percentage should
  // be an integer of the number of percentage points (50% means enter 50)
  // Returns the current health after modification.
  // Ex: percentage = 50, ofMaxHealth = true. If the max health is 200, then
  // this will add 50% of 200 to the current health. (Input -50 to subtract that
  // amount instead). If ofMaxHealth was equal to false, then it add 50% of the
  // current health
  public int modifyHealthByPercentage(int percentage, int current_health, int max_health, bool ofMaxHealth=true){
    // Decides which health value to use for percentage calculations
    int health = current_health;
    if (ofMaxHealth) {
      health = max_health;
    }
    // Applies percentage and gets value to modify health by
    int modify_amount = (health * percentage) / 100; 
    current_health += modify_amount;
    if (current_health == 0){
      die();
    }
    return current_health;
  }

}
