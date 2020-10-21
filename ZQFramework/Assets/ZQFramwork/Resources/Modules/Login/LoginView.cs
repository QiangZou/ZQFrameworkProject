using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZQFramwork
{
    public class LoginView : BaseView
    {
        public InputField z;
        public InputField m;

        public Action<string, string> login;

        protected LoginViewData LoginViewData { get { return baseViewData as LoginViewData; } }

        public override void Bind()
        {
            LoginViewData.ResetView = ResetView;
        }

        protected override void Start()
        {

        }

        public void ResetView(string z, string m)
        {
            this.z.text = z;
            this.m.text = m;
        }

        public void ButtonLogin()
        {
            string z = this.z.text;
            string m = this.m.text;

            if (LoginViewData.Login != null)
            {
                LoginViewData.Login(z, m);
            }
        }


    }

}
