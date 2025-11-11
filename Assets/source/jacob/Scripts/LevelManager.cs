using System.Collections.Generic;
using System.Numerics;
using Codice.Client.BaseCommands;
using log4net.Core;
using Mono.Cecil.Cil;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

   public RoomDatabase roomDatabase;
   public Transform LevelRoot;
   private LevelGenerator LevelGen;
   public LayerMask RoomMask;

   public GameObject VerticalHall;
   public GameObject HorizontalHall;

   const int ROWS = 15;
   const int COLS = 15;

   const int CELL_SIZE = 32;

   const int RoomCount = 1;
   int MaxSur = 2;
   char[,] Map = new char[ROWS, COLS];

   List<Room> Rooms = new List<Room>();

   public struct Surround
   {
      public Surround(bool up = false, bool down = false, bool left = false, bool right = false)
      {
         Up = up;
         Down = down;
         Left = left;
         Right = right;
      }
      public bool Up;
      public bool Down;
      public bool Left;
      public bool Right;

   }

   void Start()
   {

      int NumRooms = 10;
      LevelGen = new LevelGenerator(NumRooms, 1, RoomMask, LevelRoot);
      LevelGen.ArrowSpawnRoom(Map, ROWS, COLS, MaxSur);

      // TODO: Get random room
      for (int i = 0; i < ROWS; i++)
      {
         for (int j = 0; j < COLS; j++)
         {
            if (Map[j, i] == '#')
            {
               roomDatabase.rooms[0].Location = new Vector2Int(i * CELL_SIZE, j * CELL_SIZE);
               UnityEngine.Vector2 Position = new UnityEngine.Vector3(roomDatabase.rooms[0].Location.x, roomDatabase.rooms[0].Location.y, 0);
               UnityEngine.Object.Instantiate(roomDatabase.rooms[0], Position, UnityEngine.Quaternion.identity, LevelRoot);
               Rooms.Add(roomDatabase.rooms[0]);

            }
         }
      }
   }
}

