<!--
 * @Descripttion: 关注
 * @Author: Yerik
 * @Date: 2021-09-12 20:23:27
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 22:09:58
-->
<template>
  <div class="app-container">
    <div class="container">
      <!-- 关注 粉丝切换 -->
      <div class="follow-header">
        <div
          class="follow-item"
          :class="{ 'active-item': isFollow }"
          @click="isFollow = !isFollow"
        >
          关注 <span>{{ followNum }}</span>
        </div>
        <div class="follow-division"></div>
        <div
          class="follow-item"
          :class="{ 'active-item': !isFollow }"
          @click="isFollow = !isFollow"
        >
          粉丝 <span>{{ fansNum }}</span>
        </div>
      </div>
      <!-- 关注 粉丝数据内容 -->
      <!-- 关注 -->
      <div
        class="follow-container"
        v-if="isFollow"
      >
        <div
          v-if="followUserList.length > 0"
          class='follow'
        >
          <div
            class="follow-item"
            v-for="(item, index) in followUserList"
            :key="index"
          >
            <div class="follow-background">
              <img
                :src="$utils.imgUrl(item.getFollowInfo.avatar_url)"
                width="100%"
                alt=""
              />
            </div>
            <div class="user-info">
              <div class="user-avatar">
                <img
                  :src="$utils.imgUrl(item.getFollowInfo.avatar_url)"
                  width="100%"
                  height="100%"
                  alt=""
                  @click="toUserInfo(item,'isFollow')"
                />
              </div>
              <div class="user-name">
                <h4 class="nickname">
                  <span >{{ item.getFollowInfo.nickname }}</span>

                  <span
                    class="is-follow"
                    @click="followUser(item)"
                    v-if="item.isFollow"
                  ><i class="el-icon-error"></i>取消关注</span>
                  <span
                    class="not-follow"
                    @click="followUser(item)"
                    v-else
                  ><i class="el-icon-success"></i>点击关注</span>
                </h4>
                <p class="user-autograph">
                  <span v-if="item.getFollowInfo.autograph">{{
                    item.getFollowInfo.autograph
                  }}</span><span v-else>这个人很懒，什么都没有留下</span>
                </p>
              </div>
            </div>
          </div>
        </div>
        <div
          v-else
          class="empty-data"
        >
          <div>
            <img
              :src="emptyFollows"
              alt=""
            />
            <p>暂无关注</p>
          </div>
        </div>
      </div>
      <!-- 粉丝 -->
      <div
        class="follow-container"
        v-else
      >
        <div
          v-if="fansUserList.length > 0"
          class='follow'
        >

          <div
            class="follow-item"
            v-for="(item, index) in fansUserList"
            :key="index"
          >
            <div class="follow-background">
              <img
                :src="$utils.imgUrl(item.getFansInfo.avatar_url)"
                width="100%"
                alt=""
              />
            </div>
            <div class="user-info">
              <div class="user-avatar">
                <img
                  :src="$utils.imgUrl(item.getFansInfo.avatar_url)"
                  width="100%"
                  height="100%"
                  alt=""
                  @click="toUserInfo(item,'isFans')"
                />
              </div>
              <div class="user-name">
                <h4 class="nickname">
                  <span>{{ item.getFansInfo.nickname }}</span>

                  <span
                    class="is-follow"
                    @click="followUser(item)"
                    v-if="item.isFollow"
                  ><i class="el-icon-error"></i>取消关注</span>
                  <span
                    class="not-follow"
                    @click="followUser(item)"
                    v-else
                  ><i class="el-icon-success"></i>点击关注</span>
                </h4>
                <p class="user-autograph">
                  <span v-if="item.getFansInfo.autograph">{{
                    item.getFansInfo.autograph
                  }}</span><span v-else>这个人很懒，什么都没有留下</span>
                </p>
              </div>
            </div>
          </div>
        </div>
        <div
          v-else
          class="empty-data"
        >
          <div>
            <img
              :src="emptyFans"
              alt=""
            />
            <p>暂无粉丝</p>
          </div>
        </div>
      </div>
      <!-- 加载更多 移动端 -->
      <div
        v-if="$utils.isMobile()"
        class="get-more-container"
      >
        <el-button
          v-if="isFollow"
          :type="showFollowsGetMoreBtn?'primary':'info'"
          round
          size="mini"
          @click="getMore()"
          :loading='loading'
          :disabled='!showFollowsGetMoreBtn'
        ><span v-if='showFollowsGetMoreBtn'>加载更多</span><span v-else>没有更多了</span></el-button>
        <el-button
          v-else
          :type="showFansGetMoreBtn?'primary':'info'"
          round
          size="mini"
          @click="getMore()"
          :loading='loading'
          :disabled='!showFansGetMoreBtn'
        ><span v-if='showFansGetMoreBtn'>加载更多</span><span v-else>没有更多了</span></el-button>
      </div>
      <!-- 分页 pc端-->
      <div
        class='pags-container'
        v-if="(!isFollow&&fansUserList.length>0)||(isFollow&&followUserList.length>0)"
      >
        <el-pagination
          v-if="!$utils.isMobile()"
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
</template>

