# -*- coding: UTF-8 -*-

print("开始编译")
import os
import xlrd  #导入xlrd库


def CheckExcelSheet(sheet):

    print "开始检查表格：", sheet.name

    return True


def BuildSheetToProtoFile(sheet, name):
    nrows = sheet.nrows  #获取该sheet中的有效行数
    #print "有效行数", nrows

    ncols = sheet.ncols  #获取列表的有效列数
    #print "有效列数" , ncols

    # for i in range(ncols):
    #     for j in range(nrows):
    #         print '列=%d,行=%d' %(i,j), sheet.cell(j, i).value

    text = '''syntax = "proto3";

message %s
{
%s
}'''

    sub = '''
    %s%s %s = %s;//%s
    '''

    allSub = ""
    for i in range(ncols):

        typeName = ""
        if (sheet.cell(1, i).value.encode("utf-8") == "数组"):
            typeName = "repeated "

        allSub = allSub + sub % (typeName, sheet.cell(
            0, i).value, sheet.cell(2, i).value, i + 1, sheet.cell(3, i).value)

    text = text % (name, allSub)
    #print text

    fo = open("Proto/" + name + ".proto", "w+")
    fo.write(text.encode("utf-8"))

    # 关闭打开的文件
    fo.close()

    return


def BuildExcelToProtoFile(excelPath):
    #print excelPath  #输出Excel文件相对路径

    name = os.path.splitext(os.path.basename(excelPath))[0]  #获取文件名
    print "文件名 " + name

    excel = xlrd.open_workbook(excelPath)  #获取Excel实例

    sheet = excel.sheets()[0]  #获取第一张表格
    #print sheet.name

    if (CheckExcelSheet(sheet) == False):  #检查表格
        return

    BuildSheetToProtoFile(sheet, name)

    return


# path表示路径
path = "Excel"

filelist = os.listdir(path)  # 返回path下所有文件构成的一个list列表

for item in filelist:  # 遍历输出每一个文件的名字和类型
    if (item.endswith('.xls')):  # 输出指定后缀类型的文件
        #print(item)
        BuildExcelToProtoFile(path + "/" + item)

os.system('python BuildCSharp.py')

print("结束编译")


raw_input()  #获取控制台的输入
