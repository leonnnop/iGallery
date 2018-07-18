<template>
  <el-container>
    <el-row style="width:100%" type="flex" justify="left">
      <el-col class="fixed">
        <el-row class="box-card" style="height:500px;width:280px;margin-left:64%;">
          <!-- <div slot="header" class="clearfix"> -->
          <el-row class="usr" type="flex" align="middle">
            <el-col>
              <div class="user-img border">
                <img class="user-img img-border" src="../image/a.jpg" alt="头像" />
              </div>
            </el-col>
            <!-- <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i> -->
            <el-col style="margin-left:-70%;width:50%;overflow:hidden">
              <span style="font-size:16px; font-weight:bold">{{this.userName}}</span>
              <span style="font-size:14px; color:#999999">{{this.userName}}</span>
            </el-col>
          </el-row>
          <!-- </div> -->
          <hr class="hr" />
          <!-- <div style="font-weight:bold;font-size: 10px;">简介：</div>
          <div>这个用户很懒，</div>
          <div>但能吃。</div>
          <div> </div>
          <div> </div> -->
          <el-row style="color:#999999;font-weight:bold;margin-bottom:10px;width:280px;">
            关注用户
          </el-row>
          <!-- <div class="inset-shadow"> -->
          <el-row style="overflow-y:auto;overflow-x:hidden;height:340px;width:260px;margin-bottom:12px;">
            <el-row :key="item" v-for="item in followings" type="flex" align="middle" style="margin-bottom:12px">
              <el-col>
                <div class="user-img border">
                  <img class="user-img img-border" src="../image/a.jpg" alt="头像" />
                </div>
              </el-col>
              <!-- <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i> -->
              <el-col style="margin-left:-70%;width:50%;overflow:hidden">
                <span style="font-size:16px; font-weight:bold">{{item.userName}}</span>
                <span style="font-size:14px; color:#999999">{{item.userName}}</span>
              </el-col>
            </el-row>
          </el-row>
          <!-- </div> -->
          <hr class="hr outset-shadow" />

          <el-row style="font-size:14px;margin-bottom:5px">
            关注 iGallery，分享精彩视界
          </el-row>
          <el-row style="font-size:14px">
            © 2018 IGALLERY Uni
          </el-row>
        </el-row>
      </el-col>

      <el-row style="width:600px;margin-left:18%;" type="flex" justify="center">
        <el-col>
          <el-row v-for="card in cards" :key="card.name" class="box-color-grey">
            <el-row>
              <el-row class="usr" type="flex" align="middle" style="padding:7px 15px">
                <el-col>
                  <div class="small-user-img small-border">
                    <img class="small-user-img small-img-border" src="../image/a.jpg" alt="头像" />
                  </div>
                </el-col>
                <!-- <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i> -->
                <el-col style="margin-left:-91%;width:20%;overflow:hidden;margin-top:5px">
                  <span style="font-size:16px; font-weight:bold">{{card.userName}}</span>
                  <span style="font-size:14px; color:#999999">{{card.userName}}</span>
                </el-col>
              </el-row>
            </el-row>
            <el-row>
              <img :src="card.src" width="600px" class="box-img" alt="头像" />
            </el-row>

            <el-row>
              <img src="../image/comment-unlike.png" alt="bottomlike" height="30px" width="30px" style="margin-left:2%">
              <img src="../image/uncollect.png" alt="collect" height="30px" width="30px" style="margin-left:2%">
              <img src="../image/forward.png" alt="forward" height="30px" width="30px" style="margin-left:2%">
              <img src="../image/user-more.png" alt="more" height="30px" width="30px" style="margin-right:4%;float:right">
            </el-row>

            <el-col style="padding:5px 15px">
              <!-- 简介 -->
              <el-row style="font-size:14px">
                {{card.bio}}
              </el-row>
              <!-- tag -->
              <el-row>

              </el-row>
            </el-col>

          </el-row>
        </el-col>
      </el-row>

    </el-row>
  </el-container>
</template>

<style scoped>
  .box-color-grey {
    border-color: #e6e6e6;
    border-style: solid;
    border-width: thin;
    margin-bottom: 60px;
    background-color: white;
    /* padding: 20px; */
  }

  .inset-shadow {
    /* border-radius: 50%; */
    /* width: 180px; */
    /* height: 180px; */
    overflow-x: hidden;
    overflow-y: scroll;
    position: relative;
  }

  .inset-shadow:after {
    position: absolute;
    content: '';
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    box-shadow: 0 0 30px 10px rgba(255, 255, 255, .7) inset;
  }

  .hr {
    border-top: 1px solid #efefef;
    height: 0px;
    margin: 10px 0 12px;
    width: 100%;
    padding: 0;
  }

  .border {
    position: relative;
    border: 1px solid transparent;
    border-radius: 50px;
    background: linear-gradient(45deg, #f9a357, #db3579 40%, #c73894 95%);
    background-clip: padding-box;
    padding: 4px;
    /* just to show box-shadow still works fine */
    /* box-shadow: 0 3px 9px black, inset 0 0 9px white; */
  }

  .img-border {
    border: white solid;
    border-radius: 50px;
    border-width: 2px;
    margin: -2px;
  }

  .small-border {
    position: relative;
    border: 1px solid transparent;
    border-radius: 30px;
    background: linear-gradient(45deg, #f9a357, #db3579 40%, #c73894 95%);
    background-clip: padding-box;
    padding: 3px;
    /* just to show box-shadow still works fine */
    /* box-shadow: 0 3px 9px black, inset 0 0 9px white; */
  }

  .small-img-border {
    border: white solid;
    border-radius: 30px;
    border-width: 1px;
    margin: -1px;
  }

  .fixed {
    position: fixed;
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

  .user-img {
    width: 50px;
    height: 50px;
    border-radius: 50px
  }

  .small-user-img {
    width: 30px;
    height: 30px;
    border-radius: 30px
  }

  div {
    margin-bottom: 1px;
  }

  .box-img {
    width: 600px;
  }
</style>

<script>
  export default {
    data() {
      return {
        followings: [{
            userName: 'leonnnop'

          },
          {
            userName: 'leonnnop'

          },
          {
            userName: 'leonnnop'

          },
          {
            userName: 'leonnnop'

          },
          {
            userName: 'leonnnop'

          },
          {
            userName: 'leonnnop'

          },
        ],
        tableData: [{
          date: '2016-05-02',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }, {
          date: '2016-05-04',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1517 弄'
        }, {
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        }, {
          date: '2016-05-03',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1516 弄'
        }],

        userName: 'Leonnnop',

        cards: [{
            id: 1,
            index: 0,
            title: '',
            bio: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。',
            userName: 'leonnnop',
            src: require('../image/ins1.png')
          },
          {
            id: 2,
            index: 1,
            title: '',
            bio: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。',
            userName: 'leonnnop',
            src: require('../image/ins2.png')
          },
          {
            id: 3,
            index: 2,
            title: '',
            bio: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。',
            userName: 'leonnnop',
            src: require('../image/ins3.png')
          },
        ]
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