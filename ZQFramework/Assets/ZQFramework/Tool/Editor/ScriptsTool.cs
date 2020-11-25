using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

namespace ZQFramwork
{
    public class ScriptsTool : EditorWindow
    {
        //添加菜单项 &#1 Shift+Alt+2
        [MenuItem("ZQFramwork/Scripts(脚本)/检查脚本中的中文")]
        static void CheckScriptsChinese()
        {
            //获取项目Assets下所有文件路径
            List<string> paths = EditorHelper.GetAllFilePaths();

            List<string> scriptFile = EditorHelper.GetAllScriptFilePaths(paths);

            if (scriptFile == null)
            {
                return;
            }

            for (int i = 0; i < scriptFile.Count; i++)
            {
                string prefabPath = scriptFile[i];

                List<string> lines = GetFileLine(prefabPath);

                CheckScriptChinese(prefabPath, lines);


                //进度条
                float progressBar = (float)i / scriptFile.Count;
                EditorUtility.DisplayProgressBar("检查脚本中的中文", "进度 ：" + ((int)(progressBar * 100)).ToString() + "%", progressBar);
            }



            //EditorUtility.ClearProgressBar();

            Debug.Log("完成 检查脚本中的中文");
        }

        static List<string> GetFileLine(string path)
        {
            List<string> lines = new List<string>();

            StreamReader sr = File.OpenText(path);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }

            sr.Close();
            sr.Dispose();

            return lines;
        }

        static void CheckScriptChinese(string path, List<string> lines)
        {
            if (lines == null)
            {
                return;
            }

            string line;

            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i];

                //这种类型文件不处理
                if (line.Contains("EditorWindow"))
                {
                    return;
                }



                if (Helper.IsIncludeChinese(line) == false)
                {
                    continue;
                }

                if (line.Contains("//"))
                {
                    continue;
                }

                if (line.Contains("Debug"))
                {
                    continue;
                }

                if (line.Contains("*"))
                {
                    continue;
                }

                if (line.Contains("--"))
                {
                    continue;
                }

                if (line.Contains("log"))
                {
                    continue;
                }

                if (line.Contains("MenuItem"))
                {
                    continue;
                }

                if (line.Contains("#region"))
                {
                    continue;
                }

                if (line.Contains("#endregion"))
                {
                    continue;
                }


                if (line.Contains("@@"))
                {
                    continue;
                }

                if (line.Contains("#@@"))
                {
                    continue;
                }

                if (line.Contains("EditorGUILayout"))
                {
                    continue;
                }

                if (line.Contains("EditorUtility"))
                {
                    continue;
                }

                if (line.Contains("GUILayout"))
                {
                    continue;
                }

                Debug.Log(string.Format("路径:{0}行数:{1}内容:{2}", path, i.ToString(), line));
            }
        }

    }

}

