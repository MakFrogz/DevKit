using UnityEngine;

namespace Singletons
{
    public class MonoSingleton <T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        private static readonly object _lock = new object();
        
        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                lock (_lock)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance)
                    {
                        return _instance;
                    }
                    
                    var gameObject = new GameObject(typeof(T).Name + " - Singleton");
                    _instance = gameObject.AddComponent<T>();
                    return _instance;
                }
            }
        }
    }
}