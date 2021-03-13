using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZQFramwork
{
    public static class ConvertNumberTool
    {
        private static readonly string Zero = "零";
        private static readonly string One = "一";
        private static readonly string Two = "二";
        private static readonly string Three = "三";
        private static readonly string Four = "四";
        private static readonly string Five = "五";
        private static readonly string Six = "六";
        private static readonly string Seven = "七";
        private static readonly string Eight = "八";
        private static readonly string Nine = "九";
        private static readonly string Ten = "十";
        private static readonly string Hundred = "百";
        private static readonly string Thousand = "千";
        private static readonly string TenThousand = "万";
        private static readonly string OneHundredThousand = "十万";
        private static readonly string Million = "百万";
        private static readonly string TenMillion = "千万";
        private static readonly string AHundredMillion = "亿";

        //private static readonly string OneTraditional = "壹";
        //private static readonly string TwoTraditional = "贰";
        //private static readonly string ThreeTraditional = "叁";
        //private static readonly string FourTraditional = "肆";
        //private static readonly string FiveTraditional = "伍";
        //private static readonly string SixTraditional = "陆";
        //private static readonly string SevenTraditional = "柒";
        //private static readonly string EightTraditional = "捌";
        //private static readonly string NineTraditional = "玖";


        private static readonly Dictionary<ulong, string> numberDic = new Dictionary<ulong, string>()
        {
            { 0,Zero},
            { 1,One},
            { 2,Two},
            { 3,Three},
            { 4,Four},
            { 5,Five},
            { 6,Six},
            { 7,Seven},
            { 8,Eight},
            { 9,Nine},
            { 10,Ten},
            { 100,Hundred},
            { 1000,Thousand},
            { 10000,TenThousand},
            { 100000,OneHundredThousand},
            { 1000000,Million},
            { 10000000,TenMillion},
            { 100000000,AHundredMillion},
        };


        public static string GetChineseNumbers(int number)
        {
            return GetChineseNumbers((ulong)number);
        }

        public static string GetChineseNumbers(uint number)
        {
            return GetChineseNumbers((ulong)number);
        }

        public static string GetChineseNumbers(long number)
        {
            return GetChineseNumbers((ulong)number);
        }

        public static string GetChineseNumbers(ulong number)
        {
            float integer = 0;

            integer = Mathf.Floor(number / 100000000);
            if (integer > 0)
            {
                return string.Format("{0}{1}{2}", GetChineseNumbers((ulong)integer), numberDic[100000000], GetChineseNumbers(number - 100000000 * (ulong)integer));
            }

            integer = Mathf.Floor(number / 10000);
            if (integer > 0)
            {
                return string.Format("{0}{1}{2}", GetChineseNumbers((ulong)integer), numberDic[10000], GetChineseNumbers(number - 10000 * (ulong)integer));
            }

            integer = Mathf.Floor(number / 1000);
            if (integer > 0)
            {
                return string.Format("{0}{1}{2}", GetChineseNumbers((ulong)integer), numberDic[1000], GetChineseNumbers(number - 1000 * (ulong)integer));
            }

            integer = Mathf.Floor(number / 100);
            if (integer > 0)
            {
                return string.Format("{0}{1}{2}", GetChineseNumbers((ulong)integer), numberDic[100], GetChineseNumbers(number - 100 * (ulong)integer));
            }

            integer = Mathf.Floor(number / 10);
            if (integer > 0)
            {
                return string.Format("{0}{1}{2}", GetChineseNumbers((ulong)integer), numberDic[10], GetChineseNumbers(number - 10 * (ulong)integer));
            }

            return numberDic[number];
        }

    }

}
