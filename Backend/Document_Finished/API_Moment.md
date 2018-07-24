------

## 发表动态（孙晓青）Finished

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

# 转发动态（李璐）Finished

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
| 1      | 转发者转发了自己的动态 |
| 2      | 转发失败|
| 3      | 转发了已经转发过的动态|

------


【韩乐桐】
# 删除动态 Finished #
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
| 3 | 该动态删除失败 |
| 4 | 该动态删除成功，找不到源动态 |
| 5 | 该动态删除成功，转发表项删除失败 |
| 6 | 成功删除该动态和转发表项，源动态转发数修改失败 |

-------------


【韩乐桐】
# 修改动态 Finished #
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

