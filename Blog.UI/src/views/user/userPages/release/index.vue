<!--
 * @Descripttion: 发布
 * @Author: Yerik
 * @Date: 2021-08-09 23:03:12
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 20:43:29
-->
<template>
  <div class="app-container">
    <div class="container">
      <div class="top-link">

        <el-select
          v-model="activeArticleType"
          placeholder="请选择文章状态"
          @change="changeFilterform()"
          size="medium"
        >
          <el-option
            v-for="(item, index) in articleType"
            :key="index"
            :label="item.label"
            :value="item.id"
            v-if="item.id!=1?isSelf:true"
          >
          </el-option>
        </el-select>
      </div>
      <div class="left-link">
        <div
          v-for="(item, index) in articleType"
          :key="index"
        >
          <div
            @click="changeMenu(item)"
            class="link-item"
            v-if="item.id!=1?isSelf:true"
          >
            <p :class="{
              'router-link-exact-active': item.id == activeArticleType,
            }">
              <span>{{ item.label }}</span>
            </p>
          </div>
        </div>

      </div>
      <div class="right-container">
        <div
          class="not-data"
          v-if="articleList.length == 0"
        >
          <div class="img-container">
            <img
              width="100%"
              :src="notDataImg"
              :alt="notDataImg"
            />
          </div>
        </div>
        <div
          class="article-container"
          v-else
        >
          <div class='article'>
            <div
              class="article-item"
              v-for="(item, index) in articleList"
              :key="index"
            >
              <div
                class="article-img"
                @click="toReadArticle(item)"
              >
                <img
                  width="100%"
                  height="100%"
                  :src="item.cover_img_url?$utils.imgUrl(item.cover_img_url):uncomplete"
                  alt=""
                />
              </div>
              <div
                class="article-title"
                @click="toReadArticle(item)"
              >
                <h4>{{ item.article_title }}</h4>
              </div>
              <div
                class="article-tage"
                v-if="$utils.isObject(item.getArticleClassification)"
              >
                <el-tag
                  size="mini"
                  effect="dark"
                  class="item-tag"
                ><i class="el-icon-folder-opened"></i>
                  <span>{{item.getArticleClassification.classification_name}}</span>
                </el-tag>
                <el-tag
                  size="mini"
                  type="success"
                  effect="dark"
                  class="item-tag"
                  v-for="(value, key) in item.special"
                ><i class="el-icon-collection-tag"></i>
                  {{ value.special_name }}</el-tag>
                <el-tag
                  size="mini"
                  type="info"
                  class="item-tag"
                  v-for="(value, key) in item.label"
                ><i class="el-icon-s-flag"></i>{{ value.label_name }}</el-tag>
              </div>
              <div class="article-time">
                <div class="time">
                  <span>{{ $utils.getPastTimes(item.create_time) }}</span>
                </div>
                <div class="other">
                  <span class="other-item"><i class="el-icon-chat-dot-square"></i>
                    {{ item.articleCommentNum }}</span>
                  <span class="other-item"><i class="el-icon-view"></i> {{ item.browse_num }}</span>
                  <span class="other-item"><i class="el-icon-star-off"></i> {{ item.thumbs_num }}</span>
                  <span class="other-item"><i class="el-icon-collection-tag"></i>
                    {{ item.collection_num }}</span>
                  <el-dropdown
                    style="margin-left: 20px"
                    size="mini"
                  >
                    <span class="el-dropdown-link">
                      <i class="el-icon-setting"></i>
                    </span>

                    <el-dropdown-menu
                      slot="dropdown"
                      style="width:80px;"
                    >
                      <el-dropdown-item
                        @click.native="articleAppeal(item)"
                        v-show="activeArticleType == 2 || activeArticleType == 3 "
                      >
                        <i class="el-icon-warning-outline"></i> 申诉
                      </el-dropdown-item>
                      <el-dropdown-item @click.native="articleEdit(item)">
                        <i class="el-icon-edit"></i> 编辑
                      </el-dropdown-item>
                      <el-popconfirm
                        title="你确定要删除这篇文章吗？"
                        @cancel='cancelDelete()'
                        @confirm='deleteItem(item)'
                      >
                        <el-dropdown-item slot="reference">
                          <i class="el-icon-delete"></i> 删除
                        </el-dropdown-item>
                      </el-popconfirm>
                    </el-dropdown-menu>
                  </el-dropdown>
                </div>
              </div>
            </div>
          </div>

          <!-- 加载更多 移动端 -->
          <div
            v-if="$utils.isMobile()"
            class="get-more-container"
          >
            <el-button
              :type="showGetMoreBtn?'primary':'info'"
              round
              size="mini"
              @click="getMore()"
              :loading='loading'
              :disabled='!showGetMoreBtn'
            ><span v-if='showGetMoreBtn'>加载更多</span><span v-else>没有更多了</span></el-button>
          </div>
          <!-- 分页 pc端-->
          <div
            class='pags-container'
            v-else
          >
            <el-pagination
              background
              layout="prev, pager, next"
              :total="filterForm.total"
              :page-size='filterForm.list_rows'
              :current-page='filterForm.page'
              @current-change='currentChange'
              small
            >
            </el-pagination>
          </div>

        </div>
      </div>
    </div>
    <!-- 申诉弹出框 -->
    <el-dialog
      :visible.sync="appealShow"
      :width="$utils.isMobile()?'90%':'300px'"
      :before-close="handleClose"
    >
      <template slot="title">
        <div class="appeal-title">
          关于对文章的<span>{{ appealArticleTitle }}</span>申诉内容
        </div>
      </template>
      <div class="appeal-container">
        <p>请输入申诉内容：</p>
        <el-input
          type="textarea"
          :autosize="{ minRows: 10 }"
          placeholder="请输入内容"
          v-model="appealForm.appealContainer"
          maxlength="450"
          show-word-limit
        >
        </el-input>
      </div>
      <span
        slot="footer"
        class="dialog-footer"
      >
        <el-button
          @click="handleClose"
          size="mini"
        >取 消</el-button>
        <el-button
          type="primary"
          @click="saveData()"
          size="mini"
          :loading="btnLoading"
        >确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import { getArticleReleaseOption } from "@/api/article/releaseArticle";
