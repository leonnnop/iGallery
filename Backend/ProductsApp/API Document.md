------

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
| 1      | 该邮箱已被注册   |
| 2      | 插入数据库时出错 |

------

请求地址：/api/Users/SendMail

说明：邮箱验证，传邮箱地址,

请求方式：Get

支持格式：json

参数类型：string

返回类型：string

返回内容：验证码



------

