namespace ZQFramwork
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class QMonoSingletonPath : Attribute
    {
		private string mPathInHierarchy;

        public QMonoSingletonPath(string pathInHierarchy)
        {
            mPathInHierarchy = pathInHierarchy;
        }

        public string PathInHierarchy
        {
            get { return mPathInHierarchy; }
        }
    }
}
