
# 发送私信（李璐）

请求地址：/api/Users/Message/SendMessage

说明：将用户间传递的私信存储至数据库

请求方式：Post

支持格式：json

参数类型：Message

| 类型   | 属性     |
| :----- | -------- |
| string | Sender_ID   |
| string | Receiver_ID   |
| string | Send_Time    |
| string | Content    |


返回类型：int

| 状态码 | 说明             |
| ------ | ---------------- |
|0          | 成功         |
| 1        | 失败        |


------
