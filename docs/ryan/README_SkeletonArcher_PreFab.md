# Skeleton Archer PreFab


https://github.com/user-attachments/assets/69061cc0-dd7a-4b96-b4c5-1e7e4a66a579

**Engine**: Unity (Version [6000.2.1f1])\
**Date**: 11/19/25

## **Description**: PreFab for the Skeleton Archer enemy

## Components

### 1. Scripts
  - Enemy: Handles Base enemy initialization.
  - EnemyController: Handles enemy movement logic.
  - EnemyFactory/EnemySpawner: Factory class design to spawn enemy.
  - SkeletonArcher: Enemy's base stats, movement, and attack logic.
  - SkeletonArrow: Arrow PreFab used for attacks.

### 2. Sprite Renderer
  - Displays Skeleton Archer sprite.

### 3. Box Collider 2D
  - Allows for contact with the player and wweapons.

### 4. Rigidbody 2D
  - Allows for physics based movement and collison detection for enemy.

### 5. Circle Collider 2D
  - Allows for enemy's field of view and attack radius

### 6. Animator
  - Used to animate the states of the enemy.
