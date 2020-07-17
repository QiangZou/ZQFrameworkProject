using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