<script>
import {
  blogUserFollowUser,
  getUserFans,
  getUserFollow,
} from "@/api/user/followUser";
export default {
  name: "Follow",
  data() {
    return {
      //判断当前是用户关注还是粉丝
      isFollow: true,

      //关注用户列表
      followUserList: [],

      //关注用户数量
      followNum: 0,

      //粉丝用户列表
      fansUserList: [],

      //粉丝用户数量
      fansNum: 0,

      //没有粉丝图片
      emptyFans: require("@/assets/notData/notFans.png"),

      //没有关注图片
      emptyFollows: require("@/assets/notData/notFollows.png"),

      //分页
      filterForm: {
        list_rows: 6,
        page: 1,
        total: 0,
      },

      // 获取更多按钮加载状态
      loading: false,

      // 是否显示加载更多按钮

      showFansGetMoreBtn: true,
      showFollowsGetMoreBtn: true,
    };
  },
  created() {
    this.isFollow = this.$route.query.isFollow || false;
    this.init();
  },
  methods: {
    init(type = true, isFollow = true, isFans = true) {
      const query = {
        ...this.filterForm,
      };

      if (isFollow) {
        this.getFollow(query, type);
      }

      if (isFans) {
        this.getFans(query, type);
      }
    },
    getFollow(query, type) {
      getUserFollow(query).then((res) => {
        res.data.data.forEach((item) => {
          item.isFollow = true;
        });

        if (type) {
          this.followUserList = Object.assign([], res.data.data);
        } else {
          this.followUserList = this.followUserList.concat(res.data.data);
          // 控制获取更改按钮显示
          this.loading = false;
        }
        this.filterForm.total = res.data.total;
        this.followNum = res.data.total;

        if (
          this.followNum <=
          this.filterForm.list_rows * this.filterForm.page
        ) {
          this.showFollowsGetMoreBtn = false;
        } else {
          this.showFollowsGetMoreBtn = true;
        }
      });
    },
    getFans(query, type) {
      getUserFans(query).then((res) => {
        if (type) {
          this.fansUserList = Object.assign([], res.data.data);
        } else {
          this.fansUserList = this.fansUserList.concat(res.data.data);
          // 控制获取更改按钮显示
          this.loading = false;
        }

        this.filterForm.total = res.data.total;
        this.fansNum = res.data.total;
        if (this.fansNum <= this.filterForm.list_rows * this.filterForm.page) {
          this.showFansGetMoreBtn = false;
        } else {
          this.showFansGetMoreBtn = true;
        }
      });
    },

    //关注或取消用户
    followUser(item) {
      let follow_id = "";
      let userName = "";
      if (item.getFollowInfo) {
        follow_id = item.getFollowInfo.id;
        userName = item.getFollowInfo.nickname;
      } else {
        follow_id = item.getFansInfo.id;
        userName = item.getFansInfo.nickname;
      }
      blogUserFollowUser({ follow_id: follow_id }).then((res) => {
        if (res.code != 200) {
          return;
        }
        item.isFollow = !item.isFollow;
        if (!item.isFollow) {
          this.$notify({
            title: "成功",
            message: `你已成功取消关注${userName}`,
            type: "warning",
          });
        } else {
          this.$notify({
            title: "成功",
            message: `你已成功关注${userName}`,
            type: "success",
          });
        }
      });
    },

    //去用户页面
    toUserInfo(item, status) {
      let userId = undefined;
      if (status == "isFans") {
        userId = item.user_id;
      } else {
        userId = item.follow_id;
      }
      this.$store.commit("SET_VISITOR_ID", userId);
      const VISITORID = this.$store.getters.visitorId;
      this.$router.push({
        path: `/userInfo/${VISITORID}/releaseList`,
        query: {
          activeArticleType: 1,
        },
      });
    },

    //分页切换
    currentChange(page) {
      this.filterForm.page = page;
      if (this.isFollow) {
        this.getFollow();
      } else {
        this.getFans();
      }
    },

    // 获取更多
    getMore() {
      this.loading = true;
      this.filterForm.page++;
      this.init(false, this.isFollow, !this.isFollow);
    },
  },
  watch: {
    isFollow(value) {
      this.filterForm = {
        list_rows: 6,
        page: 1,
        total: 0,
      };
    },
  },
};
</script>

