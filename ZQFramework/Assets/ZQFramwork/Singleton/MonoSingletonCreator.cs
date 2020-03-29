namespace ZQFramwork
{
	using System.Reflection;
	using UnityEngine;
	
    /// <summary>
    /// Mono��������
    /// </summary>
	public static class MonoSingletonCreator
	{
        /// <summary>
        /// �Ƿ�Ԫ����
        /// </summary>
		public static bool IsUnitTestMode { get; set; }

        /// <summary>
        /// �����̳�Mono������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
		public static T CreateMonoSingleton<T>() where T : MonoBehaviour, ISingleton
		{
			T instance = null;

            //�ж��Ƿ��ǵ�Ԫ���Ի���û����״̬
			if (!IsUnitTestMode && !Application.isPlaying) return instance;

            //Object.FindObjectOfType   ����T��һ���������ƥ��Ķ���
            instance = Object.FindObjectOfType<T>();

            //����Ѿ��ж��������Tʵ�� �򷵻�
			if (instance != null) return instance;

            //��ȡ�йس�Ա���Ե���Ϣ
            MemberInfo info = typeof(T);

            //���������Զ������� 
            var attributes = info.GetCustomAttributes(true);
			foreach (var atribute in attributes)
			{
				var defineAttri = atribute as QMonoSingletonPath;
				if (defineAttri == null)
				{
					continue;
				}

                //������Զ������� ��������Ҫ�󴴽�����
				instance = CreateComponentOnGameObject<T>(defineAttri.PathInHierarchy, true);
				break;
			}

            //���û���Զ�����
			if (instance == null)
			{
				var obj = new GameObject(typeof(T).Name);
				if (!IsUnitTestMode)
					Object.DontDestroyOnLoad(obj);
				instance = obj.AddComponent<T>();
			}

            //��ʼ������
			instance.OnSingletonInit();

			return instance;
		}

        /// <summary>
        /// ��������������ص���Ϸ������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="path">·��</param>
        /// <param name="dontDestroy">�г����Ƿ�ɾ��</param>
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
        /// ������Ϸ����
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="build"></param>
        /// <param name="dontDestroy">�г����Ƿ�ɾ��</param>
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

            //����������Ϸ����
            return FindGameObject(null, subPath, 0, build, dontDestroy);
		}

        /// <summary>
        /// ������Ϸ����
        /// </summary>
        /// <param name="root">������</param>
        /// <param name="subPath">·������</param>
        /// <param name="index">·������</param>
        /// <param name="build">����</param>
        /// <param name="dontDestroy">�г����Ƿ�ɾ��</param>
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