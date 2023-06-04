/*
 * @Descripttion: 音乐
 * @Author: Yerik
 * @Date: 2021-08-14 14:38:51
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:40:07
 */

import { request } from '@/utils/request'

/**
 * @Description:获取音乐
 * @Author: Yerik
 * @Date: 2021-08-14 14:39:19
 * @param {*}
 * @return {*}
 */
export function getMusicList() {
    return request({
        url: 'blog/getMusicList',
        method: 'GET',
    })
}