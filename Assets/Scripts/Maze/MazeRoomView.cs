using System;
using UnityEngine;

public class MazeRoomView : MonoBehaviour
{
    [SerializeField] private float roomSize = 1;
    
    [SerializeField] private MazeWall wallFront;
    [SerializeField] private MazeWall wallBack;
    [SerializeField] private MazeWall wallLeft;
    [SerializeField] private MazeWall wallRight;

    [SerializeField] private GameObject grave;

    private bool _isFirstVisualization;

    public MazeRoom Room { get; private set; }
    
    public void Visualize(MazeRoom mazeRoom)
    {
        Room = mazeRoom;
        Room.RoomView = this;
        
        transform.localPosition = mazeRoom.RoomPosition * roomSize;
        gameObject.name = mazeRoom.Name;

        foreach (var openWall in mazeRoom.openWalls)
        {
            switch (openWall)
            {
                case WallPosition.Back:
                    wallBack.Open();
                    break;
                case WallPosition.Front:
                    wallFront.Open();
                    break;
                case WallPosition.Left:
                    wallLeft.Open();
                    break;
                case WallPosition.Right:
                    wallRight.Open();
                    break;
                default:
                    throw new Exception("Unknown wall position value.");
            }
        }
        
        if (mazeRoom.isRoot)
            Destroy(grave);
    }
}
