<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2023-01-06 20:14:38
 * @LastEditors: Yerik
 * @LastEditTime: 2023-04-19 20:35:25
-->
<template>
  <div class="app-container">
    <div class="tab-container">
      <div class="tab-item" v-for="(item,index) in tabItemList" :key="index" @click="clickTab(item)">
        <div class="item-img">
          <img :src="item.img" :alt="item.name">
        </div>
        <div class="item-text">
          <p>{{item.name}}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        isDark: false,
        tabItemList: [{
          id: 1,
          name: '西药咨询',
          img: "https://axy-uni.oss-cn-beijing.aliyuncs.com/wenzheng.png"
        }, {
          id: 2,
          name: '西药咨询',
          img: "https://axy-uni.oss-cn-beijing.aliyuncs.com/wenzheng.png"
        }, {
          id: 3,
          name: '西药咨询',
          img: "https://axy-uni.oss-cn-beijing.aliyuncs.com/wenzheng.png"
        }]

      };
    },
    methods: {
      clickTab(index) {
        console.log(index)
       
      }
    },
  };
</script>

<style lang="scss" scoped>
  .app-container {
    .dark {
      width: 200px;
      height: 200px;
      transition: 3s;
      background: var(--headBackground);
    }

  }
</style>