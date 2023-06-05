/**
 * Created by ye on 04/04/2023.
 */
 import user from '@/store/modules/user'
import axiosInstance from '../common/export'
 export default {
  // 获取管理员列表
  Login(Search) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`/Auth/Login`, {
          params: Search
        })
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // 获取管理员
  RegisterUser(user) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .post(`/Auth/RegisterUser`,user)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /删除一个管理员
  GetCode(user) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .post(`/Auth/SendCode/`,user)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /修改一个管理员信息
  UpdateUser(Info) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .put(`/User/UpdateUser`, Info)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /创建一个管理员信息
  CreateUser(Info) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .post(`/User/CreateUser`, Info)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /创建一个管理员信息
  FindLoginUser() {
    return new Promise((resolve, reject) => {
      axiosInstance
        .post(`/User/FindLoginUser`)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  }
}
