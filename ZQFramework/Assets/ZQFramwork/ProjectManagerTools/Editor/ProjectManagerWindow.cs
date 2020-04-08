using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectManagerWindow : EditorWindow
{
    private static ProjectManagerWindow me;

    [MenuItem("ZQFramwork/项目管理工具")]
    private static void Open()
    {
        if (me == null)
        {
            me = GetWindow<ProjectManagerWindow>();
            me.titleContent = new GUIContent("项目管理工具");
            me.minSize = new Vector2(500, 800);
            me.maxSize = me.minSize;
        }
        else
        {
            me.Close();
        }
    }


    private void OnGUI()
    {
        

        EditorGUILayout.BeginHorizontal();
        GUILayout.Button("一键检查", GUILayout.MinHeight(100f));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.ToggleLeft("文件命名规范", true);
        EditorGUILayout.ToggleLeft("预设文件包含空组建", true);
        EditorGUILayout.ToggleLeft("未引用组建", true);
        EditorGUILayout.ToggleLeft("重复资源", true);
        EditorGUILayout.ToggleLeft("SVN冲突文件", true);
    }
}
