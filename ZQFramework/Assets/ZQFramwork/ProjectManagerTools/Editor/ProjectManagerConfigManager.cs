using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ProjectManagerConfigManager
{
    private static ProjectManagerConfig config;
    private static string SaveKey { get { return "ProjectManagerTools" + Application.dataPath; } }

    public static ProjectManagerConfig Get()
    {
        if (config == null)
        {
            //Debug.LogError(GetAssetPath());
            config = AssetDatabase.LoadAssetAtPath<ProjectManagerConfig>(GetAssetPath());
        }

        return config;
    }

    public static void Save()
    {
        EditorUtility.SetDirty(config);//标记目标物体已改变
        AssetDatabase.SaveAssets();
    }

    static string GetAssetPath()
    {
        string path = EditorPrefs.GetString(SaveKey, "");

        if (string.IsNullOrEmpty(path))
        {
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            foreach (var item in allAssetPaths)
            {
                if (item.Contains("ProjectManagerTools/ProjectManagerConfig.asset"))
                {
                    path = item;

                    EditorPrefs.SetString(SaveKey, path);

                    break;
                }
            }
        }
        return path;
    }



    private static string GetAssetPath(string name)
    {
        string data = EditorPrefs.GetString(SaveKey, "");

        if (string.IsNullOrEmpty(data))
        {
            string[] allAssets = AssetDatabase.GetAllAssetPaths();

            foreach (string s in allAssets)
            {
                if (s.Contains("AdapterTool/Asset"))
                {
                    data = s.Substring(0, s.LastIndexOf('/') + 1);

                    EditorPrefs.SetString(SaveKey, data);

                    break;
                }
            }
        }

        return data + name;
    }
}
