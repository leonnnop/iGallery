【韩乐桐】
# ‘发现’界面点赞更新 #
请求地址：/api/Moment/UpdateLiking   

请求方式：Put  
   
支持格式：json  
    


> 参数：   

| 类型 | 参数名 |  
| --- | --- |  
| string | user_id |  
| string | moment_id |  

{user_id:"", moment_id:""}


> 返回类型：int  

| 状态码 | 说明 |  
| -- | -- |  
| 0 | 全部修改成功 |  
| 1 | 部分修改失败 |  
| 2 | 全部修改失败 |  
| 3 | 找不到指定动态 |  

  
 

----------------
  
  
  
【韩乐桐】
# '发现'界面获取动态 #
请求地址：/api/Moment/GetRankingMoments   
请求方式：Get  
支持格式：json    


> 参数：无     


> 返回类型：对象列表 List<User_Moment>  

| 类型 | 属性 | 说明 |    
| -- | -- |  
| string | Username |  
| string | Email |  
| string | Bio |  
| string | Content |  
| int | LikeNum |  
| int | ForwardNum |  
| int | CollectNum |  
| int | CommentNum |  
| string | Time |                            



---------------------



