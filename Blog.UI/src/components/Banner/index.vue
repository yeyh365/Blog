<!--
 * @Descripttion: 轮播图
 * @Author: Yerik
 * @Date: 2021-10-21 13:29:36
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-11 21:41:17
-->
<template>
  <div class='app-container'>
    <div class='container'>
      <swiper
        class="wrapper"
        :options="swiperOptions"
        style="z-index:1"
      >
        <swiper-slide
          v-for="(item,index) in getImg"
          :key='index'
          @click.native="toFindResources(item)"
        >
          <div class='swiper-item'> <img
              :src='$utils.imgUrl(item.BannerUrl)'
              style="width:100%;"
              :style="{height:$utils.isMobile()?'200px':'300px'}"
            />
            <div class='item-title'>
              <h4>{{item.BannerName}}</h4>
            </div>
            <div class='item-describe'>
              <span>{{item.Describe}}</span>
            </div>

          </div>

        </swiper-slide>
        <div
          class="swiper-pagination"
          slot="pagination"
        ></div>
        <div
          class="swiper-button-prev"
          slot="button-prev"
        ></div>
        <div
          class="swiper-button-next"
          slot="button-next"
        ></div>
      </swiper>
    </div>
  </div>
</template>

<script>
export default {
  name: "Banner",
  props: {
    imgs: {
      type: Array,
      default: () => {
        return [];
      },
    },
  },
  data() {
    return {
      swiperOptions: {
        // 分页器配置
        pagination: {
          el: ".swiper-pagination",
          clickable: true,
        },
        //过度时间
        speed: 750,
        // 设定初始化时slide的索引
        initialSlide: 0,
        //Slides的滑动方向，可设置水平(horizontal)或垂直(vertical)
        direction: "horizontal",
        // 自动切换图配置
        autoplay: {
          delay: 5000,
          disableOnInteraction: true,
        },
        effect: "coverflow",
        // 箭头配置
        navigation: {
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        },
        // 环状轮播
        loop: true,
        slidesPerView: 1,
        loopedSlides: 1,
        loopAdditionalSlides: 1,
      },
    };
  },
  methods: {
    //去发现资源
    toFindResources(item) {
      //外链
      if (item.IsLink == 1) {
        window.open(item.LinkUrl);
      } else {
        //文章0 资源 1
        if (item.IsMaterial == 0) {
          this.$router.push({
            name: "ReadArticle",
            query: {
              Id: item.QuoteId,
            },
          });
        } else {
          this.$router.push({
            name: "MaterialDetails",
            query: {
              Id: item.QuoteId,
            },
          });
        }
      }
    },
  },

  computed: {
    getImg() {
      console.log('IMGS',this.imgs)
      return this.imgs;
    },
  },
};
</script>
<style lang="scss" scoped>
.app-container {
  width: 100%;
  height: 100%;

  .container {
    width: 100%;
    height: 100%;

    .wrapper {
      border-radius: 10px;
      overflow: hidden;
      width: 100%;
      height: 100%;

      .swiper-item {
        cursor: pointer;
        position: relative;
        border-radius: 10px;
        overflow: hidden;

        .item-title {
          position: absolute;
          text-align: center;
          color: #fff;
          top: 50%;
          left: 50%;
          transform: translate(-50%, -50%);
          text-shadow: 0 0 5px #000;

          h4:hover {
            color: #409eff;
          }
        }

        .item-describe {
          width: calc(100% - 10px);
          position: absolute;
          bottom: 0;
          text-align: left;
          margin: 5px;
          padding: 5px;
          transform: translate(-5px, 0);
          background-color: rgba(0, 0, 0, 0.2);
          color: #fff;
          font-size: 13px;
          white-space: nowrap;
          text-overflow: ellipsis;
          overflow: hidden;
          background-image: linear-gradient(
            to top,
            rgba(122, 122, 122, 0.15),
            rgba(255, 255, 255, 0.03)
          );
        }
      }
    }
  }
}
</style>