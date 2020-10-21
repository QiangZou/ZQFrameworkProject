using System.Text.RegularExpressions;
using UnityEngine;


namespace ZQFramwork
{
    public static class Helper
    {

        ////////////////////////////NGUI 



        //public static void SetUILabelText(UILabel label, string text)
        //{
        //    if (label == null)
        //    {
        //        return;
        //    }

        //    label.text = text;
        //}

        //public static void SetUILabelText(Transform transform, string text)
        //{
        //    if (transform == null)
        //    {
        //        return;
        //    }

        //    UILabel label = transform.GetComponent<UILabel>();

        //    SetUILabelText(label, text);
        //}

        //public static void SetUILabelText(Transform transform, string path, string text)
        //{
        //    if (transform == null)
        //    {
        //        return;
        //    }

        //    SetUILabelText(transform.Find(path), text);
        //}

        //public static void SetUILabelText(GameObject gameObject, string path, string text)
        //{
        //    if (gameObject == null)
        //    {
        //        return;
        //    }

        //    SetUILabelText(gameObject.transform, path, text);
        //}

        //public static void SetUISprite(UISprite uiSprite, string name)
        //{
        //    if (uiSprite == null)
        //    {
        //        return;
        //    }

        //    uiSprite.spriteName = name;
        //}

        //public static void SetUISprite(Transform transform, string name)
        //{
        //    if (transform == null)
        //    {
        //        return;
        //    }

        //    SetUISprite(transform.GetComponent<UISprite>(), name);
        //}

        //public static void SetUISprite(Transform transform, string path, string name)
        //{
        //    if (transform == null)
        //    {
        //        return;
        //    }

        //    SetUISprite(transform.Find(path), name);
        //}

        //public static void SetUISprite(GameObject gameObject, string path, string name)
        //{
        //    if (gameObject == null)
        //    {
        //        return;
        //    }

        //    SetUISprite(gameObject.transform, path, name);
        //}


        //public static void SetUIEventListener(GameObject gameObject, string path, UIEventListener.VoidDelegate onClick)
        //{
        //    if (gameObject == null)
        //    {
        //        return;
        //    }

        //    Transform transform = gameObject.transform.Find(path);
        //    if (transform == null)
        //    {
        //        return;
        //    }

        //    UIEventListener.Get(transform.gameObject).onClick = onClick;
        //}


        ////////////////////////////Unity


        public static void SetActive(GameObject gameObject, bool state)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(state);
        }

        public static void SetActive(Transform transform, bool state)
        {
            if (transform == null)
            {
                return;
            }

            SetActive(transform.gameObject, state);
        }

        public static void SetActive(GameObject gameObject, string path, bool state)
        {
            if (gameObject == null)
            {
                return;
            }

            SetActive(gameObject.transform.Find(path), state);
        }

        public static void SetActive(Transform transform, string path, bool state)
        {
            if (transform == null)
            {
                return;
            }

            SetActive(transform.Find(path), state);
        }



        ////////////////////////////.NET

        /// <summary>
        /// 是否包含是否有中文
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsIncludeChinese(string content)
        {
            string regexstr = @"[\u4e00-\u9fa5]";

            if (Regex.IsMatch(content, regexstr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
