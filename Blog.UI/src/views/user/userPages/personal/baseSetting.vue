<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-07-13 15:40:39
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-03 23:36:56
-->
<template>
  <div class="app-container">
    <div class="container">
      <div class="user-form">
        <el-form
          :model="userForm"
          :rules="rules"
          ref="userForm"
          label-width="50px"
        >
          <el-form-item label="头像">
            <div class="upload-avatar">
              <el-upload
                class="avatar-uploader"
                :action="action"
                :headers="headers"
                :data="uploadData"
                :multiple="false"
                :show-file-list="false"
                :on-success="handleAvatarSuccess"
                :before-upload="beforeAvatarUpload"
              >
                <img
                  v-if="userForm.Photo"
                  :src="$utils.imgUrl(userForm.Photo)"
                  class="avatar"
                />
                <i
                  v-else
                  class="el-icon-plus avatar-uploader-icon el-upload"
                ></i>
              </el-upload>
            </div>
          </el-form-item>
          <el-form-item
            label="账号"
            prop="Account"
          >
            <el-input
              v-model="userForm.Account"
              :size="$utils.isMobile()?'':'small'"
              maxlength="10"
              show-word-limit
              placeholder="请输入你的账户 ！"
            ></el-input>
          </el-form-item>
          <el-form-item
            label="邮箱"
            prop="Email"
          >
            <el-input
              v-model="userForm.Email"
              :size="$utils.isMobile()?'':'small'"
              maxlength="30"
              placeholder="请输入你的邮箱！"
              show-word-limit
            ></el-input>
          </el-form-item>
          <el-form-item
            label="昵称"
            prop="Name"
          >
            <el-input
              v-model="userForm.Name"
              :size="$utils.isMobile()?'':'small'"
              maxlength="11"
              show-word-limit
              placeholder="请输入你的昵称！"
            ></el-input>
          </el-form-item>
          <!-- <el-form-item label="性别">
            <el-select
              v-model="userForm.gender"
              placeholder="请选择性别"
              :size="$utils.isMobile()?'':'small'"
              style="width: 100%"
            >
              <el-option
                v-for="item in genderOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
                style="width: 100%"
              >
              </el-option>
            </el-select>
          </el-form-item> -->
          <el-form-item label="签名">
            <el-input
              v-model="userForm.autograph"
              type="textarea"
              :autosize="{ minRows: 4 }"
              placeholder="写下专属于你的个性签名吧！"
              :size="$utils.isMobile()?'':'small'"
              show-word-limit
              maxlength="50"
            ></el-input>
          </el-form-item>
          <el-form-item class="savebtn">
            <el-button
              type="primary"
              :size="$utils.isMobile()?'':'small'"
              @click="saveData"
              :loading="btnLoading"
              style="width:200px"
            >保存</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>
