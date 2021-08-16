using System;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public enum ObjectType {
    freshie = 0,
    istd = 1,
    epd = 2
}


[Serializable]
public class ObjectPoolItem {
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
    public float spawnStartRangeX;
    public float spawnEndRangeX;
    public float spawnStartRangeY;
    public float spawnEndRangeY;
}

public class ExistingPoolItem {
    public GameObject gameObject;
    public ObjectType type;

    public ExistingPoolItem(GameObject gameObject, ObjectType type) {
        this.gameObject = gameObject;
        this.type = type;
    }
}

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<ExistingPoolItem> pooledObjects;
    private Vector3 spawnPoint;

    private void Awake() {
        SharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        foreach (var item in itemsToPool) {
            for (int i = 0; i < item.amount; i++)
            {
                spawnPoint = new Vector3(Random.Range(item.spawnStartRangeX, item.spawnEndRangeX), Random.Range(item.spawnStartRangeY,item.spawnEndRangeY), 0);
                GameObject pickup = (GameObject)Instantiate(item.prefab, spawnPoint, Quaternion.identity);
                pickup.SetActive(false);
                pickup.transform.parent = transform;
                pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
            }
            
        }
    }

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
    }

    public GameObject GetPooledObject(ObjectType type) {
        // return inactive pooled object if it matches the type
        for (var i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
            {
                return pooledObjects[i].gameObject;
            }
                
        }
            
        foreach (var item in itemsToPool)
            if (item.type == type)
                if (item.expandPool) {
                    var pickup = Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent = this.transform;
                    pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
                    return pickup;
                }

        return null;
    }
}