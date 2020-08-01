import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../components/auth/Login'
import Student from '../views/Student.vue'
import Employee from '../views/Employee.vue'
import StudentOffers from '../components/student/StudentOffers.vue'
import StudentApplications from '../components/student/StudentApplications.vue'
import EmployeeOffers from '../components/employee/EmployeeOffers.vue'
import EmployeeApplications from '../components/employee/EmployeeApplications.vue'

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
    path: '/student',
    name: 'Student',
    component: Student,
    children: [
      {
        path: 'offers',
        component: StudentOffers
      },
      {
        path: 'applications',
        component: StudentApplications
      },
      {
        path: '',
        redirect: 'offers'
      }
    ]
  },
  {
    path: '/employee',
    name: 'Employee',
    component: Employee,
    children: [
      {
        path: 'offers',
        component: EmployeeOffers
      },
      {
        path: 'applications',
        component: EmployeeApplications
      },
      {
        path: '',
        redirect: 'offers'
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
