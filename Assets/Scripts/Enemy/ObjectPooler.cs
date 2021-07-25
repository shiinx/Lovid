using System;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake() {
        SharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        foreach (var item in itemsToPool) {
            var pickup = Instantiate(item.prefab);
            pickup.SetActive(false);
            pickup.transform.parent = transform;
            pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
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
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
                return pooledObjects[i].gameObject;
        foreach (var item in itemsToPool)
            if (item.type == type)
                if (item.expandPool) {
                    var pickup = Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent = transform;
                    pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
                    return pickup;
                }

        return null;
    }
}