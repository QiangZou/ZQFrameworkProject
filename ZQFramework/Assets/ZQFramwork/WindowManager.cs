using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ZQFramwork
{
    public class WindowManager : BaseBehaviour
    {
        private static WindowManager me;

        public Camera currentCamera;

        public GameObject root;

        private Dictionary<string, GameObject> allWindow = new Dictionary<string, GameObject>();

        public static WindowManager Get()
        {
            if (me == null)
            {
                UnityEngine.Object prefab = ResourcesManager.Load("WinodwManager");

                GameObject go = Instantiate(prefab as GameObject);

                me = go.GetComponent<WindowManager>();
            }

            return me;
        }

        public GameObject OpenWindow(string path)
        {
            GameObject window = null;

            if (allWindow.TryGetValue(path, out window))
            {
                return window;
            }

            UnityEngine.Object prefab = ResourcesManager.Load(path);

            window = Instantiate(prefab as GameObject, root.transform);

            //window.transform.parent = root.transform;
            //window.transform.localPosition = Vector3.zero;

            allWindow.Add(path, window);

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
    }
}

