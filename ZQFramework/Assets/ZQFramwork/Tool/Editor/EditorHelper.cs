using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ZQFramwork
{
    public class EditorHelper : EditorWindow
    {
        /// <summary>
        /// 迭代获取文件路径
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="outPaths">输出所有路径</param>
        public static void IterationGetFilesPath(string directory, List<string> outPaths)
        {
            string[] files = Directory.GetFiles(directory);

            outPaths.AddRange(files);

            string[] childDirectories = Directory.GetDirectories(directory);

            if (childDirectories != null && childDirectories.Length > 0)
            {
                for (int i = 0; i < childDirectories.Length; i++)
                {
                    string dir = childDirectories[i];
                    if (string.IsNullOrEmpty(dir)) continue;
                    IterationGetFilesPath(dir, outPaths);
                }
            }
        }


        /// <summary>
        /// 获取项目Assets下所有文件路径
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllFilePaths()
        {
            List<string> paths = new List<string>();

            IterationGetFilesPath(Application.dataPath, paths);

            return paths;
        }

        /// <summary>
        /// 获取所有预设文件路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<string> GetAllPrefabFilePaths(List<string> paths)
        {
            if (paths == null)
            {
                return null;
            }

            List<string> prefabPaths = new List<string>();

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];

                if (path.EndsWith(".prefab") == true)
                {
                    prefabPaths.Add(path);
                }

                //进度条
                float progressBar = (float)i / paths.Count;
                EditorUtility.DisplayProgressBar("获取所有预设文件路径", "进度 ： " + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }

            EditorUtility.ClearProgressBar();

            return prefabPaths;
        }

        /// <summary>
        /// 获取所有脚本文件路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<string> GetAllScriptFilePaths(List<string> paths)
        {
            if (paths == null)
            {
                return null;
            }

            List<string> prefabPaths = new List<string>();

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];

                if (path.EndsWith(".cs") == true)
                {
                    prefabPaths.Add(path);
                }

                //进度条
                float progressBar = (float)i / paths.Count;
                EditorUtility.DisplayProgressBar("获取所有脚本文件路径", "进度 ： " + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }

            EditorUtility.ClearProgressBar();

            return prefabPaths;
        }

        /// <summary>
        /// 改变路径 例如  "C:/Users/XX/Desktop/aaa/New Unity Project/Assets\a.prefab" 改变成 "Assets/a.prefab"
        /// </summary>
        /// <param name="path"></param>
        public static string ChangeFilePath(string path)
        {
            path = path.Replace("\\", "/");
            path = path.Replace(Application.dataPath + "/", "");
            path = "Assets/" + path;

            return path;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static void SerializationText(string filePath, List<string> content)
        {
            if (content == null)
            {
                return;
            }

            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < content.Count; i++)
            {
                stringBuilder.AppendLine(content[i]);
            }

            streamWriter.Write(stringBuilder);

            streamWriter.Close();
        }

    }

}

