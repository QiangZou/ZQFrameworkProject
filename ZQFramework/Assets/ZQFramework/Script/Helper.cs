using System.Text.RegularExpressions;
using UnityEngine;


namespace ZQFramwork
{
    public static class Helper
    {
        /// <summary>
        /// 是否包含是否有中文
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsIncludeChinese(string content)
        {
            string regexstr = @"[\u4e00-\u9fa5]";

            if (Regex.IsMatch(content, regexstr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
