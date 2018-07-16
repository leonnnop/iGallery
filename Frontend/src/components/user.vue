<template>
  <el-container>
    <el-row :gutter="20" style="width:100%">
      <el-col :span="8">
        <el-card class="box-card" style="height:300px;">
          <div slot="header" class="clearfix">
            <img class="user-img" src="../image/a.jpg" alt="头像" />
            <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i>
            <span style="font-size:18px;">{{this.userName}}</span>
          </div>

          <div style="font-weight:bold;font-size: 10px;">简介：</div>
          <div>这个用户很懒，</div>
          <div>但能吃。</div>
          <div> </div>
          <div> </div>
        </el-card>
      </el-col>

      <el-col :span="16">
        <el-card v-for="card in cards" :key="card.name" class="box-card">

          <!-- <img :src="getSrc(card.src)" alt="头像" /> -->
          <img v-if="card.index == 0" src="../image/user_img.jpg" class="box-img" alt="头像" />
          <img v-if="card.index == 1" src="../image/gaojin_ciyun.png" class="box-img" alt="头像" />
          <img v-if="card.index == 2" src="../image/gaojin_radar.png" style="width:300px; float:right;" class="box-img" alt="头像" />

        </el-card>

      </el-col>
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
            src: '../image/user_img.jpg'
          },
          {
            id: 2,
            index: 1,
            title: '',
            src: '../image/gaojin_ciyun.png'
          },
          {
            id: 3,
            index: 2,
            title: '',
            src: '../image/gaojin_ciyun.png'
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
