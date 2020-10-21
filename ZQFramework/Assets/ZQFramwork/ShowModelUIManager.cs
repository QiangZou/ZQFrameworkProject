using UnityEngine;
using System.Collections;
#if UNITY_EDITOR	
using UnityEditor;
#endif

namespace ZQFramwork
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ShowModelUIManager))]
    public class ShowModelUIManagerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Play"))
            {
                ShowModelUIManager.GetSingleton().WatchModle(ShowModelUIManager.GetSingleton().mGameObject);
            }
        }
    }

#endif

    public class ShowModelUIManager : MonoBehaviour
    {
        private static ShowModelUIManager instance;

        public static ShowModelUIManager GetSingleton()
        {
            if (instance == null)
            {
                GameObject go = new GameObject("ShowModelUIManager");
                instance = go.AddComponent<ShowModelUIManager>();
            }
            return instance;
        }

        private const string mLayer = "GameUI";

        public RenderTexture mRenderTexture;

        //public UITexture mUITexture;

        public GameObject mGameObject;

        //实例化的时候调用;
        private void Awake()
        {
            instance = this;

            //跳场景不删除;
            DontDestroyOnLoad(this);

            //获取临时渲染纹理;
            mRenderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);

            CreateCamera(mRenderTexture);
        }

        void CreateCamera(RenderTexture RenderTexture)
        {
            GameObject go = new GameObject("Camera");

            go.transform.parent = this.transform;

            Camera tempCamera = go.AddComponent<Camera>();

            //清除标识;
            tempCamera.clearFlags = CameraClearFlags.SolidColor;

            //设置为正交相机
            tempCamera.orthographic = true;

            //相机到开始和结束渲染的距离;
            tempCamera.nearClipPlane = -50;

            tempCamera.farClipPlane = 50;

            //设置显示层;
            //tempCamera.cullingMask = WindowManager.OnlyIncluding(LayerMask.NameToLayer(mLayer));

            //目标纹理;
            tempCamera.targetTexture = mRenderTexture;
        }

        //public void WatchModle(UITexture varUITexture, GameObject varObject)
        public void WatchModle(GameObject varObject)
        {
            //if (varUITexture == null)
            //{
            //    Debug.Log("ShowModelUIManager : WatchModle() varUITexture == null");
            //    return;
            //}

            if (varObject == null)
            {
                Debug.Log("ShowModelUIManager : WatchModle() varObject == null");
                return;
            }

            //mUITexture = varUITexture;

            mGameObject = varObject;

            //mUITexture.mainTexture = mRenderTexture;

            mGameObject.transform.SetParent(this.transform);

            //NGUITools.SetLayer(this.gameObject, LayerMask.NameToLayer(mLayer));
        }
    }

}



