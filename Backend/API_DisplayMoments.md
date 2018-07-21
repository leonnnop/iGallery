------

# 展示朋友圈（吴桐欣）

请求地址：/api/DisplayMoments/Followings

请求方式：Post

支持格式：json

参数类型：string

| 类型   | 属性  |
| ------ | ----- |
| string | Email |

返回类型：

```json
[
	{
        "user": {

            (string)"ID": null,（不提供此数据，永远为null）

            (string)"Email": "",

            (string)"Password": null,（不提供此数据，永远为null）

            (string)"Username": "",

            (string)"Bio":"",

            (string)"Photo": ""

        },
        
        "forwarded": {

	        (string)"ID": null,（不提供此数据，永远为null）

            (string)"Email": "",

            (string)"Password": null,（不提供此数据，永远为null）

            (string)"Username": "",

            (string)"Bio": "",

            (string)"Photo": ""

	}

        "moment": {

            (string)"ID": "5",

            (string)"SenderID": "2",

            (string)"Content": "happy today",

            (int)"LikeNum": 2,

            (int)"ForwardNum": 0,

            (int)"CollectNum": 0,

            (int)"CommentNum": 0,

            (string)"Time": "2018/7/20 8:37:18",

            (string)"QuoteMID": ""

        },

        "tags": [

            (string)"",
    		(string)""

        ],

        (int)"liked": 1,

        (int)"collected": 1,

        "comments": [
            {
                "sender": {
                    (string)"ID": null,（不提供此数据，永远为null）
                    (string)"Email": "",
                    (string)"Password": null,（不提供此数据，永远为null）
                    (string)"Username": "",
                    (string)"Bio": null,（不提供此数据，永远为null）
                    (string)"Photo": ""
                },
                "comment": {
                    (string)"ID": "",
                    (string)"Content": "",
                    (string)"SendTime": "",
                    (string)"QuoteID": ""
                },
                "quote": {
                	"sender": {
                    	(string)"ID": null,（不提供此数据，永远为null）
                    	(string)"Email": "",
                    	(string)"Password": null,（不提供此数据，永远为null）
                    	(string)"Username": "",
                    	(string)"Bio": null,（不提供此数据，永远为null）
                    	(string)"Photo": ""
                	},
                	"comment": {
                    	(string)"ID": "",
                    	(string)"Content": "",
                    	(string)"SendTime": "",
                    	(string)"QuoteID": ""
                	},
                	"quote": null（不提供此数据，永远为null）
            	}
            }
        ]

    }
]
```



------

