<template>
  <el-card id="cards" style="width:70%" :body-style="{padding:'0px'}"> 
    <el-row style="background-color:#dcdfe6;padding-bottom:10px">
        <el-col :span="6" :offset="1" style="padding-top:20px"> 
          <img :src="headUrl" style="width:178px;height:178px;border-radius:83px;cursor:pointer" alt="头像">
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
    <el-row style="padding:0 10px">
      <el-tabs>
        <el-tab-pane label="动态">
          <!--el-col type="flex"-->
            <el-row>
              <el-col v-for="moment in moments" :key=moment.name :span="6">
                <el-col class="moments">
                  <img :src="moment.url" class="img" @click="toMoment('momentID')" alt="动态的图片"/>
                </el-col>
              </el-col>
            </el-row>
          <!--/el-col-->
        </el-tab-pane>
      </el-tabs>
    </el-row>
  </el-card>
</template>

<style>
  .el-tabs__item{
    font-size:15px;
    margin:10px;
    padding:0px;
  }
  .moments{
    display:block;
    width:200px;
    height:200px;
    margin:15px;
  }
  .img{
    display:block;
    width:200px;
    height:200px;
    border-radius:5px;
  }
  #cards{
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
      color:#161515;
      font-size:14px;
      padding-right:10px;
  }
</style>

<script>
  import qs from 'qs'
  
  export default {
    name:'userpage',
    data(){
      return{
        headUrl:require('../image/a.jpg'),
        username:'初始昵称',
        followNum:0,
        fansNum:0,
        desc:'',
        momentNum:0,
        moments:[{
            index:0,
            momentID:'0',
            url: require('../image/a.jpg')
          },{
            index:1,
            momentID:'1',
            url: require('../image/gaojin_ciyun.png')
          },{
            index:2,
            momentID:'2',
            url: require('../image/gaojin_radar.png')
          },{
            index:3,
            momentID:'3',
            url: require('../image/a.jpg')
          },{
            index:4,
            momentID:'4',
            url: require('../image/a.jpg')
          }],
      }
    },
    methods: {
      toMoment(key){
        this.$router.push('momentDetail'+key);
      },
      showFollow(){
        this.$router.push('follow');
      },
      showFans(){
        this.$router.push('fans');
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
            this.moments=response.data.moments;
            })
            .catch((error) => {
                console.log(error);
        });
    }
  }
</script>
