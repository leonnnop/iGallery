将文件 Utility.cs 加入项目

头部声明
using Utility;


1、图片出库

* 调用

  * 个人头像 Util.Get(“你的用户id”, 2)；
  * 动态组图 Util.Get(“你的动态id”, 1)；

* 返回 List< ByteArrayContent  >

  

2、图片入库

* 调用
  * 个人头像 Util.Post(“你的用户id”, 2)；
  * 动态组图 Util.Post(“你的动态id”, 1)；



