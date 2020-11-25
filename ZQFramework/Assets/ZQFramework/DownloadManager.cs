using UnityEngine;
using System.IO;
using System.Net;
using System;

namespace ZQFramwork
{
    public class AsyncTask
    {
        public string url;
        public string savePath;
        public string fileName;

        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream fileStream;
        /// <summary>
        /// 请求
        /// </summary>
        public HttpWebRequest httpWebRequest;
        /// <summary>
        /// 响应
        /// </summary>
        public HttpWebResponse httpWebResponse;

        public Stream stream;

        /// <summary>
        /// 文件总大小
        /// </summary>
        public long totalBytes;
        /// <summary>
        /// 已下载长度
        /// </summary>
        public long fileLength;
        /// <summary>
        /// 进度
        /// </summary>
        public float progress;
    }

    public class DownloadManager : MonoBehaviour
    {
        public AsyncTask asyncTask;

        private void OnDestroy()
        {
            if (asyncTask.fileStream != null)
            {
                asyncTask.fileStream.Flush();
                asyncTask.fileStream.Close();
            }
            asyncTask.fileStream = null;
        }

        private void Update()
        {
            if (asyncTask != null)
            {
                Debug.Log(asyncTask.progress);
            }
        }

        public void Download(string url, string savePath, string fileName)
        {
            Loom.RunAsync(() =>
            {
                asyncTask = new AsyncTask();
                asyncTask.url = url;
                asyncTask.savePath = savePath;
                asyncTask.fileName = fileName;


                //保存路径不存在则创建
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }



                //创建请求
                asyncTask.httpWebRequest = WebRequest.Create(url) as HttpWebRequest;

                //创建文件流
                asyncTask.fileStream = new FileStream(savePath + "/" + fileName, FileMode.OpenOrCreate, FileAccess.Write);

                //获取已下载文件长度
                asyncTask.fileLength = asyncTask.fileStream.Length;




                //断点续传核心
                //Seek 将该流的当前位置设置为给定值
                asyncTask.fileStream.Seek(asyncTask.fileLength, SeekOrigin.Begin);

                //向请求添加范围标头
                asyncTask.httpWebRequest.AddRange((int)asyncTask.fileLength);

                asyncTask.httpWebRequest.BeginGetResponse(new AsyncCallback(ResponseCallback), asyncTask);
            });

        }

        /// <summary>
        /// 响应回调
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void ResponseCallback(IAsyncResult asyncResult)
        {
            AsyncTask requestState = (AsyncTask)asyncResult.AsyncState;




            //结束请求 获取响应
            requestState.httpWebResponse = requestState.httpWebRequest.EndGetResponse(asyncResult) as HttpWebResponse;


            //获取下载的流
            requestState.stream = requestState.httpWebResponse.GetResponseStream();

            //获取请求的下载长度
            requestState.totalBytes = requestState.httpWebResponse.ContentLength;




            byte[] buffer = new byte[10240];

            int length = requestState.stream.Read(buffer, 0, buffer.Length);

            while (length > 0)
            {
                //将byte数组写入文件流中
                asyncTask.fileStream.Write(buffer, 0, length);

                requestState.fileLength += length;

                //计算进度
                requestState.progress = requestState.fileLength / (float)requestState.totalBytes;

                //类似尾递归
                length = requestState.stream.Read(buffer, 0, buffer.Length);
            }

            //清除缓冲区 写入硬盘
            asyncTask.fileStream.Flush();
            requestState.stream.Close();
            requestState.stream.Dispose();
        }
    }
}


