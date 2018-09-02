<template>
  <el-container>
    <transition name="slide-fade">
      <div v-if="loadingPage" class="loadingbackground" style="margin-top:-30px"></div>
    </transition>
    <el-col>

      <el-card style="width:70%" :body-style="{padding:'0px'}" :class="navBarFixed == true ? 'navBarWrap1' :''" shadow="never">
        <el-row style="background-color:#ffffff;padding-bottom:10px" class="clearfix">

          <el-row>
            <el-col :span="6" :offset="1" style="margin:20px;padding-left:30px">
              <el-upload v-if="$route.params.id==$store.state.currentUserId_ID" class="avatar-uploader" action="https://jsonplaceholder.typicode.com/posts/"
                :show-file-list="false" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload" :on-progress="uploadOnProgress">

                <div class="moment" :style="{backgroundImage: 'url(' + (headUrl)+'&Rand=' + Math.random() + ')'}">
                  <div class="moment-inner">
                    <div class="icon">
                      <el-row type="flex" justify="center" align="middle">
                        <img src="../image/upload-icon.png" alt="" height="60px">

                      </el-row>
                    </div>
                  </div>
                </div>
              </el-upload>
              <img v-if="$route.params.id!=$store.state.currentUserId_ID" :src="headUrl+'&Rand=' + Math.random()" class="headImg" alt="头像">

            </el-col>
            <el-col :span="12" style="padding-top:20px">
              <el-row>
                <el-button style="font-size:26px" type="text" id="username">{{username}}</el-button>
              </el-row>
              <el-row>
                <el-button type="text" class="num" @click="showFollow">关注: {{followNum}}</el-button>
                <el-dialog title="" :visible.sync="followListVisible" width="40%">
                  <span slot="title" style="color:#555;font-size:20px;letter-spacing:5px;">关注</span>
                  <div style="height:400px;overflow:hidden;overflow-y:auto;">
                    <div style="border-bottom:1px solid rgb(235,238,245);"></div>
                    <div v-for="(followUser,index) in followUsers" :key="index" class="like-user-li">
                      <el-row type="flex" justify="center" align="middle">
                        <el-col :span="3" :offset="1" style="height:50px;">
                          <img :src="followUser.headImg+'&Rand=' + Math.random()" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(followUser.ID)">
                        </el-col>
                        <el-col :span="16">
                          <el-row>{{followUser.Username}}</el-row>
                          <el-row style="font-size:12px;margin-top:5px;">{{followUser.Bio}}</el-row>
                        </el-col>
                        <el-col :span="3">
                          <el-button plain size="small" @click="followHandler(followUser,followUser.FollowState)" :class="{followed:followUser.FollowState}"
                            v-if="followUser.ID!=$store.state.currentUserId_ID">{{followUser.followState}}</el-button>
                          <!--  -->
                        </el-col>
                      </el-row>
                    </div>
                  </div>
                </el-dialog>
                <el-button type="text" class="num" @click="showFans">粉丝: {{fansNum}}</el-button>
                <el-dialog title="" :visible.sync="fanListVisible" width="40%">
                  <span slot="title" style="color:#555;font-size:20px;letter-spacing:5px;">粉丝</span>
                  <div style="height:400px;overflow:hidden;overflow-y:auto;">
                    <div style="border-bottom:1px solid rgb(235,238,245);"></div>
                    <div v-for="(fanUser,index) in fanUsers" :key="index" class="like-user-li">
                      <el-row type="flex" justify="center" align="middle">
                        <el-col :span="3" :offset="1" style="height:50px;">
                          <img :src="fanUser.headImg+'&Rand=' + Math.random()" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(fanUser.ID)">
                        </el-col>
                        <el-col :span="16">
                          <el-row>{{fanUser.Username}}</el-row>
                          <el-row style="font-size:12px;margin-top:5px;">{{fanUser.Bio}}</el-row>
                        </el-col>
                        <el-col :span="3">
                          <el-button plain size="small" @click="followHandler(fanUser,fanUser.FollowState)" :class="{followed:fanUser.FollowState}"
                            v-if="fanUser.ID!=$store.state.currentUserId_ID">{{fanUser.followState}}</el-button>
                          <!--  -->
                        </el-col>
                      </el-row>
                    </div>
                  </div>
                </el-dialog>
              </el-row>
              <el-row>
                <p style="font-size:13px;color:#6c6b6e">个人简介：{{desc}}</p>
              </el-row>
            </el-col>
            <el-col :span="4" style="height:100%">
              <el-row type="flex" align="middle" justify="center" style="height:100%;margin-top:25%">
                <el-button v-if="$route.params.id!=$store.state.currentUserId_ID" icon="el-icon-message" style="width:50px;height:50px" @click="messageClickHandler"
                  plain type="primary" circle></el-button>
                <el-button v-if="$route.params.id!=$store.state.currentUserId_ID" icon="el-icon-view" style="margin-left:15px;width:50px;height:50px;"
                  :class="{background_white: !FollowState}" @click="followClickHandler" plain type="primary" circle></el-button>
              </el-row>
            </el-col>
          </el-row>
        </el-row>
        <el-row style="padding:0 10px">
          <el-menu default-active="dynamic" class="el-menu-demo" mode="horizontal" @select="handleSelectTop" active-text-color="#409eff">
            <el-menu-item index="dynamic">我的动态
              <span style="font-size:12px;color:#000009">{{moments.length}}</span>
            </el-menu-item>
            <el-menu-item index="favors" v-if="$route.params.id==$store.state.currentUserId_ID">收藏夹
              <span style="font-size:12px;color:#000009">{{favors.length+1}}</span>
            </el-menu-item>
            <el-menu-item index="set" v-if="$route.params.id==$store.state.currentUserId_ID">设置</el-menu-item>
          </el-menu>
        </el-row>
        <el-row v-if="isDynamic" style="padding-left:30px;padding-top:15px">
          <div v-if="moments.length<1" class="message">还没有已发表的动态呢！</div>

          <el-col v-for="moment in moments" :key="moment.name" :span="6">
            <el-col>
              <div class="moments" :style="{backgroundImage:'url('+moment.url+'&Rand=' + Math.random() + ')'}" @click="toMoment(moment.momentID)"></div>
            </el-col>
          </el-col>
        </el-row>
        <el-row v-if="isFavors&&$route.params.id==$store.state.currentUserId_ID">
          <el-col :span="5">
            <el-row>
              <el-row style="margin:10px">
                <el-col :span="20" :offset="1">
                  <span style="font-size:18px;color:#bbbbbb">动态收藏</span>
                </el-col>
                <el-col :span="3">
                  <i class="el-icon-circle-plus-outline" @click="dialogFormVisible=true"></i>
                </el-col>
                <el-dialog title="" :visible.sync="dialogFormVisible" class="dialog">
                  <div slot="title">新建收藏夹</div>
                  <el-form ref="form" :model="ruleform" :rules="rules">
                    <el-form-item label="名称：" :label-width="formLabelWidth" prop="fname">
                      <el-input v-model="ruleform.fname" auto-complete="off" style="width:200px" placeholder="最大长度为15个字符"></el-input>
                    </el-form-item>
                  </el-form>
                  <div slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="finishHandler()" size="middle">确定</el-button>
                    <el-button @click="dialogFormVisible=false;ruleform.fname=''" size="middle">取消</el-button>
                  </div>
                </el-dialog>
              </el-row>
            </el-row>
            <el-menu default-active="默认收藏夹" class="el-menu-vertical-demo" @select="handleSelectLeft">

              <el-menu-item index="默认收藏夹">
                <el-col :span="18">
                  <el-row>
                    <el-col :span="7">
                      <img style="width:60%" src="../image/heart.png" />
                    </el-col>
                    <el-col :span="17">{{'默认收藏夹'+'('+defaultCollectNum+')'}}</el-col>
                  </el-row>
                </el-col>
              </el-menu-item>
              <el-row v-for="(favor,index) in favors" :key="index">
                <el-menu-item :index="favor.favorName">
                  <el-row>
                    <el-col :span="18">
                      <el-row>
                        <el-col :span="7">
                          <img style="width:60%" src="../image/folder.png" />
                        </el-col>
                        <el-col :span="17">{{favor.favorName+'('+favor.collectNum+')'}}</el-col>
                      </el-row>
                    </el-col>
                    <el-col :span="1">
                      <el-dropdown @command="handleCommand">
                        <span class="el-dropdown-link">
                          <i class="el-icon-more el-icon--right"></i>
                        </span>
                        <el-dropdown-menu slot="dropdown">
                          <el-dropdown-item @click.native="deleteFavor(favor.favorName)">删除</el-dropdown-item>
                          <el-dropdown-item @click.native="editClick()" divided>编辑名称</el-dropdown-item>
                        </el-dropdown-menu>
                      </el-dropdown>
                    </el-col>
                  </el-row>
                </el-menu-item>
                <el-dialog title="" :visible.sync="dialogFormVisible2" class="dialog" :modal-append-to-body="false">
                  <div slot="title">重命名收藏夹</div>
                  <el-form ref="form2" :model="ruleform2" :rules="rules2">
                    <el-form-item label="名称：" :label-width="formLabelWidth" prop="fname2">
                      <el-input v-model="ruleform2.fname2" auto-complete="off" style="width:200px" :value="favor.favorName"></el-input>
                    </el-form-item>
                  </el-form>
                  <div slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="finishHandler2(index,favor.favorName)" size="middle">确 定</el-button>
                    <el-button @click="dialogFormVisible2=false;ruleform2.fname2=''" size="middle">取 消</el-button>
                  </div>
                </el-dialog>
              </el-row>
             
            </el-menu>
          </el-col>
          <el-col :span="19" style="padding-left:30px;padding-top:15px">
            <div v-if="collects.length<1" class="message">还没有动态在该收藏夹哦，快去收藏吧！</div>

            <el-col v-for="(collect,index) in collects" :key="index" :span="8">
              <el-col>
                <div class="moments" :style="{backgroundImage:'url('+collect.url+'&Rand=' + Math.random() + ')'}" @click="toMoment(collect.momentID)"></div>
                <el-col :span="2">
                  <el-dropdown>
                    <span class="el-dropdown-link">
                      <i class="el-icon-more"></i>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                      <el-dropdown-item @click.native="deleteCollect(index,collect)">取消收藏</el-dropdown-item>
                      <el-dropdown-item @click.native="dialogFormVisible3=true">移动</el-dropdown-item>

                    </el-dropdown-menu>
                  </el-dropdown>
                </el-col>
              </el-col>
              <el-dialog :visible.sync="dialogFormVisible3" :modal-append-to-body="false" class="checkForm">
                <div slot="title">选择所要移动到的收藏夹</div>
                <div v-if="favors.length==0">
                  <span style="color:#aaaaaa;padding-left:5px">* 没有可移动到的收藏夹哦!</span>
                </div>
                <el-form ref="move" :model="moveForm">
                  <el-form-item>
                    <el-radio-group v-model="moveForm.favorName">
                      <div v-if="currentSelectLeft!='默认收藏夹'" class="move">
                        <el-radio label="默认收藏夹">
                          <span style="font-size:15px">默认收藏夹</span>
                        </el-radio>
                      </div>
                      <div v-for="item in favors" :key="item">
                        <div class="move" v-if="item.favorName!=currentSelectLeft">
                          <el-radio :label="item.favorName">
                            <span style="font-size:15px">{{item.favorName}}</span>
                          </el-radio>
                        </div>
                      </div>
                    </el-radio-group>
                  </el-form-item>
                </el-form>
                <div slot="footer" align="middle">
                  <el-button type="primary" @click="moveHandler(moveForm.favorName,index,collect)" size="middle">确认</el-button>
                  <el-button @click="dialogFormVisible3=false;moveForm.favorName=''" size="middle">取消</el-button>
                </div>
              </el-dialog>
            </el-col>

          </el-col>
        </el-row>
      </el-card>
    </el-col>
  </el-container>
