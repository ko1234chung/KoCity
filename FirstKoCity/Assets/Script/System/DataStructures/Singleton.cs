using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataStructures
{
    public class SingletonBase : MonoBehaviour
    {
        public virtual void Init() { }
    }

    public class Singleton<T> : SingletonBase where T : SingletonBase
    {
        private static T _instance;  

        public static T Instance
        {
            get 
            {
                if (_instance == null)
                {
                    var gameObj = new GameObject(typeof(T).Name);
                    _instance = gameObj.AddComponent<T>();
                    DontDestroyOnLoad(gameObj);
                    _instance.Init();
                }
                return _instance;
            }
        }

    }
}