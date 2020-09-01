import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home'; 
import Login from '../views/Login'; 

import StudentRegister from '../views/student/Register';
import StudentJobOffers from '../views/student/JobOffers'; 
import StudentJobOfferDetail from '../views/student/JobOfferDetail'; 
import StudentJobApplications from '../views/student/JobApplications';
import StudentJobApplicationDetail from '../views/student/JobApplicationDetail';

import CustomerJobOffers from '../views/customer/JobOffers';
import CustomerJobOfferCreate from '../views/customer/JobOfferCreate'; 
import CustomerJobOfferDetail from '../views/customer/JobOfferDetail'; 

import OperatorJobApplications from '../views/operator/JobApplications';
import OperatorJobApplicationDetail from '../views/operator/JobApplicationDetail';
import OperatorGroups from '../views/operator/Groups';

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
    path: '/student/job-offers',
    name: 'StudentJobOffers',
    component: StudentJobOffers
  },
  {
    path: '/student/job-offers/:id',
    name: 'StudentJobOfferDetail',
    component: StudentJobOfferDetail,
    props: true
  },
  {
    path: '/student/job-applications',
    name: 'StudentJobApplications',
    component: StudentJobApplications,
  },
  {
    path: '/student/job-applications/:id',
    name: 'StudentJobApplicationDetail',
    component: StudentJobApplicationDetail,
    props: true
  },
  {
    path: '/student/register',
    name: 'StudentRegister',
    component: StudentRegister,
  },
  {
    path: '/customer/job-offers',
    name: 'CustomerJobOffers',
    component: CustomerJobOffers,
  },
  {
    path: '/customer/job-offers/:id',
    name: 'CustomerJobOfferDetail',
    component: CustomerJobOfferDetail,
    props: true
  },
  {
    path: '/customer/job-offers/create',
    name: 'CustomerJobOfferCreate',
    component: CustomerJobOfferCreate,
  },
  {
    path: '/operator/job-applications',
    name: 'OperatorJobApplications',
    component: OperatorJobApplications,
  },
  {
    path: '/operator/job-applications/:id',
    name: 'OperatorJobApplicationDetail',
    component: OperatorJobApplicationDetail,
    props: true
  },
  {
    path: '/operator/groups',
    name: 'OperatorGroups',
    component: OperatorGroups,
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
