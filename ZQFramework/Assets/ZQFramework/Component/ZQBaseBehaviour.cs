using UnityEngine;

namespace ZQFramwork
{
    public class ZQBaseBehaviour : MonoBehaviour
    {
        [Header("Awake执行后立刻Disable")]
        [Tooltip("使用场景\n1.被克隆的对象\n2.默认需要隐藏的对象")]
        [SerializeField]
        private bool isAwakeToDisable = false;
        protected virtual void Awake()
        {
            Debug.Log(transform.name + " Awake");
            if (isAwakeToDisable && gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
        }
        protected virtual void OnEnable()
        {
            Debug.Log(transform.name + " OnEnable");
        }
        protected virtual void Start()
        {
            Debug.Log(transform.name + " Start");
        }
        protected virtual void Update()
        {
            //Debug.Log(transform.name + " Update");
        }
        protected virtual void OnDisable()
        {
            Debug.Log(transform.name + " OnDisable");
        }
        protected virtual void OnDestroy()
        {
            Debug.Log(transform.name + " OnDestroy");
        }
    }
}

