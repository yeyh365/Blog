/*
 * @Descripttion: 用户关注
 * @Author: Yerik
 * @Date: 2021-08-14 14:38:51
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:40:22
 */

import { request } from '@/utils/request'

/**
 * @Description:前台用户主动关注用户
 * @Author: Yerik
 * @Date: 2021-08-14 14:39:19
 * @param {*}
 * @return {*}
 */
export function blogUserFollowUser(data) {
    return request({
        url: 'blog/blogUser/blogUserFollowUser',
        method: 'POST',
        data
    })
}

/**
 * @Description: 前台用户获取关注列表
 * @Author: Yerik
 * @Date: 2021-09-13 09:51:21
 * @param {*} data
 * @return {*}
 */
export function getUserFollow(data) {
    return request({
        url: 'blog/blogUser/getUserFollow',
        method: 'GET',
        params: data
    })
}

/**
 * @Description: 前台用户获取粉丝列表
 * @Author: Yerik
 * @Date: 2021-09-13 09:51:21
 * @param {*} data
 * @return {*}
 */
export function getUserFans(data) {
    return request({
        url: 'blog/blogUser/getUserFans',
        method: 'GET',
        params: data
    })
}