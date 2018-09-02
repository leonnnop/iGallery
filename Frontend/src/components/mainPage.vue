<template>
  <el-container class="container-back">
    <div class="container-background"></div>
    <el-col>
      <el-row type="flex" id="topBar" align="middle" :class="navBarFixed == true ? 'navBarWraps' :''" style="background-color:white; width: 100%;font-size: 18px; height:55px; padding-top:5px">
        <el-col :span="1" :offset="1">
          <img src="../image/logo.png" alt="logo" height="30">
        </el-col>
        <el-col :span="3">
          <img src="../image/iga_exa.png" alt="example pic" height="35">
        </el-col>
        <el-col :span="5">
          <el-menu :default-active="topBarActiveIndex" class="el-menu-demo" mode="horizontal" @select="handleTopBarSelect" style="margin-top:-12px;border-bottom:0">
            <el-menu-item index="user">首页</el-menu-item>

            <el-menu-item index="personalpage">我的主页</el-menu-item>

            <el-menu-item index="photoWall">发现</el-menu-item>
          </el-menu>
        </el-col>

        <el-col :span="5" :offset="3">
          <el-input placeholder="请输入搜索内容" v-model="searchInput" clearable> </el-input>
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button round @click="searchHandler">搜索</el-button>
        </el-col>
        <el-col :span="1">
          <el-badge :is-dot="bellDot" class="item_bell" style="margin-top:-4px">
            <i class="el-icon-bell" @click="bellClickHandler"></i>
          </el-badge>
        </el-col>
        <el-col :span="1">
          <img src="../image/send.png" alt="sendMoment" @click="sendMomentInit">

        </el-col>
      </el-row>


      <el-row type="flex" justify="center" style="margin-top:30px">
        <el-col style="width:100%;" :class="navBarFixed == true ? 'mainContentScrolls' :''">

          <router-view></router-view>

        </el-col>
      </el-row>
    </el-col>

    <el-dialog title="" :visible.sync="sendMomentVisible" width="50%" custom-class="sends" :show-close="false" top="10px">
      <el-row>
        <el-col :span="3" :offset="0">
          <img :src="'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +this.$store.state.currentUserId_ID +'&type=2'+'&Rand=' + Math.random()"
            alt="headImg" style="width:80px;height:80px;border-radius:80px;">
        </el-col>
        <el-col :span="18" :offset="0">
          <div class="sendsContent">
            <div class="edit">
              <div style="color:#555;margin:50px 0 20px 80px;font-size:16px;font-weight:bold">{{this.$store.state.currentUsername}}</div>
              <el-row type="flex" justify="center" align="middle">
                <el-row>

                </el-row>
                <el-col :span="6" v-show="showUploadArea"></el-col>
                <el-col :span="18" v-show="showUploadArea" v-if="showUpload">

                  <el-upload ref="upload" action="http://192.168.43.249:54468/api/Picture" list-type="picture-card" :on-remove="handleRemove"
                    :file-list="uploadImgs" :auto-upload="false" :before-upload="beforeUpload" :on-change="uploadOnChange"
                    :on-success="uploadOnSuccess" :on-error="uploadOnError" :on-progress="uploadOnProgress" :on-exceed="upLoadOnExceed"
                    :show-file-list="true" :limit="9" :multiple="true" class="upload" :data="pictureObj">
                    <i class="el-icon-plus"></i>
                  </el-upload>


                </el-col>
                <el-col v-show="showTextArea" :span="16" :offset="2" style="margin-top:0;">
                  <el-input type="textarea" resize="none" :rows="12" placeholder="此刻的想法..." v-model="sendText"></el-input>
                  <div class="editTag">
                    <el-tag :key="tag" color="#fff" v-for="tag in tags" closable :disable-transitions="false" @close="handleTagClose(tag)">
                      {{tag}}
                    </el-tag>

                    <el-input class="input-new-tag" v-if="tagsInputVisible&&ableToAddTag" v-model="tagsInputValue" ref="saveTagInput" size="small"
                      @keyup.enter.native="handleTagInputConfirm" @blur="handleTagInputConfirm">
                    </el-input>
                    <el-button v-if="!tagsInputVisible&&ableToAddTag" class="button-new-tag" size="small" @click="showTagInput">+ tag</el-button>
                  </div>
                </el-col>
              </el-row>

            </div>
            <el-row type="flex" justify="space-between" align="middle" style="margin-top:10px" v-if="showUploadArea&&!showTextArea">
              <el-col :span="12" :offset="4" v-if="!showTextArea">已选择{{sendMomentImgNum}}张图片，最多可选择9张图片</el-col>
              <el-col :span="4">
              </el-col>
              <el-col :span="4">
                <img src="../image/send-moment.png" @click="sendMomentHandler" v-if="showNextBtn" class="sendMomentBtn">
              </el-col>
            </el-row>
            <el-row type="flex" justify="end" style="margin-top:10px" v-if="!showUploadArea&&showTextArea">
              <el-col :span="4">
                <img src="../image/arrow-right.png" @click="sendNextHandler" class="sendMomentBtn">
              </el-col>
            </el-row>
          </div>

        </el-col>
      </el-row>

    </el-dialog>

  </el-container>
