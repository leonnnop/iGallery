【韩乐桐】
# 返回点赞列表 #
请求地址：/api/DisplayLikeList/GetLikeList   
 
请求方式：Get  
  
支持格式：json  
    

> 参数：   

| 类型 | 参数名 |  
| --- | --- |   
| string | moment_id |  

{ moment_id:""}   


> 返回类型：对象列表 List<Users>  

| 类型 | 属性 | 说明 |    
| -- | -- |  
| string | ID |  
| string | Username |  
| string | password |  
| string | Photo |   
| string | Email |  
| string | Bio |    


-------------                        
# 返回动态点赞情况 #（陈阳）

请求地址：/api/DisplayLikeList/LikeState   
 
请求方式：Get  
    

> 参数：   

| 类型 | 参数名 |  
| --- | --- |   
| string | user_id |  


> 返回类型：Tuple<List<Moment>, List<Users>>

| 类型    |    属性    | 
Moment
| string  | ID         |  
| string  | SenderID   |  
| string  | Content    |  
| int     | Likenum    |   
| int     | ForwardNum |  
| int     | CollectNum | 
| int     | CommnetNum | 
| string  | Time       |
Users
| string  | ID         |  
| string  | Username   |  
| string  | password   |  
| string  | Photo      |   
| string  | Email      |  
| string  | Bio        | 

|null     |无动态或无用户点赞|   