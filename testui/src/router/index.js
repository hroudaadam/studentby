import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login'
import StudentOffers from '../views/StudentOffers.vue'
import StudentOfferDetail from '../views/StudentOfferDetail.vue'
import StudentApplications from '../views/StudentApplications.vue'
import EmployeeOffers from '../views/EmployeeOffers.vue'
import EmployeeCreateOffer from '../views/EmployeeCreateOffer.vue'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/student/offers',
    name: 'StudentOffers',
    component: StudentOffers
  },
  {
    path: '/student/offers/:id',
    name: 'StudentOfferDetail',
    component: StudentOfferDetail,
    props: true
  },
  {
    path: '/student/applications',
    name: 'StudentApplications',
    component: StudentApplications,
  },
  {
    path: '/employee/offers',
    name: 'EmplyoeeOffers',
    component: EmployeeOffers,
  },
  {
    path: '/employee/offers/create',
    name: 'EmployeeCreateOffer',
    component: EmployeeCreateOffer,
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
