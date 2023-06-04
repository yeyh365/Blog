/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2023-01-24 00:01:14
 * @LastEditors: Yerik
 * @LastEditTime: 2023-02-04 00:57:10
 */

//引入vuex
import store from "@/store";
/**
 * 切换主题
 */
export function themeColor() {
    // 获取已存储在本地的系统主题
    const systemTheme = localStorage.getItem('SYSTEM_THEME')
    if (systemTheme === 'light') {
        localStorage.setItem('SYSTEM_THEME', 'dark')
        document.querySelector("html").className = 'dark'
    } else if (systemTheme === 'dark') {
        localStorage.setItem('SYSTEM_THEME', 'light')
        document.querySelector("html").className = 'light'
    }
    store.commit("SET_SYETEMTHEME", localStorage.getItem('SYSTEM_THEME'));
}


/**
 * 判断系统主题
 */
export function systemTheme() {
    const themeMedia = window.matchMedia("(prefers-color-scheme: light)");
    if (themeMedia.matches) {
        localStorage.setItem('SYSTEM_THEME', 'light')
        document.querySelector("html").className = 'light'
    } else {
        localStorage.setItem('SYSTEM_THEME', 'dark')
        document.querySelector("html").className = 'dark'
    }
    store.commit("SET_SYETEMTHEME", localStorage.getItem('SYSTEM_THEME'));
}

































