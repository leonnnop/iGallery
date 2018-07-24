<template>
    <div style="margin-bottom:30px;">
        <transition name="slide-fade">
            <div v-if="loadingPage" class="loadingbackground" style="margin-top:-30px"></div>
        </transition>
        <el-row>
            <el-col :span="10" :offset="4">
                <el-carousel v-if="hackReset" height="500px" :interval="0" indicator-position="outside">
                    <el-carousel-item v-for="(img,index) in imgList" :key="index">
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
                                                        <img :src="likeUser.headImg" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(likeUser.userPage)">
                                                    </el-col>
                                                    <el-col :span="16">
                                                        <el-row>{{likeUser.Username}}</el-row>
                                                        <el-row style="font-size:12px;margin-top:5px;">{{likeUser.Bio}}</el-row>
                                                    </el-col>
                                                    <el-col :span="3">
                                                        <el-button plain size="small" @click="likeFollowHandler(likeUser,likeUser.FollowState)" :class="{followed:likeUser.FollowState}"
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
                                    <img src="../image/forward.png" alt="forward" class="op-img hover-cursor" @click="forwardHandler()">{{moment.ForwardNum}}
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
                                <img :src="'http://10.0.1.8:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID +
                        '&type=2'" alt="" style="width:40px;height:40px;border-radius:40px;">
                            </el-col>
                            <el-col :span="21" :offset="1">
                                <el-form ref="blogComment" :model="blogComment" :inline="true">
                                    <!-- <el-row> -->
                                    <el-form-item style="width:80%;">
                                        <el-input type="textarea" v-model="blogComment.comment" placeholder="评论（不超过120个字符）" size="medium" resize="none" autosize
                                            maxlength="120" style="width:370px;"></el-input>
                                    </el-form-item>
                                    <!-- </el-row> -->
                                    <!-- <el-row type="flex" justify="end" style="margin-top:-59px"> -->
                                    <!-- <el-row type="flex" justify="end"> -->
                                    <el-form-item>
                                        <el-button type="primary" @click="submitBlogComment" plain size="small">评论</el-button>
                                    </el-form-item>
                                    <!-- </el-row> -->
                                </el-form>
                            </el-col>
                        </el-row>
                        <!-- 评论展示区 -->
                        <div v-for="(comment,index) in comments" :key="index" class="show-comment">
                            <el-row type="flex" align="middle">
                                <el-col :span="3">
                                    <img :src="comment.headImg" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(comment.userPage)">
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
                                <el-col :span="20">
                                    <time class="time">{{ comment.send_time }}</time>
                                </el-col>
                                <el-col :span="1" v-show="moment.SenderID==$store.state.currentUserId_ID">
                                    <el-button type="text" @click="deleteAComment(comment)">删除</el-button>
                                </el-col>
                                <el-col :span="1">
                                    <el-button type="text" @click="commentAComment(comment)">回复</el-button>
                                </el-col>
                            </el-row>
                            <!-- 点击回复显示评论框 -->
                            <el-row type="flex" align="middle" v-if="comment.isCommentAComment">
                                <el-col>
                                    <el-form ref="commentComment" :model="commentComment">
                                        <el-row type="flex" align="middle" justify="center">
                                            <el-col :span="21">
                                                <el-form-item>
                                                    <el-row>
                                                        <el-input type="textarea" v-model="commentComment.comment" placeholder="评论（不超过120个字符）" resize="none" autosize maxlength="120"></el-input>
                                                        <!-- <el-button type="primary" @click="submitCommentComment(comment)" plain size="small" style="margin-top:8px">评论</el-button> -->
                                                    </el-row>
                                                </el-form-item>
                                            </el-col>
                                            <el-col :span="2" :offset="1">
                                                <el-form-item>
                                                    <el-button type="primary" @click="submitCommentComment(comment)" plain size="small" style="margin-top:8px">评论</el-button>
                                                </el-form-item>
                                            </el-col>
                                        </el-row>
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
                                <img :src="userHeadImg" alt="头像" style="width:80px;height:80px;border-radius:80px;" class="hover-cursor" @click="jumpToUser(moment.userPage)">
                            </el-col>
                            <el-col :span="16">
                                <el-row type="flex" align="middle">
                                    <el-col :span="10" :offset="2">
                                        <span class="hover-cursor" @click="jumpToUser(moment.userPage)">{{moment.Username}}</span>
                                    </el-col>
                                    <el-col :span="4" :offset="4">
                                        <el-button v-if="moment.SenderID!=$store.state.currentUserId_ID" plain size="small" @click="followHandler(moment,moment.FollowState)"
                                            :class="{followed:moment.FollowState}">{{moment.followState}}</el-button>
                                        <!-- <i v-if="moment.SenderID==$store.state.currentUserId_ID" class="el-icon-edit"></i> -->
                                        <el-row type="flex" align="middle">
                                            <el-button v-if="moment.SenderID==$store.state.currentUserId_ID" icon="el-icon-edit" circle style="margin-left:0px" @click="modifyClickHandler"></el-button>
                                            <el-button v-if="moment.SenderID==$store.state.currentUserId_ID" icon="el-icon-delete" circle style="margin-left:10px" @click="deleteClickHandler"></el-button>
                                        </el-row>
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

        <el-row type="flex" justify="center" style="margin-top:30px">
            <el-col style="width:100%;height:800px;" :class="navBarFixed == true ? 'mainContentScroll' :''">

                <router-view></router-view>
                <el-dialog title="" :visible.sync="sendMomentVisible" width="50%" custom-class="send" :show-close="false" top="10px">
                    <el-row>
                        <el-col :span="3" :offset="0">
                            <img :src="'http://10.0.1.8:54468/api/Picture/FirstGet?id=' +this.$store.state.currentUserId_ID +'&type=2'" alt="headImg"
                                style="width:80px;height:80px;border-radius:80px;">
                        </el-col>
                        <el-col :span="18" :offset="0">
                            <div class="sendContent">
                                <div class="edit">
                                    <div style="color:#555;margin:50px 0 20px 80px;font-size:16px;font-weight:bold">Leonnnop</div>
                                    <el-row type="flex" justify="center" align="middle">
                                        <el-row>

                                        </el-row>
                                        <el-col :span="6" v-show="showUploadArea"></el-col>
                                        <el-col :span="18" v-show="showUploadArea" v-if="showUpload">

                                            <!-- <el-upload ref="upload" action="https://jsonplaceholder.typicode.com/posts/" list-type="picture-card" :on-remove="handleRemove" -->
                                            <el-upload ref="upload" action="http://10.0.1.8:54468/api/Picture" list-type="picture-card" :on-remove="handleRemove" :file-list="uploadImgs"
                                                :auto-upload="false" :before-upload="beforeUpload" :on-change="uploadOnChange"
                                                :on-success="uploadOnSuccess" :on-error="uploadOnError" :on-progress="uploadOnProgress"
                                                :on-exceed="upLoadOnExceed" :show-file-list="true" :limit="9" :multiple="true"
                                                class="upload" :data="pictureObj">
                                                <i class="el-icon-plus"></i>
                                            </el-upload>


                                        </el-col>
                                        <el-col v-show="showTextArea" :span="16" :offset="2" style="margin-top:0;">
                                            <el-input type="textarea" resize="none" :rows="12" placeholder="此刻的想法..." v-model="moment.Content"></el-input>
                                            <div class="editTag">
                                                <el-tag :key="tag" color="#fff" v-for="tag in moment.tags" closable :disable-transitions="false" @close="handleTagClose(tag)">
                                                    {{tag}}
                                                </el-tag>

                                                <el-input class="input-new-tag" v-if="tagsInputVisible&&ableToAddTag" v-model="tagsInputValue" ref="saveTagInput" size="small"
                                                    @keyup.enter.native="handleTagInputConfirm" @blur="handleTagInputConfirm">
                                                </el-input>
                                                <el-button v-if="!tagsInputVisible&&ableToAddTag" class="button-new-tag" size="small" @click="showTagInput">+ tag</el-button>
                                            </div>
                                        </el-col>
                                    </el-row>

                                </div>
                                <el-row type="flex" justify="space-between" align="middle" style="margin-top:10px" v-if="showUploadArea&&!showTextArea">
                                    <el-col :span="12" :offset="4" v-if="!showTextArea">已选择{{sendMomentImgNum}}张图片，最多可选择9张图片</el-col>
                                    <el-col :span="4">
                                        <img src="../image/arrow-right.png" alt="" @click="sendNextHandler" v-if="showNextBtn" class="sendMomentBtn">
                                    </el-col>
                                </el-row>
                                <el-row type="flex" justify="end" style="margin-top:10px" v-if="!showUploadArea&&showTextArea">
                                    <el-col :span="4">
                                        <img src="../image/close-circle.png" @click="sendLastHandler" class="sendMomentBtn" style="width:50px;height:50px;margin-top:-5px">
                                    </el-col>
                                    <el-col :span="4">
                                        <img src="../image/send-moment.png" @click="sendMomentHandler" class="sendMomentBtn">
                                    </el-col>
                                </el-row>
                            </div>

                        </el-col>
                    </el-row>

                </el-dialog>
            </el-col>
        </el-row>

    </div>
