using System.Collections;
using System.Linq;
using pixelook;
using UnityEngine;

public class FenixSpawner : MonoBehaviour
{
    [SerializeField] private Fenix fenixPrefab;
    
    [SerializeField] private MazeSpawner mazeSpawner;
    
    [SerializeField] private float firstSpawnDelay = 3f;
    [SerializeField] private float killedRespawnDelay = 1f;

    private void Awake()
    {
        EventManager.AddListener(Events.MAZE_GENERATION_FINISHED, OnMazeGenerationFinished);
        EventManager.AddListener(Events.FENIX_KILLED, OnFenixKilled);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(Events.MAZE_GENERATION_FINISHED, OnMazeGenerationFinished);
        EventManager.RemoveListener(Events.FENIX_KILLED, OnFenixKilled);
    }

    private void OnMazeGenerationFinished()
    {
        StartCoroutine(WaitAndSpawn());
    }

    private void OnFenixKilled(Vector3 position)
    {
        StartCoroutine(WaitAndRespawn(position));
    }

    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        Instantiate(fenixPrefab, WhereToSpawn(), Quaternion.identity, transform);
    }

    IEnumerator WaitAndRespawn(Vector3 position)
    {
        yield return new WaitForSeconds(killedRespawnDelay);

        // second one will be spawned on the random position
        Instantiate(fenixPrefab, WhereToSpawn(), Quaternion.identity, transform);
        
        yield return new WaitForSeconds(killedRespawnDelay);
        
        // spawn on the position where Fenix was killed
        Instantiate(fenixPrefab, position, Quaternion.identity, transform);
    }

    Vector3 WhereToSpawn()
    {
        var availableRooms = mazeSpawner.MazeRooms.Where(r => !r.isRoot).ToList();

        return availableRooms[Random.Range(0, availableRooms.Count)].RoomView.transform.position;
    }
}