</template>

<style>
  .item_bell {
    margin-top: 10px;
    margin-right: 40px;
  }

  .container-back {
    background-image: url(../image/slash.png);
    background-repeat: repeat;
    background-size: 100% 100%
  }

  .container-background {
    position: fixed;
    height: 100%;
    width: 100%;
    background-image: url(../image/slash.png);
    background-repeat: repeat;
  }

  .navBarWraps {
    position: fixed;
    top: 0;
    z-index: 999;
  }

  .mainContentScrolls {
    margin-top: 55px
  }

  .sends {
    background: transparent;
    height: 0;
    padding-bottom: 38%;
    position: relative;
    -webkit-box-shadow: 0 0;
    box-shadow: 0 0;
  }

  .sendsContent {
    height: 500px;
    background: url('../image/send-dialog.png');
  }

  .edit {

    width: 100%;
    height: 0;
    padding-bottom: 80%;
    overflow: hidden;
    position: relative;
  }

  .edit .el-upload-list--picture-card .el-upload-list__item,
  .el-upload--picture-card {
    height: 0;
    width: 25%;
    padding-bottom: 25%;
    position: relative;
  }

  .edit .el-upload-list--picture-card .el-upload-list__item img {
    position: absolute;
    left: 0;
    top: 0;
  }

  .el-upload--picture-card i {
    position: absolute;
    left: 36%;
    top: 36%;
  }

  .sendMomentBtn {
    height: 40px;
    width: 40px;
  }

  .el-tag+.el-tag {
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

  .editTag {
    margin-top: 20px;
  }
</style>

<script>
  export default {
    data() {
      return {
        fileListLength: 0,
        bellDot: false,
        pictureURL: '',
        loadingPage: true,
        navBarFixed: false,
        searchInput: '',
        topBarActiveIndex: '1',
        sendMomentVisible: false,
        dialogImageUrl: '',
        dialogVisible: false,
        showNextBtn: false,
        showUpload: false,
        showUploadArea: true,
        showTextArea: false,
        sendText: '',
        uploadImgs2: [],
        uploadImgs: [],
        ableToUpload: true,
        tags: [],
        tagsInputVisible: false,
        tagsInputValue: '',
        ableToAddTag: true,
        pictureObj: {
          id: 2,
          type: 2
        },
        currentMomentID: '',

      }
    },
    computed: {
      sendMomentImgNum: function () {
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
      bellClickHandler() {
        this.$router.push('/main/message/self');
        this.topBarActiveIndex = '0'
        this.bellDot = false;
      },
      handleTopBarSelect(key, keyPath) {
        console.log('/' + key);

        if (key == 'user') {
          key = 'user/' + this.$store.state.currentUserId;
        } else if (key == 'personalpage') {
          key = 'personalpage/' + this.$store.state.currentUserId_ID;
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
      sendMomentInit: function () {
        this.sendMomentVisible = true;
        this.showUpload = true;
        this.sendLastHandler();

        this.axios.get('http://192.168.43.249:54468/api/Moment/NextMomentID')
          .then((response) => {
            this.currentMomentID = response.data;
          })
      },
      sendNextHandler: function () {
        this.showUploadArea = true;
        this.showTextArea = false;

        this.axios.post('http://192.168.43.249:54468/api/Moment/InsertMoment', {
            ID: this.currentMomentID,
            SenderID: this.$store.state.currentUserId_ID,
            Content: this.sendText,
            LikeNum: 0,
            ForwardNum: 0,
            CollectNum: 0,
            CommentNum: 0,
            Time: '',
          })
          .then((response) => {
            if (response.data == 0) {

              if (this.tags.length > 0) {
                this.axios.get('http://192.168.43.249:54468/api/Tag/AddTag?Moment_Id=' + this.currentMomentID + '&', {
                  params: {
                    TagNames: this.tags,
                  },
                  paramsSerializer: function (params) {
                    var Qs = require('qs');
                    return Qs.stringify(params, {
                      arrayFormat: 'repeat'
                    })
                  }
                })
              }
            } else {
              this.$message.error('本次发布失败，服务器内部错误，请重试。');
              this.uploadImgs2 = [];
              this.tags = [];
              this.sendText = '';
              this.sendMomentVisible = false;
              this.showUploadArea = true;
              this.showNextBtn = false;
              this.showTextArea = false;
              this.showUpload = false;
            }
          });

        console.log(this.tags)


      },
      sendLastHandler: function () {
        this.showUploadArea = false;
        this.showTextArea = true;
      },
      refresh() {
        history.go(0)
        this.$store.commit('addCurrentUsername', this.getCookie(Username));
        this.$store.commit('addCurrentUserPassword', this.getCookie(Password));
        this.$store.commit('addCurrentUserBio', this.getCookie(Bio));
        this.$store.commit('addCurrentUserPhoto', this.getCookie(Photo));
      },
      sendMomentHandler: function () {
        this.$refs.upload.submit(); //上传图片

        this.uploadImgs2 = [];
        this.tags = [];
        this.sendText = '';
        this.sendMomentVisible = false;
        this.showUploadArea = true;
        this.showNextBtn = false;
        this.showTextArea = false;
        this.showUpload = false;


      },
      //上传组件
      getCookie(name) {
        var strCookie = document.cookie;
        var arrCookie = strCookie.split("; ");
        for (var i = 0; i < arrCookie.length; i++) {
          var arr = arrCookie[i].split("=");
          if (arr[0] == name)
            return arr[1];
        }
        return "";
      },
      handleRemove(file, fileList) {
        if (!fileList.length) {
          this.showNextBtn = false;
        }
      },
      beforeUpload: function (file) {

      },
      myfresh() {
        this.$router.push('/main/leaderboard');
      },
      uploadOnProgress(e, file, fileList) { //开始上传


        var oneFile = file.raw;
        var formdata1 = new FormData(); // 创建form对象
        formdata1.append('file', oneFile); // 通

        let config = {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        }; //添加请求头


        this.axios.post('http://192.168.43.249:54468/api/Picture/Save?id=' + this.currentMomentID + '&type=1',
          formdata1,
          config).then((response) => { //这里的/xapi/upimage为接口
          console.log(response.data);
          console.log('啊哈哈', this.fileListLength)
          this.fileListLength--;
          if (this.fileListLength == 0) {
            this.myfresh()
          }
        })


      },
      uploadOnChange(file, fileList) {
        if (file.status == 'ready') {
          this.uploadImgs2.push(file);
        } else if (file.status == 'fail') {}
        if (fileList.length) {
          this.showNextBtn = true;
        }
        this.fileListLength = Math.max(fileList.length, this.fileListLength);
        console.log(this.fileListLength)

      },
      uploadOnSuccess(e, file, fileList) { //上传附件
      },
      upLoadOnExceed: function (files, fileList) {
        this.$message.error('exceed');
        this.$message.warning(`最多可选 9 张图片，本次选择了 ${files.length} 张图片，共选择了 ${files.length + fileList.length} 张图片`);
      },
      uploadOnError(e, file) {},

      handleTagClose(tag) {
        this.tags.splice(this.tags.indexOf(tag), 1);
        if (this.tags.length <= 4) {
          this.ableToAddTag = true;
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
        if (this.tags.length >= 4) {
          this.ableToAddTag = false;
        }
        this.tagsInputVisible = false;
        this.tagsInputValue = '';
      },

      // uploadOnProgress(event, file, fileList) {
      //   console.log(fileList)
      // }
      searchHandler: function () {
        this.$router.push('/main/searchResult/' + this.searchInput);
        this.searchInput = '';
        this.topBarActiveIndex = '0'
      },
      backgroundHandler() {
        console.log('backgroundHandler');
        this.loadingPage = false
      }
    },
    created() {
      var that = this;
      var start = () => {
        var wsImpl = window.WebSocket || window.MozWebSocket;
        console.log('websocket', that.$store.state.currentUserId_ID)

        window.ws = new wsImpl('ws://192.168.43.140:8181/' + that.$store.state.currentUserId_ID)

        ws.onmessage = function (evt) {
          console.log('..我收到了')

          that.bellDot = true;
          if ((that.$route.name == "message") && ((evt.data).indexOf("私信") != -1)) {
            that.myfresh();
          } else {
            that.$notify({
              title: '您有新的消息',
              message: '' + evt.data
            });
          }
        };

        ws.onopen = function () {
          console.log('.. connection open<br/>');
        };

        ws.onclose = function () {
          console.log('.. connection closed<br/>')
        }
      }
      var myHollowClick = function () {
        var click_cnt = 0;
        var $html = document.getElementsByTagName("html")[0];
        var $body = document.getElementsByTagName("body")[0];
        var texts = ['富强', '民主', '文明', '和谐', '自由', '平等', '公正', '法治', '爱国', '敬业', '诚信', '友善', '❤'];
        var colors = ['#FF0000', '#9F5F9F', ' #B5A642', '#5F9F9F', '#238E23'];
        $html.onclick = function (e) {
          var $elem = document.createElement("b");
          $elem.style.color = "#E94F06";
          $elem.style.zIndex = 9999;
          $elem.style.position = "absolute";
          $elem.style.select = "none";
          var x = e.pageX;
          var y = e.pageY;
          $elem.style.left = (x - 10) + "px";
          $elem.style.top = (y - 20) + "px";
          clearInterval(anim);
          let click_cnt_text = click_cnt % texts.length;
          let click_cnt_color = click_cnt % colors.length;
          $elem.innerText = texts[click_cnt_text];
          click_cnt++;
          $elem.style.fontSize = '16px'
          var increase = 0;
          var anim;
          setTimeout(function () {
            anim = setInterval(function () {
              if (++increase == 150) {
                clearInterval(anim);
                $body.removeChild($elem);
              }
              $elem.style.top = y - 20 - increase + "px";
              $elem.style.opacity = (150 - increase) / 120;
            }, 8);
          }, 70);
          $body.appendChild($elem);
        };
      };
      start();
      myHollowClick();
    }

  };
</script>