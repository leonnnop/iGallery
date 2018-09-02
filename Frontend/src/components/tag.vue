<template>
    <el-container>
        <transition name="slide-fade">
            <div v-if="loadingPage" class="loadingbackground" style="margin-top:-30px"></div>
        </transition>
        <el-row style="width:100%;margin-top:30px;" type="flex" justify="center">
            <el-col style="width:100%;">
                <el-row style="width:100%;margin-bottom:30px;" type="flex" justify="start" align="middle">
                    <el-col style="width:100%">
                        <div class="user-img border" style="margin-left:55%;width:150px;height:150px;">
                            <img class="user-img img-border" :src="tagImg+'&Rand=' + Math.random()" alt="头像" style="width:150px;height:150px;" />
                        </div>
                    </el-col>
                    <el-col style="font-size:32px;color:#262626;margin-left:-2%">
                        <el-row>
                            #{{this.$route.params.id}}
                        </el-row>
                        <el-row style="font-size:18px;margin-top:10px;margin-left:15px">
                            {{this.addCommas(this.followNum)}} 人关注
                        </el-row>

                    </el-col>
                    <el-col style="margin-left:0%">
                        <el-button round style="width:30%;border-color:#db3579" @click="followClickHandler">{{this.followword}}</el-button>
                    </el-col>
                </el-row>
                <el-row style="width:100%" type="flex" justify="center">
                    <el-row :gutter="40" style="width:70%;margin:30px;">
                        <el-col :span="8">
                            <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.ID" v-for="item in items_col_1">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img :src="item.src+'&Rand=' + Math.random()" alt="hex" height="40px" width="40px" @click="jumpToUser(item.SenderID)">
                                        <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.ID)" alt="hex" width="100%">
                                </el-row>
                                <el-row style="margin-left:5%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="8">
                            <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.ID" v-for="item in items_col_2">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img :src="item.src+'&Rand=' + Math.random()" @click="jumpToUser(item.SenderID)" alt="hex" height="40px" width="40px">
                                        <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.ID)" alt="hex" width="100%">
                                </el-row>
                                <el-row style="margin-left:5%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="8">
                            <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.ID" v-for="item in items_col_3">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img :src="item.src+'&Rand=' + Math.random()" @click="jumpToUser(item.SenderID)" alt="hex" height="40px" width="40px">
                                        <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.ID)" alt="hex" width="100%">
                                </el-row>
                                <el-row style="margin-left:5%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                    </el-row>
                </el-row>
            </el-col>
        </el-row>

    </el-container>
</template>

