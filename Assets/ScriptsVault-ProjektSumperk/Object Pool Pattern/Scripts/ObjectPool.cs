using System.Collections.Generic;
using UnityEngine;

namespace ProjektSumperk
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject objectToPool;
        public int poolSize;
        public Transform parent;
        public List<GameObject> pooledObjects;

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectToPool, parent);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}