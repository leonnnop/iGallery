【韩乐桐】
# ‘发现’界面点赞更新 #
请求地址：/api/Moment/UpdateLiking   

请求方式：Put  
   
支持格式：json  
    


> 参数：   

| 类型 | 参数名 |  
| --- | --- |  
| string | moment_id |  

{moment_id:""}


> 返回类型：int  

| 状态码 | 说明 |  
| -- | -- |  
| 0 | 修改成功 |  
| 1 | 修改失败 |  
| 2 | 找不到动态 |  
  

  
  
  


----------------
  
  
  
【韩乐桐】
# '发现'界面获取动态 #
请求地址：/api/Moment/GetRankingMoments   
请求方式：Get  
支持格式：json    


> 参数：无     


> 返回类型：对象列表  

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



