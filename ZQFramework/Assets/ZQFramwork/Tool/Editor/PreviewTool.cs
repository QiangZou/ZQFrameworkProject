using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
    /// <summary>
    /// 预览工具
    /// </summary>
    public class PreviewTool
    {
        static readonly string windowManagerPath = "Assets/ZQFramwork/Resources/WinodwManager.prefab";
        static Transform root;

        [MenuItem("ZQFramwork/工具/预览/ &q")]
        static void PreviewGameObject()
        {
            if (Application.isPlaying == true)
            {
                Debug.LogError("请勿运行状态下预览");
                return;
            }

            Object obj = Selection.activeObject;//获取第一个选中对象

            if (obj == null)
            {
                Debug.LogError("请选中对象");
                return;
            }

            if ((obj as GameObject) == null)
            {
                Debug.LogError("请选中prefab类型");
                return;
            }

            GameObject target = PrefabUtility.InstantiatePrefab(obj) as GameObject;//实例化对象 保留引用
            if (target == null)
            {
                Debug.LogError("实例化对象失败");
                return;
            }

            Transform root = GetRootTransform();

            target.transform.SetParent(root, false);

            Selection.activeGameObject = target;//选中
            EditorGUIUtility.PingObject(target);//ping

        }

        static Transform GetRootTransform()
        {
            if (root != null)
            {
                return root;
            }

            Object oneObj = AssetDatabase.LoadAssetAtPath(windowManagerPath, typeof(GameObject));
            GameObject window = GameObject.Instantiate(oneObj) as GameObject;
            WindowManager windowManager = window.GetComponent<WindowManager>();
            root = windowManager.root.transform;
            return root;
        }
    }

}
