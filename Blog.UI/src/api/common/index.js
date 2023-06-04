/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-11 19:52:42
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:39:13
 */
import { request } from '@/utils/request'

/**
 * @Description: 获取更新记录
 * @Author: Yerik
 * @Date: 2021-06-11 22:31:59
 * @return {*}
 */
export function getUpdateRecordList() {
    return request({
        url: 'blog/common/release/getUpdateRecordList',
        method: 'GET',
    })
}

/**
 * @Description: 获取声明内容
 * @Author: Yerik
 * @Date: 2021-06-11 22:31:59
 * @return {*}
 */
export function getStatement(data) {
    return request({
        url: 'blog/common/release/getStatement',
        method: 'GET',
        params: data
    })
}