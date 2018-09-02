import Vue from 'vue'
import Router from 'vue-router'
import mainPage from '@/components/mainPage'
import leaderBoard from '@/components/leaderBoard'
import user from '@/components/user'
import register from '@/components/register'
import Login from '@/components/Login'
import photoWall from '@/components/photoWall'
import ForgetPsw from '@/components/ForgetPsw'
import personalpage from '@/components/personalpage'
import set from '@/components/set'
import MomentDetail from '@/components/MomentDetail'
import tag from '@/components/tag'
import SearchResult from '@/components/SearchResult'
import Message from '@/components/Message'

Vue.use(Router)

export default new Router({
  routes: [{
      path: '/',
      name: 'register',
      component: register,
    }, {
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
      path: '/forgetpsw',
      name: 'ForgetPsw',
      component: ForgetPsw
    },
    {
      path: '/main',
      name: 'mainPage',
      component: mainPage,

      children: [{
        path: 'leaderBoard',
        name: 'leaderBoard',
        component: leaderBoard
      }, {
        path: 'user/:id',
        name: 'user',
        component: user
      },{
        path: 'photowall',
        name: 'photowall',
        component: photoWall
      },{
        path: 'personalpage/:id',
        name: 'personalpage',
        component: personalpage
      },{
        path: 'set',
        name: 'set',
        component: set
      },{
        path: 'momentDetail/:id',
        name: 'MomentDetail',
        component: MomentDetail
      }, {
        path: 'tag/:id',
        name: 'tag',
        component: tag
      },{
        path:'searchResult/:keyword',
        name:'searchResult',
        component:SearchResult
      },{
        path:'message/:id',
        name:'message',
        component:Message
      }]

    },

  ]
})