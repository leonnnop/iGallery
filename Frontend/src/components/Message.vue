<template>
    <el-container style="height:0">
        <el-row type="flex" justify="center" style="width:60%;min-height:600px;margin:0 auto;">
            <!-- 侧边栏 -->
            <el-col :span="4" style="height:inherit;background:#fff">
                <el-col>
                    <el-menu default-active="message" class="el-menu-vertical-demo" @select="menuSelectHandler" text-color="#777" active-text-color="#6191d5">
                        <el-menu-item index="message" style="text-align:center">
                            <img :src="messageIcon" alt="" class="icon">
                            <span slot="title">私信</span>
                        </el-menu-item>
                        <el-menu-item index="follow" style="text-align:center">
                            <img :src="followIcon" alt="" class="icon">
                            <span slot="title">关注</span>
                        </el-menu-item>
                        <el-menu-item index="comment" style="text-align:center">
                            <img :src="commentIcon" alt="" class="icon">
                            <span slot="title">评论</span>
                        </el-menu-item>
                        <el-menu-item index="like" style="text-align:center">
                            <img :src="likeIcon" alt="" class="icon">
                            <span slot="title">点赞</span>
                        </el-menu-item>
                        <el-menu-item index="forward" style="text-align:center">
                            <img :src="forwardIcon" alt="" class="icon">
                            <span slot="title">转发</span>
                        </el-menu-item>
                    </el-menu>
                </el-col>
            </el-col>
            <!-- 右边内容 -->
            <el-col :span="20" class="right" style="overflow-y:auto">
                <!-- 私信 -->
                <el-row v-show="isMessage">
                    <!-- 用户列表 -->
                    <el-col :span="6" class="user-list">
                        <el-row v-if="messageUserList.length" v-for="(user,index) in messageUserList" :key="index" class="user-li hover-cursor" :class="{selected:user.selected}"
                            @click.native="selectUser(user)">
                            <el-col :span="6">
                                <img :src="user.Photo+'&Rand=' + Math.random()" alt="头像" class="header-img">
                            </el-col>
                            <el-col :span="15" style="margin-left:10px">
                                <el-row style="color:#555">{{user.Username}}</el-row>
                                <el-row style="font-size:13px;color:#777;margin-top:5px" class="message-pre">{{user.message}}</el-row>
                            </el-col>
                        </el-row>
                        <div v-if="!messageUserList.length" style="color:#777;font-size:13px;line-height:100px;text-align:center;">快去找人聊聊天吧~</div>
                    </el-col>

                    <!-- 聊天框 -->
                    <el-col :span="18" style="position:relative">
                        <div style="position:absolute;background:#fff;height:590px;width:100%;z-index:10" v-if="!messageUserList.length"></div>
                        <div style="padding:30px 30px 0 30px">
                            <div style="height:30px;border-bottom:1px solid #6191d5;color:#555">{{currentTalker.Username}}</div>
                            <div class="dialog" id="dialog">
                                <div class="dialog-inner" id="dialog-inner">
                                    <div v-if="messages.length" class="content" v-for="(message,index) in messages" :key="index">
                                        <div v-if="message.identity==0">
                                            <img :src="photo+'&Rand=' + Math.random()" alt="" @click="jumpToUser($store.state.currentUserId_ID)" class="float-right header-img">
                                            <div class="message-limit float-right">

                                                <div class="message-box-right">
                                                    <span>{{message.content}}</span>
                                                </div>
                                                <i class="bubble-right"></i>
                                            </div>
                                        </div>
                                        <div v-else>
                                            <img :src="currentTalker.Photo+'&Rand=' + Math.random()" alt="" @click="jumpToUser(currentTalker.ID)" class="float-left header-img">
                                            <div class="message-limit float-left">
                                                <i class="bubble-left"></i>
                                                <div class="message-box-left">
                                                    <span>{{message.content}}</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="clear:both"></div>
                                    </div>
                                    <div v-if="!messages.length" style="color: #999;font-size: 13px;text-align: center;line-height: 50px;letter-spacing: 1px;">打个招呼吧~</div>
                                </div>
                                <div id="msg_end" style="height:0px; overflow:hidden"></div>
                            </div>
                        </div>
                        <div style="padding:0;background:#fcfcfc;height:80px">
                            <textarea class="message-input" v-model="messageInput" autofocus @keydown.ctrl.enter="sendMessage()"></textarea>
                        </div>
                    </el-col>
                </el-row>

                <!-- 关注 -->
                <el-row v-if="isFollow" style="padding:30px;">
                    <div style="height:30px;border-bottom:1px solid #6191d5;color:#555">关注</div>
                    <el-row v-if="messageFollow.length" v-for="(user,index) in messageFollow" :key="index" class="like-list" @click.native="jumpToUser(user.ID)">
                        <el-col :span="2" :offset="1">
                            <div style="width:45px;height:45px;border-radius:45px;background-position:center;background-size:cover;" :style="{backgroundImage:'url('+(user.Photo)+'&Rand=' + Math.random() + ')'}">
                            </div>
                        </el-col>
                        <el-col :span="15">
                            <el-row style="font-weight:600;line-height:25px;letter-spacing:1px;color:#333;" v-if="user.Bio!=''">{{user.Username}}</el-row>
                            <el-row class="message-pre" style="line-height:20px;font-size:12px;color:#777;" v-if="user.Bio!=''">{{user.Bio}}</el-row>

                            <el-row style="font-weight:600;line-height:45px;letter-spacing:1px;color:#333;" v-if="user.Bio==''">{{user.Username}}</el-row>
                        </el-col>
                        <el-col :span="6" style="font-weight:400;line-height:45px;color:#555;font-size:13px;text-align:end;">关注了你</el-col>
                    </el-row>
                    <div v-if="!messageFollow.length" class="alert">还没有人关注你呢，发一些动态获取更多关注吧~</div>
                </el-row>

                <!-- 其他 -->
                <el-row v-if="isForward||isComment||isLike" style="padding:30px;">
                    <div style="height:30px;border-bottom:1px solid #6191d5;color:#555">{{msgType}}</div>
                    <el-row v-if="messageMoment.length" v-for="(msg,index) in messageMoment" :key="index" class="msg-list" @click.native="jumpToDetail(msg.moment.ID)">
                        <el-col :span="2">
                            <div style="background:#deefff;width:40px;height:40px;border-radius:40px;position:relative">
                                <img :src="msgTypeSrc" alt="messageType" class="middle">
                            </div>
                        </el-col>
                        <el-col :span="3" style="font-weight:600;line-height:40px;" class="message-pre">{{msg.user.Username}}</el-col>
                        <el-col :span="13" :offset="1" style="color:#6191d5;line-height:40px;" class="message-pre">{{msg.moment.Content}}</el-col>
                        <el-col :span="5" style="font-size:12px;color:#999;line-height:40px;text-align:end;">{{msg.moment.Time}}</el-col>
                    </el-row>
                    <div v-if="!messageMoment.length" class="alert">还没有人{{msgType}}你的动态呢，加油吧~</div>
                </el-row>
            </el-col>
        </el-row>
    </el-container>
