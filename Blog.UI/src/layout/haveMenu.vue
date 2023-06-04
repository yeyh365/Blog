<!--
 * @Descripttion: 当前layout只存在菜单
 * @Author: Yerik
 * @Date: 2021-06-10 19:37:28
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-04 19:57:03
-->
<template>
  <div class="app-contianer">
    <div class="contianer">
      <!-- 头部 -->
      <div class="contianer-header">
        <div class="header">

          <div>
            <Logo></Logo>
          </div>
          <div class="header-right">
            <Menu class="header-right-pc"></Menu>
            <Theme class="header-right-pc"></Theme>
            <Login class="header-right-pc"></Login>
            <MenuOfPhone class="header-right-phone"></MenuOfPhone>
          </div>

        </div>
      </div>
      <!-- 显示内容 -->
      <div class="contianer-main">
        <Main></Main>

      </div>
      <div class="container-footer">
        <Footer></Footer>
      </div>
    </div>

  </div>
</template>
<script>
import Logo from "./components/logo.vue"; //引入logo
import Menu from "./components/menu.vue"; //引入菜单
import Main from "./components/main.vue"; //引入展示框
import Login from "./components/login.vue"; //引入登录组件
import Footer from "./components/footer.vue"; //引入版权
import Theme from "./components/theme.vue"; //引入切换主题组件
import MenuOfPhone from "./components/menuOfPhone.vue"; //引入移动端菜单
export default {
  name: "LayoutMenu",
  components: {
    Logo,
    Menu,
    Main,
    Footer,
    Login,
    MenuOfPhone,
    Theme,
  },
};
</script>
<style lang="scss" scoped>
@import "@/style/default.scss";
.app-contianer {
  width: 100%;
  display: flex;
  justify-content: center;
  background: var(--mainBackground);
  transition: 1s;

  .contianer {
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;

    .contianer-header {
      padding: 0, 10px, 10px, 10px;
      width: 100%;
      height: 60px;
      display: flex;
      justify-content: space-around;
      margin-bottom: 20px;
      position: fixed;
      top: 0;
      z-index: 1000;
      background: var(--headBackground);
      box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
      transition: 1s;
      .header {
        padding: 10px;
        max-width: 1380px;
        width: calc(100% - 20px);
        display: flex;
        justify-content: space-between;
        align-items: center;
        z-index: 1000;
        filter: (2px);
        // backdrop-filter: saturate(180%) blur(20px);
        // background-color: rgba(41, 42, 45, 0.82);
        .header-right {
          display: flex;
          justify-content: end;
        }

        @media only screen and (max-device-width: 750px) {
          .header-right-pc {
            display: none;
          }
          .header-right-phone {
            display: flex;
          }
        }
        @media only screen and (min-device-width: 750px) {
          .header-right-pc {
            display: flex;
          }
          .header-right-phone {
            display: none;
          }
        }
      }
    }
    .contianer-main {
      margin-top: 80px;
      max-width: 1200px;
      width: 100%;
      min-height: calc(100vh - 60px);
      display: flex;
    }
    .container-footer {
      z-index: 0;
      width: 100%;
      background: #22292d;
    }
  }
}

// 全局改变el-input背景色
::v-deep .el-input__inner {
  background: var(--elInputBackground);
  border-color: var(--elInputBorder);
  color: var(--materialCardText);
}
::v-deep .el-input__count-inner {
  background: var(--elInputBackground) !important;
  color: var(--materialCardText);
}

::v-deep .el-input-group__append {
  background: var(--elInputBackground) !important;
  color: var(--materialCardText);
  border-color: var(--elInputBorder);
}

::v-deep .el-textarea__inner {
  background: var(--elInputBackground);
  border-color: var(--elInputBorder);
  color: var(--materialCardText);
}
::v-deep .el-textarea .el-input__count {
  background: var(--elInputBackground) !important;
  color: var(--materialCardText);
}

// 上传组件
::v-deep .el-upload-dragger {
  background: var(--elInputBackground) !important;
  color: var(--materialCardText);
}
</style>
