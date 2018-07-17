import Vue from 'vue'
import Router from 'vue-router'
// import HelloWorld from '@/components/HelloWorld'
import mainPage from '@/components/mainPage'
import leaderBoard from '@/components/leaderBoard'
import blogs from '@/components/blogs'
import user from '@/components/user'
import users from '@/components/users'
import topic from '@/components/topic'
import register from '@/components/register'
import Login from '@/components/Login'
import photoWall from '@/components/photoWall'

Vue.use(Router)

export default new Router({
  routes: [{
    path: '/',
      name: 'register',
      component: register,
  },{
    path: '/login',
      name: 'Login',
      component: Login
  },
  {
    path: '/register',
    name: 'register',
    component: register
  },
    {
      path: '/main',
      name: 'mainPage',
      component: mainPage,

      children: [{
        path: 'leaderBoard',
        name: 'leaderBoard',
        component: leaderBoard
      },{
        path: 'blogs',
        name: 'blogs',
        component: blogs        
      },{
        path: 'users',
        name: 'users',
        component: users        
      },{
        path: 'user',
        name: 'user',
        component: user        
      },{
        path: 'topic',
        name: 'topic',
        component: topic        
      },{
        path: 'photowall',
        name: 'photowall',
        component: photoWall   
      } ]

    },

  ]
})
