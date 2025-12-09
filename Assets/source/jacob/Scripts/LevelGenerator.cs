using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Codice.CM.Common;
using Codice.CM.WorkspaceServer.Tree;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class LevelGenerator
{
   private Room[] Rooms;
   private List<Room> PlacedRooms = new List<Room>();
   Vector2Int WorldBoundsX = new Vector2Int(-30, 30);
   Vector2Int WorldBoundsY = new Vector2Int(-30, 30);
   private int RoomMask;
   private int MaxCells;
   private int LevelDifficulty;
   private Transform Root;
   private float CellSize = 32f;
   public LevelGenerator(int NumRooms, int Difficulty, LayerMask roomMask, Transform root)
   {
      MaxCells = NumRooms;
      LevelDifficulty = Difficulty;
      RoomMask = roomMask;
      Root = root;
   }

   public void SpawnStart(Room room)
   {
      room.Location = new Vector2Int(0, 0);
      UnityEngine.Vector3 position = new UnityEngine.Vector3(room.Location.x, room.Location.y, 0);
      UnityEngine.Object.Instantiate(room, position, UnityEngine.Quaternion.identity, Root);
      PlacedRooms.Add(room);
   }
   private UnityEngine.Vector3 TileToWorld(UnityEngine.Vector3 vec)
   {
      return new UnityEngine.Vector3(vec.x / CellSize, vec.y / CellSize, 0);
   }

   public void RandSpawnRoom(Room room)
   {
      bool successfullPlace = false;
      //Assert.NotNull(room);
      int i = 0;
      do
      {
         Room spawned = UnityEngine.Object.Instantiate(room, FindLocation(room), UnityEngine.Quaternion.identity, Root);

         if (!IsValidPlacement(spawned))
         {
            Debug.Log("Room Removed");
            UnityEngine.Object.Destroy(spawned.gameObject);
         }
         else
         {
            PlacedRooms.Add(spawned);
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

      Collider2D[] collisions = Physics2D.OverlapBoxAll(center, size, 0, RoomMask);

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

   public void ArrowSpawnRoom(char[,] Map, int ROWS, int COLS, int MaxSur)
   {
      for (int Row = 0; Row < ROWS; Row++)
      {
         for (int Col = 0; Col < COLS; Col++)
         {
            Map[Row, Col] = '-';
         }
      }

      // Set center of map to room
      Map[ROWS / 2 + 1, COLS / 2 + 1] = '#';

      LogMap(Map);

      int TopArrowPosition;
      int BottomArrowPosition;
      int LeftArrowPosition;
      int RightArrowPosition;

      int SurroundCount;
      int PrevPosition;

      int Cells = 1;
      Vector2Int Position = new Vector2Int(0, 0);

      while (Cells < MaxCells)
      {
         TopArrowPosition = UnityEngine.Random.Range(0, COLS);
         BottomArrowPosition = UnityEngine.Random.Range(0, COLS);
         LeftArrowPosition = UnityEngine.Random.Range(0, ROWS);
         RightArrowPosition = UnityEngine.Random.Range(0, ROWS);

         for (int i = 0; i < ROWS - 1; i++)
         {
            if (i != 0)
            {
               PrevPosition = i - 1;
            }
            else
            {
               PrevPosition = 0;
            }

            if (Map[i, TopArrowPosition] == '#')
            {
               Position.y = PrevPosition;
               Position.x = TopArrowPosition;
               SurroundCount = CheckSurround(Map, Position);

               if (SurroundCount <= MaxSur && Cells <= MaxCells)
               {
                  Debug.Log("TopArrow");
                  Map[Position.y, Position.x] = '#';
                  LogMap(Map);
                  Cells++;
                  break;
               }
            }
         }

         if (Cells >= MaxCells)
         {
            break;
         }

         for (int i = ROWS - 1; i >= 0; i--)
         {
            if (i != ROWS)
            {
               PrevPosition = i + 1;
            }
            else
            {
               PrevPosition = ROWS - 1;
            }

            if (Map[i, BottomArrowPosition] == '#')
            {
               Position.y = PrevPosition;
               Position.x = BottomArrowPosition;
               SurroundCount = CheckSurround(Map, Position);

               if (SurroundCount <= MaxSur && Cells <= MaxCells)
               {
                  Map[Position.y, Position.x] = '#';
                  LogMap(Map);
                  Cells++;
                  break;
               }
            }
         }

         if (Cells >= MaxCells)
         {
            break;
         }

         for (int i = 0; i < COLS - 1; i++)
         {
            if (i != 0)
            {
               PrevPosition = i - 1;
            }
            else
            {
               PrevPosition = 0;
            }

            if (Map[LeftArrowPosition, i] == '#')
            {
               Position.y = LeftArrowPosition;
               Position.x = PrevPosition;
               SurroundCount = CheckSurround(Map, Position);

               if (SurroundCount <= MaxSur && Cells <= MaxCells)
               {
                  Debug.Log("LeftArrow");
                  Map[Position.y, Position.x] = '#';
                  LogMap(Map);
                  Cells++;
                  break;
               }
            }
         }

         if (Cells >= MaxCells)
         {
            break;
         }

         for (int i = COLS - 1; i >= 0; i--)
         {
            if (i != COLS - 1)
            {
               PrevPosition = i + 1;
            }
            else
            {
               PrevPosition = COLS - 1;
            }

            if (Map[RightArrowPosition, i] == '#')
            {
               Position.y = RightArrowPosition;
               Position.x = PrevPosition;
               SurroundCount = CheckSurround(Map, Position);

               if (SurroundCount <= MaxSur && Cells <= MaxCells)
               {
                  Map[Position.y, Position.x] = '#';
                  LogMap(Map);
                  Cells++;
                  break;
               }
            }
         }
      }
      LogMap(Map);
   }

   private int CheckSurround(char[,] Map, Vector2Int Pos)
   {
      int Count = 0;
      if (Map[Pos.y + 1, Pos.x] == '#')
      {
         Count++;
      }
      if (Map[Pos.y - 1, Pos.x] == '#')
      {
         Count++;
      }
      if (Map[Pos.y, Pos.x + 1] == '#')
      {
         Count++;
      }
      if (Map[Pos.y, Pos.x - 1] == '#')
      {
         Count++;
      }

      return Count;
   }

   public void LogMap(char[,] map)
   {
      int rows = map.GetLength(0);
      int cols = map.GetLength(1);
      var sb = new StringBuilder(rows * (cols + 1)); // +1 for newline

      for (int r = 0; r < rows; r++)
      {
         for (int c = 0; c < cols; c++)
         {
            sb.Append(map[r, c]);
            sb.Append("  ");
         }
         sb.AppendLine(); // newline after each row
      }

      Debug.Log(sb.ToString());
   }
}


