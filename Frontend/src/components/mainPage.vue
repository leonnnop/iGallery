<template>
  <el-container style="height: 950px; border: 1px solid #eee">
    <el-col>
      <el-row type="flex" id="topBar" align="middle" :class="navBarFixed == true ? 'navBarWrap' :''" style="background-color:white; width: 100%;font-size: 18px; height:50px;">
        <el-col :span="1" :offset="1">
          <i class="el-icon-edit"></i>
        </el-col>
        <el-col :span="3">
          <span>iGallery</span>
        </el-col>
        <el-col :span="6">
          <el-menu :default-active="topBarActiveIndex" class="el-menu-demo" mode="horizontal" @select="handleTopBarSelect">
            <el-menu-item index="user">个人中心</el-menu-item>
            <el-submenu index="recommend">
              <template slot="title">推荐</template>
              <el-menu-item index="blogs">博文</el-menu-item>
              <el-menu-item index="users">用户</el-menu-item>
              <el-menu-item index="topic">话题</el-menu-item>
            </el-submenu>
            <el-menu-item index="leaderBoard">排行榜</el-menu-item>
          </el-menu>
        </el-col>

        <el-col :span="5" :offset="2">
          <el-input placeholder="请输入搜索内容" v-model="searchInput" clearable> </el-input>
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button round>搜索</el-button>
        </el-col>
        <el-col :span="1">
          <i class="el-icon-bell"></i>
        </el-col>
        <el-col :span="1">
          <i class="el-icon-service"></i>
        </el-col>
      </el-row>


      <el-row type="flex" justify="center">
        <el-col style="width:100%;height:800px;">
          <router-view></router-view>
        </el-col>
      </el-row>
    </el-col>

  </el-container>
</template>

<style scoped>
  .navBarWrap {
    position: fixed;
    top: 0;
    z-index: 999;
  }
</style>

<script>
  export default {
    data() {
      return {
        navBarFixed: false,
        searchInput: '',
        topBarActiveIndex: '1',
      }
    },
    mounted() {
      window.addEventListener('scroll', this.handleScroll)
    },
    destroyed() {
      window.removeEventListener('scroll', this.handleScroll)
    },
    methods: {
      handleTopBarSelect(key, keyPath) {
        console.log('/' + key);

        this.$router.push('/main/' + key);

      },
      handleScroll() {
        var scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
        var offsetTop = document.querySelector('#topBar').offsetTop
        if (scrollTop > offsetTop) {
          this.navBarFixed = true
        } else {
          this.navBarFixed = false
        }
      },
    },

  };
</script>