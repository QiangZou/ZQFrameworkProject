using System;
using System.Threading;
using UnityEngine;
using System.Collections;

namespace ZQFramwork
{
    public class Loom : MonoBehaviour
    {
        static Loom Instance;


        private static int maxThreads = 8;
        private static int numThreads;
        private static int mainThreadID;
        private Queue actions = new Queue();


        public static Loom GetSingleton()
        {
            if (Instance == null)
            {
                var go = new GameObject("Loom");
                Instance = go.AddComponent<Loom>();
            }

            return Instance;
        }

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);

            mainThreadID = Thread.CurrentThread.ManagedThreadId;

            //保证线程安全
            actions = Queue.Synchronized(actions);
        }

        void Update()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                object action = actions.Dequeue();
                if (action != null && action is Action)
                {
                    (action as Action)();
                }
            }
        }

        void OnDestroy()
        {
            Instance = null;
        }



        public Coroutine StartMethod(IEnumerator cr)
        {
            return Instance.StartCoroutine(cr);
        }

        public void StopMethod(Coroutine cr_ret)
        {
            Instance.StopCoroutine(cr_ret);
        }

        public bool IsInMainThread()
        {
            return Thread.CurrentThread.ManagedThreadId == mainThreadID;
        }

        public void QueueInMainThread(Action action)
        {
            Instance.actions.Enqueue(action);
        }






        public static Thread RunAsync(Action action)
        {
            while (numThreads >= maxThreads)
            {
                //暂停当前线程1毫秒
                Thread.Sleep(1);
            }

            //以原子操作的形式递增指定变量的值并存储结果  numThreads++
            Interlocked.Increment(ref numThreads);

            //将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行
            ThreadPool.QueueUserWorkItem((state) =>
            {
                try
                {
                    ((Action)state)();
                }
                catch
                {
                }
                finally
                {
                    //以原子操作的形式递减指定变量的值并存储结果  numThreads--
                    Interlocked.Decrement(ref numThreads);
                }
            }, action);
            return null;
        }
    }
}


