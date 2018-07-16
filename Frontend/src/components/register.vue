<template>
    <el-container id="background">
        <el-row :gutter="40" type="flex" :align="'middle'" style="height:100%;width:100%;">
            <el-col :span="7" :offset="6" id="imageCon">
                <img src="../image/example.png" alt="example pic" height="600">
            </el-col>
            <el-col :span="6" id="boxCon" style="width:375px">
                <el-row class="box-color-grey" style="height:500px;padding: 20px;">
                    <el-row type="flex" :justify="'center'" style="width:100%;">
                        <img src="../image/ins_ex.jpg" alt="example pic" height="50">
                    </el-row>
                    <el-row type="flex" :justify="'center'" style="margin-bottom:20px;">
                        <p>注册 iGallery，分享精彩视界</p>
                    </el-row>
                    <el-row type="flex" :justify="'center'">
                        <el-input placeholder="邮箱" v-model="email" clearable>
                        </el-input>
                    </el-row>
                    <el-row type="flex" :justify="'center'">
                        <el-input placeholder="用户名" v-model="username" clearable>
                        </el-input>
                    </el-row>
                    <el-row type="flex" :justify="'center'">
                        <el-input placeholder="密码" v-model="keyword" clearable>
                        </el-input>
                    </el-row>
                    <el-row type="flex" :justify="'center'" style="margin-top:20px;">
                        <el-button type="primary" plain size="medium" style="width:60%" @click="registerButtonClick">注册</el-button>
                    </el-row>
                    <el-row type="flex" :justify="'center'" style="margin-top:20px;color:#9b9b9b;font-size:12px;">
                        - 或 -
                    </el-row>
                    <el-row type="flex" :justify="'center'" style="margin-top:10px;color:#9b9b9b;font-size:12px;">
                        <el-button size="small" type="text">游客登陆</el-button>
                    </el-row>
                    <el-row type="flex" :justify="'center'" style="margin-top:5px;">
                        <p style="font-size:14px; margin:18px 20px 20px 20px;">
                            注册即表示你同意接受我们的条款、数据使用政策和 Cookie 政策 。
                        </p>
                    </el-row>


                </el-row>
                <el-row type="flex" :align="'middle'" :justify="'center'" class="box-color-grey" style="height:80px;font-size:14px;">
                    有帐户了？
                    <el-button type="text" @click="jumpLogIn">点我登陆</el-button>

                </el-row>
            </el-col>
        </el-row>
    </el-container>
</template>

<style scoped>
    .el-input {
        width: 250px;
        margin-top: 5px;
    }

    .box-color-grey {
        border-color: #e6e6e6;
        border-style: solid;
        border-width: thin;
        margin-bottom: 10px;
        background-color: white;
        /* padding: 20px; */
    }

    #background {
        background-color: #fafafa;
        height: 800px;
        width: 100%;
    }

    .text {
        font-size: 14px;
        color: #fafafa;
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
                tableData: [],
                email: '',
                username: '',
                keyword: '',
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
            isEmail(str) {
                var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
                return reg.test(str);
            },
            jumpLogIn() {
                this.$router.push('/login');
            },
            resultHandler(result) {
                if (result) {
                    this.$message({
                        message: '注册成功！将在三秒后跳转至登录界面！',
                        type: 'success'
                    });
                    setTimeout(jumpLogIn, 1000);
                } else {
                    this.$message.error('注册失败！请重试！');
                }
            },
            registerButtonClick() {
                if (this.isEmail(this.email)) {
                    this.axios.post('/user', {
                            username: this.username,
                            keyword: this.keyword,
                            email: this.email
                        })
                        .then((response) => {
                            let result = response.data.result;
                        })
                        .catch();
                } else {
                    this.$message.error('邮箱格式有误！请重新输入！');
                }
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