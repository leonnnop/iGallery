<template>
  <el-container style="height: 1200px; border: 1px solid #eee;background-color;background-color: #fafafa;">
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
          <img src="../image/send.png" alt="sendMoment" @click="sendMomentInit" >
            
        </el-col>
      </el-row>


      <el-row type="flex" justify="center" style="margin-top:30px">
        <el-col style="width:100%;height:800px;" :class="navBarFixed == true ? 'mainContentScroll' :''">
          
          <router-view></router-view>
          <el-dialog title="发布" :visible.sync="sendMomentVisible" width="40%" custom-class="send" :show-close="false" top="50px">
              <el-row>
                <el-col :span="4" :offset="0"><img src="../image/a.jpg" alt="headImg" style="width:80px;height:80px;border-radius:80px;"></el-col>
                <el-col :span="18" :offset="1">
                  <div class="edit">
                    <el-row type="flex" justify="center" align="middle">
                      
                      <el-col :span="24" style="margin:3% 0 5% 6%" v-show="showUploadArea" v-if="showUpload">
                        <el-upload
                          
                          action="https://jsonplaceholder.typicode.com/posts/"
                          list-type="picture-card"
                          :on-remove="handleRemove" 
                          :file-list="uploadImgs"
                          :before-upload="beforeUpload"
                          :on-change="uploadOnChange"
                          :on-success="uploadOnSuccess"
                          :on-error="uploadOnError"
                          :on-progress="uploadOnProgress"
                          :show-file-list="true"
                          :limit="9"
                          :multiple="true"
                          class="upload">
                          <i class="el-icon-plus"></i>
                        </el-upload>
                        <span v-if="showReminder">请选择图片</span>
                        
                      </el-col>
                      <el-col v-show="showTextArea" :span="20" style="margin-top:5%;">
                        <el-input type="textarea" resize="none" :rows="16" placeholder="此刻的想法..." v-model="sendText"></el-input>
                      </el-col>
                    </el-row>
                    

                  </div>
                </el-col>
              </el-row>
              <el-row type="flex" justify="end" style="margin-top:10px" v-if="showNextBtn&&!showTextArea">
                <el-col :span="4" ><el-button type="primary" plain @click="sendNextHandler" v-if="showUploadArea">下一步</el-button></el-col>
              </el-row>
              <el-row type="flex" justify="end" style="margin-top:10px" v-if="!showUploadArea&&showTextArea">
                <el-col :span="4" ><el-button plain @click="sendLastHandler">上一步</el-button></el-col>
                <el-col :span="4"><el-button type="primary" plain @click="sendMomentHandler">完成</el-button></el-col>
              </el-row>
            </el-dialog>
        </el-col>
      </el-row>
    </el-col>

  </el-container>
</template>

<style>
  .navBarWrap {
    position: fixed;
    top: 0;
    z-index: 999;
  }
  .mainContentScroll {
    margin-top: 55px
  }
  .send{
   /* background: transparent; */
    height: 0;
    padding-bottom: 38%;
    position: relative;
    -webkit-box-shadow:0 0;
    box-shadow: 0 0;
  }
  .edit{
    
    width: 100%;
    height: 0;
    padding-bottom: 98%;
    overflow: hidden;
    border: 2px dashed #444;
  }
  .edit .el-upload-list--picture-card .el-upload-list__item,.el-upload--picture-card{
    height: 0;
    width: 30%;
    padding-bottom: 30%;
    position: relative;
  }
  .edit .el-upload-list--picture-card .el-upload-list__item img{
    position: absolute;
    left: 0;
    top: 0;
  }
  .el-upload--picture-card i{
    position: absolute;
    left: 36%;
    top: 36%;
  }
  
</style>

<script>
  export default {
    data() {
      return {
        navBarFixed: false,
        searchInput: '',
        topBarActiveIndex: '1',
        sendMomentVisible:false,
        dialogImageUrl: '',
        dialogVisible: false,
        showReminder:true,
        showNextBtn:false,
        showUpload:false,
        showUploadArea:true,
        showTextArea:false,
        sendText:'',
        uploadImgs2:[],
        uploadImgs:[
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
          // {name: 'pic1', url:'http://streetwill.co/uploads/post/photo/266/show_l3Qk6zzdADiMWz3c3sQXEGHIrgNBsF5L7Jahp0dN6kY.jpg'},
        ],
        ableToUpload:true

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
      sendMomentInit:function(){
        this.sendMomentVisible=true;
        this.showUpload=true;
      },
      sendNextHandler:function(){
        this.showUploadArea=false;
        this.showTextArea=true;
      },
      sendLastHandler:function(){
        this.showUploadArea=true;
        this.showTextArea=false;
      },
      sendMomentHandler:function(){
        console.log('————发布内容————');
        console.log(this.uploadImgs2);
        console.log(this.sendText);
        this.uploadImgs2=[];
        this.sendText='';
        this.sendMomentVisible=false;
        this.showUploadArea=true;
        this.showTextArea= false;
        this.showUpload=false;
        this.$message({
          message: '发布成功！',
          type: 'success'
        })
      },
      handleRemove(file, fileList) {
        if(!fileList.length){
          this.showReminder=true;
        }
      },
      beforeUpload:function(file){
        const size = file.size / 1024 / 1024 < 3;
        if (!size) {
          this.$message.error('上传图片大小不能超过 3MB!');
        }
        return size;
      },
      uploadOnProgress(e,file){//开始上传
        console.log('——————开始上传——————');
        console.log(e.percent,file)
      },
      uploadOnChange(file){
          console.log("——————————change——————————")
          // console.log(file)
          if(file.status == 'ready'){
              console.log("ready")
          }else if(file.status == 'fail'){
              this.$message.error("图片上传出错，请刷新重试！")
          }
      },
      uploadOnSuccess(e,file,fileList){//上传附件
          console.log("——————————success——————————")
          console.log(fileList);
          if(fileList.length){
            this.showReminder=false;
            this.showNextBtn=true;
          }
          this.uploadImgs2=fileList;
          this.$message.success("上传成功")

          
      },
      uploadOnError(e,file){
          console.log("——————————error——————————")
          console.log(e)
      }

    },

  };
</script>