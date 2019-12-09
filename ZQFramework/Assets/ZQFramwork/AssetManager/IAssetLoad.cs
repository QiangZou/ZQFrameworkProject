using System;
using System.Collections;

public interface IAssetLoad
{
    IEnumerator Load(string path, Action<UnityEngine.Object> completed);
}
