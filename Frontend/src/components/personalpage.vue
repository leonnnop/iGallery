<template>
  <el-card id="cards" style="width:70%" :body-style="{padding:'0px'}"> 
    <el-row style="background-color:#e4e7ed;padding-bottom:10px">
      <el-row>
         <el-col :span="1" :offset="22">
           <el-button style="background-color:#dcdfe6" @click="handleSettingClick">设置<i class="el-icon-setting el-icon--right"></i></el-button>
         </el-col>
      </el-row> 
      <el-row>
        <el-col :span="6" :offset="1"> 
          <el-upload
            class="avatar-uploader"
            action="https://jsonplaceholder.typicode.com/posts/"
            :show-file-list="false"
            :on-success="handleAvatarSuccess"
            :before-upload="beforeAvatarUpload">
            <img :src="headUrl" style="width:178px;height:178px;border-radius:83px;;cursor:pointer" alt="头像">
          </el-upload>
        </el-col>
        <el-col :span="17" style="padding-top:20px">
          <el-row>
            <el-button type="text" id="name">{{name}}</el-button>
          </el-row>
          <el-row>
            <el-button type="text" class="num" @click="showFollow">关注:{{followNum}}</el-button>
            <el-button type="text" class="num" @click="showFans">粉丝:{{fansNum}}</el-button>
          </el-row>
          <el-row>
            <p id="desc">个人简介{{desc}}</p>
          </el-row>
        </el-col>
      </el-row>
    </el-row>
    <el-row style="padding:0 10px">
      <el-tabs>
        <el-tab-pane label="我的动态">
          <!--el-col type="flex"-->
            <el-row>
              <el-col v-for="moment in moments" :key=moment.name :span="6">
                <el-col class="moments">
                  <img :src="moment.url" class="img" @click="toMoment" alt="动态的图片"/>
                </el-col>
              </el-col>
            </el-row>
          <!--/el-col-->
        </el-tab-pane>
        <el-tab-pane label="收藏夹">
          <el-row v-if="isFavor">
            <el-row style="padding-left:830px">
              <el-button  type="text" style="font-size:15px" @click="showDialog">新建收藏夹</el-button>
              <el-dialog title="新建收藏夹" :visible.sync="dialogFormVisible">
                  <el-form ref="form" :model="ruleform" :rules="rules">
                    <el-form-item label="名称" :label-width="formLabelWidth" prop="name">
                      <el-input v-model="ruleform.name" auto-complete="off"></el-input>
                    </el-form-item>
                    <el-form-item label="简介" :label-width="formLabelWidth" prop="desc">
                      <el-input v-model="ruleform.desc" auto-complete="off"></el-input>
                    </el-form-item>
                  </el-form>
                  <div slot="footer" class="dialog-footer">
                    <el-button @click="cancelHandler">取 消</el-button>
                    <el-button type="primary" @click="finishHandler('ruleform')">确 定</el-button>
                  </div>
              </el-dialog>
            </el-row>
            <el-row>
              <el-col v-for="favor in favors" :key=favor.name :span="6">
                <el-col class="favor">
                <div style="header">
                  <el-card shadow="always" :body-style="{ padding: '0px' }">
                    <img :src="favor.url" class="img" alt="收藏夹封面" @click="toCollect"/>
                    <div style="padding-left:120px" class="bottom">
                      <el-tooltip effect="dark" :content="favor.brief" placement="bottom-end">
                        <el-button type="text" @click="toCollect">{{favor.favorName}}</el-button>
                      </el-tooltip>
                    </div>
                  </el-card>
                </div>
                </el-col>
              </el-col>
            </el-row>
          </el-row>
          <el-row v-if="isCollect">
            <el-col v-for="collect in collects" :key=collect.name :span="6">
                <el-col class="blogs">
                    <img :src="collect.url" class="img" @click="toMoment" alt="动态的图片"/>
                </el-col>
              </el-col>
          </el-row>
        </el-tab-pane>
      </el-tabs>
    </el-row>
  </el-card>
</template>

<style>
  .el-tabs__item{
    font-size:15px;
    padding:5px;
    margin:10px;
  }
  .moments{
    display:block;
    width:200px;
    height:200px;
    margin:8px 8px;
  }
  .img{
    display:block;
    width:200px;
    height:200px;
  }
  .favor{
    display:block;
    width:200px;
    height:245px;
    margin:8px 8px;
  }
  .bottom{
    margin-top: 0px;
    margin-bottom: 0px;
  }
  #cards{
      margin-left:auto;
      margin-right:auto;
      margin-top:0px;
  }
  #name{
      font-size: 18px;
      padding-top: 30px;
  }
  .num{
      color:rgb(8, 8, 8);
      font-size:15px;
      padding-right:10px;
  }
