using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;
    [SerializeField] private int poolCount;
    List<GameObject> poolList = new List<GameObject>();

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPool()
    {
        for(int i=0; i<poolCount; i++)
        {
            GameObject clone = Instantiate(poolObject, transform);
            clone.SetActive(false);
            poolList.Add(clone);
        }
    }

    public void AddToPool(GameObject obj)
    {
        poolList.Add(obj);
        obj.SetActive(false);
    }

    public GameObject GetFromPool()
    {
        GameObject obj = poolList[poolList.Count-1];
        obj.SetActive(true);
        poolList.Remove(obj);
        return obj;

    }
}
