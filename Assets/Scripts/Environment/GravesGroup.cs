using UnityEngine;

public class GravesGroup : MonoBehaviour
{
    [SerializeField] private float spawnRatio = 0.1f;
    [SerializeField] private float graveElementRatio = 0.2f;

    [SerializeField] private GameObject[] graveElements;
    
    void Start()
    {
        if (Random.Range(0f, 1f) > spawnRatio)
            Destroy(gameObject);
        
        GenerateGrave();
    }

    void GenerateGrave()
    {
        foreach (var graveElement in graveElements)
        {
            if (Random.Range(0f, 1f) > graveElementRatio)
                Destroy(graveElement);
        }
    }
}
