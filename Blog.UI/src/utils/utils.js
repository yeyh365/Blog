/*
 * @Descripttion: 工具函数
 * @Author: Yerik
 * @Date: 2022-05-04 20:56:05
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-19 23:44:01
 */



/**
 * 防抖
 * @param {需要被执行的函数} fn 
 * @param {延时} delay 
 * @param {是否立即执行} immediate 
 */
export function debounce(fn, delay = 1000, immediate = false) {

    // 定时器
    let timer = null

    // 是否执行过
    let isInvoke = false

    // 需要返回执行的函数
    const _debounce = function () {

        // 反复点击执行 如果有定时器就清除定时器
        if (timer) {
            clearTimeout(timer)
        }

        // 处理是否立即执行 如果immediate：true && isInvoke：false 则立即执行
        if (immediate && !isInvoke) {
            fn()

            // 执行完 改变执行状态
            isInvoke = true
        } else {
            // 常规防抖
            timer = setTimeout(() => {
                fn()
                isInvoke = false
                timer = null
            }, delay)
        }
    }
    return _debounce
}


/**
 * 节流
 * @param {需要被执行的函数} fn 
 * @param {间隔} interval 
 * @param {leading：第一次是否执行，trailing：最后一次是否执行} options 
 */
export function throttle(fn, interval, options = {
    leading: true,
    trailing: false,
}) {
    const {
        leading,
        trailing
    } = options

    // 上一次执行时间
    let lastTime = 0

    // 定时器
    let timer = null

    // 返回要执行的函数
    const _throttle = function () {
        // 当前时间
        const nowTime = new Date().getTime()

        // 一次是否立即执行
        if (!lastTime && !leading) {
            lastTime = nowTime
        }

        // 剩余时间
        const remainTime = interval - (nowTime - lastTime)

        // 看是否还有剩余时间 没有剩余时间就清除定时器 执行函数
        if (remainTime < 0) {
            // 清除定时器
            if (timer) {
                clearTimeout(timer)
                timer = null
            }

            fn()

            lastTime = nowTime
        }

        // 加定时器 让最后一次执行
        if (trailing && !timer) {
            timer = setTimeout(() => {
                timer = null
                lastTime = !leading ? 0 : new Date().getTime()
                fn()

            }, remainTime)
        }

    }

    return _throttle

}