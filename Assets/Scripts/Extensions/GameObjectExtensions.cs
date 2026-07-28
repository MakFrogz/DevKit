using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (!component)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
        
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;

        public static void DestroyChildren(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildren();
        }
        
        public static void DestroyChildrenImmediate(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildrenImmediate();
        }

        public static void EnableChildren(this GameObject gameObject)
        {
            gameObject.transform.EnableChildren();
        }
        
        public static void DisableChildren(this GameObject gameObject)
        {
            gameObject.transform.DisableChildren();
        }
        
        public static void ResetTransformation(this GameObject gameObject)
        {
            gameObject.transform.Reset();
        }
        
        public static T SetActive<T>(this T obj) where T : MonoBehaviour
        {
            obj.gameObject.SetActive(true);
            return obj;
        }
        
        public static T SetInactive<T>(this T obj) where T : MonoBehaviour
        {
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}