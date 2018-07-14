import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import mainPage from '@/components/mainPage'
import leaderBoard from '@/components/leaderBoard'
import blogs from '@/components/blogs'
import user from '@/components/user'
import users from '@/components/users'
import topic from '@/components/topic'

Vue.use(Router)

export default new Router({
  routes: [{
      path: '/',
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
      } ]

    },

  ]
})
