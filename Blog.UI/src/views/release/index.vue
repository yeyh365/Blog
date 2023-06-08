<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-07-29 19:25:01
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-03 23:08:57
-->
<template>
  <div
    class="app-container"
    @drop.prevent.stop="dragMd($event)"
    dragover.prevent.stop
  >
    <div class="container">
      <!-- 文章标题 -->
      <div class="article-title">
        <input
          type="text"
          v-model="articleForm.ArticleTitle"
          @mouseover="mouseover"
          @mouseleave="mouseleave"
          placeholder="请输入文章标题"
          class="title-input"
        />
        <div
          class="split-line"
          ref="split-line"
        ></div>
      </div>
      <!-- 富文编辑器 -->
      <div
        class="mark-down"
        @drop.prevent.stop="dragMd($event)"
        dragover.prevent.stop
      >
        <MavonEditor
          ref="md"
          v-model="articleForm.ArticleContent"
          :subfield="false"
          :scrollStyle="true"
          placeholder="开始编辑吧！（可以直接拖入md文件或者txt文件解析哦！）"
          style="height: 100%; z-index: 1"
        />
      </div>
      <!-- 选项文章选项 -->
      <div class="article-option">
        <div class="article-form">
          <el-form
            ref="form"
            :model="articleForm"
          >
            <el-form-item
              label="文章分类"
              style="margin:0"
            >
              <el-select
                v-model="articleForm.ArticleClassification"
                :size="$utils.isMobile()?'medium' :'mini'"
                style="width: 100%;"
                clearable
                placeholder="请选择当前文章的类型"
              >
                <el-option
                  v-for="(item,key) in classificationOptions.Keywords"
                  :key="key"
                  :label="item.TypeName"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="文章专题">
              <el-select
                v-model="articleForm.ArticleSpecial"
                :size="$utils.isMobile()?'medium' :'mini'"
                style="width: 100%"
                multiple
                clearable
                placeholder="请选择当前文章的专题"
                :multiple-limit='5'
              >

                <el-option
                  v-for="(item,index) in specialOptions.Keywords"
                  :key="index"
                  :label="item.TypeName"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="文章标签">
              <el-select
                v-model="articleForm.ArticleLabel"
                :size="$utils.isMobile()?'medium' :'mini'"
                style="width: 100%"
                multiple
                clearable
                placeholder="请选择当前文章的标签"
                :multiple-limit='5'
              >
                <el-option
                  v-for="(item,index) in labelOptions.Keywords"
                  :key="index"
                  :label="item.TypeName"
                  :value="item.Id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-form>
        </div>
        <div class="article-form">
          <el-form>
            <el-form-item label="文章封面">
              <el-upload
                class="upload-demo"
                drag
                :action="action"
                :headers="headers"
                :data="uploadData"
                :multiple="false"
                :show-file-list="false"
                :on-success="handleAvatarSuccess"
                :before-upload="beforeAvatarUpload"
              >
                <div v-if="articleForm.CoverImgUrl == ''">
                  <i class="el-icon-upload"></i>
                  <div class="el-upload__text">
                    将图片拖到此处，或<em>点击上传</em>
                  </div>
                </div>
                <div v-else>
                  <el-image :src="$utils.imgUrl(articleForm.CoverImgUrl)"></el-image>
                </div>

                <div
                  class="el-upload__tip"
                  slot="tip"
                >
                  只能上传jpg/png文件，且不超过5M
                </div>
              </el-upload>
            </el-form-item>
          </el-form>
        </div>
      </div>
      <!-- 提交保存按钮 -->
      <div class="btn-container">
        <div class="btn">
          <el-button
            type="info"
            icon="el-icon-document-checked"
            size="small"
            @click="preservationData"
            :loading='loading.preservationLoading'
          >保存</el-button>
          <el-button
            type="primary"
            @click="saveData"
            :loading='loading.saveDataLoading'
            icon="el-icon-circle-check"
            size="small"
          >提交</el-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//引入markdown富文本编辑器
import MavonEditor from "@/components/mavonEditor";

//md需要上传图片 所以引入我们对axios的封装 request
import { request } from "@/utils/request";

import baseSetting from "@/config/defaultSettings"; // 引入全局基本配置

import { isEmptyObject } from "@/utils/validate"; //引入工具函数

