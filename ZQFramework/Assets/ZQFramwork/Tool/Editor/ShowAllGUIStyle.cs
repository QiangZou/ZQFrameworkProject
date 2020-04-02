using UnityEngine;
using UnityEditor;

namespace ZQFramwork
{
    public class ShowAllGUIStyle : EditorWindow
    {
        private Vector2 scrollVector2 = Vector2.zero;

        [MenuItem("ZQFramwork/工具/查看所有GUIStyle", false)]
        static void OpenWindow()
        {
            EditorWindow window = GetWindow(typeof(ShowAllGUIStyle));
            window.minSize = new Vector2(300, 900);
        }

        string search = string.Empty;

        void OnGUI()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            search = EditorGUILayout.TextField("", search, "ToolbarSeachTextField");

            if (GUILayout.Button("", "ToolbarSeachCancelButton"))
            {
                search = string.Empty;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            scrollVector2 = GUILayout.BeginScrollView(scrollVector2);

            foreach (GUIStyle style in GUI.skin.customStyles)
            {
                if (search == string.Empty || style.name.Contains(search))
                {
                    DrawStyleItem(style);
                }

            }

            GUILayout.EndScrollView();
        }

        void DrawStyleItem(GUIStyle style)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.SelectableLabel(style.name);

            GUILayout.Button("", style);

            EditorGUILayout.EndVertical();
        }
    }
}

