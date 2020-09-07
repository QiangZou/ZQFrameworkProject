# -*- coding: UTF-8 -*-
import os
print("开始生成C#脚本")

filelist = os.listdir("Proto")  # 返回path下所有文件构成的一个list列表

for item in filelist:  # 遍历输出每一个文件的名字和类型
    if (item.endswith('.proto')):  # 输出指定后缀类型的文件
        print "生成 " + item
        #main = "protoc.exe  --csharp_out=CSharp Proto/ExampleConfig.proto"
        main = "protoc.exe  --csharp_out=CSharp Proto/" + item
        print(main)

        r_v = os.system(main)
        print (r_v )




raw_input()  #获取控制台的输入