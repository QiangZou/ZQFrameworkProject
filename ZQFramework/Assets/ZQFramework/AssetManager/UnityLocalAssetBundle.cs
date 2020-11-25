using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class UnityLocalAssetBundle : IAssetLoad
    {
        bool isInit = false;

        AssetBundleManifest manifest;

        void Init()
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(PathTool.assetBundleSavePath + "/AssetBundles");
            manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            assetBundle.Unload(false);
            assetBundle = null;
        }

        public IEnumerator Load(string path, Action<UnityEngine.Object> completed)
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }

            path = path.ToLower();

            string name = System.IO.Path.GetFileName(path);

            //加载路径
            string newPath = PathTool.assetBundleSavePath + "/" + path;

            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(newPath);
            yield return request;

            AssetBundle assetBundle = request.assetBundle;

            string[] dependencies = manifest.GetAllDependencies(path);


            for (int i = 0; i < dependencies.Length; i++)
            {
                AssetBundleCreateRequest dependence = AssetBundle.LoadFromFileAsync(PathTool.assetBundleSavePath + "/" + dependencies[i]);

                yield return dependence;
            }

            UnityEngine.Object obj = assetBundle.LoadAsset<UnityEngine.Object>(name);

            if (completed != null)
            {
                completed(obj);
            }

            assetBundle.Unload(false);
        }


    }

}
