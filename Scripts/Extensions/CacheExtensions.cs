using Il2CppSLZ.Marrow.Utilities;
using UnityEngine;

namespace NEP.Paranoia.Extensions
{
    public static class CacheExtensions
    {
        public static T[] ToArray<T>(this ComponentCache<T> self) where T : MonoBehaviour
        {
            List<T> list = new List<T>();

            foreach (var entry in self._cache)
            {
                list.Add(entry.Value as T);
            }
            
            return list.ToArray();
        }
    }
}