</template>

<style scoped>
    .alert {
        color: #999;
        font-size: 13px;
        text-align: center;
        line-height: 50px;
        letter-spacing: 1px;
    }

    #dialog-wrap {
        display: none;
    }

    .like-list {
        padding: 10px;
        border-bottom: 1px solid rgb(235, 238, 245);
    }

    .msg-list {
        padding: 20px;
        border-bottom: 1px solid rgb(235, 238, 245);
    }

    .msg-list:hover {
        background-color: #deefff;
    }

    .middle {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }

    .hover-cursor {
        cursor: pointer;
    }

    .message-input {
        margin-top: 10px;
        height: 86px;
        width: 100%;
        border: 0;
        resize: none;
        padding: 10px;
        box-sizing: border-box;
        outline: 0;
        border-top: 1px solid #d5d3d3;
        margin-top: 0
    }

    .message-pre {
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }

    .icon {
        width: 20px;
        height: 20px;
        margin-right: 5%;
    }

    .right {
        background: #fff;
        border-left: 1px solid rgb(235, 238, 245);
    }

    .el-menu {
        border: 0 solid #fff;
    }

    .el-menu-item:hover {
        background-color: #deefff;
    }

    .user-list {
        border-right: 1px solid rgb(235, 238, 245);
        height: 600px;
        overflow-x: hidden;
        overflow-y: auto;
        background: #fcfcfc;
    }

    .header-img {
        width: 45px;
        height: 45px;
        border-radius: 45px;
    }

    .user-li {
        padding: 15px 10px 10px 10px;
        border-bottom: 1px solid rgb(235, 238, 245);
    }

    .user-li:hover {
        background: #deefff;
    }

    .selected {
        background: #deefff;
    }

    .dialog {
        height: 450px;
        overflow-x: hidden;
        overflow-y: scroll;
        padding-right: 30px;
        margin-right: -30px;
        max-height: 450px;
    }

    .dialog-inner {}

    .content {
        margin: 30px 0;
    }

    .float-right {
        float: right;
    }

    .float-left {
        float: left;
    }

    .message-limit {
        width: 300px;
        position: relative;
        font-size: 13px;
    }

    .message-box-left {
        background: #eee;
        margin-left: 15px;
        padding: 14px;
        display: inline-block;
        border-radius: 5px;
        float: left;
        word-break: break-word;
    }

    .message-box-right {
        background: #79ace5;
        margin-right: 15px;
        padding: 14px 10px;
        display: inline-block;
        border-radius: 5px;
        float: right;
        word-break: break-word;
        color: #fff;
    }

    .bubble-left {
        background: url('../image/bubble-left.png');
        background-size: cover;
        height: 20px;
        width: 15px;
        position: absolute;
        top: 10px;
        left: 1px;
    }

    .bubble-right {
        background: url('../image/bubble-right.png');
        background-size: cover;
        height: 20px;
        width: 15px;
        position: absolute;
        top: 10px;
        right: 6px;
    }
