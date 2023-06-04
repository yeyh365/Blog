<!--
 * @Descripttion: 用户信息页
 * @Author: Yerik
 * @Date: 2021-07-02 21:01:17
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 22:12:49
-->
<template>
  <div class="app-container">
    <div class="container">
      <!-- 移动端 头部导航栏 -->
      <div
        class="top-link"
        v-if="$utils.isMobile()"
      >

        <router-link
          active-class="link-active"
          tag="div"
          :to="{ name: 'BaseSetting' }"
        >
          <div
            class="link-item"
            style="border-top-left-radius: 5px;border-bottom-left-radius: 5px;"
          ><i class='el-icon-s-tools'></i> <span>基本配置</span>
          </div>
        </router-link>

        <router-link
          active-class="link-active"
          tag="div"
          :to="{ name: 'AddressSetting' }"
        >
          <div class="link-item"><i class='el-icon-shopping-cart-2'></i> <span>收货地址</span>
          </div>
        </router-link>
        <router-link
          active-class="link-active"
          tag="div"
          :to="{ name: 'SecuritySetting' }"
        >
          <div
            class="link-item"
            style="border-top-right-radius: 5px;border-bottom-right-radius: 5px;"
          ><i class='el-icon-warning-outline'></i> <span>安全配置</span>
          </div>
        </router-link>

      </div>
      <!--左侧导航栏 -->
      <div
        class="left-link"
        v-if="!$utils.isMobile()"
      >
        <div class="link-item">
          <router-link
            tag="div"
            :to="{ name: 'BaseSetting' }"
          ><span>基本配置</span>
          </router-link>
        </div>
        <div class="link-item">
          <router-link
            tag="div"
            :to="{ name: 'AddressSetting' }"
          ><span>收货地址</span>
          </router-link>
        </div>

        <div class="link-item">
          <router-link
            tag="div"
            :to="{ name: 'SecuritySetting' }"
          ><span>安全配置</span></router-link>
        </div>
      </div>
      <!-- 右侧展示信息 -->
      <div class="right-container">
        <transition
          mode="out-in"
          enter-active-class="animated fadeIn"
          leave-active-class="animated fadeOut"
        >
          <keep-alive>
            <router-view :key="key" />
          </keep-alive>
        </transition>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "Persoal",
  data() {
    return {};
  },
  created() {},
  computed: {
    key() {
      return this.$route.path;
    },
  },
};
</script>
<style lang="scss" scoped>
.app-container {
  width: 100%;
  height: 100%;
  
  .container {
    height: 100%;
    width: 100%;
    display: flex;
    justify-content: flex-start;
    flex-wrap: wrap;
    .top-link {
      width: 100%;
      display: flex;
      justify-content: flex-end;
      margin-bottom: 10px;
      padding-right: 10px;
      color: var(--materialCardText);

      .link-item {
        width: 100px;
        height: 35px;
        background: var(--materialCardBackground);
        line-height: 35px;
        font-size: 14px;
      }

      .link-active .link-item {
        background: #409eff;
        color: #fff;
      }
    }

    .left-link {
      width: 200px;
      min-height: 200px;
      border-right: 1px solid var(--pageBorder);
      background: var(--materialCardBackground);

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
    @media only screen and (max-device-width: 750px) {
      .right-container {
        width: 100%;
        // width: calc(100% - 201px);
      }
    }
    @media only screen and (min-device-width: 750px) {
      .right-container {
        // width: 100%;
        width: calc(100% - 201px);
      }
    }
  }
}
</style>