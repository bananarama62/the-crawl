using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class LevelGenerator
{
   private const int MAX_CELLS = 10; 
   private Room[] rooms;
   private List<Room> placedRooms = new List<Room>();
   Vector2Int WorldBoundsX = new Vector2Int(-30, 30);
   Vector2Int WorldBoundsY = new Vector2Int(-30, 30);
   private int roomMask;
   private int numRooms;
   private int levelDifficulty;
   private Transform root;
   private float cellSize = 32f;
   public LevelGenerator(int num_rooms, int difficulty, LayerMask RoomMask, Transform Root)
   {
      numRooms = num_rooms;
      levelDifficulty = difficulty;
      roomMask = RoomMask;
      root = Root;
   }

   public void SpawnStart(Room room)
   {
      room.Location = new Vector2Int(0, 0);
      UnityEngine.Vector3 position = new UnityEngine.Vector3(room.Location.x, room.Location.y, 0);
      UnityEngine.Object.Instantiate(room, position, UnityEngine.Quaternion.identity, root);
      placedRooms.Add(room);
   }
   private UnityEngine.Vector3 TileToWorld(UnityEngine.Vector3 vec)
   {
      return new UnityEngine.Vector3(vec.x / cellSize, vec.y / cellSize, 0);
   }

   public void SpawnRoom(Room room)
   {
      bool successfullPlace = false;
      Assert.NotNull(room);
      int i = 0;
      do
      {
         Room spawned = UnityEngine.Object.Instantiate(room, FindLocation(room), UnityEngine.Quaternion.identity, root);

         if (!IsValidPlacement(spawned))
         {
            Debug.Log("Room Removed");
            UnityEngine.Object.Destroy(spawned.gameObject);
         }
         else
         {
            placedRooms.Add(spawned);
            successfullPlace = true;
         }

         //In case of unplaceable room
         i++;
         if (i >= 500)
         {
            break;
         }
         //In case of unplaceable room
      
      } while (!successfullPlace);
      
   }

   public UnityEngine.Vector3 FindLocation(Room newRoom)
   {
      // WorldBounds is Vector2Int : WorldBoundsX.x is min WorldBoundsX.y is max for WorldBoundsX 
      Vector2Int randomLocation = new Vector2Int(UnityEngine.Random.Range(WorldBoundsX.x, WorldBoundsX.y + 1),
                                                UnityEngine.Random.Range(WorldBoundsY.x, WorldBoundsY.y + 1));
      //return TileToWorld(new UnityEngine.Vector3(randomLocation.x, randomLocation.y, 0));
      return new UnityEngine.Vector3(randomLocation.x, randomLocation.y, 0);
   }

   public bool IsValidPlacement(Room room)
   {
      UnityEngine.Vector2 center = room.transform.position;
      Vector2Int size = room.RoomSize;

      Collider2D[] collisions = Physics2D.OverlapBoxAll(center, size, 0, roomMask);

      for (int i = 0; i < collisions.Length; i++)
      {
         if (collisions[i] == null)
         {
            continue;
         }
         if (collisions[i].transform.IsChildOf(room.transform))
         {
            continue;
         }
         return false;
      }
      return true;
   }

   void RoomCellAlgo()
   {
      Vector2Int StartCell = new Vector2Int(0, 0);
      Vector2Int[] Cells = new Vector2Int[MAX_CELLS];
      Vector2Int LastCell = new Vector2Int(0, 0);
      Vector2Int[] Directions = 
      {
         new Vector2Int(0, 1), // Up
         new Vector2Int(0, -1), // Down
         new Vector2Int(-1, 0), // Left
         new Vector2Int(1, 0) // Right
      };

      Cells[0] = StartCell;
      int ChanceOfSplit;
      int ChanceOfDirection;
      

      for (int i = 1; i < MAX_CELLS; i++)
      {
         LastCell = Cells[i - 1];

         ChanceOfSplit = UnityEngine.Random.Range(1, 5); // 1 - 4
         ChanceOfDirection = UnityEngine.Random.Range(1, 4); // 1 - 4

         do
         {
            ChanceOfDirection = UnityEngine.Random.Range(1, 5);
            Cells[i] += Directions[ChanceOfDirection];
            if(ChanceOfSplit == 1 && i + 1 != MAX_CELLS)
            {
               
            }
         } while (Cells[i] == LastCell);
         
      }

      
   }
   
}
