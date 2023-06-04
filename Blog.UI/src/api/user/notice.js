/*
 * @Descripttion: 消息通知
 * @Author: Yerik
 * @Date: 2021-08-14 14:38:51
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:40:29
 */

import { request } from '@/utils/request'

/**
 * @Description: 获取消息通知列表
 * @Author: Yerik
 * @Date: 2021-08-14 14:39:19
 * @param {*}
 * @return {*}
 */
export function getNoticeList(data) {
    return request({
        url: 'blog/blogUser/getNoticeList',
        method: 'GET',
        params: data
    })
}

/**
 * @Description: 修改消息未读标识
 * @Author: Yerik
 * @Date: 2021-08-14 23:07:05
 * @param {*} data
 * @return {*}
 */
export function isReadNotice(data) {
    return request({
        url: 'blog/blogUser/isReadNotice',
        method: 'POST',
        data
    })
}

/**
 * @Description: 主动删除消息
 * @Author: Yerik
 * @Date: 2021-09-11 10:44:58
 * @param {*} data
 * @return {*}
 */
export function deleteNotice(data) {
    return request({
        url: 'blog/blogUser/deleteNotice',
        method: 'DELETE',
        data
    })
}