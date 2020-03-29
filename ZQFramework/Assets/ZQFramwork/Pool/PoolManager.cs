using UnityEngine;
using System.Collections.Generic;

namespace ZQFramwork
{
    public class PoolManager : MonoBehaviour
    {
        protected static PoolManager mInstance = null;

        public static PoolManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    GameObject poolManager = new GameObject("PoolManager");

                    mInstance = poolManager.AddComponent<PoolManager>();

                    DontDestroyOnLoad(poolManager);
                }

                return mInstance;
            }
        }

        public Dictionary<string, PrefabPool> AllPrefabPool = new Dictionary<string, PrefabPool>();

        public PrefabPool CreationPool(string name, Transform prefab, int preloadAmount)
        {
            if (AllPrefabPool.ContainsKey(name))
            {
                Debug.LogError("创建重复的对象池:" + name);
                return null;
            }

            PrefabPool prefabPool = new PrefabPool(prefab, preloadAmount);

            AllPrefabPool.Add(name, new PrefabPool(prefab, preloadAmount));

            return prefabPool;
        }

        void Awake()
        {
            //transform.gameObject.SetActive(false);
        }
    }
}


