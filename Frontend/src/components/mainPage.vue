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
            <el-menu-item index="personalpage">我的主页</el-menu-item>

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
          <el-dialog title="" :visible.sync="sendMomentVisible" width="50%" custom-class="send" :show-close="false" top="10px">
              <el-row>
                <el-col :span="3" :offset="0"><img src="../image/a.jpg" alt="headImg" style="width:80px;height:80px;border-radius:80px;"></el-col>
                <el-col :span="18" :offset="0">
                  <div class="sendContent">
                    <div class="edit"><div style="color:#555;margin:50px 0 20px 80px;font-size:16px;font-weight:bold">Leonnnop</div>
                      <el-row type="flex" justify="center" align="middle">
                        <el-row>
                          
                        </el-row>
                        <el-col :span="6" v-show="showUploadArea"></el-col>
                        <el-col :span="18" v-show="showUploadArea" v-if="showUpload">
                          
                          <el-upload
                            ref="upload"
                            action="https://jsonplaceholder.typicode.com/posts/"
                            list-type="picture-card"
                            :on-remove="handleRemove" 
                            :file-list="uploadImgs"
                            :auto-upload="false"
                            :before-upload="beforeUpload"
                            :on-change="uploadOnChange"
                            :on-success="uploadOnSuccess"
                            :on-error="uploadOnError"
                            :on-progress="uploadOnProgress"
                            :on-exceed="upLoadOnExceed"
                            :show-file-list="true"
                            :limit="9"
                            :multiple="true"
                            class="upload">
                            <i class="el-icon-plus"></i>
                          </el-upload>
                          
                          
                        </el-col>
                        <el-col v-show="showTextArea" :span="16" :offset="2" style="margin-top:0;">
                          <el-input type="textarea" resize="none" :rows="12" placeholder="此刻的想法..." v-model="sendText"></el-input>
                          <div class="editTag">
                            <el-tag
                              :key="tag"
                              color="#fff"
                              v-for="tag in tags"
                              closable
                              :disable-transitions="false"
                              @close="handleTagClose(tag)">
                              {{tag}}
                            </el-tag>
                            
                              <el-input
                              class="input-new-tag"
                              v-if="tagsInputVisible&&ableToAddTag"
                              v-model="tagsInputValue"
                              ref="saveTagInput"
                              size="small"
                              @keyup.enter.native="handleTagInputConfirm"
                              @blur="handleTagInputConfirm"
                            >
                            </el-input>
                          <el-button v-if="!tagsInputVisible&&ableToAddTag" class="button-new-tag" size="small" @click="showTagInput">+ tag</el-button>
                          </div>
                        </el-col>
                      </el-row>

                    </div>
                    <el-row type="flex" justify="space-between" align="middle" style="margin-top:10px" v-if="showUploadArea&&!showTextArea">
                      <el-col :span="12" :offset="4" v-if="!showTextArea">已选择{{sendMomentImgNum}}张图片，最多可选择9张图片</el-col>
                      <el-col :span="4" >
                        <img src="../image/arrow-right.png" alt="" @click="sendNextHandler" v-if="showNextBtn" class="sendMomentBtn">
                      </el-col>
                    </el-row>
                    <el-row type="flex" justify="end" style="margin-top:10px" v-if="!showUploadArea&&showTextArea">
                      <el-col :span="4" ><img src="../image/arrow-left.png" @click="sendLastHandler" class="sendMomentBtn"></el-col>
                      <el-col :span="4"><img src="../image/send-moment.png" @click="sendMomentHandler" class="sendMomentBtn"></el-col>
                    </el-row>
                  </div>
                  
                </el-col>
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
   background: transparent;
    height: 0;
    padding-bottom: 38%;
    position: relative;
    -webkit-box-shadow:0 0;
    box-shadow: 0 0;
  }
  .sendContent{
    height: 500px;
    background:url('../image/send-dialog.png');
  }
  .edit{
    
    width: 100%;
    height: 0;
    padding-bottom: 80%;
    overflow: hidden;
    position: relative;
  }
  .edit .el-upload-list--picture-card .el-upload-list__item,.el-upload--picture-card{
    height: 0;
    width: 25%;
    padding-bottom: 25%;
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
  .sendMomentBtn{
    height: 40px;
    width: 40px;
  }
  .el-tag + .el-tag {
    margin-left: 10px;
  }
  .button-new-tag {
    margin-left: 0;
    height: 32px;
    line-height: 30px;
    padding-top: 0;
    padding-bottom: 0;
  }
  .input-new-tag {
    width: 90px;
    margin-left: 10px;
    vertical-align: bottom;
  }
  .editTag{
    margin-top: 20px;
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
        ableToUpload:true,
        tags: [],
        tagsInputVisible: false,
        tagsInputValue: '',
        ableToAddTag:true

      }
    },
    computed:{
      sendMomentImgNum:function(){
        return this.uploadImgs2.length;
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

        if (key == 'user') {
          key = 'user/' + this.$store.state.currentUserId;
          // console.log(key)
        }
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
        // console.log('————发布内容————');
        this.$refs.upload.submit(); //上传图片
        

        this.uploadImgs2=[];
        this.tags=[];
        this.sendText='';
        this.sendMomentVisible=false;
        this.showUploadArea=true;
        this.showNextBtn=false;
        this.showTextArea= false;
        this.showUpload=false;
        this.$message({
          message: '发布成功！',
          type: 'success'
        });
      },
      //上传组件
      handleRemove(file, fileList) {
        if(!fileList.length){
          this.showNextBtn=false;
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
        // console.log('——————开始上传——————');
        // console.log(e.percent,file)
      },
      uploadOnChange(file,fileList){
          //console.log("——————————change——————————")
          // console.log(file)
          if(file.status == 'ready'){
            this.uploadImgs2.push(file);
              //console.log("ready")
          }else if(file.status == 'fail'){
              this.$message.error("图片上传出错，请刷新重试！")
          }
          if(fileList.length){
            this.showNextBtn=true;
          }
      },
      uploadOnSuccess(e,file,fileList){//上传附件
          // console.log("——————————success——————————")
          // console.log(fileList);
      },
      upLoadOnExceed:function(files,fileList){
        this.$message.error('exceed');
        this.$message.warning(`最多可选 9 张图片，本次选择了 ${files.length} 张图片，共选择了 ${files.length + fileList.length} 张图片`);
      },
      uploadOnError(e,file){
          // console.log("——————————error——————————");
          // console.log(e);
      },
      handleTagClose(tag) {
        this.tags.splice(this.tags.indexOf(tag), 1);
        if(this.tags.length<=4){
          this.ableToAddTag=true;
        }
      },

      showTagInput() {
        this.tagsInputVisible = true;
        this.$nextTick(_ => {
          this.$refs.saveTagInput.$refs.input.focus();
        });
      },

      handleTagInputConfirm() {
        let tagsInputValue = this.tagsInputValue;
        if (tagsInputValue) {
          this.tags.push(tagsInputValue);
        }
        if(this.tags.length>=4){
          this.ableToAddTag=false;
        }
        this.tagsInputVisible = false;
        this.tagsInputValue = '';
      }

    },

  };
</script>