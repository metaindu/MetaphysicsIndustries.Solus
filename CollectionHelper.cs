using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public static class CollectionHelper
    {
        public static void AddRange<T>(this HashSet<T> set,
            IEnumerable<T> items)
        {
            foreach (var item in items)
                set.Add(item);
        }
    }
}