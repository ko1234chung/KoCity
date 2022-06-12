using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KoCity.System
{

    public class AssetCollector : MonoBehaviour
    {
        private string _assetTypeName;
        private AssetBundle _assetBundle;
        private AssetBundleCreateRequest _assetBundleCreateRequest;

        public void InitByAssetType(string assetTypeName)
        {
            _assetTypeName = assetTypeName;
            _assetBundleCreateRequest = AssetLoadSystem.Instance.LoadAssetBundleByType(assetTypeName);
            if (_assetBundleCreateRequest != null)
                _assetBundleCreateRequest.completed += (operation) => HandleAssetBundleLoadEnd(_assetBundleCreateRequest.assetBundle);
        }

        private void HandleAssetBundleLoadEnd(AssetBundle assetBundle)
        {
            _assetBundle = assetBundle;
            Debug.Log("Load Bundle end");
        }

        public void LoadAssetAsync<T>(string path, Action<T> callBack = null) where T : UnityEngine.Object
        {
            if (_assetBundle == null && _assetBundleCreateRequest == null)
                return;



            string fileName = path.Substring(path.LastIndexOf("/") + 1);
            if (_assetBundle == null && _assetBundleCreateRequest != null)
                _assetBundleCreateRequest.completed += (operation) => LoadAssetWithName(fileName, callBack);
            else
                LoadAssetWithName(fileName, callBack);


        }

        private void LoadAssetWithName<T>(string fileName, Action<T> callBack = null) where T : UnityEngine.Object
        {
            if (_assetBundle == null)
            {
                Debug.LogError($"Error : {_assetTypeName} for bundle null");
                return;
            }

            AssetBundleRequest request = _assetBundle.LoadAssetAsync<T>(fileName);
            request.completed += (operation) => callBack?.Invoke(request.asset as T);
        }

    }
}