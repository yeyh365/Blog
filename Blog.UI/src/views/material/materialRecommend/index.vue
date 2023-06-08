<!--
 * @Descripttion: 资源推荐
 * @Author: Yerik
 * @Date: 2021-10-12 22:43:01
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-01 23:52:15
-->
<template>
  <div class='app-container'>
    <div class='container'>
      <el-card class="box-card">
        <div
          slot="header"
          class="clearfix"
        >
          <span class='recommend-title'>推荐资源</span>
        </div>
        <div class='material-form'>
          <el-form
            :model="ruleForm"
            :rules="rules"
            ref="ruleForm"
            :label-width="$utils.isMobile()?'55px':'100px'"
          >
            <el-form-item
              label="名称"
              prop="MaterialName"
            >
              <el-input
                v-model="ruleForm.MaterialName"
                size="small"
                placeholder="请输入资源名称"
                clearable
                maxlength="20"
                show-word-limit
              >
                <el-tooltip
                  class="item"
                  effect="dark"
                  content="检测是否有重复资源"
                  placement="top"
                  slot="append"
                >
                  <el-button
                    icon="el-icon-search"
                    @click="testRepeat"
                    :loading='loading.testLoading'
                  ></el-button>
                </el-tooltip>
              </el-input>
            </el-form-item>
            <el-form-item
              label="描述"
              prop="MaterialDescribe"
            >
              <el-input
                placeholder="请输入资源简短描述"
                v-model="ruleForm.MaterialDescribe"
                size="small"
                clearable
                maxlength="40"
                show-word-limit
              >
              </el-input>
            </el-form-item>
            <el-form-item
              label="链接"
              prop="MaterialLink"
            >
              <el-input
                placeholder="请输入正确的网址链接"
                v-model="ruleForm.MaterialLink"
                size="small"
                clearable
              >
              </el-input>
            </el-form-item>
            <el-form-item
              label="标签"
              prop="label"
            >
              <el-select
                size="small"
                v-model="ruleForm.Label"
                placeholder="请选择"
                style="width:100%"
                multiple
                :multiple-limit='5'
              >
                <el-option-group
                  v-for="(item,index) in materialOption"
                  :key="index"
                  :label="item.TypeName"
                >
                  <el-option
                    v-for="(value,key) in item.Keywords"
                    :key="value.Id"
                    :label="value.TypeName"
                    :value="value.Id"
                  >
                  </el-option>
                </el-option-group>
              </el-select>
            </el-form-item>
            <el-form-item
              label="LOGO"
              prop="MaterialCover"
            >
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
                    v-if="ruleForm.MaterialCover"
                    :src="$utils.imgUrl(ruleForm.MaterialCover)"
                    class="avatar"
                  />
                  <div v-else>
                    <i class="el-icon-plus avatar-uploader-icon el-upload"></i>
                  </div>

                </el-upload>
              </div>
            </el-form-item>
            <el-form-item
              label="详情"
              prop="MaterialDetails"
            >
              <el-input
                placeholder="请介绍该资源的作用、用法"
                v-model="ruleForm.MaterialDetails"
                size="small"
                clearable
                maxlength="200"
                show-word-limit
                type="textarea"
                :autosize="{ minRows: 4, maxRows: 4}"
              >
              </el-input>
            </el-form-item>
            <el-form-item>
              <el-button
                type="primary"
                size="small"
                @click="saveData()"
                :loading='loading.saveLoading'
              ><span v-if='isAdd'>立即分享</span><span v-else>重新提交</span></el-button>
            </el-form-item>
          </el-form>
        </div>
      </el-card>
    </div>
    <!-- 弹窗 -->
    <el-dialog
      title="已有相似资源"
      :visible.sync="dialogVisible"
      :width="$utils.isMobile()?'90%':'30%'"
      :before-close="handleClose"
    >
      <el-card shadow="hover">
        <MaertrialItem
          :materialData='item'
          v-for='(item,index) in materialData'
          :key='index'
        />
      </el-card>
      <div class='pagination'>
        <el-pagination
          background
          layout="prev, pager, next"
          :Total="pages.Total"
          :Page-size='pages.Limit'
          :current-Page='pages.Page'
          @current-change='currentChange'
        >
        </el-pagination>
      </div>
      <span
        slot="footer"
        v-if='showSubmitUtils'
      >
        <el-button
          @click="handleClose"
          size="small"
        >我在想想</el-button>
        <el-button
          type="primary"
          @click="submitSave"
          size="small"
          :loading='loading.submitLoading'
        >确定提交</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import baseSetting from "@/config/defaultSettings"; // 引入全局基本配置
