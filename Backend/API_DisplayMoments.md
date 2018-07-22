------

# 展示朋友圈（吴桐欣）

请求地址：/api/DisplayMoments/Followings

请求方式：Get

支持格式：json

参数类型：string

| 类型   | 属性  | 说明                            |
| ------ | ----- | ------------------------------- |
| string | Email | 用户的email                     |
| int    | Begin | 返回的第一条动态是第Begin条动态 |
| int    | End   | 返回的最后一条动态是第End条动态 |

返回

如果没有更多动态，返回bool值false，否则返回：

```json
[
    {
        (string)"user_email": "2.com",
        (string)"user_username": "szf",
        (string)"user_bio": null,
        (string)"forwarded_email": null,
        (string)"forwarded_username": null,
        "moment": {
            (string)"ID": "2",
            (string)"SenderID": "2",
            (string)"Content": "今天遇见了Troye!",
            (int)"LikeNum": 8,
            (int)"ForwardNum": 0,
            (int)"CollectNum": 0,
            (int)"CommentNum": 3,
            (string)"Time": null,
            (string)"QuoteMID": null
        },
        "tags": [
            (string)"233"
        ],
        (int)"liked": 0,//0-已点赞/收藏
        (int)"collected": 1,//1-未点赞/收藏
        "comments": [
            {
                (string)"sender_email": "2.com",
                (string)"sender_username": "szf",
                (string)"content": "hhh",
                (string)"send_time": "2018/7/20 8:37:18",
                (string)"quote": null
            }
        ],
        (bool)"more_comments": false//是否有更多评论
    }
]
```



------

