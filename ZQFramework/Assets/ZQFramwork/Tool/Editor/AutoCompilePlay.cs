using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
    public class AutoCompilePlay : EditorWindow
    {
        [MenuItem("ZQFramwork/自动编译后启动Unity %&r", false, 0)]
        public static void Open()
        {
            AutoCompilePlay me = GetWindow<AutoCompilePlay>();
            me.titleContent = new GUIContent("自动启动工具");
            me.minSize = new Vector2(200, 100);
            me.maxSize = me.minSize;

            EditorApplication.isPlaying = false;//停止运行
            AssetDatabase.Refresh();//刷新资源
        }

        //每秒10帧调用
        private void OnInspectorUpdate()
        {
            Repaint();//重绘
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (EditorUtility.scriptCompilationFailed)
            {
                Debug.LogError("编译报错");
                Close();
                return;
            }

            if (EditorApplication.isCompiling)
            {
                EditorGUILayout.LabelField("正在编译");
                return;
            }

            if (Application.isPlaying == false)
            {
                EditorGUILayout.LabelField("正在启动");
                EditorApplication.isPlaying = true;
            }
            else if (Application.isPlaying == true)
            {
                Close();
            }
        }
    }

}

