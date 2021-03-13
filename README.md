# ZQFramework

## 目录规范

- Tools 工具目录
- Plugins 插件目录
- 多脚本组成的功能 创建文件夹存放
- 单脚本放在Script目录

## 目录结构

├─AssetManager 资源管理
│      AssetBundleManager.cs
│      AssetManager.cs
│      DeviceDownloadAssetBundle.cs
│      DownloadManager.cs
│      IAssetLoad.cs
│      ResourcesManager.cs
│      UnityDefault.cs
│      UnityLocalAssetBundle.cs
│
├─Component 独立组件
│      AwakeToDisable.cs 激活自动隐藏
│      TouchControl.cs 摇杆控制器
│      ZQBaseBehaviour.cs 
│
├─Extensions 扩展类
├─Module 模块
│      BaseController.cs
│      BaseModel.cs
│      BaseModelData.cs
│      BaseView.cs
│      BaseViewData.cs
│      ModuleDefine.cs
│      ModuleDefineConfig.cs
│      ModuleID.cs
│      MVCManager.cs
│
├─ObjectPool 对象池
│      IPool.cs
│      PoolManager.cs
│      PrefabPool.cs
│
├─Plugins 插件
│  ├─JsonFx 
│  │      JsonFx.Json.dll
│  │      License.txt
│  │      Readme.txt
│  │
│  └─VPTimer
│          VPTimer.cs
│          VPTimeUtility.cs
│
├─Script 单功能脚本
│      EnumComparer.cs
│      EventBase.cs
│      Helper.cs
│      LoadManager.cs
│      Loom.cs
│      PathTool.cs
│      ShowModelUIManager.cs
│      SimpleReferenceType.cs
│      ThreadTask.cs
│      WindowManager.cs
│
├─Singleton 单例
│      ISingleton.cs
│      MonoSingleton.cs
│      MonoSingletonCreator.cs
│      MonoSingletonPath.cs
│      MonoSingletonProperty.cs
│      Singleton.cs
│      SingletonCreator.cs
│      SingletonProperty.cs
│
└─Tools 工具 
    │  ConvertNumberTool.cs 
    │  ConvertTimeTool.cs

​    ├─AdapterTool 适配工具
​    ├─ComponentDebugTool 组件调试工具
​    ├─Editor
​    │      AssetBundlesTool.cs
​    │      AutoCompilePlay.cs 自动编译启动
​    │      CheckIllegalFileNamesTool.cs
​    │      CreateBaseClassTool.cs
​    │      EditorCoroutineLooper.cs
​    │      EditorHelper.cs
​    │      FindreAssetFerencesTool.cs
​    │      PrefabsTool.cs
​    │      PreviewTool.cs
​    │      QuickPositioningUITool.cs 快速定位UI工具
​    │      ScriptsTool.cs 
​    │      ShowAllGUIStyle.cs 查看所有GUI样式
​    │
​    └─ProjectManagerTools 项目管理工具

## 代码命名规范
- 帕斯卡命名法（首字母大写）
- Util在其他项目依然可以使用
- Tool通用业务相关的使用
- Service处理单一业务
- Helper功能辅助