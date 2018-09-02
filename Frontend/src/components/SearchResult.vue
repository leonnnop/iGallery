<template>
    <el-container>
        <transition name="slide-fade">
            <div v-if="loadingPage" class="loadingbackground" style="margin-top:-30px"></div>
        </transition>
        <el-row style="width:100%">
            <el-row class="box user-container">
                <el-row style="margin:20px">
                    <span style="margin-left:20px;color:#777">相关用户</span>
                    <!-- <span class="more hover-cursor" @click="moreHandler(keyword)" v-if="hasUser">查看全部</span> -->
                </el-row>
                <el-row class="user-inner" v-if="hasUser">
                    <div class="arrow arrow-left" :class="{'arrow-display':showUsersLeftArrow}" @click="slideToRightUsers">
                        <img src="../image/sm-arrow-left.png" class="arrow-img" alt="left-arrow">
                    </div>
                    <div class="arrow arrow-right" :class="{'arrow-display':showUsersRightArrow}" @click="slideToLeftUsers">
                        <img src="../image/sm-arrow-right.png" class="arrow-img" alt="right-arrow">
                    </div>

                    <el-row type="flex" justify="space-around">
                        <div id="user-list" :style="{width:userListLength+'px'}">
                            <div class="display-user" v-for="(user,index) in users" :key="index">
                                <el-row style="width:80%;margin:0 auto">
                                    <el-row>
                                        <img :src="user.Photo+'&Rand=' + Math.random()" alt="头像" class="user-img hover-cursor" @click="jumpToUser(user.ID)">
                                    </el-row>
                                    <el-row style="margin-top:10%;color:#333;font-weight:600">{{user.Username}}</el-row>
                                    <el-row style="margin-top:5%;color:#777;font-size:13px" class="self-intro">{{user.Bio}}</el-row>
                                    <el-row v-show="user.showFollowBtn">
                                        <el-button plain type="primary" size="medium" style="width:80%;margin-top:15%" @click="followUserHandler(user)">{{user.followWord}}</el-button>
                                    </el-row>
                                </el-row>
                            </div>
                        </div>
                    </el-row>

                </el-row>
                <div v-if="!hasUser" class="message">没有找到相关用户</div>
            </el-row>

            <el-row class="box tag-container">
                <el-row style="margin:20px">
                    <span style="margin-left:20px;color:#777">相关标签</span>
                    <!-- <span class="more hover-cursor" @click="moreHandler(keyword)" v-if="hasTag">查看全部</span> -->
                </el-row>
                <el-row class="tag-inner" v-if="hasTag">
                    <div class="arrow arrow-left" :class="{'arrow-display':showTagsLeftArrow}" @click="slideToRightTags">
                        <img src="../image/sm-arrow-left.png" class="arrow-img" alt="left-arrow">
                    </div>
                    <div class="arrow arrow-right" :class="{'arrow-display':showTagsRightArrow}" @click="slideToLeftTags">
                        <img src="../image/sm-arrow-right.png" class="arrow-img" alt="right-arrow">
                    </div>
                    <el-row type="flex" justify="space-around">
                        <div style="height:260px;" id="tag-list" :style="{width:tagListLength+'px'}">
                            <div class="display-tag" v-for="(tag,index) in tags" :key="index">
                                <el-row style="width:80%;margin:0 auto">
                                    <el-row>
                                        <img :src="tag.Pic+'&Rand=' + Math.random()" alt="tagImg" class="tag-img hover-cursor" @click="jumpToTag(tag.Tag)">
                                    </el-row>
                                    <el-row style="margin-top:10%;color:#333;font-weight:600">#{{tag.Tag}}</el-row>
                                    <el-row>
                                        <el-button plain size="medium" type="primary" style="width:80%;margin-top:15%" @click="followUserHandler(tag.Content)">{{tag.followWord}}</el-button>
                                    </el-row>
                                </el-row>
                            </div>
                        </div>
                    </el-row>

                </el-row>
                <div v-if="!hasTag" class="message">没有找到相关标签</div>
            </el-row>

            <el-row style="width:902px;margin:50px auto;border:1px solid rgb(235, 238, 245);border-radius:5px;background:#fff;">
                <el-row style="margin:20px 0;;width:902px">
                    <div style="color:#777;text-align:center;letter-spacing:1px">相关动态</div>
                </el-row>
                <div v-if="!hasMoment" class="message">没有找到相关动态</div>
                <div style="width:902px" class="moment-container">
                    <div v-for="(moment,index) in moments" :key="index" class="moment" :style="{backgroundImage: 'url(' + (moment.src) +'&Rand=' + Math.random() +  ')'}">
                        <div class="moment-inner">
                            <div class="icon">
                                <el-row type="flex" justify="space-between">
                                    <el-col :span="8">
                                        <img src="../image/like-white.png" alt="">
                                        <div>{{moment.LikeNum}}</div>
                                    </el-col>
                                    <el-col :span="8">
                                        <img src="../image/look.png" alt="look" class="look hover-cursor" @click="jumpToDetail(moment.ID)">
                                        <span>查看</span>
                                    </el-col>
                                </el-row>
                            </div>
                        </div>
                    </div>
                </div>
            </el-row>

        </el-row>

    </el-container>
