<template>
  <el-container>
    <el-col>
      <el-card v-for="card in cards" :key="card.name" class="box-card">
        <div slot="header" class="clearfix">
          <a :href="excerptClick(card)">{{card.title}}</a>
          <el-button style="float: right; padding: 3px 0; color:#4B5CC4;" type="text" @click="handleCancelClick(card.index)">X</el-button>
        </div>
        <!-- <div v-for="o in 4" :key="o" class="text item">
          {{'列表内容 ' + o }}
        </div> -->
        <a :href="authorNameClick(card)" class="authorName">{{card.authorName}}</a>
        <!-- <div>{{card.authorURL}}</div>
        <div>{{card.url}}</div> -->
        <div></div>
        <a :href="excerptClick(card)" class="excerpt">{{'&nbsp;&nbsp;&nbsp;&nbsp;'+card.excerpt}}</a>
      </el-card>

    </el-col>
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

  .authorName {
    font-size: 18px;
    font-weight: bold;
  }

  a {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: left;
    color: #2c3e50;
    text-decoration: none;
    /* }
  { */
    overflow: hidden;

    text-overflow: ellipsis;

    display: -webkit-box;

    -webkit-box-orient: vertical;

    -webkit-line-clamp: 7;
  }

</style>

<script>
  export default {
    data() {
      return {
        cards: [
          //     {
          //     id: 1,
          //     index: 0,
          //     authorName: '爬虫',
          //     authorURL: 'http',
          //     url: 'https://www.baidu.com',
          //     title: 'A title.',
          //     excerpt: 'A excerpt.',
          //   },
          //   {
          //     id: 2,
          //     index: 1
          //   }
        ]
      }
    },

    methods: {
      authorNameClick(card) {
        return card.authorURL;
      },
      excerptClick(card) {
        return card.url;
      },
      handleCancelClick(index) {
        this.$confirm('您觉得这条消息很无聊嘛？qwq？', '提示', {
          confirmButtonText: '是的，别再给我看相关的谢谢!',
          cancelButtonText: '点错了',
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

    beforeRouteEnter(to, from, next) {
      // 处理无法访问的情况
      next((vm) => {
        vm.axios.get('http://192.168.0.37:8000/api/feed/sss')
          .then((response) => {
            let responce = response.data;
            for (let index = 0; index < responce.length; index++) {
              let curData = responce[index];
              let card = {};
              let author = curData.author;
              card.index = index;
              card.authorName = author.name;
              card.authorURL = author.url;
              card.url = curData.url;
              card.title = curData.title;
              card.excerpt = curData.excerpt;

              console.log(vm.cards);
              vm.cards.push(card);
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