</template>

<script>
    import Vue from 'vue'
    export default {
        name: 'MomentDetail',
        data() {
            return {
                // sendText:'',
                displayDelete: false,
                loadingPage: true,

                sendMomentImgNum: 0,
                userHeadImg: '',
                hackReset: false,
                pictureURL: '',
                navBarFixed: false,
                searchInput: '',
                topBarActiveIndex: '1',
                sendMomentVisible: false,
                dialogImageUrl: '',
                dialogVisible: false,
                showNextBtn: false,
                showUpload: false,
                showUploadArea: true,
                showTextArea: false,
                sendText: '',
                uploadImgs2: [],
                uploadImgs: [
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                    // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
                ],
                ableToUpload: true,
                tags: [],
                tagsInputVisible: false,
                tagsInputValue: '',
                ableToAddTag: true,
                likeListVisible: false,
                pictureObj: {
                    id: 2,
                    type: 2
                },
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
                Username: this.$store.state.currentUsername,
                Bio: '万年单身汪',
                likeSrc: require('../image/comment-unlike.png'),
                collectSrc: require('../image/uncollect.png'),
                blogComment: {
                    comment: ''
                },
                commentComment: {
                    comment: ''
                },
                imgList: [{
                        url: "http://10.0.1.8:54468/api/Picture/Gets?pid=21341"
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
                moment: {

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
                        'tag1', 'tag2', 'tag3'
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
                    send_time: '2018-07-18 12:00',
                    content: '第三条评论',
                    isCommentAComment: false,
                    quoteComment: {

                    }
                }, {
                    headImg: require('../image/a.jpg'),
                    ID: '333',
                    Username: 'user2',
                    userPage: '',
                    send_time: '2018-07-17 15:00',
                    content: '第二条评论',
                    isCommentAComment: false,
                    quoteComment: {

                    }
                }, {
                    headImg: require('../image/a.jpg'),
                    ID: '444',
                    Username: 'user1',
                    userPage: '',
                    send_time: '2018-07-17 08:00',
                    content: '第一条评论',
                    isCommentAComment: false,
                    quoteComment: {

                    }
                }],

            }

        },
        methods: {
            deleteClickHandler() {
                // this.$router.push('/main/personalpage');

                this.axios.put('http://10.0.1.8:54468/api/ModifyMoment/DeleteMoment?email=' + this.$store.state.currentUserId +
                        '&moment_id=' + this.$route.params.id)
                    .then((response) => {
                        if (response.data == 0) {
                            this.$message({
                                message: '删除成功！',
                                type: 'success'
                            });
                            this.$router.push('/main/personalpage');

                        } else {
                            this.$message.error('删除失败，服务器内部错误，请重试。');
                        }
                    })
            },
            refresh() {
                history.go(0)
                this.$store.commit('addCurrentUsername', this.getCookie(Username));
                this.$store.commit('addCurrentUserPassword', this.getCookie(Password));
                this.$store.commit('addCurrentUserBio', this.getCookie(Bio));
                this.$store.commit('addCurrentUserPhoto', this.getCookie(Photo));
            },
            getCookie(name) {
                var strCookie = document.cookie;
                var arrCookie = strCookie.split("; ");
                for (var i = 0; i < arrCookie.length; i++) {
                    var arr = arrCookie[i].split("=");
                    if (arr[0] == name)
                        return arr[1];
                }
                return "";
            },
            deleteAComment(comment) {
                //////////////api/Coment/DelCmt
                var formdata = new FormData();
                formdata.append('Cid', comment.ID);
                console.log(comment.ID)
                // formdata.append('Sender_id', this.$store.state.currentUserId_ID);
                // formdata.append('Content', moment.newComment);
                // formdata.append('Send_time', '');
                // formdata.append('Quote_comment_id', '');
                let config = {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                };
                this.axios.post('http://10.0.1.8:54468/api/Coment/DelCmt', formdata, config)
                    .then((response) => {
                        // if (response.data == 'OK') {
                        if (true) {
                            this.$message({
                                message: '评论删除成功！',
                                type: 'success'
                            });
                            // this.$router.push('/main/momentDetail/' + this.$route.params.id);
                            setTimeout(this.refresh(), 3000)

                        }
                        // location.reload();
                        //////////////
                    })
                    .catch((error) => {
                        console.log(error);
                        this.$message.error('评论删除失败，请稍后重试！');
                    })
            },
            sendMomentInit: function () {
                this.sendMomentVisible = true;
                this.showUpload = true;
            },
            sendNextHandler: function () {
                this.showUploadArea = false;
                this.showTextArea = true;
            },
            sendLastHandler: function () {
                // this.showUploadArea = true;
                // this.showTextArea = false;
                this.uploadImgs2 = [];
                this.tags = [];
                this.sendText = '';
                this.sendMomentVisible = false;
                this.showUploadArea = true;
                this.showNextBtn = false;
                this.showTextArea = false;
                this.showUpload = false;
            },
            sendMomentHandler: function () {
                console.log('————发布内容————');
                // this.pictureURL = 'http://10.0.1.8:54468/api/Picture?id=2&type=2';
                this.$refs.upload.submit(); //上传图片

                this.axios.put('http://10.0.1.8:54468/api/ModifyMoment/ModifyMoment', {
                        email: this.$store.state.currentUserId,
                        moment_id: this.$route.params.id,
                        content: this.moment.Content
                    })
                    .then((response) => {
                        if (response.data == 0) {
                            this.$message({
                                message: '修改成功！',
                                type: 'success'
                            });
                        } else {
                            this.$message.error('修改失败，服务器内部错误，请重试。');
                        }

                        this.uploadImgs2 = [];
                        this.tags = [];
                        this.sendText = '';
                        this.sendMomentVisible = false;
                        this.showUploadArea = true;
                        this.showNextBtn = false;
                        this.showTextArea = false;
                        this.showUpload = false;
                    })
                    .catch((error) => {
                        this.uploadImgs2 = [];
                        this.tags = [];
                        this.sendText = '';
                        this.sendMomentVisible = false;
                        this.showUploadArea = true;
                        this.showNextBtn = false;
                        this.showTextArea = false;
                        this.showUpload = false;
                    })

                if (this.moment.tags.length > 0) {
                    // this.axios.get('http://10.0.1.8:54468/api/Tag/AddTag?Moment_Id='+this.currentMomentID+'&', {
                    this.axios.get('http://10.0.1.8:54468/api/Tag/AddTag?Moment_Id=' + this.$route.params.id + '&', {
                        params: {
                            TagNames: this.moment.tags,
                            // Moment_Id: this.$route.params.id
                        },
                        paramsSerializer: function (params) {
                            var Qs = require('qs');
                            return Qs.stringify(params, {
                                arrayFormat: 'repeat'
                            })
                        }
                    })
                }

            },
            //上传组件
            handleRemove(file, fileList) {
                if (!fileList.length) {
                    this.showNextBtn = false;
                }
            },
            beforeUpload: function (file) {
                const size = file.size / 1024 / 1024 < 3;
                if (!size) {
                    this.$message.error('上传图片大小不能超过 3MB!');
                }
                return size;
            },
            uploadOnProgress(e, file) { //开始上传
                // console.log('——————开始上传——————');
                // console.log(file)
                // var file = document.getElementById("upload_file").files[0];
                var oneFile = file.raw;
                var formdata1 = new FormData(); // 创建form对象
                formdata1.append('file', oneFile); // 通
                // formdata1.append('id', 2); // 通
                // formdata1.append('type', 2); // 通
                let config = {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                }; //添加请求头
                this.axios.post('http://10.0.1.8:54468/api/Picture/Save?id=2&type=1', formdata1, config).then((response) => { //这里的/xapi/upimage为接口
                    console.log(response.data);
                })

            },
            uploadOnChange(file, fileList) {
                //console.log("——————————change——————————")
                // console.log(file)
                if (file.status == 'ready') {
                    this.uploadImgs2.push(file);
                    //console.log("ready")
                } else if (file.status == 'fail') {
                    this.$message.error("图片上传出错，请刷新重试！")
                }
                if (fileList.length) {
                    this.showNextBtn = true;
                }
            },
            uploadOnSuccess(e, file, fileList) { //上传附件
                // console.log("——————————success——————————")
                // console.log(fileList);
            },
            upLoadOnExceed: function (files, fileList) {
                this.$message.error('exceed');
                this.$message.warning(
                    `最多可选 9 张图片，本次选择了 ${files.length} 张图片，共选择了 ${files.length + fileList.length} 张图片`);
            },
            uploadOnError(e, file) {
                // console.log("——————————error——————————");
                // console.log(e);
            },

            handleTagClose(tag) {
                this.moment.tags.splice(this.moment.tags.indexOf(tag), 1);
                // console.log(this.moment.tags)
                if (this.moment.tags.length <= 4) {
                    this.ableToAddTag = true;
                }
            },

            showTagInput() {
                this.tagsInputVisible = true;
                this.$nextTick(_ => {
                    this.$refs.saveTagInput.$refs.input.focus();
                });
            },

            handleTagInputConfirm() {
                let tagsInputValue = this.tagsInputValue;
                if (tagsInputValue) {
                    this.moment.tags.push(tagsInputValue);
                }
                if (this.moment.tags.length >= 4) {
                    this.ableToAddTag = false;
                }
                this.tagsInputVisible = false;
                this.tagsInputValue = '';
            },
            modifyClickHandler() {
                this.sendMomentVisible = true;
                this.showUpload = true;
                // this.showTextArea = true;
                this.sendNextHandler();
            },
            likeListHandler() {
                this.likeListVisible = true
                this.axios.get('http://10.0.1.8:54468/api/DisplayLikeList/GetLikeList?&moment_id=' + this.$route.params
                        .id + '&email=' + this.$store.state.currentUserId)
                    .then(((response) => {
                        this.likeUsers = response.data;
                        this.likeUsers.forEach(element => {
                            // element.headImg = require('../image/a.jpg');
                            var headImg = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' +
                                element.ID +
                                '&type=2';
                            Vue.set(element, 'headImg', headImg)
                            console.log(element)
                            // 等待修改
                            // element.FollowState = true;
                            if (element.FollowState == 'true') {
                                element.FollowState = true
                                element.followState = '已关注'
                            } else {
                                element.FollowState = false
                                element.followState = '关注'
                            }
                        });
                    }));
            },
            jumpToTag: function (tag) {
                this.$router.push('/main/tag/' + tag);
            },
            jumpToUser: function (url) {
                console.log(url);
                this.$router.push(url)
            },
            collectHandler: function () {
                if (!this.moment.collectState) {
                    this.axios.get('http://10.0.1.8:54468/api/Collect/InsertCollect?moment_id=' + this.moment.ID +
                            '&founder_id=' + this.$store.state.currentUserId_ID +
                            '&name=' + '默认收藏夹'
                        )
                        .then((response) => {
                            if (response.data == 0) {
                                this.collectSrc = require('../image/collect.png');
                                this.moment.CollectNum++;
                                this.moment.collectState = !this.moment.collectState;

                            } else {
                                this.$message.error('收藏失败，请重试！');
                            }
                        })
                        .catch((error) => {
                            console.log(error);
                        });
                    // this.collectSrc = require('../image/collect.png');
                    // this.moment.CollectNum++;
                } else {
                    this.axios.get('http://10.0.1.8:54468/api/Collect/DeleteCollect?moment_id=' + this.moment.ID +
                            '&user_id=' + this.$store.state.currentUserId_ID
                        )
                        .then((response) => {
                            if (response.data == 0) {
                                this.collectSrc = require('../image/uncollect.png');
                                this.moment.CollectNum--;
                                this.moment.collectState = !this.moment.collectState;

                            } else {
                                this.$message.error('取消收藏失败，请重试！');
                            }
                        })
                        .catch((error) => {
                            console.log(error);
                        });
                    // this.collectSrc = require('../image/uncollect.png');
                    // this.moment.CollectNum--;
                }

            },
            likeHandler: function () {
                // console.log(item)
                this.axios.put('http://10.0.1.8:54468/api/DiscoverMoment/UpdateLiking?email=' + this.$store.state.currentUserId +
                    '&moment_id=' + this.moment.ID
                    // , {
                    //     email: this.$store.state.currentUserId,
                    //     moment_id: item.MomentId
                    //   }
                )

                console.log(this.moment.LikeState)
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
                this.$confirm('', '确定转发？', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        center: true
                    }).then(() => {
                        this.axios.post('http://10.0.1.8:54468/api/Moment/ForwardMoment', {
                                User_ID: this.$store.state.currentUserId_ID,
                                Moment_ID: this.moment.ID
                            })
                            .then((response) => {
                                if (response.data == 0) {
                                    this.$message({
                                        message: '转发成功！',
                                        type: 'success'
                                    });
                                } else if (response.data == 1) {
                                    this.$message({
                                        message: '不可以转发自己的动态哦！',
                                        type: 'warning'
                                    });
                                } else {
                                    this.$message.error('转发失败，请稍后重试！')
                                }
                            })
                            .catch((error) => {
                                console.log(error);
                            });
                    })
                    .catch(() => {});
            },
            likeFollowHandler: function (user, FollowState) {
                console.log(user)
                console.log(this.$store.state.currentUserId_ID)
                console.log(user.FollowState);
                //////////////
                if (!user.FollowState) {
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
            followHandler: function (user, FollowState) {
                console.log(user)
                console.log(this.$store.state.currentUserId_ID)
                console.log(user.FollowState);
                //////////////
                if (!user.FollowState) {
                    user.followState = '已关注';
                } else {
                    user.followState = '关注';
                }
                user.FollowState = !user.FollowState;
                console.log(user.FollowState);

                this.axios.get('http://10.0.1.8:54468/api/Users/Follow?followID=' + this.$store.state.currentUserId_ID +
                        '&followedID=' + user.SenderID)
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
            getNowFormatDate() {
                var date = new Date();
                var strMonth = date.getMonth() + 1;
                var strDate = date.getDate();
                var strHour = date.getHours();
                var strMin = date.getMinutes();
                var strSec = date.getSeconds();
                if (strMonth >= 1 && strMonth <= 9) {
                    strMonth = "0" + strMonth;
                }
                if (strDate >= 0 && strDate <= 9) {
                    strDate = "0" + strDate;
                }
                if (strHour >= 0 && strHour <= 9) {
                    strHour = "0" + strHour;
                }
                if (strMin >= 0 && strMin <= 9) {
                    strMin = "0" + strMin;
                }
                if (strSec >= 0 && strSec <= 9) {
                    strSec = "0" + strSec;
                }
                var currentdate = date.getFullYear() + '-' +
                    strMonth + '-' +
                    strDate + ' ' +
                    strHour + ':' +
                    strMin + ':' +
                    strSec;
                return currentdate;
            },
            submitBlogComment: function () {
                if (this.blogComment.comment) {
                    this.comments.unshift({
                        headImg: 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID +
                            '&type=2',
                        Username: this.$store.state.currentUsername,
                        userPage: '',
                        send_time: this.getNowFormatDate(),
                        content: this.blogComment.comment,
                        isCommentAComment: false,
                        quoteComment: {

                        }
                    });
                    this.moment.CommentNum++;
                    // this.$message('评论成功！');
                    // this.blogComment.comment = '';

                    //////////////
                    // this.axios.post('/moment', {
                    //         CommentNum: this.moment.CommentNum,
                    //         comment: this.comments[0]
                    //     })
                    //     .then((response) => {
                    //         if (response.data.code) {
                    //             this.$message('评论成功！');
                    //             this.blogComment.comment = '';
                    //         } else {
                    //             this.$message.error('评论失败，请重试！');
                    //         }

                    //     })
                    //     .catch((error) => {
                    //         console.log(error);
                    //     });

                    var formdata = new FormData();
                    formdata.append('Mid', this.$route.params.id);
                    formdata.append('Sender_id', this.$store.state.currentUserId_ID);
                    // console.log(this.commentComment.comment)
                    formdata.append('Content', this.blogComment.comment);
                    formdata.append('Send_time', '');
                    formdata.append('Quote_id', '');
                    let config = {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        }
                    };
                    this.axios.post('http://10.0.1.8:54468/api/Coment/SvCmt', formdata, config)
                        .then((response) => {
                            // if (response.data == 'OK') {
                            if (true) {
                                this.$message({
                                    message: '评论成功！',
                                    type: 'success'
                                });
                                this.blogComment.comment = '';
                            } else {
                                this.$message.error('评论失败，请稍后重试！');
                            }
                        })
                        .catch((error) => {
                            console.log(error);
                            this.$message.error('评论失败，请稍后重试！');
                        })
                } else {
                    this.$message.error('请输入评论！');
                    return false;
                }


            },
            submitCommentComment: function (comment) {
                if (this.commentComment.comment) {
                    this.comments.unshift({
                        headImg: 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID +
                            '&type=2',
                        Username: this.$store.state.currentUsername,
                        userPage: '',
                        send_time: this.getNowFormatDate(),
                        content: this.commentComment.comment,
                        isCommentAComment: false,
                        quoteComment: {
                            Username: comment.Username,
                            userPage: comment.userPage,
                            content: comment.content
                        }
                    });
                    this.moment.CommentNum++;
                    // this.$message('评论成功！');
                    // this.commentComment.comment = '';
                    this.commentAComment(comment);
                    //////////////
                    // this.$axios.post('/moment', {
                    //         CommentNum: this.moment.CommentNum,
                    //         comment: this.comments[0]
                    //     })
                    //     .then((response) => {
                    //         if (response.data.code) {
                    //             this.$message('评论成功！');
                    //             this.commentComment.comment = '';
                    //         } else {
                    //             this.$message.error('评论失败，请重试！');
                    //         }
                    //     })
                    //     .catch((error) => {
                    //         console.log(error);
                    //     });
                    var formdata = new FormData();
                    formdata.append('Mid', this.$route.params.id);
                    formdata.append('Sender_id', this.$store.state.currentUserId_ID);
                    formdata.append('Content', this.commentComment.comment);
                    formdata.append('Send_time', '');
                    console.log(comment.ID)
                    formdata.append('Quote_id', comment.ID);
                    let config = {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        }
                    };
                    this.axios.post('http://10.0.1.8:54468/api/Coment/SvCmt', formdata, config)
                        .then((response) => {
                            // if (response.data == 'OK') {
                            if (true) {
                                this.$message({
                                    message: '评论成功！',
                                    type: 'success'
                                });
                                this.commentComment.comment = '';
                            } else {
                                this.$message.error('评论失败，请稍后重试！');
                            }
                        })
                        .catch((error) => {
                            console.log(error);
                            this.$message.error('评论失败，请稍后重试！');
                        })
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
            this.hackReset = false
            this.$nextTick(() => {
                this.hackReset = true
            })
            this.axios.get('http://10.0.1.8:54468/api/Picture/FirstGet?id=' + this.$route.params.id +
                    '&type=1')
                .then((response) => {
                    this.imgList = [];
                    console.log('ge')
                    response.data.forEach(element => {
                        console.log('gege')
                        this.imgList.push({
                            url: 'http://10.0.1.8:54468/api/Picture/Gets?pid=' +
                                element
                        })
                        console.log(this.imgList)
                    });
                })
            //
            this.axios.get('http://10.0.1.8:54468/api/DisplayMoments/Detail?UserID=' + this.$store.state.currentUserId_ID +
                    '&MomentID=' + this.$route.params.id)
                .then((response) => {

                    this.moment = response.data.moment;
                    this.moment.Username = response.data.user_username;
                    // this.moment.displayDelete = 

                    this.userHeadImg = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' + this.moment.SenderID +
                        '&type=2';
                    // this.imgList = [{
                    //     url: "http://10.0.1.8:54468/api/Picture/Gets?pid=21341"
                    // }];

                    if (response.data.FollowState == 0) {
                        this.moment.FollowState = true
                        var string = '已关注'
                        Vue.set(this.moment, 'followState', string)
                        // this.moment.followState = '已关注'
                    } else {
                        this.moment.FollowState = false
                        // this.moment.followState = '关注'
                        var astring = '关注'

                        Vue.set(this.moment, 'followState', astring)
                    }

                    if (response.data.liked == 0) {
                        this.moment.LikeState = true
                        this.likeSrc = require('../image/comment-like.png');
                    } else {
                        this.moment.LikeState = false
                    }

                    if (response.data.collected == 0) {
                        this.moment.collectState = true
                        this.collectSrc = require('../image/collect.png');
                    } else {
                        this.moment.collectState = false
                    }

                    // this.comments = response.data.comments;
                    this.FollowState = response.data.FollowState;
                    // this.moment = response.data;
                    /////
                    Vue.set(this.moment, 'tags', response.data.tags)
                    // this.moment.tags = response.data.tags;
                    // this.moment.tags = [];
                    // response.data.tags.forEach(element => {
                    //     this.moment.tags.push({
                    //         name: element
                    //     })
                    // });

                })
                .catch((error) => {
                    console.log(error);
                });

            this.axios.get('http://10.0.1.8:54468/api/Coment/LdCmt?Mid=' + this.$route.params.id)
                .then((response) => {
                    this.comments = [];
                    response.data.forEach(element => {
                        var comment = {};
                        var headImg = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' + element.Sender_id +
                            '&type=2';

                        Vue.set(comment, 'headImg', headImg)
                        comment.ID = element.Cid;
                        comment.Username = element.Sender_username;
                        comment.send_time = element.Send_time;
                        comment.content = element.Content;
                        comment.isCommentAComment = false;
                        if (element.Quote_username != null) {
                            comment.quoteComment = {
                                Username: element.Quote_username,
                                content: element.Quote_content,
                            }
                        } else {
                            comment.quoteComment = {}
                        }

                        this.comments.push(comment);
                    });
                })


            // this.axios
        },
        mounted: function () {
            this.$nextTick(function () {
                // Code that will run only after the
                // entire view has been rendered
                // console.log('mouted')
                // setTimeout("backgroundHandler()", 1000);
                var self = this;
                // this.toastrVal = inVal;
                // this.loadState = true;
                // this.noBg = bgState;
                setTimeout(function () {
                    self.loadingPage = false;
                }, 1000)
            })
        },
        beforeRouteEnter(from, to, next) {
            next(vm => {
                vm.$nextTick(function () {
                    var self = vm;
                    // this.toastrVal = inVal;
                    // this.loadState = true;
                    // this.noBg = bgState;
                    setTimeout(function () {
                        self.loadingPage = false;
                    }, 1000)

                    // Code that will run only after the
                    // entire view has been rendered
                    // console.log('mouted')
                    // window.setTimeout("this.backgroundHandler()", 1000);
                })
            })
        }
    }
</script>

<style scoped>
    /* 可以设置不同的进入和离开动画 */

    /* 设置持续时间和动画函数 */

    .slide-fade-enter-active {
        transition: all .3s ease;
    }

    .slide-fade-leave-active {
        transition: all .8s cubic-bezier(1.0, 0.5, 0.8, 1.0);
    }

    .slide-fade-enter,
    .slide-fade-leave-to
    /* .slide-fade-leave-active for below version 2.1.8 */

        {
        transform: translateX(10px);
        opacity: 0;
    }

    .loadingbackground {
        position: fixed;
        z-index: 1000;
        height: 100%;
        width: 100%;
        background-image: url(../image/loading3.gif);
        background-repeat: no-repeat;
        background-position: center;
        background-color: white;
        /* background-size: cover */
    }

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