</template>

<style scoped>
    .more {
        float: right;
        margin-right: 20px;
        color: #6191d5;
        font-size: 13px;
    }

    .message {
        height: 270px;
        width: 100%;
        line-height: 270px;
        text-align: center;
        color: #999;
        letter-spacing: 1px;
        border-top: 1px solid rgb(235, 238, 245);
    }

    .arrow {
        position: absolute;
        top: 50%;
        margin-top: -15px;
        height: 30px;
        width: 30px;
        border-radius: 30px;
        display: none;
        z-index: 10;
        background-color: rgba(0, 0, 0, 0.5);
        text-align: center;
    }

    .arrow-left {
        left: 1%;
    }

    .arrow-right {
        right: 1%;
    }

    .arrow-img {
        height: 15px;
        width: 15px;
        position: absolute;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);

    }

    .box {
        width: 902px;
        margin: 0 auto;
        border: 1px solid rgb(235, 238, 245);
        border-radius: 5px;
        background: #fff;
        overflow: hidden;
        box-sizing: border-box;
    }

    .user-container {
        height: 360px;
        position: relative;
    }

    .user-inner {
        position: relative;
        height: 270px;
    }

    .user-inner:hover .arrow-display {
        display: block;
    }

    .tag-inner:hover .arrow-display {
        display: block;
    }

    .tag-container {
        height: 340px;
        position: relative;
        margin-top: 30px;
    }

    .tag-inner {
        position: relative;
        height: 260px;
    }

    .user-img,
    .tag-img {
        height: 80px;
        width: 80px;
        margin-top: 30px;
        border-radius: 80px
    }

    .display-user {
        border: 1px solid rgb(235, 238, 245);
        border-radius: 5px;
        text-align: center;
        width: 230px;
        height: 270px;
        display: inline-block;
        margin: 0 35px;
        box-sizing: border-box;
    }

    .display-tag {
        border: 1px solid rgb(235, 238, 245);
        border-radius: 5px;
        text-align: center;
        width: 230px;
        height: 250px;
        display: inline-block;
        margin: 0 35px;
        box-sizing: border-box;
    }

    .self-intro {
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }

    .hover-cursor {
        cursor: pointer;
    }

    .moment {
        width: 260px;
        height: 0;
        padding-bottom: 260px;
        margin: 0 60px 30px 0;
        background-position: center;
        background-size: cover;
        display: inline-block;
        box-sizing: border-box;
    }

    .moment-container div:nth-child(3n) {
        margin: 0 0 30px 0;
    }

    .moment-inner {
        height: 0;
        width: 260px;
        padding-bottom: 100%;
        display: none;
        background: rgba(0, 0, 0, 0.3);
        position: relative;
    }

    .moment:hover .moment-inner {
        display: block;
    }

    .icon {
        position: absolute;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);
        text-align: center;
        color: #fff;
        font-size: 12px;
    }

    #user-list,
    #tag-list {
        position: absolute;
        top: 0;
        left: 0;
    }

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
</style>

