------

# 展示朋友圈（吴桐欣）

请求地址：/api/DisplayMoments/Followings

请求方式：Post

支持格式：json

参数类型：Users

| 类型   | 属性   |
| ------ | ------ |
| string | UserID |

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

