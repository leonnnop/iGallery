<template>
  <el-tabs type="border-card" class="tabs" style="box-shadow:0 0 0;margin-top:40px">
    <el-tab-pane label="修改个人信息">
      <div style="margin-left:15%;margin-top:5%">
        <!-- <el-form ref="ruleForm" :model="ruleForm" :rules="rules" :label-position="right" label-width="90px"> -->
        <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-position="right" label-width="90px">
          <el-form-item label="昵称：">
            <el-input type="text" v-model="ruleForm.name" auto-complete="off" style="width:50%" clearable></el-input>
          </el-form-item>
          <el-form-item label="个人简介：">
            <el-input type="textarea" v-model="ruleForm.desc" rows="5" style="width:50%" id="descAlert"></el-input>
          </el-form-item>
          <span style="font-size:10px;padding-left:90px">*简介最大长度为40个字符。</span>
          <el-form-item style="padding-top:25px">
            <el-button type="primary" @click="onSubmit('ruleForm')" size="medium">确认修改</el-button>
            <el-button @click="onCancel" size="medium">取消</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-tab-pane>
    <el-tab-pane label="重置密码">
      <div style="margin-left:15%;margin-top:5%">
        <el-form ref="ruleForm2" :model="ruleForm2" :rules="rules2" label-width="90px">
          <el-form-item label="原密码：" prop="opass">
            <el-input type="password" v-model="ruleForm2.opass" auto-complete="off" style="width:50%" clearable></el-input>
          </el-form-item>
          <el-form-item label="密码：" prop="pass">
            <el-input type="password" v-model="ruleForm2.pass" auto-complete="off" style="width:50%" clearable></el-input>
          </el-form-item>
          <el-form-item label="确认密码：" prop="checkPass">
            <el-input type="password" v-model="ruleForm2.checkPass" auto-complete="off" style="width:50%" clearable></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="finishHandler('ruleForm2')" size="medium">确认</el-button>
            <el-button @click="cancelHandler" size="medium">取消</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-tab-pane>
  </el-tabs>
</template>

<style>
  #descAlert {
    margin-bottom: -20px;
  }

  .tabs {
    width: 50%;
    height: 400px;
    margin-left: auto;
    margin-right: auto;
    margin-top: 0px;
  }

  .el-tabs__item {
    font-size: 15px;
    width: 150px;
    padding-left: 30px;
  }
</style>

<script>
  import qs from 'qs'
  export default {
    name: 'set',
    data() {
      var validateName = (rule, value, callback) => {
        if (value === '') {
          return callback(new Error('请输入昵称'));
        } else {
          return callback();
        }
      };
      var validateOpass = (rule, value, callback) => {
        if (value === '') {
          return callback(new Error('请输入原密码'));
        } else {
          return callback();
        }
      };
      var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入密码'));
        } else {
          if (this.ruleForm2.checkPass !== '') {
            this.$refs.ruleForm2.validateField('checkPass');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.ruleForm2.pass) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return {
        ruleForm: {
          name: 'he',
          desc: '呵呵'
        },
        rules: {
          name: [{
            validator: validateName,
            message: '请输入昵称',
            trigger: 'blur'
          }, {
            min: 1,
            max: 15,
            message: '昵称长度为1~15',
            trigger: 'blur'
          }],
          desc: [{
            min: 0,
            max: 40,
            message: '简介长度为0~40',
            trigger: 'blur'
          }],
        },
        ruleForm2: {
          opass: '',
          pass: '',
          checkPass: '',
        },
        rules2: {
          opass: [{
            validator: validateOpass,
            trigger: 'blur'
          }, {
            min: 8,
            max: 20,
            message: '密码长度为8~20',
            trigger: 'blur'
          }],
          pass: [{
            validator: validatePass,
            trigger: 'blur'
          }, {
            min: 8,
            max: 20,
            message: '密码长度为8~20',
            trigger: 'blur'
          }],
          checkPass: [{
            validator: validatePass2,
            trigger: 'blur'
          }, {
            min: 8,
            max: 20,
            message: '密码长度为8~20',
            trigger: 'blur'
          }],
        },
      };
    },
    methods: {
      onSubmit: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.axios.post('http://192.168.43.249:54468/api/Users/ModifyUserInfo', {
                UserName: this.ruleForm.name,
                Bio: this.ruleForm.desc,
                ID: this.$store.state.currentUserId_ID,
              })
              .then((response) => {
                let result = response.data;
                // this.resultHandler1(result);
                /////
                if (result == 0) {
                  this.$store.commit('addCurrentUsername', this.ruleForm.name);
                  this.$store.commit('addCurrentUserBio', this.ruleForm.desc);

                  this.$message({
                    message: '信息修改成功！',
                    type: 'success'
                  });
                  setTimeout(this.onCancel, 3000);
                } else {
                  this.$message.error('信息修改失败！请重试。');
                }
              })
              .catch((error) => {
                console.log(error);
              })
          } else {
            this.$message.error('信息不合法，请重新输入！')
          }
        });

      },
      resultHandler1: function (result) {
        if (result == 0) {
          this.$message({
            message: '信息修改成功！',
            type: 'success'
          });
          setTimeout(this.onCancel, 1000);
        } else {
          this.$message.error('信息修改失败！请重试。');
        }
      },
      onCancel: function () {
        this.$router.push('personalpage/'+this.$store.state.currentUserId_ID);
      },
      finishHandler: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            console.log('adsfaadfgadsgs')
            //this.opassForm.opass='';
            this.axios.get("http://192.168.43.249:54468/api/Users/Login?Email=" + this.$store.state.currentUserId +
                "&Password=" +
                this.ruleForm2.opass
              )
              .then((response) => {
                if (response.data != 'Error' || esponse.data != 'NotFound') {
                  this.axios.put('http://192.168.43.249:54468/api/Users/ChangePassword?email=' + this.$store.state.currentUserId +
                      '&NewPassword=' + this.ruleForm2.checkPass
                    )
                    .then((response) => {
                      let result = response.data;
                      // this.resultHandler(result);
                      if (result) {
                        this.$store.commit('addCurrentUserPassword', this.ruleForm2.checkPass);

                        this.$message({
                          message: '密码重置成功！将在三秒后跳转至登录界面,重新登录！',
                          type: 'success'
                        });
                        setTimeout(this.toLogin, 3000);
                      } else {
                        this.$message.error('密码重置失败，请重试！');
                      }
                    })
                    .catch((error) => {
                      console.log(error);
                    })
                } else {
                  this.$message.error('密码不合法，请重新输入！')
                }
              });
          } else {
            this.$message.error('原密码不正确，请重新输入！');
            return false;
          }
        })
      },
      toLogin: function () {
        this.$router.push('/login');
      },
      resultHandler: function (result) {
        if (result) {
          // this.$store.commit('addCurrentUserPassword', this.ruleForm.email);

          this.$message({
            message: '密码重置成功！将在三秒后跳转至登录界面,重新登录！',
            type: 'success'
          });
          setTimeout(this.toLogin, 3000);
        } else {
          this.$message.error('密码重置失败，请重试！');
        }
      },
      cancelHandler: function () {
        this.$confirm('放弃修改?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.onCancel();
        }).catch();
      }
    },




    created() {
      this.ruleForm.name = this.$store.state.currentUsername;
      this.ruleForm.desc = this.$store.state.currentUserBio;
      window.scroll(0, 0);

    }
  }
</script>