------

# 注册用户（吴桐欣）Finished

请求地址：/api/Users/Register

说明：注册用户，只传用户名、邮箱、密码即可，其他设为空

请求方式：Post

支持格式：json

参数类型：Users

| 类型   | 属性     |
| :----- | -------- |
| string | ID       |
| string | Email    |
| string | Password |
| string | Username |
| string | Bio      |
| string | Photo    |

返回类型：int

| 状态码 | 说明             |
| ------ | ---------------- |
| 0      | 注册成功         |
| 1      | 注册失败         |

------
# 注册验证邮箱（李璐）Finished

请求地址：/api/Users/VerifyRegister

说明：验证邮箱是否已经被注册，若没有被注册则发送给邮箱一个验证码

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | Email    |


返回类型：string

| 状态码 | 说明             |
| ------ | ---------------- |
|yzm（验证码）       | 邮箱没有被注册过，给邮箱发送一个验证码         |
| 1        | 邮箱被注册过         |

------
# 登录（陈阳）Finished
请求地址：/api/Users/Login   
请求方式：Get   
支持格式：json    


> 参数：string Email，string Password

> 返回消息：
| 状态码   | 说明                 |
| -------- | ---------------------|
| string   | 登录成功，返回用户ID |
| Error    | 密码错误             |
| NotFound | 找不到此用户         |

------

# 查询用户邮箱（苏昭帆）

请求地址：api/Users/SearchEmail
说明：查询用户邮箱，参数为字符串，用户邮箱

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | email    |


返回类型：bool

| 状态码 | 说明             |
| ------ | ---------------- |
| true      | 邮箱存在         |
| false      | 邮箱不存在  |

------

# 修改用户密码（苏昭帆）Finished

请求地址：api/Users/ChangePassword
说明：修改用户密码，参数为字符串，用户邮箱，用户新密码

请求方式：Put

支持格式：json

参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | email   |
| string | NewPassword   |

返回类型：bool

| 状态码 | 说明             |
| ------ | ---------------- |
| true      | 修改成功         |
| false      | 修改不成功   |
------

#邮箱验证 Finished#
请求地址：/api/Users/VerifyMail

说明：邮箱验证是否存在，并向邮箱传递一个验证码

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | email    |


返回类型：string

| 状态码 | 说明             |
| ------ | ---------------- |
| yzm（验证码）     | 邮箱存在,并返回验证码         |
| false      | 邮箱不存在  |

-------------


# 获取个人信息(韩乐桐) Finished #
请求地址：/api/Users/GetUserInfo  
请求方式：Get   
支持格式：json    


> 参数：   

| 类型 | 参数名 | 说明 |
| ---- | ---- | ---- |
| string | email | 通过email唯一确定用户 |


> 返回结果：json文件   


| 参数名 | 说明 |
| -- | -- |
| ID | 用户ID |
| Email | 邮箱地址 |
| Username | 用户昵称 |
| Password | 用户密码 |
| Bio | 个人简介 |
| Photo | 头像URL |

------


# 通过id获取个人信息(韩乐桐)#
请求地址：/api/Users/GetUserInfobyID  
请求方式：Get   
支持格式：json    


> 参数：   

| 类型 | 参数名 | 说明 |
| ---- | ---- | ---- |
| string | id | 通过id唯一确定用户 |

{id:""}

> 返回结果：json文件   


| 参数名 | 说明 |
| -- | -- |
| ID | 用户ID |
| Email | 邮箱地址 |
| Username | 用户昵称 |
| Password | 用户密码 |
| Bio | 个人简介 |
| Photo | 头像URL |

------


# 修改个人信息（韩乐桐）Finished #
请求地址：/api/Users/ModifyUserInfo   
请求方式：Put   
支持格式：json    


> 参数（body中）：   

| 类型 | 参数名 |
| --- | --- |
| string | id |
| string | UserName |
| string | Bio |



> 返回结果：  

| 状态码 | 说明 |
| -- | -- |
| 0 | 成功修改用户信息 |
| 1 | 修改失败 |

 



------

# （取消）关注用户 （陈阳、李璐）Finished
请求地址：/api/Users/Follow
请求方式：Get   
支持格式：json    
参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | followID （关注用户）   |
| string | followedID  （被关注用户） |


返回类型：int

| 状态码 | 说明 |
| -- | -- |
| 0 | 成功 |
| 1 | 失败 |
------

# 查看关注列表 （陈阳）Finished
请求地址：/api/Users/FollowList
请求方式：Get   
参数类型：string
参数：userID


> 返回消息：
|状态码    |说明      |
| string   | ID       |
| string   | Email    |
| string   | Password |
| string   | Username |
| string   | Bio      |
| string   | Photo    |
未找到
| string   | Not found|

-------

# 查看粉丝列表 （陈阳）Finished
请求地址：/api/Users/FanList
请求方式：Get   
参数类型：string
参数：user_id


> 返回消息：
用户列表：followed_list
|状态码    |说明      |
| string   | ID       |
| string   | Email    |
| string   | Password |
| string   | Username |
| string   | Bio      |
| string   | Photo    |
| string   | FollowState（True为已关注，False为未关注）|
未找到
| string   | Not found|


=======

>>>>>>> Stashed changes



------

# 返回关注状态（吴桐欣）

请求地址：/api/Users/FollowState

请求方式：Get

参数   

| 类型   | 参数名  |
| ------ | ------- |
| string | from_id |
| string | to_id   |

返回

| 状态码 | 说明   |
| ------ | ------ |
| -1     | 是自己 |
| 0      | 已关注 |
| 1      | 未关注 |