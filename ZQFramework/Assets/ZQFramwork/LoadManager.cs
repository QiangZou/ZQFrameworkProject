using UnityEngine;
using System.Collections;
using System;

namespace ZQFramwork
{
    public enum LoadType
    {
        Editor,
        AssetBundle
    }

    public class LoadManager : MonoSingleton<LoadManager>
    {
        /// <summary>
        /// 保存AssetBundles路径
        /// </summary>
        public static string assetBundleSavePath
        {
            get { return Application.dataPath + "/../AssetBundles"; }
        }

        /// <summary>
        /// 读取AssetBundles路径
        /// </summary>
        public static string assetBundleReadPath
        {
            get { return Application.dataPath + "/R"; }
        }

        /// <summary>
        /// 加载类型
        /// </summary>
        public static LoadType loadType = LoadType.Editor;

        public LoadManager()
        {

        }

        private void Awake()
        {
#if UNITY_EDITOR
            loadType = (LoadType)PlayerPrefs.GetInt("LoadType", 0);
#else
			loadType = LoadType.AssetBundle;
#endif
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Load(string path, Action<UnityEngine.Object> completed)
        {
            if (completed == null)
            {
                return;
            }

            switch (loadType)
            {
                case LoadType.Editor:
                    EditorLoad(path, completed);
                    break;
                case LoadType.AssetBundle:
                    StartCoroutine(AssetBundleLoad(path, completed));
                    break;
                default:
                    break;
            }

        }

        void EditorLoad(string path, Action<UnityEngine.Object> completed)
        {
            if (completed == null)
            {
                return;
            }

            path = string.Format("Assets/{0}", path);

            UnityEngine.Object obj = null;

#if UNITY_EDITOR
            obj = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
#endif

            if (completed != null)
            {
                completed(obj);
            }
        }

        IEnumerator AssetBundleLoad(string path, Action<UnityEngine.Object> completed)
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
