/*
 * @Descripttion: 自定义指令封装（防抖，节流）
 * @Author: Yerik
 * @Date: 2023-01-19 20:58:23
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-20 21:50:23
 */

// 引入vue
import Vue from 'vue'


// 引入防抖 节流
import { debounce, throttle } from '@/utils/utils'


// 防抖
Vue.directive('debounce', {
    inserted: (el, binding) => {

        // let [delay, immediate] = (binding.arg.split(','))
        
        let delay = Number(binding.arg) || 1000

        // 是否立即执行
        // immediate = immediate == 'true' ? true : false
        let immediate = true

        el.addEventListener('click', debounce(binding.value, delay, immediate))

    }
})

// 节流
Vue.directive('throttle', {
    bind: (el, binding) => {

        // 间隔时间
        let interval = Number(binding.arg) || 1000

        el.addEventListener('click', throttle(binding.value, interval, {
            leading: true,
            trailing: true,
        }))

    }
})
