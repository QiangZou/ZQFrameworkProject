using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZQFramwork
{
    public class LoginModel : BaseModel
    {
        protected LoginModelData LoginModelData { get { return baseModelData as LoginModelData; } }
        protected LoginViewData LoginViewData { get { return baseViewData as LoginViewData; } }

        public LoginModel(BaseViewData baseViewData, BaseModelData baseModelData) : base(baseViewData, baseModelData)
        {

        }

        public override void Bind()
        {
            LoginViewData.Login = Login;
        }

        public override void Open()
        {
            //获取缓存账号密码

            LoginModelData.z = "qwer";
            LoginModelData.m = "1234";

            if (LoginViewData.ResetView != null)
            {
                LoginViewData.ResetView(LoginModelData.z, LoginModelData.m);
            }
        }

        private void Login(string z, string m)
        {
            LoginModelData.z = z;
            LoginModelData.m = m;

            Debug.Log(string.Format("登入成功 账号 {0} 密码 {1}", z, m));

            MVCManager.Me.OpenModule(ModuleID.Main);
        }
    }

}