</template>

<style scoped>
  .background_white {
    background-color: rgba(255, 255, 255, 1)
  }

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
  }

  .icon {
    position: absolute;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
    color: #fff;
    font-size: 20px;
  }

  .moment {
    width: 178px;
    height: 178px;
    border-radius: 89px;
    cursor: pointer;
    padding-bottom: 26px;
    margin: 0 60px 30px 0;
    background-position: center;
    background-size: cover;
    box-sizing: border-box;
  }


  .moment-inner {
    width: 178px;
    height: 178px;
    border-radius: 89px;

    display: none;
    background: rgba(12, 12, 12, 0.5);
    position: relative;
  }

  .moment:hover .moment-inner {
    display: block;
  }

  .headImg-inner {
    height: 0;
    width: 260px;
    padding-bottom: 100%;
    display: none;
    background: rgba(0, 0, 0, 0.3);
    position: relative;
  }

  .headImg:hover .headImg-inner {
    display: block;
  }

  .show-comment {
    border-top: 1px solid rgb(235, 238, 245);
    padding-top: 20px;
    padding-bottom: 10px;
  }

  .show-comment-img {
    width: 50px;
    height: 50px;
    border-radius: 50px;
  }

  .hover-cursor {
    cursor: pointer;
  }

  .like-user-li {
    padding: 10px 0;
    border-bottom: 1px solid rgb(235, 238, 245);
  }

  .followed {
    padding: 9px 9px;
  }

  .headImg {
    width: 178px;
    height: 178px;
    border-radius: 83px;
    cursor: pointer;
  }

  .headImg:hover {
    opacity: 0.8;
  }

  .navBarWrap1 {
    margin-left: auto;
    margin-right: auto;
    margin-top: 10px;
    margin-bottom: 10px;
  }

  .el-tabs__item {
    font-size: 15px;
    margin: 10px;
  }

  .moments {
    display: block;
    width: 200px;
    height: 200px;
    margin: 10px 0;
    border: 1px;
    background-image: url('../image/ins1.png');
    background-repeat: no-repeat;
    background-size: cover;
    -webkit-background-size: cover;
    -moz-background-size: cover;
    -o-background-size: cover;
  }

  .img {
    display: block;
    width: 200px;
    height: 200px;
    border-radius: 5px;
  }

  .moments:hover {
    opacity: 0.85;
  }

  .icon {
    margin-top: -5px;
  }

  .dialog {
    margin-left: auto;
    margin-right: auto;
    display: justify;
    width: 60%;
  }

  .checkForm {

    margin-left: auto;
    margin-right: auto;
    width: 40%;
  }

  .cards {
    margin-left: auto;
    margin-right: auto;
    margin-top: 10px;
    margin-bottom: 10px;
  }

  #username {
    font-size: 18px;
    padding-top: 30px;
  }

  .num {
    color: #4e4c4c;
    font-size: 14px;
    padding-right: 10px;
  }

  .el-dropdown-link {
    cursor: pointer;
    color: #409EFF;
    padding-left: 20px;
  }

  .move {
    margin: 10px 50px;
  }

  .el-icon-arrow-down {
    font-size: 20px;
  }

  .message {
    height: 270px;
    width: 100%;
    line-height: 270px;
    text-align: center;
    color: #999;
    letter-spacing: 1px;
    border-top: 1px solid rgb(235, 238, 245);
  }
