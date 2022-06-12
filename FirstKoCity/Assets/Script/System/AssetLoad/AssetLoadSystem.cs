using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using DataStructures;

namespace KoCity.System
{

    public class AssetLoadSystem : Singleton<AssetLoadSystem>
    {
        private Dictionary<string, AssetCollector> assetTypeToCollector = new Dictionary<string, AssetCollector>();


        public AssetCollector GetCollector(string assetTypeName)
        {
            AssetCollector collector = null;
            if (assetTypeToCollector.TryGetValue(assetTypeName, out collector) && collector != null)
                return collector;

            GameObject collectorObj = new GameObject(assetTypeName + "_Collector");
            collector = collectorObj.AddComponent<AssetCollector>();
            collector.InitByAssetType(assetTypeName);

            assetTypeToCollector.Add(assetTypeName, collector);



            return collector;
        }

        public AssetBundleCreateRequest LoadAssetBundleByType(string assetTypeName, Action<AssetBundle> callBack = null)
        {
            string path = Path.Combine(Application.streamingAssetsPath, assetTypeName);
            //var loadOp = AssetBundle.LoadFromFile(path);
            var loadAsyncOp = AssetBundle.LoadFromFileAsync(path);
            loadAsyncOp.completed += (operation) => callBack?.Invoke(loadAsyncOp.assetBundle);

            return loadAsyncOp;
        }


    }
}
