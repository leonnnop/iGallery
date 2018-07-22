#### 图片出库

##### 请求

请求路由：/api/Picture?{请求参数}

请求方式：Get

请求参数类型：

| 类型   | 属性 | 描述                  |
| ------ | ---- | --------------------- |
| string | id   | {ID}                  |
| int    | type | {1:动态ID，2：用户ID} |

##### 响应

响应Content部  ByteArrayContent

响应Head部      img/{图片格式}

------

#### 图片入库

##### 请求

请求路由：/api/Picture

请求方式：Post

请求参数格式：form-data

| 类型   | 属性               | 描述              |
| ------ | ------------------ | ----------------- |
| File   | 用户上传的一份文件 |                   |
| string | id                 | {动态ID，用户ID}  |
| int    | type               | {1:动态，2：用户} |

##### 响应

成功数据库操作 OK

失败数据库操作  ISE



#### 代码文件位置

| 文件名               | 路径           |
| -------------------- | -------------- |
| Util.cs              | \              |
| PictureController.cs | \Controller.cs |


