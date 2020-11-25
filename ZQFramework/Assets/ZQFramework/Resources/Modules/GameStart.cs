using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZQFramwork;

namespace ZQFramwork
{
    public class GameStart : MonoBehaviour
    {

        public void Start()
        {
            MVCManager.Me.OpenModule(ModuleID.Login);

        }
    }
}