<style lang="scss" scoped>
.app-container {
  width: calc(100% - 10px);
  height: 100%;
  background: var(--pageBackground);
  .container {
    width: 100%;
    height: 100%;
    padding: 10px;
    .follow-header {
      width: calc(100% - 20px);
      height: 40px;
      display: flex;
      justify-content: center;
      .follow-item {
        font-size: 14px;
        color: #b4b6bb;
        cursor: pointer;
      }
      .active-item {
        color: #3390ff;
        font-weight: 900;
      }
      .follow-division {
        width: 1px;
        height: 20px;
        background: #b4b6bb;
        border-radius: 20px;
        margin-left: 10px;
        margin-right: 10px;
      }
    }
    .follow-container {
      width: 100%;
      min-height: 380px;
      display: flex;
      justify-content: center;
      @media only screen and (max-device-width: 750px) {
        .follow {
          display: block;
          .follow-item {
            width: calc(100% - 20px);
          }
        }
      }
      @media only screen and (min-device-width: 750px) {
        .follow {
          display: flex;
          .follow-item {
            width: 250px;
          }
        }
      }
      .follow {
        width: 100%;
        justify-content: flex-start;
        flex-wrap: wrap;
        .follow-item {
          height: 65px;
          padding: 5px;
          border-radius: 5px;
          margin: 5px;
          overflow: hidden;
          cursor: pointer;
          position: relative;
          border: 1px solid var(--pageBorder);
          .follow-background {
            opacity: 0.4;
            filter: blur(1px);
            position: absolute;
            width: 100%;
            transform: translateY(-20%);
            top: -50%;
            right: 0;
          }
          .user-info {
            position: absolute;
            top: 0;
            display: flex;
            justify-content: flex-start;
            align-items: center;
            padding: 5px;
            width: calc(100% - 10px);
            height: calc(100% - 10px);
            .user-avatar {
              width: 50px;
              height: 50px;
              border-radius: 50%;
              overflow: hidden;
              margin-right: 10px;
            }
            .user-name {
              text-align: left;
              .nickname{
                color: var(--materialCardText);
              }
              .user-autograph {
                width: 150px;
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
                color: #f2f6fc;
                font-size: 12px;
                margin: 5px;
              }
              .is-follow {
                margin-left: 5px;
                color: #3390ff;
              }
              .not-follow {
                margin-left: 5px;
                color: #67c23a;
              }
            }
          }
        }
      }

      .empty-data {
        width: 100%;
        display: flex;
        justify-content: center;
        text-align: center;
        color: #b1b1b1;
        font-size: 12px;
        img {
          width: 200px;
          display: block;
        }
      }
    }
    .pags-container {
      width: 100%;
      display: flex;
      justify-content: flex-end;
      margin-top: 20px;
    }
  }
}
</style>