using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class BaseMainView : BaseView
    {
        public ModuleID moduleID;
        public ViewType viewType;



        protected override void Awake()
        {
            base.Awake();
            SetAllViewModel();
        }

        private void SetAllViewModel()
        {
            SetModel(moduleID);

            BaseView[] baseViews = transform.GetComponentsInChildren<BaseView>(true);
            foreach (var item in baseViews)
            {
                item.SetModel(moduleID);
            }

        }
    }
}