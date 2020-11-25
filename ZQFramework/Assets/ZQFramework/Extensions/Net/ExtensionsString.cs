using System.Collections.Generic;

namespace ZQFramwork
{
    public static class ExtensionsString
    {
        /// <summary>
        /// 获取数字
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int GetNumber(this string self)
        {
            return int.Parse(System.Text.RegularExpressions.Regex.Replace(self, @"[^0-9]+", ""));
        }

       
    }
}
