<!--
 * @Descripttion: 
 * @Author: Yerik
 * @Date: 2021-08-18 14:41:42
 * @LastEditors: Yerik
 * @LastEditTime: 2021-08-18 14:45:22
-->
<template>
	<div class="user">
		<div class="user-img"><img :src="avatar?$utils.imgUrl(avatar):require('./img/icon/avatar.jpg')"/></div>
	</div>
</template>


<script>
	export default {
	  props:{
	  	avatar:{
	  		type:String,
	  		default:''
	  	}
	  },	
	  data() {
	    return {
	      comment: ''
	    }
	  },
	 
	}
</script>

<style type="text/css" scoped>
	.user{
		height: 50px;
		width: 50px;
	}
	.user-img img{
		height: 45px;
		width: 45px;
		border-radius:50%;
	}

</style>