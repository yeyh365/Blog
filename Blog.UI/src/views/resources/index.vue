<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-07-26 21:17:48
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-01 21:40:15
-->
<template>
  <div class="app-container">
    <div class="container">
      <!-- 头部背景 -->
      <div class="header-container">

        <el-image
          style="width: 100%; height: 100%"
          fit="cover"
          :src="$utils.imgUrl(imgContent.imgUrl)"
        >
        </el-image>
        <div class='describe'>
          <div>
            <h4><i
                class="el-icon-edit"
                v-if='imgContent.status==0'
              ></i>
              <i
                class="el-icon-folder-opened"
                v-if='imgContent.status==1'
              ></i>
              <i
                class="el-icon-collection-tag"
                v-if='imgContent.status==2'
              ></i>
              <i
                class="el-icon-s-flag"
                v-if='imgContent.status==3'
              ></i> {{imgContent.text}}
            </h4>
            <p>{{imgContent.describe}}</p>
          </div>
        </div>
      </div>
      <!-- 过滤方法 -->
      <div class="filter-container">
        <div class='filter-status'>
          <div
            class='status-item'
            :class="{'excellent':filterForm.status=='excellent'}"
            @click="changeFilterFormStatus('excellent')"
          >
            <svg-icon
              icon-class="excellent"
              class="svg-icon"
            />
            <span>
              推荐
            </span>
          </div>
          <div
            class='status-item'
            :class="{'hot':filterForm.status=='hot'}"
            @click="changeFilterFormStatus('hot')"
          >
            <svg-icon
              icon-class="hot"
              class="svg-icon"
            />
            <span>
              最火
            </span>
          </div>
          <div
            class='status-item'
            :class="{'new':filterForm.status=='new'}"
            @click="changeFilterFormStatus('new')"
          >
            <svg-icon
              icon-class="new"
              class="svg-icon"
            />
            <span>
              最新
            </span>
          </div>

        </div>
        <!-- 文章分类过滤 -->
        <div
          class="filter-classification"
          v-if="optionsData[0]!= null"
        >
          <div class="filter-header">
            <svg-icon
              icon-class="filter-class"
              class="icon-class"
            /> 分类
            <span class="division">|</span>
          </div>
          <div
            class="filter-item"
            v-if="optionsData[0]"
          >
            <span
              v-for="(item, index) in optionsData[0].Keywords"
              :key="index"
              @click="changeClassificationFilter(item)"
              :class="{
                'actice-item': item.Id == filterForm.ClassIfication,
              }"
            >{{ item.TypeName }}</span>
          </div>
        </div>
        <!-- 文章专题过滤 -->
        <div
          class="filter-classification"
          v-if="optionsData[2]!= null"
        >
          <div class="filter-header">
            <svg-icon
              icon-class="filter-special"
              class="icon-class"
            /> 专题
            <span class="division">|</span>
          </div>
          <div
            class="filter-item"
            v-if="optionsData[2]"
          >
            <span
              v-for="(item, index) in optionsData[2].Keywords"
              :key="index"
              @click="changeSpecialFilter(item)"
              :class="{
                'actice-item': filterForm.Special==item.Id,
              }"
            >{{ item.TypeName }}</span>
          </div>
        </div>
        <!-- 文章标签 -->
        <div
          class="filter-classification"
          v-if="optionsData[1]!= null"
        >
          <div class="filter-header">
            <svg-icon
              icon-class="filter-label"
              class="icon-class"
            />标签 <span class="division">|</span>
          </div>
          <div
            class="filter-item"
            v-if="optionsData[1]"
          >
            <span
              v-for="(item, index) in optionsData[1].Keywords"
              :key="index"
              @click="changeLabelFilter(item)"
              :class="{
                'actice-item': filterForm.Label==item.Id,
              }"
            >{{ item.TypeName }}</span>
          </div>
        </div>
      </div>
      <!-- 内容展示 -->
      <div class="article-container">
        <!-- 没有数据 -->
        <div
          class="no-data"
          v-if="this.articleList.length == 0"
        >
          <div><span>找不到文章咯！</span></div>
        </div>
        <!-- 数据展示 -->
        <div
          class="article-data"
          v-else
        >
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
                :src="$utils.imgUrl(item.CoverImgUrl)"
                alt=""
              />
            </div>
            <div
              class="article-title"
              @click="toReadArticle(item)"
            >
              <h4>{{ item.ArticleTitle }}</h4>
            </div>
            <div class="article-tage">
              <el-tag
                size="mini"
                effect="dark"
                class="item-tag"
              ><i class="el-icon-folder-opened"></i>
                {{ item.Classification=item.Classification.length===0?"111":"222" }}</el-tag>
              <el-tag
                size="mini"
                type="success"
                effect="dark"
                class="item-tag"
                v-for="(value, key) in item.Special"
              ><i class="el-icon-collection-tag"></i>
                {{ value.TypeName }}</el-tag>
              <el-tag
                size="mini"
                type="info"
                class="item-tag"
                v-for="(value, key) in item.Label"
              ><i class="el-icon-s-flag"></i>{{ value.TypeName }}</el-tag>
            </div>
             <div class="article-time">
              <div class="time">
                <div
                  class="avatar"
                  style="margin-right: 5px"
                >
                  <img
                    width="100%"
                    height="100%"
                    :src="$utils.imgUrl(item.UserInfo.Photo)"
                    alt=""
                  />
                </div>
                <div style="margin-right: 5px">
                  <span>{{ item.UserInfo.Name }}</span>
                </div>
                <div>
                   <!-- <span>{{ item.Created=item.Created?'没时间':$utils.getPastTimes(item.Created) }}</span>  -->
                </div>
              </div>
              <div class="other">
                <!-- <span class="other-item"><i class="el-icon-chat-dot-square"></i> {{item.articleCommentNum}}</span> -->
                <span class="other-item"><i class="el-icon-view"></i> {{ item.BrowseNumCount }}</span>
                <span class="other-item"><i class="el-icon-star-off"></i> {{ item.ThumbsNum }}</span>
                <span class="other-item"><i class="el-icon-collection-tag"></i> {{ item.CollectionNum }}</span>
              </div>
            </div>
          </div>
        </div>
        <!-- 加载跟多按钮 -->
        <div class="load-more">
          <el-button
            :type="showGetMoreBtn?'primary':'info'"
            round
            size="mini"
            @click="getMoreArticle()"
            :disabled='!showGetMoreBtn'
          ><span v-if='showGetMoreBtn'>加载更多</span><span v-else>没有更多了</span></el-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getArticleReleaseOption } from "@/api/article/releaseArticle";
