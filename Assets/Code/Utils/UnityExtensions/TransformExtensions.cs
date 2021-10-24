using UnityEngine;

namespace Utils.UnityExtensions
{
    public static class TransformExtensions
    {
        public static void RemoveChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}