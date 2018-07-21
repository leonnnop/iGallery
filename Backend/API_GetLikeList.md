【韩乐桐】
# 返回点赞列表 #
请求地址：/api/DisplayLikeList/GetLikeList   
 
请求方式：Get  
  
支持格式：json  
    

> 参数：   

| 类型 | 参数名 |  
| --- | --- |   
| string | email |  
| string | moment_id |    

- 前端需要传入当前用户（发表该动态的用户）邮箱以及当前动态id

{ email:"",moment_id:""}   


> 返回类型：对象列表 List<User_Follow>  

| 类型 | 属性 | 说明 |    
| -- | -- |  
| string | Username |  
| string | Photo |   
| string | Email |  
| string | Bio |    
| string | FollowState |

>注：
- 出于安全考虑，只返回点赞该条动态的用户姓名、头像、邮箱以及个人简介
- FollowState属性表示当前用户是否关注该点赞用户，“true”表示已关注，"false"表示未关注


-------------                        
