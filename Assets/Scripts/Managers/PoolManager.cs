using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [OdinSerialize]
    public List<GameObject> poolPrefabs = new List<GameObject>();
    
    [OdinSerialize]
    [DictionaryDrawerSettings(KeyLabel = "Custom Key Name", ValueLabel = "Custom Value Label")]
    public Dictionary<string, Queue<GameObject>> poolQueues =
        new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;

        foreach (var poolPrefab in poolPrefabs)
        {
            InitPool(poolPrefab, 3);
        }
    }

    public void InitPool(GameObject poolPrefab, int count, Transform parent = null)
    {
        if (poolPrefab == null)
            return;
        
        if(!poolQueues.ContainsKey(poolPrefab.name))
        {
            poolPrefabs.Add(poolPrefab);
            poolQueues.Add(poolPrefab.name, new Queue<GameObject>());
        }

        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(poolPrefab, parent, true);
            poolQueues[poolPrefab.name].Enqueue(go);
            go.SetActive(false);
        }
    }

    public GameObject Spawn(string key)
    {
        if(!poolQueues.ContainsKey(key) || poolQueues[key].Count <= 0) 
            InitPool(poolPrefabs.Find(x => x.name == key), 3);
        var go = poolQueues[key].Dequeue();
        go.SetActive(true);
        return go;
    }

    public void Despawn(GameObject obj)
    {
        poolQueues[obj.name.Split('(')[0]].Enqueue(obj);
        obj.SetActive(false);
    }
    
}
