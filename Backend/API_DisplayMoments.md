------

# 展示朋友圈（吴桐欣）

请求地址：/api/DisplayMoments/Followings

请求方式：Get

参数类型

| 类型   | 属性  | 说明                            |
| ------ | ----- | ------------------------------- |
| string | Email | 用户的email                     |
| int    | Begin | 返回的第一条动态是第Begin条动态 |
| int    | End   | 返回的最后一条动态是第End条动态 |

返回类型

如果没有更多动态，返回bool值false，否则返回：

```json
[
    {
        (string)"user_email": "2.com",
        (string)"user_username": "szf",
        (string)"user_bio": null,
        (int)"follow_state": 0,//为0或-1，可忽略
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

# 单个动态的详情页（吴桐欣）

请求地址：/api/DisplayMoments/Detail

请求方式：Get

参数类型

| 类型   | 属性     | 说明               |
| ------ | -------- | ------------------ |
| string | UserID   | 发送请求的用户的ID |
| string | MomentID | 动态的ID           |

返回类型

```json
{
    (string)"user_email": "wtx@163.com",//动态发送者
    (string)"user_username": "SarashWtx",
    (string)"user_bio": "sarash",
    (int)"follow_state": 1,//0-已关注，1-未关注，-1-是自己
    (string)"forwarded_email": null,
    (string)"forwarded_username": null,//转发自…
    "moment": {
        (string)"ID": "3",
        (string)"SenderID": "3",
        (string)"Content": "今天太阳好毒！",
        (int)"LikeNum": 4,
        (int)"ForwardNum": 0,
        (int)"CollectNum": 0,
        (int)"CommentNum": 0,
        (string)"Time": "2018/7/21 11:07:49",
        (string)"QuoteMID": null
    },
    "tags": [
        (string)"233",
        (string)"世界杯"
    ],
    (int)"liked": 0,//0-已点赞/收藏
    (int)"collected": 1,//1-未点赞/收藏
    "comments": [
        {
            (string)"sender_email": "wtx@163.com",
            (string)"sender_username": "SarashWtx",
            (string)"id": "3",//评论的ID
            (string)"content": "HH！！2",
            (string)"send_time": "2018/3/20 19:31:34",
            		"quote": {
            			(string)"sender_email": "",
            			(string)"sender_username": "",
            			(string)"id": "",//评论的ID
            			(string)"content": "",
            			(string)"send_time": "",
            			(string)"quote":null//不展示多级评论
        			}
        }
    ],
    "more_comments": false
}
```

