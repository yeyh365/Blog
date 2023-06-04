<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2023-02-02 20:23:31
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 20:38:46
-->
<template>
  <div class="app-container">
    <svg-icon
      :icon-class="isDark?'dark':'light'"
      class="svg-icon"
      @click="changeTheme"
    />
  </div>
</template>

<script>
import { themeColor } from "@/utils/themeColor";
export default {
  name: "theme",
  data() {
    return {
      isDark: true,
    };
  },
  created() {
    // 系统主题
    const systemTheme = this.$store.getters.systemTheme;
    if (systemTheme === "dark") {
      this.isDark = true;
    } else {
      this.isDark = true;
    }
  },
  methods: {
    /**
     * 改变主题
     */
    changeTheme() {
      themeColor();
      this.isDark = !this.isDark;
    },
  },
};
</script>

<style lang="scss" scoped>
.app-container {
  height: 60px;
  display: flex;
  align-items: center;
  justify-content: center;
  .svg-icon {
    font-size: 25px;
    cursor: pointer;
  }
}
</style>