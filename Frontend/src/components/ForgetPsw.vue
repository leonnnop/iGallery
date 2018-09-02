<template>
  <div class="forget-psw">
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span style="font-size:18px;">重置密码</span>
      </div>
      <el-row type="flex" justify="center">
        <el-col :span="22">
          <el-form ref="emailForm" :rules="emailRules" :model="emailForm" status-icon>
            <el-form-item prop="email">
              <el-row type="flex" style="center">
                <el-col :span="18">
                  <el-input v-model="emailForm.email" placeholder="邮箱"></el-input>
                </el-col>
                <el-col :span="5" offset="1">
                  <el-button type="primary" plain @click="verifyEmailHandler('emailForm')" style="width:100%" :disabled="verifyBtn">验证</el-button>
                </el-col>
              </el-row>
            </el-form-item>
          </el-form>
          <el-form ref="verifyCodeForm" :rules="verifyCodeRules" :model="verifyCodeForm" status-icon>
            <el-form-item prop="verifyCode">
              <el-row>
                <el-col :span="18">
                  <el-input v-model="verifyCodeForm.verifyCode" placeholder="验证码"></el-input>
                </el-col>
              </el-row>
            </el-form-item>
          </el-form>
          <el-row type="flex" justify="end" v-if="showButton">
            <el-col :span="5">
              <el-button @click="cancelHandler" plain>取消</el-button>
            </el-col>
          </el-row>

          <div v-if="isEmailValid">
            <el-form ref="passwordForm" :rules="passwordRules" :model="passwordForm" status-icon>
              <el-row>
                <el-col :span="18">
                  <el-form-item prop="password">
                    <el-input type="password" v-model="passwordForm.password" placeholder="新密码" maxlength="20"></el-input>
                  </el-form-item>
                  <el-form-item prop="checkPassword">
                    <el-input type="password" v-model="passwordForm.checkPassword" placeholder="确认密码" maxlength="20"></el-input>
                  </el-form-item>
                </el-col>
              </el-row>

              <el-form-item>
                <el-row v-if="isEmailValid">
                  <el-col :span="5">
                    <el-button @click="cancelHandler" plain>取消</el-button>
                  </el-col>
                  <el-col :span="5" :offset="14">
                    <el-button type="primary" plain @click="finishHandler('passwordForm')">完成</el-button>
                  </el-col>
                </el-row>
              </el-form-item>
            </el-form>
          </div>
        </el-col>
      </el-row>
    </el-card>
  </div>
</template>

<script>
  import qs from 'qs'
  import md5 from 'js-md5';
  export default {
    name: 'ForgetPsw',
    data() {
      var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入密码'));
        } else {
          if (this.passwordForm.checkPassword !== '') {
            this.$refs.passwordForm.validateField('checkPassword');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.passwordForm.password) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();

        }
      };
      var validateVerifyCode = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入验证码'));
        } else if (value != this.checkVerifyCode) {
          console.log('value:' + value + 'check:' + this.checkVerifyCode)
          callback(new Error('验证码错误!'));
        } else {
          callback(this.changeBtnVisible());
        }
      };
      return {
        email: '',
        checkVerifyCode: '',
        emailForm: {
          email: ''
        },
        verifyCodeForm: {
          verifyCode: ''
        },
        passwordForm: {
          password: '',
          checkPassword: ''
        },
        emailRules: {
          email: [{
              required: true,
              message: '请输入登陆邮箱',
              trigger: 'blur'
            },
            {
              pattern: /^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/,
              message: '请输入合法邮箱'
            }
          ]
        },
        verifyCodeRules: {
          verifyCode: [{
            validator: validateVerifyCode,
            trigger: 'blur'
          }]
        },
        passwordRules: {
          password: [{
              validator: validatePass,
              trigger: 'blur'
            },
            {
              min: 8,
              max: 20,
              message: '密码长度为8~20',
              trigger: 'blur'
            }
          ],
          checkPassword: [{
            validator: validatePass2,
            trigger: 'blur'
          }]
        },
        isEmailValid: false,
        showButton: true,
        verifyBtn: false
      };
    },
    methods: {
      verifyEmailHandler: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.email = '';
            this.checkVerifyCode = '';
            this.axios.get('http://192.168.43.249:54468/api/Users/VerifyMail', {
                params: {
                  email: this.emailForm.email
                }
              })
              .then((response) => {
                if (response.data !== false) {
                  this.email = this.emailForm.email;
                  this.checkVerifyCode = response.data;
                  console.log(this.checkVerifyCode)
                  this.$message({
                    message: '已发送验证码请查看邮件。',
                    type: 'success'
                  });
                } else {
                  this.$message.error('用户不存在，请重新输入！');
                }
              })
              .catch((error) => {
                console.log(error);
              })
          } else {
            this.$message.error('邮箱不合法，请重新输入！');
            return false;
          }
        });

      },
      finishHandler: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.axios.put('http://192.168.43.249:54468/api/Users/ChangePassword?email=' + this.email + '&NewPassword=' +
              md5(this.passwordForm.password).substr(0,20) // , {
              //   email: this.email,
              //   NewPassword: this.passwordForm.password
              // }
            ).
            then((response) => {
                let result = response.data;
                this.resetHandler(result);
              })
              .catch((error) => {
                console.log(error);
              })
          } else {
            this.$message.error('密码不合法，请重新输入！')
          }
        });
      },
      toLogin: function () {
        this.$router.push('/login');
      },
      resetHandler: function (result) {
        if (result) {
          this.$message({
            message: '密码重置成功！将在三秒后跳转至登录界面！',
            type: 'success'
          });
          setTimeout(this.toLogin, 3000);
        } else {
          this.$message.error('密码重置失败，请重试！');
        }
      },
      cancelHandler: function () {
        this.$confirm('放弃重置密码?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.toLogin();
        }).catch();
      },
      changeBtnVisible: function () {
        this.isEmailValid = true;
        this.showButton = false;
        this.verifyBtn = false;
      },
      beforeRouteLeave(to, from, next) {
        this.email = '';
        this.checkVerifyCode = '';
        next();
      }
    }
  }
</script>

<style scoped>
  .clearfix:before,
  .clearfix:after {
    display: table;
    content: "";
  }

  .clearfix:after {
    clear: both
  }

  .box-card {
    width: 400px;
    margin: 50px auto;
    font-size: 14px;
  }

  .forget-psw {
    overflow: hidden;
    height: 750px;
    background-color: #fafafa;
    text-align: center;
  }
</style>