<template>
  <el-tabs type="border-card" class="tabs">
    <el-tab-pane label="修改个人信息">
      <el-row style="margin-left:15%;margin-top:5%">
        <el-form ref="form" :model="form" :rules="rules" :label-position="right" label-width="90px">
          <el-form-item label="昵称：">
            <el-input v-model="form.name" style="width:50%">{{name}}</el-input>
          </el-form-item>
          <el-form-item label="个人简介：">
            <el-input type="textarea" v-model="form.desc" style="width:50%">{{desc}}</el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit('form')">确认修改</el-button>
            <el-button @click="onCancel">取消</el-button>
          </el-form-item>
        </el-form>
      </el-row>
    </el-tab-pane>
    <el-tab-pane label="重置密码">
      <el-form ref="ruleForm" :model="ruleForm" status-icon :rules="rules" label-width="90px" class="demo-ruleForm">
        <div v-if="showButton" style="margin-left:15%;margin-top:5%">
          <el-form-item label="原密码：" prop="opass">
            <el-input type="password" v-model="ruleForm.opass" auto-complete="off" style="width:50%" clearable></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="passHandler('ruleForm')">确认</el-button>
            <el-button @click="cancelHandler">取消</el-button>
          </el-form-item>
        </div>
        </el-form>
        <div v-if="passCorrect">
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
        </div>
    </el-tab-pane>
  </el-tabs>
</template>

<style>
  .tabs {
    width: 50%;
    height: 60%;
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
      var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入密码'));
        } else {
          if (this.ruleForm.checkPass !== '') {
            this.$refs.ruleForm.validateField('checkPass');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value == '') {
          callback(new Error('请再次输入密码'));
        } else if (value != this.ruleForm.pass) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return {
        form: {
          name: '呵',
          desc: '呵呵'
        },
        ruleForm: {
          opass: '',
          pass: '',
          checkPass: ''
        },
        rules: {
          name: [{
            validator: validatePass,
            message: '请输入名称',
            trigger: 'blur'
          }, {
            min: 1,
            max: 15,
            message: '密码长度为1~15',
            trigger: 'blur'
          }],
          desc: [{
            min: 0,
            max: 40,
            message: '简介长度为0~40',
            trigger: 'blur'
          }],
          opass: [{
            validator: validatePass,
            message: '请输入原密码',
            trigger: 'blur'
          }, {
            min: 8,
            max: 20,
            message: '密码长度为8~20',
            trigger: 'blur'
          }],
          pass: [{
            validator: validatePass,
            message: '请输入密码',
            trigger: 'blur'
          }, {
            min: 8,
            max: 20,
            message: '密码长度为8~20',
            trigger: 'blur'
          }],
          checkPass: [{
            validator: validatePass2,
            message: '请再次输入密码',
            trigger: 'blur'
          }, {
            min: 8,
            max: 20,
            message: '密码长度为8~20',
            trigger: 'blur'
          }]
        },
        showButton: true,
        passCorrect: false
      }
    },
    methods: {
      onSubmit: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            let umessage = {
              name: this.name,
              desc: this.desc,
            }
            this.$axios.post('/users', qs.stringify({
                name: this.name,
                desc: this.desc
              }))
              .then((response) => {
                let result = response.data.result;
                this.resultHandler1(result);
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
        if (result) {
          this.$message({
            message: '信息修改成功！',
            type: 'success'
          });
          setTimeout(this.onCancel, 3000);
        } else {
          this.$message.error('信息修改失败！请重试。');
        }
      },
      onCancel: function () {
        this.$router.push('personalpage');
      },
      resetForm: function (formName) {
        this.$refs[formName].resetFields();
      },
      passHandler: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.passCorrect = true;
            this.showButton = false;
          } else {
            this.$message.error('密码不正确，请重新输入！');
            return false;
          }
        })
      },
      finishHandler: function (formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            this.$axios.post('/users', qs.stringify({
                pass: this.pass
              }))
              .then((response) => {
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
      this.$axios.get('/')
        .then((response) => {
          this.name = response.data.name;
          this.desc = response.data.desc;
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }
</script>