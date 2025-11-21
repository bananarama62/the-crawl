# Level Generator Prefab

![](https://github.com/bananarama62/the-crawl/blob/main/docs/jacob/PrefabGif.gif)

**Engine:** Unity (Version [6000.2.1f1])  
**Date:** 11/20/25  
★ (1999 reviews)

---

## Description

This is the prefab for the level generator. This does not include the rooms or the room database.

## Components

1. LevelGenerator script
    - This script use the LevelGenerator class to generate a map.
    - Uses the generated map to place prefab rooms

## Setup Instructions

The level manager must use the level generator script. It must also need a Unity "Database" of prefab rooms. You also need an empty object for the "Room Root".
The room root will be a parent of all the spawned rooms.
Requirements:

- Must have Room database with prefab rooms
- Must use the level generator class.
- Must have an empty object for the Rooms root. 
