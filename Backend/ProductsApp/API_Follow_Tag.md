------

# 关注标签（苏昭帆）

请求地址：/api/Follow_Tag/FollowTag

说明：用户关注标签。传参：用户ID、标签名称

请求方式：Put

支持格式：json

参数类型：string	string

| 类型   | 属性     |
| :----- | -------- |
| string | UserId       |
| string | tag    |

返回类型：string

| 状态码 | 说明             |
| ------ | ---------------- |
| 200      | 关注成功         |
| 403      | 关注失败         |

------

# 取关标签（苏昭帆）

请求地址：/api/Follow_Tag/CancleFollowTag

说明：用户取关标签。传参：用户ID、标签名称

请求方式：Put

支持格式：json

参数类型：string	string

| 类型   | 属性     |
| :----- | -------- |
| string | UserId       |
| string | tag    |

返回类型：string

| 状态码 | 说明             |
| ------ | ---------------- |
| 200      | 取关成功         |
| 403      | 取关失败         |

------