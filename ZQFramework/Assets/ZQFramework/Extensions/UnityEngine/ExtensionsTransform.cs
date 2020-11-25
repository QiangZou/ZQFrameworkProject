using UnityEngine;
using System.Collections;

namespace ZQFramwork
{
    public static class ExtensionsTransform
    {
        /// <summary>
        /// 设置位置X
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        /// <summary>
        /// 设置位置Y
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        /// <summary>
        /// 设置位置Z
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        /// <summary>
        /// 获取位置X
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float GetPositionX(this Transform transform)
        {
            return transform.position.x;
        }

        /// <summary>
        /// 获取位置Y
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float GetPositionY(this Transform transform)
        {
            return transform.position.y;
        }

        /// <summary>
        /// 获取位置Z
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float GetPositionZ(this Transform transform)
        {
            return transform.position.z;
        }

        /// <summary>
        /// 设置本地坐标X
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void SetlocalPositionX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        /// <summary>
        /// 设置本地坐标Y
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void SetlocalPositionY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        /// <summary>
        /// 设置本地坐标X
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void SetlocalPositionZ(this Transform transform, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }

        /// <summary>
        /// 获取本地坐标X
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float GetlocalPositionX(this Transform transform)
        {
            return transform.localPosition.x;
        }

        /// <summary>
        /// 获取本地坐标Y
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float GetlocalPositionY(this Transform transform)
        {
            return transform.localPosition.y;
        }

        /// <summary>
        /// 获取本地坐标Z
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static float GetlocalPositionZ(this Transform transform)
        {
            return transform.localPosition.z;
        }

        /// <summary>
        /// 获取或添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Transform self) where T : Component
        {
            T component = self.GetComponent<T>();
            if (component == null)
            {
                component = self.gameObject.AddComponent<T>();
            }

            return component;
        }



        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="self"></param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static T GetComponent<T>(this Transform self, string path) where T : Component
        {
            Transform target = self.Find(path);

            if (target == null)
            {
                return null;
            }

            T component = target.GetComponent<T>();

            return component;
        }



        ////------------------------------------------NGUI



        //public static void SetUILabelText(this Transform self, string text)
        //{
        //    UILabel label = self.GetComponent<UILabel>();

        //    if (label == null)
        //    {
        //        return;
        //    }

        //    label.text = text;
        //}

        //public static void SetUILabelText(this Transform self, string path, string text)
        //{
        //    Transform transform = self.Find(path);

        //    transform.SetUILabelText(text);
        //}
    }

}
