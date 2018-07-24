# API Collection 收藏夹

——by 孙晓青

## 1. InsertCollection

功能说明：创建收藏夹

后台操作：在数据库的收藏夹表（Collection）中增加一项

请求地址： /api/Collection/InsertCollection

请求类型：Post

输入参数：

| 参数名称   | 参数类型 | 说明                   |
| ---------- | -------- | ---------------------- |
| founder_id | string   | 请求创建收藏夹的用户ID |
| name       | string   | 收藏夹名称             |

输出状态码（status）：

| 状态码 | 含义                                                         |
| ------ | ------------------------------------------------------------ |
| 0      | 新收藏夹记录成功插入数据库                                   |
| 1      | 新收藏夹与该用户的现有收藏夹**重名**而无法创建（前端应提示用户） |
| 2      | ....插入失败                                                 |



## 2. DeleteCollection

功能说明：删除收藏夹（收藏夹下原有的所有动态也被取消收藏）

后台操作：在数据库的收藏夹表（Collection）中删除一项，收藏联系集中的相关记录也被级联删除

请求地址： /api/Collection/DeleteCollection

请求类型：Post

输入参数：

| 参数名称   | 参数类型 | 说明                   |
| ---------- | -------- | ---------------------- |
| founder_id | string   | 请求删除收藏夹的用户ID |
| name       | string   | 收藏夹名称             |

输出状态码（status）：

| 状态码 | 含义                                   |
| ------ | -------------------------------------- |
| 0      | ....删除成功                           |
| 1      | name = “默认收藏夹”——>无法执行删除操作 |
| 2      | ....删除失败                           |



## 3. RenameCollection

功能说明：重命名收藏夹（新名字不可与同一用户的其他现有收藏夹重复）

后台操作：检查重命名操作是否会导致收藏夹重名，不会重名的情况下再更新数据库收藏夹表（Collection）的相应记录（涉及级联更新）

请求地址： /api/Collection/RenameCollection

请求类型：Put

输入参数：

| 参数名称      | 参数类型 | 说明                     |
| ------------- | -------- | ------------------------ |
| founder_id    | string   | 请求重命名收藏夹的用户ID |
| original_name | string   | 收藏夹原来的名字         |
| new_name      | string   | 收藏夹的新名字           |

输出状态码（status）：

| 状态码 | 含义                                                         |
| ------ | ------------------------------------------------------------ |
| 0      | ....重命名成功                                               |
| 1      | name = “默认收藏夹”——>无法执行重命名操作                     |
| 2      | 新名字与同一用户的其他现有收藏夹名称重复，不执行重命名操作（前端应给用户提示） |
| 3      | ....更新数据库信息时出现错误，重命名失败                     |



## 4. ReturnMomentNumInACollection

功能说明：统计某一收藏夹下的动态总数

请求地址： /api/Collection/ReturnMomentNumInACollection

输入参数：

| 参数名称   | 参数类型 | 说明                 |
| ---------- | -------- | -------------------- |
| founder_id | string   | 收藏夹所有者的用户ID |
| name       | string   | 收藏夹名称           |

返回值类型：int   （该收藏夹下的动态总数）



## 5. GetFirstPicIDbyMomentID

功能说明：根据动态ID获取该动态第一张图片的ID

请求地址： /api/Collection/GetFirstPicIDbyMomentID

输入参数：

| 参数名称  | 参数类型 | 说明   |
| --------- | -------- | ------ |
| moment_id | string   | 动态ID |

返回值类型：string （图片ID）



## 6. ReturnCollectionCoverPicID

功能说明：

获取收藏夹中最新加入的一条动态里的第一张图片的ID作为收藏夹封面

请求地址： /api/Collection/ReturnCollectionCoverPicID

输入参数：

| 参数名称 | 参数类型 | 说明                 |
| -------- | -------- | -------------------- |
| FounerID | string   | 收藏夹所有者的用户ID |
| Name     | string   | 收藏夹名称           |

返回值类型：string （封面图片ID——包含默认封面）



## 7. ReturnUserCollections

功能说明：

从数据库中获取某一用户的全部收藏夹（除默认收藏夹外）并以Json格式返回给前端

请求地址： /api/Collection/ReturnUserCollections

请求类型：Get

输入参数：

| 参数名称 | 参数类型 | 说明   |
| -------- | -------- | ------ |
| user_id  | string   | 用户ID |

返回值类型：Json数组

[ { "FOUNDER_ID":"1" , "NAME":"吃吃吃"},

  { "FOUNDER_ID":"1" , "NAME":"旅行"} ] 



## 8. ReturnCollectionContentID

功能说明：

从数据库中获取某一收藏夹下全部动态的ID(按动态收藏时间由新到旧排序)并以string列表形式返回给前端

请求地址： /api/Collection/ReturnCollectionContentID

输入参数：

| 参数名称 | 参数类型 | 说明                 |
| -------- | -------- | -------------------- |
| FounerID | string   | 收藏夹所有者的用户ID |
| Name     | string   | 收藏夹名称           |

返回值类型：List<string>     （动态ID的列表）



## 9. MoveMomentToAnotherCollection

功能说明：移动收藏的动态到其他收藏夹

后台操作：更新收藏联系集（Collect）中对应的记录项

请求地址： /api/Collection/MoveMomentToAnotherCollection

请求类型：Put

输入参数：

| 参数名称                 | 参数类型 | 说明                       |
| ------------------------ | -------- | -------------------------- |
| moment_id                | string   | 要移动的动态ID             |
| founder_id               | string   | 收藏夹所有者的用户ID       |
| original_collection_name | string   | 动态原来所在的收藏夹名称   |
| new_collection_name      | string   | 动态将要移动到的收藏夹名称 |

输出状态码：

| 状态码 | 含义           |
| ------ | -------------- |
| 0      | 数据库更新成功 |
| 1      | 更新出现错误   |
