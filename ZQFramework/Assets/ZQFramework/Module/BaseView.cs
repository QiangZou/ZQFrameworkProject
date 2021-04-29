using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class BaseView : ZQBaseBehaviour
    {
        protected BaseModel model;
        public BaseViewData baseViewData;

        public void SetModel(ModuleID moduleID)
        {
            model = MVCManager.Me.GetModel(moduleID);
        }

        public virtual void Bind()
        {

        }
    }
}


