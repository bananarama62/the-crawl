# Fireball

https://github.com/bananarama62/the-crawl/blob/3b507501af8d8ba83cd288f9490d06897520abd0/docs/josh/prefab_demo.mkv

**Engine:** Unity (Version [6000.2.1f1])  
**Date:** 11/19/25  
★★★ (1 review)  

---

## Description
This is a prefab for an entity that performs impact damage to a player. Note that this prefab is just the red circle in the video  
## Components
1. Sprite Renderer  
   This sprite is briefly shown when the impact damage spawns  
2. Circle Collider 2D  
   This is the collider that encompasses the area that the impact damage will occur in.
   The 'Is Trigger' flag must be set to true  
3. Impact Script  
   This script is what makes the damage temporary and controls is display.
   Serialized Fields:
   * Time To Live - This is the number of seconds that the impact damage will be active for. Ex: 0.25 for a quarter of a second.
   * Damage - The amount of damage that the impact will cause. Ex: 10 for 10 damage  
## Setup Instructions
This entity is designed to be spawned by some other entity. For example, an object that falls from the sky will spawn this entity when it lands. 
Thus said object will need to Instantiate this prefab at the desired location and at the desired time. 
Requirements:
  * This will only deal damage to the player object. This object must have the tag of 'Player'
  * The Player object must have defined the function `takeDamage(int)`. This is what is called when the player is within the collision.
