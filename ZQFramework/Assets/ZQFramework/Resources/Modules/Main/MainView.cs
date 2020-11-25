using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZQFramwork;


namespace ZQFramwork
{
    public class MainView : BaseView
    {
        public Text z;

        public override void Bind()
        {
            z.text = (MVCManager.Me.GetModelData(ModuleID.Login) as LoginModelData).z;
            z.text = LoginModelData.me.z;
        }

        protected override void Start()
        {
            base.Start();
            z.text = LoginModelData.me.z;
        }


        public void ButtonLogin()
        {
            MVCManager.Me.OpenModule(ModuleID.Login);
        }
    }
}

