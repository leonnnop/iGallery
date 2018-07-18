------

请求地址：/api/Users/Users

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
| 1      | 该邮箱已被注册   |
| 2      | 插入数据库时出错 |

------

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

<<<<<<< Updated upstream
请求地址：/api/Users/SendMail

说明：邮箱验证，只传邮箱地址即可，其他可为空

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

返回类型：string
返回内容：验证码



------
=======
>>>>>>> Stashed changes

