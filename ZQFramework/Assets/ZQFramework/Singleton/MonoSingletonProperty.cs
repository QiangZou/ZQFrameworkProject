

namespace ZQFramwork
{
	using UnityEngine;

	public static class MonoSingletonProperty<T> where T : MonoBehaviour, ISingleton
	{
		private static T mInstance = null;

		public static T Instance
		{
			get
			{
				if (null == mInstance)
				{
					mInstance = MonoSingletonCreator.CreateMonoSingleton<T>();
				}

				return mInstance;
			}
		}

		public static void Dispose()
		{
			if (MonoSingletonCreator.IsUnitTestMode)
			{
				Object.DestroyImmediate(mInstance.gameObject);
			}
			else
			{
				Object.Destroy(mInstance.gameObject);
			}

			mInstance = null;
		}
	}
	
	[System.Obsolete("弃用啦，请使用 MonoSingletonProperty")]
	public static class QMonoSingletonProperty<T> where T : MonoBehaviour, ISingleton
	{
		public static T Instance
		{
			get { return MonoSingletonProperty<T>.Instance; }
		}
	}
}