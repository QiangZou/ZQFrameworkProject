using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class ModuleDefineConfig
    {
        public static ModuleDefine[] allModuleDefine = new ModuleDefine[]
        {
            new ModuleDefine()
            {
                moduleID = ModuleID.Login,
                baseViewDataType = typeof(LoginViewData),
                baseModelDataType = typeof(LoginModelData),
                baseModelType = typeof(LoginModel),
                baseControllerType = typeof(LoginController),
                baseViewType = typeof(LoginView),
            },
            new ModuleDefine()
            {
                moduleID = ModuleID.Main,
                baseViewDataType = typeof(MainViewData),
                baseModelDataType = typeof(MainModelData),
                baseModelType = typeof(MainModel),
                baseControllerType = typeof(MainController),
                baseViewType = typeof(MainView),
            },
        };

    }
}


