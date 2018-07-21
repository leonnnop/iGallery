------

# 返回个人主页动态内容（李璐）

请求地址：/api/HomePage/GetMyMoments

说明：传本用户的ID

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性     |
| ------ | -------- |
| string | Sender_id       |


返回类型：JSON结构（Moment数组）

样例JSON

| 类型                   | 属性     |
| ---------------------- | -------- |
| string               | ID   |
| string               | SenderID |
| string               | Content |
| string               | LikeNum     |
| string               | ForwardNum    |
| string               | CollectNum   |
| string               | CommentNum |
| string               | Time   |

------

