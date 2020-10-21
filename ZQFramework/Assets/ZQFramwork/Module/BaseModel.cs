using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class BaseModel
    {
        protected BaseModelData baseModelData;
        protected BaseViewData baseViewData;

        public BaseModel(BaseViewData baseViewData, BaseModelData baseModelData)
        {
            this.baseViewData = baseViewData;
            this.baseModelData = baseModelData;
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
