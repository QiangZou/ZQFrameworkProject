using System;
using System.Threading;
using UnityEngine;

namespace ZQFramwork
{
    public class ThreadTask
    {
        private Thread thread;
        private Action action;

        public ThreadTask(Action action)
        {
            this.action = action;
            thread = new Thread(ThreadRun);
            thread.Start();
        }

        private void ThreadRun()
        {
            action();
            //Debug.LogError("线程结束");
        }
    }
}


