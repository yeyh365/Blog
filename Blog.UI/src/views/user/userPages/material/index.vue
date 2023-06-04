<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-10-22 17:00:59
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-02 20:59:02
-->
<template>
  <div class='app-container'>
    <div class='container'>
      <!-- 过滤按钮 -->
      <div class='material-status'>
        <el-button-group class="button-group">
          <el-button
            :type="filterForm.status==1?'primary':''"
            size="small"
            @click="changeMaterialStatus(1)"
            :disabled='!isSelf'
          ><i class='el-icon-remove-outline'></i> 待审核</el-button>
          <el-button
            :type="filterForm.status==2?'success':''"
            size="small"
            @click="changeMaterialStatus(2)"
          ><i class='el-icon-circle-check'></i> 已审核</el-button>
          <el-button
            type=""
            size="small"
            :type="filterForm.status==4?'warning':''"
            @click="changeMaterialStatus(4)"
            :disabled='!isSelf'
          ><i class='el-icon-circle-close'></i> 未通过</el-button>
          <el-button
            :type="filterForm.status==3?'danger':''"
            size="small"
            @click="changeMaterialStatus(3)"
            :disabled='!isSelf'
          ><i class='el-icon-warning-outline'></i> 下架</el-button>
          <el-button
            size="small"
            @click="changeMaterialStatus(5)"
            :disabled='!isSelf'
            :class='{"isCollection":filterForm.status==5}'
          ><i class='el-icon-star-off'></i> 收藏</el-button>
        </el-button-group>

      </div>
      <!-- 资源数据 -->
      <div
        class="not-data"
        v-if="materialList.length == 0"
      >
        <div class="img-container">
          <img
            width="100%"
            :src="notDataImg"
            :alt="notDataImg"
          />
        </div>
      </div>
      <div class='material-container'>
        <div
          v-for="(item,index) in materialList"
          :key='index'
          class='material-item'
        >
          <el-card
            class="box-card"
            shadow="hover"
            v-if='item.constructor === Object'
          >
            <MaertrialItem :materialData='item' />
            <div class='material-operation' v-if="isSelf">
              <el-button
                type="primary"
                icon="el-icon-edit"
                circle
                size="mini"
                style="margin-right:5px"
                @click="editMaterial(item)"
                v-if='filterForm.status!=5'
              ></el-button>
              <el-popconfirm
                title="你确定要删除这项推荐资源吗？"
                @confirm='deletMaterial(item)'
                v-if='filterForm.status!=5'
              >
                <el-button
                  type="danger"
                  icon="el-icon-delete"
                  circle
                  size="mini"
                  slot="reference"
                ></el-button>
              </el-popconfirm>
            </div>
          </el-card>
          <el-card
            class="box-card"
            shadow="hover"
            v-else
          >
            <div
              slot="header"
              class="clearfix"
            >
              <el-button
                style="float: right; padding: 0 0;color:red"
                type="text"
                class="delete-btn"
                @click="deleteCollection(item)"
              >删除</el-button>
            </div>
            <div class='delete-container'>
              <img
                :src="deleteDataImg"
                alt="资源不存在"
                width="105"
              >
            </div>
            <div>
              <p style="color:#CFCFCF;font-size:12px">资源已被删除</p>
            </div>

          </el-card>
        </div>
      </div>

      <!-- 加载更多 移动端 -->
      <div
        v-if="$utils.isMobile()"
        class="get-more-container"
      >
        <el-button
          :type="showGetMoreBtn?'primary':'info'"
          round
          size="mini"
          @click="getMore()"
          :loading='loading'
          :disabled='!showGetMoreBtn'
        ><span v-if='showGetMoreBtn'>加载更多</span><span v-else>没有更多了</span></el-button>
      </div>
      <!-- 分页 pc端-->
      <div
        class='pags-container'
        v-if="!$utils.isMobile() && materialList.length!=0"
      >
        <el-pagination
          background
          layout="prev, pager, next"
          :total="filterForm.total"
          :page-size='filterForm.list_rows'
          :current-page='filterForm.page'
          @current-change='currentChange'
          small
        >
        </el-pagination>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import {
  getUserMaterialByStatus,
  userDeleteMaterial,
  getUserCollectionMaterial,
  deleteUserCollectionMaterial,
} from "@/api/material/materialRecommend";
import MaertrialItem from "@/components/materialItem/index.vue";
export default {
  name: "Material",
  components: {
    MaertrialItem,
  },
  data() {
    return {
      //过滤表单
      filterForm: {
        status: 2,
        list_rows: 8,
        page: 1,
        total: 0,
      },

      //资源列表
      materialList: [],

      //没有数据图片
      notDataImg: require("@/assets/notData/notData.png"),

      //以被删除
      deleteDataImg: require("@/assets/notData/hasDelete.png"),

      //访客ID
      userId: undefined,

      //判断是否是本人
      isSelf: false,

      // 获取更多按钮加载状态
      loading: false,

      // 是否显示加载更多按钮
      showGetMoreBtn: true,
    };
  },
  created() {
    this.init();
  },
  methods: {
    async init(type = true) {
      //判断当前用户是本人还是访客 并判断是否禁用按钮
      this.userId = this.$route.params.userId;
      if (this.userId != this.$store.getters.userId) {
        this.isSelf = false;
        this.filterForm.status = 2;
      } else {
        this.isSelf = true;
      }

      let query = Object.assign({}, this.filterForm);
      query.total = 0;

      query.userId = this.userId;

      let data = null;
      //收藏数据
      if (query.status == 5) {
        data = await getUserCollectionMaterial(query);
      } else {
        data = await getUserMaterialByStatus(query);
      }

      if (type) {
        this.materialList = Object.assign([], data.data.data);
      } else {
        this.materialList = this.materialList.concat(data.data.data);

        // 控制获取更改按钮显示

        this.loading = false;
      }
      this.filterForm.total = data.data.total;

      if (
        this.filterForm.total <
        this.filterForm.list_rows * this.filterForm.page
      ) {
        this.showGetMoreBtn = false;
      } else {
        this.showGetMoreBtn = true;
      }
    },

    //切换资源状态
    changeMaterialStatus(status) {
      this.filterForm.status = status;
      this.filterForm.list_rows = 8;
      this.filterForm.page = 1;
      this.filterForm.total = 0;
      this.init();
    },

    //删除资源
    deletMaterial({ id }) {
      userDeleteMaterial({ id }).then((res) => {
        if (res.code == 200) {
          this.init();
          this.$notify({
            title: "成功",
            message: "资源已删除",
            type: "success",
          });
        }
      });
    },

    //删除不存在的资源
    deleteCollection(item) {
      deleteUserCollectionMaterial({ materialId: item }).then((res) => {
        this.init();
        this.$notify({
          title: "成功",
          message: "资源已移除",
          type: "success",
        });
      });
    },

    //编辑资源
    editMaterial(item) {
      //label重组
      item.label = [];
      item.typeId.forEach((value) => {
        item.label.push(value.type_id);
      });

      //改变下架的和未通过的状态
      if (item.status == 3 || item.status == 4) {
        item.status = 1;
      }

      //移除不需要的元素
      delete item.update_time;
      delete item.create_time;
      delete item.typeId;

      //跳转页面
      this.$router.push({
        path: "/materialRecommend",
        query: {
          status: "edit",
          data: JSON.stringify(item),
        },
      });
    },

    //分页切换
    currentChange(page) {
      this.filterForm.page = page;
      this.init();
    },

    // 获取更多
    getMore() {
      this.loading = true;
      this.filterForm.page++;
      this.init(false);
    },
  },
  computed: {
    ...mapGetters(["visitorId"]),
  },
  watch: {
    visitorId(value) {
      if (value == this.$store.getters.userId) {
        this.filterForm.status = 2;
      } else {
        this.filterForm.status = 5;
      }
      this.init();
    },
  },
};
</script>

