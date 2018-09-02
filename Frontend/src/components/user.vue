<template>
  <el-container>
    <transition name="slide-fade">
      <div v-if="loadingPage" class="loadingbackground" style="margin-top:-30px"></div>
    </transition>

    <el-row style="width:100%" type="flex" justify="left">
      <el-col class="fixed">
        <el-row class="box-card" style="height:500px;width:280px;margin-left:64%;">
          <!-- <div slot="header" class="clearfix"> -->
          <el-row class="usr" type="flex" align="middle">
            <el-col>
              <div class="user-img border">
                <img class="user-img img-border hover-cursor" :src="'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + this.$store.state.currentUserId_ID +
              '&type=2'+'&Rand=' + Math.random()" alt="头像" @click="jumpToUser($store.state.currentUserId_ID)" />
              </div>
            </el-col>
            <!-- <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i> -->
            <el-col style="margin-left:-70%;width:50%;overflow:hidden">
              <el-row>
                <div style="font-size:16px; font-weight:bold" class="hover-cursor" @click="jumpToUser($store.state.currentUserId_ID)">{{this.$store.state.currentUsername}}</div>
              </el-row>
              <el-row>
                <div style="font-size:14px; color:#999999" class="self-intro">{{this.$store.state.currentUserBio}}</div>
              </el-row>

            </el-col>
          </el-row>
          <!-- </div> -->
          <hr class="hr" />
          <el-row style="color:#999999;font-weight:bold;margin-bottom:10px;width:280px;">
            关注用户
          </el-row>
          <!-- <div class="inset-shadow"> -->
          <el-row style="overflow-y:auto;overflow-x:hidden;height:340px;width:260px;margin-bottom:12px;">
            <el-row v-if="followings.length>0" :key="index" v-for="(following,index) in followings" type="flex" align="middle" style="margin-bottom:12px">
              <el-col>
                <div class="user-img border">
                  <img class="user-img img-border hover-cursor" :src="following.Photo+'&Rand=' + Math.random()" alt="头像" @click="jumpToUser(following.ID)"
                  />
                </div>
              </el-col>
              <!-- <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i> -->
              <el-col style="margin-left:-70%;width:50%;overflow:hidden">
                <el-row>
                  <div style="font-size:16px; font-weight:bold" class="hover-cursor" @click="jumpToUser(following.ID)">{{following.Username}}</div>
                </el-row>
                <el-row>
                  <div style="font-size:14px; color:#999999" class="self-intro">{{following.Bio}}</div>
                </el-row>

              </el-col>
            </el-row>
            <el-row v-if="followings.length==0" style="font-size:13px;color:#777;text-align:center;margin-left:-40px">
              暂无关注用户
            </el-row>
          </el-row>
          <!-- </div> -->
          <hr class="hr outset-shadow" />

          <el-row style="font-size:14px;margin-bottom:5px">
            关注 iGallery，分享精彩视界
          </el-row>
          <el-row style="font-size:14px">
            © 2018 IGALLERY Uni
          </el-row>
        </el-row>
      </el-col>

      <el-row style="width:41%;margin-left:18%;" type="flex" justify="center">
        <el-col>
          <el-row v-for="(moment,index) in totalMoments" :key="index" class="box-color-grey">
            <el-row>
              <el-row class="usr" type="flex" align="middle" style="padding:7px 15px;margin-top:5px">
                <el-col :span="2">
                  <div class="small-user-img small-border">
                    <img class="small-user-img small-img-border hover-cursor" :src="moment.Photo+'&Rand=' + Math.random()" alt="头像" @click="jumpToUser(moment.moment.SenderID)"
                    />
                  </div>
                </el-col>
                <!-- <i class="el-icon-star-off" style="float: right; padding: 3px 0;"></i> -->
                <el-col :span="6">
                  <el-row>
                    <span style="font-size:16px; font-weight:bold hover-cursor" @click="jumpToUser(moment.moment.SenderID)" class="hover-cursor">{{moment.user_username}}</span>
                  </el-row>
                  <el-row>
                    <span style="font-size:14px; color:#999999">{{moment.user_bio}}</span>
                  </el-row>
                </el-col>
              </el-row>
            </el-row>
            <el-row style="font-size:13px;color:#555;margin-left:15px" v-if="moment.forwarded_id!=null">
              <span>
                <img src="../image/forwarded-icon.png" alt="forwarded-icon"> 转发自
                <span @click="jumpToUser(moment.forwarded_id)" style="color:#6191d5;display:inline-block;margin:5px 0" class="hover-cursor">
                  @{{moment.forwarded_username}}</span>
              </span>
            </el-row>
            <el-row>
              <el-carousel :height="autoHeight(moment.moment.ID)" :interval="0" indicator-position="outside" :arrow="showArrow(moment)">
                <el-carousel-item v-for="(img,index) in moment.moment.imgList" :key="index">
                  <div class="pic">
                    <img :src="img" alt="movementImg">
                  </div>
                </el-carousel-item>
              </el-carousel>
            </el-row>

            <el-row>
              <img :src="moment.likeImg" alt="bottomlike" @click="likeHandler(moment)" height="30px" width="30px" style="margin-left:2%">
              <img :src="moment.collectImg" alt="collect" @click="collectHandler(moment)" height="30px" width="30px" style="margin-left:2%">
              <img src="../image/forward.png" alt="forward" @click="forwardHandler(moment)" height="30px" width="30px" style="margin-left:2%">
              <img src="../image/user-more.png" alt="more" @click="userMoreHandler(moment)" height="30px" width="30px" style="margin-right:2%;float:right">
            </el-row>

            <el-col style="padding:5px 15px">
              <!-- 简介 -->
              <el-row style="font-size:14px">
                <!-- {{moment.moment.Content}} -->
                {{moment.moment.Content}}
              </el-row>
              <!-- tag -->
              <el-row>
                <span class="hover-cursor tag" @click="jumpToTag(tag)" v-for="(tag,index) in moment.tags" :key="index">#{{tag}}</span>
              </el-row>
              <!-- comments -->

              <el-row v-for="(comment,index) in moment.comments" :key="index" style="font-size:13px;">

                <span style="display:inline-block;margin:2px 0;color:#000;font-weight:600" class="hover-cursor" @click="jumpToUser(comment.sender.ID)">{{comment.sender_username}}</span>
                <span>{{comment.content}}</span>
              </el-row>
              <span v-if="moment.more_comments" @click="jumpToDetail(moment.moment.ID)" style="color:#999;font-size:12px;font-weight:500"
                class="hover-cursor">加载更多</span>
              <el-row class="moment-time">{{moment.moment.Time}}</el-row>
              <div style="border-top:1px solid rgb(235,238,245)"></div>
              <el-row type="flex" justify="space-between" style="margin:10px 0">

                <input placeholder="添加评论..." v-model="moment.newComment" style="border:0;padding:5px;width:100%" @keyup.enter="commentHandler(moment)">
              </el-row>
            </el-col>
          </el-row>
          <el-row type="flex" justify="center" align="middle" style="margin-top:-20px">
            <span>{{this.bottomHint}}</span>
          </el-row>
        </el-col>
      </el-row>

    </el-row>

    <!-- 更多操作 -->
    <el-dialog title="更多" :center="true" :visible.sync="usermoreDialogVisible" width="20%" :show-close="false" top="15%">
      <el-row>
        <div style="border-top:1px solid rgb(235,238,245)"></div>
        <div @click="jumpToDetail(usermoreMomentId)" style="text-align:center;margin:10px 0;letter-spacing:1px" class="hover-cursor">查看动态</div>
        <div style="border-top:1px solid rgb(235,238,245)"></div>
      </el-row>
    </el-dialog>
  </el-container>
