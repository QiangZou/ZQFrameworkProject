using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public enum AssetLoadType
    {
        /// <summary>
        /// Unity使用默认资源加载
        /// </summary>
        UnityDefault,
        /// <summary>
        /// Unity使用加载本地AssetBundle
        /// </summary>
        UnityLocalAssetBundle,
        /// <summary>
        /// Unity使用下载AssetBundle
        /// </summary>
        UnityDownloadAssetBundle,
        /// <summary>
        /// 设备使用本地AssetBundle
        /// </summary>
        DeviceLocalAssetBundle,
        /// <summary>
        /// 设备使用下载AssetBundle
        /// </summary>
        DeviceDownloadAssetBundle,
    }

    public class AssetManager : MonoBehaviour
    {
        static AssetManager instance;

        public static AssetManager me
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("AssetManager");
                    instance = go.AddComponent<AssetManager>();
                }
                return instance;
            }
        }

        void Awake()
        {
            //防止场景默认就有这个组建
            instance = this;
        }

        public AssetLoadType assetLoadType;
        IAssetLoad assetLoad;

        public void Load(string path, Action<UnityEngine.Object> completed)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            assetLoad = GetAssetLoad(assetLoadType);

            if (assetLoad == null)
            {
                return;
            }

            StartCoroutine(assetLoad.Load(path, completed));
        }

        IAssetLoad GetAssetLoad(AssetLoadType assetLoadType)
        {
            switch (assetLoadType)
            {
                case AssetLoadType.UnityDefault: return new UnityDefault();
                case AssetLoadType.UnityLocalAssetBundle: return new UnityLocalAssetBundle();
                case AssetLoadType.UnityDownloadAssetBundle:
                case AssetLoadType.DeviceLocalAssetBundle:
                case AssetLoadType.DeviceDownloadAssetBundle: return new DeviceDownloadAssetBundle();
            }
            return null;
        }


        void LoadDefaultAsset(string path, Action<UnityEngine.Object> completed)
        {
            UnityEngine.Object obj = null;

            path = string.Format("Assets/{0}", path);

#if UNITY_EDITOR
            obj = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
#endif

            if (completed != null)
            {
                completed(obj);
            }
        }

        IEnumerator LoadAssetBundle(string path, Action<UnityEngine.Object> completed)
        {
            if (completed == null) yield break;

            path = path.ToLower();

            //加载路径
            string newPath = Application.dataPath.Replace("Assets", "").Replace("\\", "/") + "/AssetBundles/" + path;


            AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(newPath);

            yield return assetBundleCreateRequest;

            var assetBundle = assetBundleCreateRequest.assetBundle;
            if (assetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }

            AssetBundleRequest assetLoadRequest = assetBundle.LoadAssetAsync<GameObject>(path);
            yield return assetLoadRequest;

            completed(assetLoadRequest.asset);

            assetBundle.Unload(false);
        }

    }

}


