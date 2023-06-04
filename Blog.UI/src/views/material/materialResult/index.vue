<!--
 * @Descripttion: 推荐结果
 * @Author: Yerik
 * @Date: 2021-10-14 16:08:17
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 00:04:29
-->
<template>
  <div class='app-container'>
    <div class='container'>
      <div class='recommend-result'>
        <el-card class="box-card">
          <div
            slot="header"
            class="clearfix"
          >
            <span class='recommend-title'>推荐结果</span>
          </div>
          <el-result icon="success">
            <template slot='title'>
              <div class="title"> 提交成功</div>
            </template>
            <template slot='subTitle'>
              <div class="subTitle">感谢您的推荐，我们将尽快审核！</div>
            </template>
            <template slot="extra">
              <el-button
                type="primary"
                size="small"
                @click="continueRecommend"
              >继续推荐</el-button>
              <el-button
                size="small"
                @click="toUserInfo"
              >返回个人中心</el-button>
            </template>
          </el-result>
        </el-card>
      </div>
      <div class='recommend-item'>
        <el-card
          class="box-card"
          shadow="hover"
        >
          <div
            slot="header"
            class="clearfix"
          >
            <span class="title">推荐资源</span>
          </div>
          <MaertrialItem :materialData='materialData' />
        </el-card>
      </div>
    </div>
  </div>
</template>

<script>
import MaertrialItem from "@/components/materialItem/index.vue";
import { getMaterialById } from "@/api/material/materialRecommend";
export default {
  name: "MaterialResult",
  components: {
    MaertrialItem,
  },
  data() {
    return {
      //资源数据
      materialData: {},

      //资源ID
      materialId: 0,
    };
  },
  created() {
    this.materialId = this.$route.query.id || 40;
    this.init();
  },
  methods: {
    //数据初始化
    init() {
      getMaterialById({ id: this.materialId }).then((res) => {
        this.materialData = res.data;
      });
    },

    //继续推荐
    continueRecommend() {
      this.$router.push({
        path: "/materialRecommend",
      });
    },

    //返回个人中心
    toUserInfo() {
      const USERID = this.$store.getters.userId;
      this.$store.commit("SET_VISITOR_ID", USERID);
      const VISITORID = this.$store.getters.visitorId;
      this.$router.push({
        path: `/userInfo/${VISITORID}/material`,
        query: {
          activeArticleType: 1,
        },
      });
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
      width: 100%;
    }
  }
}
.app-container {
  width: 100%;
  .container {
    .recommend-result {
      width: 100%;
      margin-bottom: 20px;
      text-align: left;
      @include title-color-scroll-style;
      color: var(--materialCardText);
      .recommend-title {
        font-weight: 800;
      }
      .title {
        margin-top: 20px;
        font-size: 20px;
        color: var(--materialCardText);
        line-height: 1.3;
      }
      .subTitle {
        margin-top: 10px;
        font-size: 14px;
        color: var(--materialCardContent);
        line-height: 1.3;
      }
    }
    .recommend-item {
      width: 100%;
      display: flex;
      justify-content: center;
      .box-card {
        width: 400px;
        .clearfix {
          text-align: left;
        }
        .title {
          font-size: 16px;
          color: var(--materialCardText);
          font-weight: 600;
        }
      }
    }
  }
}
// 修改el-card背景色
::v-deep .el-card {
  background-color: var(--materialCardBackground);
  border-color: var(--materialCardBackground);
}
</style>
