# 搜索用户（陈阳）

请求地址：/api/Search/Search_user

请求方式：Get

参数类型：string keyword

返回结果：
用户列表：User_list
|状态码  |说明        |
| string | ID         |
| string | Email      |
| string | Password   |
| string | Username   |
| string | Bio        |
| string | Photo      |
| string | FollowState（True为已关注，False为未关注））
|Not found|未找到用户 |



# 搜索动态、标签（陈阳）

请求地址：/api/Search/Search_all

请求方式：Get

参数类型：string keyword

返回结果：
|状态码  |    说明    |

（标签列表）:tags
| string | UserID  |
| string | Tag     |
| string | FollowState（True为已关注，False为未关注） |

（动态列表）:moments
| string | ID          |
| string | Sender_Id   |
| string | Content     |
| int    | Like_num    |
| int    | Forward_num |
| int    | Collect_num |
| int    | Comment_num |
|string  | time        |
|null    |未找到任何相关内容 |