using System.Numerics;
using log4net.Core;
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

    const int RoomCount = 1;
    int MaxSur = 2;
    char[,] Map = new char[ROWS, COLS];

    void Start()
    {
        /* int numRooms = 10;
        levelGenerator = new LevelGenerator(numRooms, 1, roomMask, levelRoot);
        levelGenerator.SpawnStart(roomDatabase.rooms[0]);
        for(int i = 0; i <= numRooms; i++)
        {
            levelGenerator.RandSpawnRoom(roomDatabase.rooms[0]);
        } */
        int NumRooms = 10;
        LevelGen = new LevelGenerator(NumRooms, 1, RoomMask, LevelRoot);
        LevelGen.ArrowSpawnRoom(Map, ROWS, COLS, MaxSur);

        // TODO: Get random room
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                if(Map[j,i] == '#')
                {
                    roomDatabase.rooms[0].Location = new Vector2Int(i * CELL_SIZE, j * CELL_SIZE);
                    UnityEngine.Vector2 Position = new UnityEngine.Vector3(roomDatabase.rooms[0].Location.x, roomDatabase.rooms[0].Location.y, 0);
                    UnityEngine.Object.Instantiate(roomDatabase.rooms[0], Position, UnityEngine.Quaternion.identity, LevelRoot);
                }
                
            }
        }

    }

    /* room.Location = new Vector2Int(0, 0);
    UnityEngine.Vector3 position = new UnityEngine.Vector3(room.Location.x, room.Location.y, 0);
    UnityEngine.Object.Instantiate(room, position, UnityEngine.Quaternion.identity, Root);
    PlacedRooms.Add(room); */
}