import { getRecommendArticle } from "@/api/article/recommendArticle";
import ArticleService from '@/api/services/ArticleService'
export default {
  name: "Resources",

  data() {
    return {
      //过滤数据
      optionsData: {
        classification: [], //文章分类
        special: [], //文章专题
        label: [], //文章标签
      },

      //文章数据
      articleList: [],

      //过滤表单
      filterForm: {
        status: "excellent",
        ClassIfication: "",
        Special: "",
        Label: "",
      },

      //分页参数
      Pages: {
        Limit: 3,
        Page: 1,
      },

      //显示加载跟多按钮
      showGetMoreBtn: true,

      //顶部图片展示内容
      imgContent: {
        imgUrl:
          "https://yinheyibei.oss-cn-beijing.aliyuncs.com/BLOG-MD/index.jpg",
        text: "洞幽察微",
        describe: "相信奇迹的人，一定也和奇迹一样了不起吧！",
        status: 0,
      },
    };
  },
  created() {
    this.configData();
    this.init(true);
  },
  methods: {
    //初始化函数
    init(status = true) {
      const query = Object.assign({}, this.filterForm, this.Pages);
      console.log('query',query)
       ArticleService.GetArticleList(query).then((res) => {
        console.log('111',res)
        if (status) {
          this.articleList = Object.assign([], res.Data);
        } else {
          if (res.Data.length > 0) {
            this.articleList = this.articleList.concat(res.Data);
          }
        }
        console.log('222',res.Data.length)
        if (res.Data.length < this.Pages.Limit) {
          this.showGetMoreBtn = false;
        }
      console.log('articleList',this.articleList.length)
      })
      //文章数据
      //getRecommendArticle(query).then((res) => {
      //   if (status) {
      //     this.articleList = Object.assign([], res.data);
      //   } else {
      //     if (res.data.length > 0) {
      //       this.articleList = this.articleList.concat(res.data);
      //     }
      //   }
      //   if (res.data.length < this.Pages.Limit) {
      //     this.showGetMoreBtn = false;
      //   }
      // });
    },

    //更改过滤表单参数 （文章分类）
    changeClassificationFilter(item) {
      console.log('item',item)
      this.imgContent.imgUrl = item.ImgUrl;
      this.imgContent.describe = item.Describe;
      this.imgContent.text = item.TypeName;
      this.imgContent.status = 1;
      if (item.Id == this.filterForm.ClassIfication) {
        this.filterForm.ClassIfication = "";
      } else {
        this.filterForm.ClassIfication = item.Id;
      }
      // this.articleList = [];
      this.Pages.Page = 1;
      this.showGetMoreBtn = true;
      this.init(true);
    },

    //更改过滤表单参数 （文章专题）
    changeSpecialFilter(item) {
            console.log('changeSpecialFilteritem',item)
      this.imgContent.imgUrl = item.ImgUrl;
      this.imgContent.describe = item.Describe;
      this.imgContent.text = item.TypeName;
      this.imgContent.status = 2;
      if (this.filterForm.Special == item.Id) {
        this.filterForm.Special = "";
      } else {
        this.filterForm.Special = item.Id;
      }
      // this.articleList = [];
      this.Pages.Page = 1;
      this.showGetMoreBtn = true;
      this.init(true);

      return;
      const index = this.filterForm.Special.indexOf(item.id);
      if (index == -1) {
        this.filterForm.Special.push(item.id);
      } else {
        this.filterForm.Special.splice(index, 1);
      }
    },

    //更改过滤表单参数 （文章标签）
    changeLabelFilter(item) {
                  console.log('changeLabelFilteritem',item)
      this.imgContent.imgUrl = item.ImgUrl;
      this.imgContent.describe = item.Describe;
      this.imgContent.text = item.TypeName;
      this.imgContent.status = 3;
      if (this.filterForm.Label == item.Id) {
        this.filterForm.Label = "";
      } else {
        this.filterForm.Label = item.Id;
      }
      // this.articleList = [];
      this.Pages.Page = 1;
      this.showGetMoreBtn = true;
      this.init(true);

      return;
      const index = this.filterForm.Label.indexOf(item.id);
      if (index == -1) {
        this.filterForm.Label.push(item.id);
      } else {
        this.filterForm.Label.splice(index, 1);
      }
    },
    //改变排序方式
    changeFilterFormStatus(status) {
      this.filterForm.status = status;
      this.showGetMoreBtn = true;
      this.Pages.Page = 1;
      this.init(true);
    },

    //查看详细文章
    toReadArticle(item) {
      var Id=item.Id
      this.$router.push({ name: "ReadArticle", query: { Id } });
    },

    //加载更多文章
    getMoreArticle() {
      this.Pages.Page++;
      this.init(false);
    },

    //配置数据函数
    configData() {
     ArticleService.GetArticleTypeList().then((res) => {
            this.optionsData = Object.assign([], res.Data);
            console.log('DATA',this.optionsData[0].Keywords[0].Id)
      
      })
      //获取过滤数据
      // getArticleReleaseOption().then((res) => {
      //   this.optionsData = Object.assign({}, res.data);
      // });
    },
  },
  // watch: {
  //   filterForm: {
  //     handler() {
  //       this.showGetMoreBtn = true;
  //       this.pages.page = 0;
  //       this.init(true);
  //     },
  //     deep: true,
  //   },
  // },
};
</script>

