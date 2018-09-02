<template>
  <el-container>
    <transition name="slide-fade">
      <div v-if="loadingPage" class="loadingbackground" style="margin-top:-30px"></div>
    </transition>
    <el-row style="width:100%" type="flex" justify="center">
      <el-row :gutter="30" style="width:90%;margin:30px;">
        <el-col :span="6">
          <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.MomentID" v-for="item in items_col_1">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img :src="item.src+'&Rand=' + Math.random()" @click="jumpToUser(item.SenderID)" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.Username}}</span>
                <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
              </el-row>
            </div>
            <el-row>
              <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.MomentID)" alt="hex" width="100%">
            </el-row>
            <el-row style="margin-left:5%">
              {{item.Content}}
            </el-row>
            <el-row type="flex" align="middle" style="color:#a2a2a2;margin:5%">
              <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.MomentID" v-for="item in items_col_2">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img :src="item.src+'&Rand=' + Math.random()" @click="jumpToUser(item.SenderID)" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.Username}}</span>
                <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
              </el-row>
            </div>
            <el-row>
              <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.MomentID)" alt="hex" width="100%">
            </el-row>
            <el-row style="margin-left:5%">
              {{item.Content}}
            </el-row>
            <el-row type="flex" align="middle" style="color:#a2a2a2;margin:5%">
              <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.MomentID" v-for="item in items_col_3">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img :src="item.src+'&Rand=' + Math.random()" @click="jumpToUser(item.SenderID)" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.Username}}</span>
                <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
              </el-row>
            </div>
            <el-row>
              <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.MomentID)" alt="hex" width="100%">
            </el-row>
            <el-row style="margin-left:5%">
              {{item.Content}}
            </el-row>
            <el-row type="flex" align="middle" style="color:#a2a2a2;margin:5%">
              <img src="../image/bluelike.png" alt="bottomlike" height="30px" width="30px" style="margin-right:5%"> {{item.LikeNum}}人喜欢
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card :body-style="{ padding: '0px' }" class="box-card" :key="item.MomentID" v-for="item in items_col_4">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img :src="item.src+'&Rand=' + Math.random()" @click="jumpToUser(item.SenderID)" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px" @click="jumpToUser(item.SenderID)">{{item.Username}}</span>
                <img :src="item.likeIMG" @click="handleLikeClick(item)" alt="like" height="30px" width="30px" style="float: right; padding: 3px 0">
              </el-row>
            </div>
            <el-row style="">
              <img :src="item.contentSrc+'&Rand=' + Math.random()" @click="jumpToMoment(item.MomentID)" alt="hex" width="100%">
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

  </el-container>
</template>

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

  /* .el-card__body {
    padding: 0px;
  } */
</style>

<script>
  import Vue from 'vue'
  export default {
    data() {
      return {
        loadingPage: true,

        tableData: [],
        Username: 'Leonnnop',
        cards: [],
        height: [100, 200, 300, 400],
        imgValue: 0,
        items_col_1: [{
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          }
        ],
        items_col_2: [{
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          }
        ],
        items_col_3: [{
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          }
        ],
        items_col_4: [{
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          },
          {
            Username: 'Leonnnop',
            likeIMG: require('../image/unlike.png'),
            LikeState: false,
            Content: 'test 文案 阿拉拉。',
            LikeNum: 5
          }
        ],
      }
    },

    created() {
      this.axios.get('http://192.168.43.249:54468/api/DiscoverMoment/GetRankingMoments?email=' + this.$store.state.currentUserId)
        .then((response) => {
          let totalMoments = response.data;
          totalMoments.forEach(element => {
            console.log(element.LikeState)
            if (element.LikeState == 'true') {
              element.likeIMG = require('../image/like.png');
              element.LikeState = true
            } else {
              element.likeIMG = require('../image/unlike.png');
              element.LikeState = false
            }

            element.src = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.SenderID +
              '&type=2'
            this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.MomentID +
                '&type=1')
              .then((response) => {
                // this.axios.get('http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                //         response.data[0], {
                //             responseType: 'blob'
                //         })
                //     .then((response) => {
                //         element.contentSrc = response.data
                //     })
                Vue.set(element, 'contentSrc',
                  'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                  response.data[0]);
                // element.contentSrc = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                //     response.data[0]
              })
            // 更新后删除
            // element.LikeState = false;
          });
          let momentNum = totalMoments.length;
          console.log(momentNum)
          this.items_col_1 = totalMoments.slice(0, Math.floor(momentNum / 4));
          console.log(this.items_col_1)
          this.items_col_2 = totalMoments.slice(Math.floor(momentNum / 4), Math.floor(momentNum / 2));
          console.log(this.items_col_2)

          this.items_col_3 = totalMoments.slice(Math.floor(momentNum / 2), Math.floor(3 * momentNum / 4));
          console.log(this.items_col_3)

          this.items_col_4 = totalMoments.slice(Math.floor(3 * momentNum / 4));
          console.log(this.items_col_4)

        })
        .catch(function (error) {
          console.log(error);
        });
    },

    methods: {
      jumpToMoment(momentId) {
        // console.log(momentId);
        this.$router.push('/main/momentDetail/' + momentId);
      },
      jumpToUser: function (ID) {
        // console.log('ge')
        // if (ID == this.$store.state.currentUserId_ID) {
        //   this.$router.push('/main/personalpage/');
        // } else {
        this.$router.push('/main/personalpage/' + ID);
        // }
      },
      handleLikeClick(item) {

        // console.log(item)
        this.axios.put('http://192.168.43.249:54468/api/DiscoverMoment/UpdateLiking?email=' + this.$store.state.currentUserId +
          '&moment_id=' + item.MomentID
          // , {
          //     email: this.$store.state.currentUserId,
          //     moment_id: item.MomentId
          //   }
        ).then((response) => {
          console.log(item.LikeState)

          item.LikeState = !item.LikeState
          console.log(item.LikeState)

          if (item.LikeState == true) {
            item.likeIMG = require('../image/like.png');
            item.LikeNum++;
            this.messageWebsocketHandler(item.SenderID, 1)
          } else {
            item.likeIMG = require('../image/unlike.png');
            item.LikeNum--;
          }
        })
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
      messageWebsocketHandler(path, state) {
        // 0 关注 1 点赞 2 评论 3 转发 4 私信
        window.ws.send('/' + path + ' ' + state);
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
          }, 2000)
})
      })
    }
  }
</script>