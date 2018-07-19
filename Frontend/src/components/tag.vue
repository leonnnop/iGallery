<template>
    <el-container>
        <el-row style="width:100%" type="flex" justify="center">
            <el-col style="width:100%">
                <el-row style="width:100%" type="flex" justify="start" align="middle">
                    <el-col style="width:100%">
                        <div class="user-img border" style="margin-left:12%">
                            <img class="user-img img-border" src="../image/a.jpg" alt="头像" />
                        </div>
                    </el-col>
                    <el-col style="font-size:26px;color:#262626;margin-left:-61%">
                        {{this.tagName}}
                    </el-col>
                </el-row>
                <el-row style="width:100%" type="flex" justify="center">
                    <el-row :gutter="35" style="width:85%;margin:30px;">
                        <el-col :span="6">
                            <el-card class="box-card" :key="item" v-for="item in items_col_1">
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
                                    {{item.text}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.likeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="6">
                            <el-card class="box-card" :key="item" v-for="item in items_col_2">
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
                                    {{item.text}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.likeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="6">
                            <el-card class="box-card" :key="item" v-for="item in items_col_3">
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
                                    {{item.text}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.likeNum}}人喜欢
                                </el-row>
                            </el-card>
                        </el-col>
                        <el-col :span="6">
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
                                    {{item.text}}
                                </el-row>
                                <el-row type="flex" align="middle" style="color:#a2a2a2;margin-top:5%">
                                    <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.likeNum}}人喜欢
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
        padding: 5px;
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
                tagName:'#ExampleTag',
                tableData: [],
                userName: 'Leonnnop',
                cards: [],
                height: [100, 200, 300, 400],
                imgValue: 0,
                items_col_1: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    }
                ],
                items_col_2: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    }
                ],
                items_col_3: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    }
                ],
                items_col_4: [{
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    },
                    {
                        userName: 'Leonnnop',
                        likeIMG: require('../image/unlike.png'),
                        likeState: false,
                        text: 'test 文案 阿拉拉。',
                        likeNum: 5
                    }
                ],
            }
        },

        created() {
            this.axios.get('http://10.0.1.61:51738/api/products/1')
                ///// 箭头函数会改变this的作用域
                .then((response) => {
                    this.tableData = [];
                    this.totalElements = response.data.totalElements;
                    let content = response.data.content;
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        methods: {
            handleLikeClick(item) {
                item.likeState = !item.likeState
                if (item.likeState) {
                    item.likeIMG = require('../image/like.png')
                    item.likeNum++
                } else {
                    item.likeIMG = require('../image/unlike.png')
                    item.likeNum--
                }
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
                    confirmButtonText: '是的，别再给我看相关的谢谢!',
                    cancelButtonText: '点错了',
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