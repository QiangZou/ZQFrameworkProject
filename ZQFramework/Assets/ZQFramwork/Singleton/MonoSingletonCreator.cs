namespace ZQFramwork
{
	using System.Reflection;
	using UnityEngine;
	
    /// <summary>
    /// Mono单例创建
    /// </summary>
	public static class MonoSingletonCreator
	{
        /// <summary>
        /// 是否单元测试
        /// </summary>
		public static bool IsUnitTestMode { get; set; }

        /// <summary>
        /// 创建继承Mono单例类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
		public static T CreateMonoSingleton<T>() where T : MonoBehaviour, ISingleton
		{
			T instance = null;

            //判断是否是单元测试或者没运行状态
			if (!IsUnitTestMode && !Application.isPlaying) return instance;

            //Object.FindObjectOfType   返回T第一个与该类型匹配的对象
            instance = Object.FindObjectOfType<T>();

            //如果已经有对象挂在了T实例 则返回
			if (instance != null) return instance;

            //获取有关成员属性的信息
            MemberInfo info = typeof(T);

            //返回所有自定义特性 
            var attributes = info.GetCustomAttributes(true);
			foreach (var atribute in attributes)
			{
				var defineAttri = atribute as QMonoSingletonPath;
				if (defineAttri == null)
				{
					continue;
				}

                //如果有自定义特性 则按照特性要求创建对象
				instance = CreateComponentOnGameObject<T>(defineAttri.PathInHierarchy, true);
				break;
			}

            //如果没有自定特性
			if (instance == null)
			{
				var obj = new GameObject(typeof(T).Name);
				if (!IsUnitTestMode)
					Object.DontDestroyOnLoad(obj);
				instance = obj.AddComponent<T>();
			}

            //初始化单例
			instance.OnSingletonInit();

			return instance;
		}

        /// <summary>
        /// 创建代码组件挂载到游戏对象上
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">路径</param>
        /// <param name="dontDestroy">切场景是否删除</param>
        /// <returns></returns>
		private static T CreateComponentOnGameObject<T>(string path, bool dontDestroy) where T : MonoBehaviour
		{
			var obj = FindGameObject(path, true, dontDestroy);
			if (obj == null)
			{
				obj = new GameObject("Singleton of " + typeof(T).Name);
				if (dontDestroy && !IsUnitTestMode)
				{
					Object.DontDestroyOnLoad(obj);
				}
			}

			return obj.AddComponent<T>();
		}

        /// <summary>
        /// 查找游戏对象
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="build"></param>
        /// <param name="dontDestroy">切场景是否删除</param>
        /// <returns></returns>
		private static GameObject FindGameObject(string path, bool build, bool dontDestroy)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}

			var subPath = path.Split('/');
			if (subPath == null || subPath.Length == 0)
			{
				return null;
			}

            //迭代查找游戏对象
            return FindGameObject(null, subPath, 0, build, dontDestroy);
		}

        /// <summary>
        /// 查找游戏对象
        /// </summary>
        /// <param name="root">根对象</param>
        /// <param name="subPath">路径集合</param>
        /// <param name="index">路径索引</param>
        /// <param name="build">生成</param>
        /// <param name="dontDestroy">切场景是否删除</param>
        /// <returns></returns>
		private static GameObject FindGameObject(GameObject root, string[] subPath, int index, bool build, bool dontDestroy)
		{
			GameObject client = null;

			if (root == null)
			{
				client = GameObject.Find(subPath[index]);
			}
			else
			{
				var child = root.transform.Find(subPath[index]);
				if (child != null)
				{
					client = child.gameObject;
				}
			}

			if (client == null)
			{
				if (build)
				{
					client = new GameObject(subPath[index]);
					if (root != null)
					{
						client.transform.SetParent(root.transform);
					}

					if (dontDestroy && index == 0 && !IsUnitTestMode)
					{
						GameObject.DontDestroyOnLoad(client);
					}
				}
			}

			if (client == null)
			{
				return null;
			}

			return ++index == subPath.Length ? client : FindGameObject(client, subPath, index, build, dontDestroy);
		}
	}
}