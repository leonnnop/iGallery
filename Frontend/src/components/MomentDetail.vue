<template>
    <div style="margin:30px 0;">
        <el-row>
            <el-col :span="10" :offset="3">
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
                                    <img :src="likeSrc" alt="likeButton" class="op-img" @click="likeHandler">{{this.moment.likeNum}}
                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img src="../image/forward.png" alt="likeButton" class="op-img" @click="forwardHandler">{{this.moment.forwardNum}}
                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img src="../image/comment.png" alt="likeButton" class="op-img">{{this.moment.commentNum}}
                                </el-row>
                            </el-col>
                            <el-col :span="6">
                                <el-row type="flex" align="middle" justify="center">
                                    <img :src="collectSrc" alt="collectNum" class="op-img" @click="collectHandler">{{this.moment.collectNum}}
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
                                <img src="../image/a.jpg" alt="" style="width:40px;height:40px;border-radius:80px;">
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

                                    <img src="../image/a.jpg" alt="" class="show-comment-img" style="cursor:pointer" @click="jumpToUser(comment.userPage)">

                                </el-col>
                                <el-col :span="3">
                                    <span style="cursor:pointer" @click="jumpToUser(comment.userPage)">{{comment.username}}</span>
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
                                    <el-button type="text" @click="jumpToUser(comment.quoteComment.userPage)">@{{comment.quoteComment.username}}：</el-button>{{comment.quoteComment.content}}
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
                                <img src="../image/a.jpg" alt="头像" style="width:80px;height:80px;border-radius:80px;cursor:pointer" @click="jumpToUser(this.userPage)">
                            </el-col>
                            <el-col :span="16">
                                <el-row type="flex" align="middle">
                                    <el-col :span="10" :offset="2">
                                        <span style="cursor:pointer" @click="jumpToUser(this.moment.userPage)">{{this.moment.username}}</span>
                                    </el-col>
                                    <el-col :span="4" :offset="4">
                                        <el-button size="small" plain round @click="followHandler" style="">{{this.followState}}</el-button>
                                    </el-col>
                                </el-row>
                                <el-row>
                                    <el-col :span="20" :offset="2" style="font-size:12px;color:#999">{{this.moment.sendTime}}</el-col>

                                </el-row>
                            </el-col>

                        </el-row>
                    </div>
                    <div id="content">
                        <div id="text">
                            <p style="margin:0">{{this.moment.text}}</p>
                            <el-button type="text" v-for="(tag,index) in moment.tags" :key="index" @click="jumpToTag(tag.url)">#{{tag.name}}</el-button>
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
                username:'loststars',
                isFollowed:false,
                followState:'关注',
                likeSrc:require('../image/comment-unlike.png'),
                collectSrc:require('../image/uncollect.png'),
                blogComment:{
                    comment:''
                },
                commentComment:{
                    comment:''
                },
                moment:{
                    imgList: [{
                            url: require('../image/a.jpg')
                        },
                        {
                            url: require('../image/gaojin_ciyun.png')
                        },
                        {
                            url: require('../image/gaojin_radar.png')
                        },
                        {
                            url: require('../image/user_img.jpg')
                    }],
                    username: 'Leonnnop',
                    userPage:'',
                    sendTime:'2018-07-15 00:00',
                    text: '恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...' +
                        '恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...' +
                        '恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...恭喜生活喜提我狗命blablabla...',
                    tags: [{
                            name: 'tag1',
                            url: ''
                        },
                        {
                            name: 'tag2',
                            url: ''
                        },
                        {
                            name: 'tag3',
                            url: ''
                        },
                        {
                            name: 'tag4',
                            url: ''
                    }],
                    collectNum: 10,
                    forwardNum: 0,
                    commentNum: 3,
                    likeNum: 5,
                    likeState:false,
                    collectState:false,
                },
                comments:[{
                    profilePhoto:require('../image/a.jpg'),
                    username:'user3',
                    userPage:'',
                    commentTime:'2018-07-18 12:00',
                    content:'第三条评论',
                    isCommentAComment:false,
                    quoteComment:{

                    }
                },{
                    profilePhoto:require('../image/a.jpg'),
                    username:'user2',
                    userPage:'',
                    commentTime:'2018-07-17 15:00',
                    content:'第二条评论',
                    isCommentAComment:false,
                    quoteComment:{

                    }
                },{
                    profilePhoto:require('../image/a.jpg'),
                    username:'user1',
                    userPage:'',
                    commentTime:'2018-07-17 08:00',
                    content:'第一条评论',
                    isCommentAComment:false,
                    quoteComment:{

                    }
                }]

            }
        },
        methods: {
            jumpToTag: function (url) {
                console.log(url);
                this.$router.push(url);
            },
            jumpToUser:function(url){
                console.log(url);
                this.$router.push(url)
            },
            collectHandler:function(){
                if(!this.moment.collectState){
                    this.collectSrc=require('../image/collect.png');
                    this.moment.collectNum++;
                }else{
                    this.collectSrc=require('../image/uncollect.png');
                    this.moment.collectNum--;
                }
                this.moment.collectState=!this.moment.collectState;
            },
            commentHandler:function(){
                console.log(this.currentDate);
            },
            likeHandler:function(){
                if(!this.moment.likeState){
                    this.likeSrc=require('../image/comment-like.png');
                    this.moment.likeNum++;
                }else{
                    this.likeSrc=require('../image/comment-unlike.png');
                    this.moment.likeNum--;
                }
                this.moment.likeState=!this.moment.likeState;
            },
            forwardHandler:function(){

            },
            followHandler:function(){
                if(!this.isFollowed){
                    this.followState='已关注';
                }else{
                    this.followState='关注';
                }
                this.isFollowed=!this.isFollowed;
            },
            submitBlogComment:function(){
                if(this.blogComment.comment){
                    this.comments.unshift({
                    profilePhoto:require('../image/a.jpg'),
                    username:'loststars',
                    userPage:'',
                    commentTime:'2018-07-18 23:00',
                    content:this.blogComment.comment,
                    isCommentAComment:false,
                    quoteComment:{

                    }
                });
                this.moment.commentNum++;
                this.$message('评论成功！');
                this.blogComment.comment='';

                this.$axios.post('/moment',{
                    commentNum:this.moment.commentNum,
                    comment:this.comments[0]
                })
                .then((response) => {
                    if(response.data.code){
                        this.$message('评论成功！');
                        this.blogComment.comment='';
                    }else{
                        this.$message.error('评论失败，请重试！');
                    }
                    
                })
                .catch((error) => {
                    console.log(error);
                });
                }else{
                    this.$message.error('请输入评论！');
                    return false;
                }
                
                
            },
            submitCommentComment:function(comment){
                if(this.commentComment.comment){
                    this.comments.unshift({
                    profilePhoto:require('../image/a.jpg'),
                    username:'loststars',
                    userPage:'',
                    commentTime:'2018-07-18 20:00',
                    content:this.commentComment.comment,
                    isCommentAComment:false,
                    quoteComment:{
                        username:comment.username,
                        userPage:comment.userPage,
                        content:comment.content
                    }
                });
                this.commentNum++;
                this.$message('评论成功！');
                this.commentComment.comment='';
                this.commentAComment(comment);

                this.$axios.post('/moment',{
                    commentNum:this.moment.commentNum,
                    comment:this.comments[0]
                })
                .then((response) => {
                    if(response.data.code){
                        this.$message('评论成功！');
                        this.commentComment.comment='';
                    }else{
                        this.$message.error('评论失败，请重试！');
                    }
                })
                .catch((error) => {
                    console.log(error);
                });
                }else{
                    this.$message.error('请输入评论！');
                    return false;
                }
                
            },
            commentAComment:function(comment){
                console.log(comment.isCommentAComment);
                comment.isCommentAComment=!comment.isCommentAComment;
            }
        },
        created() {
            this.$axios.get('/moment')
            .then((response) => {
                this.moment=response.data.moment;
                this.comments=response.data.comments;
                this.isFollowed=response.data.isFollowed;
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
        width: 106%;
        overflow-y: scroll;
        overflow-x: hidden;
    }
    .time {
    font-size: 13px;
    color: #999;
  }
  .op-img{
      height:25px;
      width:25px;
      margin-right:5%;
  }
  .show-comment{
      border-top:1px solid rgb(235,238,245);
      padding-top:20px;
      padding-bottom:10px;
  }
  .show-comment-img{
      width:50px;
      height:50px;
      border-radius:80px;
  }
</style>