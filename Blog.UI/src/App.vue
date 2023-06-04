<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-10 12:07:39
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-03 23:37:41
-->
<template>
  <div id="app">
    <transition
      name="slide-fade"
      mode="out-in"
    >
      <div style="z-index:100">
        <router-view />
      </div>

    </transition>
    <!-- 音乐播放器 -->
    <!--  :lrcType="3" -->
    <aplayer
      :audio="audio"
      @update:volume="onListHide"
      :autoplay="false"
      fixed
      float
    />
    <!-- 拖拽按钮 -->
    <div>
      <DragBtn />
    </div>
    <el-backtop :visibility-height="400"></el-backtop>

  </div>
</template>
<script>
import { getMusicList } from "@/api/music/index";
import DragBtn from "@/components/DragBtn/index";
export default {
  name: "App",
  components: {
    DragBtn,
  },
  data() {
    return {
      audio: [],
      musicCover: require("@/assets/defaultData/other/music.png"),
    };
  },
  created() {
    this.init();
  },
  computed: {
    key() {
      return this.$route.path;
    },
  },
  methods: {
    init() {
      getMusicList().then((res) => {
        res.data.forEach((item) => {
          // 添加默认音乐播放器图片
          if (!item.cover) {
            item.cover = this.musicCover;
          }
        });
        this.audio = res.data;
      });
    },
    onListHide() {},
    randomColor() {
      return `#${((Math.random() * 0xffffff) << 0).toString(16)}`;
    },
  },
};
</script>
<style lang="scss">
* {
  padding: 0;
  margin: 0;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  background: #f5f6f7;
  height: 100%;
}

.slide-fade-enter-active {
  transition: all 0.8s ease;
}
.slide-fade-leave-active {
  transition: all 0.3s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-enter,
.slide-fade-leave-to {
  transform: translateX(10px);
  opacity: 0;
}


</style>
