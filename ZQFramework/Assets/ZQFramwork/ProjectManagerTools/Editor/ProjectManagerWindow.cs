using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ProjectManagerWindow : EditorWindow
{
    private static ProjectManagerWindow me;

    private static ProjectManagerConfig projectManagerConfig;

    [MenuItem("ZQFramwork/项目管理工具/主界面", false)]
    private static void Open()
    {
        if (me == null)
        {
            me = GetWindow<ProjectManagerWindow>();
            me.titleContent = new GUIContent("项目管理工具");
            projectManagerConfig = ProjectManagerConfigManager.Get();

            me.minSize = projectManagerConfig.windowSize;
            me.maxSize = projectManagerConfig.windowSize;
        }
        else
        {
            me.Close();
        }
    }

    List<FileSystemInfo> illegalFiles;

    private void ShowLllegalFiles()
    {
        if (illegalFiles == null)
        {
            return;
        }

        foreach (var item in illegalFiles)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.SelectableLabel(Path.GetFileNameWithoutExtension(item.Name));
            if (GUILayout.Button("定位"))
            {
                //Object obj = AssetDatabase.LoadMainAssetAtPath(item.FullName);

                string path = item.FullName.Substring(item.FullName.IndexOf("Assets/"), item.FullName.Length - item.FullName.IndexOf("Assets/"));

                Debug.LogError(path);
                Object obj = AssetDatabase.LoadMainAssetAtPath(path);
                
                Selection.activeObject = obj;
            }
            if (GUILayout.Button("打开目录"))
            {

            }

            EditorGUILayout.EndHorizontal();
        }

        //GUILayout.Box(new GUIContent("一个200x200的BOX"), new[] { GUILayout.Height(200), GUILayout.Width(200) });

        //GUILayout.BeginArea(new Rect(200, 200, 100, 100), "GroupBox");

        //GUILayout.Button("Click me", "GroupBox");

        //GUILayout.Button("Or me");

        //GUILayout.EndArea();
    }


    private void ShowOneKeyCheck()
    {
        if (GUILayout.Button("一键检查", GUILayout.MinHeight(100f)))
        {
            Debug.LogError("一键检查");

            illegalFiles = new List<FileSystemInfo>();

            FoldersTool.CheckFolderName(FoldersTool.Folder, illegalFiles, new List<char>(ProjectManagerConfigManager.Get().checkFileName.legal.ToCharArray()));

            foreach (var item in illegalFiles)
            {
                Debug.LogError(item.Name);
            }
        }
    }

    private void ShowCheckFileName()
    {
        if (GUILayout.Button("文件命名规范", "DropDownButton"))
        {
            projectManagerConfig.checkFileName.isFold = !projectManagerConfig.checkFileName.isFold;
        }

        if (projectManagerConfig.checkFileName.isFold)
        {
            EditorGUILayout.LabelField("合法规制：字母数字组合");

            string legal = EditorGUILayout.TextField("添加合法字符：", projectManagerConfig.checkFileName.legal);
            if (legal != projectManagerConfig.checkFileName.legal)
            {
                projectManagerConfig.checkFileName.legal = legal;
            }
        }
    }

    private Vector2 scrollVector2 = Vector2.zero;

    private void OnGUI()
    {
        scrollVector2 = GUILayout.BeginScrollView(scrollVector2);

        //代码刷新 引用丢失
        if (projectManagerConfig == null)
        {
            projectManagerConfig = ProjectManagerConfigManager.Get();
        }

        //EditorGUILayout.BeginHorizontal();
        ShowOneKeyCheck();

        ShowCheckFileName();

        ShowLllegalFiles();

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
        GUILayout.EndScrollView();
    }
}
