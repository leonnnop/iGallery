<template>
  <el-container>
    <el-col>
      <el-table :data="tableData" stripe style="width: 100%; margin-top:20px;">
        <el-table-column prop="date" label="排名" width="180">
        </el-table-column>
        <el-table-column prop="name" label="用户名" width="180">
        </el-table-column>
        <el-table-column prop="address" label="简介">
        </el-table-column>
      </el-table>
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
</style>

<script scoped>
  export default {
    data() {
      return {
        to_Route: '',
        tableData: [],
        cards: [{
            id: 1,
            index: 0
          },
          {
            id: 2,
            index: 1
          }
        ]
      }
    },

    methods: {
      myrefresh(path) {
        console.log('假刷新',path)
        this.$router.push(''+path);
      }
    },
    created() {
      // this.myrefresh(this.to_Route);
    },
    beforeRouteEnter(to, from, next) {
      // 处理无法访问的情况
      next((vm) => {
        // vm.to_Route = from.path;
        console.log(from)
        vm.myrefresh(from.path)
        // vm.axios.get('http://192.168.0.37:5000/feed/sss')
        //   .then((response) => {
        //     for (let index = 0; index < response.length; index++) {
        //       let responce = response[index].data;
        //       let card = {};
        //       let author = responce.author;
        //       card.index = index;
        //       card.authorName = author.name;
        //       card.authorURL = author.url;
        //       card.url = responce.url;
        //       card.title = responce.title;
        //       card.excerpt = responce.excerpt;

        //       this.cards.push(card);
        //     }
        //   })
        //   .catch();
        // vm.myrefresh();
      });
    },
    beforeRouteLeave(to, from, next) {
      this.cards = [];
      this.tableData = [];
      next();
    }
  }
</script>