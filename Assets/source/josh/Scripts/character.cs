using UnityEngine;
using System;

public abstract class Character : MonoBehaviour {
  private int current_health; // Current health of the player
  private int base_health; // Starting max health of the player. Should not be changed
  private int max_health; // Effective max health (modifiable by things like armor)
  [SerializeField] private float speed;

  public virtual void die(){ // Called when health is set to 0
    Destroy(gameObject);
  }

  // Returns the current health
  public int getHealth(){
    return current_health;
  }

  // Sets the current health to the specified value (Must be 0 or greater)
  // Returns true on error (attempted to set to negative value)
  public bool setHealth(int new_health){
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
  public int modifyHealth(int modify_health_by){
    current_health += modify_health_by;
    if (current_health < 0){
      current_health = 0;
    }
    if (current_health == 0){
      die();
    }
    if (current_health > max_health){
      current_health = max_health;
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
  public int modifyHealthByPercentage(int percentage, bool ofMaxHealth=true){
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
    if (current_health > max_health){
      current_health = max_health;
    }
    return current_health;
  }

  // Wrapper for getHealth() to adhere to naming convention.
  public int getCurrentHealth(){
    return getHealth();
  }

  // Returns current health / max health; This is the percentage
  // of health the player is currently at.
  public float getCurrentHealthPercentage(){
    return (float)(current_health)/max_health;
  }

  // Wrapper for setHealth() to adhere to naming convention.
  public bool setCurrentHealth(int new_health){
    return setHealth(new_health);
  }

  // Wrapper for modifyHealth() to adhere to naming convention.
  public int modifyCurrentHealth(int modify_health_by){
    return modifyHealth(modify_health_by);
  }

  // Wrapper for modifyHealthByPercentage() to adhere to naming convention.
  public int modifyCurrentHealthByPercentage(int percentage, bool ofMaxHealth=true){
    return modifyHealthByPercentage(percentage,ofMaxHealth);
  }

  // Returns the base health
  public int getBaseHealth(){
    return base_health;
  }

  // Sets the base health (must be greater than or equal to 0).
  // Returns true if attempting to set to a negative value
  public bool setBaseHealth(int new_health){
    if (new_health >= 0) {
      base_health = new_health;
      return false;
    } else {
      return true;
    }
  }

  // Returns the max health
  public int getMaxHealth(){
    return max_health;
  }

  // Sets the max health (must be greater than or equal to 0).
  // Returns true if attempting to set to a negative value.
  public bool setMaxHealth(int new_health){
    if (new_health >= 0) {
      max_health = new_health;
      return false;
    } else {
      return true;
    }
  }

  // Modifies the max health by an integer (use positive to add health
  // and negative to subtract health). Returns the max health after the
  // modification
  public int modifyMaxHealth(int modify_health_by){
    max_health += modify_health_by;
    if (max_health < 0){
      max_health = 0;
    }
    return max_health;
  }


  // Returns the speed
  public float getSpeed(){
    return speed;
  }

  // Sets speed. (Must be greater than or equal to 0).
  // Returns true when attempting to set to negative value.
  public bool setSpeed(float new_speed){
    if (new_speed >= 0f) {
      speed = new_speed;
      return false;
    } else {
      return true;
    }
  }

  // Performs additive modification of the speed variable.
  // Returns the speed value after modification
  public float modifySpeed(float modify_speed_by){
    speed += modify_speed_by;
    if (speed < 0){
      speed = 0f;
    }
    return speed;
  }

  // Sets the base health, max health, current health, and speed variables.
  // Base health is the only required value. If max health is not supplied, it will
  // be set to base health value. If current health is not supplied, it will
  // be set to the max health value. Speed defaults to 0.
  public void initHealthAndSpeed(int set_base_health, int set_max_health=-1, int set_current_health=-1,float speed=0f){
    if(setBaseHealth(set_base_health)){ // Setting base health resulted in error -- less than 0.
      throw new System.ArithmeticException("Base health must be greater than or equal to 0.");
    }
    // Sets max_health to provided value or defaults to same value as base_health
    if (set_max_health > -1){
      setMaxHealth(set_max_health);
    } else {
      setMaxHealth(base_health);
    }

    // Sets current_health to provided value or defaults to same value as base_health
    if (set_current_health > -1){
      setHealth(set_current_health);
    } else {
      setHealth(max_health);
    }

    setSpeed(speed);
  }

}
