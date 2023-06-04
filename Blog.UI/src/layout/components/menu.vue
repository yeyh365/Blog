<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-10 19:58:57
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-03 23:29:41
-->
<template>
  <div class="app-container">
    <div class="container">
      <div class="menu-list">
        <keep-alive
          v-for="(item, index) in menuList"
          :key="index"
        >
          <router-link
            :to="item.path"
            tag="div"
            class="menu-item"
            :class="{
              'active-menu': activeMenu == item.id || activePath == item.path,
            }"
            @mouseover="activeMenu = item.id"
            @mouseleave="activeMenu = 0"
          >
            <span>{{ item.name }}</span>
          </router-link>
        </keep-alive>
      </div>
    </div>
  </div>
</template>
<script>
import defaultSettings from "@/config/defaultSettings";
export default {
  name: "Menu",
  data() {
    return {
      //菜单列表
      menuList: [],

      //活动路由
      activeMenu: 0,
      activePath: "/",
    };
  },
  created() {
    // 菜单列表赋值
    this.menuList = defaultSettings.menuList;
    const sonMenu = ["Personal", "Collection", "Comment", "Follow", "Release"];
    if (sonMenu.includes(this.$route.name)) {
      this.activePath = undefined;
    } else {
      this.activePath = this.$route.path;
    }
  },
  watch: {
    //监听路由 动态改变样式
    $route(to) {
      this.activePath = to.path;
    },
  },
};
</script>
<style lang="scss" scoped>
.app-container {
  display: flex;
  width: 880px;
  height: 100%;
  z-index: 100;
  .container {
    width: 100%;
    display: flex;
    justify-content: flex-end;
    align-items: center;
    height: 100%;
    .menu-list {
      display: flex;
      height: 100%;
      margin-right: 20px;
      .menu-item {
        width: 100px;
        line-height: 60px;
        height: 100%;
        position: relative;
        cursor: pointer;
        color: var(--menuText);
      }
      .menu-item:hover .active-men {
        display: block;
      }
      .active-menu {
        color: var(--menuTextActive);
        font-weight: 900;
      }
      .active-menu::before {
        content: " ";
        position: absolute;
        width: 100%;
        background:var(--menuTextActive);
        height: 5px;
        border-radius: 0 0 15px 15px;
        left: 0;
      }
    }
  }
}
</style>
