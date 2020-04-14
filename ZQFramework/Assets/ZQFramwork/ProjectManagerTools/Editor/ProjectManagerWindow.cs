using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectManagerWindow : EditorWindow
{
    private static ProjectManagerWindow me;

    [MenuItem("ZQFramwork/项目管理工具", false)]
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

    string text;

    private void OnGUI()
    {


        //EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("一键检查", GUILayout.MinHeight(100f)))
        {
            Debug.LogError("一键检查");
        }

        if (GUILayout.Button("文件命名规范", "DropDownButton"))
        {
            Debug.LogError("文件命名规范");
        }

        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //GUILayout.Button("文件命名规范", GUILayout.MinHeight(30f));
        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.ToggleLeft("文件命名规范", true);
        //EditorGUILayout.ToggleLeft("预设文件包含空组建", true);
        //EditorGUILayout.ToggleLeft("未引用组建", true);
        //EditorGUILayout.ToggleLeft("重复资源", true);
        //EditorGUILayout.ToggleLeft("SVN冲突文件", true);

        //if (Event.current.type == EventType.MouseDown)
        //{
        //    GUI.FocusControl(null);
        //}

        //GUI.SetNextControlName("text:");
        //text = EditorGUILayout.TextField(text);
        //var rect = GUILayoutUtility.GetLastRect();
        //if (GUILayout.Button("111"))
        //{
        //    text = string.Empty;
        //}

        //if (Event.current.type == EventType.MouseDown && !rect.Contains(Event.current.mousePosition) && GUI.GetNameOfFocusedControl() == "text:")
        //{ //判断控件处于聚焦状态时

        //    GUI.FocusControl(null);
        //}

    }
}
