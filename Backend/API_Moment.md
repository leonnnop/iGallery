------

## 发表动态（孙晓青）

请求地址：/api/Moment/InsertMoment

说明：发表动态（将新动态插入数据库）

请求方式：Post

支持格式：json

参数类型：Moment

| 类型     | 属性       |
| :------- | ---------- |
| string   | ID         |
| string   | SenderID   |
| string   | Content    |
| int      | LikeNum    |
| int      | ForwardNum |
| int      | CollectNum |
| int      | CommentNum |
| DateTime | Time       |

返回类型：int

| 状态码 | 说明     |
| ------ | -------- |
| 0      | 插入成功 |
| 1      | 插入失败 |

------
------

# 转发动态（李璐）

请求地址：/api/Moment/ForwardMoment

说明：转发动态（将转发者的id和转发动态的id建立联系，加入数据库）

请求方式：Post

支持格式：json

参数类型：Forward

| 类型     | 属性       |
| :------- | ---------- |
| string   | User_Id      |
| string   | Moment_Id   |


返回类型：int

| 状态码 | 说明     |
| ------ | -------- |
| 0      | 转发成功 |
| 1      | 转发失败 |

------


【韩乐桐】
# 删除动态 #
请求地址：/api/ModifyMoment/DeleteMoment   
 
请求方式：Put  
  
支持格式：json  
    

> 参数：   

| 类型 | 参数名 |  
| --- | --- |   
| string | email |   
| string | moment_id |  

{ email:"", moment_id:""}   


> 返回类型：int 

| 状态码 | 说明 |    
| -- | -- |  
| 0 | 成功删除 |  
| 1 | 不存在该用户 |  
| 2 | 该用户没有发表过该动态 |  
| 3 | 动态删除失败 |   


-------------                        


【韩乐桐】
# 修改动态 #
请求地址：/api/ModifyMoment/ModifyMoment  
 
请求方式：Put  
  
支持格式：json  
    

> 参数（FromBody）： Modify_Moment对象  

| 类型 | 参数名 |  
| --- | --- |   
| string | email |   
| string | moment_id |  
| string | content |  

{ email:"", moment_id:"", content:"" }   


> 返回类型：int 

| 状态码 | 说明 |    
| -- | -- |  
| 0 | 修改成功 |  
| 1 | 不存在该用户 |  
| 2 | 该用户没有发表过该动态 |  
| 3 | 动态修改失败 |   


-------------                        
