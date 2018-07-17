<template>
  <el-container style="height: 950px; border: 1px solid #eee">
    <el-col>
      <el-row type="flex" id="topBar" align="middle" :class="navBarFixed == true ? 'navBarWrap' :''" style="background-color:white; width: 100%;font-size: 18px; height:55px; padding-top:5px">
        <el-col :span="1" :offset="1">
          <img src="../image/logo.png" alt="logo" height="30">
        </el-col>
        <el-col :span="3">
          <!-- <span>iGallery</span> -->
          <img src="../image/iga_exa.png" alt="example pic" height="35">
        </el-col>
        <el-col :span="4">
          <el-menu :default-active="topBarActiveIndex" class="el-menu-demo" mode="horizontal" @select="handleTopBarSelect" style="margin-top:-12px">
            <el-menu-item index="user">首页</el-menu-item>
            <!-- <el-submenu index="recommend">
              <template slot="title">个人主页</template>
              <el-menu-item index="blogs">博文</el-menu-item>
              <el-menu-item index="users">用户</el-menu-item>
              <el-menu-item index="topic">话题</el-menu-item>
            </el-submenu> -->
            <el-menu-item index="blogs">我的主页</el-menu-item>

            <el-menu-item index="photoWall">发现</el-menu-item>
          </el-menu>
        </el-col>

        <el-col :span="5" :offset="4">
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