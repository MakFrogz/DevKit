using UnityEngine;

namespace Singletons
{
    public class PersistentSingleton<T> : MonoSingleton<T> where T : Component
    {
        private void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}