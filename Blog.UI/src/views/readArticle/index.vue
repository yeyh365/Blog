<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-08-11 15:31:23
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-03 21:41:50
-->
<template>
  <div class="app-container">
    <div class="container">
      <!-- 文章标题 -->
      <div class="article-title">
        <div>
          <h2>{{ articleData.ArticleTitle }}</h2>
        </div>
        <div class="back">
          <span @click="goBack">返回 <i class="el-icon-s-home"></i>
          </span>
        </div>
      </div>
      <!-- pc端 -->
      <div class="article-info">
        <div
          class="user-info"
          @click.stop="toUserInfo(articleData.UserInfo)"
        >
          <div class="user-avatar">
            <img
              width="100%"
              height="100%"
              :src="$utils.imgUrl(UserInfo.Photo )"
              alt="作者头像"
            />
          </div>
          <div class="user-name">
            <h4>{{ UserInfo.Name}}</h4> 
            <!-- <p>发布于 {{ $utils.getPastTimes(articleData.Created) }}</p>  -->
          </div>
          <!-- <div class="user-follow">
            <el-button
              size="mini"
              style="background: #fff1f4; border-color: #fff1f4"
              :icon="
                articleData.isFollow ? 'el-icon-star-on' : 'el-icon-star-off'
              "
              :loading="followBtnLoading"
              @click.stop="followUser"
            ><span v-if="articleData.IsFollow"> 已关注</span>
              <span v-else>关注</span>
            </el-button>
          </div> -->
        </div> 
        <div class="article-data">
          <a
            href="#article-comment"
            style=" text-decoration: none;"
          >
            <span class="other-item"><i class="el-icon-chat-dot-square"></i> {{ commentNum }}
            </span>
          </a>
          <span class="other-item"><i class="el-icon-view"></i> {{ articleData.BrowseNumCount }}
          </span>

          <span
            class="other-item"
            @click="changeThumbs()"
            :class="{ 'thumbs-item': InteractionType.isThumbs }"
          ><i
              class="el-icon-star-on"
              v-if="InteractionType.isThumbs"
            ></i><i
              class="el-icon-star-off"
              v-else
            ></i>
            {{ articleData.ThumbsNum }}
          </span>
          <span
            class="other-item"
            @click="changeCollection()"
            :class="{ 'is-Collection': InteractionType.isCollection }"
          ><i class="el-icon-collection-tag"></i>
            {{ articleData.CollectionNum }}
          </span>
        </div>
      </div>
      <!-- 移动端 -->
      <div class="article-info-phone">
        <div class="user-name">
           <h4 style="color:#66B1FF"><span @click.stop="toUserInfo(articleData.UserInfo)">{{ UserInfo.Name }}</span> <span class="article-time">发布于 </span> </h4>
        </div>
        <!-- <div class="user-follow">
          <el-button
            size="mini"
            style="background: #fff1f4; border-color: #fff1f4"
            :icon="
                articleData.IsFollow ? 'el-icon-star-on' : 'el-icon-star-off'
              "
            :loading="followBtnLoading"
            @click.stop="followUser"
          ><span v-if="articleData.IsFollow"> 已关注</span>
            <span v-else>关注</span>
          </el-button>
        </div> -->
      </div>
      <!-- 文章内容 -->
      <div class="article-container">
        <!-- <mavonEditor
          v-model="articleData.articleContent"
          :defaultOpen="'preview'"
          :toolbarsFlag="false"
          :subfield="false"
          :scrollStyle="true"
          :editable="false"
          :ishljs="true"
          :boxShadow="true"
          style="height: 100%;width:100%"
        /> -->
        <MavonEditor
          v-model="articleData.ArticleContent"
          :defaultOpen="'preview'"
          :toolbarsFlag="false"
          :subfield="false"
          :scrollStyle="true"
          :editable="false"
          :ishljs="true"
          :boxShadow="true"
          style="height: 100%;width:100%"
        />
      </div>
      <!-- 文章版权 -->
      <div
        class="article-copyright"
        v-if="!isAuthor"
      >
        <p>© 版权声明</p>
        <p>分享是一种美德，转载请保留原链接。</p>
        <p>
          当前链接：<span>
            <el-link
              :href="articleLink"
              type="primary"
            >{{
              articleLink
            }}</el-link>
          </span>
        </p>
      </div>
      <!-- 文章分类 -->
      <div class="article-tag">
        <p>
          <el-tag
            size="mini"
            effect="dark"
            class="item-tag"
          ><i class="el-icon-folder-opened"></i>
            <span v-if="  articleData.Classification">{{  articleData.Classification[0].TypeName }}</span>
          </el-tag>
        </p>
        <p>
          <el-tag
            size="mini"
            type="success"
            effect="dark"
            class="item-tag"
            v-for="(value, key) in articleData.Special"
            :key="key"
          ><i class="el-icon-collection-tag"></i>
            {{ value.TypeName }}</el-tag>
        </p>
        <p>
          <el-tag
            size="mini"
            type="info"
            class="item-tag"
            v-for="(value, key) in articleData.Label"
            :key="key"
          ><i class="el-icon-collection-tag"></i>
            {{ value.TypeName }}</el-tag>
        </p>
      </div>
      <!-- 移动端文章数据显示 -->
      <div
        class="article-data-container"
        v-if="$utils.isMobile()"
      >
        <div class="article-data">
          <a
            href="#article-comment"
            style=" text-decoration: none;color:#000"
          >
            <span class="other-item"><i class="el-icon-chat-dot-square"></i> {{ articleData.ArticleCommentNum }}
            </span>
          </a>
          <span class="other-item"><i class="el-icon-view"></i> {{ articleData.BrowseNumCount }}
          </span>

          <span
            class="other-item"
            @click="changeThumbs()"
            :class="{ 'thumbs-item': InteractionType.isThumbs }"
          ><i
              class="el-icon-star-on"
              v-if="InteractionType.isThumbs"
            ></i><i
              class="el-icon-star-off"
              v-else
            ></i>
            {{ articleData.ThumbsNum }}
          </span>
          <span
            class="other-item"
            @click="changeCollection()"
            :class="{ 'is-Collection': InteractionType.isCollection }"
          ><i class="el-icon-collection-tag"></i>
            {{ articleData.IsCollection }}
          </span>
        </div>
      </div>
      <!-- 评论 -->
      <div
        class="article-comment"
        id="article-comment"
      >
        <comment
          ref="refcomment"
          :avatar="UserInfo.Photo"
          :authorId="UserInfo.Id"
          :label="'作者'"
          :commentNum="commentNum"
          :commentList="commentList"
          @doSend="doSend($event)"
          @doChidSend="doChidSend(arguments)"
        ></comment>
      </div>
    </div>
  </div>
