using UnityEngine;
using UnityEngine.Collections;
#if UNITY_EDITOR	
using UnityEditor;
#endif

namespace ZQFramwork
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AwakeToDisable))]
    public class AwakeToDisableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox("Awake执行后立刻Disable\n\n使用场景\n1.被克隆的对象\n2.默认需要隐藏的对象", MessageType.Info);
        }
    }
#endif
    public class AwakeToDisable : MonoBehaviour
    {
        private void Awake()
        {
            if (gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
        }

    }

}
