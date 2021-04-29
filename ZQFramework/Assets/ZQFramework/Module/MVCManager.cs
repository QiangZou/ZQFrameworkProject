using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class MVCManager
    {
        private static MVCManager me;
        public static MVCManager Me
        {
            get
            {
                if (me == null)
                {
                    me = new MVCManager();
                }
                return me;
            }
        }

        Dictionary<ModuleID, BaseModel> allModule;
        Dictionary<ModuleID, BaseModelData> allBaseModelData = new Dictionary<ModuleID, BaseModelData>();
        Dictionary<ModuleID, BaseViewData> allBaseViewData = new Dictionary<ModuleID, BaseViewData>();
        Dictionary<ModuleID, ModuleDefine> allModuleDic = new Dictionary<ModuleID, ModuleDefine>();

        public MVCManager()
        {
            int count = Enum.GetValues(typeof(ModuleID)).Length;

            allModule = new Dictionary<ModuleID, BaseModel>(count, new EnumComparer<ModuleID>());
        }


        public BaseModel GetModel(ModuleID moduleId)
        {
            BaseModel model = null;
            if (allModule.TryGetValue(moduleId, out model) == false)
            {
                model = Activator.CreateInstance(Type.GetType(moduleId.ToString() + "Model"), moduleId) as BaseModel;
                allModule.Add(moduleId, model);
            }

            return model;
        }

        public BaseModelData GetBaseModelData(ModuleID moduleId)
        {
            BaseModelData value = null;
            if (allBaseModelData.TryGetValue(moduleId, out value) == false)
            {
                value = Activator.CreateInstance(Type.GetType(moduleId.ToString() + "ModelData")) as BaseModelData;
                allBaseModelData.Add(moduleId, value);
            }

            return value;
        }

        public BaseViewData GetBaseViewData(ModuleID moduleId)
        {
            BaseViewData value = null;
            if (allBaseViewData.TryGetValue(moduleId, out value) == false)
            {
                value = Activator.CreateInstance(Type.GetType(moduleId.ToString() + "ViewData")) as BaseViewData;
                allBaseViewData.Add(moduleId, value);
            }

            return value;
        }



        //public MVCManager()
        //{
        //    int count = Enum.GetValues(typeof(ModuleID)).Length;

        //    allModuleDic = new Dictionary<ModuleID, ModuleDefine>(count, new EnumComparer<ModuleID>());

        //    for (int i = 0; i < ModuleDefineConfig.allModuleDefine.Length; i++)
        //    {
        //        ModuleDefine moduleDefine = ModuleDefineConfig.allModuleDefine[i];

        //        allModuleDic.Add(moduleDefine.moduleID, moduleDefine);
        //    }
        //}

        public void Init()
        {

        }

        public void InitModule(ModuleID moduleId)
        {
            ModuleDefine moduleDefine = allModuleDic[moduleId];

            if (moduleDefine.isInit)
            {
                return;
            }

            moduleDefine.baseViewData = Activator.CreateInstance(moduleDefine.baseViewDataType) as BaseViewData;
            moduleDefine.baseModelData = Activator.CreateInstance(moduleDefine.baseModelDataType) as BaseModelData;
            moduleDefine.baseModel = Activator.CreateInstance(moduleDefine.baseModelType, moduleDefine.baseViewData, moduleDefine.baseModelData) as BaseModel;
            moduleDefine.baseController = Activator.CreateInstance(moduleDefine.baseControllerType, moduleDefine.baseModel) as BaseController;

            moduleDefine.isInit = true;
        }

        public void OpenModule(ModuleID moduleId)
        {
            InitModule(moduleId);

            Bind(moduleId);

            ModuleDefine moduleDefine = allModuleDic[moduleId];
            moduleDefine.baseController.Open();
        }


        public void Bind(ModuleID moduleId)
        {
            ModuleDefine moduleDefine = allModuleDic[moduleId];

            moduleDefine.baseView = WindowManager.Get().OpenWindow<BaseView>(string.Format("Modules/{0}/{0}", moduleId.ToString(), moduleId.ToString()));

            moduleDefine.baseView.baseViewData = moduleDefine.baseViewData;

            moduleDefine.baseModel.Bind();
            moduleDefine.baseView.Bind();
        }

        public BaseController GetController(ModuleID moduleId)
        {
            InitModule(moduleId);

            ModuleDefine moduleDefine = allModuleDic[moduleId];

            return moduleDefine.baseController;
        }

        public BaseModelData GetModelData(ModuleID moduleId)
        {
            InitModule(moduleId);

            ModuleDefine moduleDefine = allModuleDic[moduleId];

            return moduleDefine.baseModelData;
        }


    }
}

