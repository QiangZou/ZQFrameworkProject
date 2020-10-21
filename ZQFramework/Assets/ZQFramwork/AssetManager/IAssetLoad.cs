using System;
using System.Collections;

namespace ZQFramwork
{
    public interface IAssetLoad
    {
        IEnumerator Load(string path, Action<UnityEngine.Object> completed);
    }

}