</style>

<script>
  import qs from 'qs'
  
  export default {
    name:'personalpage',
    data(){
      return{
        headUrl:require('../image/a.jpg'),
        name:'初始昵称',
        followNum:0,
        fansNum:0,
        desc:'',
        momentNum:0,
        moments:[{
            index:0,
            url: require('../image/a.jpg')
          },{
            index:1,
            url: require('../image/gaojin_ciyun.png')
          },{
            index:2,
            url: require('../image/gaojin_radar.png')
          },{
            index:3,
            url: require('../image/a.jpg')
          },{
            index:4,
            url: require('../image/a.jpg')
          }],
        favors:[{
            index:0,
            url: require('../image/a.jpg'),
            favorName:'默认收藏夹',
            brief:'默认收藏夹的简介'
          },{
            index:1,
            url: require('../image/gaojin_ciyun.png'),
            favorName:'MyCollect',
            brief:'默认收藏夹的简介'
          },{
            index:2,
            url: require('../image/gaojin_radar.png'),
            favorName:'Something',
            brief:'默认收藏夹的简介'
          },{
            index:3,
            url: require('../image/a.jpg'),
            favorName:'Anything',
            brief:'默认收藏夹的简介'
          },{
            index:4,
            url: require('../image/a.jpg'),
            favorName:'Nothing',
            brief:'默认收藏夹的简介'
          }],
        collects:[{
            index:0,
            url: require('../image/a.jpg')
          },{
            index:1,
            url: require('../image/gaojin_ciyun.png')
          },{
            index:2,
            url: require('../image/gaojin_radar.png')
          },{
            index:3,
            url: require('../image/a.jpg')
          },{
            index:4,
            url: require('../image/a.jpg')
          }],
        ruleform: {
          name: '',
          desc: ''
        },
        rules:{
          name:[
            {
              min: 1,
              max: 10,
              message: '密码长度为1~10',
              trigger: 'blur'
            }
          ],
          desc:[
            { 
              min: 0,
              max: 20,
              message: '简介长度为0~20',
              trigger: 'blur'
            }
          ]
        },
        formLabelWidth: '120px',
        isFavor:true,
        isCollect:false,
        dialogFormVisible:false,
      }
    },
    methods: {
      handleAvatarSuccess(res, file) {
        this.imageUrl = URL.createObjectURL(file.raw);
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
      toMoment(){
        this.$router.push('moment');
      },
      toCollect(){
        this.isFavor=false;
        this.isCollect=true;
      },
      showMessage(){
    
      },
      showDialog(){
        this.dialogFormVisible=true;
      },
      handleSettingClick:function(){
        this.$router.push('set');
      },
      toFavor: function() {
        this.dialogFormVisible=false;
      },
      finishHandler: function (formName) {
          this.$refs[formName].validate((valid) => {
          if (valid) {
              let fmessage={
                fname:this.fname,
                fdesc:this.fdesc,
              }
              this.$axios.post('/users',qs.stringify({fmessage}))
              .then((response) => {
                  let result = response.data.result;
                  this.resultHandler(result);
              })
              .catch((error) => {
                  console.log(error);
              })
          } else {
            this.$message.error('内容不合法，请重新输入！')
          }
        });
      },
      resultHandler: function (result) {
        if (result) {
          this.$message({
            message: '新建收藏夹成功！',
            type: 'success'
          });
          setTimeout(this.toFavor, 3000);
        } else {
          this.$message.error('新建收藏夹失败，请重试！');
        }
      },
      cancelHandler:function(){
        this.$confirm('放弃新建?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.toFavor();
        }).catch();
      },
      showFollow(){
        this.$router.push('set');
      },
      showFans(){
        this.$router.push('set');
      }
    },
    created() {
      this.$axios.get('/')
        .then((response) => {
            this.headUrl=response.data.headUrl;
            this.name=response.data.name;
            this.fansNum=response.data.fansNum;
            this.followNum=response.data.followNum;
            this.desc=response.data.desc;
            this.momentNum=response.data.momentNum;
            })
            .catch((error) => {
                console.log(error);
        });
      this.$axios.get('/moment')
        .then((response) => {
          this.moments=response.data.moments;
        })
        .catch((error) => {
         console.log(error);
        });
      this.$axios.get('/favorite')
        .then((response) => {
          this.favors=response.data.favors;
          this.collects=response.data.collects;
        })
        .catch((error) => {
         console.log(error);
        });
    }
  }
</script>
