using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ZQFramwork
{

    public class AssetBundleManager : MonoSingleton<AssetBundleManager> 
    {
        AssetBundleManifest assetBundleManifest;

        public Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();

        void Awake()
        {
            LoadAssetBundleManifest();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        /// <summary>
        /// 加载AssetBundleManifest
        /// </summary>
        void LoadAssetBundleManifest()
        {
            string path = GetAssetBundleSavePath() + "/AssetBundle";

            LoadAssetBundle(path, (assetBundle) =>
            {
                assetBundleManifest = (AssetBundleManifest)assetBundle.LoadAsset("AssetBundleManifest");
            });
        }

        /// <summary>
        /// 加载Object
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <param name="async">异步</param>
        public void LoadObject(string path, Action<UnityEngine.Object> callback, bool async = true)
        {
            if (callback == null) return;

            GetAssetBundle(path, async, (assetBundle) =>
             {
                 string name = GetAssetName(path);

                 GetAsset(assetBundle, name, async, callback);
             });
        }

        /// <summary>
        /// 加载Scene
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <param name="async">异步</param>
        public void LoadScene(string path, Action callback, bool async = false)
        {
            if (callback == null) return;

            GetAssetBundle(path, async, (assetBundle) =>
             {
                 if (assetBundle != null)
                 {
                     callback();
                 }
             });
        }

        /// <summary>
        /// 获取AssetBundle(同时加载依赖)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <param name="async"></param>
        void GetAssetBundle(string path, bool async, Action<AssetBundle> callback)
        {
            if (callback == null)
            {
                return;
            }

            //如果已经加载过的AssetBundle
            if (this.assetBundles.ContainsKey(path))
            {
                AssetBundle assetBundle = this.assetBundles[path];

                callback(assetBundle);

                return;
            }

            //获取依赖文件列表;  
            string[] allDependencies = assetBundleManifest.GetAllDependencies(path);

            //加载所有的依赖文件;  
            //AssetBundle[] assetBundles = new AssetBundle[allDependencies.Length];
            for (int i = 0; i < allDependencies.Length; i++)
            {
                string dependenciesPath = allDependencies[i];

                GetOneAssetBundle(dependenciesPath, async, (assetBundle) =>
                 {
                     //这里也要添加到集合
                 });
            }

            string assetBundlePath = GetAssetBundleSavePath() + "/" + path;

            GetOneAssetBundle(assetBundlePath, async, (assetBundle) =>
            {
                //添加assetBundle到集合
                AddAssetBundles(path, assetBundle);

                callback(assetBundle);
            });
        }

        /// <summary>
        /// 获取一个AssetBundle(确认没依赖)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="async"></param>
        /// <param name="callback"></param>
        void GetOneAssetBundle(string path, bool async, Action<AssetBundle> callback)
        {
            if (async)
            {
                LoadAssetBundle(path, (assetBundle) =>
                {
                    callback(assetBundle);
                });
            }
            else
            {
                StartCoroutine(LoadAssetBundleAsync(path, (assetBundle) =>
                {
                    callback(assetBundle);
                }));
            }
        }


        void GetAsset(AssetBundle assetBundle, string name, bool async, Action<UnityEngine.Object> callback)
        {
            if (assetBundle == null || callback == null) return;

            if (async)
            {
                LoadAsset(assetBundle, name, callback);
            }
            else
            {
                StartCoroutine(LoadAssetAsync(assetBundle, name, callback));
            }
        }






        /// <summary>
        /// 同步加载AssetBundle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        void LoadAssetBundle(string path, Action<AssetBundle> callback)
        {
            if (callback == null) return;

            Debug.Log("LoadAssetBundle path : " + path);

            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);

            callback(assetBundle);
        }

        /// <summary>
        /// 同步加载Asset
        /// </summary>
        /// <param name="assetBundle"></param>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        void LoadAsset(AssetBundle assetBundle, string name, Action<UnityEngine.Object> callback)
        {
            if (assetBundle == null || callback == null) return;

            UnityEngine.Object obj = assetBundle.LoadAsset(name);

            callback(obj);
        }

        /// <summary>
        /// 协程加载AssetBundle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        IEnumerator LoadAssetBundleAsync(string path, Action<AssetBundle> callback)
        {
            if (callback == null) yield return null;

            AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(path);

            yield return assetBundleCreateRequest;

            AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;

            callback(assetBundle);
        }

        /// <summary>
        /// 协程加载asset
        /// </summary>
        /// <param name="assetBundle"></param>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        IEnumerator LoadAssetAsync(AssetBundle assetBundle, string name, Action<UnityEngine.Object> callback)
        {
            if (assetBundle == null || callback == null) yield return null;

            AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync(name);

            yield return assetBundleRequest;

            if (assetBundleRequest != null)
            {
                callback(assetBundleRequest.asset);
            }

            yield return null;
        }













        /// <summary>
        /// 添加AssetBundle到集合
        /// </summary>
        /// <param name="path"></param>
        /// <param name="assetBundle"></param>
        void AddAssetBundles(string path, AssetBundle assetBundle)
        {
            if (assetBundles.ContainsKey(path))
            {
                assetBundles[path] = assetBundle;
            }
            else
            {
                assetBundles.Add(path, assetBundle);
            }
        }

        /// <summary>
        /// 获取资源名字 根据路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAssetName(string path)
        {
            string name = string.Empty;

            if (string.IsNullOrEmpty(path) == false)
            {
                string[] temp = path.Split(new string[] { "/" }, StringSplitOptions.None);

                name = temp[temp.Length - 1];
            }

            return name;
        }

        /// <summary>
        /// 获取AssetBundle保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetAssetBundleSavePath()
        {
            string path = string.Empty;

            if (Application.platform == RuntimePlatform.Android)
            {
                path = Application.dataPath + "!assets/AssetBundle";
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = Application.persistentDataPath + "/AssetBundle";

                path = Application.streamingAssetsPath + "/AssetBundle";
            }
            else
            {
                //pc上读取路径
                path = Application.dataPath + "/../AssetBundle";
            }

            Debug.Log("GetAssetBundleSavePath path : " + path);

            return path;
        }
    }

}