# API Collect 收藏

—— by 孙晓青

## 1. InsertCollect

功能说明：收藏一条动态到某个指定的收藏夹中

后台操作：在数据库的收藏联系集中新增一项，更新动态实体集中的COLLECT_NUM

请求地址： /api/Collect/InsertCollect

请求类型：Post

输入参数：

| 参数名称   | 参数类型 | 说明                 |
| :--------- | -------- | -------------------- |
| moment_id  | string   | 动态ID               |
| founder_id | string   | 收藏夹所有者的用户ID |
| name       | string   | 收藏夹名称           |

返回状态码（status）：

| 状态码 | 含义                                      |
| ------ | ----------------------------------------- |
| 0      | 新动态插入数据库成功，COLLECT_NUM更新成功 |
| 1      | ....插入失败                              |
| 2      | ....COLLECT_NUM更新失败                   |



## 2. DeleteCollect

功能说明：取消收藏动态

后台操作：在数据库的收藏联系集中删除对应项，更新动态实体集中的COLLECT_NUM

请求地址： /api/Collect/DeleteCollect

请求类型：Post

输入参数：

| 参数名称  | 参数类型 | 说明                     |
| :-------- | -------- | ------------------------ |
| moment_id | string   | 动态ID                   |
| user_id   | string   | 请求取消收藏动态的用户ID |

返回状态码（status）：

| 状态码 | 含义                                            |
| ------ | ----------------------------------------------- |
| 0      | 成功删除数据库中的相应记录，COLLECT_NUM更新成功 |
| 1      | ....删除失败                                    |
| 2      | ....COLLECT_NUM更新失败                         |