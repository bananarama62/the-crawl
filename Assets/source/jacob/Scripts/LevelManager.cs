using System.Collections.Generic;
using System.Numerics;
//using Codice.Client.BaseCommands;
//using log4net.Core;
//using Mono.Cecil.Cil;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

   public RoomDatabase roomDatabase;
   public Transform LevelRoot;
   private LevelGenerator LevelGen;
   public LayerMask RoomMask;
   
   public GameObject PlayerObject;

   private PlayerController Player;

   public BoxCollider2D Exit;

   const int ROWS = 15;
   const int COLS = 15;

   const int CELL_SIZE = 64;

   const int ROOM_COUNT = 6;
   int MaxSur = 2;
   char[,] Map = new char[ROWS, COLS];

   List<Room> Rooms = new List<Room>();

   void Start()
   {

      int NumRooms = 10;
      LevelGen = new LevelGenerator(NumRooms, 1, RoomMask, LevelRoot);
      LevelGen.ArrowSpawnRoom(Map, ROWS, COLS, MaxSur);
      
      // Points used for deciding where to spawn boss room and start room
      int HighestPoint = ROWS;
      int LowestPoint = -1;
      int RightMostPoint = -1;
      int LeftMostPoint = ROWS;

      Vector2Int HStartSpawn = new Vector2Int(0, 0);
      Vector2Int HEndSpawn = new Vector2Int(0, 0);
      Vector2Int VStartSpawn = new Vector2Int(0, 0);
      Vector2Int VEndSpawn = new Vector2Int(0, 0);
      Vector2Int FinalSpawn = new Vector2Int(0,0);
      Vector2Int FinalEnd = new Vector2Int(0,0);


      for(int i = 0; i < ROWS; i++)
		{
			for(int j = 0; j < COLS; j++)
			{
				if(Map[j,i] == '#')
				{
					if(i < HighestPoint)
					{
						HighestPoint = i;
                  VEndSpawn = new Vector2Int(i, j);
					}
               if(j < LeftMostPoint)
					{
						LeftMostPoint = j;
                  HStartSpawn = new Vector2Int(i, j);
					}
               if(i > LowestPoint)
					{
						LowestPoint = i;
                  VStartSpawn = new Vector2Int(i, j);
					}
               if(j > RightMostPoint)
					{
						RightMostPoint = j;
                  HEndSpawn = new Vector2Int(i, j);
					}

				}
			}
		}

      // Check Difference between top and bottom and left and right
      // If the distance is greater top to bottom then start is bottom end is top
      Room SpawnRoom;
      Room EndRoom;
      if(RightMostPoint - LeftMostPoint < LowestPoint - HighestPoint)
		{
         // Spawn Start Room at bottom
         FinalSpawn = VStartSpawn;
         FinalEnd = VEndSpawn;
			UnityEngine.Vector3 Position = new UnityEngine.Vector3(VStartSpawn.y * CELL_SIZE, VStartSpawn.x * CELL_SIZE, 0);
         SpawnRoom = UnityEngine.Object.Instantiate(roomDatabase.rooms[6], Position, UnityEngine.Quaternion.identity, LevelRoot);
         SpawnRoom.Location = new Vector2Int(VStartSpawn.y * CELL_SIZE, VStartSpawn.x * CELL_SIZE);
         Rooms.Add(SpawnRoom);

         // Spawn End Room at Top
         Position = new UnityEngine.Vector3(VEndSpawn.y * CELL_SIZE, VEndSpawn.x * CELL_SIZE, 0);
         EndRoom = UnityEngine.Object.Instantiate(roomDatabase.rooms[7], Position, UnityEngine.Quaternion.identity, LevelRoot);
         EndRoom.Location = new Vector2Int(VEndSpawn.y * CELL_SIZE, VEndSpawn.x * CELL_SIZE);
         Rooms.Add(EndRoom);
		}
      // Spawn start at left and end at right
		else
		{
         // Spawn Start Room at Left
         FinalSpawn = HStartSpawn;
         FinalEnd = HEndSpawn;
			UnityEngine.Vector3 Position = new UnityEngine.Vector3(HStartSpawn.y * CELL_SIZE, HStartSpawn.x * CELL_SIZE, 0);
         SpawnRoom = UnityEngine.Object.Instantiate(roomDatabase.rooms[6], Position, UnityEngine.Quaternion.identity, LevelRoot);
         SpawnRoom.Location = new Vector2Int(HStartSpawn.y * CELL_SIZE, HStartSpawn.x * CELL_SIZE);
         Rooms.Add(SpawnRoom);

         // Spawn End Room at Right
         Position = new UnityEngine.Vector3(HEndSpawn.y * CELL_SIZE, HEndSpawn.x * CELL_SIZE, 0);
         EndRoom = UnityEngine.Object.Instantiate(roomDatabase.rooms[7], Position, UnityEngine.Quaternion.identity, LevelRoot);
         EndRoom.Location = new Vector2Int(HEndSpawn.y * CELL_SIZE, HEndSpawn.x * CELL_SIZE);
         Rooms.Add(EndRoom);
		}

      Map[FinalSpawn.y, FinalSpawn.x] = '/';
      Map[FinalEnd.y, FinalEnd.x] = '/';
      LevelGen.LogMap(Map);

      Exit = EndRoom.GetComponent<BoxCollider2D>();

      int RandomRoom;

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

      //Check Surround for Start and End rooms
      CheckSurround(new Vector2Int(FinalSpawn.y, FinalSpawn.x), Map, SpawnRoom);
      CheckSurround(new Vector2Int(FinalEnd.y, FinalEnd.x), Map, EndRoom);

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

      // Set Player Spawn
      Player = PlayerObject.GetComponent<PlayerController>();

      Player.SetPosition(new UnityEngine.Vector2(FinalSpawn.y * CELL_SIZE, FinalSpawn.x * CELL_SIZE));
   }

   void CheckSurround(Vector2Int Location, char[,] Map, Room NewRoom)
   {
      NewRoom.HasTopHall = false;
      NewRoom.HasBottomHall = false;
      NewRoom.HasLeftHall = false;
      NewRoom.HasRightHall = false;

      if (Location.y + 1 < ROWS && (Map[Location.x, Location.y + 1] == '#' || Map[Location.x, Location.y + 1] == '/'))
      {
         NewRoom.HasTopHall = true;
      }
      if (Location.y - 1 >= 0 && (Map[Location.x, Location.y - 1] == '#' || Map[Location.x, Location.y - 1] == '/'))
      {
         NewRoom.HasBottomHall = true;
      }
      if (Location.x - 1 >= 0 && (Map[Location.x - 1, Location.y] == '#' || Map[Location.x - 1, Location.y] == '/'))
      {
         NewRoom.HasLeftHall = true;
      }
      if (Location.x + 1 < COLS && (Map[Location.x + 1, Location.y] == '#' || Map[Location.x + 1, Location.y] == '/'))
      {
         NewRoom.HasRightHall = true;
      }
	}

}

