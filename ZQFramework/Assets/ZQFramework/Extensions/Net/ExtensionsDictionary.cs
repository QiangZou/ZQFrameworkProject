using System.Collections.Generic;

namespace ZQFramwork
{
    public static class ExtensionsDictionary
    {
        /// <summary>
        /// 添加(安全) 如已经有key则添加失败
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddSafety<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
        {
            if (self.ContainsKey(key))
            {
                return false;
            }

            self.Add(key, value);
            return true;
        }

        /// <summary>
        /// 更新(安全) 如没有key则更新失败
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdateSafety<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
        {
            if (!self.ContainsKey(key))
            {
                return false;
            }

            self[key] = value;
            return true;
        }
    }

}
