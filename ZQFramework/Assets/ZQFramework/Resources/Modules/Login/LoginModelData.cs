using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

namespace ZQFramwork
{
    public class LoginModelData : BaseModelData
    {
        public static LoginModelData me;

        public string z;
        public string m;

        public LoginModelData()
        {
            me = this;
        }
    }
}

