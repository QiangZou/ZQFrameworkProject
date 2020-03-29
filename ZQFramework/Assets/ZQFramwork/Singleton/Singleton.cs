namespace ZQFramwork
{
    //抽象类Singleton
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        //只有当前类的成员与继承该类的类才能访问.
        protected static T mInstance;
        private static object mLock = new object();

        protected Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                //线程锁
                lock (mLock)
                {
                    if (mInstance == null)
                    {
                        mInstance = SingletonCreator.CreateSingleton<T>();
                    }
                }

                return mInstance;
            }
        }

        //丢弃后 实例至空
        public virtual void Dispose()
        {
            mInstance = null;
        }

        public virtual void OnSingletonInit()
        {
        }
    }
}