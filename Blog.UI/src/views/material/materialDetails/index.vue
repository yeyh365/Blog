<!--
 * @Descripttion: 资源详情
 * @Author: Yerik
 * @Date: 2021-10-14 22:26:19
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-01 21:04:19
-->
<template>
  <div class='app-container'>
    <div class='container'>
      <!-- 资源数据 -->
      <el-card
        class="box-card"
        v-if="haveDate"
      >
        <div
          slot="header"
          class="clearfix"
        >
          <span class="recommend-title">资源详情</span>
        </div>
        <div class='material-container'>
          <div class='material-item'>
            <div class='img'>
              <img
                :src="$utils.imgUrl(materialData.MaterialCover)"
                alt="LOGO"
                height="80px"
              />
            </div>
          </div>
          <div class='material-item'>
            <div class='name'>
              {{materialData.MaterialName}}
            </div>
          </div>
          <div class='material-item'>
            <div class="browse">
              <el-tooltip
                class="item"
                effect="dark"
                content="浏览量"
                placement="top"
              >
                <el-tag
                  type="info"
                  size="small"
                  effect="dark"
                  class='tag'
                ><i class="el-icon-view icon"></i><span>{{materialData.BrowseNum}}</span></el-tag>
              </el-tooltip>
              <el-tooltip
                class="item"
                effect="dark"
                content="收藏"
                placement="top"
              >
                <el-tag
                  type="danger"
                  size="small"
                  effect="dark"
                  class='tag'
                  @click="addMaterialLinkNum()"
                ><i
                    class='el-icon-star-on icon'
                    v-if="isCollection"
                  ></i><i
                    class="el-icon-star-off icon"
                    v-else
                  ></i><span>{{materialData.LikeNum}}</span></el-tag>
              </el-tooltip>
              <el-tag
                size="small"
                effect="dark"
                class='tag'
                v-clipboard:copy="conyContainer"
                v-clipboard:success="onCopy"
              ><i class="el-icon-share icon"></i><span>分享</span></el-tag>
            </div>
          </div>
          <div class='material-item'>
            <div class='describe'>{{materialData.MaterialDescribe}}</div>
          </div>
          <div class='material-item'>
            <div class='type'>
              <el-tag
                v-for="(item,index) in materialData.Keywords"
                size='small'
                effect="plain"
                class="type-item"
              >{{item.TypeName}}</el-tag>
            </div>
          </div>
          <el-divider></el-divider>
          <div class='material-other'>
            <div class='material-link'>
              <p>链接：<span>
                  <el-link
                    :href="materialData.MaterialLink"
                    target="_blank"
                    type="primary"
                    style="margin-right:5px"
                  >{{materialData.MaterialLink}}</el-link>
                </span>
                <el-tooltip
                  class="item"
                  effect="dark"
                  :content="copyLinkIcon?'已复制':'复制'"
                  placement="top"
                ><span
                    style="cursor: pointer;"
                    v-clipboard:copy="conyContainer"
                    v-clipboard:success="copyLink"
                    v-if='!copyLinkIcon'
                  ><i
                      class="el-icon-document-copy"
                      style="color:#409EFF"
                    ></i></span>
                  <span
                    style="cursor: pointer;"
                    v-else
                  ><i
                      class="el-icon-check"
                      style="color:#67C23A"
                    ></i></span>
                </el-tooltip>
              </p>
            </div>
            <div class='material-link'>
              <p>详情：<span>{{materialData.MaterialDetails}}</span></p>
            </div>
            <div class='material-link'>
              <p>时间：<span>{{materialData.Created}}</span></p>
            </div>
            <div class='material-link'>
              <div class='recommender'>
                <div>推荐人：</div>
                <div
                  class='user-info'
                  @click="toUserInfo(materialData.User[0])"
                >
                  <div class='user-avatar'><img
                      :src="$utils.imgUrl(materialData.User[0].Photo)"
                      alt="avatar"
                      style="width:100%;"
                    ></div>
                  <div>
                    <span>{{materialData.User[0].Name}}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </el-card>
      <el-card
        class="box-card"
        v-else
      >
        <div
          slot="header"
          class="clearfix"
        >
          <span class="recommend-title">没有发现资源</span>
        </div>
        <div class='material-container'>
          <img
            width="100%"
            :src="emptyDataImg"
            alt=""
          >
          <el-button
            type="primary"
            round
            size="small"
            @click="toHome()"
          >跟我去发现更多资源吧！</el-button>
        </div>
      </el-card>
    </div>
  </div>
</template>

