using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine.UI;

namespace ZQFramwork
{
    public class PrefabsTool : EditorWindow
    {
        //添加菜单项 &#1 Shift+Alt+2
        [MenuItem("ZQFramwork/Prefab(预设)/检查预设为空")]
        static void CheckNullPrefab()
        {
            List<string> paths = EditorHelper.GetAllFilePaths();

            List<string> prefabPaths = EditorHelper.GetAllPrefabFilePaths(paths);

            if (prefabPaths == null)
            {
                return;
            }

            for (int i = 0; i < prefabPaths.Count; i++)
            {
                string prefabPath = prefabPaths[i];

                //修改路径格式
                prefabPath = EditorHelper.ChangeFilePath(prefabPath);

                AssetImporter tmpAssetImport = AssetImporter.GetAtPath(prefabPath);

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(tmpAssetImport.assetPath);

                if (prefab == null)
                {
                    Debug.LogError("空的预设 ：" + tmpAssetImport.assetPath);
                }

                //进度条
                float progressBar = (float)i / prefabPaths.Count;
                EditorUtility.DisplayProgressBar("检查预设为空", "进度 ：" + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }



            EditorUtility.ClearProgressBar();

            Debug.Log("完成检查预设为空");
        }


        [MenuItem("ZQFramwork/Prefab(预设)/检查预设中文")]
        static void CheckChinesePrefabs()
        {
            List<string> paths = EditorHelper.GetAllFilePaths();

            List<string> prefabPaths = EditorHelper.GetAllPrefabFilePaths(paths);

            if (prefabPaths == null)
            {
                return;
            }

            for (int i = 0; i < prefabPaths.Count; i++)
            {
                string prefabPath = prefabPaths[i];

                //修改路径格式
                prefabPath = EditorHelper.ChangeFilePath(prefabPath);

                AssetImporter tmpAssetImport = AssetImporter.GetAtPath(prefabPath);

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(tmpAssetImport.assetPath);

                if (prefab == null)
                {
                    continue;
                }

              
                Text[] uiLabels = prefab.GetComponentsInChildren<Text>(true);

                for (int j = 0; j < uiLabels.Length; j++)
                {
                    Text uiLabel = uiLabels[j];

                    if (Helper.IsIncludeChinese(uiLabel.text))
                    {
                        Debug.LogError(string.Format("路径:{0} 预设名:{1} 对象名:{2} 中文:{3}", prefabPath, prefab.name, uiLabel.name, uiLabel.text));
                    }
                }

                //进度条
                float progressBar = (float)i / prefabPaths.Count;
                EditorUtility.DisplayProgressBar("检查预设中文", "进度 ：" + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }

            EditorUtility.ClearProgressBar();

            Debug.Log("完成检查预设中文");
        }

        [MenuItem("ZQFramwork/Prefab(预设)/检查预设中文并且生成文本")]
        static void CheckChinesePrefabsAndSerialization()
        {
            List<string> paths = EditorHelper.GetAllFilePaths();

            List<string> prefabPaths = EditorHelper.GetAllPrefabFilePaths(paths);

            if (prefabPaths == null)
            {
                return;
            }

            List<string> text = new List<string>();

            for (int i = 0; i < prefabPaths.Count; i++)
            {
                string prefabPath = prefabPaths[i];

                //修改路径格式
                prefabPath = EditorHelper.ChangeFilePath(prefabPath);

                AssetImporter tmpAssetImport = AssetImporter.GetAtPath(prefabPath);

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(tmpAssetImport.assetPath);

                if (prefab == null)
                {
                    continue;
                }

                Text[] uiLabels = prefab.GetComponentsInChildren<Text>(true);

                for (int j = 0; j < uiLabels.Length; j++)
                {
                    Text uiLabel = uiLabels[j];

                    if (Helper.IsIncludeChinese(uiLabel.text))
                    {
                        Debug.LogError(string.Format("路径:{0} 预设名:{1} 对象名:{2} 中文:{3}", prefabPath, prefab.name, uiLabel.name, uiLabel.text));
                        text.Add(uiLabel.text);
                    }
                }

                //进度条
                float progressBar = (float)i / prefabPaths.Count;
                EditorUtility.DisplayProgressBar("检查预设中文", "进度 ：" + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }

            EditorHelper.SerializationText(Application.dataPath + "/中文.txt", text);

            EditorUtility.ClearProgressBar();

            AssetDatabase.Refresh();

            Debug.Log("完成检查预设中文并且生成文本");
        }


        [MenuItem("ZQFramwork/Prefab(预设)/检查预设中文并且删除中文")]
        static void CheckChinesePrefabsAndDeleteChinese()
        {
            List<string> paths = EditorHelper.GetAllFilePaths();

            List<string> prefabPaths = EditorHelper.GetAllPrefabFilePaths(paths);

            if (prefabPaths == null)
            {
                return;
            }

            for (int i = 0; i < prefabPaths.Count; i++)
            {
                string prefabPath = prefabPaths[i];

                //修改路径格式
                prefabPath = EditorHelper.ChangeFilePath(prefabPath);

                AssetImporter tmpAssetImport = AssetImporter.GetAtPath(prefabPath);

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(tmpAssetImport.assetPath);

                if (prefab == null)
                {
                    continue;
                }

                GameObject obj = Instantiate(prefab) as GameObject;

                Text[] uiLabels = obj.GetComponentsInChildren<Text>(true);

                bool isChange = false;

                for (int j = 0; j < uiLabels.Length; j++)
                {
                    Text uiLabel = uiLabels[j];

                    if (Helper.IsIncludeChinese(uiLabel.text))
                    {
                        Debug.LogError(string.Format("路径:{0} 预设名:{1} 对象名:{2} 中文:{3}", prefabPath, prefab.name, uiLabel.name, uiLabel.text));
                        uiLabel.text = "";
                        isChange = true;
                    }
                }

                if (isChange)
                {
                    PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ReplaceNameBased);
                }

                DestroyImmediate(obj);

                //进度条
                float progressBar = (float)i / prefabPaths.Count;
                EditorUtility.DisplayProgressBar("检查预设中文并且删除中文", "进度 ：" + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }

            EditorUtility.ClearProgressBar();

            AssetDatabase.Refresh();

            Debug.Log("完成检查预设中文并且删除中文");
        }







    }

}

