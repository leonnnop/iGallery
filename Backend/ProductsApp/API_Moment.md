------

## 请求地址：/api/Moment/InsertMoment
（孙晓青）

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

（李璐）
## 请求地址：/api/Moment/ForwardMoment

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

