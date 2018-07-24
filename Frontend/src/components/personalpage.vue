<template>
  <el-col>
    <el-card style="width:70%" :body-style="{padding:'0px'}" :class="navBarFixed == true ? 'navBarWrap1' :''" shadow="never">
      <el-row style="background-color:#ffffff;padding-bottom:10px" class="clearfix">
        <!--el-row>
           <el-button style="background-color:#c1c4c9" @click="handleSettingClick" size="small">设置<i class="el-icon-setting el-icon--right"></i></el-button>
         </el-col>
      </el-row-->
        <el-row>
          <el-col :span="6" :offset="1" style="margin:20px;padding-left:30px">
            <el-upload class="avatar-uploader" action="https://jsonplaceholder.typicode.com/posts/" :show-file-list="false" :on-success="handleAvatarSuccess"
              :before-upload="beforeAvatarUpload">
              <img :src="headUrl" class="headImg" alt="头像">
            </el-upload>
          </el-col>
          <el-col :span="17" style="padding-top:20px">
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
                        <img :src="followUser.headImg" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(followUser.userPage)">
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
                        <img :src="fanUser.headImg" alt="" class="show-comment-img hover-cursor" @click="jumpToUser(fanUser.userPage)">
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
        </el-row>
      </el-row>
      <!-- </el-row> -->
      <el-row style="padding:0 10px">
        <el-menu default-active="dynamic" class="el-menu-demo" mode="horizontal" @select="handleSelectTop" active-text-color="#409eff">
          <el-menu-item index="dynamic">我的动态
            <span style="font-size:12px;color:#000009">{{moments.length}}</span>
          </el-menu-item>
          <el-menu-item index="favors">收藏夹
            <span style="font-size:12px;color:#000009">{{favors.length+1}}</span>
          </el-menu-item>
          <el-menu-item index="set">设置</el-menu-item>
        </el-menu>
      </el-row>
      <el-row v-if="isDynamic" style="padding-left:30px;padding-top:15px">
        <el-col v-for="moment in moments" :key="moment.name" :span="6">
          <el-col>
            <!--el-card shadow="always" :body-style="{ padding: '0px' }"-->
            <div class="moments" :style="{backgroundImage:'url('+moment.url+')'}" @click="toMoment(moment.momentID)"></div>
            <!--/el-card-->
          </el-col>
        </el-col>
      </el-row>
      <el-row v-if="isFavors">
        <el-col :span="5">
          <el-menu default-active="默认收藏夹" class="el-menu-vertical-demo" @select="handleSelectLeft" collapse-transition="false">
            <!--el-submenu index="1"-->
            <el-menu-item index="title">
              <div slot="title">
                <el-row>
                  <el-col :span="21" :offset="0">
                    <span style="font-size:16px;color:#bbbbbb">动态收藏</span>
                  </el-col>
                  <el-col :span="1">
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
                      <el-button type="primary" @click="finishHandler('ruleForm')" size="middle">确定</el-button>
                      <el-button @click="dialogFormVisible=false;ruleform.fname=''" size="middle">取消</el-button>
                    </div>
                  </el-dialog>
                </el-row>
              </div>
            </el-menu-item>
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
              <el-menu-item :index="index">
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
                    <el-dropdown>
                      <span class="el-dropdown-link">
                        <i class="el-icon-more el-icon--right"></i>
                      </span>
                      <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item @click.native="deleteFavor(index)">删除</el-dropdown-item>
                        <el-dropdown-item @click.native="dialogFormVisible2=true" divided>编辑名称</el-dropdown-item>
                        <el-dialog title="" :visible.sync="dialogFormVisible2" class="dialog" :modal-append-to-body="false">
                          <div slot="title">重命名收藏夹</div>
                          <el-form ref="form2" :model="ruleform2" :rules="rules2">
                            <el-form-item label="名称：" :label-width="formLabelWidth" prop="fname2">
                              <el-input v-model="ruleform2.fname2" auto-complete="off" style="width:200px" :value="favor.favorName"></el-input>
                            </el-form-item>
                          </el-form>
                          <div slot="footer" class="dialog-footer">
                            <el-button type="primary" @click="finishHandler2('ruleform2','index')" size="middle">确 定</el-button>
                            <el-button @click="dialogFormVisible2=false;ruleform2.fname2=''" size="middle">取 消</el-button>
                          </div>
                        </el-dialog>
                      </el-dropdown-menu>
                    </el-dropdown>
                  </el-col>
                </el-row>
              </el-menu-item>
            </el-row>
            <!-- <el-menu-item index="set">设置</el-menu-item> -->

          </el-menu>
        </el-col>
        <el-col :span="19" style="padding-left:30px;padding-top:15px">
          <el-col v-for="(collect,index) in collects" :key="index" :span="8">
            <el-col>
              <div class="moments" :style="{backgroundImage:'url('+collect.url+')'}" @click="toMoment(collect.momentID)"></div>
              <el-col :span="2">
                <el-dropdown>
                  <span class="el-dropdown-link">
                    <i class="el-icon-more"></i>
                  </span>
                  <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item @click.native="deleteCollect('index')">删除收藏</el-dropdown-item>
                    <el-dropdown-item @click.native="dialogFormVisible3=true">移动</el-dropdown-item>
                    <el-dialog title="" :visible.sync="dialogFormVisible3" class="checkForm" :modal-append-to-body="false">
                      <div slot="title">移动</div>
                      <el-checkbox-group v-model="checkedFavor" :min="1" :max="2">
                        <div style="padding:10px,10px">
                          <el-checkbox chcecked="false" key="默认" label="默认">
                            <span style="font-size:15px">默认收藏夹</span>
                          </el-checkbox>
                        </div>
                        <div v-for="favor in favors" :key="favor.favorName" :label="favor.favorName" style="padding:10px,10px">
                          <el-checkbox chcecked="false" :label="favor.favorName">
                            <span style="font-size:15px">{{favor.favorName}}</span>
                          </el-checkbox>
                        </div>
                      </el-checkbox-group>
                      <div slot="footer" class="dialog-footer">
                        <el-button type="primary" @click="moveHandler('key')" size="middle">确认</el-button>
                        <el-button @click="dialogFormVisible3=false" size="middle">取消</el-button>
                      </div>
                    </el-dialog>
                  </el-dropdown-menu>
                </el-dropdown>
              </el-col>
            </el-col>
          </el-col>

        </el-col>
      </el-row>
      <!-- <el-row v-if="isDynamic" style="padding-left:30px;padding-top:15px">
        <el-col v-for="moment in moments" :key="moment.name" :span="6">
          <el-col>
            <div class="moments" :style="{backgroundImage:'url('+moment.url+')'}" @click="toMoment(moment.momentID)"></div>
          </el-col>
        </el-col>
      </el-row> -->
      <!-- </el-menu> -->

      <!-- <el-col :span="19" style="padding-left:30px;padding-top:15px">
        <el-col v-for="(collect,index) in collects" :key="index" :span="8">
          <el-col>
            <div class="moments" :style="{backgroundImage:'url('+collect.url+')'}" @click="toMoment(collect.momentID)"></div>
            <el-col :span="2">
              <el-dropdown>
                <span class="el-dropdown-link">
                  <i class="el-icon-more"></i>
                </span>
                <el-dropdown-menu slot="dropdown">
                  <el-dropdown-item @click.native="deleteCollect('index')">删除收藏</el-dropdown-item>
                  <el-dropdown-item @click.native="dialogFormVisible3=true">移动</el-dropdown-item>
                  <el-dialog title="" :visible.sync="dialogFormVisible3" class="checkForm" :modal-append-to-body="false">
                    <div slot="title">移动</div>
                    <el-checkbox-group v-model="checkedFavor" :min="1" :max="2">
                      <div style="padding:10px,10px">
                        <el-checkbox chcecked="false" key="默认" label="默认">
                          <span style="font-size:15px">默认收藏夹</span>
                        </el-checkbox>
                      </div>
                      <div v-for="favor in favors" :key="favor.favorName" :label="favor.favorName" style="padding:10px,10px">
                        <el-checkbox chcecked="false" :label="favor.favorName">
                          <span style="font-size:15px">{{favor.favorName}}</span>
                        </el-checkbox>
                      </div>
                    </el-checkbox-group>
                    <div slot="footer" class="dialog-footer">
                      <el-button type="primary" @click="moveHandler('key')" size="middle">确认</el-button>
                      <el-button @click="dialogFormVisible3=false" size="middle">取消</el-button>
                    </div>
                  </el-dialog>
                </el-dropdown-menu>
              </el-dropdown>
            </el-col>
          </el-col>
        </el-col>

      </el-col> -->
    </el-card>
  </el-col>
