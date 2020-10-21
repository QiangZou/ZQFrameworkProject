using UnityEngine;
using System.Collections;

namespace ZQFramwork
{
    public static class ExtensionsGameObject
    {
        public static GameObject Clone(this GameObject self, string name = "", bool active = true)
        {
            GameObject gameObject = GameObject.Instantiate(self) as GameObject;
            gameObject.transform.parent = self.transform.parent;
            gameObject.transform.localScale = self.transform.localScale;

            if (name != "")
            {
                gameObject.name = name;
            }

            gameObject.SetActive(active);

            return gameObject;
        }

        public static void SetActive(this GameObject self, bool active, string path)
        {
            Transform transform = self.transform.Find(path);
            if (transform == null)
            {
                return;
            }

            transform.gameObject.SetActive(active);
        }

        public static T GetComponent<T>(this GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();

            if (component == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning(string.Format("{0} 对象上 {1} 组建为空", self.name, typeof(T).ToString()));
#endif
                component = self.AddComponent<T>();
            }

            return component;
        }

    }

}