import MaertrialItem from "@/components/materialItem/index.vue";
import {
  getMaterialType,
  addNewMaterialRecommend,
  getMaterialByName,
  editMaterialRecommend,
} from "@/api/material/materialRecommend";
import MaterialService from "@/api/services/MaterialService"
export default {
  name: "MaterialRecommend",
  components: {
    MaertrialItem,
  },
  data() {
    return {
      //表单数据
      ruleForm: {
        MaterialName: "",
        MaterialDescribe: "",
        MaterialDetails: "",
        MaterialLink: "",
        MaterialCover: "",
        MaterialCoverid: "",
        Label: [],
      },

      //表单验证规则
      rules: {
        MaterialName: [
          { required: true, message: "请输入资源名称", trigger: "blur" },
          {
            min: 1,
            max: 20,
            message: "名称长度在1到20之间",
            trigger: "blur",
          },
        ],
        MaterialDescribe: [
          { required: true, message: "请输入资源描述", trigger: "blur" },
          {
            min: 1,
            max: 40,
            message: "描述长度在1到20之间",
            trigger: "blur",
          },
        ],
        MaterialLink: [
          { required: true, message: "请输入资源链接", trigger: "blur" },
          {
            pattern:
              /^(((ht|f)tps?):\/\/)?[\w-]+(\.[\w-]+)+([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$/,
            message: "请正确输入链接或网址格式",
            trigger: "blur",
          },
        ],
        MaterialDetails: [
          { required: true, message: "请输入资源详情", trigger: "blur" },
          {
            max: 200,
            message: "资源详情应小于200字之间",
            trigger: "blur",
          },
        ],
        Label: [{ required: true, message: "资源标签必选", trigger: "change" }],
        MaterialCover: [
          { required: true, message: "LOGO必传", trigger: "blur" },
        ],
      },

      // 组件上传额外参数
      uploadData: {
        module: "blog",
      },

      //分类选项
      materialOption: [],

      //loading动画
      loading: {
        saveLoading: false,
        testLoading: false,
        submitLoading: false,
      },

      //弹窗控制变量
      dialogVisible: false,

      //资源数据
      materialData: [],

      //分页参数
      pages: {
        Page: 1,
        Limit: 1,
        Total: 0,
      },
      LoginUserId:'',
      //提交工具栏
      showSubmitUtils: false,

      //判断是添加还行编辑
      isAdd: true,
    };
  },
  created() {
    this.isAdd = true;
    //将当前页面变为编辑页面
    if (this.$route.query.status == "edit") {
      this.isAdd = false;
      this.ruleForm = Object.assign({}, JSON.parse(this.$route.query.data));
    }
     this.LoginUserId=this.$store.getters.userId;

     this.LoginUserInfo=Object.assign([], this.$store.getters.userInfo)
    this.init();
  },
  methods: {
    //数据初始化
    init() {
       MaterialService.GetMaterialTypeList().then((res) => {
       this.materialOption = Object.assign([], res.Data);
        // this.materialTypeList.forEach((item, index) => {
        //   item.icon = this.filterIncons[index];
        // });
       console.log('materialOption',this.materialOption)
      })
      // getMaterialType().then((res) => {
      //   this.materialOption = Object.assign([], res.data);
      // });
    },

    //提交数据
    saveData() {
      this.$refs.ruleForm.validate((valid) => {
        if (valid) {
          const testQuery = {
            name: this.ruleForm.MaterialName,
            ...this.pages,
          };
          this.submitSave();
          //先验证重复资源
          // getMaterialByName(testQuery).then((res) => {
          //   if (res.code == 200) {
          //     if (res.data.data.length > 0) {
          //       this.pages.Total = res.data.Total;
          //       this.pages.Page = res.data.current_page;
          //       this.materialData = Object.assign([], res.data.data);
          //       this.dialogVisible = true;
          //       this.showSubmitUtils = true;
          //     } else {
          //       this.submitSave();
          //     }
          //   }
          // });
        } else {
          this.loading.submitLoading = false;
          return false;
        }
      });
    },

    //确认提交数据
    submitSave() {
      console.log
      //this.loading.submitLoading = true;
      //this.loading.saveLoading = true;
      const query = Object.assign({}, this.ruleForm);
      if (this.isAdd) {
        query.Status=0
        query.UserId=this.LoginUserId
        query.MaterialCoverid=0
        console.log('query',query)
        MaterialService.CreateMaterial(query).then(res=>{
                    this.loading.submitLoading = false;
          this.loading.saveLoading = false;
          this.dialogVisible = false;
          this.loading.submitLoading = false;
          if (res.Code == 200) {
            this.$notify({
              title: "成功",
              message: "你的资源已重新分享！",
              type: "success",
            });
            this.$router.push({
              path: "/materialResult",
              query: {
                id: query.Id,
              },
            });
          }
        })
      } else {
        MaterialService.UpdateMaterial(query).then(res=>{
                    this.loading.submitLoading = false;
          this.loading.saveLoading = false;
          this.dialogVisible = false;
          this.loading.submitLoading = false;
          if (res.Code == 200) {
            this.$notify({
              title: "成功",
              message: "你的资源已重新编辑！",
              type: "success",
            });
            this.$router.push({
              path: "/materialResult",
              query: {
                id: query.Id,
              },
            });
          }
        })
        // editMaterialRecommend(query).then((res) => {
        //   this.loading.submitLoading = false;
        //   this.loading.saveLoading = false;
        //   this.dialogVisible = false;
        //   this.loading.submitLoading = false;
        //   if (res.code == 200) {
        //     this.$notify({
        //       title: "成功",
        //       message: "你的资源已重新编辑，但是资源审核状态不会改变！",
        //       type: "success",
        //     });
        //     this.$router.push({
        //       path: "/materialResult",
        //       query: {
        //         id: query.id,
        //       },
        //     });
        //   }
        // });
      }
    },

    //检测重复资源
    testRepeat() {
      this.loading.testLoading = true;
      if (this.ruleForm.MaterialName == "") {
        this.$message({
          message: "请先输入资源名称！",
          type: "warning",
        });
        this.loading.testLoading = false;
      } else {
        const query = { name: this.ruleForm.MaterialName, ...this.pages };
        getMaterialByName(query).then((res) => {
          this.loading.testLoading = false;
          if (res.code == 200) {
            if (res.data.data.length > 0) {
              this.pages.Total = res.data.Total;
              this.pages.Page = res.data.current_page;
              this.materialData = Object.assign([], res.data.data);
              this.dialogVisible = true;
            } else {
              this.$message({
                message: "未发现有重复资源",
                type: "success",
              });
            }
          }
        });
      }
    },

    //头像上传成后
    handleAvatarSuccess(response) {
      if (response.Code == 200) {
        //this.ruleForm.MaterialCoverid = response.data.id;
        this.ruleForm.MaterialCover = response.Data;
        this.$notify({
          title: "LOGO上传成功",
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

    //弹窗关闭回调
    handleClose() {
      this.showSubmitUtils = false;
      this.dialogVisible = false;
    },

    //分页变化
    currentChange(current) {
      this.pages.Page = current;
      const query = { name: this.ruleForm.MaterialName, ...this.pages };
      //重新请求赋值
      getMaterialByName(query).then((res) => {
        this.pages.Total = res.data.Total;
        this.pages.Page = res.data.current_page;
        this.materialData = Object.assign([], res.data.data);
      });
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
@import "@/style/mixin.scss";
@media only screen and (max-device-width: 750px) {
  .app-container {
    .container {
      padding: 0 5px;
      width: calc(100% - 10px);
    }
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    .container {
    }
  }
}
.app-container {
  width: 100%;
  .container {
    margin-bottom: 20px;
    .box-card {
      width: 100%;

      .clearfix {
        text-align: left;
        @include title-color-scroll-style;
        color: var(--materialCardText);

        .recommend-title {
          font-weight: 800;
        }
      }
      .material-form {
        width: 100%;
      }
    }
    .upload-avatar {
      display: flex;
      justify-content: flex-start;
      .avatar-uploader .el-upload {
        border: 1px dashed #d9d9d9;
        border-radius: 6px;
        cursor: pointer;
        position: relative;
        overflow: hidden;
        display: flex;
        justify-content: center;
        align-items: center;
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
  }
  .pagination {
    width: 100%;
    display: flex;
    justify-content: center;
    margin-top: 20px;
  }
}
// 修改el-card背景色
::v-deep .el-card {
  background-color: var(--materialCardBackground);
  border-color: var(--materialCardBackground);
}
</style>
