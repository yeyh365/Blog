<!--
 * @Descripttion: 单个资源元素
 * @Author: Yerik
 * @Date: 2021-10-14 21:04:38
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-01 20:26:56
-->
<template>
  <div
    class='app-contaier'
    @click="toMaterialDetails"
  >
    <div class='material-container'>
      <div class="material-info">
        <div class='material-img'><img
            :src="$utils.imgUrl(getMaterial.MaterialCover)"
            alt="LOGO"
            width="100%"
          >
        </div>
        <div class='material-title'>
          <h4>{{getMaterial.MaterialName}}</h4>
          <p>{{getMaterial.MaterialDescribe}}</p>
        </div>
      </div>
      <div class='material-type'>
        <el-tag
          v-for="(item,index) in getMaterial.Keywords"
          :key="index"
          size='small'
          effect="plain"
          class="material-type-item"
        >{{item.TypeName}}</el-tag>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MaterialItem",
  props: {
    materialData: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      material: {},
    };
  },
  created() {
    this.material = Object.assign({}, this.materialData);
  },
  methods: {
    //去到资源详情页
    toMaterialDetails() {
      this.$router.push({
        path: "/materialDetails",
        query: {
          id: this.materialData.Id,
        },
      });
    },
  },
  computed: {
    getMaterial() {
      return this.materialData;
    },
  },
};
</script>


<style lang="scss" scoped>
.app-contaier {
  width: 100%;

  .material-container {
    width: 100%;

    &:hover {
      h4 {
        color: #63b0ff;
      }
    }

    cursor: pointer;
    width: 100%;

    .material-info {
      width: 100%;
      display: flex;
      justify-content: flex-start;

      .material-img {
        width: 80px;
        height: 80px;
        margin-right: 20px;
        display: flex;
        justify-content: center;
        align-items: center;
      }

      .material-title {
        text-align: left;
        width: calc(100% - 80px);

        h4 {
          width: 100%;
          margin-bottom: 8px;
          width: 100%;
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
          color: var(--materialCardText);
        }

        p {
          color: var(--materialCardContent);
          font-size: 14px;
          width: 100%;
          overflow: hidden;
          text-overflow: ellipsis;
          display: -webkit-box;
          -webkit-line-clamp: 2; //行数
          -webkit-box-orient: vertical;
        }
      }
    }

    .material-type {
      display: flex;
      justify-content: flex-start;
      margin-top: 5px;
      width: 100%;
      overflow-x: auto;

      &::-webkit-scrollbar {
        height: 4px;
       
      }

      &::-webkit-scrollbar-thumb {
        background-color: rgba(144, 147, 153, 0.3);
        border-radius: 2px;
      }

      &::-webkit-scrollbar-track {
        background-color: #f0f2f5dd;
      }

      &::-webkit-scrollbar-thumb:hover {
        background-color: rgba(144, 147, 153, 0.6);
      }

      &::-webkit-scrollbar-thumb:active {
        background-color: rgba(144, 147, 153, 0.9);
      }
      .material-type-item {
        margin-right: 5px;
        color: #888;
        border-color: transparent;
        background-color: rgba(136, 136, 136, 0.1);
      }
    }
  }
}
</style>