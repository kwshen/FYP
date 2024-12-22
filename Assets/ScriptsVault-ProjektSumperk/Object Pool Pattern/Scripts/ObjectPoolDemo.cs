using System.Collections;
using UnityEngine;

namespace ProjektSumperk
{
    public class ObjectPoolDemo : MonoBehaviour
    {
        public ObjectPool objectPool;
        public float spawnRate = 1f;
        private float nextSpawnTime = 0f;

        private void Update()
        {
            if (Time.time >= nextSpawnTime)
            {
                GameObject obj = objectPool.GetPooledObject();
                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.SetActive(true);

                    // Deactivate the object and return it to the pool after a certain time
                    StartCoroutine(DeactivateAfterDelay(obj, 3.0f));
                }
                nextSpawnTime = Time.time + spawnRate;
            }
        }

        private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            objectPool.ReturnObjectToPool(obj);
        }
    }
}