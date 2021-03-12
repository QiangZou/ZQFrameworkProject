using UnityEngine;
using System.Collections.Generic;

namespace ZQFramwork
{
    public class PrefabPool
    {
        /// <summary>
        /// 预设
        /// </summary>
        public Transform prefab;

        /// <summary>
        /// 最大保存数量
        /// </summary>
        public int preloadAmount = 1;

        /// <summary>
        /// 未使用的
        /// </summary>
        public List<Transform> unUsed = new List<Transform>();

        /// <summary>
        /// 正在使用
        /// </summary>
        public List<Transform> used = new List<Transform>();

        public PrefabPool(Transform prefab, int preloadAmount)
        {
            this.prefab = prefab;
            this.preloadAmount = preloadAmount;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public Transform Get()
        {
            Transform inst = null;


            if (unUsed.Count > 0)
            {
                inst = unUsed[0];
                unUsed.Remove(inst);
                used.Add(inst);

                //inst.gameObject.SetActive(true);

                return inst;
            }
            //判断界限
            else if (Limit())
            {
                return inst;
            }

            //实例化一个
            inst = Instance();
            used.Add(inst);

            return inst;
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="transform"></param>
        public void Recycle(Transform transform)
        {
            transform.gameObject.SetActive(false);

            transform.SetParent(PoolManager.Instance.transform);

            used.Remove(transform);

            unUsed.Add(transform);
        }

        /// <summary>
        /// 获取创建的所有数量
        /// </summary>
        /// <returns></returns>
        int GetAllCount()
        {
            return unUsed.Count + used.Count;
        }

        /// <summary>
        /// 获取是否超过界限
        /// </summary>
        /// <returns></returns>
        bool Limit()
        {
            return  GetAllCount() > preloadAmount;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <returns></returns>
        Transform Instance()
        {
            GameObject gameObject = prefab.gameObject.Clone("", true);
            Transform transform = gameObject.transform;

            return transform;
        }
    }
}

