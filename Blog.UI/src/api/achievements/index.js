/*
 * @Descripttion: 作品管理
 * @Author: Yerik
 * @Date: 2021-08-14 14:38:51
 * @LastEditors: Yerik
 * @LastEditTime: 2021-10-28 16:35:10
 */

import { request } from '@/utils/request'

/**
 * @Description:获取作品列表
 * @Author: Yerik
 * @Date: 2021-08-14 14:39:19
 * @param {*}
 * @return {*}
 */
export function getAchievementsList() {
    return request({
        url: '/blog/achievements/release/getAchievementsList',
        method: 'GET',
    })
}