using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class BaseModel
    {
        public ModuleID moduleID;

        private BaseModelData baseModelData;
        public BaseModelData BaseModelData
        {
            get
            {
                if (baseModelData == null)
                {
                    baseModelData = MVCManager.Me.GetBaseModelData(this.moduleID);
                }
                return baseModelData;
            }
        }


        private BaseViewData baseViewData;
        public BaseViewData BaseViewData
        {
            get
            {
                if (baseViewData == null)
                {
                    baseViewData = MVCManager.Me.GetBaseViewData(this.moduleID);
                }
                return baseViewData;
            }
        }

     

        public BaseModel() { }

        public BaseModel(ModuleID moduleID)
        {
            this.moduleID = moduleID;
        }

        public BaseModel(BaseViewData baseViewData, BaseModelData baseModelData)
        {
            //this.BaseViewData = baseViewData;
            //this.BaseModelData = baseModelData;
        }

        public virtual void Bind()
        {

        }


        /// <summary>
        ///注册消息
        /// </summary>
        protected virtual void RegisterMessage()
        {

        }

        /// <summary>
        /// 模块打开
        /// </summary>
        public virtual void Open()
        {

        }

        /// <summary>
        /// 模块关闭
        /// </summary>
        protected virtual void Close()
        {

        }
    }
}