<style lang="scss" scoped>
@media only screen and (max-device-width: 750px) {
  .app-container {
    width: 100%;
    .container {
      .header-container {
        margin: 0 10px 0 10px;
        width: calc(100% - 20px);
        height: 200px;
      }
    }
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    width: 880px;
    .container {
      .header-container {
        width: 100%;
        height: 300px;
      }
    }
  }
}
.app-container {
  .container {
    width: 100%;
    .header-container {
      border-radius: 10px;
      overflow: hidden;
      margin-bottom: 20px;
      position: relative;
      .describe {
        position: absolute;
        text-align: center;
        color: #fff;
        top: 0;
        width: 100%;
        background-color: rgba(0, 0, 0, 0.2);
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        text-shadow: 0 0 5px #000;
        cursor: pointer;
        h4:hover {
          color: #409eff;
        }
        p {
          font-size: 13px;
        }
      }
    }
    .filter-container {
      width: 100%;
      margin-bottom: 20px;
      .filter-classification {
        display: flex;
        justify-content: flex-start;
        align-items: flex-start;
        width: 100%;
        margin-top: 10px;
        .filter-header {
          color: #777;
          font-size: 14px;
          cursor: pointer;
          width: 75px;
          .icon-class {
            width: 25px;
          }
          .division {
            margin-left: 3px;
            margin-right: 3px;
          }
        }
        .filter-item {
          color: #999;
          font-size: 14px;
          text-align: left;
          width: calc(100% - 75px);
          display: flex;
          justify-content: flex-start;
          flex-wrap: wrap;
          span {
            margin-left: 5px;
            margin-right: 5px;
            cursor: pointer;
          }
          span:hover {
            color: #409eff;
          }
        }
        .actice-item {
          color: #409eff;
          font-weight: 600;
        }
      }
      .filter-status {
        width: 100%;
        display: flex;
        .status-item {
          color: #777;
          font-size: 14px;
          cursor: pointer;
          margin-right: 20px;
          .svg-icon {
            font-size: 18px;
          }
        }
        .excellent {
          font-weight: 600;
          color: #ffd90c;
        }
        .hot {
          font-weight: 600;
          color: #f56c6c;
        }
        .new {
          font-weight: 600;
          color: #409eff;
        }
      }
    }
    .article-container {
      width: 100%;
      margin-bottom: 20px;
      .article-data {
        width: 100%;
        display: flex;
        justify-content: flex-start;
        flex-wrap: wrap;

        @media only screen and (max-device-width: 750px) {
          .article-item {
            width: 100%;
          }
        }
        @media only screen and (min-device-width: 750px) {
          .article-item {
            width: 260px;
          }
        }
        .article-item {
          padding: 10px;
          cursor: pointer;
          display: flex;
          justify-content: center;
          flex-wrap: wrap;
          background: var(--materialCardBackground);
          border-radius: 5px;
          overflow: hidden;
          transition: 0.5s;
          margin: 0px 5px 10px 5px;
          border: 1px solid var(--materialCardBackground);

          .article-img {
            width: 100%;
            height: 150px;
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
              background-color: #f0f2f5dd;
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
            .time {
              display: flex;
              justify-content: flex-start;
              align-items: center;
              .avatar {
                width: 15px;
                height: 15px;
                overflow: hidden;
                border-radius: 50%;
              }
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
      .no-data {
        width: 100%;
        height: 200px;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 14px;
        color: #777;
        span:before,
        span:after {
          content: "";
          width: 200px;
          height: 1px;
          background: #777;
          display: block; /*1.首先使伪类显示方式为块级元素*/
          position: relative; /*2.通过相对定位的方式控制两侧内容的位置*/
        }
        span:before {
          /*3.控制左侧横线的位置*/
          top: 11px;
          left: 180px;
        }
        /*4.控制右侧横线的位置*/
        span:after {
          top: -10px;
          right: 180px;
        }
      }
      .load-more {
        width: 100%;
      }
    }
  }
}
</style>