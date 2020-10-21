using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{

    public class ModuleDefine
    {
        public ModuleID moduleID;

        public Type baseViewDataType;
        public Type baseModelDataType;
        public Type baseModelType;
        public Type baseControllerType;
        public Type baseViewType;

        public bool isInit = false;

        public BaseViewData baseViewData;
        public BaseModelData baseModelData;
        public BaseModel baseModel;
        public BaseController baseController;
        public BaseView baseView;


    }
}
