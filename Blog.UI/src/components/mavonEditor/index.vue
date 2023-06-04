<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2023-02-02 23:04:50
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-03 23:28:39
-->
<template>
  <div class="markdown-container">
    <mavon-editor
      ref="md"
      v-model="mdContent"
      :defaultOpen="defaultOpen"
      :toolbarsFlag="toolbarsFlag"
      :subfield="subfield"
      :scrollStyle="scrollStyle"
      :editable="scrollStyle"
      :ishljs="scrollStyle"
      :boxShadow="scrollStyle"
      :placeholder="placeholder"
      @imgAdd="mdImsAdd"
      @change="change"
      :codeStyle="codeStyle"
      :externalLink="externalLink"
      style="width:100%;height: 100%;"
    />

  </div>
</template>
  <script>
import { mapGetters } from "vuex";
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
import codeStyleList from "./codeStyle.json";

//md需要上传图片 所以引入我们对axios的封装 request
import { request } from "@/utils/request";
import defaultSettings from "@/config/defaultSettings.js";
export default {
  components: { mavonEditor },
  props: {
    value: {
      type: String,
      default: "",
    },
    defaultOpen: {
      type: String,
      default: "edit",
    },
    toolbarsFlag: {
      type: Boolean,
      default: true,
    },
    subfield: {
      type: Boolean,
      default: true,
    },
    scrollStyle: {
      type: Boolean,
      default: true,
    },
    editable: {
      type: Boolean,
      default: true,
    },
    ishljs: {
      type: Boolean,
      default: true,
    },
    boxShadow: {
      type: Boolean,
      default: true,
    },
    placeholder: {
      type: String,
      default: "可以开始撰写md文档了哦~",
    },
  },
  data: function () {
    // mavonEditor css样式CDN
    const mavonEditorCDN =
      "https://yinheyibei.oss-cn-beijing.aliyuncs.com/BLOG-CDN/mavonEditor";
    return {
      codeStyleList: codeStyleList.codeStyle,
      content: "",
      codeStyle: "",
      externalLink: {
        markdown_css: function () {
          // 这是你的markdown css文件路径
          return (
            mavonEditorCDN + "/mavon-editor/markdown/github-markdown.min.css"
          );
        },
        hljs_js: function () {
          // 这是你的hljs文件路径
          return mavonEditorCDN + "/mavon-editor/highlightjs/highlight.min.js";
        },
        hljs_css: function (css) {
          // 这是你的代码高亮配色文件路径
          return (
            mavonEditorCDN +
            "/mavon-editor/highlightjs/styles/" +
            css +
            ".min.css"
          );
        },
        hljs_lang: function (lang) {
          // 这是你的代码高亮语言解析路径
          return (
            mavonEditorCDN +
            "/mavon-editor/highlightjs/languages/" +
            lang +
            ".min.js"
          );
        },
        katex_css: function () {
          // 这是你的katex配色方案路径路径
          return mavonEditorCDN + "/mavon-editor/katex/katex.min.css";
        },
        katex_js: function () {
          // 这是你的katex.js路径
          return mavonEditorCDN + "/mavon-editor/katex/katex.min.js";
        },
      },

      //md文件图片资源
      img_file: {},

      //文档内容
      mdContent: undefined,
    };
  },

  // 实现数据双向绑定
  model: { prop: "value", event: "change" },

  created() {
    this.changeTheme();
    this.mdContent = this.value;
  },

  methods: {
    imgDel(pos) {},
    change(value) {
      this.$emit("change", value);
    },
    // 添加图片
    mdImsAdd(pos, $file) {
      this.img_file[pos] = $file;
      return;
    },

    /**
     * 上传图片路径
     */
    async uploadMdImgs() {
      for (let pos in this.img_file) {
        let formdata = new FormData();
        formdata.append("file", this.img_file[pos]);
        formdata.append("module", defaultSettings.articleTag);
        const data = {
          url: defaultSettings.uploadImgUrl,
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

    /**
     * 保存数据
     */
    save() {
      this.$refs.md.save();
    },

    /**
     * 切换主题
     */
    changeTheme() {
      const systemTheme = this.$store.getters.systemTheme;
      if (systemTheme === "dark") {
        this.codeStyle = "agate";
      } else {
        this.codeStyle = "atom-one-light";
      }
    },
  },
  computed: {
    ...mapGetters(["systemTheme"]),
  },
  watch: {
    systemTheme() {
      this.changeTheme();
    },
    value(value) {
      this.mdContent = value;
    },
  },
};
</script>

<style lang="scss" scoped>
.markdown-container {
  width: 100%;
  z-index: 1;
}

// 覆盖编辑器原有样式
::v-deep .v-note-op {
  background: var(--materialCardBackground) !important;
}
::v-deep .v-note-panel {
  background: var(--materialCardBackground) !important;
  .content-input-wrapper {
    background: var(--materialCardBackground) !important;
    textarea {
      background: var(--materialCardBackground) !important;
      color: var(--materialCardText) !important;
    }
  }
}
::v-deep .v-note-show {
  .v-show-content {
    background: var(--materialCardBackground) !important;
    color: var(--materialCardText) !important;
  }
}
</style>