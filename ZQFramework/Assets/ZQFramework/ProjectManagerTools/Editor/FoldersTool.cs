using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace ZQFramwork
{
    public static class FoldersTool
    {
        private static Folders folder;

        public static Folders Folder()
        {
            if (folder == null)
            {
                folder = new Folders(Application.dataPath);
            }
            return folder;
        }


        [MenuItem("ZQFramwork/项目管理工具/检查文件命名", false)]
        public static void CheckFolderName()
        {
            List<FileSystemInfo> files = new List<FileSystemInfo>();

            CheckFolderName(Folder(), files, new List<char>(ProjectManagerConfigManager.Get().checkFileName.legal.ToCharArray()));

            foreach (var item in files)
            {
                Debug.Log(item.FullName);
            }
        }

        public static void CheckFolderName(Folders folder, List<FileSystemInfo> paths, List<char> ignoreChar)
        {
            foreach (var item in folder.dicFileSystemInfo)
            {
                FileSystemInfo fileSystemInfo = item.Value;

                if (fileSystemInfo.Extension == ".meta" || fileSystemInfo.Extension == ".DS_Store")
                {
                    continue;
                }

                string name = Path.GetFileNameWithoutExtension(fileSystemInfo.Name);

                bool isIllegal = IsLegal(name, ignoreChar);
                if (isIllegal == false)
                {
                    paths.Add(fileSystemInfo);
                }
            }

            foreach (var item in folder.listFolder)
            {
                CheckFolderName(item, paths, ignoreChar);
            }
        }

        public static string GetLoadPath(string filePath)
        {
            string path = filePath.Replace('\\', '/');

            path = path.Replace(Application.dataPath, "Assets");

            return path;
        }

        static bool IsLegal(string str, List<char> ignoreChar)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (char.IsLower(c) || char.IsUpper(c) || char.IsDigit(c))
                {
                    continue;
                }

                bool isIllegal = false;

                foreach (var item in ignoreChar)
                {
                    if (c == item)
                    {
                        isIllegal = true;
                        continue;
                    }
                }
                if (isIllegal)
                {
                    continue;
                }

                return false;
            }
            return true;
        }

    }


}