<script>
    import Vue from 'vue';

    function length(list) {
        let length = list.length;
        if (length % 3 == 0) {
            return (length / 3) * 902;
        } else if (length % 3 == 1) {
            return ((length + 2) / 3) * 902;
        } else if (length % 3 == 2) {
            return ((length + 1) / 3) * 902;
        }
        // console.log(this.userListLength);
    }
    export default {
        data() {
            return {
                keyword: '',
                loadingPage: true,

                showUsersLeftArrow: false,
                showUsersRightArrow: false,
                showTagsLeftArrow: false,
                showTagsRightArrow: false,
                userListLength: 2700,
                tagListLength: 1800,
                hasUser: true,
                hasTag: true,
                hasMoment: true,
                users: [{
                    Username: 'loststars',
                    ID: '',
                    Email: '123@qq.com',
                    Bio: 'self introduction self introduction self introduction',
                    Photo: require('../image/a.jpg')
                }, {
                    Username: 'loststars',
                    ID: '',
                    Email: '',
                    Bio: 'self introduction',
                    Photo: require('../image/a.jpg')
                }, {
                    Username: 'loststars',
                    ID: '',
                    Email: '',
                    Bio: 'self introduction',
                    Photo: require('../image/a.jpg')
                }, {
                    Username: 'loststars',
                    ID: '',
                    Email: '',
                    Bio: 'self introduction',
                    Photo: require('../image/a.jpg')
                }, {
                    Username: 'loststars',
                    ID: '',
                    Email: '',
                    Bio: 'self introduction',
                    Photo: require('../image/a.jpg')
                }, {
                    Username: 'loststars',
                    ID: '',
                    Email: '',
                    Bio: 'self introduction',
                    Photo: require('../image/a.jpg')
                }, {
                    Username: 'loststars',
                    ID: '',
                    Email: '',
                    Bio: 'self introduction',
                    Photo: require('../image/a.jpg')
                }],
                tags: [{
                    Content: 'tag1',
                    src: require('../image/hex.jpeg')
                }, {
                    Content: 'tag2',
                    src: require('../image/hex.jpeg')
                }, {
                    Content: 'tag3',
                    src: require('../image/hex.jpeg')
                }, {
                    Content: 'tag4',
                    src: require('../image/hex.jpeg')
                }, {
                    Content: 'tag5',
                    src: require('../image/hex.jpeg')
                }],
                moments: [{
                    ID: '1',
                    src: require('../image/photo1.jpg')
                }, {
                    ID: '2',
                    src: require('../image/photo2.jpg')
                }, {
                    ID: '3',
                    src: require('../image/photo3.jpg')
                }, {
                    ID: '4',
                    src: require('../image/photo1.jpg')
                }, {
                    ID: '5',
                    Sender_Id: '',
                    src: require('../image/photo2.jpg')
                }, {
                    ID: '3',
                    src: require('../image/photo3.jpg')
                }, {
                    ID: '4',
                    src: require('../image/photo1.jpg')
                }, {
                    ID: '5',
                    Sender_Id: '',
                    src: require('../image/photo2.jpg')
                }]

            }
        },
        beforeRouterEnter(to, from, next) {
            //用户
            next(vm => {
                vm.axios.all([vm.axios.get('http://192.168.43.249:54468/api/Search/Search_user?keyword=' + vm.$route
                            .params
                            .keyword + '&user_id=' + vm.$store.state.currentUserId_ID),
                        vm.axios.get('http://192.168.43.249:54468/api/Search/Search_all?keyword=' + vm.$route
                            .params.keyword +
                            '&user_id=' + vm.$store.state.currentUserId_ID)
                    ])
                    .then(vm.axios.spread((res1, res2) => {
                        //用户
                        if (res1.data != 'Not found') {
                            vm.users = res1.data;
                            //关注状态
                            vm.users.forEach(element => {
                                if (element.ID == vm.$store.state.currentUserId_ID) {
                                    Vue.set(element, 'showFollowBtn', false);
                                } else {
                                    Vue.set(element, 'showFollowBtn', true);
                                    if (element.FollowState == 'True') {
                                        Vue.set(element, 'followWord', '已关注');
                                    } else {
                                        Vue.set(element, 'followWord', '关注');
                                    }
                                }

                                var photo =
                                    'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                    element.ID +
                                    '&type=2';
                                Vue.set(element, 'Photo', photo)
                            });
                            vm.hasUser = true;
                        } else {
                            vm.hasUser = false;
                            vm.users = []
                        }

                        vm.userListLength = length(vm.users);
                        console.log(vm.userListLength);

                        //tag和动态
                        if (res2.data.m_Item1.length != 0) {
                            vm.tags = res2.data.m_Item1;
                        } else {
                            vm.hasTag = false;
                            vm.tags = []
                        }

                        if (res2.data.m_Item2.length != 0) {
                            vm.moments = res2.data.m_Item2;
                            vm.hasMoment = true;
                            // Vue.set()

                            moments.forEach(element => {
                                Vue.set(element, 'LikeNum', element.LikeNum)
                                vm.axios.get(
                                        'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                        element.ID +
                                        '&type=1')
                                    .then((response) => {
                                        Vue.set(element, 'src',
                                            'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                                            response.data[0]);
                                    })
                            })
                            console.log('moments----------');
                            console.log(vm.moments);
                        } else {
                            vm.hasMoment = false;
                            vm.moments = []
                        }

                        if (vm.tags.length) {
                            vm.hasTag = true;
                            //tag的关注状态
                            vm.tags.forEach(element => {

                                if (element.FollowState == 'True') {
                                    Vue.set(element, 'followWord', '已关注');
                                } else {
                                    Vue.set(element, 'followWord', '关注');
                                }
                                var Pic = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                                    element.Pic;
                                Vue.set(element, 'Pic', Pic)
                            })
                        } else {
                            vm.hasTag = false;
                        }

                        vm.tagListLength = length(vm.tags);

                        if (vm.userListLength > 902) {
                            vm.showUsersRightArrow = true;
                        }
                        if (vm.tagListLength > 902) {
                            vm.showTagsRightArrow = true;
                        }
                    }))
                    .catch((error) => {
                        console.log(error);
                    });
            });



        },
        created() {
            //用户
            this.axios.all([this.axios.get('http://192.168.43.249:54468/api/Search/Search_user?keyword=' + this.$route.params
                        .keyword + '&user_id=' + this.$store.state.currentUserId_ID),
                    this.axios.get('http://192.168.43.249:54468/api/Search/Search_all?keyword=' + this.$route.params
                        .keyword +
                        '&user_id=' + this.$store.state.currentUserId_ID)
                ])
                .then(this.axios.spread((res1, res2) => {
                    //用户
                    if (res1.data != 'Not found') {
                        this.users = res1.data;
                        //关注状态
                        this.users.forEach(element => {
                            if (element.ID == this.$store.state.currentUserId_ID) {
                                Vue.set(element, 'showFollowBtn', false);
                            } else {
                                Vue.set(element, 'showFollowBtn', true);
                                if (element.FollowState == 'True') {
                                    Vue.set(element, 'followWord', '已关注');
                                } else {
                                    Vue.set(element, 'followWord', '关注');
                                }
                            }

                            var photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                element.ID +
                                '&type=2';
                            Vue.set(element, 'Photo', photo)
                        });
                        this.hasUser = true;
                    } else {
                        this.hasUser = false;
                        this.users = []
                    }

                    this.userListLength = length(this.users);
                    console.log(this.userListLength);

                    //tag和动态
                    if (res2.data.m_Item1.length != 0) {
                        this.tags = res2.data.m_Item1;
                    } else {
                        this.hasTag = false;
                        this.tags = []
                    }

                    if (res2.data.m_Item2.length != 0) {
                        this.moments = res2.data.m_Item2;
                        this.hasMoment = true;
                        // Vue.set()

                        this.moments.forEach(element => {
                            Vue.set(element, 'LikeNum', element.LikeNum)
                            this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                    element.ID +
                                    '&type=1')
                                .then((response) => {
                                    Vue.set(element, 'src',
                                        'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                                        response.data[0]);
                                })
                        })
                        console.log('moments----------');
                        console.log(this.moments);
                    } else {
                        this.hasMoment = false;
                        this.moments = []
                    }

                    if (this.tags.length) {
                        this.hasTag = true;
                        //tag的关注状态
                        this.tags.forEach(element => {

                            if (element.FollowState == 'True') {
                                Vue.set(element, 'followWord', '已关注');
                            } else {
                                Vue.set(element, 'followWord', '关注');
                            }
                            var Pic = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + element.Pic;
                            Vue.set(element, 'Pic', Pic)
                        })
                    } else {
                        this.hasTag = false;
                    }

                    this.tagListLength = length(this.tags);

                    if (this.userListLength > 902) {
                        this.showUsersRightArrow = true;
                    }
                    if (this.tagListLength > 902) {
                        this.showTagsRightArrow = true;
                    }
                }))
                .catch((error) => {
                    console.log(error);
                });


        },
        methods: {
            followUserHandler: function (user) {
                this.axios.get('http://192.168.43.249:54468/api/Users/Follow?followID=' + this.$store.state.currentUserId_ID +
                        '&followedID=' + user.ID)
                    .then((response) => {
                        if (response.data == 0) {
                            if (user.FollowState == 'False') {
                                user.followWord = '已关注';
                                user.FollowState = 'True';
                                this.messageWebsocketHandler(user.ID, 0)
                            } else {
                                user.followWord = '关注';
                                user.FollowState = 'False';
                            }
                            // user.FollowState = !user.FollowState;
                        } else {
                            this.$message.error('关注失败，服务器内部错误，请重试。');
                        }
                    });
            },
            followTagHandler: function (tag) {
                this.axios.put('http://192.168.43.249:54468/api/Follow_Tag/FollowTag?Email=' + this.$store.state.currentUserId +
                        '&tag=' + tag)
                    .then((response) => {
                        if (response.data) {
                            if (tag.FollowState == 'False') {
                                tag.followWord = '已关注'
                            } else {
                                tag.followWord = '关注'
                            }
                            tag.FollowState = !tag.FollowState;
                        } else {
                            this.$message.error('服务器内部错误。请重试。');
                        }
                    })
            },
            jumpToUser: function (ID) {
                // if (email == this.$store.state.currentUserId) {
                //     this.$router.push('/main/personalpage/');
                // } else {
                this.$router.push('/main/personalpage/' + ID);
                // }
            },
            jumpToTag: function (tag) {
                this.$router.push('/main/tag/' + tag);
            },
            jumpToDetail: function (momentId) {
                this.$router.push('/main/momentDetail/' + momentId);
            },
            moreHandler: function () {

            },
            slideToLeftUsers: function () {
                var list = document.getElementById('user-list');
                var offset = list.offsetLeft;
                var width = parseInt(list.style.width);
                list.style.left = list.offsetLeft - 902 + 'px';
                list.style.transition = "all 1s";
                this.showUsersLeftArrow = true;
                if (list.offsetLeft - 1800 == -width) {
                    this.showUsersRightArrow = false;
                } else {
                    this.showUsersRightArrow = true;
                }
            },
            slideToRightUsers: function () {
                var list = document.getElementById('user-list');
                var offset = list.offsetLeft;
                var width = list.style.width;
                list.style.left = list.offsetLeft + 902 + 'px';
                list.style.transition = "all 1s";
                this.showUsersRightArrow = true;
                if (list.offsetLeft + 902 == 0) {
                    this.showUsersLeftArrow = false;
                    this.showUsersRightArrow = true;
                } else {
                    this.showUsersLeftArrowtrue;
                }
            },
            slideToLeftTags: function () {
                var list = document.getElementById('tag-list');
                var offset = list.offsetLeft;
                var width = parseInt(list.style.width);
                list.style.left = list.offsetLeft - 902 + 'px';
                list.style.transition = "all 1s";
                this.showTagsLeftArrow = true;
                if (list.offsetLeft - 1800 == -width) {
                    this.showTagsRightArrow = false;
                } else {
                    this.showUsersRightArrow = true;
                }
            },
            slideToRightTags: function () {
                var list = document.getElementById('tag-list');
                var offset = list.offsetLeft;
                var width = list.style.width;
                list.style.left = list.offsetLeft + 902 + 'px';
                list.style.transition = "all 1s";
                if (list.offsetLeft + 902 == 0) {
                    this.showTagsLeftArrow = false;
                    this.showTagsRightArrow = true;
                } else {
                    this.showTagsLeftArrow = true;
                }
            },
            messageWebsocketHandler(path, state) {
                // 0 关注 1 点赞 2 评论 3 转发 4 私信
                window.ws.send('/' + path + ' ' + state);
            },

        },
        mounted: function () {
            this.$nextTick(function () {
                var self = this;
                setTimeout(function () {
                    self.loadingPage = false;
                }, 1700);
                window.scroll(0, 0);

            })
        },

        beforeRouteEnter(from, to, next) {
            next(vm => {
                var self = vm;
                vm.$nextTick(function () {
                    setTimeout(function () {
                        self.loadingPage = false;
                    }, 1500)
                })
            })
        },
        beforeRouteUpdate(to, from, next) {
            this.loadingPage = true
            var self = this;
            setTimeout(function () {
                self.loadingPage = false;
            }, 1500)
            this.axios.all([this.axios.get('http://192.168.43.249:54468/api/Search/Search_user?keyword=' + to.params
                        .keyword + '&user_id=' + this.$store.state.currentUserId_ID),
                    this.axios.get('http://192.168.43.249:54468/api/Search/Search_all?keyword=' + to.params.keyword +
                        '&user_id=' + this.$store.state.currentUserId_ID)
                ])
                .then(this.axios.spread((res1, res2) => {
                    //用户
                    if (res1.data != 'Not found') {
                        this.users = res1.data;
                        //关注状态
                        this.users.forEach(element => {
                            if (element.ID == this.$store.state.currentUserId_ID) {
                                Vue.set(element, 'showFollowBtn', false);
                            } else {
                                Vue.set(element, 'showFollowBtn', true);
                                if (element.FollowState == 'True') {
                                    Vue.set(element, 'followWord', '已关注');
                                } else {
                                    Vue.set(element, 'followWord', '关注');
                                }
                            }

                            var photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                element.ID +
                                '&type=2';
                            Vue.set(element, 'Photo', photo)
                        });
                        this.hasUser = true;
                    } else {
                        this.hasUser = false;
                        this.users = []
                    }

                    this.userListLength = length(this.users);
                    console.log(this.userListLength);

                    //tag和动态
                    if (res2.data.m_Item1.length != 0) {
                        this.tags = res2.data.m_Item1;
                    } else {
                        this.hasTag = false;
                        this.tags = []
                    }

                    if (res2.data.m_Item2.length != 0) {
                        this.moments = res2.data.m_Item2;
                        this.hasMoment = true;
                        // Vue.set()

                        this.moments.forEach(element => {
                            Vue.set(element, 'LikeNum', element.LikeNum)
                            this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
                                    element.ID +
                                    '&type=1')
                                .then((response) => {
                                    Vue.set(element, 'src',
                                        'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                                        response.data[0]);
                                })
                        })
                        console.log('moments----------');
                        console.log(this.moments);
                    } else {
                        this.hasMoment = false;
                        this.moments = []
                    }

                    if (this.tags.length) {
                        this.hasTag = true;
                        //tag的关注状态
                        this.tags.forEach(element => {

                            if (element.FollowState == 'True') {
                                Vue.set(element, 'followWord', '已关注');
                            } else {
                                Vue.set(element, 'followWord', '关注');
                            }
                            var Pic = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + element.Pic;
                            Vue.set(element, 'Pic', Pic)
                        })
                    } else {
                        this.hasTag = false;
                    }

                    this.tagListLength = length(this.tags);

                    if (this.userListLength > 902) {
                        this.showUsersRightArrow = true;
                    }
                    if (this.tagListLength > 902) {
                        this.showTagsRightArrow = true;
                    }

                }))
                .catch((error) => {
                    console.log(error);
                });
            next()
        }
    }
</script>