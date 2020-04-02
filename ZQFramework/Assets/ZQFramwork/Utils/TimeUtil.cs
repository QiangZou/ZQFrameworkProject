using System;
using System.Text;

namespace ZQFramwork
{
    /// <summary>
    /// 时间工具
    /// </summary>
    public static class TimeUtil
    {
        private static readonly string Year = "年";
        private static readonly string Month = "月";
        private static readonly string Day = "日";
        private static readonly string Hour = "时";
        private static readonly string Minute = "分";
        private static readonly string Second = "秒";

        private static StringBuilder TempStringBuilder = new StringBuilder(128);

        public static DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }

    }
}

