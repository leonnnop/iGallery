<template>
  <el-col>
  <el-card style="width:70%" :body-style="{padding:'0px'}" :class="navBarFixed == true ? 'navBarWrap1' :''"> 
    <el-row style="background-color:#ffffff;padding-bottom:10px" class="clearfix">
      <!--el-row>
           <el-button style="background-color:#c1c4c9" @click="handleSettingClick" size="small">设置<i class="el-icon-setting el-icon--right"></i></el-button>
         </el-col>
      </el-row--> 
      <el-row>
        <el-col :span="6" :offset="1" style="padding-top:20px"> 
          <el-upload
            class="avatar-uploader"
            action="https://jsonplaceholder.typicode.com/posts/"
            :show-file-list="false"
            :on-success="handleAvatarSuccess"
            :before-upload="beforeAvatarUpload">
            <img :src="headUrl" class="headImg" alt="头像">
          </el-upload>
        </el-col>
        <el-col :span="17" style="padding-top:20px">
          <el-row>
            <el-button type="text" id="username">{{username}}</el-button>
          </el-row>
          <el-row>
            <el-button type="text" class="num" @click="showFollow">关注:{{followNum}}</el-button>
            <el-button type="text" class="num" @click="showFans">粉丝:{{fansNum}}</el-button>
          </el-row>
          <el-row>
            <p style="font-size:13px;color:#6c6b6e">个人简介：{{desc}}</p>
          </el-row>
        </el-col>
      </el-row>
    </el-row>
    <el-row style="padding:0 10px">
      <el-menu 
      default-active="dynamic" 
      class="el-menu-demo" 
      mode="horizontal" 
      @select="handleSelectTop"
      active-text-color="#409eff">
        <el-menu-item index="dynamic">我的动态
          <span style="font-size:12px;color:#000009">{{momentNum}}</span>
        </el-menu-item>
        <el-menu-item index="favors">收藏夹
          <span style="font-size:12px;color:#000009">{{favors.length+1}}</span></el-menu-item>
        <el-menu-item index="set">设置</el-menu-item>
      </el-menu>
    </el-row>
  <el-row v-if="isDynamic" style="padding-left:30px;padding-top:15px">
    <el-col v-for="moment in moments" :key=moment.name :span="6">
      <el-col>
        <!--el-card shadow="always" :body-style="{ padding: '0px' }"-->
          <div class="moments" :style="{backgroundImage:'url('+moment.url+')'}" @click="toMoment"></div>
        <!--/el-card-->
      </el-col>
     </el-col>
  </el-row>
  <el-row v-if="isFavors">
    <!--el-row  v-if="isFavor">
            <el-row style="background-color:#dedfe6">
              <el-col :span="12" :offset="9">
                <div style="padding:10px;">收藏夹内容仅自己可见</div>
              </el-col>
              <el-col :span="2">
                <el-button  type="text" style="font-size:15px" @click="dialogFormVisible=true">新建收藏夹</el-button>
              </el-col>
              <el-dialog title="新建收藏夹" :visible.sync="dialogFormVisible">
                  <el-form ref="form" :model="ruleform" :rules="rules">
                    <el-form-item label="名称：" :label-width="formLabelWidth" prop="fname">
                      <el-input v-model="ruleform.fname" auto-complete="off" style="width:200px"></el-input>
                    </el-form-item>
                  </el-form>
                  <div slot="footer" class="dialog-footer">
                    <el-button @click="cancelHandler">取 消</el-button>
                    <el-button type="primary" @click="finishHandler('ruleform')">确 定</el-button>
                  </div>
              </el-dialog>
            </el-row>
            <el-row>
              <el-col v-for="(favor,index) in favors" :key="index" :span="6">
                <el-col class="favor">
                <div style="header">
                  <el-card shadow="always" :body-style="{ padding: '0px' }">
                    <img :src="favor.url" class="img" alt="收藏夹封面" @click="toCollect"/>
                    <el-row>
                      <el-col :span="17" :offset="1">
                        <el-tooltip effect="dark" :content="favor.favorName" placement="bottom-end">
                          <el-button type="text" @click="toCollect">{{favor.favorName+'('+favor.collectNum+')'}}</el-button>
                        </el-tooltip>
                      </el-col>
                      <el-col :span="6">
                        <el-dropdown @command="handleCommand" v-if="favor.favorName!='默认收藏夹'">
                          <span class="el-dropdown-link">
                            <i class="el-icon-arrow-down el-icon--right"></i>
                          </span>
                          <el-dropdown-menu slot="dropdown">
                            <el-dropdown-item command="del">删除</el-dropdown-item>
                            <el-dropdown-item command="change" divided>编辑名称</el-dropdown-item>
                          </el-dropdown-menu>
                        </el-dropdown>
                        
                      </el-col>
                    </el-row>
                  </el-card>
                </div>
                </el-col>
              </el-col>
            </el-row>
    </el-row>
    <el-row v-if="isCollect">
            <el-row>
              <el-button type="text" style="font-size:15px;padding-left:15px" @click="backToFavors">返回</el-button>
            </el-row>
            <el-row>
              <el-col v-for="collect in collects.collectList" :key=collect.name :span="6">
                <el-col class="moments">
                    <img :src="collect.url" class="img" @click="toMoment('momentID')" alt="动态的图片"/>
                </el-col>
              </el-col>
            </el-row>
    </el-row-->
    
    <el-col :span="5">
      <el-menu
      default-active="默认收藏夹"
      class="el-menu-vertical-demo"
      @select="handleSelectLeft"
      collapse-transition="false">
      <!--el-submenu index="1"-->
      <el-menu-item index="title">
        <div slot="title">
          <el-row>
            <el-col :span="21" :offset="0"><span style="font-size:16px;color:#bbbbbb">动态收藏</span></el-col>
            <el-col :span="1"><i class="el-icon-circle-plus-outline" @click="dialogFormVisible=true"></i></el-col>
            <el-dialog title="新建收藏夹" :visible.sync="dialogFormVisible" class="dialog">
                  <el-form ref="form" :model="ruleform" :rules="rules">
                    <el-form-item label="名称：" :label-width="formLabelWidth" prop="fname">
                      <el-input v-model="ruleform.fname" auto-complete="off" style="width:200px" placeholder="最大长度为15个字符"></el-input>
                    </el-form-item>
                  </el-form>
                  <div  slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="finishHandler('ruleform')" size="middle">确定</el-button>
                    <el-button @click="dialogFormVisible=false" size="middle">取消</el-button></div>
            </el-dialog>
          </el-row>
        </div>
      </el-menu-item>
      <el-menu-item index="默认收藏夹">
            <el-col :span="18">
              <el-row>
                <el-col :span="7"><img style="width:60%" src="../image/heart.png"/></el-col>
                <el-col :span="17">{{'默认收藏夹'+'('+defaultCollectNum+')'}}</el-col>
              </el-row>
            </el-col>
      </el-menu-item>
      <el-row v-for="favor in favors" :key=favor.name>
          <el-menu-item :index="favor.favorName">
            <el-row v-if="favor.favorName!='默认收藏夹'">
            <el-col :span="18">
              <el-row>
                <el-col :span="7"><img style="width:60%" src="../image/folder.png"/></el-col>
                <el-col :span="17">{{favor.favorName+'('+favor.collectNum+')'}}</el-col>
              </el-row>
            </el-col>
            <el-col :span="1">
            <el-dropdown>
                <span class="el-dropdown-link">
                  <i class="el-icon-more el-icon--right"></i>
                </span>
                <el-dropdown-menu slot="dropdown">
                  <el-dropdown-item @click.native="deleteFavor">删除</el-dropdown-item>
                  <el-dropdown-item @click.native="dialogFormVisible2=true" divided>编辑名称</el-dropdown-item>
                  <el-dialog title="编辑名称" :visible.sync="dialogFormVisible2" class="dialog" :modal-append-to-body="false">
                   <el-form ref="form2" :model="ruleform2" :rules="rules2">
                    <el-form-item label="名称：" :label-width="formLabelWidth" prop="fname2">
                      <el-input v-model="ruleform2.fname2" auto-complete="off" style="width:200px"></el-input>
                    </el-form-item>
                   </el-form>
                  <div  slot="footer" class="dialog-footer">
                    <el-button type="primary" @click="finishHandler2('ruleform2','index')" size="middle">确 定</el-button>
                    <el-button @click="dialogFormVisible2=false" size="middle">取 消</el-button></div>
                </el-dialog>
                </el-dropdown-menu>
            </el-dropdown>
            </el-col>
          </el-row>
          </el-menu-item>
      </el-row>
    </el-menu>
    </el-col>
    <el-col :span="19" style="padding-left:30px;padding-top:15px">
      <el-col v-for="collect in collects" :key=collect.name :span="8">
        <el-col >
          <div class="moments" :style="{backgroundImage:'url('+collect.url+')'}" @click="toMoment"></div>
            <!--el-col :span="2">
              <el-dropdown >
                <span class="el-dropdown-link">
                   <i class="el-icon-more"></i>
                </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item @click.native="deleteCollect">删除收藏</el-dropdown-item>
                <el-dropdown-item @click.native="dialogFormVisible3=true">移动</el-dropdown-item>
                <el-dialog title="移动" :visible.sync="dialogFormVisible3" class="checkForm" :modal-append-to-body="false">
                  <el-checkbox-group v-model="checkedFavor" :min="1" :max="1">
                    <ul><li>
                      <el-checkbox v-for="favor in favors" :label="favor.favorName" :key="favor"><span>{{favor.favorName}}</span></el-checkbox> 
                    </li></ul>
                  </el-checkbox-group>
                  <div slot="footer" class="dialog-footer">
                      <el-button type="primary" @click="moveHandler" size="middle">确认</el-button>
                      <el-button @click="dialogFormVisible3=false" size="middle">取消</el-button>
                  </div>                     
                </el-dialog>
              </el-dropdown-menu>
              </el-dropdown>
            </el-col-->
          
        </el-col>
      </el-col>
    </el-col>
    </el-row>
  </el-card>
  </el-col>
