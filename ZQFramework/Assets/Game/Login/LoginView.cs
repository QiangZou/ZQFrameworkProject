using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using ZQFramwork;
public class LoginView : BaseView

{
    public InputField z;
    public InputField m;

    protected override void Start()
    {

    }


    public void Button()
    {
        string z = this.z.text;
        string m = this.m.text;

        MVCManager.Me.GetController<LoginController>().Login(z,m);
    }

    
}
