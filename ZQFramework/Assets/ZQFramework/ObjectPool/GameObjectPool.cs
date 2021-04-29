using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class GameObjectPool : MonoBehaviour
    {
        public static GameObjectPool CreatePool(GameObject cloneObject)
        {
            GameObject poolGameObject = new GameObject(cloneObject.name + "Pool");

            GameObjectPool instance = poolGameObject.AddComponent<GameObjectPool>();

            instance.cloneObject = cloneObject;

            return instance;
        }

        public GameObject cloneObject;

        /// <summary>
        /// 未使用的
        /// </summary>
        public List<GameObject> unUsed = new List<GameObject>();

        public T Get<T>()
        {
            GameObject gameObject = null;
            if (unUsed.Count > 0)
            {
                gameObject = unUsed[0];
                gameObject.SetActive(true);
                gameObject.transform.SetParent(cloneObject.transform.parent);
                unUsed.RemoveAt(0);
            }
            else
            {
                gameObject = cloneObject.Clone("", true);
            }
        
            return gameObject.GetComponent<T>();
        }
        public void Recycle(GameObject gameObject)
        {
            gameObject.SetActive(false);

            gameObject.transform.SetParent(this.transform);

            unUsed.Add(gameObject);
        }
    }
}


