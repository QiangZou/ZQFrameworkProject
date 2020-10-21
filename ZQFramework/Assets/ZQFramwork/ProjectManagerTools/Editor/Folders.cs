using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZQFramwork
{
    /// <summary>
    /// 文件夹
    /// </summary>
    public class Folders
    {
        /// <summary>
        /// 当前目录信息
        /// </summary>
        public DirectoryInfo currentDirectoryInfo;
        /// <summary>
        /// 当前文件夹中的文件夹
        /// </summary>
        public List<Folders> listFolder;
        /// <summary>
        /// 当前文件夹的文件
        /// </summary>
        public List<FileInfo> listFileInfo;

        /// <summary>
        /// 当前文件夹中所有文件
        /// </summary>
        public Dictionary<string, FileSystemInfo> dicFileSystemInfo;

        public Folders(string path)
        {
            currentDirectoryInfo = new DirectoryInfo(path);

            Init();
        }

        Folders(DirectoryInfo directoryInfo)
        {
            currentDirectoryInfo = directoryInfo;

            Init();
        }

        private void Init()
        {
            listFileInfo = new List<FileInfo>();
            foreach (var item in currentDirectoryInfo.GetFiles())
            {
                listFileInfo.Add(item);
            }
            listFolder = new List<Folders>();
            foreach (var item in currentDirectoryInfo.GetDirectories())
            {
                listFolder.Add(new Folders(item));
            }

            dicFileSystemInfo = new Dictionary<string, FileSystemInfo>();
            foreach (var item in listFileInfo)
            {
                dicFileSystemInfo.Add(item.Name, item);
            }
            foreach (var item in listFolder)
            {
                dicFileSystemInfo.Add(item.currentDirectoryInfo.Name, item.currentDirectoryInfo);
            }
        }





        public void DebugSelf()
        {
            Debug.Log(currentDirectoryInfo.Name);
            foreach (var item in listFolder)
            {
                item.DebugSelf();
            }

            foreach (var item in listFileInfo)
            {
                Debug.Log(item.Name);
            }
        }
    }

}
