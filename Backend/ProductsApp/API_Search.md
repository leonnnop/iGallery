# 搜索用户（陈阳）

请求地址：/api/Search/Search_user

请求方式：Get

参数类型：string keyword

返回结果：
|状态码   |说明       |
|List     |用户列表   |
|Not found|未找到用户 |



# 搜索动态、标签（陈阳）

请求地址：/api/Search/Search_all

请求方式：Get

参数类型：string keyword

返回结果：
|状态码         |说明               |
|List<Tag>      |标签列表           |
|List<Users>    |用户列表           |
|List<Moment>   |动态列表           |
|null           |未找到任何相关内容 |