------

# 展示朋友圈

请求地址：/api/DisplayMoments/Followings

说明：只传用户ID即可，其他设为空

请求方式：Post

支持格式：json

参数类型：Users

| 类型   | 属性     |
| ------ | -------- |
| string | ID       |
| string | Email    |
| string | Password |
| string | Username |
| string | Bio      |
| string | Photo    |

返回类型：DisplayedMoment数组（以下为数组的一项）

| 类型                   | 属性     |
| ---------------------- | -------- |
| Users                  | user     |
| Moment                 | moment   |
| List<DisplayedComment> | comments |
| List<stirng>           | tags     |

DisplayedComment

| 类型             | 属性    |
| ---------------- | ------- |
| Users            | sender  |
| Coment           | comment |
| DisplayedComment | quote   |

------