import {
  getArticleTypeList,
  appealArticle,
  deleteArticle,
} from "@/api/article/articleList";
import { mapGetters } from "vuex";
export default {
  name: "ReleaseList",
  data() {
    return {
      //判断是不是自己
      isSelf: undefined,
      //文章类型
      articleType: [
        { id: 0, label: "待审核", color: "#f50" },
        { id: 1, label: "审核通过", color: "#2db7f5" },
        { id: 2, label: "审核未通过", color: "#87d068" },
        { id: 3, label: "下架", color: "#87d068" },
        { id: 4, label: "草稿", color: "#87d068" },
        { id: 5, label: "申诉", color: "#108ee9" },
      ],

      //当前文章类型
      activeArticleType: 0,
      //过滤表单
      filterForm: {
        list_rows: 6,
        page: 1,
        total: 0,
      },

      //文章数据
      articleList: [],

      //页面选项配置参数
      configData: {
        //文章分类
        classification: [],

        //文章专题
        special: [],

        //文章标签
        label: [],
      },
      //没有数据图片
      notDataImg: require("@/assets/notData/notData.png"),

      //弹出框控制变量
      appealShow: false,

      //申诉文章标题
      appealArticleTitle: "",

      //申诉内容
      appealForm: {
        id: "",
        appealContainer: "",
      },

      //申诉框按钮加载状态
      btnLoading: false,

      // 获取更多按钮加载状态
      loading: false,

      // 是否显示加载更多按钮
      showGetMoreBtn: true,

      //当前用户
      userId: undefined,

      // 未完成图片
      uncomplete: require("@/assets/notData/uncomplete.png"),
    };
  },
  created() {
    this.activeArticleType = this.$route.query.activeArticleType || 1;
    this.activeArticleType = Number(this.activeArticleType);
    this.init();
  },
  methods: {
    /**
     * 初始化函数
     */
    init(type = true) {
      this.userId = this.$route.params.userId;
      if (this.userId != this.$store.getters.userId) {
        this.isSelf = false;
      } else {
        this.isSelf = true;
      }
      const query = {
        status: this.activeArticleType,
        userId: this.userId,
        ...this.filterForm,
      };
      getArticleTypeList(query).then((res) => {
        if (type) {
          this.articleList = Object.assign([], res.data.data);
        } else {
          this.articleList = this.articleList.concat(res.data.data);

          // 控制获取更改按钮显示

          this.loading = false;
        }
        this.filterForm.total = res.data.total;
        if (
          this.filterForm.total <
          this.filterForm.list_rows * this.filterForm.page
        ) {
          this.showGetMoreBtn = false;
        } else {
          this.showGetMoreBtn = true;
        }
      });
    },

    /**
     * 提交申诉内容
     */
    saveData() {
      this.btnLoading = true;
      if (this.appealForm.appealContainer == "") {
        this.$message.error("请输入申诉内容！");
        this.btnLoading = false;
        return;
      }
      const data = {
        article_id: this.appealForm.id,
        reason: this.appealForm.appealContainer,
      };
      appealArticle(data).then((res) => {
        this.btnLoading = false;
        if (res.code == 200) {
          this.$notify({
            title: "成功",
            message: "你的申诉已提交至后台管理员审核，请耐心等待1到3个工作日！",
            type: "success",
          });
        }
        this.handleClose();
      });
    },

    /**
     *编辑文章
     */
    articleEdit(item) {
      this.$router.push({
        path: "/release",
        query: {
          id: item.id,
          status: "edit",
        },
      });
    },

    /**
     * 删除文章
     */
    deleteItem({ id }) {
      deleteArticle({ id }).then((res) => {
        if (res.code == 200) {
          this.init();
          this.$notify({
            title: "成功",
            message: "你的文章已被删除！",
            type: "success",
          });
        }
      });
    },
    /**
     * 点击下拉菜单(申诉)
     */
    articleAppeal(item) {
      this.appealForm.id = item.id;
      this.appealArticleTitle = item.article_title;
      this.appealShow = true;
    },

    /**
     * 页面配置参数
     */
    getConfigData() {
      getArticleReleaseOption().then((res) => {
        this.configData = Object.assign({}, res.data);
      });
    },

    /**
     * 去阅读文章
     */
    toReadArticle({ id }) {
      this.$router.push({ name: "ReadArticle", query: { id } });
    },

    /**
     * 改变菜单
     */
    changeMenu({ id }) {
      if (id != this.activeArticleType) {
        this.activeArticleType = id;
      }
    },

    /**
     * 弹出框关闭回调
     */
    handleClose() {
      this.appealContainer = "";
      this.appealShow = false;
    },

    //取消删除
    cancelDelete() {
      return false;
    },

    //分页切换
    currentChange(page) {
      this.filterForm.page = page;
      this.init();
    },

    // 获取更多
    getMore() {
      this.loading = true;
      this.filterForm.page++;
      this.init(false);
    },

    // 下拉框改变筛选条件
    changeFilterform() {
      this.filterForm = {
        list_rows: 6,
        page: 1,
        total: 0,
      };
    },
  },
  computed: {
    ...mapGetters(["visitorId"]),
  },
  watch: {
    visitorId() {
      this.init();
    },
    activeArticleType() {
      this.init();
    },
  },
};
</script>

