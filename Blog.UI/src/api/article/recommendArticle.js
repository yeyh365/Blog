/*
 * @Descripttion: 获取推荐文件
 * @Author: Yerik
 * @Date: 2021-08-15 21:03:24
 * @LastEditors: Yerik
 * @LastEditTime: 2022-02-21 21:38:52
 */
import { request } from '@/utils/request'

/**
 * @Description: 获取可展示的文章列表 默认获取推荐的文章
 * @Author: Yerik
 * @Date: 2021-08-15 21:04:26
 * @param {*}
 * @return {*}
 */
export function getRecommendArticle(data) {
    return request({
        url: 'blog/article/release/getRecommendArticle',
        method: 'POST',
        data
    })
}

/**
 * @Description: 文章阅读量增加
 * @Author: Yerik
 * @Date: 2021-08-16 00:00:36
 * @param {*}
 * @return {*}
 */
export function addArticleBrowse(data) {
    return request({
        url: 'blog/article/release/addArticleBrowse',
        method: 'GET',
        params: data
    })
}

/**
 * @Description: 文章点赞
 * @Author: Yerik
 * @Date: 2021-08-16 16:36:15
 * @param {*} data
 * @return {*}
 */
export function changeArticleThumbs(data) {
    return request({
        url: 'blog/article/verify/changeArticleThumbs',
        method: 'POST',
        data
    })
}

/**
 * @Description: 文章收藏
 * @Author: Yerik
 * @Date: 2021-08-28 20:36:02
 * @param {*} data
 * @return {*}
 */
export function changArticleCollection(data) {
    return request({
        url: 'blog/article/verify/changArticleCollection',
        method: 'POST',
        data
    })
}