</template>

<script>
//引入markdown富文本编辑器
import MavonEditor from "@/components/mavonEditor";

//评论组件
import comment from "@/components/comment/index.vue";
import { readArticleContent } from "@/api/article/articleList";
import {
  sendArticleComment,
  getArticleComment,
} from "@/api/article/articleComment";
import {
  addArticleBrowse,
  changeArticleThumbs,
  changArticleCollection,
} from "@/api/article/recommendArticle";
import { blogUserFollowUser } from "@/api/user/followUser";
import ArticleService from '@/api/services/ArticleService'
import InteractionService from '@/api/services/InteractionService'
export default {
  name: "ReadArticle",
  components: {
    comment,
    MavonEditor,
  },
  data() {
    return {
      //文章ID
      articleId: undefined,

      //文章数据
      articleData: {
        //文章内容
        articleContent: "",

        //文章标题
        article_title: "",

        //文章发布时间
        create_time: "",

        //文章点赞
        thumbs_num: 0,

        //文章浏览
        browse_num: 0,

        //文章收藏量
        collection_num: 0,

        //作者信息
        getUserInfo: {},

        //是否已点赞
        isThumbs: false,

        //是否收藏
        isCollection: false,

        //是否关注
        isFollow: false,

        //文章专题
        special: [],

        //文章标签
        label: [],

        //文章分类
        getArticleClassification: {},
      },
      InteractionType:{
        //是否已点赞
        isThumbs: false,

        //是否收藏
        isCollection: false,

        //是否关注
        isFollow: false,
      },
      //定时器
      timer: "",

      //当前用户信息
      // userInfo: {
      //   id: 0,
      //   avatar_url:
      //     "uploads/admin/1/images/2021/08/18/462d9a7f1bc5909f111f230cdeaf78bc.jpg",
      // },

      //文章链接
      articleLink: "",

      //判断是不是作者
      isAuthor: false,

      //评论数据
      commentList: [],

      //评论数量
      commentNum: 0,

      //关注按钮loding状态
      followBtnLoading: false,
      UserInfo:{
        Name:'',
        Photo:'',
        Id:0
      },
      LgoinUserInfo:{
        Name:'',
        Photo:'',
        Id:0
      },
      Interaction:{
        TypeName:'',
        ArticleId:0,
        UserId:1,
        Status:null,
        AttentionUserId:0
      }
    };
  },
  created() {
    if (this.$store.getters.userInfo) {
      this.LgoinUserInfo.Name= this.$store.getters.userInfo.Name;
      this.LgoinUserInfo.Photo= this.$store.getters.userInfo.Photo;
      this.LgoinUserInfo.Id= this.$store.getters.userInfo.Id;
      
    } else {
      this.UserInfo.Id = 0;
    }

    this.articleId = this.$route.query.Id;
    this.init();
  },
  methods: {
    init() {
    ArticleService.GetArticleInfo(this.articleId,this.UserInfo.Id).then((res) => {

                this.articleData = Object.assign({}, res.Data);
        console.log('111 this.articleData', this.articleData)
         if (this.articleData.UserId != this.UserInfo.Id) {
          this.isAuthor = false;
        } else {
          this.isAuthor = true;
        }
        //获取当前完整路径
        this.articleLink = window.location.href;
        // return;
        //阅读时间为5秒
        if (this.articleData.UserId != this.UserInfo.Id) {
          this.timer = setTimeout(this.reading, 8000);
        }
      //   if (status) {
      //     this.articleList = Object.assign([], res.Data);
      //   } else {
      //     if (res.Data.length > 0) {
      //       this.articleList = this.articleList.concat(res.Data);
      //     }
      //   }
      //   console.log('222',res.Data.length)
      //   if (res.Data.length < this.Pages.Limit) {
      //     this.showGetMoreBtn = false;
      //   }
      // console.log('articleList',this.articleList.length)
      }).then(res=>{
         this.UserInfo.Name =this.articleData.UserInfo.Name
         this.UserInfo.Photo =this.articleData.UserInfo.Photo
         this.UserInfo.Id =this.articleData.UserInfo.Id
         this.Interaction.ArticleId=this.articleData.Id
         
      })
      //获取文章评论
      this.GetArticleComment()
      //获取文章收藏和点赞
      const CollectionInfo={
        TypeName:'Collection',
        ArticleId:this.articleId,
        UserId:this.LgoinUserInfo.Id
      }
InteractionService.GetInteractionInfo(CollectionInfo).then(res=>{
  if (res.Code==200){
    if(res.Data){
      this.InteractionType.isCollection=res.Data.Status
    }

  }
})
 const ThumbsInfo={
        TypeName:'Thumbs',
        ArticleId:this.articleId,
        UserId:this.LgoinUserInfo.Id
      }
      InteractionService.GetInteractionInfo(ThumbsInfo).then(res=>{
  if (res.Code==200){
    if(res.Data){
      this.InteractionType.isThumbs=res.Data.Status
      console.log('this.articleData',this.articleData)
    }
      
  }
})
      //页面初始化 获取除文章评论外所有的数据
      //readArticleContent({ articleId: 61 }).then((res) => {
        // this.articleData = Object.assign({}, res.data);
        // if (this.articleData.user_id != this.userInfo.id) {
        //   this.isAuthor = false;
        // } else {
        //   this.isAuthor = true;
        // }
        // //获取当前完整路径
        // this.articleLink = window.location.href;
        // // return;
        // //阅读时间为5秒
        // if (this.articleData.user_id != this.userInfo.id) {
        //   this.timer = setTimeout(this.reading, 5000);
        // }
      //});

      //获取文章评论
      // const aricleData = { article_id: this.articleId };
      // getArticleComment(aricleData).then((res) => {
      //   console.log(res)
      //   this.commentList = Object.assign([], res.data);

      //   //获取文章评论数量
      //   this.commentNum = this.getCommentList(this.commentList);
      // });
    },

    /**
     * 关注用户（作者）
     */
    followUser() {
      this.followBtnLoading = true;
        this.Interaction.TypeName='Follow'
        this.Interaction.Status=!this.articleData.IsFollow
        this.Interaction.AttentionUserId=this.articleData.UserInfo.Id
        console.log(this.Interaction)
        ArticleService.UpdateInteraction(this.Interaction).then((res) => {
        this.followBtnLoading = false;
        console.log(res)
        if (res.Code == 200) {
          if (this.articleData.isFollow) {
            this.$notify({
              title: "成功",
              message: `你已成功取消关注${this.articleData.UserInfo.Name}`,
              type: "warning",
            });
          } else {
            this.$notify({
              title: "成功",
              message: `你已成功关注${this.articleData.UserInfo.Name}`,
              type: "success",
            });
          }
          this.articleData.isFollow = !this.articleData.isFollow;
        }
      })
      //const data = { follow_id: this.articleData.UserInfo.Id };
      //blogUserFollowUser(data).then((res) => {
        // this.followBtnLoading = false;
        // if (res.code == 200) {
        //   if (this.articleData.isFollow) {
        //     this.$notify({
        //       title: "成功",
        //       message: `你已成功取消关注${this.articleData.getUserInfo.nickname}`,
        //       type: "warning",
        //     });
        //   } else {
        //     this.$notify({
        //       title: "成功",
        //       message: `你已成功关注${this.articleData.getUserInfo.nickname}`,
        //       type: "success",
        //     });
        //   }
        //   this.articleData.isFollow = !this.articleData.isFollow;
        // }
     // });
    },
    GetArticleComment(){
        ArticleService.GetArticleComment(this.articleId).then((res) => {
        this.commentList = Object.assign([], res.Data);
        this.commentNum = res.Total;
                console.log('111',this.commentList)
      })
    },
     
    /**
     * 获取评论条数
     */
    // getCommentList(item) {
    //   let num = item.length;
    //   item.forEach((value) => {
    //     if (value.children) {
    //       num += this.getCommentList(value.children);
    //     }
    //   });
    //   return num;
    // },

    /**
     * 初始输入框的发送事件
     */
    doSend(content) {
      if (content == "") {
        this.$message.error("请输入评论的内容");
        return;
      }
      const data = {
        //parent_id: 0,//父级ID
        FromUserId: this.LgoinUserInfo.Id,
        ReplyUserId: 0,
        Content:content,
        ArticleId: this.articleId,
      };
      ArticleService.CreateArticleComment(data).then((res) => {
        this.followBtnLoading = false;
        console.log(res)
        if (res.Code == 200) {
          this.$notify({
            title: "评论提交成功",
            message: "你的评论稍后展示",
            type: "success",
          });
         this.GetArticleComment()
         this.$refs.refcomment.cancelAll()
        }
      })
      // sendArticleComment(data).then((res) => {
      //   if (res.code == 200) {
      //     this.$notify({
      //       title: "评论提交成功",
      //       message: "你的评论管理员将在客服审核后给予你回复",
      //       type: "success",
      //     });
      //   }
      // });
    },

    /**
     * 子级回复评论
     */
    doChidSend(item) {
      const data = {
        Content: item[0], //文章内容
        ReplyUserId: item[1], //回复ID
        ConParentkey: item[2], //父级ID
        FromUserId: this.LgoinUserInfo.Id, //评论者ID
        ArticleId: this.articleId, //文章ID
      };
      if (data.content == "") {
        this.$message.error("请输入评论的内容");
        return;
      }
            ArticleService.CreateChildComment(data).then((res) => {
        this.followBtnLoading = false;
        console.log(res)
        if (res.Code == 200) {
          this.$notify({
            title: "评论提交成功",
            message: "你的评论稍后展示",
            type: "success",
          });
        this.GetArticleComment()
                 this.$refs.refcomment.cancelAll()
        }
      })
      // sendArticleComment(data).then((res) => {
      //   if (res.Code == 200) {
      //     this.$notify({
      //       title: "评论提交成功",
      //       message: "你的评论管理员将在客服审核后给予你回复",
      //       type: "success",
      //     });
      //   }
      // });
    },

    /**
     * 文章点赞
     */
    changeThumbs() {
        this.Interaction.TypeName='Thumbs'
        this.Interaction.Status=!this.articleData.IsThumbs
        console.log(this.Interaction)
        ArticleService.UpdateInteraction(this.Interaction).then((res) => {
         if (res.Code == 200) {
           if (this.articleData.IsThumbs) {
             this.articleData.ThumbsNum--;
           } else {
             this.articleData.ThumbsNum++;
           }
           this.InteractionType.isThumbs = !this.InteractionType.isThumbs;
         }
      })
    //   const data = {
    //     article_id: this.articleData.Id,
    //   };
    //   changeArticleThumbs(data).then((res) => {
    //     if (res.code == 200) {
    //       if (this.articleData.isThumbs) {
    //         this.articleData.thumbs_num--;
    //       } else {
    //         this.articleData.thumbs_num++;
    //       }
    //       this.articleData.isThumbs = !this.articleData.isThumbs;
    //     }
    //   });
     },

    /**
     * 收藏文章
     */
    changeCollection() {
        this.Interaction.TypeName='Collection'
         this.Interaction.Status=!this.articleData.IsCollection
        console.log(this.Interaction)
        ArticleService.UpdateInteraction(this.Interaction).then((res) => {
         if (res.Code == 200) {
           if (this.articleData.CollectionNum) {
             this.articleData.CollectionNum--;
           } else {
             this.articleData.CollectionNum++;
           }
           this.InteractionType.isCollection = !this.InteractionType.isCollection;
         }
      })
      // const data = {
      //   article_id: this.articleData.Id,
      // };
      // changArticleCollection(data).then((res) => {
      //   if (res.code == 200) {
      //     if (this.articleData.isCollection) {
      //       this.articleData.collection_num--;
      //     } else {
      //       this.articleData.collection_num++;
      //     }
      //     this.articleData.isCollection = !this.articleData.isCollection;
      //   }
      // });
    },

    /**
     * 返回上一页
     */
    goBack() {
      this.$router.go(-1);
    },

    /**
     * 阅读时间达标
     */
    reading() {
      //const data = { id: this.articleData.id };

      this.Interaction.TypeName='Browse'
      console.log(this.Interaction)
        ArticleService.CreateInteraction(this.Interaction).then((res) => {
        if (res.Code == 200) {
          this.articleData.BrowseNumCount++;
        }
      })
      // addArticleBrowse(data).then((res) => {
      //   if (res.code == 200) {
      //     this.articleData.BrowseNumCount++;
      //   }
      // });
    },

    /**
     * 去用户中心 访客
     */
    toUserInfo(item) {
      const USERID = item.id;
      this.$store.commit("SET_VISITOR_ID", USERID);
      const VISITORID = this.$store.getters.visitorId;
      this.$router.push({
        path: `/userInfo/${VISITORID}/releaseList`,
        query: {
          activeArticleType: 1,
        },
      });
    },
  },

  //页面销毁 清除定时器
  beforeDestroy() {
    clearTimeout(this.timer);
  },
};
</script>

