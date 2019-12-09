using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ZouQiang
{
    public class WindowManager : MonoSingleton<WindowManager>
    {
        Camera currentCamera;

        private Dictionary<string, GameObject> allWindow;



        public override void OnSingletonInit()
        {
            base.OnSingletonInit();

            CreateRoot();

            allWindow = new Dictionary<string, GameObject>();
        }





        /// <summary>
        /// 创建根目录.
        /// </summary>
        public void CreateRoot()
        {

            UIRoot uiRoot = gameObject.AddComponent<UIRoot>();

            uiRoot.scalingStyle = global::UIRoot.Scaling.Constrained;
            uiRoot.manualWidth = 750;
            uiRoot.manualHeight = 1334;

            //需要计算屏幕分辨率 和 设计分辨率 的宽高比 来决定使用宽度适配还是高度适配
            float tmpScreenAspectRatio = (Screen.width * 1.0f) / Screen.height;
            float tmpDesignAspectRatio = 750 / 1334;
            if (tmpScreenAspectRatio < tmpDesignAspectRatio)
            {
                uiRoot.fitWidth = true;
                uiRoot.fitHeight = false;
            }
            else
            {
                uiRoot.fitWidth = false;
                uiRoot.fitHeight = true;
            }


            //UIPanel uiPanel = gameObject.AddComponent<UIPanel>();




            GameObject camera = new GameObject("Camera");
            camera.transform.SetParent(transform);

            currentCamera = camera.AddComponent<Camera>();

            //清除标识;
            currentCamera.clearFlags = CameraClearFlags.SolidColor;

            //设置为正交相机
            currentCamera.orthographic = true;
            currentCamera.orthographicSize = 1.0f;

            //相机到开始和结束渲染的距离;
            currentCamera.nearClipPlane = -50;
            currentCamera.farClipPlane = 50;

            //设置显示层;
            //currentCamera.cullingMask = OnlyIncluding(LayerMask.NameToLayer(mLayer));

            //UICamera uiCamera = camera.AddComponent<UICamera>();
        }


        public static int OnlyIncluding(params int[] layers)
        {
            int mask = 0;
            for (int i = 0; i < layers.Length; i++)
            {
                mask |= 1 << layers[i];
            }
            return mask;
        }

        /// <summary>
        /// 是否打开窗口
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsOpenWindow(string path)
        {
            if (allWindow.ContainsKey(path))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取窗口名字
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetWindowName(string path)
        {
            string name = path.Replace(".prefab", "");

            int index = name.LastIndexOf("/");

            if (index > 0)
            {
                name = name.Substring(index + 1);
            }

            return name;
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="path"></param>
        /// <param name="completed"></param>
        public void OpenWindow(string path, Action<GameObject> completed = null)
        {
            //如果已经打开过了窗口;
            if (IsOpenWindow(path))
            {
                return;
            }

            LoadManager.Instance.Load(path, (obj) =>
             {

                 GameObject prefab = obj as GameObject;

                 if (prefab == null)
                 {
                     return;
                 }

                 string name = GetWindowName(path);

                 Transform window = prefab.transform.Find(name);

                 if (window == null)
                 {
                     return;
                 }

                 GameObject ui = GameObject.Instantiate(window.gameObject) as GameObject;

                 ui.transform.parent = transform;

                 ui.transform.name = name;

                 ui.transform.localScale = Vector3.one;

                 allWindow.Add(path, ui);

                 SetPanelDepth();

                 if (completed != null) completed(ui);

             });
        }


        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="path"></param>
        public void CloseWindow(string path)
        {
            GameObject window;

            if (allWindow.TryGetValue(path, out window))
            {
                Destroy(window);

                allWindow.Remove(path);
            }
        }

        /// <summary>
        /// 设置UI层级.
        /// </summary>
        void SetLayer()
        {
            //NGUITools.SetLayer(this.gameObject, LayerMask.NameToLayer(mLayer));
        }

        /// <summary>
        /// 设置Panel深度.
        /// </summary>
        void SetPanelDepth()
        {
            //获取所有的Panel
            UIPanel[] uiPanels = transform.GetComponentsInChildren<UIPanel>(true);

            //遍历Panel，Depth 递增
            for (int i = 0; i < uiPanels.Length; i++)
            {
                uiPanels[i].depth = 1 + i * 50;
            }

        }







        /// <summary>
        /// 获取NGUI坐标 根据屏幕坐标;
        /// </summary>
        /// <param name="varVec3"></param>
        /// <returns></returns>
        public Vector3 GetPosition(Vector3 varVec3)
        {
            varVec3.z = 0;

            varVec3 = currentCamera.WorldToScreenPoint(varVec3);

            varVec3.x -= (Screen.width / 2.0f);
            varVec3.y -= (Screen.height / 2.0f);

            return varVec3;
        }

        /// <summary>
        /// 世界坐标转屏幕坐标.
        /// </summary>
        /// <param name="varVec3">transform的世界坐标为 transform.position</param>
        /// <returns></returns>
        public Vector3 WorldToScreenPoint(Vector3 varVec3)
        {
            varVec3.z = 0;

            varVec3 = currentCamera.WorldToScreenPoint(varVec3);

            return varVec3;
        }

    }
}

