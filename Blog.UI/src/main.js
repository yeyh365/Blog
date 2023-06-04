/*
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-06-10 12:07:39
 * @LastEditors: Yerik
 * @LastEditTime: 2023-01-31 18:39:24
 */
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import '@/router/permission' // 全局路由钩子

//引入全局scss
import "@/style/index.scss";

//给vuex设置token
import { getToken } from '@/utils/auth'
store.commit('SET_TOKEN', getToken())

// 判断系统主题
import { systemTheme } from './utils/themeColor'
systemTheme()


// 挂载自定义指令
import '@/directive/index'

//引入svg组件
import './icons'
//animate动画库
import 'animate.css'

//引入md5加密方式
import md5 from 'js-md5';
import { imgUrl } from '@/utils/imgUrl' // 导入自己处理图片路径的工具类
import { getPastTimes } from "@/utils/time"; //时间处理类

//音乐播放器全局注册
import APlayer from "@moefe/vue-aplayer";
Vue.use(APlayer);

//swiper轮播图全局使用
import VueAwesomeSwiper from 'vue-awesome-swiper';
import 'swiper/dist/css/swiper.min.css';
Vue.use(VueAwesomeSwiper);

// npm install animate.css --save安装，再引入
import animated from 'animate.css'
Vue.use(animated)

//引入element-ui库
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
Vue.use(ElementUI);

//引入vant
import Vant from 'vant';
import 'vant/lib/index.css';
Vue.use(Vant);

//引入全局注册组件
import { LoginBox } from '@/components/loginBox/index'
Vue.prototype.$Login = LoginBox.install;


//赋值到剪切板插件
import VueClipboard from 'vue-clipboard2'
Vue.use(VueClipboard)

// 验证是移动端还是PC端
import { isMobile } from '@/utils/validate'

//验证是否是对象
import { isObject } from "@/utils/validate";

//把自定义的工具类挂载在vue实例上
Vue.prototype.$utils = {
    md5,
    imgUrl,
    getPastTimes,
    isObject, isMobile
};


import '@/style/theme.css'





Vue.config.productionTip = false

new Vue({
    router,
    store,
    render: function (h) { return h(App) }
}).$mount('#app')