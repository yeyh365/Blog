/**
 * Created by caohong on 5/31/2023.
 */
 import axiosInstance from '../common/export'

 export default {
   // GET /api/dictData  根据Key来获取字典数据
   GetArticleList(Type) {
     return new Promise((resolve, reject) => {
       axiosInstance.get(`/Article/GetArticleList`, { params:Type })
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
 