/**
 * Created by ye on 04/04/2023.
 */
import axiosInstance from '../common/export'
export default {
  // 获取管理员列表
  GetUserList(Search) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`/User/GetUserList`, {
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
  GetUser(Id) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`/User/GetUserInfo/${Id}`)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /删除一个管理员
  DelUser(delId) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .delete(`/User/DeleteUser/${delId}`)
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
