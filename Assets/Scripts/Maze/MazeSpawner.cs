using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pixelook;
using Unity.AI.Navigation;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private MazeRoomView mazeRoomViewPrefab;
    [SerializeField] private MazeSetup mazeSetup;

    private bool _isRootSpawned;
    private int _spawnedRoomsCount;

    private List<MazeRoom> _mazeRooms;

    public List<MazeRoom> MazeRooms => _mazeRooms;

    private void Awake()
    {
        EventManager.AddListener(Events.MAZE_GENERATION_FINISHED, OnMazeGenerationFinished);
    }

    void Start()
    {
        InitializeMaze();

        var root = SpawnRoot();
        SpawnRooms(FindNextParentRoom(), mazeSetup.RandomTrunkNumberOfRooms);

        Visualize();
    }

    void OnDestroy()
    {
        EventManager.RemoveListener(Events.MAZE_GENERATION_FINISHED, OnMazeGenerationFinished);
    }

    void OnMazeGenerationFinished()
    {
        foreach (var room in _mazeRooms)
        {
            var surface = room.RoomView.GetComponentInChildren<NavMeshSurface>();
            surface.BuildNavMesh();
        }
    }

    void InitializeMaze()
    {
        _mazeRooms = new List<MazeRoom>();
    }
    
    MazeRoom SpawnRoot()
    {
        var mazeRoom = new MazeRoom
        {
            isRoot = true,
            Number = 0,
            AllRooms = _mazeRooms,
        };

        _mazeRooms.Add(mazeRoom);
        _spawnedRoomsCount++;

        return mazeRoom;
    }

    void SpawnRooms(MazeRoom parentRoom, int numberOfRooms)
    {
        while (true)
        {
            // limit the number of rooms we will spawn
            numberOfRooms = Mathf.Min(numberOfRooms, parentRoom.FreeWallsCount);
            
            for (var i = 0; i < numberOfRooms; i++)
            {
                var childRoom = new MazeRoom
                {
                    Number = _spawnedRoomsCount, 
                    AllRooms = _mazeRooms,
                };

                if (!parentRoom.AddChildToRandomWall(childRoom))
                    continue;

                _mazeRooms.Add(childRoom);
                _spawnedRoomsCount++;
            }

            if (_spawnedRoomsCount >= mazeSetup.totalNumberOfRooms)
                // we have enough rooms
                return;

            parentRoom = FindNextParentRoom();
            numberOfRooms = mazeSetup.RandomTrunkNumberOfRooms;
        }
    }

    MazeRoom FindNextParentRoom()
    {
        // find rooms where we can attach new room
        var rooms = _mazeRooms.Where(r => r.HasFreeWall).ToList();
        
        // pick the one where we will attach new room
        var nextParentRoom = rooms[Random.Range(0, rooms.Count)];

        return nextParentRoom;
    }

    void Visualize()
    {
        foreach (var room in _mazeRooms)
        {
            var spawned = Instantiate(mazeRoomViewPrefab, transform);
            spawned.Visualize(room);
        }

        StartCoroutine(TriggerFinishedEvent());
    }

    IEnumerator TriggerFinishedEvent()
    {
        // wait for one frame to init all objects
        yield return null;
        
        EventManager.TriggerEvent(Events.MAZE_GENERATION_FINISHED);
    }
}
