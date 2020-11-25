using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class DeviceDownloadAssetBundle : IAssetLoad
    {
        public IEnumerator Load(string path, Action<UnityEngine.Object> completed)
        {
            path = path.ToLower();

            //加载路径
            string newPath = Application.streamingAssetsPath + "/" + path;

            AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(newPath);

            yield return assetBundleCreateRequest;

            var assetBundle = assetBundleCreateRequest.assetBundle;
            if (assetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }

            AssetBundleRequest assetLoadRequest = assetBundle.LoadAssetAsync<GameObject>("start.prefab");
            yield return assetLoadRequest;

            if (completed != null)
            {
                completed(assetLoadRequest.asset);
            }

            assetBundle.Unload(false);
        }


    }
}

