<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-10-27 20:03:41
 * @LastEditors: Yerik
 * @LastEditTime: 2023-04-21 16:19:10
-->
<template>
  <div class='app-container'>
    <div class='conatiner'>
      <!-- tips内容 -->
      <div class='tips'>
        <div class='tips-title'>
          <span class="recommend-title">作品展示 </span>
        </div>
        <div
          ref='tips-close'
          class='tips-close'
        >
          <div
            class='tips-close-icon'
            @click="tipsShow=!tipsShow"
          ><i
              class="el-icon-close"
              v-if='tipsShow'
            ></i>
            <i
              class="el-icon-question"
              v-else
            ></i>
          </div>
        </div>
        <div
          ref='tips-container'
          class='tips-container animate__animated'
        >
          <p>本网站目前所有展示作品均由作者本人完成,暂时不允许网友个人上传，在一定程度上保证本网站的绿色、干净、安全。网络不是法外之地，本站建立的初衷是为了给更多的小白指路。如果你也想把你的作品分享给大家，并在这里展示，请联系作者！</p>
        </div>
      </div>
      <!-- 作品内容 -->
      <div class='achievements'>

        <div
          class='empty-data'
          v-if='achievementsList.length==0'
        >
          <div><img
              :src="emptyData"
              alt="没有数据"
              width="500px"
            ></div>
        </div>
        <div
          class='achievements-container'
          v-else
        >
          <el-card
            shadow="hover"
            v-for="(item,index) in achievementsList"
            :key='index'
            style="margin: 5px; "
          >
            <div
              class='achievements-item'
              @click="openAchievements(item)"
            >
              <div class='item-img'>
                <div><img
                    :src="$utils.imgUrl(item.img_url)"
                    alt=""
                  ></div>
              </div>
              <div class='item-title'>
                <h4>{{item.achievements_name}}</h4>
              </div>
              <div class='item-user'>
                <div>推荐人：</div>
                <div class='user-info'>
                  <div class='user-avatar'><img
                      :src="$utils.imgUrl(item.userInfo.avatar_url)"
                      alt="avatar"
                      style="width:100%;height:100px"
                    ></div>
                  <div>
                    <span>{{item.userInfo.nickname}}</span>
                  </div>
                </div>
              </div>
              <div class="item-describe">
                <p>{{item.describe}}</p>
              </div>
            </div>
          </el-card>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import { getAchievementsList } from "@/api/achievements/index";
import { getToken } from "@/utils/auth";
export default {
  name: "Achievements",
  data() {
    return {
      //作品列表
      achievementsList: [],

      //tips显示 关闭
      tipsShow: true,

      //emptyData
      emptyData: require("@/assets/notData/notfind.png"),
    };
  },
  created() {
    setTimeout(() => {
      // this.tipsShow = false;
    }, 20000);
    this.init();
  },
  methods: {
    init() {
      getAchievementsList().then((res) => {
        this.achievementsList = Object.assign([], res.data);
      });
    },

    //打开网页
    openAchievements(item) {
      window.open(item.achievements_url + "?token=" + getToken());
    },

    //显示tips
    showTipsFun() {
      this.$refs["tips-container"].style.display = "block";
      this.$refs["tips-container"].style.height = "auto";
      this.$refs["tips-close"].style.borderBottomRightRadius = "0";
      this.$refs["tips-close"].style.borderBottomLeftRadius = "0";
      this.$refs["tips-close"].style.height = "45px";
      this.$refs["tips-container"].classList.remove(
        "animate__rotateOutDownRight"
      );
      this.$refs["tips-container"].classList.add("animate__rotateInUpRight");
    },
    //关闭tips
    closeTipsFun() {
      this.$refs["tips-container"].classList.add("animate__rotateOutDownRight");
      this.$refs["tips-close"].style.borderBottomRightRadius = "10px";
      this.$refs["tips-close"].style.borderBottomLeftRadius = "10px";
      this.$refs["tips-close"].style.height = "40px";
      this.$refs["tips-container"].style.dispaly = "none";
      setTimeout(() => {
        this.$refs["tips-container"].style.height = "0px";
        this.$refs["tips-container"].style.display = "none";
      }, 500);
    },
  },

  watch: {
    tipsShow(value) {
      if (value) {
        this.showTipsFun();
      } else {
        this.closeTipsFun();
      }
    },
  },
};
</script>


<style lang="scss" scoped>
@import "@/style/mixin.scss";

.app-container {
  width: 100%;
  .conatiner {
    width: 100%;
    .tips {
      width: 100%;
      display: flex;
      justify-content: flex-start;
      flex-wrap: wrap;
      margin-bottom: 10px;
      .tips-title {
        width: calc(100% - 80px);
        height: 40px;

        line-height: 40px;
        text-align: left;
        background: var(--pageBackground);
        border-radius: 10px;
        overflow: hidden;
        margin-right: 10px;
        margin-bottom: 5px;
        padding-left: 20px;
        @include title-color-scroll-style;
        color: var(--pageTitle);

        .recommend-title {
          font-weight: 800;
        }
      }
      .tips-close {
        width: 50px;
        height: 45px;
        line-height: 40px;
        background: var(--pageBackground);
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
        overflow: hidden;
        transition: 0.3s;
        color: var(--pageTitle);
        .tips-close-icon {
          cursor: pointer;
        }
      }
      .tips-container {
        width: calc(100% - 20px);
        background: var(--pageBackground);
        border-radius: 10px;
        border-top-right-radius: 0;
        overflow: hidden;
        text-indent: 2em;
        padding: 10px;
        text-align: left;
        font-size: 14px;
        line-height: 20px;
        transition: 0.3s;
        color: var(--materialCardContent);
      }
    }
    .achievements {
      transition: 1s;
      width: 100%;
      .achievements-container {
        width: 100%;
        display: flex;
        justify-content: flex-start;
        flex-wrap: wrap;
        @media only screen and (max-device-width: 750px) {
          .achievements-item {
            width: 100%;
          }
        }
        @media only screen and (min-device-width: 750px) {
          .achievements-item {
            width: 250px;
          }
        }
        .achievements-item {
          border-radius: 10px;
          overflow: hidden;
          display: flex;
          justify-content: center;
          flex-wrap: wrap;
          text-align: left;
          cursor: pointer;

          &:hover {
            .item-title {
              color: #409eff;
            }
          }
          .item-img {
          
            width: 100%;
            display: flex;
            justify-content: center;
            img{
              width: 100%;
              border-radius: 10px;
            }
          }
          .item-title {
            width: 100%;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            margin-bottom: 10px;
            color: var(--materialCardText);
          }
          .item-user {
            width: 100%;
            font-size: 14px;
            display: flex;
            align-items: center;
            color: var(--materialCardContent);
            .user-info {
              margin-bottom: 10px;
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
          .item-describe {
            color: var(--materialCardContent);
            width: 100%;
            font-size: 12px;
            overflow: hidden;
            text-overflow: ellipsis;
            display: -webkit-box;
            -webkit-line-clamp: 3; //行数
            -webkit-box-orient: vertical;
          }
        }
      }
    }
    .empty-data {
      width: 100%;
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 400px;
    }
  }
}
// 修改el-card背景色
::v-deep .el-card {
  background-color: var(--materialCardBackground);
  border-color: var(--materialCardBackground);
}
</style>