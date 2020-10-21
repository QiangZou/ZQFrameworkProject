using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class BaseController
    {
        private BaseModel baseModel;

        public BaseController(BaseModel baseModel)
        {
            this.baseModel = baseModel;
        }

        public virtual void Open()
        {
            Debug.Log("Open");
            baseModel.Open();
        }
    }
}