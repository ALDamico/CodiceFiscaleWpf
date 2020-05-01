import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/fiscalCode',
    name: 'fiscalCode',
    component: () => import('../views/FiscalCodeResult.vue'),
    props: true
  },
  {
    path: '/myFiscalCodes',
    name: 'myFiscalCodes',
    component: () => import('../views/FiscalCodeListComponent.vue')
  },
  {
    path: '/validate',
    name: 'Convalida',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "validate" */ '../views/Validate.vue')
  },
  {
    path: '/error',
    name: 'Errore',
    props: true,
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "error" */ '../views/Error/ErrorPage.vue')
  },
  {
    path: '/validationResult',
    name: 'validationResult',
    props: true,
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "validationResult" */ '../views/ValidationResultView.vue')
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
