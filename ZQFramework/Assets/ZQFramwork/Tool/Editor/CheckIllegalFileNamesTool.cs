using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
    public class CheckIllegalFileNamesTool
    {
        [MenuItem("ZQFramwork/工具/检查文件名非法")]
        static void Check()
        {
            List<string> allFilePaths = EditorHelper.GetAllFilePaths();
            for (int i = 0; i < allFilePaths.Count; i++)
            {
                Debug.Log(allFilePaths[i]);

                if (IsChineseLetter(allFilePaths[i]))
                {
                    Debug.LogError(allFilePaths[i]);
                }

            }
        }

        [MenuItem("ZQFramwork/工具/检查文件名非法1")]
        static void Check1()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);

            foreach (var item in directoryInfo.GetDirectories())
            {
                Debug.LogError(item.Name);
            }

            foreach (var item in directoryInfo.GetFiles())
            {
                Debug.LogError(item.Name);
            }

        }

        static bool IsIllegal(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                //字符不属于字母和数字
                if (char.IsLetter(c) == false || char.IsDigit(c) == false)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsChineseLetter(string input)
        {
            int chfrom = Convert.ToInt32("4e00", 16); //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);

            for (int i = 0; i < input.Length; i++)
            {
                int code = Char.ConvertToUtf32(input, i);

                if (code >= chfrom && code <= chend)
                {
                    return true; //当code在中文范围内返回true
                }
            }

            return false;
        }
    }
}


