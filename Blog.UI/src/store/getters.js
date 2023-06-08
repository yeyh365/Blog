/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-11 20:47:50
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-31 19:35:08
 */
const getters = {

    //获取用户token
    token: state => state.user.token,

    //获取用户token
    hasLogin: state => {
        return !!state.user.token
    },

    //获取用户信息
    userInfo: state => state.user.info,

    //获取用户ID
    userId: state => state.user.info.Id,

    //访客用户ID
    visitorId: state => state.visitor.visitorId,

    //访客信息
    visitorInfo: state => state.visitor.visitorInfo,

    //系统主题
    systemTheme: state => state.setting.systemTheme,

}

export default getters