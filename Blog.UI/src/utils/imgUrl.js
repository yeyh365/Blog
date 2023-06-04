/*
 * @Descripttion: 处理图片路径工具类
 * @Author: Yerik
 * @Date: 2021-06-01 14:11:58
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-18 20:25:57
 */

import baseSetting from '@/config/defaultSettings' // 引入图片基本访问路径

/**
 * @Description: 处理图片路径工具函数
 * @Author: Yerik
 * @Date: 2021-06-01 14:30:35
 * @param {*} url 图片路径 
 * @param {*} isNet 判断是否是本地图片
 * @return {*}
 */
export function imgUrl(url, isNet = false) {
    //判断是否是本地图片
    if (isNet) {
        return url
    }
    if (!url) return require('@/assets/notData/notData.png')


    // 判断图片链接是否为网络图片或base64
    if (url.substring(0, 4) === 'http' || url.substring(0, 10) === 'data:image') {
        // 网络图片或base64直接返回URL
        return url
    } else if(url.substring(0, 11) === 'UploadFiles') {
        // 非网络图片则拼接服务器BASE_URL
        return process.env.VUE_APP_BASE_URL + url
    }
    else{
        // 非网络图片则拼接服务器BASE_URL
        return baseSetting.baseImg + url
    }
}