/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-10 12:07:39
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-31 18:59:19
 */
import Vue from 'vue'
import Vuex from 'vuex'

//引入用户部分的VUEX
import user from './modules/user'

//引入访客部分的VUEX
import visitor from './modules/visitor'

// 引入系统设置部分的VUEX
import setting from './modules/setting'

//引入全局的getters方法
import getters from './getters'
Vue.use(Vuex)

export default new Vuex.Store({
    state: {},
    mutations: {},
    actions: {},
    modules: {
        user,
        visitor,
        setting
    },
    getters
})