using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class ResourcesManager : MonoSingleton<ResourcesManager>
    {

        /// <summary>
        /// 加载对象.
        /// </summary>
        /// <param name="assetPaths">路径格式"Assets/Resources/a1.prefab"</param>
        /// <returns></returns>
        public static Object Load(string assetPaths)
        {
            //float startTime = Time.realtimeSinceStartup;

            Object prefab = Resources.Load(assetPaths);

            //prefab = UnityEditor.AssetDatabase.LoadMainAssetAtPath(assetPaths);

            //float elapsedTime = Time.realtimeSinceStartup - startTime;

            ////Debug.Log(assetPaths + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");

            return prefab;
        }



    }
}


