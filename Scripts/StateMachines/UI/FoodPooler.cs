using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPooler : MonoBehaviour {

    public GameObject pooledObject;
    public Sprite[] foodSprites;
    public int pooledAmount = 5;
    List<GameObject> pooledObjects;

    void Start() {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++) {
            GameObject obj = (GameObject)Instantiate(pooledObject, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                int r = Random.Range(0, foodSprites.Length);
                Sprite f = foodSprites[r];
                pooledObjects[i].GetComponent<Food>().SetSprite(f);
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject FindNearest(Vector3 position) {
        GameObject o = null;
        float dist = Mathf.Infinity;
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (pooledObjects[i].activeInHierarchy) {
                float newDist = Vector3.Distance(pooledObjects[i].transform.position, position);
                if (newDist < dist) {
                    o = pooledObjects[i];
                    dist = newDist;
                }
            }
        }
        return o;
    }
}
