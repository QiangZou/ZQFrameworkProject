using System;
using System.Text;

namespace ZQFramwork
{
    /// <summary>
    /// 时间工具
    /// </summary>
    public static class ConvertTimeTool
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

        public static string GetChineseData(long timestamp)
        {
            DateTime dateTime = new DateTime(timestamp);
            TempStringBuilder.Length = 0;
            TempStringBuilder.Append(dateTime.Year);
            TempStringBuilder.Append(Year);
            TempStringBuilder.Append(dateTime.Month);
            TempStringBuilder.Append(Month);
            TempStringBuilder.Append(dateTime.Day);
            TempStringBuilder.Append(Day);
            TempStringBuilder.Append(dateTime.Hour);
            TempStringBuilder.Append(Hour);
            TempStringBuilder.Append(dateTime.Minute);
            TempStringBuilder.Append(Minute);
            TempStringBuilder.Append(dateTime.Second);
            TempStringBuilder.Append(Second);

            return TempStringBuilder.ToString();
        }
    }
}

