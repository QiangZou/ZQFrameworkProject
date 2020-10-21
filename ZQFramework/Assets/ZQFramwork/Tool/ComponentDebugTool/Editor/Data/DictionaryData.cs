using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class DictionaryData
    {
        public object instance;
        public Type instanceType;
        public string name;
        public event ObjectData.GetValueHandle getValueHandler;
        public event ObjectData.SetValueHandle setValueHandler;

        public IDictionary iDictionary;
        public Type keyType;
        public Type valueType;
        public Dictionary<ObjectData, ObjectData> elements = new Dictionary<ObjectData, ObjectData>();

        public DictionaryData(object value, Type t, string describe, ObjectData.GetValueHandle getHandler, ObjectData.SetValueHandle setHandler)
        {
            instance = value;
            instanceType = t;
            name = describe;
            getValueHandler = getHandler;
            setValueHandler = setHandler;

            iDictionary = instance as IDictionary;
            keyType = instanceType.GetGenericArguments()[0];
            valueType = instanceType.GetGenericArguments()[1];

            elements = GetElements();
        }

        Dictionary<ObjectData, ObjectData> GetElements()
        {
            Dictionary<ObjectData, ObjectData> dic = new Dictionary<ObjectData, ObjectData>();

            if (iDictionary == null)
            {
                return dic;
            }

            IDictionaryEnumerator item = iDictionary.GetEnumerator();

            while (item.MoveNext())
            {
                object index = item.Key;

                ObjectData key = new ObjectData(item.Key, keyType, keyType.Name, null, null, false, 0);
                ObjectData value = new ObjectData(item.Value, valueType, valueType.Name,
                    () =>
                    {
                        return iDictionary[index];
                    },
                    (obj) =>
                    {
                        iDictionary[index] = obj;
                    }, false, 0);

                dic.Add(key, value);
            }

            return dic;
        }

        public object GetValue()
        {
            if (getValueHandler == null)
            {
                return null;
            }

            return getValueHandler.Invoke();
        }

        public void SetValue(object value)
        {
            instance = value;
            iDictionary = instance as IDictionary;

            if (setValueHandler != null)
            {
                setValueHandler(instance);
            }
        }

        public void RefreshValue()
        {
            instance = GetValue();

            iDictionary = instance as IDictionary;

            elements = GetElements();
        }

        public void Remove(object key)
        {
            iDictionary.Remove(key);
            SetValue(iDictionary);
        }

        public void Add(object key, object value)
        {
            if (iDictionary.Contains(key))
            {
                return;
            }

            iDictionary.Add(key, value);
            SetValue(iDictionary);
        }
    }
}

