using System.Collections.Generic;
using System.Text;

namespace ZQFramwork
{
    public static class ExtensionsList
    {
        /// <summary>
        /// 设置新大小
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="newCount"></param>
        /// <param name="filler"></param>
        /// <returns></returns>
        public static bool SetCount<T>(this List<T> self, int newCount, T filler)
        {
            if (newCount > self.Count)
            {
                for (int i = self.Count; i < newCount; i++)
                {
                    self.Add(filler);
                }
                return true;
            }
            else if (newCount < self.Count)
            {
                self.RemoveRange(newCount, self.Count - newCount);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 删除所有指定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int RemoveAll<T>(this List<T> self, T value)
        {
            return self.RemoveAll(i => i.Equals(value));
        }

        private static StringBuilder stringBuilder = new StringBuilder(1024);
        public static string GetString<T>(this List<T> self)
        {
            if (self == null)
            {
                return "List is null";
            }

            stringBuilder.Length = 0;
            for (int i = 0; i < self.Count; i++)
            {
                stringBuilder.AppendFormat("Key:{0} Value:{1}\n", i.ToString(), self[i] != null ? self[i].ToString() : "null");
            }

            return stringBuilder.ToString();
        }
    }
}
