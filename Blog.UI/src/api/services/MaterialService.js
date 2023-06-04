/**
 * Created by caohong on 5/31/2023.
 */
 import axiosInstance from '../common/export'

 export default {
   // GET /api/dictData  根据Key来获取字典数据
   GetMaterialList(Type) {
     return new Promise((resolve, reject) => {
       axiosInstance.get(`/Material/GetMaterialList`, { params:Type })
         .then(response => {
           resolve(response.data)
         })
         .catch(err => {
           reject(err)
         })
     })
   },
   GetMaterialInfo(Type) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Material/GetMaterialInfo`, { params:{Id:Type} })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  GetMaterialTypeList() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Material/GetMaterialType`)
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
 