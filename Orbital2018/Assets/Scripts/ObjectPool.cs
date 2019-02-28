using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int countToPool;
    public bool canExpand = true;

    public ObjectPoolItem(GameObject obj, int amt, bool exp = true)
    {
        objectToPool = obj;
        countToPool = Mathf.Max(amt, 2);
        canExpand = exp;
    }
}

public class ObjectPool : MonoBehaviour {

    public static ObjectPool instance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    public List<List<GameObject>> pooledObjectsList;
    private List<int> positions;

    

    private void Awake()
    {
        instance = this;
        
        pooledObjects = new List<GameObject>();
        pooledObjectsList = new List<List<GameObject>>();
        positions = new List<int>();

        for (int i = 0; i < itemsToPool.Count; i++)
        {
            ObjectPoolItemToPooledObject(i);
        }
       
    }

    public GameObject GetPooledObject(int index)
    {

        int curSize = pooledObjectsList[index].Count;
        for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
        {

            if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
            {
                positions[index] = i % curSize;
                return pooledObjectsList[index][i % curSize];
            }
        }

        if (itemsToPool[index].canExpand)
        {

            GameObject obj = (GameObject)Instantiate(itemsToPool[index].objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjectsList[index].Add(obj);
            return obj;

        }
        return null;
    }


    void ObjectPoolItemToPooledObject(int index)
    {
        ObjectPoolItem item = itemsToPool[index];

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < item.countToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(item.objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjects.Add(obj);
        }
        pooledObjectsList.Add(pooledObjects);
        positions.Add(0);
    }


}
