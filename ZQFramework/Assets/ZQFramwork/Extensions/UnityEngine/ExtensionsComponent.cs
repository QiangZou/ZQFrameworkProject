using UnityEngine;
using System.Collections;

namespace ZQFramwork
{
    public static class ExtensionsComponent
    {

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="self"></param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static T GetComponent<T>(this Component self, string path) where T : Component
        {
            Component target = self.transform.Find(path);

            if (target == null)
            {
                return null;
            }

            T component = target.GetComponent<T>();

            return component;
        }
    }

}
