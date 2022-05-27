using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] treePrefabs;
    
    void Start()
    {
        Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], transform);
    }
}