<style scoped>
    .user-img {
        width: 150px;
        height: 150px;
        border-radius: 150px;
    }

    .border {
        position: relative;
        border: 1px solid transparent;
        border-radius: 150px;
        background: linear-gradient(45deg, #f9a357, #db3579 40%, #c73894 95%);
        background-clip: padding-box;
        padding: 6px;
    }

    .img-border {
        border: white solid;
        border-radius: 150px;
        border-width: 2px;
        margin: -2px;
    }

    .text {
        font-size: 14px;
    }

    .item {
        margin-bottom: 18px;
    }

    .clearfix:before,
    .clearfix:after {
        display: table;
        content: "";
    }

    .clearfix:after {
        clear: both
    }

    .box-card {
        width: 100%;
        margin-top: 20px;
    }

    .user-img {
        width: 100px;
        height: 100px;
        border-radius: 100px
    }

    div {
        margin-bottom: 1px;
    }

    .box-img {
        width: 500px;
        height: 300px;
    }


    .slide-fade-enter-active {
        transition: all .3s ease;
    }

    .slide-fade-leave-active {
        transition: all .8s cubic-bezier(1.0, 0.5, 0.8, 1.0);
    }

    .slide-fade-enter,
    .slide-fade-leave-to {
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
    }
</style>

<script>
    import Vue from 'vue'
    export default {
        data() {
            return {
                tagName: '#ExampleTag',
                followNum: 1430456,
                followState: false,
                followword: '关注',
                tableData: [],
                userName: 'Leonnnop',
                cards: [],
                height: [100, 200, 300, 400],
                imgValue: 0,
                items_col_1: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    }
                ],
                items_col_2: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    }
                ],
                items_col_3: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    }
                ],
                items_col_4: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        Content: 'test 文案 阿拉拉。',
                        LikeNum: 5
                    }
                ],
                loadingPage: true,
                tagImg: ''
            }
        },

        created() {
            this.axios.get('http://192.168.43.249:54468/api/Moment_Tag/Followers?Page=1' + '&PageSize=10' +
                    '&TagContent=' +
                    this.$route.params.id + '&Email=' + this.$store.state.currentUserId)
                .then((response) => {
                    let totalMoments = response.data.m_Item1;
                    this.followNum = response.data.m_Item2;
                    ////
                    this.followState = response.data.m_Item3;
                    if (this.followState) {
                        this.followword = '已关注'
                    } else {
                        this.followword = '关注'
                    }
                    var index = 0;
                    totalMoments.forEach((element) => {
                        element.likeState = response.data.m_Item5[index]
                        if (element.likeState == 0) {
                            element.likeIMG = require('../image/like.png');
                        } else {
                            element.likeIMG = require('../image/unlike.png');
                        }
                        element.likeState = false;
                        element.userName = response.data.m_Item4[index]
                        index++;
                        element.src = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.SenderID +
                            '&type=2'
                        this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.ID +
                                '&type=1')
                            .then((response) => {
                                this.tagImg = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                                    response.data[0];
                                Vue.set(element, 'contentSrc',
                                    'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                                    response.data[0]);
                            })
                    });
                    let momentNum = totalMoments.length;
                    this.items_col_1 = totalMoments.slice(0, Math.floor(momentNum / 3));
                    this.items_col_2 = totalMoments.slice(Math.floor(momentNum / 3), Math.floor(2 * momentNum /
                        3));
                    this.items_col_3 = totalMoments.slice(Math.floor(2 * momentNum / 3));
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        methods: {
            jumpToMoment: function (momentId) {
                this.$router.push('/main/momentDetail/' + momentId);
            },
            followClickHandler() {
                this.axios.put('http://192.168.43.249:54468/api/Follow_Tag/FollowTag?Email=' + this.$store.state.currentUserId +
                        '&tag=' + this.$route.params.id)
                    .then((response) => {
                        if (response.data) {
                            this.followState = !this.followState
                            if (this.followState) {
                                this.followNum++
                                    this.followword = '已关注'
                            } else {
                                this.followNum--
                                    this.followword = '关注'
                            }
                        } else {
                            this.$message.error('服务器内部错误。请重试。');

                        }
                    })

            },
            messageWebsocketHandler(path, state) {
                // 0 关注 1 点赞 2 评论 3 转发 4 私信
                window.ws.send('/' + path + ' ' + state);
            },
            addCommas(val) {
                var aIntNum = val.toString().split(".");
                if (aIntNum[0].length >= 5) {
                    aIntNum[0] = aIntNum[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (aIntNum[1] && aIntNum[1] >= 5) {
                    aIntNum[1] = aIntNum[1] ? aIntNum[1].replace(/\B(?=(\d{3})+(?!\d))/g, " ") : " ";
                }
                return aIntNum.join(".");
            },
            handleLikeClick(item) {
                this.axios.put('http://192.168.43.249:54468/api/DiscoverMoment/UpdateLiking?email=' + this.$store.state
                    .currentUserId +
                    '&moment_id=' + item.ID
                ).then((response) => {
                    console.log(item.LikeState);

                    item.LikeState = !item.LikeState;
                    console.log(item.LikeState);

                    if (item.LikeState == true) {
                        item.likeIMG = require('../image/like.png');
                        item.LikeNum++;
                        this.messageWebsocketHandler(item.SenderID, 1);
                    } else {
                        item.likeIMG = require('../image/unlike.png');
                        item.LikeNum--;
                    }
                })
            },
            jumpToUser: function (ID) {
                this.$router.push('/main/personalpage/' + ID);
            },
            getNaturalWidth(id) {
                var image = new Image()
                let img = document.getElementById(id)
                image.src = img.src
                var naturalWidth = image.width
                return naturalWidth
            },
            rnd(id, start, end) {
                console.log(this.getNaturalWidth(id))
                console.log(id)
                return Math.floor(Math.random() * (end - start) + start);
            },
            getSrc(src) {
                console.log(src);
                return src;
            },
            handleCancelClick(index) {
                this.$confirm('您觉得这条消息很无聊嘛？qwq？', '提示', {
                    confirmButtonContent: '是的，别再给我看相关的谢谢!',
                    cancelButtonContent: '点错了',
                    type: 'info'
                }).then(() => {
                    var length = this.cards.length;
                    for (var i = 0; i < length; i++) {
                        if (this.cards[i].index == index) {
                            this.cards.splice(i, 1);
                        }
                    }
                    this.$message({
                        type: 'success',
                        message: '删除成功!'
                    });
                }).catch(() => {
                    this.$message({
                        type: 'info',
                        message: '已取消删除'
                    });
                });

            }
        },

        beforeRouteLeave(to, from, next) {
            this.cards = [];
            this.tableData = [];
            next();
        },
        mounted: function () {
            this.$nextTick(function () {
                var self = this;
                setTimeout(function () {
                    self.loadingPage = false;
                }, 2000);
                window.scroll(0, 0);

            })
        },
        beforeRouteEnter(from, to, next) {
            next(vm => {
                vm.$nextTick(function () {
                    var self = vm;

                    setTimeout(function () {
                        self.loadingPage = false;
                    }, 1500)

                })
            })
        }
    }
</script>