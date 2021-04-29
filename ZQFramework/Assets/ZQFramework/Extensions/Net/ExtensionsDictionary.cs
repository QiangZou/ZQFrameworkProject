using System.Collections.Generic;
using System.Text;

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

        private static StringBuilder stringBuilder = new StringBuilder(1024);
        public static string GetString<TKey, TValue>(this Dictionary<TKey, TValue> self)
        {
            if (self == null)
            {
                return "Dictionary is null";
            }

            stringBuilder.Length = 0;
            foreach (var item in self)
            {
                stringBuilder.AppendFormat("Key:{0} Value:{1}\n", item.Key.ToString(), item.Value != null ? item.Value.ToString() : "null");
            }

            return stringBuilder.ToString();
        }
    }

}
