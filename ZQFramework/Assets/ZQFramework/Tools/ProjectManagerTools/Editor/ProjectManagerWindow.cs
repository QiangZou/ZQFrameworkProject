using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
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

            for (int i = 0; i < illegalFiles.Count; i++)
            {
                //防止加载太多导致很卡
                if (i >= 99)
                {
                    break;
                }

                FileSystemInfo item = illegalFiles[i];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.SelectableLabel(Path.GetFileNameWithoutExtension(item.Name));
                if (GUILayout.Button("定位"))
                {
                    string path = FoldersTool.GetLoadPath(item.FullName);

                    Object obj = AssetDatabase.LoadMainAssetAtPath(path);

                    Selection.activeObject = obj;
                }
                if (GUILayout.Button("打开目录"))
                {
                    EditorUtility.RevealInFinder(item.FullName);
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
                illegalFiles = new List<FileSystemInfo>();

                FoldersTool.Folder();//提前初始化 防止线程不能调用unity api

                ThreadPool.QueueUserWorkItem((go) =>
                {
                    FoldersTool.CheckFolderName(FoldersTool.Folder(), illegalFiles, new List<char>(ProjectManagerConfigManager.Get().checkFileName.legal.ToCharArray()));
                });

                //ThreadTask threadTask = new ThreadTask(() =>
                //{
                //    FoldersTool.CheckFolderName(FoldersTool.Folder(), illegalFiles, new List<char>(ProjectManagerConfigManager.Get().checkFileName.legal.ToCharArray()));
                //});

                //FoldersTool.CheckFolderName(FoldersTool.Folder, illegalFiles, new List<char>(ProjectManagerConfigManager.Get().checkFileName.legal.ToCharArray()));


            }
        }

        private void ShowCheckFileName()
        {
            if (GUILayout.Button("文件命名规范配置", "DropDownButton"))
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



        void OnDestroy()
        {
            ProjectManagerConfigManager.Save();
        }
    }

}
