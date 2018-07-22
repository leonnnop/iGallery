<template>
    <div style="margin-bottom:30px;">
        <el-row>
            <el-col :span="10" :offset="4">
                <el-carousel height="500px" :interval="0" indicator-position="outside">
                    <el-carousel-item v-for="(img,index) in moment.imgList" :key="index">
                        <div class="pic">
                            <img :src="img.url" alt="movementImg">
                        </div>
                    </el-carousel-item>
                </el-carousel>
                <!-- 操作 -->
                <el-row>
                    <el-card :body-style="{ padding: '5px' }" shadow="never">
                        <el-row type="flex" align="middle" style="color:#a2a2a2;">
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img :src="likeSrc" alt="like" class="op-img hover-cursor" @click="likeHandler">
                                    <span @click="likeListHandler" class="hover-cursor">{{moment.LikeNum}}</span>
                                    <el-dialog title="" :visible.sync="likeListVisible" width="40%">
                                        <span slot="title" style="color:#555;font-size:20px;letter-spacing:5px;">点赞</span>
                                        <div style="height:400px;overflow:hidden;overflow-y:auto;">
                                            <div style="border-bottom:1px solid rgb(235,238,245);"></div>
                                            <div v-for="(likeUser,index) in likeUsers" :key="index" class="like-user-li">
                                                <el-row type="flex" justify="center" align="middle">
                                                    <el-col :span="3" :offset="1" style="height:50px;">
                                                        <img src="../image/a.jpg" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(likeUser.userPage)">
                                                    </el-col>
                                                    <el-col :span="16">
                                                        <el-row>{{likeUser.Username}}</el-row>
                                                        <el-row style="font-size:12px;margin-top:5px;">{{likeUser.Bio}}</el-row>
                                                    </el-col>
                                                    <el-col :span="3">
                                                        <el-button plain size="small" @click="followHandler(likeUser,likeUser.FollowState)" :class="{followed:likeUser.FollowState}"
                                                            v-if="likeUser.ID!=$store.state.currentUserId_ID">{{likeUser.followState}}</el-button>
                                                        <!--  -->
                                                    </el-col>
                                                </el-row>
                                            </div>
                                        </div>
                                    </el-dialog>

                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img src="../image/forward.png" alt="forward" class="op-img hover-cursor" @click="forwardHandler">{{moment.ForwardNum}}
                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img src="../image/comment.png" alt="comment" class="op-img hover-cursor">{{moment.CommentNum}}
                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img :src="collectSrc" alt="collect" class="op-img hover-cursor" @click="collectHandler">{{moment.CollectNum}}
                                </el-row>
                            </el-col>
                        </el-row>
                    </el-card>
                </el-row>
                <!-- 评论区 -->

                <el-row class="comment-area">
                    <el-card :body-style="{ padding: '20px' }" shadow="never">
                        <!-- 顶部评论区 -->
                        <el-row type="flex" justify="center">
                            <el-col :span="2">
                                <img src="../image/a.jpg" alt="" style="width:40px;height:40px;border-radius:40px;">
                            </el-col>
                            <el-col :span="21" :offset="1">
                                <el-form ref="blogComment" :model="blogComment">
                                    <el-row>
                                        <el-form-item>
                                            <el-input type="textarea" v-model="blogComment.comment" placeholder="评论（不超过120个字符）" size="medium" resize="none" autosize
                                                maxlength="120"></el-input>
                                        </el-form-item>
                                    </el-row>
                                    <el-row type="flex" justify="end">
                                        <el-form-item>
                                            <el-button type="primary" @click="submitBlogComment" plain size="small">评论</el-button>
                                        </el-form-item>
                                    </el-row>
                                </el-form>
                            </el-col>
                        </el-row>
                        <!-- 评论展示区 -->
                        <div v-for="(comment,index) in comments" :key="index" class="show-comment">
                            <el-row type="flex" align="middle">
                                <el-col :span="3">

                                    <img src="../image/a.jpg" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(comment.userPage)">

                                </el-col>
                                <el-col :span="3">
                                    <span class="hover-cursor" @click="jumpToUser(comment.userPage)">{{comment.Username}}</span>
                                </el-col>
                            </el-row>
                            <el-row>
                                <div class="comment-content">
                                    {{comment.content}}
                                </div>
                            </el-row>
                            <!-- 引用评论 -->
                            <el-row v-if="JSON.stringify(comment.quoteComment)!=='{}'" style="margin-top:10px;">
                                <el-card shadow="never" :body-style="{ padding: '10px' }" v-if="comment.quoteComment!=undefined">
                                    <el-button type="text" @click="jumpToUser(comment.quoteComment.userPage)">@{{comment.quoteComment.Username}}：</el-button>{{comment.quoteComment.content}}
                                </el-card>
                            </el-row>
                            <el-row type="flex" justify="space-between" align="middle">
                                <el-col :span="6">
                                    <time class="time">{{ comment.commentTime }}</time>
                                </el-col>
                                <el-col :span="1">
                                    <el-button type="text" @click="commentAComment(comment)">回复</el-button>
                                </el-col>
                            </el-row>
                            <!-- 点击回复显示评论框 -->
                            <el-row type="flex" align="middle" v-if="comment.isCommentAComment">
                                <el-col :span="24">
                                    <el-form ref="commentComment" :model="commentComment">
                                        <el-col :span="21">
                                            <el-form-item>
                                                <el-input type="textarea" v-model="commentComment.comment" placeholder="评论（不超过120个字符）" resize="none" autosize maxlength="120"></el-input>
                                            </el-form-item>
                                        </el-col>
                                        <el-col :span="2" :offset="1">
                                            <el-form-item>
                                                <el-button type="primary" @click="submitCommentComment(comment)" plain size="small" style="margin-top:8px">评论</el-button>
                                            </el-form-item>
                                        </el-col>

                                    </el-form>
                                </el-col>


                            </el-row>
                        </div>
                    </el-card>

                </el-row>
            </el-col>
            <el-col :span="6" :offset="0">
                <el-card class="aside" shadow="never">
                    <div slot="header" class="clearfix">
                        <el-row type="flex" align="middle">
                            <el-col :span="6" :offset="1">
                                <img src="../image/a.jpg" alt="头像" style="width:80px;height:80px;border-radius:80px;" class="hover-cursor" @click="jumpToUser(moment.userPage)">
                            </el-col>
                            <el-col :span="16">
                                <el-row type="flex" align="middle">
                                    <el-col :span="10" :offset="2">
                                        <span class="hover-cursor" @click="jumpToUser(moment.userPage)">{{moment.Username}}</span>
                                    </el-col>
                                    <el-col :span="4" :offset="4">
                                        <el-button v-if="moment.SenderID!=$store.state.currentUserId_ID" plain size="small" @click="followHandler(moment,moment.followState)" :class="{followed:moment.FollowState}">{{moment.followState}}</el-button>
                                        <i v-if="moment.SenderID==$store.state.currentUserId_ID" class="el-icon-edit" ></i>
                                    </el-col>
                                </el-row>
                                <el-row>
                                    <el-col :span="20" :offset="2" style="font-size:12px;color:#999">{{moment.Time}}</el-col>
                                </el-row>
                            </el-col>

                        </el-row>
                    </div>
                    <div id="content">
                        <div id="text">
                            <p style="margin:0">{{moment.Content}}</p>
                            <el-button type="text" v-for="(tag,index) in moment.tags" :key="index" @click="jumpToTag(tag)">#{{tag}}</el-button>
                        </div>
                    </div>

                </el-card>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default {
        name: 'MomentDetail',
        data() {
            return {
                likeListVisible: false,
                likeUsers: [{
                    ID: '1',
                    headImg: require('../image/a.jpg'),
                    Username: 'user1',
                    Bio: '诚信肥宅',
                    FollowState: true,
                    followState: '已关注'
                }, {
                    ID: '2',
                    headImg: require('../image/a.jpg'),
                    Username: 'user2',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'

                }, {
                    ID: '3',
                    headImg: require('../image/a.jpg'),
                    Username: 'user3',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'
                }, {
                    ID: '4',
                    headImg: require('../image/a.jpg'),
                    Username: 'user4',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'
                }, {
                    ID: '5',
                    headImg: require('../image/a.jpg'),
                    Username: 'user5',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'
                }, {
                    ID: '6',
                    headImg: require('../image/a.jpg'),
                    Username: 'user6',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'
                }, {
                    ID: '7',
                    headImg: require('../image/a.jpg'),
                    Username: 'user7',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'
                }, {
                    ID: '8',
                    headImg: require('../image/a.jpg'),
                    Username: 'user8',
                    Bio: '诚信肥宅',
                    FollowState: false,
                    followState: '关注'
                }],
                ID: '000',
                Username: 'loststars',
                Bio: '万年单身汪',
                likeSrc: require('../image/comment-unlike.png'),
                collectSrc: require('../image/uncollect.png'),
                blogComment: {
                    comment: ''
                },
                commentComment: {
                    comment: ''
                },
                moment: {
                    imgList: [{
                            url: require('../image/ins1.png')
                        },
                        {
                            url: require('../image/ins2.png')
                        },
                        {
                            url: require('../image/ins3.png')
                        },
                        {
                            url: require('../image/a.jpg')
                        }
                    ],
                    ID: '111',
                    Username: 'Leonnnop',
                    SenderID: '16',
                    FollowState: false,
                    followState: '关注',
                    userPage: '',
                    Time: '2018-07-15 00:00',
                    Content: '恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...' +
                        '恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...' +
                        '恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...',
                    tags: [
                        'tag1','tag2','tag3'
                    ],
                    CollectNum: 10,
                    ForwardNum: 0,
                    CommentNum: 3,
                    LikeNum: 8,
                    LikeState: false,
                    collectState: false,
                },
                comments: [{
                    headImg: require('../image/a.jpg'),
                    ID: '222',
                    Username: 'user3',
                    userPage: '',
                    commentTime: '2018-07-18 12:00',
                    content: '第三条评论',
                    isCommentAComment: false,
                    quoteComment: {

                    }
                }, {
                    headImg: require('../image/a.jpg'),
                    ID: '333',
                    Username: 'user2',
                    userPage: '',
                    commentTime: '2018-07-17 15:00',
                    content: '第二条评论',
                    isCommentAComment: false,
                    quoteComment: {

                    }
                }, {
                    headImg: require('../image/a.jpg'),
                    ID: '444',
                    Username: 'user1',
                    userPage: '',
                    commentTime: '2018-07-17 08:00',
                    content: '第一条评论',
                    isCommentAComment: false,
                    quoteComment: {

                    }
                }]

            }
        },
        methods: {
            likeListHandler() {
                this.likeListVisible = true
                this.axios.get('http://10.0.1.8:54468/api/DisplayLikeList/GetLikeList?&moment_id=' + this.$route.params
                        .id + '&email=' + this.$store.state.currentUserId)
                    .then(((response) => {
                        this.likeUsers = response.data;
                        this.likeUsers.forEach(element => {
                            element.headImg = require('../image/a.jpg');
                            // 等待修改
                            // element.FollowState = true;
                            if (element.FollowState == 'true') {
                                element.followState = '已关注'
                            } else {
                                element.followState = '关注'
                            }
                        });
                    }));
            },
            jumpToTag: function (tag) {
                this.$router.push('/main/tag/'+tag);
            },
            jumpToUser: function (url) {
                console.log(url);
                this.$router.push(url)
            },
            collectHandler: function () {
                if (!this.moment.collectState) {
                    this.collectSrc = require('../image/collect.png');
                    this.moment.CollectNum++;
                } else {
                    this.collectSrc = require('../image/uncollect.png');
                    this.moment.CollectNum--;
                }
                this.moment.collectState = !this.moment.collectState;
            },
            likeHandler: function () {
                // console.log(item)
                this.axios.put('http://10.0.1.8:54468/api/DiscoverMoment/UpdateLiking?email=' + this.$store.state.currentUserId +
                    '&moment_id=' + this.moment.MomentID
                    // , {
                    //     email: this.$store.state.currentUserId,
                    //     moment_id: item.MomentId
                    //   }
                )
                if (!this.moment.LikeState) {
                    this.likeSrc = require('../image/comment-like.png');
                    this.moment.LikeNum++;
                    //加入喜欢列表
                    this.likeUsers.push({
                        headImg: require('../image/a.jpg'),
                        //
                        ID: this.$store.state.currentUserId_ID,
                        Username: this.$store.state.currentUsername,
                        Bio: this.$store.state.currentUserBio,

                    });
                } else {
                    this.likeSrc = require('../image/comment-unlike.png');
                    //从喜欢列表删除
                    this.likeUsers.pop();
                    this.moment.LikeNum--;
                }
                this.moment.LikeState = !this.moment.LikeState;
            },
            forwardHandler: function () {

            },
            followHandler: function (user, FollowState) {
                console.log(user)
                console.log(this.$store.state.currentUserId_ID)
                console.log(user.FollowState);

                if (!FollowState) {
                    user.followState = '已关注';
                } else {
                    user.followState = '关注';
                }
                user.FollowState = !user.FollowState;
                console.log(user.FollowState);

                this.axios.get('http://10.0.1.8:54468/api/Users/Follow?followID=' + this.$store.state.currentUserId_ID +
                        '&followedID=' + user.ID)
                    .then((response) => {
                        if (response.data == 0) {
                            // this.$message({
                            //     message: '关注成功',
                            //     type: 'success'
                            // });
                        } else {
                            this.$message.error('关注失败，服务器内部错误，请重试。');
                        }
                    });
            },
            submitBlogComment: function () {
                if (this.blogComment.comment) {
                    this.comments.unshift({
                        headImg: require('../image/a.jpg'),
                        Username: 'loststars',
                        userPage: '',
                        commentTime: '2018-07-18 23:00',
                        content: this.blogComment.comment,
                        isCommentAComment: false,
                        quoteComment: {

                        }
                    });
                    this.moment.CommentNum++;
                    this.$message('评论成功！');
                    this.blogComment.comment = '';

                    this.axios.post('/moment', {
                            CommentNum: this.moment.CommentNum,
                            comment: this.comments[0]
                        })
                        .then((response) => {
                            if (response.data.code) {
                                this.$message('评论成功！');
                                this.blogComment.comment = '';
                            } else {
                                this.$message.error('评论失败，请重试！');
                            }

                        })
                        .catch((error) => {
                            console.log(error);
                        });
                } else {
                    this.$message.error('请输入评论！');
                    return false;
                }


            },
            submitCommentComment: function (comment) {
                if (this.commentComment.comment) {
                    this.comments.unshift({
                        headImg: require('../image/a.jpg'),
                        Username: 'loststars',
                        userPage: '',
                        commentTime: '2018-07-18 20:00',
                        content: this.commentComment.comment,
                        isCommentAComment: false,
                        quoteComment: {
                            Username: comment.Username,
                            userPage: comment.userPage,
                            content: comment.content
                        }
                    });
                    this.moment.CommentNum++;
                    this.$message('评论成功！');
                    this.commentComment.comment = '';
                    this.commentAComment(comment);

                    this.$axios.post('/moment', {
                            CommentNum: this.moment.CommentNum,
                            comment: this.comments[0]
                        })
                        .then((response) => {
                            if (response.data.code) {
                                this.$message('评论成功！');
                                this.commentComment.comment = '';
                            } else {
                                this.$message.error('评论失败，请重试！');
                            }
                        })
                        .catch((error) => {
                            console.log(error);
                        });
                } else {
                    this.$message.error('请输入评论！');
                    return false;
                }

            },
            commentAComment: function (comment) {
                console.log(comment.isCommentAComment);
                comment.isCommentAComment = !comment.isCommentAComment;
            }
        },
        created() {
            //
            this.$axios.get('/moment')
                .then((response) => {
                    // this.moment = response.data.moment;
                    // this.comments = response.data.comments;
                    // this.FollowState = response.data.FollowState;
                    this.moment = response.data;
                    /////
                })
                .catch((error) => {
                    console.log(error);
                });
        }
    }
</script>

<style scoped>
    .pic {
        width: 100%;
        height: 500px;
        text-align: center;
    }

    .pic:before {
        content: "";
        display: inline-block;
        height: 100%;
        vertical-align: middle;
        width: 0;
    }

    .pic img {
        vertical-align: middle;
        width: 100%;
    }

    .aside {
        height: 500px;
        position: relative;
    }

    #content {
        width: 100%;
        height: 380px;
        overflow: hidden;

    }

    #text {
        height: 350px;
        width: 100%;
        overflow-y: auto;
        overflow-x: hidden;

    }

    .time {
        font-size: 13px;
        color: #999;
    }

    .op-img {
        height: 25px;
        width: 25px;
        margin-right: 5%;
    }

    .show-comment {
        border-top: 1px solid rgb(235, 238, 245);
        padding-top: 20px;
        padding-bottom: 10px;
    }

    .show-comment-img {
        width: 50px;
        height: 50px;
        border-radius: 50px;
    }

    .hover-cursor {
        cursor: pointer;
    }

    .like-user-li {
        padding: 10px 0;
        border-bottom: 1px solid rgb(235, 238, 245);
    }

    .followed {
        padding: 9px 9px;
    }
</style>