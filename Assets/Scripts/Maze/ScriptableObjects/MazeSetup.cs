using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Maze Setup", fileName = "MazeSetup")]
public class MazeSetup : ScriptableObject
{
    public int columns;
    public int rows;
    
    public int totalNumberOfRooms;
    public int minTrunkNumberOfRooms;
    public int maxTrunkNumberOfRooms;

    public bool isSpawningAnimated;
    public float roomSpawnDelay;

    public int RandomTrunkNumberOfRooms => Random.Range(minTrunkNumberOfRooms, maxTrunkNumberOfRooms);
}
