using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ZQFramwork
{
    public class EditorDictionary
    {
        public DictionaryData dictionaryData;

        public bool isFold = false;
        public int level = 0;
        public Dictionary<EditorObject, EditorObject> editorElements = new Dictionary<EditorObject, EditorObject>();

        public EditorDictionary(DictionaryData data, int indentLevel)
        {
            dictionaryData = data;
            level = indentLevel;

            RefreshValue();
        }

        public void RefreshValue()
        {
            dictionaryData.RefreshValue();

            List<ObjectData> keys = new List<ObjectData>(dictionaryData.elements.Keys);

            List<EditorObject> editorKeys = new List<EditorObject>(editorElements.Keys);

            for (int i = 0; i < keys.Count; i++)
            {
                ObjectData key = keys[i];
                ObjectData value = dictionaryData.elements[key];

                if (editorKeys.Count > i)
                {
                    EditorObject editorKey = editorKeys[i];
                    EditorObject editorValue = editorElements[editorKey];

                    editorKey.RefreshValue(key, level, i.ToString() + " key:");
                    editorValue.RefreshValue(value, level, i.ToString() + " value:");
                }
                else
                {
                    EditorObject editorKey = new EditorObject(key, level, i.ToString() + " key:");
                    EditorObject editorValue = new EditorObject(value, level, i.ToString() + " value:");

                    editorElements.Add(editorKey, editorValue);
                }
            }

            //foreach (var item in dictionaryData.elements)
            //{
            //    ObjectData key = item.Key;
            //    ObjectData value = item.Value;

            //    EditorObject editorKey = new EditorObject(key, level, "key:");
            //    EditorObject editorValue = new EditorObject(value, level, "value:");

            //    editorElements.Add(editorKey, editorValue);
            //}
        }

        public void RefreshValue(DictionaryData data)
        {
            dictionaryData = data;
        }


        public static void GUI(EditorDictionary info)
        {
            EditorGUI.indentLevel = info.level;

            info.isFold = EditorGUILayout.Foldout(info.isFold, info.dictionaryData.name);
            if (info.isFold == false)
            {
                return;
            }

            info.RefreshValue();

            foreach (var item in info.editorElements)
            {
                EditorObject.GUI(item.Key);
                EditorObject.GUI(item.Value);
            }
        }
    }
}

