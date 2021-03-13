using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//主界面
//全屏窗口 登入窗口 主窗口  加载窗口 
//大屏窗口 背包界面 角色界面 商城界面 宠物界面-- 半透明背景
//居中大弹窗 宠物详情  -- 半透明背景 有关闭按钮
//固定位置弹窗 对话框 物品使用框 --透明背景 点击其他区域关闭
//不固定位置弹窗 装备详情--透明背景 点击其他区域关闭
//飘字-- 最顶层  没背景
//

namespace ZQFramwork
{
    public enum ViewType
    {
        /// <summary>
        /// 一级
        /// </summary>
        OneLevel,
        /// <summary>
        /// 二级
        /// </summary>
        SecondLevel,
        /// <summary>
        /// 三级
        /// </summary>
        ThreeLevel,
        /// <summary>
        /// 对话框
        /// </summary>
        Dialog,
        /// <summary>
        /// 悬浮框
        /// </summary>
        Popup,
        /// <summary>
        /// 提示
        /// </summary>
        Prompt
    }

    public interface IWindowManager
    {
        void OpenView(string path);
        void OpenView(string path, ViewType viewType);
    }


    public class WindowManager : ZQBaseBehaviour
    {
        private static WindowManager me;

        public Camera currentCamera;

        public GameObject root;

        public Transform oneLevel;
        public Transform secondLevel;
        public Transform threeLevel;
        public Transform dialog;
        public Transform popup;
        public Transform prompt;

        /// <summary>
        /// 缓存数量
        /// </summary>
        public int cacheNumber = 1;

        private Dictionary<ViewType, Dictionary<string, GameObject>> allView = new Dictionary<ViewType, Dictionary<string, GameObject>>();


        public static WindowManager Get()
        {
            if (me == null)
            {
                UnityEngine.Object prefab = ResourcesManager.Load("WinodwManager");

                GameObject go = Instantiate(prefab as GameObject);

                me = go.GetComponent<WindowManager>();

                DontDestroyOnLoad(go);
            }

            return me;
        }

        public GameObject OpenWindow(string path)
        {
            //待优化
            foreach (var items in allView)
            {
                foreach (var item in items.Value)
                {
                    if (item.Key == path)
                    {
                        ExecuteRule(items.Key, path);

                        return item.Value;
                    }
                }
            }

            UnityEngine.Object prefab = ResourcesManager.Load(path);

            GameObject window = Instantiate(prefab as GameObject, root.transform);

            BaseView baseView = window.GetComponent<BaseView>();
            ViewType viewType = baseView.viewType;

            //解耦
            switch (viewType)
            {
                case ViewType.OneLevel:
                    window.transform.parent = oneLevel;
                    break;
                case ViewType.SecondLevel:
                    window.transform.parent = secondLevel;
                    window.transform.SetSiblingIndex(secondLevel.childCount - 1);
                    break;
                case ViewType.ThreeLevel:
                    window.transform.parent = threeLevel;
                    window.transform.SetSiblingIndex(threeLevel.childCount - 1);
                    break;
                case ViewType.Dialog:
                    window.transform.parent = dialog;
                    break;
                case ViewType.Popup:
                    window.transform.parent = popup;
                    break;
                case ViewType.Prompt:
                    window.transform.parent = prompt;
                    break;
            }

            Dictionary<string, GameObject> keyValues = null;

            if (allView.TryGetValue(viewType, out keyValues) == false)
            {
                keyValues = new Dictionary<string, GameObject>();
            }

            keyValues[path] = window;

            allView[viewType] = keyValues;

            ExecuteRule(viewType, path);

            return window;
        }

        public T OpenWindow<T>(string path)
        {
            GameObject go = OpenWindow(path);

            T window = go.GetComponent<T>();

            return window;
        }

        public Component OpenWindow(string path, Type type)
        {
            GameObject go = OpenWindow(path);

            Component window = go.GetComponent(type);

            return window;
        }


        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private GameObject GetNewView(string path)
        {
            UnityEngine.Object prefab = ResourcesManager.Load(path);

            GameObject window = Instantiate(prefab as GameObject, root.transform);

            return window;
        }

        /// <summary>
        /// 执行显示规制
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="key"></param>
        private void ExecuteRule(ViewType viewType, string key)
        {
            SetViewSiblingIndex(viewType, key);

            switch (viewType)
            {
                case ViewType.OneLevel:
                    {
                        Hide(ViewType.OneLevel, key);
                        Hide(ViewType.SecondLevel);
                        Hide(ViewType.ThreeLevel);
                        Hide(ViewType.Dialog);
                        Hide(ViewType.Popup);
                        Hide(ViewType.Prompt);
                    }
                    break;
                case ViewType.SecondLevel:
                    {
                        Hide(ViewType.SecondLevel, key);
                        Hide(ViewType.ThreeLevel);
                        Hide(ViewType.Dialog);
                        Hide(ViewType.Popup);
                    }
                    break;
                case ViewType.ThreeLevel:
                    {
                        Hide(ViewType.ThreeLevel, key);
                        Hide(ViewType.Dialog);
                        Hide(ViewType.Popup);
                    }
                    break;
                case ViewType.Dialog:
                    {
                        Hide(ViewType.Dialog, key);
                        Hide(ViewType.Popup);
                    }
                    break;
                case ViewType.Popup:
                    {
                        Hide(ViewType.Dialog);
                        Hide(ViewType.Popup, key);
                    }
                    break;
                case ViewType.Prompt:
                    {
                        Hide(ViewType.Prompt, key);
                    }
                    break;
            }
        }

        /// <summary>
        /// 设置界面位置
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="key"></param>
        private void SetViewSiblingIndex(ViewType viewType, string key)
        {
            Dictionary<string, GameObject> keyValuePairs = null;
            if (allView.TryGetValue(viewType, out keyValuePairs) == false)
            {
                return;
            }

            GameObject view = keyValuePairs[key];

            switch (viewType)
            {
                case ViewType.OneLevel:
                    view.transform.SetSiblingIndex(oneLevel.childCount - 1);
                    break;
                case ViewType.SecondLevel:
                    view.transform.SetSiblingIndex(secondLevel.childCount - 1);
                    break;
                case ViewType.ThreeLevel:
                    view.transform.SetSiblingIndex(threeLevel.childCount - 1);
                    break;
            }
        }

        private void Destroy(ViewType viewType, string key = "")
        {
            Dictionary<string, GameObject> keyValuePairs = null;
            if (allView.TryGetValue(viewType, out keyValuePairs) == false)
            {
                return;
            }


        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="viewType"></param>
        /// <param name="key"></param>
        private void Hide(ViewType viewType, string key = "")
        {
            Dictionary<string, GameObject> keyValuePairs = null;
            if (allView.TryGetValue(viewType, out keyValuePairs) == false)
            {
                return;
            }

            foreach (var item in keyValuePairs)
            {
                if (item.Key != key)
                {
                    item.Value.SetActive(false);
                }
                else
                {
                    item.Value.SetActive(true);
                }
            }
        }



    }
}

