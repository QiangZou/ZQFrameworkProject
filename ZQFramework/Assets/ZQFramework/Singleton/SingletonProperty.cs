namespace ZQFramwork
{
    /// <summary>
    /// �������ԣ�������Ҫ�̳���������ʹ�ã�
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public static class SingletonProperty<T> where T : class, ISingleton
	{
		private static T mInstance;
		private static readonly object mLock = new object();

		public static T Instance
		{
			get
			{
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

		public static void Dispose()
		{
			mInstance = null;
		}
	}
}