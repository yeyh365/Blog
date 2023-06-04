/*
 * @Descripttion: 前台基本默认配置
 * @Author: Yerik
 * @Date: 2021-06-11 19:53:27
 * @LastEditors: Yerik
 * @LastEditTime: 2023-04-21 16:19:56
 */


export default {
    baseURL: process.env.NODE_ENV == 'development' ? 'https://yinheyibei.serve.yinheyibei.cn/' : 'https://yinheyibei.serve.yinheyibei.cn/', // 开发环境 基本请求路径,
    baseImg: process.env.NODE_ENV == 'development' ? 'https://yinheyibei.serve.yinheyibei.cn/' : 'https://yinheyibei.serve.yinheyibei.cn/', // 开发环境 图片基础访问路径
    websocketUrl: 'ws://8.131.60.32:2348', //长链接接口
    uploadImgUrl: '/blog/common/uploadImages', // 上传图片的接口
    module: 'blog', // 上传时携带的 模块表标识
    articleTag: 'md', //md上传图片时需要携带的标识、
    // 菜单列表
    menuList: [{
        id: 1,
        name: "资源推荐",
        path: "/home",
    },
    {
        id: 2,
        name: "文章",
        path: "/resources",
    },
    {
        id: 3,
        name: "项目",
        path: "/achievements",
    },
    // {
    //     id: 4,
    //     name: "积分商城",
    //     path: "/shoppingMall",
    // }
    ]
}