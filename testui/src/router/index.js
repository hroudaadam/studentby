import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home';
import Login from '../views/Login';
import StudentOffers from '../views/StudentOffers';
import StudentOfferDetail from '../views/StudentOfferDetail';
import StudentApplications from '../views/StudentApplications';
import EmployeeOffers from '../views/EmployeeOffers';
import EmployeeCreateOffer from '../views/EmployeeCreateOffer';
import EmployeeOfferDetail from '../views/EmployeeOfferDetail';
import StudentRegister from '../views/StudentRegister';

Vue.use(VueRouter);

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
    path: '/student/job-applications',
    name: 'StudentJobApplications',
    component: StudentApplications,
  },
  {
    path: '/student/register',
    name: 'StudentRegister',
    component: StudentRegister,
  },
  {
    path: '/employee/offers',
    name: 'EmplyoeeOffers',
    component: EmployeeOffers,
  },
  {
    path: '/employee/offers/:id',
    name: 'EmployeeOfferDetail',
    component: EmployeeOfferDetail,
    props: true
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
