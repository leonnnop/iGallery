------

## 请求地址：/api/MomentPublish/InsertMoment

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

