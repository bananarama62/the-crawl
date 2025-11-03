using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public RoomDatabase roomDatabase;
    public Transform levelRoot;
    private LevelGenerator levelGenerator;
    public LayerMask roomMask;
    void Start()
    {
        int numRooms = 10;
        levelGenerator = new LevelGenerator(numRooms, 1, roomMask, levelRoot);
        levelGenerator.SpawnStart(roomDatabase.rooms[0]);
        for(int i = 0; i <= numRooms; i++)
        {
            levelGenerator.RandSpawnRoom(roomDatabase.rooms[0]);
        }
        
    }

}
