<template>
  <div class="LoginForm" style="text-align:center">
    <el-card class="box-card" style="width:400px;">
      <!-- <div slot="header" class="clearfix">
      <h1>登陆</h1>
  </div> -->
      <el-row style="margin-top:30px;">
        <img src="../image/iga_exa.png" alt="logo" height="50">
      </el-row>
      <el-row type="flex" justify="center">
        <el-col :span="18">

          <el-form :model="ruleForm" :rules="rules" ref="ruleForm" class="form">
            <el-form-item prop="email">
              <el-input v-model="ruleForm.email" placeholder="邮箱"></el-input>
            </el-form-item>
            <el-form-item label="" prop="password">
              <el-input type="password" v-model="ruleForm.password" placeholder="密码"></el-input>
            </el-form-item>
            <el-row type="flex" justify="end">
              <el-col :span="4">
                <el-button type="text" @click="toForgetPsw">忘记密码?</el-button>
              </el-col>
            </el-row>
            <el-button type="primary" @click="handleLogin('ruleForm')" style="margin-top:20px;width:80%" plain>登陆</el-button>
            <el-row></el-row>
            <el-row type="flex" justify="center" align="middle" style="margin-top:20px">
              <el-col :span="6">
                <div style="height:30px;line-height:30px">没有账户?</div>
              </el-col>
              <el-col :span="3" :offset="0">
                <el-button type="text" @click="toRegister">注册</el-button>
              </el-col>
            </el-row>
          </el-form>
        </el-col>
      </el-row>
    </el-card>

  </div>
</template>

<script>
  import qs from 'qs'

  export default {
    name: 'Login',
    data() {
      return {
        ruleForm: {
          email: '',
          password: ''
        },
        rules: {
          email: [{
              required: true,
              message: '请输入登陆邮箱',
              trigger: 'blur'
            },
            {
              pattern: /^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/,
              message: '请输入合法邮箱'
            }
          ],
          password: [{
              required: true,
              message: '请输入密码',
              trigger: 'blur'
            },
            {
              min: 8,
              max: 20,
              message: '密码长度为8~20',
              trigger: 'blur'
            }
          ]
        }
      }
    },
    methods: {
      handleLogin(formName) {
        console.log(this.ruleForm);
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.axios.get("http://10.0.1.8:54468/api/Users/Login?Email=" + this.ruleForm.email + "&Password=" +
                this.ruleForm.password)
              .then((response) => {
                if (response.data == 'Not Found') {
                  this.$message.error('邮箱不存在！请先注册一个账户！');
                } else if (response.data == 'Error') {
                  this.$message.error('密码错误！');
                } else {
                  this.$store.commit('addCurrentUserId', this.ruleForm.email);
                  this.$store.commit('addCurrentUserId_ID', response.data);
                  console.log(this.$store.state.currentUserId_ID)
                  this.$router.push('/main/user/' + this.ruleForm.email);

                  this.axios.get("http://10.0.1.8:54468/api/Users/GetUserInfo?email=" + this.$store.state.currentUserId)
                    .then((response) => {
                      this.$store.commit('addCurrentUsername', response.data.Username);
                      this.$store.commit('addCurrentUserPassword', response.data.Password);
                      this.$store.commit('addCurrentUserBio', response.data.Bio);
                      this.$store.commit('addCurrentUserPhoto', response.data.Photo);

                      console.log(this.$store.state)
                    })

                }
              })
              .catch((error) => {
                console.log(error);
              })
          } else {
            //this.$message.error('不合法的输入！');
            return false;
          }
        });
      },
      toForgetPsw: function () {
        this.$router.push('/forgetpsw');
      },
      toRegister: function () {
        this.$router.push('/register');
      }
    },
  }
</script>

<style scoped>
  .form {
    margin: 50px 0;
  }

  .clearfix:before,
  .clearfix:after {
    display: table;
    content: "";
  }

  .clearfix:after {
    clear: both
  }

  .box-card {
    width: 500px;
    margin: 50px auto;
    font-size: 14px;
  }
</style>