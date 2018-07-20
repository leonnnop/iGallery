<template>
  <el-container>
    <el-row style="width:100%" type="flex" justify="center">
      <el-row :gutter="30" style="width:90%;margin:30px;">
        <el-col :span="6">
          <el-card class="box-card" :key="item" v-for="item in items_col_1">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px">{{item.Username}}</span>
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
        <el-col :span="6">
          <el-card class="box-card" :key="item" v-for="item in items_col_2">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px">{{item.Username}}</span>
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
        <el-col :span="6">
          <el-card class="box-card" :key="item" v-for="item in items_col_3">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px">{{item.Username}}</span>
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
        <el-col :span="6">
          <el-card class="box-card" :key="item" v-for="item in items_col_4">
            <div slot="header" class="clearfix">
              <el-row type="flex" align="middle" justify="space-between">
                <img src="../image/hex.jpeg" alt="hex" height="40px" width="40px">
                <span style="margin-left:-30%; font-size:18px">{{item.Username}}</span>
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
      </el-row>
    </el-row>

  </el-container>
</template>

<style scoped>
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
      this.axios.get('http://10.0.1.8:54468/api/Moment/GetRankingMoments?user_id=' + this.store.state.currentUserId)
        .then((response) => {
          let totalMoments = response.data.User_Moment;
          totalMoments.forEach(element => {
            element.likeIMG = require('../image/unlike.png');
            // 更新后删除
            // element.LikeState = false;
          });
          let momentNum = totalMoments.length;
          this.items_col_1 = totalMoments.slice(0, Math.floor(momentNum / 4));
          this.items_col_2 = totalMoments.slice(Math.floor(momentNum / 4) + 1, Math.floor(momentNum / 2));
          this.items_col_3 = totalMoments.slice(Math.floor(momentNum / 2) + 1, Math.floor(3 * momentNum / 4));
          this.items_col_4 = totalMoments.slice(Math.floor(3 * momentNum / 4) + 1);
        })
        .catch(function (error) {
          console.log(error);
        });
    },

    methods: {
      handleLikeClick(item) {
        item.LikeState = !item.LikeState
        if (item.LikeState) {
          item.likeIMG = require('../image/like.png');
          item.likeNum++;
          this.axios.put('http://10.0.1.8:54468/api/Moment/UpdateLiking', {
            user_id: this.store.state.currentUserId,
            moment_id: item.moment_id
          })
        } else {
          item.likeIMG = require('../image/unlike.png');
          item.likeNum--;
          this.axios.put('http://10.0.1.8:54468/api/Moment/UpdateLiking', {
            user_id: this.store.state.currentUserId,
            moment_id: item.moment_id
          })
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