using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

public class LoginViewData : BaseViewData
{
    public Action<string, string> ResetView;

    public Action<string, string> Login;
}
