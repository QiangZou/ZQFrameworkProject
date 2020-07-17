using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDefineConfig
{
    public static ModuleDefine[] allModuleDefine = new ModuleDefine[]
    {
        new ModuleDefine()
        {
            moduleID = ModuleID.Login,
            BaseModel = new LoginModel(),
            baseController = new LoginController(),
            baseView = new LoginView(),
        },
        new ModuleDefine()
        {
            moduleID = ModuleID.Main,
            BaseModel = new MainModel(),
            baseController = new MainController(),
            baseView = new MainView(),
        }
    };

}