<style lang="scss" scoped>
.app-container {
  width: calc(100% - 10px);
  background: var(--userDataBackground);
  height: 100%;

  .container {
    width: 100%;
    .material-status {
      width: 100%;
      display: flex;
      justify-content: flex-end;
      margin-bottom: 20px;
      overflow-x: auto;
      &::-webkit-scrollbar {
        height: 0;
      }
      &::-webkit-scrollbar-thumb {
        background-color: rgba(144, 147, 153, 0.3);
        border-radius: 2px;
      }
      &::-webkit-scrollbar-track {
        background-color: #f0f2f5;
      }
      &::-webkit-scrollbar-thumb:hover {
        background-color: rgba(144, 147, 153, 0.6);
      }
      &::-webkit-scrollbar-thumb:active {
        background-color: rgba(144, 147, 153, 0.9);
      }

      @media only screen and (max-device-width: 750px) {
        .button-group {
          width: 100%;
        }
      }
      .button-group {
        display: flex;
        flex-wrap: nowrap;
      }
    }
    .not-data {
      width: 100%;
      display: flex;
      background-color: var(--pageBackground);
      justify-content: center;
      align-content: center;
      .img-container {
        width: 400px;
      }
    }
    .material-container {
      width: 100%;
      display: flex;
      justify-content: flex-start;
      flex-wrap: wrap;

      @media only screen and (max-device-width: 750px) {
        .material-item {
          width: 100%;
        }
      }
      @media only screen and (min-device-width: 750px) {
        .material-item {
          width: 285px;
        }
      }
      .material-item {
        margin: 5px;

        .box-card {
          width: 100%;
          .material-operation {
            width: calc(100% - 5px);
            padding: 5px 5px 0 5px;
            display: flex;
            justify-content: flex-end;
          }
        }
      }
    }
    .isCollection {
      background: hotpink;
      color: #fff;
    }
    .pags-container {
      width: 100%;
      display: flex;
      justify-content: flex-end;
    }
    .get-more-container {
      width: 100%;
      display: flex;
      justify-content: center;
      margin-top: 5px;
    }
  }
}
// 修改el-card背景色
::v-deep .el-card {
  background-color: var(--materialCardBackground);
  border-color: var(--materialCardBackground);
}
</style>