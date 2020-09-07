using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZQFramwork
{
    public class BaseView : BaseBehaviour
    {
        public ViewType viewType;

        public BaseViewData baseViewData;

        public virtual void Bind()
        {

        }
    }
}