</style>

<script>
  import qs from 'qs'
  import Vue from 'vue'

  export default {
    name: 'personalpage',
    data() {
      var validateName = (rule, value, callback) => {
        if (value === '') {
          return callback(new Error('请输入名称'));
        } else {
          return callback();
        }
      };
      return {
        FollowState: true,
        followword: '关注',
        followUsers: [{
          ID: '1',
          headImg: require('../image/a.jpg'),
          Username: 'user1',
          Bio: '诚信肥宅',
          FollowState: true,
          followState: '已关注'
        }, {
          ID: '2',
          headImg: require('../image/a.jpg'),
          Username: 'user2',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'

        }, {
          ID: '3',
          headImg: require('../image/a.jpg'),
          Username: 'user3',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '4',
          headImg: require('../image/a.jpg'),
          Username: 'user4',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '5',
          headImg: require('../image/a.jpg'),
          Username: 'user5',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '6',
          headImg: require('../image/a.jpg'),
          Username: 'user6',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '7',
          headImg: require('../image/a.jpg'),
          Username: 'user7',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '8',
          headImg: require('../image/a.jpg'),
          Username: 'user8',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }],
        fanUsers: [{
          ID: '1',
          headImg: require('../image/a.jpg'),
          Username: 'user1',
          Bio: '诚信肥宅',
          FollowState: true,
          followState: '已关注'
        }, {
          ID: '2',
          headImg: require('../image/a.jpg'),
          Username: 'user2',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'

        }, {
          ID: '3',
          headImg: require('../image/a.jpg'),
          Username: 'user3',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '4',
          headImg: require('../image/a.jpg'),
          Username: 'user4',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '5',
          headImg: require('../image/a.jpg'),
          Username: 'user5',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '6',
          headImg: require('../image/a.jpg'),
          Username: 'user6',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '7',
          headImg: require('../image/a.jpg'),
          Username: 'user7',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }, {
          ID: '8',
          headImg: require('../image/a.jpg'),
          Username: 'user8',
          Bio: '诚信肥宅',
          FollowState: false,
          followState: '关注'
        }],
        followListVisible: false,
        currentSelectLeft: '默认收藏夹',
        fanListVisible: false,
        defaultCollectNum: 0,
        activeIndex: '1',
        navBarFixed: true,
        checkedFavor: '默认收藏夹',
        headUrl: require('../image/a.jpg'),
        username: '初始昵称',
        followNum: 0,
        fansNum: 0,
        desc: '感觉头发被掉光。',
        momentNum: 0,
        moments: [{
          momentID: '0',
          url: require('../image/a.jpg'),
          text: 'zero'
        }, {
          momentID: '1',
          url: require('../image/ins1.png'),
          text: 'first'
        }, {
          momentID: '2',
          url: require('../image/ins2.png'),
          text: 'second'
        }, {
          momentID: '3',
          url: require('../image/ins3.png'),
          text: 'third'
        }, {
          momentID: '4',
          url: require('../image/ins_ex.jpg'),
          text: 'forth'
        }],
        favors: [ //收藏夹信息,
          {
            //url: require('../image/gaojin_ciyun.png'),
            favorName: 'MyCollect',
            collectNum: 1,
          }, {
            //url: require('../image/gaojin_radar.png'),
            favorName: 'Something',
            collectNum: 2,
          }, {
            //url: require('../image/a.jpg'),
            favorName: 'Anything',
            collectNum: 3,
          }, {
            //url: require('../image/a.jpg'),
            favorName: 'Nothing',
            collectNum: 4,
          }
        ],
        collects: [{ //收藏夹内的动态
          momentID: '',
          url: require('../image/a.jpg'),
        }, {
          momentID: '',
          url: require('../image/ins1.png'),
        }, {
          momentID: '',
          url: require('../image/ins2.png'),
        }, {
          momentID: '',
          url: require('../image/ins3.png'),
        }, {
          momentID: '',
          url: require('../image/ins_ex.jpg'),
        }],
        ruleform: {
          fname: ''
        },
        rules: {
          fname: [{
            validator: validateName,
            message: '请输入名称',
            trigger: 'blur'
          }, {
            min: 1,
            max: 15,
            message: '名称长度为1~15',
            trigger: 'blur'
          }]
        },
        ruleform2: {
          fname2: ''
        },
        rules2: {
          fname2: [{
            validator: validateName,
            message: '请输入新名称',
            trigger: 'blur'
          }, {
            min: 1,
            max: 15,
            message: '名称长度为1~15',
            trigger: 'blur'
          }]
        },
        formLabelWidth: '100px',
        //isFavor:true,
        //isCollect:false,
        isDynamic: true,
        isFavors: false,
        dialogFormVisible: false,
        dialogFormVisible2: false,
        dialogFormVisible3: false,
        loadingPage: true,
        moveForm: {
          favorName: ''
        },
        moveRules: {
          favorName: [{
            validator: true,
            message: '请选择',
            trigger: 'blur',
          }]
        },
      }
    },
    methods: {
      messageClickHandler() {
        this.$router.push('/main/message/' + this.$route.params.id)
      },
      jumpToUser: function (url) {
        console.log(url);
        this.$router.push('/main/personalpage/' + url)
      },
      messageWebsocketHandler(path, state) {
        // 0 关注 1 点赞 2 评论 3 转发 4 私信
        window.ws.send('/' + path + ' ' + state);
      },
      followClickHandler() {

        this.axios.get('http://192.168.43.249:54468/api/Users/Follow?followID=' + this.$store.state.currentUserId_ID +
            '&followedID=' + this.$route.params.id)
          .then((response) => {
            if (response.data == 0) {
              if (!this.FollowState) {
                this.fansNum++;
                this.messageWebsocketHandler(this.$route.params.id, 0)
              } else {
                this.fansNum--;
              }
              this.FollowState = !this.FollowState;
            } else {
              this.$message.error('关注失败，服务器内部错误，请重试。');
            }
          });

      },
      myfresh() {
        this.$router.push('/main/leaderboard');
        // this.$router.push('/main/personalpage/' + email);
      },

      moveHandler(favorName, index, collect) {
        // console.log('favorName',favorName.favorName)
        this.axios.get(' http://192.168.43.249:54468/api/Collection/MoveMomentToAnotherCollection?moment_id=' + collect
            .momentID +
            '&founder_id=' + this.$store.state.currentUserId_ID +
            '&new_collection_name=' + favorName
          )
          .then((response) => {
            if (response.data == 0) {
              this.$message({
                message: '移动成功！',
                type: 'success'
              });
              this.collects.splice(index - 1, 1); // this.deleteCollect(index, collect);
              // this.myfresh();
              this.dialogFormVisible3 = false;
              (this.favors).foreach((element) => {
                if (element.favorName == currentSelectLeft) {
                  element.collectNum--;
                } else if (currentSelectLeft == '默认收藏夹')
                  this.defaultCollectNum--;

                if (element.favorName == favorName)
                  element.collectNum++;
                else if (favorName == '默认收藏夹')
                  this.defaultCollectNum++;
              })

            } else if (response.data == 1) {
              this.$message({
                message: '移动失败，请重试！',
                type: 'warning'
              });

            }
          })
      },
      uploadOnProgress(e, file) { //开始上传
        // console.log('——————开始上传——————');
        // console.log(file)
        // var file = document.getElementById("upload_file").files[0];
        var oneFile = file.raw;
        var formdata1 = new FormData(); // 创建form对象
        formdata1.append('file', oneFile); // 通
        // formdata1.append('id', 2); // 通
        // formdata1.append('type', 2); // 通
        let config = {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        }; //添加请求头

        this.headUrl = file.raw;
        this.axios.post('http://192.168.43.249:54468/api/Picture/Save?id=' + this.$store.state.currentUserId_ID +
          '&type=2',
          formdata1,
          config).then((response) => { //这里的/xapi/upimage为接口
          console.log(response.data);
          // this.headUrl = 
          // this.myfresh();
          this.$message({
            message: '上传成功!',
            type: 'success'
          });
          this.myfresh()
          // var self = this;
          // this.toastrVal = inVal;
          // this.loadState = true;
          // this.noBg = bgState;
          // setTimeout(function () {
          //   self.myfresh();
          // }, 500);
        })

      },
      editClick() {
        this.dialogFormVisible2 = true;
        console.log(this.dialogFormVisible2)
      },
      handleCommand(command) {
        // this.$message('click on item ' + command);
        // command()
      },
      followHandler: function (user, FollowState) {
        console.log(user)
        console.log(this.$store.state.currentUserId_ID)
        console.log(user.FollowState);
        //////////////
        if (!user.FollowState) {
          user.followState = '已关注';
        } else {
          user.followState = '关注';
          console.log(user.followState)
        }
        user.FollowState = !user.FollowState;
        console.log(user.FollowState);

        this.axios.get('http://192.168.43.249:54468/api/Users/Follow?followID=' + this.$store.state.currentUserId_ID +
            '&followedID=' + user.ID)
          .then((response) => {
            if (response.data == 0) {
              // this.$message({
              //     message: '关注成功',
              //     type: 'success'
              // });
            } else {
              this.$message.error('关注失败，服务器内部错误，请重试。');
            }
          });
      },
      contains: function (a, obj) {
        var i = a.length;
        while (i--) {
          if (a[i].favorName === obj)
            return i;
        }
      },
      handleSelectTop(key, keypath) {
        console.log('/', key);

        if (key == 'dynamic') {
          this.isDynamic = true;
          this.isFavors = false;
        } else if (key == 'favors') {
          this.isDynamic = false;
          this.isFavors = true;
        } else {
          this.$router.push('/main/' + key);
        }
        //this.$router.push('/main/' + key);
      },
      handleSelectLeft(key, keypath) {
        // console.log(key)
        // console.log(keypath)
        // if (key == '默认收藏夹') {
        this.currentSelectLeft = key;
        console.log(this.currentSelectLeft)
        this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnCollectionContentID?FounderID=' + this.$store.state
            .currentUserId_ID +
            '&Name=' + key)
          .then((response) => {
            // this.collects = response.data
            // //===============
            // let totalMoments = response.data;
            // totalMoments.foreach((element) => {
            //   this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
            //     .then((responsed) => {
            //       Vue.set(element, 'url',
            //         'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
            //         response.data[0]);
            //     })
            // })
            var momentIDList = response.data;
            var index = 0;
            this.collects = []

            momentIDList.forEach(element => {
              var temp = {}
              temp.momentID = element;
              this.axios.get('http://192.168.43.249:54468/api/Collection/GetFirstPicIDbyMomentID?moment_id=' +
                  element)
                .then((response) => {
                  //////////////////////////////
                  temp.url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data;
                  this.collects.push(temp)
                })
            });
            index++;
          }) // } else if (key != 'title') {
        //   this.$axios.post('http://192.168.43.249:54468/api/Users/ReturnCollectionContentID', {
        //       FounderID: this.$store.state.currentUserId_ID,
        //       Name: this.favors[key].favorName
        //     })
        //     .then((response) => {
        //       this.collects = response.data
        //       //===============
        //       let totalMoments = response.data;
        //       totalMoments.foreach((element) => {
        //         this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
        //           .then((responsed) => {
        //             Vue.set(element, 'url',
        //               'http://192.168.43.249:54468/api/Picture/Gets?pid=' +
        //               response.data[0]);
        //           })
        //       })

        //     })
        // }
      },
      handleAvatarSuccess(res, file) {
        this.headUrl = URL.createObjectURL(file.raw);
      },
      beforeAvatarUpload(file) {
        // const isJPG = file.type === 'image/jpeg';
        const isJPG = true;
        const isLt2M = file.size / 1024 / 1024 < 2;

        if (!isJPG) {
          this.$message.error('上传头像图片只能是 JPG 格式!');
        }
        if (!isLt2M) {
          this.$message.error('上传头像图片大小不能超过 2MB!');
        }
        return isJPG && isLt2M;
      },
      //+++++++++++++++++++向后端发送headUrl sendHeadUrl(){},
      toMoment(momentID) {
        this.$router.push('/main/momentDetail/' + momentID);
      },
      handleSettingClick() {
        this.$router.push('set');
      },
      toFavor() {
        this.dialogFormVisible = false;
      },
      toFavor2() {
        this.dialogFormVisible2 = false;
      },
      //删除收藏夹
      deleteFavor: function (index) {
        // this.favors.splice(index, 1);
        this.axios.get('http://192.168.43.249:54468/api/Collection/DeleteCollection?founder_id=' + this.$store.state.currentUserId_ID +
            '&name=' + index
          )
          .then((response) => {
            if (response.data == 0) {
              this.$message({
                message: '删除成功！',
                type: 'success'
              });
              this.favors.splice(index, 1);
              this.collects = []
            } else if (response.data == 2) {
              this.$message.error('删除失败，请重试！');
            } else {
              this.$message({
                message: '默认收藏夹不允许删除。',
                type: 'warning'
              });
            }
          })
      },
      //删除收藏夹内容
      ///////////////////////
      deleteCollect: function (index, collect) {

        // this.axios.get('http://192.168.43.249:54468/api/Collection/DeleteCollection?founder_id=' + this.$store.state.currentUserId_ID +
        //     '&name=' + index
        //   )
        //   .then((response) => {
        //     if (response == '0') {
        //       // this.$message('删除成功！');
        //       this.$message({
        //         message: '删除成功！',
        //         type: 'success'
        //       });
        //     } else if (response == '2') {
        //       this.$message.error('删除失败，请重试！');
        //     }
        //   })
        this.axios.get('http://192.168.43.249:54468/api/Collect/DeleteCollect?moment_id=' + collect.momentID +
            '&user_id=' + this.$store.state.currentUserId_ID
          )
          .then((response) => {
            if (response.data == 0) {
              // this.collectSrc = require('../image/uncollect.png');
              // this.moment.CollectNum--;
              // this.moment.collectState = !this.moment.collectState;
              this.collects.splice(index - 1, 1);
              (this.favors).foreach((element) => {
                if (element.favorName == currentSelectLeft) {
                  element.collectNum--;
                } else if (currentSelectLeft == '默认收藏夹')
                  this.defaultCollectNum--;
              })
            } else {
              this.$message.error('取消收藏失败，请重试！');
            }
          })
          .catch((error) => {
            console.log(error);
          });

      },
      //新建收藏夹
      finishHandler: function () {
        //this.$refs[formName].validate((valid) => {
        // if (true) {
        // this.favors.unshift({
        //   favorName: this.ruleform.fname,
        //   collectNum: 0,
        // });

        console.log(this.favors)
        this.dialogFormVisible = false;
        // this.ruleform.fname = '';
        this.axios.get('http://192.168.43.249:54468/api/Collection/InsertCollection?name=' + this.ruleform.fname +
            '&founder_id=' + this.$store.state.currentUserId_ID
          )
          .then((response) => {
            if (response.data == 0) {
              this.$message({
                message: '新建成功！',
                type: 'success'
              });
              this.favors.unshift({
                favorName: this.ruleform.fname,
                collectNum: 0,
              });
              this.ruleform.fname = '';

            } else if (response.data == 2) {
              this.$message({
                message: '新建收藏夹失败，请重试！',
                type: 'warning'
              });
              // this.$message.error('新建收藏夹失败，请重试！')
            } else if (response.data == 1) {
              this.$message.error('该名称已被使用，请重试！')
            }
          })

      },
      //重命名收藏夹
      finishHandler2: function (index, name) {
        // this.dialogFormVisible2=true;
        // this.$refs[formName].validate((valid) => {
        //   if (valid) {
        // this.axios.get('http://192.168.43.249:54468/api/Collection/RenameCollection?founder_id='+ this.$store.state.currentUserId_ID+
        //     '&favorName='+ index+
        //     '&fname2='+ this.ruleform2.fname2
        //   )
        //   .then((response) => {
        //     if (response.data == 0) {
        //       this.$message({
        //         message: '编辑成功！',
        //         type: 'success'
        //       });
        //     } else {
        //       this.$message.error('编辑失败，请重试！');
        //     }
        //   })
        //   .catch((error) => {
        //     console.log(error);
        //   })
        // } else {
        //   this.$message.error('内容不合法，请重新输入！')
        // }
        // this.dialogFormVisible2 = false;
        // let j = this.findIndexForFavor(index);
        // this.favors[index].favorName = this.ruleform2.fname2;
        // this.ruleform2.fname2 = '';

        // });

        this.axios.get('http://192.168.43.249:54468/api/Collection/RenameCollection?founder_id=' + this.$store.state.currentUserId_ID +
            '&original_name=' + name +
            '&new_name=' + this.ruleform2.fname2
          )
          .then((response) => {
            if (response.data == 0) {
              this.$message({
                message: '重命名成功！',
                type: 'success'
              });
              this.dialogFormVisible2 = false;
              this.favors[index].favorName = this.ruleform2.fname2;
              this.ruleform2.fname2 = '';
            } else if (response.data == 1) {
              this.$message.error('不可命名为默认收藏夹，请重试！');
            } else if (response.data == 2) {
              // console.log('gaimingchengyicun')
              this.$message.error('该名称已存在，请重试！');
            } else if (response.data == 3) {
              this.$message.error('重命名失败，请重试！');
            }
          })
          .catch((error) => {
            console.log(error);
          })


      },
      //移动收藏夹内容
      // moveHandler() {


      // },
      showFollow() {
        // this.$router.push('follow/', {
        //   userID: this.userID
        // });
        this.followListVisible = true

      },
      showFans() {
        // this.$router.push('fans/', {
        //   userID: this.userID
        // });
        this.fanListVisible = true

      }
    },
    created() {
      this.axios.get('http://192.168.43.249:54468/api/Users/FollowState?from_id=' + this.$store.state.currentUserId_ID +
          '&to_id=' + this.$route.params.id)
        .then((response) => {
          if (response.data == 0) {
            this.FollowState = true;
          } else {
            this.FollowState = false;
          }

        })
      this.axios.get('http://192.168.43.249:54468/api/Users/GetUserInfobyID?id=' + this.$route.params.id)
        .then((response) => {
          this.username = response.data.Username;
          this.desc = response.data.Bio;
        })
      this.axios.get('http://192.168.43.249:54468/api/HomePage/GetMyMoments?Sender_id=' + this.$route.params.id)
        .then((response) => {
          this.moments = response.data;
          console.log(this.moments);

          (this.moments).forEach(element => {
            // element.ID = response.data.ID[index]
            // index++;
            console.log(this.moments)
            element.momentID = element.ID
            this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
              .then((response) => {
                var url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data[0];
                Vue.set(element, 'url', url);
              })
          })
        })
      this.followUsers = [];
      this.followNum = this.followUsers.length

      this.axios.get('http://192.168.43.249:54468/api/Users/FollowList?userID=' + this.$route.params.id)
        .then((response) => {
          if (response.data == 'Not found') {
            this.followUsers = [];
          } else {
            this.followUsers = response.data;
          }
          // this.followUsers = response.data;
          this.followNum = this.followUsers.length
          this.followUsers.forEach(element => {
            element.headImg = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
              element.ID +
              '&type=2';
            // 等待修改
            element.FollowState = true;
            // element.followState = '已关注'
            Vue.set(element, 'followState', '已关注')
            // if (element.FollowState == 'true') {
            //   element.FollowState = true
            //   element.followState = '已关注'
            // } else {
            //   element.FollowState = false
            //   element.followState = '关注'
            // }
          });
        })

      this.fanUsers = [];
      this.fansNum = this.fanUsers.length;
      this.axios.get('http://192.168.43.249:54468/api/Users/FanList?user_id=' + this.$route.params.id)
        .then((response) => {
          if (response.data == 'Not found') {
            this.fanUsers = [];
          } else {
            this.fanUsers = response.data;
          }

          this.fansNum = this.fanUsers.length;
          this.fanUsers.forEach(element => {
            var headImg = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
              element.ID +
              '&type=2';
            Vue.set(element, 'headImg', headImg)
            // 等待修改
            // element.FollowState = 'true';
            if (element.FollowState == 'True') {
              element.FollowState = true
              element.followState = '已关注'
            } else {
              element.FollowState = false
              element.followState = '关注'
            }
          });
        })
      this.favors = []
      this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnUserCollections?user_id=' + this.$store.state.currentUserId_ID)
        .then((response) => {
          // this.favors.favorName = response.data.NAME;
          let totalFavorName = response.data;
          var index = 0;



          this.favors = []

          // if (totalFavorName.length < 1) {
          //   this.favors = []
          // }

          totalFavorName.forEach((element) => {
            var temp = {}
            temp.favorName = element.Name;

            this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnMomentNumInACollection?founder_id=' +
                this
                .$store.state.currentUserId_ID + '&name=' + element.Name)
              .then((response) => {
                temp.collectNum = response.data
              })
            this.favors.push(temp);
            // index++;
          })

          this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnMomentNumInACollection?founder_id=' + this
              .$store
              .state.currentUserId_ID +
              '&name=' + '默认收藏夹')
            .then((response) => {
              this.defaultCollectNum = response.data
            })
        })
        .catch(function (error) {
          console.log(error);
        });

      var headUrl = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
        this.$route.params.id +
        '&type=2';
      Vue.set(this, 'headUrl', headUrl)

      ///////请求个人信息
      // this.username = this.$store.state.currentUsername;
      // this.desc = this.$store.state.currentUserBio;


      this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnCollectionContentID?FounderID=' + this.$store.state
          .currentUserId_ID +
          '&Name=' + '默认收藏夹')
        .then((response) => {
          var momentIDList = response.data;
          var index = 0;


          this.collects = []
          // if (momentIDList < 1) {
          //   this.collects = []
          // }

          momentIDList.forEach(element => {
            var temp = {}
            // this.collects[index].momentID = element;
            temp.momentID = element;
            this.axios.get('http://192.168.43.249:54468/api/Collection/GetFirstPicIDbyMomentID?moment_id=' +
                element)
              .then((response) => {
                //////////////////////////////
                // this.collects[index].url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data;
                var url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data;
                // Vue.set(this.collects[index], 'url', url)
                Vue.set(temp, 'url', url)
                this.collects.push(temp)
              })
          });
          // index++;
        })
      //this.fname2=this.$store.state.;
      /*

            //     this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnMomentNumInACollection', {
            //         founder_id: this.$store.state.currentUserId_ID,
            //         name: '默认收藏夹',
            //       })
            //       .then((response) => {
            //         this.defaultCollectNum = response.data
            //       })
            //   })
            //   .catch(function (error) {
            //     console.log(error);
            //   });

            this.headUrl = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID + '&type=2';
            // //this.fansNum=this.$store.state.;
            // //this.followNum=this.$store.state.;
            this.username = this.$store.state.currentUsername;
            this.desc = this.$store.state.currentUserBio;
            //this.fname2=this.$store.state.;
            /*



                  function getMessageList(){
                    return this.axios.get('/getList');
                  }


                  function getFavorList(){
                    return this.axios.get('http://192.168.43.249:54468/api/ReturnUserCollections',{user_id:this.$store.state.currentUserId_ID});
                  }
                  function getCollectNum(){
                    return axios.get('http://192.168.43.249:54468/api/ReturnMomentNumInACollection',{
                      founder_id:this.$store.state.currentUserId_ID,
                      //=====================================================
                      });
                  }
                  this.$axios.all([getMessageList(),getFavorList(),getCollectNum()])
                    .then(axios.spread(function(msg,favor,collN){
                    //多个请求都成功触发这个函数，多个参数代表两次请求返回的结果
                     
                      //this.userID=msg.data.userID;
                      this.momentNum=msg.data.momentNum;
                      this.moments=msg.data.moments;

                      this.favors.favorName=favor.data;
                      //this.favors.url=favor.data.url;

                      this.favors.collectNum=collN.data;
                    }))
                    .catch((error) => {
                       console.log(error);
                    });*/
    },
    mounted: function () {
      this.$nextTick(function () {
        // Code that will run only after the
        // entire view has been rendered
        // console.log('mouted')
        // setTimeout("backgroundHandler()", 1000);
        var self = this;
        // this.toastrVal = inVal;
        // this.loadState = true;
        // this.noBg = bgState;
        setTimeout(function () {
          self.loadingPage = false;
        }, 1000);
        window.scroll(0, 0);

      })
    },
    beforeRouteEnter(from, to, next) {
      next(vm => {
        vm.$nextTick(function () {
          var self = vm;
          // this.toastrVal = inVal;
          // this.loadState = true;
          // this.noBg = bgState;
          setTimeout(function () {
            self.loadingPage = false;
          }, 1000)

          // Code that will run only after the
          // entire view has been rendered
          // console.log('mouted')
          // window.setTimeout("this.backgroundHandler()", 1000);
        })

      })
    },
    beforeRouteUpdate(to, from, next) {
      this.followListVisible = false;
      this.fanListVisible = false;
      this.axios.get('http://192.168.43.249:54468/api/Users/FollowState?from_id=' + this.$store.state.currentUserId_ID +
          '&to_id=' + to.params.id)
        .then((response) => {
          if (response.data = 0) {
            this.FollowState = true;
          } else {
            this.FollowState = false;
          }

        })
      this.axios.get('http://192.168.43.249:54468/api/Users/GetUserInfobyID?id=' + to.params.id)
        .then((response) => {
          this.username = response.data.Username;
          this.desc = response.data.Bio;
        })
      this.axios.get('http://192.168.43.249:54468/api/HomePage/GetMyMoments?Sender_id=' + to.params.id)
        .then((response) => {
          this.moments = response.data;
          console.log(this.moments);

          (this.moments).forEach(element => {
            // element.ID = response.data.ID[index]
            // index++;
            console.log(this.moments)
            element.momentID = element.ID
            this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
              .then((response) => {
                var url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data[0];
                Vue.set(element, 'url', url);
              })
          })
        })
      this.followUsers = [];
      this.followNum = this.followUsers.length

      this.axios.get('http://192.168.43.249:54468/api/Users/FollowList?userID=' + to.params.id)
        .then((response) => {
          if (response.data == 'Not found') {
            this.followUsers = [];
          } else {
            this.followUsers = response.data;
          }
          // this.followUsers = response.data;
          this.followNum = this.followUsers.length
          this.followUsers.forEach(element => {
            element.headImg = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
              element.ID +
              '&type=2';
            // 等待修改
            element.FollowState = true;
            // element.followState = '已关注'
            Vue.set(element, 'followState', '已关注')
            // if (element.FollowState == 'true') {
            //   element.FollowState = true
            //   element.followState = '已关注'
            // } else {
            //   element.FollowState = false
            //   element.followState = '关注'
            // }
          });
        })

      this.fanUsers = [];
      this.fansNum = this.fanUsers.length;
      this.axios.get('http://192.168.43.249:54468/api/Users/FanList?user_id=' + to.params.id)
        .then((response) => {
          if (response.data == 'Not found') {
            this.fanUsers = [];
          } else {
            this.fanUsers = response.data;
          }

          this.fansNum = this.fanUsers.length;
          this.fanUsers.forEach(element => {
            var headImg = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
              element.ID +
              '&type=2';
            Vue.set(element, 'headImg', headImg)
            // 等待修改
            // element.FollowState = 'true';
            if (element.FollowState == 'True') {
              element.FollowState = true
              element.followState = '已关注'
            } else {
              element.FollowState = false
              element.followState = '关注'
            }
          });
        })
      this.favors = []
      this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnUserCollections?user_id=' + this.$store.state.currentUserId_ID)
        .then((response) => {
          // this.favors.favorName = response.data.NAME;
          let totalFavorName = response.data;
          var index = 0;



          this.favors = []

          // if (totalFavorName.length < 1) {
          //   this.favors = []
          // }

          totalFavorName.forEach((element) => {
            var temp = {}
            temp.favorName = element.Name;

            this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnMomentNumInACollection?founder_id=' +
                this
                .$store.state.currentUserId_ID + '&name=' + element.Name)
              .then((response) => {
                temp.collectNum = response.data
              })
            this.favors.push(temp);
            // index++;
          })

          this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnMomentNumInACollection?founder_id=' + this
              .$store
              .state.currentUserId_ID +
              '&name=' + '默认收藏夹')
            .then((response) => {
              this.defaultCollectNum = response.data
            })
        })
        .catch(function (error) {
          console.log(error);
        });

      this.headUrl = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' +
        to.params.id +
        '&type=2';
      // Vue.set(this, 'headUrl', headUrl)

      ///////请求个人信息
      // this.username = this.$store.state.currentUsername;
      // this.desc = this.$store.state.currentUserBio;


      this.axios.get('http://192.168.43.249:54468/api/Collection/ReturnCollectionContentID?FounderID=' + this.$store.state
          .currentUserId_ID +
          '&Name=' + '默认收藏夹')
        .then((response) => {
          var momentIDList = response.data;
          var index = 0;


          this.collects = []
          // if (momentIDList < 1) {
          //   this.collects = []
          // }

          momentIDList.forEach(element => {
            var temp = {}
            // this.collects[index].momentID = element;
            temp.momentID = element;
            this.axios.get('http://192.168.43.249:54468/api/Collection/GetFirstPicIDbyMomentID?moment_id=' +
                element)
              .then((response) => {
                //////////////////////////////
                // this.collects[index].url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data;
                var url = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + response.data;
                // Vue.set(this.collects[index], 'url', url)
                Vue.set(temp, 'url', url)
                this.collects.push(temp)
                next()
              })
          });
          // index++;
        })

    },
  }
</script>