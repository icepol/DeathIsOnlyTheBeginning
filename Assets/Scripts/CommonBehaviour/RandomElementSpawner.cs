using UnityEngine;

public class RandomElementSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    
    void Start()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
    }
}