<style lang="scss" scoped>
@media only screen and (max-device-width: 750px) {
  .app-container {
    padding: 0 10px;
    .container {
      width: calc(100% - 10px);
      padding: 0 5px;
    }
  }
}
@media only screen and (min-device-width: 750px) {
  .app-container {
    .container {
      width: calc(100% - 30px);
      padding: 0 15px;
    }
  }
}

.app-container {
  width: 100%;
  overflow: hidden;

  .container {
    border-radius: 10px;

    display: flex;
    justify-content: center;
    flex-wrap: wrap;
    background: var(--pageBackground);
    margin-bottom: 20px;
    .article-title {
      width: 100%;
      text-align: left;
      font-size: 18px;
      font-weight: 700;
      color: var(--materialCardText);
      margin-top: 10px;
      margin-bottom: 10px;
      display: flex;
      justify-content: space-between;
      align-items: flex-start;
      cursor: pointer;
      h2:hover {
        color: #00a2e3;
      }
      @media only screen and (max-device-width: 750px) {
        .back {
          display: none;
        }
      }
      @media only screen and (min-device-width: 750px) {
        .back {
          font-size: 14px;
          cursor: pointer;
          display: none;
          margin-right: 7px;
          display: block;
        }
        .back:hover {
          color: #00a2e3;
        }
      }
    }
    @media only screen and (min-device-width: 750px) {
      .article-info-phone {
        display: none;
      }
      .article-info {
        width: 100%;
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 10px;
        .user-info {
          display: flex;
          justify-content: flex-start;
          align-items: center;
          cursor: pointer;
          .user-avatar {
            width: 60px;
            height: 60px;
            border-radius: 50%;
            overflow: hidden;
            margin-right: 10px;
          }
          .user-name {
            text-align: left;
            margin-right: 20px;
            color: var(--materialCardText);
            p {
              font-size: 12px;
              color: var(--materialCardContent);
            }
          }
          .user-follow {
            button {
              color: #ff768f;
            }
          }
        }
        .article-data {
          background: #f5f6f7;
          padding: 7px;
          border-radius: 20px;
          cursor: pointer;
          .other-item {
            margin-right: 10px;
            font-size: 14px;
            color: var(--materialCardContent);
            .comment-anchor {
              text-decoration: none;
              color: var(--materialCardContent);
            }
          }
          .thumbs-item {
            color: #ebe15b;
          }
          .is-Collection {
            color: #ebe15b;
          }
        }
      }
    }
    @media only screen and (max-device-width: 750px) {
      .article-info {
        display: none;
      }
      .article-info-phone {
        width: 100%;
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 10px;
        .user-name {
          .article-time {
            font-size: 12px;
            color: #999;
          }
        }
        .user-follow {
          button {
            color: #ff768f;
          }
        }
      }
    }
  }
  .article-container {
    max-height: 500px;
    overflow: hidden;
    margin-bottom: 10px;
    width: 100%;
    z-index: 0;
  }
  .article-copyright {
    width: 100%;
    text-align: left;
    font-size: 13px;
    margin-bottom: 10px;
    color: var(--materialCardContent);
  }
  .article-tag {
    text-align: left;
    width: 100%;
    margin-bottom: 10px;
    p {
      margin-top: 5px;
    }
    .item-tag {
      margin-right: 3px;
    }
  }
  .article-data-container {
    width: 100%;
    display: flex;
    justify-content: end;
    .article-data {
      background: #f5f6f7;
      padding: 7px;
      border-radius: 20px;
      cursor: pointer;
      .other-item {
        margin-right: 10px;
        font-size: 14px;
        .comment-anchor {
          text-decoration: none;
          color: var(--materialCardContent);
        }
      }
      .thumbs-item {
        color: #ebe15b;
      }
      .is-Collection {
        color: #ebe15b;
      }
    }
  }
  .article-comment {
    z-index: 10;
    margin-bottom: 10px;
    width: 100%;
  }
}
</style>