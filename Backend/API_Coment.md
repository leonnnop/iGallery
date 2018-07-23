#### 删除评论

##### 请求

路由：api/Coment/DelCmt

方式：Post

参数格式：form-data

参数：

| 类型   | 名   | 描述   |
| ------ | ---- | ------ |
| string | id   | 评论id |

##### 响应

OK 

NotFound



#### 保存评论

##### 请求

路由：api/Coment/SvCmt

方式：Post

参数格式：form-data

参数：

| 类型   | 名               | 描述                  |
| ------ | ---------------- | --------------------- |
| string | Mid              | 被评论动态ID          |
| string | Sender_id        | 发表评论的用户ID      |
| string | Content          |                       |
| string | Send_time        |                       |
| string | Quote_comment_id | 二级需 ，一级评论为空 |

##### 响应

OK 

InternalServerError



#### 加载评论

##### 请求

路由：api/Coment/LdCmt?id=“动态ID”

方式：Get

##### 响应

Json

键：自增的数字

值：以下Keys为Attributes的对象

##### ![1532192777619](C:\Users\李\AppData\Local\Temp\1532192777619.png)

```
[
    {(全是string)
        "Mid": "81767",
        "Sender_id": "16",
        "Sender_username": "leonnnop",
        "Cid": "89760",
        "Content": "尝试评论",
        "Quote_username": null,
        "Send_time": "2018/7/23 13:59:22",
        "Quote_id": "",
        "Type": "0",
        "Quote_content": null
    }
]
```

