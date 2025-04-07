using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    public Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void CreatePool(string key, GameObject prefab, int initialSize)
    {
        prefabDictionary[key] = prefab;
        poolDictionary[key] = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolDictionary[key].Enqueue(obj);
        }
    }

    public GameObject GetFromPool(string key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogError($"Pool {key} doesn't exist!");
            return null;
        }

        GameObject obj = poolDictionary[key].Count > 0
            ? poolDictionary[key].Dequeue()
            : Instantiate(prefabDictionary[key]);

        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(string key, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[key].Enqueue(obj);
    }
}
