using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZQFramwork
{
    public class QuickPositioningUITool : Editor
    {
        [MenuItem("ZQFramwork/快速定位UI %f", false, 0)]
        public static void QuickPositioning()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            //使焦点移动到Game视图
            Type gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            EditorWindow window = EditorWindow.GetWindow(gameViewType);
            window.Focus();


            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            //获取鼠标位置所有碰撞对象
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                //选择第一个对象
                Selection.activeGameObject = raycastResults[0].gameObject;

                EditorGUIUtility.PingObject(raycastResults[0].gameObject);
            }
        }
    }

}
