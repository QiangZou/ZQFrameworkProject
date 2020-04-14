using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;

namespace ZQFramwork
{
    public static class FolderManager
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            Debug.Log(char.IsLetter('看'));
        }

        private static Folders folder;

        public static Folders Folder
        {
            get
            {
                if (folder == null)
                {
                    folder = new Folders(Application.dataPath);
                }
                return folder;
            }
        }

        private static List<string> paths;

        [MenuItem("Tools/检查中文")]
        static void CheckFolderName()
        {
            paths = new List<string>();
            CheckFolderName(Folder, paths);
            foreach (var item in paths)
            {
                Debug.Log(item);
            }
        }

        static void CheckFolderName(Folders folder, List<string> paths)
        {
            foreach (var item in folder.dicFileSystemInfo)
            {
                string name = item.Key;
                FileSystemInfo fileSystemInfo = item.Value;

                if (fileSystemInfo.Extension == ".meta" || fileSystemInfo.Extension == ".DS_Store")
                {
                    continue;
                }

                bool isIllegal = IsIllegal(name, true, false);
                if (isIllegal == false)
                {
                    paths.Add(fileSystemInfo.FullName);
                }
            }

            foreach (var item in folder.listFolder)
            {
                CheckFolderName(item, paths);
            }
        }



        static bool IsIllegal(string str, bool isDigit = true, bool isSpace = true)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (char.IsLower(c) || char.IsUpper(c))
                {
                    return true;
                }
                if (char.IsDigit(c) && isDigit == true)
                {
                    return true;
                }
                if (char.IsWhiteSpace(c) && isSpace == true)
                {
                    return true;
                }
            }
            return false;
        }

    }




}
