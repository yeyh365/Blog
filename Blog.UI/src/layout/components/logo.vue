<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-10 19:58:48
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-04 00:59:23
-->
<template>
  <div class="app-contianer">
    <div
      class="contianer"
      @click="toHme"
    >
      <div class="logo">
        <img
          :src="logo"
          alt="Yerik~logo"
          class="logo-img"
        />
      </div>
      <div
        class="logo-title"
        @click="toHme"
      >
        <h3>Yerik</h3>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "Logo",
  data() {
    return {
      //引入logo图片
      logo: require("@/assets/logo.png"),
    };
  },
  methods: {
    toHme() {
      this.$router.push("/");
    },
  },
};
</script>
<style lang="scss" scoped>
.app-contianer {
  width: 200px;
  height: 100%;
  .contianer {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: start;
    align-items: center;
    background: var(--headBackground);
    transition: 1s;

    .logo {
      .logo-img {
        width: 50px;
        margin-right: 20px;
        cursor: pointer;
        border-radius: 50%;
        overflow: hidden;
      }
    }
    .logo-title {
      width:80px;
      cursor: pointer;
      background-image: -webkit-linear-gradient(left, #5eaff8, #87c2f8);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
    }
    @media only screen and (max-device-width: 750px) {
      .logo-title {
        display: none;
      }
    }
    @media only screen and (min-device-width: 750px) {
      .logo-title {
        display: block;
      }
    }
  }
}


</style>