</template>

<style scoped>
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
  /* .slide-fade-leave-active for below version 2.1.8 */

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
    /* background-size: cover */
  }

  .pic {
    width: 100%;
    height: 500px;
    text-align: center;
  }

  .pic img {
    vertical-align: middle;
    width: 100%;
  }

  .box-color-grey {
    border-color: #e6e6e6;
    border-style: solid;
    border-width: thin;
    margin-bottom: 60px;
    background-color: white;
    /* padding: 20px; */
  }

  .inset-shadow {
    /* border-radius: 50%; */
    /* width: 180px; */
    /* height: 180px; */
    overflow-x: hidden;
    overflow-y: scroll;
    position: relative;
  }

  .inset-shadow:after {
    position: absolute;
    content: '';
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
    box-shadow: 0 0 30px 10px rgba(255, 255, 255, .7) inset;
  }

  .hr {
    border-top: 1px solid #efefef;
    height: 0px;
    margin: 10px 0 12px;
    width: 100%;
    padding: 0;
  }

  .border {
    position: relative;
    border: 1px solid transparent;
    border-radius: 50px;
    background: linear-gradient(45deg, #f9a357, #db3579 40%, #c73894 95%);
    background-clip: padding-box;
    padding: 4px;
  }

  .img-border {
    border: white solid;
    border-radius: 50px;
    border-width: 2px;
    margin: -2px;
  }

  .small-border {
    position: relative;
    border: 1px solid transparent;
    border-radius: 30px;
    background: linear-gradient(45deg, #f9a357, #db3579 40%, #c73894 95%);
    background-clip: padding-box;
    padding: 3px;
    /* just to show box-shadow still works fine */
    /* box-shadow: 0 3px 9px black, inset 0 0 9px white; */
  }

  .small-img-border {
    border: white solid;
    border-radius: 30px;
    border-width: 1px;
    margin: -1px;
  }

  .fixed {
    position: fixed;
  }

  .text {
    font-size: 14px;
  }

  .clearfix:before,
  .clearfix:after {
    display: table;
    content: "";
  }

  .clearfix:after {
    clear: both
  }

  .user-img {
    width: 50px;
    height: 50px;
    border-radius: 50px
  }

  .small-user-img {
    width: 30px;
    height: 30px;
    border-radius: 30px
  }

  div {
    margin-bottom: 1px;
  }

  .box-img {
    width: 600px;
  }

  .self-intro {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }

  .send {
    background: transparent;
    height: 0;
    padding-bottom: 30%;
    position: relative;
    -webkit-box-shadow: 0 0;
    box-shadow: 0 0;
  }

  .forwardContent {
    height: 400px;
    background: url('../image/send-dialog.png');
  }

  .edit {

    width: 100%;
    height: 0;
    padding-bottom: 60%;
    overflow: hidden;
    position: relative;
  }

  .moment-time {
    margin: 8px 0;
    color: #999;
    font-size: 12px;
    font-weight: 500;
    letter-spacing: 1px;
  }

  .tag {
    color: #6191d5;
    font-size: 12px;
    margin: 5px 8px 5px 0;
    display: inline-block;
  }

  .hover-cursor {
    cursor: pointer;
  }
</style>

<script>
  import Vue from 'vue'
  //文档高度
  function getDocumentTop() {
    var scrollTop = 0,
      bodyScrollTop = 0,
      documentScrollTop = 0;
    if (document.body) {
      bodyScrollTop = document.body.scrollTop;
    }
    if (document.documentElement) {
      documentScrollTop = document.documentElement.scrollTop;
    }
    scrollTop = (bodyScrollTop - documentScrollTop > 0) ? bodyScrollTop : documentScrollTop;
    return scrollTop;
  }
  //可视窗口高度
  function getWindowHeight() {
    var windowHeight = 0;
    if (document.compatMode == "CSS1Compat") {
      windowHeight = document.documentElement.clientHeight;
    } else {
      windowHeight = document.body.clientHeight;
    }
    return windowHeight;
  }
  //滚动条滚动高度
  function getScrollHeight() {
    var scrollHeight = 0,
      bodyScrollHeight = 0,
      documentScrollHeight = 0;
    if (document.body) {
      bodyScrollHeight = document.body.scrollHeight;
    }
    if (document.documentElement) {
      documentScrollHeight = document.documentElement.scrollHeight;
    }
    scrollHeight = (bodyScrollHeight - documentScrollHeight > 0) ? bodyScrollHeight : documentScrollHeight;
    return scrollHeight;
  }

  var Dictionary = (function () {
    const items = {};
    class Dictionary {
      constructor() {}
      set(key, value) { //向字典中添加新的元素
        items[key] = value;
      }
      delete(key) { //删除字典中某个指定元素
        if (this.has(key)) {
          delete items[key];
          return true;
        }
        return false;
      }
      has(key) { //如果某个键值存在于这个字典中，则返回true，否则返回false
        return items.hasOwnProperty(key);
      }
      get(key) { //通过键值查找特定的数值并返回。
        return this.has(key) ? items[key] : undefined;
      };
      clear() { //将这个字典中的所有元素全部删除。
        items = {};
      }
      size() { //返回字典所包含元素的数量。
        return Object.keys(items).length;
      }
      keys() { //将字典所包含的所有键名以数组形式返回。
        return Object.keys(items);
      }
      values() { //将字典所包含的所有数值以数组形式返回。
        var values = [];
        for (var k in items) {
          if (this.has(k)) {
            values.push(items[k]);
          }
        }
        return values;
      }
      each(fn) { //遍历每个元素并且执行方法
        for (var k in items) {
          if (this.has(k)) {
            fn(k, items[k]);
          }
        }
      }
      getItems() { //返回字典
        return items;
      }
    }
    return Dictionary;
  })();

  
  export default {
    data() {
      return {

        carouselHeight: [],
        loadingPage: true,
        dic: new Dictionary(),
        flag: true,
        bottomHint: '动态加载中，耐心等待啦！( •̀ .̫ •́ )✧',
        currentPage: 1,
        followings: [
        ],

        totalMoments: [{
            //请求的数据
            user_email: '',
            user_username: 'leonnnop',
            user_bio: 'leonnnop',
            forwarded_id: 'dzq@qq.com',
            forwarded_username: 'dzq',
            moment: {
              ID: '1',
              Content: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。',
              Time: '2018/7/20 8:37:18',
              QuoteMID: '',
              imgList: ['']
            },
            tags: [
              'tag1', 'tag2'
            ],
            liked: false,
            collected: false,
            comments: [{
              sender_email: '',
              sender_username: 'user1',
              content: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。'
            }, {
              sender_email: '',
              sender_username: 'user1',
              content: 'comment1'
            }, {
              sender_email: '',
              sender_username: 'user1',
              content: 'comment1'
            }, {
              sender_email: '',
              sender_username: 'user1',
              content: 'comment1'
            }],
            more_comments: true,
            Photo: require('../image/a.jpg'),
            //增加的属性
            likeImg: require('../image/comment-unlike.png'),
            collectImg: require('../image/uncollect.png'),
            newComment: '',
          },
          {
            //请求的数据
            user_email: '',
            user_username: 'leonnnop',
            user_bio: 'leonnnop',
            forwarded_id: '',
            forwarded_username: '',
            moment: {
              ID: '2',
              Content: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。',
              Time: '2018/7/20 8:37:18',
              QuoteMID: '',
              imgList: ['']
            },
            tags: [
              'tag1'
            ],
            liked: false,
            collected: false,
            comments: [{
              sender_email: '',
              sender_username: 'user1',
              content: 'comment1'
            }],
            more_comments: false,
            Photo: require('../image/a.jpg'),
            //增加的属性
            likeImg: require('../image/comment-unlike.png'),
            collectImg: require('../image/uncollect.png'),
            newComment: '',
          },
          {
            //请求的数据
            user_email: '',
            user_username: 'leonnnop',
            user_bio: 'leonnnop',
            forwarded_id: '',
            forwarded_username: '',
            moment: {
              ID: '3',
              Content: '恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。恭喜生活喜提我狗命。',
              Time: '2018/7/20 8:37:18',
              QuoteMID: '',
              imgList: ['']
            },
            tags: [

            ],
            liked: false,
            collected: false,
            comments: [

            ],
            more_comments: false,
            Photo: require('../image/a.jpg'),
            //增加的属性
            likeImg: require('../image/comment-unlike.png'),
            collectImg: require('../image/uncollect.png'),
            newComment: '',
          }
        ],
        usermoreDialogVisible: false,
        usermoreMomentId: '',
        askNum: 0 //再次请求动态的次数
      }
    },

    destroyed() {
      window.removeEventListener('scroll', this.handleScroll)
    },
    beforeCreate() {

      console.log('beforecreate');
    },
    created() {
      this.dic = new Dictionary();
      this.bodyWidth = window.screen.width;
      console.log('------------------------------------------body');
      console.log(this.bodyWidth);
      var self = this;
      setTimeout(function () {
        self.loadingPage = false;
      }, 1000)
      // }
      //监听滚动条，到底时请求动态...
      this.axios.all([this.axios.get('http://192.168.43.249:54468/api/DisplayMoments/Followings', {
            params: {
              Email: this.$store.state.currentUserId,
              Page: 1,
            }
          }),
          this.axios.get('http://192.168.43.249:54468/api/Users/FollowList?userID=' + this.$store.state.currentUserId_ID)
        ])
        .then(this.axios.spread((res1, res2) => {
          this.totalMoments = res1.data;

          if (this.totalMoments.length < 1) {
            this.bottomHint = '没有东西可以看哦，多去关注点人啦！(๑•̀ㅂ•́) ✧'
          }

          this.totalMoments.forEach(element => {
            //点赞状态
            if (element.liked == 0) {
              var likeImg = require('../image/comment-like.png');
              Vue.set(element, 'likeImg', likeImg)
            } else {
              var unlikeImg = require('../image/comment-unlike.png');
              Vue.set(element, 'likeImg', unlikeImg)
            }
            //收藏状态
            if (element.collected == 0) {
              var collectImg = require('../image/collect.png');
              Vue.set(element, 'collectImg', collectImg)

            } else {
              var uncollectImg = require('../image/uncollect.png');
              Vue.set(element, 'collectImg', uncollectImg)

            }

            //请求图片
            this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.moment.ID + '&type=1')
              .then((response) => {
                let list = response.data;
                //将id数组变为url数组
                // element.moment.imgList = [];
                var imgList = []
                list.forEach(ele => {
                  // element.moment.imgList.push({
                  imgList.push('http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                    ele
                  )
                  // ele = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + ele;
                });
                Vue.set(element.moment, 'imgList', imgList);
                this.loadingPage = false;
                console.log(this.loadingPage)
              })
              .catch((error) => {
                console.log(error);
              });
            element.newComment = '';

            var Photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.moment.SenderID +
              '&type=2';
            Vue.set(element, 'Photo', Photo)
            console.log(this.totalMoments)

            if (res2.data != 'Not found') {
              res2.data.forEach(element => {
                let Photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.ID + '&type=2';
                Vue.set(element, 'Photo', Photo)
              });
              this.followings = res2.data;
              console.log(this.followings)
            } else {
              this.followings = [];
            }

            let carouselWidth = this.bodyWidth * 0.41;
            this.axios.get('http://192.168.43.249:54468/api/Picture/GetSz?mid=' + element.moment.ID)
              .then((response) => {
                if (response.data != null) {
                  var width = response.data.width;
                  var height = response.data.height;
                  var rate = height / width;
                  var height = carouselWidth * rate;

                  this.dic.set(element.moment.ID, height + 'px');
                }
                // console.log(this.dic.getItems())

              })
              .catch((error) => {
                console.log(error);
              });

          });
        }))
   },
    methods: {
      autoHeight(ID) {
        console.log(this.dic.get(ID))
        return this.dic.get(ID)
      },
      handleScroll() {
        if (getScrollHeight() == getWindowHeight() + getDocumentTop()) {
          //当滚动条到底时,这里是触发内容
          //异步请求数据,局部刷新dom
          // ajax_function()
          console.log('请求')
          this.currentPage++;
          this.requestHandler(this.currentPage);
        }
      },
      showArrow: function (moment) {
        if (moment.moment.imgList.length > 1) {
          return 'hover';
        } else {
          return 'never';
        }
      },
      messageWebsocketHandler(path, state, content = "") {
        // 0 关注 1 点赞 2 评论 3 转发 4 私信
        window.ws.send('/' + path + ' ' + state + content);
      },
      likeHandler(moment) {
        console.log('websocket', moment.moment.SenderID)
        if (moment.liked == 0) {
          console.log('moment.liked == 0')
          moment.likeImg = require('../image/comment-unlike.png');
          // Vue.set(moment, 'likeImg', likeImg)
          moment.liked = 1
        } else {
          console.log('moment.liked == 1')
          moment.likeImg = require('../image/comment-like.png');
          moment.liked = 0
        }
        console.log(moment)
        this.axios.put('http://192.168.43.249:54468/api/DiscoverMoment/UpdateLiking?email=' + this.$store.state.currentUserId +
            '&moment_id=' + moment.moment.ID)
          .then((response) => {
            if (response.data == 0) {
              if (moment.liked == 0) {
                this.messageWebsocketHandler(moment.moment.SenderID, 1);
              }
            } else {
              this.$message.error('点赞失败，请重试！');
            }
          })
          .catch((error) => {
            console.log(error);
          });
      },
      collectHandler: function (moment) {
        if (moment.collected == 0) {
          this.axios.get('http://192.168.43.249:54468/api/Collect/DeleteCollect?moment_id=' + moment.moment.ID +
              '&user_id=' + this.$store.state.currentUserId_ID
            )
            .then((response) => {
              if (response.data == 0) {
                moment.collectImg = require('../image/uncollect.png');
                moment.collected = 1;
              } else {
                this.$message.error('取消收藏失败，请重试！');
              }
            })
            .catch((error) => {
              console.log(error);
            });
          // moment.collected = 1
        } else {
          this.axios.get('http://192.168.43.249:54468/api/Collect/InsertCollect?moment_id=' + moment.moment.ID +
              '&founder_id=' + this.$store.state.currentUserId_ID +
              '&name=' + '默认收藏夹'
            )
            .then((response) => {
              if (response.data == 0) {
                moment.collectImg = require('../image/collect.png');
                moment.collected = 0;
                // this.messageWebsocketHandler(moment.moment.SenderID, 0);
              } else {
                this.$message.error('收藏失败，请重试！');
              }
            })
            .catch((error) => {
              console.log(error);
            });
        }
      },
      forwardHandler: function (moment) {
        this.$confirm('', '确定转发？', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            center: true
          }).then(() => {
            this.axios.post('http://192.168.43.249:54468/api/Moment/ForwardMoment', {
                User_ID: this.$store.state.currentUserId_ID,
                Moment_ID: moment.moment.ID
              })
              .then((response) => {
                if (response.data == 0) {
                  this.$message({
                    message: '转发成功！',
                    type: 'success'
                  });
                  this.messageWebsocketHandler(moment.moment.SenderID, 3);
                  this.myfresh();
                } else if (response.data == 1) {
                  this.$message({
                    message: '不可以转发自己的动态哦！',
                    type: 'warning'
                  });
                } else if (response.data == 3) {
                  this.$message({
                    message: '已经转发过了喔，看看其他的吧！！',
                    type: 'warning'
                  });
                } else {
                  this.$message.error('转发失败，请稍后重试！')
                }
              })
              .catch((error) => {
                console.log(error);
              });
          })
          .catch(() => {});
      },
      userMoreHandler: function (moment) {
        this.usermoreDialogVisible = true;
        this.usermoreMomentId = moment.moment.ID;
      },
      commentHandler: function (moment) {
        var formdata = new FormData();
        formdata.append('Mid', moment.moment.ID);
        formdata.append('Sender_id', this.$store.state.currentUserId_ID);
        formdata.append('Content', moment.newComment);
        formdata.append('Send_time', '');
        formdata.append('Quote_comment_id', '');
        let config = {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        };
        this.axios.post('http://192.168.43.249:54468/api/Coment/SvCmt', formdata, config)
          .then((response) => {
            // if (response.data == 'OK') {
            if (true) {
              this.$message({
                message: '评论成功！',
                type: 'success'
              });
              this.messageWebsocketHandler(moment.moment.SenderID, 2, ' ' + moment.newComment)
              if (moment.comments.length == 4) {
                moment.more_comments = true;
                moment.newComment = '';
              } else {
                moment.comments.push({
                  sender_email: '',
                  sender_username: 'Leonnnop',
                  content: moment.newComment,
                  send_time: '2018/7/20 8:37:18'
                });
                moment.newComment = '';
              }
            } else {
              this.$message.error('评论失败，请稍后重试！');
            }
          })
          .catch((error) => {
            console.log(error);
            this.$message.error('评论失败，请稍后重试！');
          })
      },
      jumpToDetail: function (momentId) {
        // console.log(momentId);
        this.$router.push('/main/momentDetail/' + momentId);
      },
      jumpToTag: function (tag) {
        this.$router.push('/main/tag/' + tag);
      },
      jumpToUser: function (ID) {
        this.$router.push('/main/personalpage/' + ID);
        // this.$router.push('/main/leaderboard');

      },

      myfresh() {
        this.$router.push('/main/leaderboard');
        // this.$router.push('/main/personalpage/' + email);
      },

      getSrc(src) {
        console.log(src);
        return src;
      },
      handleCancelClick(index) {
        this.$confirm('您觉得这条消息很无聊嘛？qwq？', '提示', {
          confirmButtonText: '是的，别再给我看相关的谢谢!',
          cancelButtonText: '点错了',
          type: 'info'
        }).then(() => {
          // for 循环
          var length = this.totalMoments.length;
          for (var i = 0; i < length; i++) {
            if (this.totalMoments[i].index == index) {
              this.totalMoments.splice(i, 1);
            }
          }
          this.$message({
            type: 'success',
            message: '删除成功!'
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });
        });

      },
      requestHandler(page) {
        if (!this.flag) {
          return;
        }
        this.axios.get('http://192.168.43.249:54468/api/DisplayMoments/Followings', {
            params: {
              Email: this.$store.state.currentUserId,
              Page: page,
            }
          })
          .then((response) => {
            var newTotalMoments = response.data;

            if (newTotalMoments.length < 1) {
              this.bottomHint = '刷完了辣！(⑉꒦ິ^꒦ິ⑉)！我可是有底线的！'
              this.flag = false;
            }

            newTotalMoments.forEach(element => {
              //点赞状态
              if (element.liked == 0) {
                var likeImg = require('../image/comment-like.png');
                Vue.set(element, 'likeImg', likeImg)
              } else {
                var unlikeImg = require('../image/comment-unlike.png');
                Vue.set(element, 'likeImg', unlikeImg)
              }
              //收藏状态
              if (element.collected == 0) {
                var collectImg = require('../image/collect.png');
                Vue.set(element, 'collectImg', collectImg)

              } else {
                var uncollectImg = require('../image/uncollect.png');
                Vue.set(element, 'collectImg', uncollectImg)
              }

              //请求图片
              this.axios.get('http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.moment.ID + '&type=1')
                .then((response) => {
                  let list = response.data;
                  //将id数组变为url数组
                  // element.moment.imgList = [];
                  var imgList = []
                  list.forEach(ele => {
                    // element.moment.imgList.push({
                    imgList.push('http://192.168.43.249:54468/api/Picture/Gets?pid=' +
                      ele
                    )
                    // ele = 'http://192.168.43.249:54468/api/Picture/Gets?pid=' + ele;
                  });
                  Vue.set(element.moment, 'imgList', imgList);
                })
                .catch((error) => {
                  console.log(error);
                });
              element.newComment = '';

              var Photo = 'http://192.168.43.249:54468/api/Picture/FirstGet?id=' + element.moment.SenderID +
                '&type=2';
              Vue.set(element, 'Photo', Photo)
              console.log(newTotalMoments)
            })
            this.totalMoments = this.totalMoments.concat(newTotalMoments);
          })
      },
      backgroundHandler() {
        console.log('backgroundHandler');
        // setTimeout('this.loadingPage = false', 1000);
        this.loadingPage = false
      }

    },
    beforeRouteLeave(to, from, next) {
      this.totalMoments = [];
      this.tableData = [];
      next();
    },

    mounted: function () {
      this.$nextTick(function () {
        window.addEventListener('scroll', this.handleScroll)

        //   // Code that will run only after the
        //   // entire view has been rendered
        //   // console.log('mouted')
        // setTimeout("backgroundHandler()", 1000);
        var self = this;
        // this.toastrVal = inVal;
        // this.loadState = true;
        // this.noBg = bgState;
        setTimeout(function () {
          self.loadingPage = false;
        }, 1000)
      })
    },
    beforeRouteEnter(from, to, next) {
      next(vm => {
        var self = vm;

        vm.$nextTick(() => {
          // this.toastrVal = inVal;
          // this.loadState = true;
          // this.noBg = bgState;
          setTimeout(() => {
            self.loadingPage = false;
          }, 1000)

          // Code that will run only after the
          // entire view has been rendered
          // console.log('mouted')
          // window.setTimeout("this.backgroundHandler()", 1000);
        })
      })
    }
  }
</script>