</template>

<style>
  .headImg{
    width:178px;
    height:178px;
    border-radius:83px;
    cursor:pointer;
  }
  .headImg:hover{
    opacity: 0.8;
  }
  .navBarWrap1 {
      margin-left:auto;
      margin-right:auto;
      margin-top:10px;
      margin-bottom: 10px;
  /*
    top: 200px;
    z-index: 999;*/
  }
  .el-tabs__item{
    font-size:15px;
    margin:10px;
  }
  .moments{
    display:block;
    width:200px;
    height:200px;
    margin:10px 0;
    /*background-image: url('../image/ins1.png');*/
    border:1px;
    background-image:url('../image/ins1.png');
    background-repeat:no-repeat;
    background-size:cover;
    -webkit-background-size:cover;
    -moz-background-size:cover;
    -o-background-size:cover;
  }
  .img{
    display:block;
    width:200px;
    height:200px;
    border-radius:5px;
  }
  .moments:hover{
    opacity: 0.85; 
  }
  /*
  .favor{
    display:block;
    width:200px;
    height:245px;
    margin:15px;
  }*/
  .icon{
    margin-top:-5px;
  }
  .dialog{
    margin-left:auto;
    margin-right: auto;
    display:justify;
    width:60%;
  }
  .checkForm{
    
    margin-left:auto;
    margin-right: auto;
    width:40%;
  }
  .cards{
      margin-left:auto;
      margin-right:auto;
      margin-top:10px;
      margin-bottom: 10px;
  }
  #username{
      font-size: 18px;
      padding-top: 30px;
  }
  .num{
      color:#4e4c4c;
      font-size:14px;
      padding-right:10px;
  }
  .el-dropdown-link {
    cursor: pointer;
    color: #409EFF;
    padding-left:20px;
  }
  .el-icon-arrow-down {
    font-size: 20px;
  }