<style lang="scss" scoped>
@media only screen and (max-device-width: 750px) {
  .app-container {
    width: 100%;
    .container {
      flex-wrap: wrap;
      .top-link {
        display: flex;
      }
      .left-link {
        display: none;
      }
      .right-container {
        width: 100%;
      }
    }
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    width: 880px;
    .container {
      .top-link {
        display: none;
      }
      .right-container {
        width: calc(100% - 200px);
      }
    }
  }
}
.app-container {
  width: 100%;
  height: 100%;
  .container {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: flex-start;
    .top-link {
      width: 100%;
      justify-content: flex-end;
      margin-bottom: 20px;
      margin-right: 10px;
    }

    .left-link {
      width: 200px;
      min-height: 200px;
      border-right: 1px solid var(--pageBorder);
      background: var(--pageBackground);
      margin-right: 10px;
      .link-item {
        width: 100%;
        height: 40px;
        cursor: pointer;
        line-height: 40px;
        text-align: right;
        position: relative;
        color: var(--materialCardText);
        // 活动路由样式
        .router-link-exact-active {
          color: #1890ff;
          background: #e6f7ff;
          display: block;
        }
        .router-link-exact-active::after {
          content: " ";
          width: 2px;
          height: 40px;
          background: #409eff;
          position: absolute;
        }
        span {
          padding-right: 20px;
        }
      }
    }
    .right-container {
      background-color: var(--userDataBackground);
      .article-container {
        width: 100%;
        margin-top: 0;
        display: flex;
        justify-content: flex-start;
        align-items: top;
        flex-wrap: wrap;
        .article {
          width: 100%;
          display: flex;
          justify-content: flex-start;
          flex-wrap: wrap;
          min-height: 500px;

          @media only screen and (max-device-width: 750px) {
            .article-item {
              width: 100%;
            }
          }
          @media only screen and (min-device-width: 750px) {
            .article-item {
              width: 250px;
            }
          }
          .article-item {
            cursor: pointer;
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            background: var(--materialCardBackground);
            border-radius: 5px;
            overflow: hidden;
            transition: 0.5s;
            margin-right: 10px;
            margin-bottom: 10px;
            height: 230px;
            padding: 5px;
            border: 1px solid var(--pageBorder);
            .article-img {
              width: 100%;
              height: 125px;
              border-radius: 5px;
              overflow: hidden;
            }
            .article-title {
              width: 100%;
              overflow: hidden;
              display: -webkit-box;
              -webkit-box-orient: vertical;
              -webkit-line-clamp: 2;
              text-align: left;
              height: 60px;
              color: var(--materialCardText);
              h4 {
                margin-top: 10px;
              }
            }
            .article-title:hover {
              color: #3390ff;
            }
            .article-tage {
              display: flex;
              justify-content: flex-start;
              width: 100%;
              overflow-x: auto;
              &::-webkit-scrollbar {
                height: 4px;
              }
              &::-webkit-scrollbar-thumb {
                background-color: rgba(144, 147, 153, 0.3);
                border-radius: 2px;
              }
              &::-webkit-scrollbar-track {
                background-color: #f0f2f5;
              }
              &::-webkit-scrollbar-thumb:hover {
                background-color: rgba(144, 147, 153, 0.6);
              }
              &::-webkit-scrollbar-thumb:active {
                background-color: rgba(144, 147, 153, 0.9);
              }
              .item-tag {
                margin-right: 3px;
              }
            }
            .article-time {
              margin-top: 10px;
              font-size: 12px;
              width: 100%;
              display: flex;
              justify-content: space-between;
              align-items: center;
              position: relative;
              color: var(--materialCardContent);
              .time::before {
                position: relative;
                content: " ";
                display: inline-block;
                width: 5px;
                height: 5px;
                background-color: #409eff;
                border-radius: 50%;
                margin-right: 5px;
                top: -1px;
              }
              .other-item {
                margin-right: 5px;
              }
            }
          }
          .article-item:hover {
            box-shadow: 2px 4px 6px rgba(0, 0, 0, 0.22),
              0 0 6px rgba(0, 0, 0, 0.14);
          }
        }

        .pags-container {
          width: 100%;
          display: flex;
          justify-content: flex-end;
        }
        .get-more-container {
          width: 100%;
          display: flex;
          justify-content: center;
          margin-top: 5px;
        }
      }
      .not-data {
        width: 100%;
        height: 100%;
        display: flex;
        background-color: var(--pageBackground);
        justify-content: center;
        align-content: center;
        .img-container {
          width: 400px;
        }
      }
    }
  }
  .appeal-title {
    width: 100%;
    text-align: left;
    font-size: 16px;
    span {
      color: #409eff;
      font-size: 16px;
      font-weight: 600;
    }
  }
  .appeal-container {
    width: 100%;
    text-align: left;
    p {
      margin-bottom: 5px;
    }
  }
}
</style>