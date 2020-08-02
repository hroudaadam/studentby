import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login'
//import Employee from '../views/Employee.vue'
import StudentOffers from '../views/StudentOffers.vue'
import StudentOfferDetail from '../views/StudentOfferDetail.vue'
import StudentApplications from '../views/StudentApplications.vue'
//import EmployeeOffers from '../components/employee/EmployeeOffers.vue'
//import EmployeeApplications from '../components/employee/EmployeeApplications.vue'

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
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