<script>
import baseSetting from "@/config/defaultSettings"; // 引入全局基本配置
import { getBlogUserInfo, updateBlogUserInfo } from "@/api/user/userInfo";
import UserService from '@/api/services/UserService';
export default {
  name: "BaseSetting",
  data() {
    return {
      //表单数据
      userForm: {
        Account: "", //昵称
        Name: "", //电话
        Email: "", //邮箱
        gender: 1, //性别
        autograph: "", //签名
        avatar_id: "", //头像ID
        Photo: "", //头像路径
        Id:""
      },

      //验证规则
      rules: {
        Account: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          {
            min: 1,
            max: 10,
            message: "用户名长度在1到12之间哦~",
            trigger: "blur",
          },
        ],
        Email: [
          { required: true, message: "请输入邮箱", trigger: "blur" },
          {
            pattern:
              /^[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*\.[a-z]{2,}$/,
            message: "请正确输入邮箱格式",
            trigger: "blur",
          },
        ]
      },

      //性别下来框
      genderOptions: [
        {
          value: 0,
          label: "女",
        },
        {
          value: 1,
          label: "男",
        },
      ],

      //按钮loading
      btnLoading: false,

      // 组件上传额外参数
      uploadData: {
        module: "blog",
      },
    };
  },
  created() {
    this.init();
  },

  methods: {
    async init() {
      const data = this.$store.getters.userInfo;
      console.log(data)
      this.userForm.Account = data.Account;
      this.userForm.Name = data.Name;
      this.userForm.Email = data.Email;

      //this.userForm.gender = data.Name;
      //this.userForm.avatar_id = data.Name;
      this.userForm.Photo = data.Photo;
            this.userForm.Id = data.Id;


    },
    /* 保存数据 */
    saveData() {
      this.btnLoading = true;
      this.$refs.userForm.validate((validate) => {
        if (validate) {
          //浅克隆数据
          const data = Object.assign({}, this.userForm);
          console.log(data)
          // this.userForm.EmpAccount = 'Test'
          UserService.UpdateUser(this.userForm).then((res) => {
            if (res.Code == 200) {
              this.init();
              this.btnLoading = false;
              this.$notify({
                title: "修改成功！",
                message: `你的信息已经修改成功咯！`,
                type: "success",
              });
            }
          });
          // updateBlogUserInfo(data).then((res) => {
          //   if (res.code == 200) {
          //     this.init();
          //     this.btnLoading = false;
          //     this.$notify({
          //       title: "修改成功！",
          //       message: `你的信息已经修改成功咯！`,
          //       type: "success",
          //     });
          //   }
          // });
        } else {
          this.btnLoading = false;
          return fasle;
        }
      });
    },

    //头像上传成后
    handleAvatarSuccess(response) {
      if (response.Code == 200) {
        //this.userForm.avatar_id = response.data;
        this.userForm.Photo = response.Data;
        console.log( this.userForm.Photo)
        this.$notify({
          title: "头像上传成功",
          message: "你的头像已经上传成功，记得点击保存按钮哦！",
          type: "success",
        });
      }
    },

    //头像上传之前
    beforeAvatarUpload(file) {
      const regs = /.+?(\.jpg|\.png|\.jpeg|\.PNG|\.JPG)/g;
      const isImg = regs.test(file.name);
      const isLt5M = file.size / 1024 < 5000;
      if (!isImg) {
        this.$notify.error({
          title: "文件格式错误",
          message: "请上传jpg或者png格式图片",
        });
        return false;
      }
      if (!isLt5M) {
        this.$notify.error({
          title: "文件大小错误",
          message: "图片不能大于5M！",
        });
        return false;
      }
    },
  },
  computed: {
    // 动态拼接上传路径
    action() {
      return process.env.VUE_APP_API_URL + '/User/UPLoadPhoto';
    },

    // 设置请求头参数 token
    headers() {
      return {
        Authorization:"Bearer "+ this.$store.getters.token,
      };
    },
  },
};
</script>
<style lang="scss" scoped>
.app-container {
  width: 100%;
  background: var(--materialCardBackground);
  .container {
    width: 100%;
    display: flex;
    justify-content: center;
    @media only screen and (max-device-width: 750px) {
      .user-form {
        width: 100%;
      }
    }
    @media only screen and (min-device-width: 750px) {
      .user-form {
        width: 700px;
      }
    }
    .user-form {
      margin: 20px;
      .upload-avatar {
        display: flex;
        justify-content: flex-start;
        .avatar-uploader .el-upload {
          border: 1px dashed #d9d9d9;
          border-radius: 6px;
          cursor: pointer;
          position: relative;
          overflow: hidden;
        }
        .avatar-uploader .el-upload:hover {
          border-color: #409eff;
        }
        .avatar-uploader-icon {
          font-size: 28px;
          color: #8c939d;
          width: 128px;
          height: 128px;
          text-align: center;
        }
        .avatar {
          width: 128px;
          height: 128px;
          display: block;
          border-radius: 5px;
        }
      }
      .savebtn {
        // 覆盖element-ui样式
        ::v-deep .el-form-item__content {
          margin: 0 !important;
        }
      }
    }
  }
}

</style>