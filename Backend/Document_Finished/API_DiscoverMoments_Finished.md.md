【韩乐桐】
# ‘发现’界面点赞更新 #
请求地址：/api/Moment/UpdateLiking   

请求方式：Put  
   
支持格式：json   
  
功能说明：若用户未点赞，则进行点赞，若已点赞则取消
    


> 参数：   

| 类型 | 参数名 |  
| --- | --- |   
| string | email |   
| string | moment_id |  

{email:"", moment_id:""}


> 返回类型：int  

| 状态码 | 说明 |  
| -- | -- |  
| 0 | 修改成功 |  
| 1 | 部分修改失败 |  
| 2 | 两个表都修改失败 |  
| 3 | 找不到动态 |  
| 4 | 用户不存在 |  

  
 

----------------
  
  
【韩乐桐】
# '发现'界面获取动态 #
请求地址：/api/Moment/GetRankingMoments  
  
请求方式：Get
    
支持格式：json  
  
功能说明：传入当前用户的email，按点赞数降序返回<=20个动态    


> 参数：  

| 类型 | 参数名 |  
| --- | --- |   
| string | email |   

{email:""}   


> 返回类型：对象列表  

| 类型 | 属性 | 说明 |    
| -- | -- |  
| string | SenderID |   
| string | Username |  
| string | Email |  
| string | Bio |  
| string | Photo |  
| string | Content |  
| int | LikeNum |  
| int | ForwardNum |  
| int | CollectNum |  
| int | CommentNum |  
| string | Time |     
| string | MomentID |  
| string | LikeState |  



>注：
>
- Username, Email, Bio, Photo 为发表该条动态的用户名、邮箱、个人简介以及头像
- Content, LikeNum, ForwardNum, CollectNum, CommentNum, Time 分别为该条动态的相关信息
- LikeState 表示当前用户对该条动态的点赞状态
- MomentID 为该条动态的id，用于后续传参
  
                       



---------------------