</style>

<script>
    import Vue from 'vue'

    export default {
        data() {
            return {
                messageIcon: require('../image/active-message-icon.png'),
                followIcon: require('../image/follow-icon.png'),
                commentIcon: require('../image/comment-icon.png'),
                likeIcon: require('../image/like-icon.png'),
                forwardIcon: require('../image/forward-icon.png'),
                isMessage: true,
                isDialog: false,
                isFollow: false,
                isComment: false,
                isLike: false,
                isForward: false,

                msgType: '',
                msgTypeSrc: require('../image/forward-icon.png'),
                currentTalker: {
                    ID: '',
                    Username: '',
                    Photo: ''
                },
                messageInput: '',
                photo: require('../image/hex.jpeg'),
                bubbleRight: require('../image/bubble-right.png'),
                //私信列表
                messageUserList: [

                ],
                // 两人的私信
                messages: [

                ],
                //其他通知列表
                messageMoment: [

                ],
                //关注通知列表
                messageFollow: [

                ]
            }
        },
        created() {
            this.photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID +
                '&type=2';
            //一开始只请求私信的相关数据
            this.messageInit();

        },
        mounted() {
            var dialog = document.getElementById('dialog');
            dialog.scrollTop = dialog.scrollHeight;
        },
        methods: {
            init: function () {
                this.isMessage = false;
                this.isFollow = false;
                this.isComment = false;
                this.isLike = false;
                this.isForward = false;
                this.messageIcon = require('../image/message-icon.png');
                this.followIcon = require('../image/follow-icon.png');
                this.commentIcon = require('../image/comment-icon.png');
                this.likeIcon = require('../image/like-icon.png');
                this.forwardIcon = require('../image/forward-icon.png');
            },
            menuSelectHandler(key, keyPath) {
                this.init();
                if (key == 'message') {
                    this.isMessage = true;
                    this.messageIcon = require('../image/active-message-icon.png');
                } else if (key == 'follow') {
                    this.isFollow = true;
                    this.followIcon = require('../image/active-follow-icon.png');
                    this.questFollows();
                } else if (key == 'comment') {
                    this.isComment = true;
                    this.msgType = '评论';
                    this.msgTypeSrc = require('../image/active-comment-icon.png');
                    this.commentIcon = require('../image/active-comment-icon.png');
                    this.questOthers('Message/CommentState');
                } else if (key == 'like') {
                    this.isLike = true;
                    this.msgType = '点赞';
                    this.msgTypeSrc = require('../image/active-like-icon.png');
                    this.likeIcon = require('../image/active-like-icon.png');
                    this.questOthers('Message/LikeState');
                } else {
                    this.isForward = true;
                    this.msgType = '转发';
                    this.msgTypeSrc = require('../image/active-forward-icon.png');
                    this.forwardIcon = require('../image/active-forward-icon.png');
                    this.questOthers('Message/ForwardState');
                }
                // console.log(this.currentTalker.ID)
            },
            sendMessage() {
                this.axios.post('http://192.168.43.249:54468/api/Message/SendMessage', {
                        Sender_ID: this.$store.state.currentUserId_ID,
                        Receiver_ID: this.currentTalker.ID,
                        Send_Time: this.getNowFormatDate(),
                        Content: this.messageInput
                    })
                    .then((response) => {
                        if (response.data == 0) {
                            var dialogInner = document.getElementById('dialog-inner');
                            var dialog = document.getElementById('dialog');
                            dialogInner.innerHTML = dialogInner.innerHTML +
                                '<div class="content">' +
                                '<div>' +
                                '<img src="' + this.photo +
                                '" alt="" @click="jumpToUser(this.$store.state.currentUserId_ID)" class="float-right header-img" />' +
                                '<div class="message-limit float-right">' +
                                '<div class="message-box-right" ><span>' + this.messageInput + '</span></div>' +
                                '<i class="bubble-right"></i>' +
                                '</div>' +
                                '</div>' +
                                '<div style="clear:both"></div>' +
                                '</div>' +
                                '<style>' +
                                '.content{margin:30px 0;}' +
                                '.header-img{width:45px;height:45px;border-radius: 45px;}' +
                                '.message-limit{width: 300px;position: relative;font-size: 13px;}' +
                                '.float-right{float: right;}' +
                                '.message-box-right{background: #79ace5;margin-right: 15px;padding: 14px 10px;display: inline-block;border-radius: 5px;float: right;word-break: break-word;color:#fff;}' +
                                '.bubble-right{background: url("' + this.bubbleRight +
                                '");background-size: cover;height: 20px;width: 15px;position: absolute;top:10px;right: 6px;}' +
                                '</style>';

                            var msg_end = document.getElementById('msg_end');
                            msg_end.scrollIntoView();
                            this.messageWebsocketHandler(this.currentTalker.ID, 4, " " + this.messageInput);
                            this.currentTalker.message = this.messageInput;
                            console.log(this.currentTalker)
                            this.messageInput = '';
                        } else {
                            this.$message.error('发送失败，请重试');
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            jumpToUser: function (id) {
                this.$router.push('/main/personalpage/' + id);
            },
            jumpToDetail: function (id) {
                this.$router.push('/main/momentDetail/' + id);
            },
            selectUser: function (user) {
                //选中私信用户的状态
                this.messageUserList.forEach(element => {
                    element.selected = false;
                });
                //选择要私信的用户，请求历史私信
                user.selected = true;
                this.currentTalker = user;
                this.questMessages();

                var dialog = document.getElementById('dialog');
                dialog[0].scrollTop = dialog[0].scrollHeight;
                console.log(dialog.scrollTop)
                this.messageInput = '';
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
            messageInit: function () {
                //请求私信用户列表
                this.axios.get('http://192.168.43.249:54468/api/Message/GetUser?Sender_ID=' + this.$store.state.currentUserId_ID)
                    .then((response) => {
                        this.messageUserList = response.data;
                        this.messageUserList.forEach(element => {
                            Vue.set(element, 'selected', false);
                            var photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                element.ID +
                                '&type=2';
                            Vue.set(element, 'Photo', photo);
                            //最后一条私信内容
                            this.axios.get('http://192.168.43.249:54468/api/Message/GetMessage', {
                                    params: {
                                        Sender_ID: this.$store.state.currentUserId_ID,
                                        Receiver_ID: element.ID
                                    }
                                })
                                .then((res) => {
                                    console.log(res.data.Item2.length)
                                    if (res.data.Item2.length) {
                                        let message = res.data.Item2[res.data.Item2.length - 1];
                                        console.log(message)
                                        Vue.set(element, 'message', message);
                                    } else {
                                        Vue.set(element, 'message', '');
                                    }
                                })
                                .catch((error) => {
                                    console.log(error);
                                })
                        });

                        var currentTalkerId = this.$route.params.id;
                        if (currentTalkerId != 'self') { //从私信按钮进来
                            let index = 0;
                            this.messageUserList.forEach(element => {
                                //在历史私信列表中，直接选中
                                if (element.ID == currentTalkerId) {
                                    element.selected = true;
                                    this.currentTalker = element;
                                    if (!("message" in this.currentTalker)) {
                                        Vue.set(this.currentTalker, 'message', "")
                                    }

                                    index--;
                                }
                                index++;
                            });
                            //不在历史私信用户列表中
                            if (index == this.messageUserList.length) {
                                //插入新的私信用户
                                this.axios.get('http://192.168.43.249:54468/api/Users/GetUserInfobyID', {
                                        params: {
                                            id: currentTalkerId
                                        }
                                    })
                                    .then((response) => {
                                        let user = response.data;
                                        Vue.set(user, 'selected', true);
                                        var photo =
                                            'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                            user.ID +
                                            '&type=2';
                                        Vue.set(user, 'Photo', photo);
                                        Vue.set(user, 'message', '');
                                        this.messageUserList.unshift(user);
                                        this.currentTalker = user;
                                    })
                                    .catch((error) => {
                                        console.log(error);
                                    })
                            }
                        } else {
                            //默认选择第一个
                            console.log(this.currentTalker.ID);
                            if (this.currentTalker.ID == '') {
                                this.messageUserList[0].selected = true;
                                this.currentTalker = this.messageUserList[0];
                            }
                        }
                        this.questMessages();
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            messageWebsocketHandler(path, state, content = "") {
                // 0 关注 1 点赞 2 评论 3 转发 4 私信
                window.ws.send('/' + path + ' ' + state + content);
            },
            questUsers: function () {
                //请求私信用户列表
                this.axios.get('http://192.168.43.249:54468/api/Message/GetUser?Sender_ID=' + this.$store.state.currentUserId_ID)
                    .then((response) => {
                        this.messageUserList = response.data;
                        this.messageUserList.forEach(element => {
                            Vue.set(element, 'selected', false);
                            var photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                element.ID +
                                '&type=2';
                            Vue.set(element, 'Photo', photo);
                            //最后一条私信内容
                            this.axios.get('http://192.168.43.249:54468/api/Message/GetMessage', {
                                    params: {
                                        Sender_ID: this.$store.state.currentUserId_ID,
                                        Receiver_ID: element.ID
                                    }
                                })
                                .then((res) => {
                                    console.log(res.data.Item2.length)
                                    if (res.data.Item2.length) {
                                        let message = res.data.Item2[res.data.Item2.length - 1];
                                        console.log(message)
                                        Vue.set(element, 'message', message);
                                    } else {
                                        Vue.set(element, 'message', '');
                                    }
                                })
                                .catch((error) => {
                                    console.log(error);
                                })
                        });
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            questMessages: function () {
                this.axios.get('http://192.168.43.249:54468/api/Message/GetMessage', {
                        params: {
                            Sender_ID: this.$store.state.currentUserId_ID,
                            Receiver_ID: this.currentTalker.ID
                        }
                    })
                    .then((response) => {
                        if (response.data.Item1.length) {
                            var index = 0;
                            this.messages = [];
                            response.data.Item1.forEach(element => {
                                this.messages.push({
                                    identity: response.data.Item1[index],
                                    content: response.data.Item2[index]
                                });
                                index++;
                            });

                        } else {
                            this.messages = [];
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
                var dialog = document.getElementById('dialog');
                dialog.scrollTop = dialog.scrollHeight;
            },
            questFollows: function () {
                this.axios.get('http://192.168.43.249:54468/api/Users/FollowList?userID=' + this.$store.state.currentUserId_ID)
                    .then((response) => {
                        if (response.data.length) {
                            this.messageFollow = response.data;
                            this.messageFollow.forEach(element => {
                                var photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                    element.ID +
                                    '&type=2';
                                console.log(photo)
                                Vue.set(element, 'Photo', photo);
                            })
                        } else {
                            this.messageFollow = [];
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            questOthers: function (api) {
                this.axios.get('http://192.168.43.249:54468/api/' + api + '?user_id=' + this.$store.state.currentUserId_ID)
                    .then((response) => {
                        if (response.data.m_Item1 != null) {
                            let moments = response.data.m_Item1;
                            let users = response.data.m_Item2;
                            let index = 0;
                            this.messageMoment = [];
                            response.data.m_Item1.forEach(element => {
                                this.messageMoment.push({
                                    moment: {
                                        ID: moments[index].ID,
                                        Content: moments[index].Content
                                    },
                                    user: {
                                        ID: users[index].ID,
                                        Username: users[index].Username
                                    }
                                });
                                index++
                            });
                        } else {
                            this.messageMoment = [];
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            }
        }
    }
</script>