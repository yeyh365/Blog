/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2023-01-31 18:56:02
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-31 18:58:25
 */

const setting = {
    state: {
        // 系统主题
        systemTheme: 'light'
    },

    mutations: {
        SET_SYETEMTHEME: (state, theme) => {
            state.systemTheme = theme
        },
    },

}

export default setting