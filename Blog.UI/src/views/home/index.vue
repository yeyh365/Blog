<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-10 12:07:39
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-04 19:37:57
-->
<template>
  <div class="app-container">
    <div class='container'>
      <!-- 轮播图 -->
      <div class='banner-container'>
        <Banner :imgs='bannerList' />
      </div>

      <!-- 资源部分-->
      <div class='material-container'>
        <!-- 过滤部分 -->

        <div
          class='material-filter'
          v-if='materialTypeList'
        >
          <div class='filter-status'>
            <div
              class='status-item'
              :class="{'excellent':filterForm.sort=='excellent'}"
              @click="changeFilterFormStatus('excellent')"
            >
              <svg-icon
                icon-class="excellent"
                class="svg-icon"
              />
              <span>
                推荐
              </span>
            </div>
            <div
              class='status-item'
              :class="{'hot':filterForm.sort=='hot'}"
              @click="changeFilterFormStatus('hot')"
            >
              <svg-icon
                icon-class="hot"
                class="svg-icon"
              />
              <span>
                最火
              </span>
            </div>
            <div
              class='status-item'
              :class="{'new':filterForm.sort=='new'}"
              @click="changeFilterFormStatus('new')"
            >
              <svg-icon
                icon-class="new"
                class="svg-icon"
              />
              <span>
                最新
              </span>
            </div>

          </div>
          <div
            class='filter-container'
            v-for='(item,index) in materialTypeList'
            :key='index'
          >
            <div class="filter-classification">
              <div class="filter-header">
                <svg-icon
                  :icon-class="item.icon"
                  class="svg-icon"
                /> {{item.TypeName}}
                <span class="division">|</span>
              </div>
              <div
                class="filter-item"
                v-if="item.Keywords"
              >
                <span
                  v-for="(v, k) in item.Keywords"
                  :key="k"
                  @click="changeMaterialFilter(v)"
                  :class="{
                'actice-item': v.TypeName == filterForm.keywords,
              }"
                >{{ v.TypeName }}</span>
              </div>
              <div
                v-else
                class="filter-item"
              >
                <span style="font-size:12px">没有数据</span>
              </div>
            </div>
          </div>

        </div>

        <div
          v-else
          class='material-filter'
        >请等待资源加载</div>
        <div
          class='material'
          v-if='materialList.length>0'
        >
          <div
            v-for="(item,index) in materialList"
            :key='index'
            class='material-item'
          >
            <el-card
              class="box-card"
              shadow="hover"
            >
              <MaertrialItem :materialData='item' />
            </el-card>
          </div>
          <div class='get-more'>
            <el-button
              :type="showGetMoreBtn?'primary':'info'"
              round
              size="mini"
              @click="getMore()"
              :loading='loading'
              :disabled='!showGetMoreBtn'
            ><span v-if='showGetMoreBtn'>加载更多</span><span v-else>没有更多了</span></el-button>
          </div>
        </div>
        <div
          class="no-data"
          v-else
        >
          <div><span>找不到资源咯！</span></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// 引入插件
