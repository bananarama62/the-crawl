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

   const int ROWS = 15;
   const int COLS = 15;

   const int CELL_SIZE = 32;

   const int ROOM_COUNT = 6;
   int MaxSur = 2;
   char[,] Map = new char[ROWS, COLS];

   List<Room> Rooms = new List<Room>();

   void Start()
   {

      int NumRooms = 10;
      LevelGen = new LevelGenerator(NumRooms, 1, RoomMask, LevelRoot);
      LevelGen.ArrowSpawnRoom(Map, ROWS, COLS, MaxSur);

      int RandomRoom = -1;

      // TODO: Get random room
      for (int i = 0; i < ROWS; i++)
      {
         for (int j = 0; j < COLS; j++)
         {
            if (Map[j, i] == '#')
            {
               UnityEngine.Vector3 Position = new UnityEngine.Vector3(j * CELL_SIZE, i * CELL_SIZE, 0);
               RandomRoom = UnityEngine.Random.Range(0, ROOM_COUNT);
               Room NewRoom = UnityEngine.Object.Instantiate(roomDatabase.rooms[RandomRoom], Position, UnityEngine.Quaternion.identity, LevelRoot);
               NewRoom.Location = new Vector2Int(j * CELL_SIZE, i * CELL_SIZE);
               CheckSurround(new Vector2Int(j, i), Map, NewRoom);
               Rooms.Add(NewRoom);
            }
         }
      }

      for(int i = 0; i < Rooms.Count; i++)
      {
         // Set hallways
         Rooms[i].TopHall.SetActive(Rooms[i].HasTopHall);
         Rooms[i].BottomHall.SetActive(Rooms[i].HasBottomHall);
         Rooms[i].LeftHall.SetActive(Rooms[i].HasLeftHall);
         Rooms[i].RightHall.SetActive(Rooms[i].HasRightHall);

         // Set Doorways
         Rooms[i].TopDoor.SetActive(!Rooms[i].HasTopHall);
         Rooms[i].BottomDoor.SetActive(!Rooms[i].HasBottomHall);
         Rooms[i].LeftDoor.SetActive(!Rooms[i].HasLeftHall);
         Rooms[i].RightDoor.SetActive(!Rooms[i].HasRightHall);      
		}
   }

   void CheckSurround(Vector2Int Location, char[,] Map, Room NewRoom)
   {
      NewRoom.HasTopHall = false;
      NewRoom.HasBottomHall = false;
      NewRoom.HasLeftHall = false;
      NewRoom.HasRightHall = false;

      if (Location.y + 1 < ROWS && Map[Location.x, Location.y + 1] == '#')
      {
         NewRoom.HasTopHall = true;
      }
      if (Location.y - 1 >= 0 && Map[Location.x, Location.y - 1] == '#')
      {
         NewRoom.HasBottomHall = true;
      }
      if (Location.x - 1 >= 0 && Map[Location.x - 1, Location.y] == '#')
      {
         NewRoom.HasLeftHall = true;
      }
      if (Location.x + 1 < COLS && Map[Location.x + 1, Location.y] == '#')
      {
         NewRoom.HasRightHall = true;
      }
	}
}

