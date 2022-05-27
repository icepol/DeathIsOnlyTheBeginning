using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WallPosition
{
    Left,
    Right,
    Front,
    Back,
}

public class MazeRoom
{
    public bool isRoot;
    public bool isFloorRoot;

    public List<WallPosition> freeWalls;
    public List<WallPosition> openWalls;
    public Dictionary<WallPosition, MazeRoom> connectedRooms;
    
    private MazeRoom _parent;
    private WallPosition _position;

    public int Number { get; set; }
    public List<MazeRoom> AllRooms { get; set; }

    public string Name => _parent == null ? "root" : $"{_parent.Name} - {Number} [{_position}]";

    public int X { get; private set; }
    public int Z { get; private set; }

    public Vector3 RoomPosition => new(X, 0, Z);

    public bool HasFreeWall => FreeWallsCount > 0;
    public int FreeWallsCount => freeWalls.Count;

    public MazeRoomView RoomView { get; set; }

    public MazeRoom()
    {
        freeWalls = new List<WallPosition>
        {
            WallPosition.Back,
            WallPosition.Left,
            WallPosition.Right,
            WallPosition.Front
        };

        openWalls = new List<WallPosition>();

        connectedRooms = new Dictionary<WallPosition, MazeRoom>();
    }

    public bool SetParent(MazeRoom parent, WallPosition position)
    {
        _parent = parent;
        _position = position;

        X = parent.X;
        Z = parent.Z;
        
        WallPosition connectedWall;

        switch (position)
        {
            case WallPosition.Left:
                X = parent.X - 1;
                connectedWall = WallPosition.Right;
                break;
            case WallPosition.Right:
                X = parent.X + 1;
                connectedWall = WallPosition.Left;
                break;
            case WallPosition.Front:
                Z = parent.Z + 1;
                connectedWall = WallPosition.Back;
                break;
            case WallPosition.Back:
                connectedWall = WallPosition.Front;
                Z = parent.Z - 1;
                break;
            default:
                throw new Exception("Unknown wall position value.");
        }
        
        if (AllRooms.Any(room => room.X == X && room.Z == Z))
            // this position is occupied already
            return false;
        
        openWalls.Add(connectedWall);
        
        UpdateFreeWalls();

        return true;
    }

    private void UpdateFreeWalls()
    {
        if (AllRooms.Any(room => room.X == X - 1 && room.Z == Z))
            freeWalls.Remove(WallPosition.Left);
        
        if (AllRooms.Any(room => room.X == X + 1 && room.Z == Z))
            freeWalls.Remove(WallPosition.Right);
        
        if (AllRooms.Any(room => room.Z == Z - 1 && room.X == X))
            freeWalls.Remove(WallPosition.Back);
        
        if (AllRooms.Any(room => room.Z == Z + 1 && room.X == X))
            freeWalls.Remove(WallPosition.Front);
    }

    public bool AddChildToRandomWall(MazeRoom childRoom)
    {
        // pick the wall / position
        var freeWall = freeWalls[Random.Range(0, freeWalls.Count)];
        return AddChildRoom(freeWall, childRoom);
    }

    public bool AddChildRoom(WallPosition position, MazeRoom childRoom)
    {
        connectedRooms[position] = childRoom;

        if (!childRoom.SetParent(this, position))
            // room hasn't been added
            return false;
        
        freeWalls.Remove(position);
        openWalls.Add(position);

        return true;
    }

    public void RemoveChildRoom(WallPosition position)
    {
        connectedRooms.Remove(position);
        freeWalls.Add(position);
    }

    public void OpenWall(WallPosition wallPosition)
    {
        if (!openWalls.Contains(wallPosition))
            openWalls.Add(wallPosition);
    }

    public void CloseWall(WallPosition wallPosition)
    {
        if (openWalls.Contains(wallPosition))
            openWalls.Add(wallPosition);
    }
}
