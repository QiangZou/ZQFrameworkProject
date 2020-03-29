using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace ZQFramwork
{
    public class AssetBundlesTool : Editor
    {
        /// <summary>
        /// 清理所有的BundleName
        /// </summary>
        static void CleanAllBundleNames()
        {
            string[] names = AssetDatabase.GetAllAssetBundleNames();

            if (names == null) return;

            for (int i = 0; i < names.Length; ++i)
            {
                EditorUtility.DisplayProgressBar("CleanAllBundleNames", "CleanAllBundleNames", i / (float)names.Length);

                AssetDatabase.RemoveAssetBundleName(names[i], true);
            }

            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        ///获取所有资源路径
        /// </summary>
        /// <returns></returns>
        static List<string> GetAllAssetBundlePath()
        {
            List<string> paths = new List<string>();

            //找到目录里所有文件
            string[] files = Directory.GetFiles(LoadManager.assetBundleReadPath, "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                EditorUtility.DisplayProgressBar("GetAllAssetBundlePath", "GetAllAssetBundlePath", i / (float)files.Length);

                string path = files[i];

                if (path.EndsWith(".meta")) continue;

                if (path.EndsWith(".cs")) continue;

                paths.Add(path);
            }

            EditorUtility.ClearProgressBar();

            return paths;
        }

        /// <summary>
        /// 设置资源名字
        /// </summary>
        [MenuItem("ZQFramwork/AssetBundles/设置名字")]
        static void SetAssetBundleName()
        {
            CleanAllBundleNames();

            List<string> paths = GetAllAssetBundlePath();

            for (int i = 0; i < paths.Count; i++)
            {
                EditorUtility.DisplayProgressBar("SetAssetBundleName", "SetAssetBundleName", i / (float)paths.Count);

                string path = paths[i];

                

                path = path.Replace('\\', '/');
                path = path.Replace(Application.dataPath, "Assets");

                

                AssetImporter assetImporter = AssetImporter.GetAtPath(path);

                if (assetImporter == null) continue;

                string assetBundleName = assetImporter.assetPath.Replace("Assets/", "");


                assetImporter.assetBundleName = assetBundleName;
            }

            EditorUtility.ClearProgressBar();

            
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            Debug.Log("SetAssetBundleName Succeed ");
        }

        [MenuItem("ZQFramwork/AssetBundles/打包")]
        static void BuildAssetBundles()
        {
            //若文件夹不存在则新建文件夹  
            if (!Directory.Exists(LoadManager.assetBundleSavePath))
            {
                Directory.CreateDirectory(LoadManager.assetBundleSavePath);
            }

            //打包资源
            BuildPipeline.BuildAssetBundles(LoadManager.assetBundleSavePath, BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);

            Debug.Log("BuildAssets Succeed");
        }

    }
}


