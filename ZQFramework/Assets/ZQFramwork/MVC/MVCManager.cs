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

        Dictionary<ModuleID, ModuleDefine> allModuleDic;

        public MVCManager()
        {
            int count = Enum.GetValues(typeof(ModuleID)).Length;

            allModuleDic = new Dictionary<ModuleID, ModuleDefine>(count, new EnumComparer<ModuleID>());

            for (int i = 0; i < ModuleDefineConfig.allModuleDefine.Length; i++)
            {
                ModuleDefine moduleDefine = ModuleDefineConfig.allModuleDefine[i];

                allModuleDic.Add(moduleDefine.moduleID, moduleDefine);
            }
        }

        public void Init()
        {

        }

       

        public void OpenModule(ModuleID moduleID)
        {
            InitModule(moduleID);
            Bind(moduleID);

            ModuleDefine moduleDefine = allModuleDic[moduleID];
            moduleDefine.baseController.Open();
        }

        public void InitModule(ModuleID moduleID)
        {
            ModuleDefine moduleDefine = allModuleDic[moduleID];

            if (moduleDefine.isInit)
            {
                return;
            }

            //ÊµÀý»¯Ä£¿é
            moduleDefine.baseViewData = Activator.CreateInstance(moduleDefine.baseViewDataType) as BaseViewData;
            moduleDefine.baseModelData = Activator.CreateInstance(moduleDefine.baseModelDataType) as BaseModelData;
            moduleDefine.baseModel = Activator.CreateInstance(moduleDefine.baseModelType, moduleDefine.baseViewData, moduleDefine.baseModelData) as BaseModel;
            moduleDefine.baseController = Activator.CreateInstance(moduleDefine.baseControllerType, moduleDefine.baseModel) as BaseController;

            moduleDefine.isInit = true;
        }

        public void Bind(ModuleID moduleID)
        {
            ModuleDefine moduleDefine = allModuleDic[moduleID];

            moduleDefine.baseView = WindowManager.Get().OpenWindow(string.Format("Modules/{0}/{0}", moduleID.ToString(), moduleID.ToString()), moduleDefine.baseViewType) as BaseView;

            moduleDefine.baseView.baseViewData = moduleDefine.baseViewData;

            moduleDefine.baseModel.Bind();
            moduleDefine.baseView.Bind();
        }

        public BaseController GetController(ModuleID moduleId)
        {
            ModuleDefine moduleDefine = allModuleDic[moduleId];

            return moduleDefine.baseController;
        }

        public T GetModel<T>(ModuleID moduleID)
        {
            ModuleDefine moduleDefine = null;
            if (allModuleDic.TryGetValue(moduleID, out moduleDefine))
            {
                //return allModuleDic[moduleID].baseModel as T;
            }

            return default(T);
        }


    }
}