<script>
import {
  getMaterialDetails,
  addMaterialBrowse,
  addMaterialLike,
  hasCollection,
} from "@/api/material/materialRecommend";
import MaterialService from '@/api/services/MaterialService'
import ArticleService from '@/api/services/ArticleService'
export default {
  name: "MaterialDetails",
  data() {
    return {
      //资源ID
      materialId: 0,

      //资源数据
      materialData: {
        userInfo: {},
      },

      //复制链接图标
      copyLinkIcon: false,

      //计时器
      timer: null,

      //判断用户是否关注
      isCollection: false,

      // 判断是否有数据
      haveDate: false,

      // 没有数据图片
      emptyDataImg: require("@/assets/notData/notfind.png"),
      imgUrl:process.env.VUE_APP_BASE_URL,
            Interaction:{
        TypeName:'',
        ArticleId:0,
        UserId:1,
        Status:null,
        AttentionUserId:0
      }
    };
  },
  created() {
    this.materialId = this.$route.query.Id;
    this.init();
  },
  methods: {
    //数据初始化
    init() {
      console.log(this.imgUrl)
      this.isCollection = false;
       MaterialService.GetMaterialInfo(this.materialId).then((res) => {
        console.log(res.Data)
                 if (res.Data) {
          this.materialData = Object.assign({}, res.Data);
          this.Interaction.ArticleId=this.materialData.Id
          this.haveDate = true;
          this.timer = setTimeout(() => {
                  this.Interaction.TypeName='BrowseMaterial'
                  this.Interaction.Status=true
      console.log(this.Interaction)
        ArticleService.CreateInteraction(this.Interaction).then((res) => {
        if (res.Code == 200) {
          this.materialData.BrowseNum++;
        }
      })
          }, 5000);
        } else {
          this.haveDate = false;
        }
                if (this.haveDate) {
          if (this.$store.getters.userInfo) {
            const query = {
              materialId: this.materialId,
              userId: this.$store.getters.UserId,
            };
            // hasCollection(query).then((res) => {
            //   this.isCollection = res.data;
            // });
          }
        }
      })
      // getMaterialDetails({ id: this.materialId }).then((res) => {
      //   if (res.data) {
      //     this.materialData = Object.assign({}, res.data);
      //     this.haveDate = true;
      //     this.timer = setTimeout(() => {
      //       addMaterialBrowse({ id: res.data.id });
      //     }, 3000);
      //   } else {
      //     this.haveDate = false;
      //   }
      //   if (this.haveDate) {
      //     if (this.$store.getters.userInfo) {
      //       const query = {
      //         materialId: this.materialId,
      //         userId: this.$store.getters.userInfo.user.id,
      //       };
      //       hasCollection(query).then((res) => {
      //         this.isCollection = res.data;
      //       });
      //     }
      //   }
      // });

    },

    //复制
    onCopy() {
      this.$message({
        message: "内容已成功复制到剪切板！",
        type: "success",
      });
    },

    //去用户中心，访客的
    toUserInfo(item) {
      const USERID = item.id;
      this.$store.commit("SET_VISITOR_ID", USERID);
      const VISITORID = this.$store.getters.visitorId;
      this.$router.push({
        path: `/userInfo/${VISITORID}/releaseList`,
        query: {
          activeArticleType: 1,
        },
      });
    },

    //复制链接
    copyLink() {
      this.copyLinkIcon = true;
      this.onCopy();
      setTimeout(() => {
        this.copyLinkIcon = false;
      }, 3000);
    },

    //增加收藏
    addMaterialLinkNum() {
        this.Interaction.TypeName='LikeMaterial'
         this.Interaction.Status=!this.materialData.IsLike
         this.Interaction.ArticleId=this.materialData.Id
        console.log(this.Interaction)
        ArticleService.UpdateInteraction(this.Interaction).then((res) => {
         if (res.Code == 200) {
           if (this.materialData.LikeNum) {
             this.materialData.LikeNum--;
           } else {
             this.materialData.LikeNum++;
           }
           this.materialData.IsLike = !this.materialData.IsLike;
         }
      })
      // const query = {
      //   materialId: this.materialId,
      //   userId:
      //     this.$store.getters.userInfo && this.$store.getters.userInfo.user.id,
      // };
      // addMaterialLike(query).then((res) => {
      //   if (this.isCollection) {
      //     this.materialData.like_num--;
      //   } else {
      //     this.materialData.like_num++;
      //   }
      //   this.isCollection = !this.isCollection;
      // });
    },

    // 回到首页
    toHome() {
      this.$router.push({
        path: "/home",
      });
    },
  },
  computed: {
    //剪切板内容
    conyContainer() {
      const text = `我在Yerik发现了『 ${this.materialData.material_name} 』,快来看看${this.materialData.material_link}`;
      return text;
    },
  },
};
</script>
<style lang="scss" scoped>
@import "@/style/mixin.scss";

@media only screen and (max-device-width: 750px) {
  .app-container {
    width: 100%;
    margin: 0 10px 0 10px;
    width: calc(100% - 20px);
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    width: 100%;
  }
}
.app-container {
  .container {
    width: 100%;
    .clearfix {
      text-align: left;
      @include title-color-scroll-style;
      color: var(--pageTitle);
      .recommend-title {
        font-weight: 800;
      }
    }
    .material-container {
      width: 100%;
      min-height: 400px;
      .material-item {
        width: 100%;
        display: flex;
        justify-content: center;
        .img {
          width: 100%;
          cursor: pointer;
        }
        .name {
          color: var(--materialCardText);
          font-weight: 500;
          font-size: 24px;
          letter-spacing: 1px;
          margin-bottom: 10px;
        }
        .browse {
          width: 100%;
          display: flex;
          justify-content: center;
          margin-bottom: 20px;
          .tag {
            cursor: pointer;
            margin-right: 5px;
            .icon {
              margin-right: 5px;
            }
          }
        }
        .type-item {
          margin-right: 5px;
          color: #888;
          border-color: transparent;
          background-color: rgba(136, 136, 136, 0.1);
        }
        .describe {
          text-align: center;
          color: var(--materialCardContent);
          font-size: 14px;
          margin-bottom: 20px;
        }
      }
      .material-other {
        width: 100%;
        text-align: left;
        font-size: 14px;
        color: var(--materialCardContent);
        .material-link {
          margin-top: 20px;
          .recommender {
            display: flex;
            align-items: center;
            width: 100%;
            .user-info {
              display: flex;
              align-items: center;
              cursor: pointer;
              .user-avatar {
                width: 32px;
                height: 32px;
                border-radius: 50%;
                overflow: hidden;
                margin-right: 5px;
              }
            }
          }
        }
      }
    }
  }
}
// 修改el-card背景色
::v-deep .el-card {
  background-color: var(--pageBackground);
  border-color: var(--materialCardBackground);
}
</style>
