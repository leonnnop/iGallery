<template>
    <el-container>
        <el-row style="width:100%;margin-top:30px;" type="flex" justify="center">
            <el-col style="width:100%;">
                <el-row style="width:100%;margin-bottom:30px;" type="flex" justify="start" align="middle">
                    <el-col style="width:100%">
                        <div class="user-img border" style="margin-left:55%;width:150px;height:150px;">
                            <img class="user-img img-border" src="../image/a.jpg" alt="头像" style="width:150px;height:150px;" />
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
                        <!-- <el-button type="primary" plain style="width:50%">主要按钮</el-button> -->
                    </el-col>
                </el-row>
                <el-row style="width:100%" type="flex" justify="center">
                    <el-row :gutter="40" style="width:70%;margin:30px;">
                        <el-col :span="8">
                            <el-card class="box-card" :key="item" v-for="item in items_col_1">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                                        <span style="margin-left:-30%; font-size:18px">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img src="../image/hex.jpeg" alt="hex" width="300px;" style="margin:-20px">
                                </el-row>
                                <el-row style="margin-top:10%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="8">
                            <el-card class="box-card" :key="item" v-for="item in items_col_2">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                                        <span style="margin-left:-30%; font-size:18px">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img src="../image/hex.jpeg" alt="hex" width="300px;" style="margin:-20px">
                                </el-row>
                                <el-row style="margin-top:10%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="8">
                            <el-card class="box-card" :key="item" v-for="item in items_col_3">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                                        <span style="margin-left:-30%; font-size:18px">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img src="../image/hex.jpeg" alt="hex" width="300px;" style="margin:-20px">
                                </el-row>
                                <el-row style="margin-top:10%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <!-- <el-col :span="6">
                            <el-card class="box-card" :key="item" v-for="item in items_col_4">
                                <div slot="header" class="clearfix">
                                    <el-row type="flex" align="middle" justify="space-between">
                                        <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                                        <span style="margin-left:-30%; font-size:18px">{{item.userName}}</span>
                                        <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
                                    </el-row>
                                </div>
                                <el-row>
                                    <img src="../image/hex.jpeg" alt="hex" width="280px;" style="margin:-20px">
                                </el-row>
                                <el-row style="margin-top:10%">
                                    {{item.Content}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col> -->
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
        /* just to show box-shadow still works fine */
        /* box-shadow: 0 3px 9px black, inset 0 0 9px white; */
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
</style>

<script>
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
            }
        },

        created() {
            this.axios.get('http://10.0.1.8:54468/api/Moment_Tag/Followers?Page=1' + '&PageSize=10' + '&TagContent=' +
                    this.$route.params.id)
                .then((response) => {
                    let totalMoments = response.data.m_Item1;
                    this.followNum = response.data.m_Item2;
                    totalMoments.forEach(element => {
                        element.likeIMG = require('../image/unlike.png');
                        // 更新后删除
                        element.likeState = false;
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
            followClickHandler() {
                this.axios.put()
                    .then((response) => {

                    })
                this.followState = !this.followState
                if (this.followState) {
                    this.followNum++
                        this.followword = '已关注'
                } else {
                    this.followNum--
                        this.followword = '关注'
                }
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
                console.log(item.LikeState)

                item.LikeState = !item.LikeState
                console.log(item.LikeState)

                if (item.LikeState == true) {
                    item.likeIMG = require('../image/like.png');
                    item.LikeNum++;
                } else {
                    item.likeIMG = require('../image/unlike.png');
                    item.LikeNum--;
                }
                // console.log(item)
                this.axios.put('http://10.0.1.8:54468/api/DiscoverMoment/UpdateLiking?email=' + this.$store.state.currentUserId +
                    '&moment_id=' + item.MomentID
                    // , {
                    //     email: this.$store.state.currentUserId,
                    //     moment_id: item.MomentId
                    //   }
                )
            },
            getNaturalWidth(id) {
                var image = new Image()
                // image.src = img.src
                let img = document.getElementById(id)
                image.src = img.src
                var naturalWidth = image.width
                return naturalWidth
            },
            rnd(id, start, end) {
                // this.imgValue = this.imgValue + 1
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
                    // for 循环
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

        beforeRouteEnter(to, from, next) {
            // 处理无法访问的情况
            next((vm) => {
                vm.axios.get('http://192.168.0.37:5000/feed/sss')
                    .then((response) => {
                        for (let index = 0; index < response.length; index++) {
                            let responce = response[index].data;
                            let card = {};
                            let author = responce.author;
                            card.index = index;
                            card.authorName = author.name;
                            card.authorURL = author.url;
                            card.url = responce.url;
                            card.title = responce.title;
                            card.excerpt = responce.excerpt;

                            this.cards.push(card);
                        }
                    })
                    .catch();
            });
        },
        beforeRouteLeave(to, from, next) {
            this.cards = [];
            this.tableData = [];
            next();
        }
    }
</script>