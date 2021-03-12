using UnityEngine;
using System.Collections.Generic;

namespace ZQFramwork
{
    public class PoolManager : MonoBehaviour
    {
        protected static PoolManager instance = null;

        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject poolManager = new GameObject("PoolManager");

                    instance = poolManager.AddComponent<PoolManager>();

                    DontDestroyOnLoad(poolManager);
                }

                return instance;
            }
        }

        private Dictionary<string, PrefabPool> allPrefabPool = new Dictionary<string, PrefabPool>();

        public PrefabPool CreationPool(string name, Transform prefab, int preloadAmount)
        {
            if (allPrefabPool.ContainsKey(name))
            {
                Debug.LogError("创建重复的对象池:" + name);
                return null;
            }

            PrefabPool prefabPool = new PrefabPool(prefab, preloadAmount);

            allPrefabPool.Add(name, new PrefabPool(prefab, preloadAmount));

            return prefabPool;
        }
    }
}


