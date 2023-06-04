/*
 * @Descripttion: 用户登录 && 注册接口
 * @Author: Yerik
 * @Date: 2021-06-11 19:52:42
 * @LastEditors: Yerik
 * @LastEditTime: 2022-04-11 11:30:13
 */
import { request } from '@/utils/request'

/**
 * @Description: 用户登录 
 * @Author: Yerik
 * @Date: 2021-06-11 22:31:59
 * @param {*} data 账号、密码 account password
 * @return {*}
 */
export function login(data) {
    return request({
        url: 'blog/login',
        method: 'POST',
        data
    })
}

/**
 * @Description: 获取用户信息 携带token即可
 * @Author: Yerik
 * @Date: 2021-06-11 22:32:57
 * @param {*}
 * @return {*}
 */
export function getUserInfo() {
    return request({
        url: 'blog/getUserInfo',
        method: 'GET',
    })
}

/**
 * @Description: 用于前台用户退出登录 携带token即可
 * @Author: Yerik
 * @Date: 2021-06-15 19:23:54
 * @param {*}
 * @return {*}
 */
export function logout() {
    return request({
        url: 'blog/userLogout',
        method: 'POST',
    })
}


/**
 * @Description: 用户前端用户注册获取验证码
 * @Author: Yerik
 * @Date: 2021-07-03 22:15:20
 * @param {*} data 邮箱email
 * @return {*}
 */
export function getRegisterCode(data) {
    return request({
        url: 'blog/getRegisterCode',
        method: 'POST',
        data
    })
}

/**
 * @Description: 前台用户获取邮箱验证码进行注册
 * @Author: Yerik
 * @Date: 2021-07-09 08:59:05
 * @param {*} data  邮箱emial 验证码code 密码password
 * @return {*}
 */
export function userRegister(data) {
    return request({
        url: 'blog/userRegister',
        method: 'POST',
        data
    })
}

/**
 * @Description: 获取前台访客信息
 * @Author: Yerik
 * @Date: 2021-10-25 19:53:24
 * @param {*} data
 * @return {*}
 */
export function getVisitorInfo(data) {
    return request({
        url: 'blog/getVisitorInfo',
        method: 'GET',
        params: data
    })
}

/**
 * @Description: 用户找回密码验证码code
 * @Author: Yerik
 * @Date: 2021-07-03 22:15:20
 * @param {*} data 邮箱email
 * @return {*}
 */
 export function retrievePasswordCode(data) {
    return request({
        url: 'blog/retrievePasswordCode',
        method: 'POST',
        data
    })
}

/**
 * @Description: 用户找回密码（重置密码）
 * @Author: Yerik
 * @Date: 2021-07-03 22:15:20
 * @param {*} data 邮箱email
 * @return {*}
 */
 export function userRetrievePassword(data) {
    return request({
        url: 'blog/userRetrievePassword',
        method: 'POST',
        data
    })
}