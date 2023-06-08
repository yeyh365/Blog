/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-11 20:42:10
 * @LastEditors: Yerik
 * @LastEditTime: 2021-10-25 13:59:52
 */

//引入登录方法
import { login, getUserInfo, logout } from "@/api/login/index";
import LoginService from "@/api/services/LoginService"
import UserService from "@/api/services/UserService"


//设置token
import { setToken, removeToken } from '@/utils/auth'
const user = {
    state: {
        //token 
        token: undefined,

        //用户信息
        info: undefined,
    },

    mutations: {
        SET_TOKEN: (state, token) => {
            state.token = token
        },
        SET_INFO: (state, info) => {
            state.info = info
        }
    },

    actions: {
        // 登录
        Login({ commit }, userInfo) {
            return new Promise((resolve, reject) => {
                const data = { Account: userInfo.email, Password: userInfo.password }
                LoginService.Login(data).then(response=>{
                    //登录时发生错误
                    if (response.Code !== 200) {
                        resolve(false)
                        return
                    }
                    const token = response.Data

                    //vuex存token
                    commit('SET_TOKEN', token)

                    //本地存token
                    setToken(token)

                    //成功登录
                    resolve(true)
                    
                }).catch(error => {
                    reject(error)
                })
                // login(data).then(response => {

                //     //登录时发生错误
                //     if (response.code !== 200) {
                //         resolve(false)
                //         return
                //     }
                //     const token = response.data

                //     //vuex存token
                //     commit('SET_TOKEN', token)

                //     //本地存token
                //     setToken(token)

                //     //成功登录
                //     resolve(true)
                // }).catch(error => {
                //     reject(error)
                // })
            })
        },
         
        // 获取用户信息
        GetInfo({ commit }) {
            return new Promise((resolve, reject) => {
                UserService.FindLoginUser().then(response=>{
                    if (response) {
                        const result = response.Data
                        console.log('RES',result)
                        //在vueX设置用户信息
                        if (result) {
                            commit('SET_INFO', result)
                        } else {
                            reject(new Error('获取用户信息失败!'))
                        }
                        //返回请求信息
                        resolve(response)
                    }
                }).catch(error => {
                    reject(error)
                })
                // getUserInfo().then(response => {
                //     if (response) {
                //         const result = response.data

                //         //在vueX设置用户信息
                //         if (result) {
                //             commit('SET_INFO', result)
                //         } else {
                //             reject(new Error('获取用户信息失败!'))
                //         }
                //         //返回请求信息
                //         resolve(response)
                //     }

                // }).catch(error => {
                //     reject(error)
                // })
            })
        },

        // 用户退出登录
        Logout({ commit, state }) {
            return new Promise((resolve) => {
                commit('SET_TOKEN', '')
                        commit('SET_INFO', '')

                        //移除本地存储的token
                        removeToken()
                // logout(state.token).then((res) => {
                //     if (res.code == 200) {

                //         //移除vuex存在的token 个人信息
                        
                //     }
                //     resolve(true)
                // })
            })
        }

    }
}

export default user