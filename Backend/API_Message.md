
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

# 获取两个人的私信内容（李璐）

请求地址：/api/Users/Message/GetMessage

说明：传入互发私信两人的id，返回两个人的消息内容ContentList，按照时间顺序排序，用IndentityList数组表明每条消息是谁发的

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | Sender_ID   |
| string | Receiver_ID   |

返回类型：JSON结构（Tuple<List<int>, List<string>>）

JSON样例
| 类型| 属性          |
| ------ | ---------------- |
|List<int>         | IndentityList        |
|LIst<string>    | ContentList        |


------
# 获取私信对象用户（李璐）

请求地址：/api/Users/Message/GetUser

说明：传入发私信的用户的id，返回他曾经发过私信的用户列表

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性     |
| :----- | -------- |
| string | Sender_ID   |


返回类型：JSON结构（UsersList数组）

JSON样例
| 类型| 属性          |
| ------ | ---------------- |
|string         | ID      |
|string         | Email      |
|string         | Username     |
|string         | Bio      |
|string         | Photo     |
|string         | Password      |



------

