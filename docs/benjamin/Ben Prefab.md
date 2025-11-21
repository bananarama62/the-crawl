# Bow and Arrow

![](https://github.com/bananarama62/the-crawl/blob/main/docs/benjamin/benGif.gif)

**Engine:** Unity (Version [6000.2.1f1])  
**Date:** 11/20/25  
★★★ (1 review)

---

## Description

This is a prefab for an bow that spawns arrow projectiles. Note that this is just the prefab for the bow, not for the arrows.

## Components

1. Sprite Renderer  
   This sprite is shown rotating around the player
2. Bow Script  
   This is the script that handles the majority of the logic for the bow
3. Weapon Script  
   This script is what opperates the damage and cooldown of the bow
   Serialized Fields:
   - Damage - The amount of damage that the impact will cause. Ex: 10 for 10 damage
   - Effect - What type of weapon will be triggered (bow)
   - Icon - The Icon that displays in the player hotbar
   - Caster - The source where the arrows fire from
   - Swing Sound - The sound used

## Setup Instructions

This entity is designed to be spawned by some other entity. For example, a player will equip this weapon and control it while moving around.
The object will be instantiated and will spawn arrow prefabs from itself.
Requirements:

- This will only deal damage to the enemy object. This object must have the tag of 'enemy'
- There must be a defined arrow prefab for this to be useable
