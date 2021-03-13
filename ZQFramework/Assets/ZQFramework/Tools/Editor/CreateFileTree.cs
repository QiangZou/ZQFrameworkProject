using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
    /// <summary>
    /// 创建文件树
    /// </summary>
    public class CreateFileTree
    {
        [MenuItem("ZQFramwork/工具/创建文件树")]
        static void Create()
        {
            Debug.Log("开始创建文件树");

            StringBuilder text = new StringBuilder();

            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(Application.dataPath);

            GetFileTree(currentDirectoryInfo, text, -1);

            FileStream fileStream = new FileStream(Application.dataPath + "/../../FileTree.md", FileMode.Create, FileAccess.ReadWrite);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.Write(text);

            streamWriter.Close();

            Debug.Log("完成创建文件树");
        }

        static void GetFileTree(DirectoryInfo currentDirectoryInfo, StringBuilder text, int layer)
        {
            layer++;

            foreach (var item in currentDirectoryInfo.GetDirectories())
            {
                text.AppendLine(GetFormat(layer, item.Name));

                GetFileTree(item, text, layer);
            }
            foreach (var item in currentDirectoryInfo.GetFiles())
            {
                if (item.Extension != ".meta")
                {
                    text.AppendLine(GetFormat(layer, item.Name));
                }
            }
        }

        static string GetFormat(int layer, string text)
        {
            string format = string.Empty;
            for (int i = 0; i <= layer; i++)
            {
                if (i == layer)
                {
                    text = format + "- " + text;
                    break;
                }
                format = "  " + format;
            }

            return text;
        }
    }

}