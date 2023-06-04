<!--
 * @Descripttion: 收藏
 * @Author: Yerik
 * @Date: 2021-08-28 22:24:23
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 20:50:56
-->
<template>
  <div class="app-container">
    <div class="container">
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
        <div
          v-for="(item, index) in articleList"
          :key="index"
          class="article-item-container"
        >
          <div
            v-if="item.articleContent"
            class="article-item"
          >
            <div
              class="article-img"
              @click="toReadArticle(item)"
            >
              <img
                width="100%"
                height="100%"
                :src="$utils.imgUrl(item.articleContent.cover_img_url)"
                alt=""
              />
            </div>
            <div
              class="article-title"
              @click="toReadArticle(item)"
            >
              <h4>{{ item.articleContent.article_title }}</h4>
            </div>
            <div class="article-tage">
              <el-tag
                size="mini"
                effect="dark"
                class="item-tag"
              ><i class="el-icon-folder-opened"></i>
                {{ item.articleContent.getArticleClassification.classification_name }}</el-tag>
              <el-tag
                size="mini"
                type="success"
                effect="dark"
                class="item-tag"
                v-for="(value, key) in item.articleContent.special"
              ><i class="el-icon-collection-tag"></i>
                {{ value.special_name }}</el-tag>
              <el-tag
                size="mini"
                type="info"
                class="item-tag"
                v-for="(value, key) in item.articleContent.label"
              ><i class="el-icon-s-flag"></i>{{ value.label_name }}</el-tag>
            </div>
            <div class="article-time">
              <div class="time">
                <span>{{
                $utils.getPastTimes(item.articleContent.create_time)
              }}</span>
              </div>
              <div class="other">
                <span class="other-item"><i class="el-icon-chat-dot-square"></i> {{item.articleContent.articleCommentNum}}</span>
                <span class="other-item"><i class="el-icon-view"></i>
                  {{ item.articleContent.browse_num }}</span>
                <span class="other-item"><i class="el-icon-star-off"></i>
                  {{ item.articleContent.thumbs_num }}</span>
                <span class="other-item"><i class="el-icon-collection-tag"></i>
                  {{ item.articleContent.collection_num }}</span>
                <!-- <el-dropdown
                style="margin-left: 20px"
                size="mini"
              >
                <span class="el-dropdown-link">
                  <i class="el-icon-setting"></i>
                </span>
                <el-dropdown-menu slot="dropdown">
                  <el-dropdown-item @click.native="articleAppeal(item)">
                    申诉</el-dropdown-item
                  >
                </el-dropdown-menu>
              </el-dropdown> -->
              </div>
            </div>
          </div>

          <!-- -->
          <div
            v-else
            class='dont-look'
          >
            <el-card
              style="width:100%"
              shadow="never"
            >
              <div slot="header">
                <el-button
                  style="float: right; padding: 0px 0"
                  type="text"
                  @click="cancelCollection(item)"
                >取消收藏</el-button>
              </div>
              <div>
                <p>很抱歉！由于某种特殊原因，当前文章处于不可见状态，如需阅读，请联系文章作者，或耐心等待作者处理、管理员审核。如若不需要可删除。</p>
              </div>
            </el-card>
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
</template>

<script>
import { getUserArticleCollection } from "@/api/article/articleList";
import { changArticleCollection } from "@/api/article/recommendArticle";
export default {
  name: "Collection",
  data() {
    return {
      //文章数据
      articleList: [],

      //没有数据图片
      notDataImg: require("@/assets/notData/notData.png"),

      //访客ID
      userId: undefined,

      //分页
      filterForm: {
        list_rows: 8,
        page: 1,
        total: 0,
      },

      // 获取更多按钮加载状态
      loading: false,

      // 是否显示加载更多按钮
      showGetMoreBtn: true,
    };
  },
  created() {
    this.userId = this.$route.params.userId;
    this.init();
  },
  methods: {
    init(type = true) {
      //数据初始化
      const query = {
        userId: this.userId,
        ...this.filterForm,
      };
      getUserArticleCollection(query).then((res) => {
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

    //取消收藏
    cancelCollection({ article_id }) {
      changArticleCollection({ article_id }).then((res) => {
        if (res.code == 200) {
          this.init();
          this.$notify({
            title: "成功",
            message: "你已取消关注！",
            type: "success",
          });
        }
      });
    },

    /**
     * 去阅读文章
     */
    toReadArticle(item) {
      this.$router.push({
        name: "ReadArticle",
        query: { id: item.article_id },
      });
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
  },
};
</script>

<style lang="scss" scoped>
.app-container {
  width: calc(100% - 10px);
  height: 100%;
  background-color: var(--userDataBackground);
  .container {
    width: 100%;
    display: flex;
    justify-content: flex-start;
    align-items: flex-start;
    flex-wrap: wrap;
    height: 100%;
    .article-container {
      width: 100%;
      display: flex;
      justify-content: flex-start;
      align-items: top;
      flex-wrap: wrap;

      .article-item-container {
        margin:0 5px ;
      }
      @media only screen and (max-device-width: 750px) {
        .article-item-container {
          width: calc(100% - 10px);
        }
      }
      @media only screen and (min-device-width: 750px) {
        .article-item-container {
          width: 250px;
        }
      }
      .article-item {
        width: calc(100% - 10px);
        padding:5px;
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
      .dont-look {
        width: 270px;
        p {
          font-size: 14px;
          text-indent: 2em;
          text-align: left;
          color: rgb(177, 177, 177);
        }
      }
      .article-item:hover {
        box-shadow: 2px 4px 6px rgba(0, 0, 0, 0.22), 0 0 6px rgba(0, 0, 0, 0.14);
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
</style>