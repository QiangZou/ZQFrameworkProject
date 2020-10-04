using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using ZQFramwork;

public class Test : MonoBehaviour
{

    private static Test instance;

    // Use this for initialization
    void Start()
    {
        //OpenDirectoryUtils.Open("C:\\");

        //Thread thr1 = new Thread(Th_test1);
        //Thread thr2 = new Thread(Th_test2);
        //Thread thr3 = new Thread(Th_test3);


        //thr1.Start("参数");
        //thr2.Start();
        //thr3.Start();


        Debug.Log(TimeUtil.GetChineseData(637373687463016454));

    }

    void Th_test1(object data)
    {
        print("Th_test1" + data);
    }

    void Th_test2()
    {
        print("Th_test2");


    }
    void Th_test3()
    {
        print("Th_test3");


    }

    // Update is called once per frame
    void Update()
    {

    }
}