import Banner from "@/components/Banner/index.vue";
import MaertrialItem from "@/components/materialItem/index.vue";
import { getBannerList } from "@/api/banner/index";
import {
  getMaterialType,
  getMaterialListByPages,
} from "@/api/material/materialRecommend";
import MaterialService from '@/api/services/MaterialService'
import DictDataService from '@/api/services/DictDataService'
export default {
  name: "Home",
  components: {
    Banner,
    MaertrialItem,
  },
  data() {
    return {
      //轮播图图片
      bannerList: [],

      //资源类型
      materialTypeList: [],

      //过滤表单
      filterForm: {
        sort: "excellent", //最新 最热 推荐
        keywords: undefined, //类型ID
        Limit: 6, //每页数量
        Page: 1, //当前页
      },

      //资源列表
      materialList: [],

      //loading加载动画
      loading: false,
      showGetMoreBtn: true,

      // 过滤标签图标
      filterIncons: [
        "filter-hot",
        "filter-code",
        "filter-direction",
        "filter-type",
        "filter-platform",
        "filter-frame",
      ],
    };
  },
  created() {
    this.init(true);
    this.configData();
  },

  methods: {
    //初始化数据
    init(status = true) {
      const query = {
        ...this.filterForm,
      };
       MaterialService.GetMaterialList(this.filterForm).then((res) => {
            this.loading = false;
        if (status) {
          this.materialList = Object.assign([], res.Data);
        } else {
          this.materialList = this.materialList.concat(res.Data);
          if (
            this.materialList.length <
            this.filterForm.Limit * this.filterForm.Page
          ) {
            this.showGetMoreBtn = false;
          }
        }
      })
      // getMaterialListByPages(query).then((res) => {
      //   this.loading = false;
      //   if (status) {
      //     this.materialList = Object.assign([], res.data.data);
      //   } else {
      //     this.materialList = this.materialList.concat(res.data.data);
      //     if (
      //       this.materialList.length <
      //       this.filterForm.list_rows * this.filterForm.page
      //     ) {
      //       this.showGetMoreBtn = false;
      //     }
      //   }
      // });
    },

    //配置数据
    configData() {
      //获取轮播图
      getBannerList().then((res) => {
        this.bannerList = Object.assign([], res.data);
      });
      //获取资源分类
      MaterialService.GetMaterialTypeList().then((res) => {
       this.materialTypeList = Object.assign([], res.Data);
        this.materialTypeList.forEach((item, index) => {
          item.icon = this.filterIncons[index];
        });
       
      })
      //获取资源分类
      // getMaterialType().then((res) => {
      //   this.materialTypeList = Object.assign([], res.data);
      //   this.materialTypeList.forEach((item, index) => {
      //     item.icon = this.filterIncons[index];
      //   });
      // });
    },

    //加载更多
    getMore() {
      this.loading = true;
      this.filterForm.Page++;
      this.init(false);
    },

    //改变选择类型
    changeMaterialFilter(info) {
      console.log('1111',info)
      this.showGetMoreBtn = true;
      this.filterForm.Limit = 6;
      this.filterForm.Page = 1;
      if (this.filterForm.keywords == info.TypeName) {
        this.filterForm.keywords = undefined;
      } else {
        this.filterForm.keywords = info.TypeName;
      }
      this.init();
    },

    //改变排序方式
    changeFilterFormStatus(sort) {
      this.showGetMoreBtn = true;
      this.filterForm.Page = 1;
      this.filterForm.sort = sort;
      this.init();
    },
  },
};
</script>
<style lang="scss" scoped>
@media only screen and (max-device-width: 750px) {
  .app-container {
    width: 100%;
    .container {
      .banner-container {
        margin: 0 10px 0 10px;
        width: calc(100% - 20px);
        height: 200px;
      }
    }
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    width: 880px;
    .container {
      .banner-container {
        width: 100%;
        height: 300px;
      }
    }
  }
}
.app-container {
  .container {
    width: 100%;
    .banner-container {
      border-radius: 10pxt;
      overflow: hidden;
      margin-bottom: 20px;
    }
    .material-container {
      width: 100%;
      .material-filter {
        width: 100%;
        margin-bottom: 20px;
        .filter-container {
          width: 100%;
          .filter-classification {
            display: flex;
            justify-content: flex-start;
            align-items: flex-start;
            flex-wrap: wrap;
            width: 100%;
            margin-top: 10px;
            .filter-header {
              color: var(--filterHeadeText);
              transition: 1s;
              font-size: 14px;
              cursor: pointer;
              width: 60px;
              font-weight: 500;
              .division {
                margin-left: 3px;
                margin-right: 3px;
              }
            }
            .filter-item {
              color: var(--filterText);
              transition: 1s;
              font-size: 14px;
              width: calc(100% - 60px);
              display: flex;
              justify-content: flex-start;
              flex-wrap: wrap;
              span {
                margin-left: 5px;
                margin-right: 5px;
                cursor: pointer;
              }
              span:hover {
                color: #409eff;
              }
            }
            .actice-item {
              color: #409eff;
              font-weight: 600;
            }
          }
        }
        .filter-status {
          width: 100%;
          display: flex;
          .status-item {
            color: #777;
            font-size: 14px;
            cursor: pointer;
            margin-right: 20px;
            .svg-icon {
              font-size: 18px;
            }
          }
          .excellent {
            font-weight: 600;
            color: #ffd90c;
          }
          .hot {
            font-weight: 600;
            color: #f56c6c;
          }
          .new {
            font-weight: 600;
            color: #409eff;
          }
        }
      }
      .material {
        width: 100%;
        display: flex;
        flex-wrap: wrap;
        justify-content: flex-start;
        margin-bottom: 20px;
        @media only screen and (max-device-width: 750px) {
          .material-item {
            margin: 5px;
            width: 100%;

            background: var(--materialCardBackground);
            transition: 1s;
            .box-card {
              width: 100%;
            }
          }
        }
        @media only screen and (min-device-width: 750px) {
          .material-item {
            margin: 5px;
            .box-card {
              width: 280px;
            }
          }
        }
        .get-more {
          width: 100%;
          display: flex;
          justify-content: center;
          margin-top: 5px;
        }
      }
      .no-data {
        width: 100%;
        height: 200px;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 14px;
        color: #777;
        span:before,
        span:after {
          content: "";
          width: 200px;
          height: 1px;
          background: #777;
          display: block; /*1.首先使伪类显示方式为块级元素*/
          position: relative; /*2.通过相对定位的方式控制两侧内容的位置*/
        }
        /*3.控制左侧横线的位置*/
        span:before {
          top: 11px;
          left: 180px;
        }
        /*4.控制右侧横线的位置*/
        span:after {
          top: -10px;
          right: 180px;
        }
      }
    }
  }
}

// 修改el-card背景色
::v-deep .el-card {
  background-color: var(--materialCardBackground);
  border-color: var(--materialCardBackground);
  transition: 1s;
}
</style>
