/*
 * @Descripttion: 测试类型接口
 * @Author: Yerik
 * @Date: 2021-06-11 19:52:42
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:40:13
 */
import { request } from '@/utils/request'

/**
 * @Description: 用户登录 
 * @Author: Yerik
 * @Date: 2021-06-11 22:31:59
 * @param {*} data 账号、密码 account password
 * @return {*}
 */
export function sendMessages() {
    return request({
        url: 'test/test',
        method: 'GET',
    })
}