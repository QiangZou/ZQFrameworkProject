using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
    [Serializable]
    public class CheckRegulation
    {
        public bool isFold = false;

        public float lastTime = 0;

        public List<string> ignoreFilePath = new List<string>();
        public List<string> ignoreDirectoryPath = new List<string>();

        public List<FileSystemInfo> ignoreFile = new List<FileSystemInfo>();
        public List<DirectoryInfo> ignoreDirectory = new List<DirectoryInfo>();

    }

    [Serializable]
    public class legalChar
    {
        public char c;
        public bool isLegal;
    }

    [Serializable]
    public class CheckFileName : CheckRegulation
    {
        /// <summary>
        /// 合法字符
        /// </summary>
        public string legal = string.Empty;
    }

    [CreateAssetMenu(menuName = "ZQFramwork/Create ProjectManagerConfig ScriptableObject")]

    public class ProjectManagerConfig : ScriptableObject
    {
        /// <summary>
        /// 窗口大小
        /// </summary>
        public Vector2 windowSize = new Vector2(500, 800);


        public CheckFileName checkFileName = new CheckFileName();



    }

}
