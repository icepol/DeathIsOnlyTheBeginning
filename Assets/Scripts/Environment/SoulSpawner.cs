using System.Collections;
using System.Linq;
using pixelook;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private Soul soulPrefab;
    
    [SerializeField] private MazeSpawner mazeSpawner;
    
    [SerializeField] private float spawnDelay = 2.5f;
    private void Awake()
    {
        EventManager.AddListener(Events.MAZE_GENERATION_FINISHED, OnMazeGenerationFinished);
        EventManager.AddListener(Events.SOUL_SAVED, OnSoulSaved);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.MAZE_GENERATION_FINISHED, OnMazeGenerationFinished);
        EventManager.RemoveListener(Events.SOUL_SAVED, OnSoulSaved);
    }

    private void OnMazeGenerationFinished()
    {
        StartCoroutine(WaitAndSpawn());
    }

    private void OnSoulSaved()
    {
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(soulPrefab, WhereToSpawn(), Quaternion.identity, transform);
    }

    Vector3 WhereToSpawn()
    {
        var availableRooms = mazeSpawner.MazeRooms.Where(r => !r.isRoot).ToList();

        return availableRooms[Random.Range(0, availableRooms.Count)].RoomView.transform.position;
    }
}
