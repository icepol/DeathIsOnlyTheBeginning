using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private ObjectPoolPrewarmSetup[] objectPoolPrewarmSetups;
    
    private const int POOL_GROW_SIZE = 10;
    
    private static readonly Dictionary<string, GameObject> _parents = new Dictionary<string, GameObject>();
    private static readonly Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    public static ObjectPoolManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Prewarm();
    }

    private void Prewarm()
    {
        if (objectPoolPrewarmSetups == null) return;

        foreach (ObjectPoolPrewarmSetup prewarmSetup in objectPoolPrewarmSetups)
        {
            CreatePool(prewarmSetup.gameObject, prewarmSetup.count);
        }
    }

    public GameObject GetFromPool(GameObject prefab) {
        // create pool if doesn't exists yet
        if (!_pools.ContainsKey(prefab.name))
            CreatePool(prefab);

        // grow pool if there isn't available item
        if (_pools[prefab.name].Count == 0)
            GrowPool(prefab);

        GameObject instance = _pools[prefab.name].Dequeue();
        instance.SetActive(true);

        return instance;
    }

    public void ReturnToPool(GameObject instance) {
        instance.SetActive(false);
        
        instance.transform.SetParent(GetPoolParentTransform(instance.name));

        _pools[instance.name].Enqueue(instance);
    }

    private Transform GetPoolParentTransform(string prefabName)
    {
        if (!_parents.ContainsKey(prefabName))
        {
            GameObject parent = new GameObject {name = $"{prefabName}Pool"};
            parent.transform.parent = transform;
            
            _parents[prefabName] = parent;
        }

        return _parents[prefabName].transform;
    }

    private void CreatePool(GameObject prefab, int growSize = POOL_GROW_SIZE) {
        _pools[prefab.name] = new Queue<GameObject>();

        GrowPool(prefab, growSize);
    }

    void GrowPool(GameObject prefab, int growSize = POOL_GROW_SIZE)
    {
        Transform poolParentTransform = GetPoolParentTransform(prefab.name);
        
        for (int i = 0; i < growSize; i++) {
            GameObject instance = Instantiate(prefab, poolParentTransform);
            
            instance.name = prefab.name;
            instance.SetActive(false);

            _pools[prefab.name].Enqueue(instance);
        }
    }
}