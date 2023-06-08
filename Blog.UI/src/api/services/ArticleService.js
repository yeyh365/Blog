/**
 * Created by caohong on 5/31/2023.
 */
 import { Info } from 'vant'
import axiosInstance from '../common/export'

 export default {
   // GET /api/dictData  根据Key来获取字典数据
   GetArticleList(Search) {
     return new Promise((resolve, reject) => {
       axiosInstance.get(`/Article/GetArticleList`, { params:Search })
         .then(response => {
           resolve(response.data)
         })
         .catch(err => {
           reject(err)
         })
     })
   },
   GetArticleInfo(Id,UserId) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Article/GetArticleInfo`,{ params:{
        Id:Id,
        UserId:UserId
      } })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  CreateArticle(Info) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Article/CreateArticle`,Info)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
    // /删除一个文章
    DeleteArticle(delId) {
      return new Promise((resolve, reject) => {
        axiosInstance
          .delete(`/Article/DeleteArticle/${delId}`)
          .then((response) => {
            resolve(response.data)
          })
          .catch((err) => {
            reject(err)
          })
      })
    },
  GetArticleComment(Type) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Article/GetArticleComment/${Type}`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  GetArticleTypeList() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Article/GetArticleType`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  CreateInteraction(info) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Article/CreateInteraction`,info)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  UpdateInteraction(info) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Article/UpdateInteraction`,info)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  //增加文章评论
  CreateArticleComment(info) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Article/CreateArticleComment`,info)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
    //增加文章子级评论
    CreateChildComment(info) {
      return new Promise((resolve, reject) => {
        axiosInstance.post(`/Article/CreateChildComment`,info)
          .then(response => {
            resolve(response.data)
          })
          .catch(err => {
            reject(err)
          })
      })
    },
   dictDataByKey(Type, Key) {
     return new Promise((resolve, reject) => {
       axiosInstance.get(`/Dictionary/GetDictionaryList`, { params: { Type: Type, Key: Key }})
         .then(response => {
           resolve(response.data)
         })
         .catch(err => {
           reject(err)
         })
     })
   }
 }
 