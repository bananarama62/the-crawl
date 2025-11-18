# Fireball

https://github.com/user-attachments/assets/7ca6bc5c-4c16-40bc-8b3e-3f17cd697ffc

**Engine:** Unity (Version [6000.2.1f1])  
**Date:** 11/14/25  
★★★★★ (1943 reviews)  

---

## Description
This is a prefab for a basic fireball ability in Unity   
## Components
1. Fireball Behaviour Script  
   Controls fireball movement and detection on enemy box colliders and wall to deal damage  
   Can change damage and ball speed variables  
2. Sprite Renderer  
   Displays fireball sprite  
   Ensure display layer is set according to your scene to properly display  
3. Circle Collider 2D  
   Allows fireball to be detected by other entities in the scene  
   Ensure to set collider to boundaries of sprite for proper detection  
4. Rigid Body 2D  
   Manages Physics movement for fireball  
   Ensure Collision detection is set to continuous so fireball does not phase through objects  
5. Audio Source  
   Plays audio when object is generated in scene  
   Drag desired audio resource into inspector and ensure play on awake is checked  
## Setup Instructions
Create a gameobject that will use the fireball, then drag provided script fireball from zip folder onto the object, create a empty gameobject as a child of the first object and name it Cooldown<-- this is a requirement then attach the script Cooldown from the zip folder to this object. In the parent object attach the fireball prefab to the Ball slot in the inspector under Fireball script and then you will need to set a button to use the ability, the scripts have no restrictions on this so you can do as you will.