</style>

<script>
  import qs from 'qs'
  
  export default {
    name:'personalpage',
    data(){
      var validateName = (rule, value, callback) => {
        if (value === '') {
          return callback(new Error('请输入名称'));
        } else {
          return callback();
        }
      };
      return{
        defaultCollectNum:0,
        activeIndex:'1',
        navBarFixed:true,
        checkedFavor: '默认收藏夹',
        headUrl:require('../image/a.jpg'),
        username:'初始昵称',
        followNum:0,
        fansNum:0,
        desc:'感觉头发被掉光。',
        momentNum:0,
        moments:[{
            momentID:'0',
            url: require('../image/a.jpg'),
            text:'zero'
          },{
            momentID:'1',
            url: require('../image/ins1.png'),
            text:'first'
          },{
            momentID:'2',
            url: require('../image/ins2.png'),
            text:'second'
          },{
            momentID:'3',
            url: require('../image/ins3.png'),
            text:'third'
          },{
            momentID:'4',
            url: require('../image/ins_ex.jpg'),
            text:'forth'
          }],
        favors:[//收藏夹信息,
          {
            //url: require('../image/gaojin_ciyun.png'),
            favorName:'MyCollect',
            collectNum:1,
          },{
            //url: require('../image/gaojin_radar.png'),
            favorName:'Something',
            collectNum:2,
          },{
            //url: require('../image/a.jpg'),
            favorName:'Anything',
            collectNum:3,
          },{
            //url: require('../image/a.jpg'),
            favorName:'Nothing',
            collectNum:4,
          }],
        collects:[{//收藏夹内的动态
            momentID:'',
            url: require('../image/a.jpg'),
            text:''
          },{
            momentID:'',
            url: require('../image/ins1.png'),
            text:''
          },{
            momentID:'',
            url: require('../image/ins2.png'),
            text:''
          },{
            momentID:'',
            url: require('../image/ins3.png'),
            text:''
          },{
            momentID:'',
            url: require('../image/ins_ex.jpg'),
            text:''
          }],
        ruleform: {
          fname: ''
        },
        rules:{
          fname:[            
            { validator: validateName,
              message:'请输入名称',
              trigger: 'blur' 
            },{
              min: 1,
              max: 15,
              message: '名称长度为1~15',
              trigger: 'blur'
            }
          ]
        },
        ruleform2: {
          fname2: ''
        },
        rules2:{
          fname2:[          
            { validator: validateName,
              message:'请输入新名称',
              trigger: 'blur' 
            },{
              min: 1,
              max: 15,
              message: '名称长度为1~15',
              trigger: 'blur'
            }
          ]
        },
        formLabelWidth: '100px',
        //isFavor:true,
        //isCollect:false,
        isDynamic:true,
        isFavors:false,
        dialogFormVisible:false,
        dialogFormVisible2:false,
        dialogFormVisible3:false,
      }
    },
    methods: {
       handleSelectTop(key,keypath) {
        console.log('/', key);
        
        if(key=='dynamic'){
          this.isDynamic=true;
          this.isFavors=false;
        }else if(key=='favors'){
          this.isDynamic=false;
          this.isFavors=true;
        }else{
          this.$router.push('/main/'+key);
        }
        //this.$router.push('/main/' + key);
      },
      handleSelectLeft(key,keypath){
        this.$axios.get('http://10.0.1.8:54468/api/Users/ReturnCollectionContentID',{
          FounderID:this.$store.state.currentUseId_ID,
          Name:key
          })
        .then((response=>{
          this.collects.momentID=response.data
          //===============

        }))
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
      toMoment(key){
        this.$router.push('momentDetail/'+key);
      },
      handleSettingClick(){
        this.$router.push('set');
      },
      toFavor() {
        this.dialogFormVisible=false;
      },
      toFavor2(){
        this.dialogFormVisible2=false;
      },
      deleteFavor:function(index){
        this.$axios.post('http://10.0.1.8:54468/api/Collection/DeleteCollection',{
          founder_id:this.$store.state.currentUseId_ID,
          name:index,
        })
        .then((response)=>{
          if(response=='0'){
            this.$message('删除成功！');
          }else if(response=='2'){
            this.$message.error('删除失败，请重试！');
          }
        })
      },
      deleteCollect(){

      },
      //新建收藏夹
      finishHandler:function(formName) {
          //this.$refs[formName].validate((valid) => {
            if (this.ruleForm.fname) {
              this.favors.unshift({
              favorName:this.ruleform.fname,
              collectNum:0,
              });
              //this.dialogFormVisible=false;
              this.axios.post('http://10.0.1.8:54468/api/Collection/InsertCollection/',{
                name:this.ruleForm.fname,
                founder_id:this.$store.state.currentUseId_ID,
                })
              .then((response) => {
                  if (response==0) {
                    this.$message({
                    message: '新建收藏夹成功！',
                    type: 'success'
                  })
                  this.ruleform.fname='';
                  }else if(response==2){
                    this.$message.error('新建收藏夹失败，请重试！')
                  }else if(response==1){
                    this.$message.error('该名称已被使用，请重试！')
                  }
              })
              .catch((error) => {
                  console.log(error);
              })
          } else {
            this.$message.error('内容不合法，请重新输入！')
          }
        //});
        this.dialogFormVisible=false;
      },
      //重命名收藏夹
      finishHandler2:function(formName,index) {
           // this.dialogFormVisible2=true;
        this.$refs[formName].validate((valid) => {
            if (this.ruleForm2.fname2) {
              this.axios.post('/',{
                favorName:this.favors[index-1].favorName,
                fname2:this.ruleForm2.fname2,
              })
              .then((response) => {
                  if (response) {
                    this.$message({
                    message: '编辑成功！',
                    type: 'success'
                  });
                  }else {
                    this.$message.error('编辑失败，请重试！');
                  }
              })
              .catch((error) => {
                  console.log(error);
              })
          } else {
              this.$message.error('内容不合法，请重新输入！')
          }
              this.favors[index-1].favorName=formName.fname2;
              this.dialogFormVisible2=false;
              this.ruleform2.fname2='';
              
        });
      },
      //移动收藏夹内容
      moveHandler(){
        

      },
      showFollow(){
        this.$router.push('follow/',{userID:this.userID});
      },
      showFans(){
        this.$router.push('fans/',{userID:this.userID});
      }
    },
    created() {
      this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnUserCollections',{
        user_id:this.$store.state.currentUseId_ID})
        .then((response)=>{
          this.favors.favorName=response.data.NAME;
          let totalFavorName=response.data;
          var index=0;

          totalFavorName.foreach((element)=>{
            element.NAME=response.data[index].NAME;
            
            this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnMomentNumInACollection',{
              founder_id:this.$store.state.currentUseId_ID,
              name:element.NAME,
            })
            .then((response)=>{
              this.favors[index].collectNum=response.data
            })
            index++;
          })

          this.axios.get('http://10.0.1.8:54468/api/Collection/ReturnMomentNumInACollection',{
              founder_id:this.$store.state.currentUseId_ID,
              name:'默认收藏夹',
            })
            .then((response)=>{
              this.defaultCollectNum=response.data
            })
        })
        .catch(function(error){
          console.log(error);
        });

        //this.headUrl=this.$store.state.;
        //this.fansNum=this.$store.state.;
        //this.followNum=this.$store.state.;
        this.name=this.$store.state.currentUsername;
        this.desc=this.$store.state.currentUserBio;
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
