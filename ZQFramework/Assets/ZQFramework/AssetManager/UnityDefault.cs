using System;
using System.Collections;

namespace ZQFramwork
{
    public class UnityDefault : IAssetLoad
    {
        public IEnumerator Load(string path, Action<UnityEngine.Object> completed)
        {
            UnityEngine.Object obj = null;

            path = string.Format("Assets/{0}", path);

#if UNITY_EDITOR
            obj = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
#endif

            if (completed != null)
            {
                completed(obj);
            }

            yield return null;
        }
    }

}
