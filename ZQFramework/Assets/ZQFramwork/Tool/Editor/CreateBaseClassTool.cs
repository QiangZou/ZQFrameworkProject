using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace ZQFramwork
{
    public class CreateBaseClassTool : EditorWindow
    {
        public const string className = "{0}Base";
        public const string saveFile = "{0}/2048/Scripts/BaseClass";

        #region Text

        public const string menu = "创建预设索引类";
        public const string rootLabel = "父级游戏对象：";
        public const string clearAllComponent = "清空所有命名";
        public const string resetAllnamingFormat = "重置所有组件 命名格式";
        public const string advancedText = "高级设置";
        public const string nameFormatText = "命名格式";
        public const string componentText = "{0} 组件";
        public const string tips = "你必须先选择一个预设父级游戏对象。";
        public const string tips_gameObject = "游戏对象名";
        public const string tips_component = "组件名";
        public const string tips_gameObject_component = "游戏对象_组件名";
        public const string tips_path = "路径名";
        public const string tips_path_component = "路径_组件名";
        public const string ErrorName = "变量名相同或为数字开头";
        public const string buildClass = "生成代码";
        public const string baseClass =
    @"using UnityEngine;
using System.Collections;

/// <summary>
/// 该类由 CreateBaseClassTool 自动创建，请勿修改！
/// </summary>";


        #endregion

        //添加菜单项 &#1 Shift+Alt+1
        [MenuItem("ZQFramwork/UI/创建组件索引类 &#1")]
        static public void OpenWindow()
        {
            GameObject gameObject = Selection.activeGameObject;
            if (gameObject != null)
            {
                //获取窗口 参数 系统窗口(true)unity窗口(false) 标题 焦点(已经存在状态下)
                CreateBaseClassTool window = GetWindow<CreateBaseClassTool>(false, menu, true);

                window.parentGameObject = gameObject;
            }
            else
            {
                Debug.LogError(tips);
            }
        }





        public GameObjectIndex parentGameObjectIndex = null;

        public List<ComponentIndex> allComponentIndex = null;

        public GameObject parentGameObject = null;

        public NameFormat allNameType = NameFormat.component;

        public bool allIndex = true;

        public bool advanced = false;


        void OnGUI()
        {
            //间距
            EditorGUILayout.Space();

            //显示父级游戏对象
            ShowParentGameObject();

            //获取父级游戏对象索引
            GetParentGameObjectIndexData();

            EditorGUILayout.Space();

            //显示清理按钮
            ShowClearButton();

            EditorGUILayout.Space();

            //显示高级设置选项
            ShowAdvanced();

            EditorGUILayout.Space();

            //显示重置所有组建 命名格式
            ShowResetAllNamingFormat();

            EditorGUILayout.Space();
            EditorGUILayout.Space();


            //显示所有索引
            ShowAllIndex();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //显示提示
            ShowTip();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //显示生成代码按钮
            ShowBuildClassButton();



        }

        /// <summary>
        /// 显示父级游戏对象
        /// </summary>
        void ShowParentGameObject()
        {
            GameObject gameObject = EditorGUILayout.ObjectField(rootLabel, parentGameObject, typeof(GameObject), true) as GameObject;
            //更改 预设父级游戏对象
            if (parentGameObject != gameObject)
            {
                parentGameObject = gameObject;

                //重置 父级游戏对象索引
                parentGameObjectIndex = null;
            }

            EditorGUILayout.Space();

            if (parentGameObject == null)
            {
                //获取选中游戏对象
                parentGameObject = Selection.activeGameObject;
            }

            //没预设父级 不执行下面的
            if (parentGameObject == null)
            {
                //提示
                EditorGUILayout.LabelField(tips);
            }
        }

        /// <summary>
        /// 获取父级游戏对象索引
        /// </summary>
        void GetParentGameObjectIndexData()
        {
            if (parentGameObject == null)
            {
                return;
            }

            if (parentGameObjectIndex != null)
            {
                return;
            }

            parentGameObjectIndex = new GameObjectIndex();

            allComponentIndex = new List<ComponentIndex>();

            IterationGetIndex(parentGameObject.transform, parentGameObjectIndex, string.Empty);

            CheckName();
        }

        /// <summary>
        /// 显示清理按钮
        /// </summary>
        void ShowClearButton()
        {
            if (parentGameObjectIndex == null)
            {
                return;
            }

            if (GUILayout.Button(clearAllComponent))
            {
                SetIndexs(parentGameObjectIndex.children, false);
            }
        }

        /// <summary>
        /// 显示高级设置选项
        /// </summary>
        void ShowAdvanced()
        {
            if (parentGameObjectIndex == null)
            {
                return;
            }

            advanced = GUILayout.Toggle(advanced, advancedText);
        }

        /// <summary>
        /// 显示重置所有组建 命名格式
        /// </summary>
        void ShowResetAllNamingFormat()
        {
            NameFormat nameType = (NameFormat)EditorGUILayout.EnumPopup(resetAllnamingFormat, allNameType);
            if (allNameType != nameType)
            {
                allNameType = nameType;

                if (parentGameObjectIndex != null)
                {
                    SetIndexs(parentGameObjectIndex.children, allNameType);
                }

                CheckName();
            }

            switch (allNameType)
            {
                case NameFormat.None:
                    break;
                case NameFormat.gameObject:
                    EditorGUILayout.HelpBox(tips_gameObject, MessageType.Info);
                    break;
                case NameFormat.component:
                    EditorGUILayout.HelpBox(tips_component, MessageType.Info);
                    break;
                case NameFormat.gameObject_component:
                    EditorGUILayout.HelpBox(tips_gameObject_component, MessageType.Info);
                    break;
                case NameFormat.path:
                    EditorGUILayout.HelpBox(tips_path, MessageType.Info);
                    break;
                case NameFormat.path_component:
                    EditorGUILayout.HelpBox(tips_path_component, MessageType.Info);
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// 显示所有索引
        /// </summary>
        void ShowAllIndex()
        {
            if (parentGameObjectIndex == null)
            {
                return;
            }

            //迭代解析父级下所有的子集
            IterationParseGameObjectIndex(parentGameObjectIndex.children);
        }


        void CheckName()
        {
            if (allComponentIndex == null)
            {
                return;
            }

            for (int i = 0; i < allComponentIndex.Count; i++)
            {
                ComponentIndex componentIndex = allComponentIndex[i];

                //if (componentIndex.component.GetType() == typeof(Transform))
                //{
                //    continue;
                //}

                CheckName(componentIndex);
            }
        }

        void CheckName(ComponentIndex componentIndex)
        {
            componentIndex.nameIsError = false;

            //判断是否为数字开头
            Regex regNum = new Regex("^[0-9]");
            if (regNum.IsMatch(componentIndex.name))
            {
                componentIndex.nameIsError = true;
            }

            //判断是否有一样的命名
            CheckSameName(componentIndex);
        }


        /// <summary>
        /// 检查相同名字
        /// </summary>
        void CheckSameName(ComponentIndex check)
        {
            if (string.IsNullOrEmpty(check.name))
            {
                return;
            }

            if (allComponentIndex == null)
            {
                return;
            }

            for (int i = 0; i < allComponentIndex.Count; i++)
            {
                ComponentIndex componentIndex = allComponentIndex[i];

                if (string.IsNullOrEmpty(componentIndex.name))
                {
                    continue;
                }

                //if (componentIndex.component.GetType() == typeof(Transform))
                //{
                //    continue;
                //}

                if (componentIndex == check)
                {
                    continue;
                }

                if (componentIndex.name == check.name)
                {
                    check.nameIsError = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 显示提示
        /// </summary>
        void ShowTip()
        {
            if (allComponentIndex == null)
            {
                return;
            }


            if (IsError() == true)
            {
                EditorGUILayout.HelpBox(ErrorName, MessageType.Error);
            }
        }

        /// <summary>
        /// 显示生成代码按钮
        /// </summary>
        void ShowBuildClassButton()
        {
            if (parentGameObjectIndex == null)
            {
                return;
            }

            if (allComponentIndex == null)
            {
                return;
            }

            if (GUILayout.Button(buildClass))
            {
                if (IsError() == true)
                {
                    return;
                }

                Serialization();

                //刷新工程文件夹
                AssetDatabase.Refresh();

                Close();
            }
        }

        bool IsError()
        {
            bool isError = false;

            for (int i = 0; i < allComponentIndex.Count; i++)
            {
                ComponentIndex componentIndex = allComponentIndex[i];
                if (componentIndex.nameIsError == true)
                {
                    isError = true;
                    break;
                }
            }

            return isError;
        }

        void SetIndexs(List<GameObjectIndex> prefabIndexs, NameFormat nameType)
        {
            for (int i = 0; i < prefabIndexs.Count; i++)
            {
                GameObjectIndex children = prefabIndexs[i];

                for (int j = 0; j < children.components.Count; j++)
                {
                    ComponentIndex component = children.components[j];
                    component.nameFormat = nameType;
                    component.name = GetName(nameType, children, component);
                }

                SetIndexs(children.children, nameType);
            }
        }

        void SetIndexs(List<GameObjectIndex> prefabIndexs, bool index)
        {
            for (int i = 0; i < prefabIndexs.Count; i++)
            {
                GameObjectIndex children = prefabIndexs[i];

                for (int j = 0; j < children.components.Count; j++)
                {
                    ComponentIndex component = children.components[j];

                    if (index == false)
                    {
                        component.name = string.Empty;
                    }
                    else
                    {
                        component.name = GetName(component.nameFormat, children, component);
                    }
                }

                SetIndexs(children.children, index);
            }
        }

        /// <summary>
        /// 迭代解析游戏对象索引
        /// </summary>
        /// <param name="gameObjectIndex">游戏对象索引</param>
        void IterationParseGameObjectIndex(List<GameObjectIndex> gameObjectIndex)
        {
            for (int i = 0; i < gameObjectIndex.Count; i++)
            {
                GameObjectIndex children = gameObjectIndex[i];

                EditorGUI.indentLevel = children.hierarchy;
                {
                    GUIStyle style = new GUIStyle("Foldout");
                    style.fontStyle = FontStyle.Bold;
                    Color myStyleColor = new Color(0.2985075f, 0.4923412f, 1f);
                    style.normal.textColor = myStyleColor;
                    style.onNormal.textColor = myStyleColor;
                    style.hover.textColor = myStyleColor;
                    style.onHover.textColor = myStyleColor;
                    style.focused.textColor = myStyleColor;
                    style.onFocused.textColor = myStyleColor;
                    style.active.textColor = myStyleColor;
                    style.onActive.textColor = myStyleColor;

                    children.foldout = EditorGUILayout.Foldout(children.foldout, children.gameObject.name, style);
                    if (children.foldout)
                    {
                        //解析组件
                        ParseComponents(children);

                        //迭代
                        IterationParseGameObjectIndex(children.children);
                    }
                }
            }
        }

        /// <summary>
        /// 解析组建
        /// </summary>
        /// <param name="prefabIndex"></param>
        void ParseComponents(GameObjectIndex prefabIndex)
        {
            for (int i = 0; i < prefabIndex.components.Count; i++)
            {
                ComponentIndex activeComponent = prefabIndex.components[i];

                //if (activeComponent.component.GetType() == typeof(Transform))
                //{
                //    continue;
                //}


                EditorGUILayout.BeginHorizontal();
                {
                    GUIStyle style = new GUIStyle("TextField");

                    if (activeComponent.nameIsError)
                    {
                        Color myStyleColor = Color.red;
                        style.normal.textColor = myStyleColor;
                        style.onNormal.textColor = myStyleColor;
                        style.hover.textColor = myStyleColor;
                        style.onHover.textColor = myStyleColor;
                        style.focused.textColor = myStyleColor;
                        style.onFocused.textColor = myStyleColor;
                        style.active.textColor = myStyleColor;
                        style.onActive.textColor = myStyleColor;
                    }

                    string name = EditorGUILayout.TextField(string.Format(componentText, activeComponent.component.GetType().Name), activeComponent.name, style);
                    if (activeComponent.name != name)
                    {
                        name = name.Replace(" ", "");

                        activeComponent.name = name;

                        //修改后  遍历一次名字
                        CheckName();
                    }

                    if (advanced)
                    {
                        NameFormat nameFormat = (NameFormat)EditorGUILayout.EnumPopup(nameFormatText, activeComponent.nameFormat);

                        if (activeComponent.nameFormat != nameFormat)
                        {
                            activeComponent.nameFormat = nameFormat;
                            activeComponent.name = GetName(nameFormat, prefabIndex, activeComponent);
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                }

            }
        }



        /// <summary>
        /// 迭代获取对象上的索引
        /// </summary>
        /// <param name="prefabIndexs"></param>
        /// <param name="transform"></param>
        /// <param name="path"></param>
        /// <param name="variableName"></param>
        void IterationGetIndex(Transform transform, GameObjectIndex Parent, string path)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);


                if (child.parent != parentGameObject.transform)
                {
                    path = string.Format("{0}/{1}", path, child.gameObject.name);
                }
                else
                {
                    path = child.gameObject.name;
                }

                GameObjectIndex prefabIndex = GetGameObjectIndex(child.gameObject, Parent.hierarchy, path);

                if (child.parent != parentGameObject.transform)
                {
                    prefabIndex.path = Parent.path + "_" + child.name;
                }
                else
                {
                    prefabIndex.path = child.name;
                }

                Parent.children.Add(prefabIndex);

                //迭代
                IterationGetIndex(child, prefabIndex, path);
            }


        }

        /// <summary>
        /// 获取游戏对象索引
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="parentHierarchy">父级层级</param>
        /// <returns></returns>
        GameObjectIndex GetGameObjectIndex(GameObject gameObject, int parentHierarchy, string path)
        {
            GameObjectIndex prefabIndex = new GameObjectIndex();
            prefabIndex.gameObject = gameObject;
            prefabIndex.hierarchy = parentHierarchy + 1;

            Component[] components = gameObject.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];

                ComponentIndex activeComponent = new ComponentIndex();

                activeComponent.component = component;

                activeComponent.name = GetName(activeComponent.nameFormat, prefabIndex, activeComponent);

                activeComponent.path = path;

                prefabIndex.components.Add(activeComponent);

                allComponentIndex.Add(activeComponent);
            }

            return prefabIndex;
        }





        string GetName(NameFormat nameFormat, GameObjectIndex gameObjectIndex, ComponentIndex componentIndex)
        {
            string name = string.Empty;
            switch (nameFormat)
            {
                case NameFormat.gameObject:
                    name = gameObjectIndex.gameObject.name;
                    break;
                case NameFormat.component:
                    name = componentIndex.component.GetType().Name;
                    break;
                case NameFormat.gameObject_component:
                    name = gameObjectIndex.gameObject.name + "_" + componentIndex.component.GetType().Name;
                    break;
                case NameFormat.path:
                    name = gameObjectIndex.path;
                    break;
                case NameFormat.path_component:
                    name = gameObjectIndex.path + "_" + componentIndex.component.GetType().Name;
                    break;
                default:
                    break;
            }

            name = name.Replace(" ", "");

            return name;
        }








        void Serialization()
        {
            //类名
            string classBaseName = string.Format(className, parentGameObject.name);
            //保存文件夹路径
            string tempSavePath = string.Format(saveFile, Application.dataPath);
            //类文件路径
            string filePath = tempSavePath + "/" + classBaseName + ".cs";

            //若文件夹不存在则新建文件夹  
            if (!Directory.Exists(tempSavePath))
            {
                Directory.CreateDirectory(tempSavePath);
            }

            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            StringBuilder stringBuilder = new StringBuilder();


            stringBuilder.AppendLine(baseClass);

            stringBuilder.AppendLine(string.Format("public class {0} : MonoBehaviour", classBaseName));

            stringBuilder.AppendLine("{");

            for (int i = 0; i < allComponentIndex.Count; i++)
            {
                ComponentIndex componentIndex = allComponentIndex[i];

                //if (componentIndex.component.GetType() == typeof(Transform))
                //{
                //    continue;
                //}
                if (string.IsNullOrEmpty(componentIndex.name))
                {
                    continue;
                }

                string str = string.Format("    protected {0} {1};", componentIndex.component.GetType(), componentIndex.name);

                stringBuilder.AppendLine(str);
            }

            stringBuilder.AppendLine();

            stringBuilder.AppendLine("    public void InitBase()");

            stringBuilder.AppendLine("    {");


            for (int i = 0; i < allComponentIndex.Count; i++)
            {
                ComponentIndex componentIndex = allComponentIndex[i];

                //if (componentIndex.component.GetType() == typeof(Transform))
                //{
                //    continue;
                //}
                if (string.IsNullOrEmpty(componentIndex.name))
                {
                    continue;
                }

                string str = string.Format("        {0} = transform.Find(\"{1}\").GetComponent<{2}>();", componentIndex.name, componentIndex.path, componentIndex.component.GetType());

                stringBuilder.AppendLine(str);
            }

            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");

            streamWriter.Write(stringBuilder);

            streamWriter.Close();
        }









        /// <summary>
        /// 名字命名格式
        /// </summary>
        public enum NameFormat
        {
            None,
            /// <summary>
            /// 游戏对象
            /// </summary>
            gameObject,
            /// <summary>
            /// 组件
            /// </summary>
            component,
            /// <summary>
            /// 游戏对象_组件
            /// </summary>
            gameObject_component,
            /// <summary>
            /// 路径
            /// </summary>
            path,
            /// <summary>
            /// 路径_组件
            /// </summary>
            path_component,
        }


        /// <summary>
        /// 组件索引
        /// </summary>
        public class ComponentIndex
        {
            /// <summary>
            /// 组件
            /// </summary>
            public Component component;
            /// <summary>
            /// 名字格式
            /// </summary>
            public NameFormat nameFormat = NameFormat.gameObject;
            /// <summary>
            /// 名字
            /// </summary>
            public string name = string.Empty;
            /// <summary>
            /// 名字是否有错误（同名 不符合变量规则）
            /// </summary>
            public bool nameIsError = false;
            /// <summary>
            /// 路径
            /// </summary>
            public string path = string.Empty;
        }


        /// <summary>
        /// 游戏对象索引
        /// </summary>
        public class GameObjectIndex
        {
            /// <summary>
            /// 游戏对象
            /// </summary>
            public GameObject gameObject;
            /// <summary>
            /// 层级
            /// </summary>
            public int hierarchy = -1;
            /// <summary>
            /// 路径
            /// </summary>
            public string path = string.Empty;
            /// <summary>
            /// 折叠标签
            /// </summary>
            public bool foldout = true;
            /// <summary>
            /// 游戏对象上的组件索引
            /// </summary>
            public List<ComponentIndex> components = new List<ComponentIndex>();
            /// <summary>
            /// 所有的子集
            /// </summary>
            public List<GameObjectIndex> children = new List<GameObjectIndex>();
        }
    }
}