</template>

<style>
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
    /*
    top: 200px;
    z-index: 999;*/
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

  .el-icon-arrow-down {
    font-size: 20px;
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
      }
    },
    methods: {
      // Array:prototype.contains=function(val){
      //   for(var i=0;i<this.favors.length;i++){
      //     if(this.favors[i].favoeName==val)
      //     return i;
      //   }
      // },
      followHandler: function (user, FollowState) {
        console.log(user)
        console.log(this.$store.state.currentUserId_ID)
        console.log(user.FollowState);
        //////////////
        if (!user.FollowState) {
          user.followState = '已关注';
        } else {
          user.followState = '关注';
        }
        user.FollowState = !user.FollowState;
        console.log(user.FollowState);

        this.axios.get('http://10.0.1.8:54468/api/Users/Follow?followID=' + this.$store.state.currentUserId_ID +
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
        if (key == '默认收藏夹') {
          this.$axios.post('http://10.0.1.8:54468/api/Users/ReturnCollectionContentID', {
              FounderID: this.$store.state.currentUserId_ID,
              Name: key
            })
            .then((response) => {
              this.collects = response.data
              //===============
              let totalMoments = response.data;
              totalMoments.foreach((element) => {
                this.axios.get('http://10.0.1.8:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
                  .then((responsed) => {
                    Vue.set(element, 'url',
                      'http://10.0.1.8:54468/api/Picture/Gets?pid=' +
                      response.data[0]);
                  })
              })
            })
        } else if (key != 'title') {
          this.$axios.post('http://10.0.1.8:54468/api/Users/ReturnCollectionContentID', {
              FounderID: this.$store.state.currentUserId_ID,
              Name: this.favors[key].favorName
            })
            .then((response) => {
              this.collects = response.data
              //===============
              let totalMoments = response.data;
              totalMoments.foreach((element) => {
                this.axios.get('http://10.0.1.8:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
                  .then((responsed) => {
                    Vue.set(element, 'url',
                      'http://10.0.1.8:54468/api/Picture/Gets?pid=' +
                      response.data[0]);
                  })
              })

            })
        }
      },
      handleAvatarSuccess(res, file) {
        this.headUrl = URL.createObjectURL(file.raw);
      },
      beforeAvatarUpload(file) {
        const isJPG = file.type === 'image/jpeg';
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
        this.$router.push('momentDetail/' + momentID);
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
        this.favors.splice(index, 1);
        this.$axios.post('http://10.0.1.8:54468/api/Collection/DeleteCollection', {
            founder_id: this.$store.state.currentUserId_ID,
            name: this.favors[index].favorName,
          })
          .then((response) => {
            if (response == '0') {
              this.$message('删除成功！');
            } else if (response == '2') {
              this.$message.error('删除失败，请重试！');
            }
          })
      },
      //删除收藏夹内容
      deleteCollect: function (index) {
        this.collects.splice(index - 1, 1);
        this.$axios.post('http://10.0.1.8:54468/api/Collection/DeleteCollection', {
            userid: this.$store.state.currentUserId_ID,
            momentid: this.collects[index - 1].momentID,
          })
          .then((response) => {
            if (response == '0') {
              this.$message('删除成功！');
            } else if (response == '2') {
              this.$message.error('删除失败，请重试！');
            }
          })

      },
      //新建收藏夹
      finishHandler: function (formName) {
        //this.$refs[formName].validate((valid) => {
        if (true) {
          this.favors.unshift({
            favorName: this.ruleform.fname,
            collectNum: 0,
          });
          this.dialogFormVisible = false;
          this.ruleform.fname = '';
          this.axios.post('http://10.0.1.8:54468/api/Collection/InsertCollection/', {
            name: this.ruleForm.fname,
            founder_id: this.$store.state.currentUserId_ID,
          })
          this.ruleform.fname = '';
        } else if (response == 2) {
          this.$message.error('新建收藏夹失败，请重试！')
        } else if (response == 1) {
          this.$message.error('该名称已被使用，请重试！')
        }
      },
      //重命名收藏夹
      finishHandler2: function (formName, index) {
        // this.dialogFormVisible2=true;
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.axios.post('http://10.0.1.8:54468/api/Collection/RenameCollection', {
                founderid: this.$store.state.currentUserId_ID,
                favorName: index,
                fname2: this.formName.fname2,
              })
              .then((response) => {
                if (response) {
                  this.$message({
                    message: '编辑成功！',
                    type: 'success'
                  });
                } else {
                  this.$message.error('编辑失败，请重试！');
                }
              })
              .catch((error) => {
                console.log(error);
              })
          } else {
            this.$message.error('内容不合法，请重新输入！')
          }
          this.dialogFormVisible2 = false;
          let j = this.findIndexForFavor(index);
          this.favors[j].favorName = formName.fname2;
          this.formName.fname2 = '';

        });
      },
      //移动收藏夹内容
      moveHandler() {


      },
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
      this.axios.get('http://10.0.1.8:54468/api/HomePage/GetMyMoments?Sender_id=' + this.$store.state.currentUserId_ID)
        .then((response) => {
          this.moments = response.data;
          console.log(this.moments);

          (this.moments).forEach(element => {
            // element.ID = response.data.ID[index]
            // index++;
            console.log(this.moments)
            element.momentID = element.ID
            this.axios.get('http://10.0.1.8:54468/api/Picture/FirstGet?id=' + element.momentID + '&type=1')
              .then((response) => {
                var url = 'http://10.0.1.8:54468/api/Picture/Gets?pid=' + response.data[0];
                Vue.set(element, 'url', url);
              })
          })
        })

      this.axios.get('http://10.0.1.8:54468/api/Users/FollowList?userID=' + this.$store.state.currentUserId_ID)
        .then((response) => {
          this.followUsers = response.data;
          this.followNum = this.followUsers.length
          this.followUsers.forEach(element => {
            element.headImg = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' +
              element.ID +
              '&type=2';
            // 等待修改
            element.FollowState = 'true';
            if (element.FollowState == 'true') {
              element.followState = '已关注'
            } else {
              element.followState = '关注'
            }
          });
        })

      this.axios.get('http://10.0.1.8:54468/api/Users/FanList?user_id=' + this.$store.state.currentUserId_ID)
        .then((response) => {
          this.fanUsers = response.data;
          this.fansNum = this.fanUsers.length;
          this.fanUsers.forEach(element => {
            var headImg = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' +
              element.ID +
              '&type=2';
            Vue.set(element, 'headImg', headImg)
            // 等待修改
            // element.FollowState = 'true';
            if (element.FollowState == 'True') {
              element.followState = '已关注'
            } else {
              element.followState = '关注'
            }
          });
        })

      this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnUserCollections', {
          user_id: this.$store.state.currentUserId_ID
        })
        .then((response) => {
          this.favors.favorName = response.data.NAME;
          let totalFavorName = response.data;
          var index = 0;

          totalFavorName.foreach((element) => {
            element.NAME = response.data[index].NAME;

            this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnMomentNumInACollection', {
                founder_id: this.$store.state.currentUserId_ID,
                name: element.NAME,
              })
              .then((response) => {
                this.favors[index].collectNum = response.data
              })
            index++;
          })

          this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnMomentNumInACollection', {
              founder_id: this.$store.state.currentUserId_ID,
              name: '默认收藏夹',
            })
            .then((response) => {
              this.defaultCollectNum = response.data
            })
        })
        .catch(function (error) {
          console.log(error);
        });

      this.headUrl = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' +
        this.$store.state.currentUserId_ID +
        '&type=2';
      //this.fansNum=this.$store.state.;
      //this.followNum=this.$store.state.;
      this.username = this.$store.state.currentUsername;
      this.desc = this.$store.state.currentUserBio;
      //this.fname2=this.$store.state.;
      /*

            //     this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnMomentNumInACollection', {
            //         founder_id: this.$store.state.currentUseId_ID,
            //         name: '默认收藏夹',
            //       })
            //       .then((response) => {
            //         this.defaultCollectNum = response.data
            //       })
            //   })
            //   .catch(function (error) {
            //     console.log(error);
            //   });

            this.headUrl = 'http://10.0.1.8:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID + '&type=2';
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
                    return this.axios.get('http://10.0.1.8:54468/api/ReturnUserCollections',{user_id:this.$store.state.currentUseId_ID});
                  }
                  function getCollectNum(){
                    return axios.get('http://10.0.1.8:54468/api/ReturnMomentNumInACollection',{
                      founder_id:this.$store.state.currentUseId_ID,
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
    }
  }
</script>