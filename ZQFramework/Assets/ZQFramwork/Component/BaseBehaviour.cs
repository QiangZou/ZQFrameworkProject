using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        Debug.Log(transform.name + " Awake");
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
        Debug.Log(transform.name + " Update");
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
