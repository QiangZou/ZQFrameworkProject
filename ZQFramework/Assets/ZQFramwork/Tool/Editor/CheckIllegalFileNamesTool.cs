using System.Collections;
using System.Collections.Generic;
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

                if (IsIllegal(allFilePaths[i]) == false)
                {
                    Debug.LogError(allFilePaths[i]);
                }
                
            }
        }

        static bool IsIllegal(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                //字符不属于字母和数字
                //if (char.IsLetter(c) == false || char.IsDigit(c) == false)
                //{
                //    return false;
                //}


            }

            return true;
        }
    }
}