import defaultSettings from "@/config/defaultSettings.js";
import ArticleService from"@/api/services/ArticleService"
import {
  getArticleReleaseOption,
  blogUserReleaseContent,
  getEditArticle,
} from "@/api/article/releaseArticle";
export default {
  name: "Release",
  components: {
    MavonEditor,
  },
  data() {
    return {
      //表单数据
      articleForm: {
        ArticleTitle: "", //文章标题
        ArticleContent: "", //文章主体内容
        ArticleClassification: "", //文章分类ID
        ArticleSpecial: [], //文章专题ID
        ArticleLabel: [], //文章标签ID
        CoverImgId: "", //封面ID
        CoverImgUrl: "", //封面路
      },

      //loading动画
      loading: {
        preservationLoading: false,
        saveDataLoading: false,
      },

      //文章分类选项 （单选）
      classificationOptions: [],

      //文章专题选项 （多选）
      specialOptions: [],

      //文章标签选项 （多选）
      labelOptions: [],

      //md文件图片资源
      img_file: {},

      // 组件上传额外参数
      uploadData: {
        module: "md",
      },
    };
  },
  created() {
    this.init();
    if (this.$route.query.status == "edit") {
      this.articleForm.id = this.$route.query.id;
      this.getEditData();
    }
  },

  methods: {
    init() {
           ArticleService.GetArticleTypeList().then(res=>{
        this.classificationOptions = res.Data[0];
         this.specialOptions = res.Data[1];
         this.labelOptions = res.Data[2];
           })
      //初始化文章选项数据
      // getArticleReleaseOption().then((res) => {
      //   this.classificationOptions = res.data.classification;
      //   this.specialOptions = res.data.special;
      //   this.labelOptions = res.data.label;
      // });
    },

    /**
     * 获取需要修改的数据
     */
    getEditData() {
      const query = {
        id: this.articleForm.id,
      };
      getEditArticle(query).then((res) => {
        this.articleForm = Object.assign({}, res.data);
        if (this.articleForm.ArticleClassification == 0) {
          this.articleForm.ArticleClassification = undefined;
        }
      });
    },

    /**
     *提交文章数据
     */
    async saveData() {
      if (this.articleForm.ArticleTitle == "") {
        this.$message.error("你不会忘记了文章标题吧！");
        return;
      } else if (this.articleForm.ArticleContent == "") {
        this.$message.error("文章内容不能为空呀！");
        return;
      } else if (this.articleForm.ArticleClassification == "") {
        this.$message.error("记得选择文章分类哦！");
        return;
      } else if (this.articleForm.CoverImgUrl == "") {
        this.$message.error("记得上传文章封面哦！");
        return;
      }
      this.$refs.md.save();
      //设置延迟时间
      await this.$refs.md.uploadMdImgs();
      this.$refs.md.save();
            const data = Object.assign({}, this.articleForm);
      data.Status = 0;
      data.IsAppeal = 0;
             ArticleService.CreateArticle(data).then(res=>{
                if (res) {
          this.articleForm.ArticleTitle = "";
          this.articleForm.ArticleContent = "";
          this.articleForm.ArticleClassification = "";
          this.articleForm.ArticleLabel = "";
          this.articleForm.ArticleSpecial = "";
          const USERID = this.$store.getters.userId;
          this.$store.commit("SET_VISITOR_ID", USERID);
          const VISITORID = this.$store.getters.visitorId;
          this.$router.push(`/userInfo/${VISITORID}/releaseList`);
          this.$notify({
            title: "成功",
            message: "你的文章已提交，请耐心等待！",
            type: "success",
          });
        }
       })

      // blogUserReleaseContent(data).then((res) => {
      //   if (res) {
      //     this.articleForm.ArticleTitle = "";
      //     this.articleForm.ArticleContent = "";
      //     this.articleForm.ArticleClassification = "";
      //     this.articleForm.ArticleLabel = "";
      //     this.articleForm.ArticleSpecial = "";
      //     const USERID = this.$store.getters.userId;
      //     this.$store.commit("SET_VISITOR_ID", USERID);
      //     const VISITORID = this.$store.getters.visitorId;
      //     this.$router.push(`/userInfo/${VISITORID}/releaseList`);
      //     this.$notify({
      //       title: "成功",
      //       message: "你的文章已提交至后台管理员审核，请耐心等待！",
      //       type: "success",
      //     });
      //   }
      // });
    },

    /**
     * 保存md文件
     */
    saveMd(value) {
      this.articleForm.ArticleContent = value;
    },

    /**
     * md文件上传图片时的事件
     */
    mdImsAdd(pos, $file) {
      this.img_file[pos] = $file;
      return;
      //封装图片数据格式
      let formdata = new FormData();
      formdata.append("file", $file);
      formdata.append("module", defaultSettings.articleTag);

      //封装请求数据
      const data = {
        url: defaultSettings.uploadImgUrl,
        method: "POST",
        data: formdata,
      };

      request(data).then((res) => {
        if (res.code == 200) {
          //获取后端返回的图片路径
          const mdImgUrl = this.$utils.imgUrl(res.data.img_path);
          //替换md文章中的图片路径
          this.$refs.md.$img2Url(pos, mdImgUrl);
        }
      });
    },

    /**
     * 上传图片路径
     */
    async uploadMdImgs() {
      for (let pos in this.img_file) {
        let formdata = new FormData();
        formdata.append("file", this.img_file[pos]);
        formdata.append("module", action);
        const data = {
          url: action,
          method: "POST",
          data: formdata,
        };
        const res = await request(data);
        //获取后端返回的图片路径
        const mdImgUrl = this.$utils.imgUrl(res.data.img_path);
        //替换md文章中的图片路径
        this.$refs.md.$img2Url(pos, mdImgUrl);
      }
    },

    //封面上传成后
    handleAvatarSuccess(response) {
      if (response.Code == 200) {
        //this.articleForm.CoverImgId = response.Data.id;
        this.articleForm.CoverImgUrl = response.Data;
        this.$notify({
          title: "封面上传成功",
          message: "你的封面已经上传成功，记得点击保存按钮哦！",
          type: "success",
        });
      }
    },

    //封面上传之前
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

    /**
     * 保存文章
     */
    async preservationData() {
      const data = Object.assign({}, this.articleForm);
      // 判断用户是否什么都没填写
      if (isEmptyObject(data)) {
        this.$notify.error({
          title: "错误",
          message: "您的文章完全没内容（文章封面除外），点击保存不会保存哦",
        });
        return;
      }
      this.loading.preservationLoading = true;
      this.$refs.md.save();
      await this.$refs.md.uploadMdImgs();
      data.ArticleContent = this.articleForm.ArticleContent;

      data.status = 4;
      this.$refs.md.save();
             ArticleService.CreateArticle(data).then(res=>{
                if (res) {
          this.articleForm.ArticleTitle = "";
          this.articleForm.ArticleContent = "";
          this.articleForm.ArticleClassification = "";
          this.articleForm.ArticleLabel = "";
          this.articleForm.ArticleSpecial = "";
          const USERID = this.$store.getters.userId;
          this.$store.commit("SET_VISITOR_ID", USERID);
          const VISITORID = this.$store.getters.visitorId;
          this.$router.push(`/userInfo/${VISITORID}/releaseList`);
          this.$notify({
            title: "成功",
            message: "你的文章已提交，请耐心等待！",
            type: "success",
          });
        }
       })
      // blogUserReleaseContent(data).then((res) => {
      //   if (res.code == 200) {
      //     this.articleForm.id = res.data;
      //     this.$notify.success({
      //       title: "成功",
      //       message: "文章保存成功",
      //     });
      //     this.loading.preservationLoading = false;
      //   }
      // });
    },

    //鼠标移入输入框
    mouseover() {
      this.$refs["split-line"].classList.add("split-line-active");
    },
    mouseleave() {
      this.$refs["split-line"].classList.remove("split-line-active");
    },

    /**
     * 拖拽事件
     * @param {*} e
     * @param {*} index
     */
    dragMd(e) {
      e.stopPropagation();
      e.preventDefault();

      // 读取md文件内容
      const mdFile = e.dataTransfer.files[0];

      // 判断是否是md或者txt文件
      const regs = /md|MD|txt|TXT/g;
      // const name =
      const isMdOrTxt = regs.test(mdFile.name);
      if (isMdOrTxt) {
        const reader = new FileReader();
        reader.readAsText(mdFile, "UTF-8");
        reader.onload = (e) => {
          const mdContent = e.target.result;
          this.articleForm.ArticleContent = mdContent;
        };
      } else {
        this.$notify.error({
          title: "文件格式错误",
          message: "请上传md或者txt格式文件",
        });
      }
      // 清除文件
      e.dataTransfer.clearData();
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
@media only screen and (max-device-width: 750px) {
  .app-container {
    .container {
      width: calc(100% - 10px);
      padding: 0 5px;
    }
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    .container {
      width: 100%;
    }
  }
}
.app-container {
  width: 100%;

  .container {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;

    .article-title {
      width: 100%;
      padding: 20px;
      height: 20px;
      background: var(--materialCardBackground);
      border-radius: 5px;
      margin-bottom: 10px;
      height: 30px;
      position: relative;

      .title-input {
        border: none;
        border-bottom: 1px solid #f3f3f3;
        width: 100%;
        outline: none;
        line-height: 30px;
        z-index: 0;
        font-size: 18px;
        background: var(--materialCardBackground);
        color: var(--materialCardText);
      }

      .split-line {
        position: absolute;
        background: #7bbdf8;
        bottom: 21px;
        z-index: 100;
        transition: width 2s;
        height: 1px;
      }

      .split-line-active {
        transition: 2s;
        width: calc(100% - 40px);
      }
    }

    .mark-down {
      width: 100%;
      z-index: 100;
      max-height: 500px;
      overflow: hidden;
      margin-bottom: 10px;
      background: var(--materialCardBackground);
    }
    @media only screen and (max-device-width: 750px) {
      .article-option {
        flex-wrap: wrap;
        .article-form {
          width: 100%;
        }
      }
    }
    @media only screen and (min-device-width: 750px) {
      .article-option {
        .article-form {
          width: 50%;
        }
      }
      .article-option :first-child {
        margin-right: 10px;
      }
    }
    .article-option {
      width: 100%;
      margin-bottom: 10px;
      display: flex;
      justify-content: space-evenly;
      align-items: top;

      .article-form {
        padding: 10px;
        background: var(--materialCardBackground);
        border-radius: 5px;

        .upload-demo {
          width: 100%;
        }
      }
    }

    .btn-container {
      // display: none;
      width: calc(100% - 40px);
      display: flex;
      justify-content: center;
      padding: 20px;
      background: var(--materialCardBackground);
      border-radius: 5px;
      margin-bottom: 10px;

      .btn {
        width: 200px;
        display: flex;
        justify-content: space-around;
      }
    }
  }
}
</style>