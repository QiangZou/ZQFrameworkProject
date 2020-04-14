using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ProjectManagerConfigManager
{
    public static ProjectManagerConfig config;

    public static ProjectManagerConfig Get()
    {
        if (config == null)
        {
            config = AssetDatabase.LoadAssetAtPath<ProjectManagerConfig>("Assets/Resources/Game/Battle/NBNetworkSpawnObjects.asset");
        }

        return config;
    }

    public static void Save()
    {

    }
}

[CreateAssetMenu(menuName = "ZQFramwork/Create ProjectManagerConfig ScriptableObject")]
public class ProjectManagerConfig : ScriptableObject
{

    public Vector2 size;




}
