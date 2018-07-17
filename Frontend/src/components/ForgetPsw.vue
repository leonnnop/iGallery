<template>
    <div class="forget-psw">
    <el-card class="box-card">
      <div slot="header" class="clearfix">
        <span style="font-size:18px;">重置密码</span>
      </div>
      <el-row type="flex" justify="center">
        <el-col :span="18">
          <el-form ref="ruleForm" :rules="rules" :model="ruleForm">
            <el-form-item prop="email">
              <el-input v-model="ruleForm.email" placeholder="邮箱" clearable></el-input>
            </el-form-item>
            <el-form-item>
              <el-row v-if="showButton">
                <el-col :span="10">
                  <el-button @click="cancelHandler" plain style="width:100%">取消</el-button>
                </el-col>
                <el-col :span="10" :offset="4">
                  <el-button type="primary" plain @click="emailHandler('ruleForm')" style="width:100%">确定</el-button>
                </el-col>
              </el-row>
            </el-form-item>
            <div v-if="isEmailValid">
              <el-form-item prop="password">
                <el-input type="password" v-model="ruleForm.password" placeholder="新密码" maxlength="20" clearable></el-input>
              </el-form-item>
              <el-form-item prop="checkPassword">
                <el-input type="password" v-model="ruleForm.checkPassword" placeholder="确认密码" maxlength="20" clearable></el-input>
              </el-form-item>
              <el-form-item>
                
                <el-row v-if="isEmailValid">
                <el-col :span="10">
                  <el-button @click="cancelHandler" plain style="width:100%">取消</el-button>
                </el-col>
                <el-col :span="10" :offset="4">
                  <el-button type="primary" plain @click="finishHandler('ruleForm')" style="width:100%">完成</el-button>
                </el-col>
              </el-row>
              </el-form-item>
            </div>
          </el-form>
        </el-col>
      </el-row>
    </el-card>
  </div>
</template>

<script>
  import qs from 'qs'

  export default {
    name: 'ForgetPsw',
    data() {
      var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入密码'));
        } else {
          if (this.ruleForm.checkPassword !== '') {
            this.$refs.ruleForm.validateField('checkPassword');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.ruleForm.password) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return {
        ruleForm: {
          email: '',
          password: '',
          checkPassword: ''
        },
        rules: {
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
          }],
          email: [{
              required: true,
              message: '请输入登陆邮箱',
              trigger: 'blur'
            },
            {
              pattern: /^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/,
              message: '请输入合法邮箱'
            },
          ]
        },
        isEmailValid: false,
        showButton: true,
      };
    },
    methods: {
      emailHandler: function (formName) {
        //验证邮箱
        //.....
        // this.$refs[formName].validate((valid) => {
        //   if (valid) {
        //     this.$axios.post('/users', qs.stringify(this.ruleForm.email))
        //       .then((response) => {
        //         if (response.data.code) {
        //           this.isEmailValid = true;
        //           this.showButton = false;
        //         } else {
        //           this.$message.error('用户不存在！');
        //         }
        //       })
        //       .catch((error) => {
        //         console.log(error);
        //       })
        //   } else {
        //     this.$message.error('邮箱不合法，请重新输入！');
        //     return false;
        //   }
        // });
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.isEmailValid = true;
            this.showButton = false;
          } else {
            this.$message.error('邮箱不合法，请重新输入！');
            return false;
          }
        })

      },
      finishHandler: function (formName) {
          this.$refs[formName].validate((valid) => {
          if (valid) {
              this.$axios.post('/users',qs.stringify({password:this.password})).
              then((response) => {
                  let result = response.data.result;
                  this.resultHandler(result);
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
      resultHandler: function (result) {
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
      cancelHandler:function(){
        this.$confirm('放弃重置密码?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.toLogin();
        }).catch();

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
  .forget-psw{
    overflow: hidden;
    height: 750px;
    background-color: #fafafa;
    text-align: center;
  }
  

</style>
