/**
 * Created by ye on 04/04/2023.
 */
import axiosInstance from '../common/export'
export default {
  // 获取管理员列表
  GetAdministratorList(Search) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`/Administrator/GetAdministratorList`, {
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
  GetAdministrator(Id) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`/Administrator/GetAdministratorInfo/${Id}`)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /删除一个管理员
  DelAdministrator(delId) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .delete(`/Administrator/DeleteAdministrator/${delId}`)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /修改一个管理员信息
  UpdateAdministrator(Info) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .put(`/Administrator/UpdateAdministrator`, Info)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  },
  // /创建一个管理员信息
  CreateAdministrator(Info) {
    return new Promise((resolve, reject) => {
      axiosInstance
        .post(`/Administrator/CreateAdministrator`, Info)
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
        .post(`/Administrator/FindLoginUser`)
        .then((response) => {
          resolve(response.data)
        })
        .catch((err) => {
          reject(err)
        })
    })
  }
}
