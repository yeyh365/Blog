/*
 * @Descripttion: 用户信息获取 && 修改
 * @Author: Yerik
 * @Date: 2021-07-21 00:23:22
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:41:21
 */


import { request } from '@/utils/request'

/**
 * @Description: 用户获取自己的用户信息 
 * @Author: Yerik
 * @Date: 2021-06-11 22:31:59
 * @param {*} data 账号、密码 account password
 * @return {*}
 */
export function getBlogUserInfo() {
    return request({
        url: 'blog/blogUser/getBlogUserInfo',
        method: 'GET',
    })
}


/**
 * @Description: 用户修改自己的用户信息
 * @Author: Yerik
 * @Date: 2021-07-21 14:01:04
 * @param {*} data
 * @return {*}
 */
export function updateBlogUserInfo(data) {
    return request({
        url: 'blog/blogUser/updateBlogUserInfo',
        method: 'PUT',
        data
    })
}


/**
 * @Description: 用户修改自己的用户密码
 * @Author: Yerik
 * @Date: 2021-07-21 14:01:04
 * @param {*} data
 * @return {*}
 */
export function updateBlogUserPassword(data) {
    return request({
        url: 'blog/blogUser/updateBlogUserPassword',
        method: 'PUT',
        data
    })
}