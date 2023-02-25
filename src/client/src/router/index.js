import Vue from "vue";
import VueRouter from "vue-router";

import Home from "../views/Home"; 
import Login from "../views/Login";

import StudentRegister from "../views/student/Register";
import StudentJobOffers from "../views/student/JobOffers"; 
import StudentJobOfferDetail from "../views/student/JobOfferDetail"; 
import StudentJobApplications from "../views/student/JobApplications";
import StudentJobApplicationDetail from "../views/student/JobApplicationDetail";
import StudentProfile from "../views/student/Profile";

import CustomerJobOffers from "../views/customer/JobOffers";
import CustomerJobOfferCreate from "../views/customer/JobOfferCreate"; 
import CustomerJobOfferDetail from "../views/customer/JobOfferDetail"; 
import CustomerProfile from "../views/customer/Profile";
import SetPassword from "../views/SetPassword";

import OperatorJobOffers from "../views/operator/JobOffers";
import OperatorJobOfferDetail from "../views/operator/JobOfferDetail"; 
import OperatorJobApplications from "../views/operator/JobApplications";
import OperatorJobApplicationDetail from "../views/operator/JobApplicationDetail";
import OperatorGroups from "../views/operator/Groups";
import OperatorGroupCreate from "../views/operator/GroupCreate";
import OperatorGroupDetail from "../views/operator/GroupDetail";
import OperatorCustomerCreate from "../views/operator/CustomerCreate";
import OperatorStudents from "../views/operator/Students";
import OperatorStudentDetail from "../views/operator/StudentDetail";
import OperatorJobApplicationResult from "../views/operator/JobApplicationResult";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/student/profile",
    name: "StudentProfile",
    component: StudentProfile
  },
  {
    path: "/student/job-offers",
    name: "StudentJobOffers",
    component: StudentJobOffers
  },
  {
    path: "/student/job-offers/:jobOfferId",
    name: "StudentJobOfferDetail",
    component: StudentJobOfferDetail,
    props: true
  },
  {
    path: "/student/job-applications",
    name: "StudentJobApplications",
    component: StudentJobApplications,
  },
  {
    path: "/student/job-applications/:jobApplicationId",
    name: "StudentJobApplicationDetail",
    component: StudentJobApplicationDetail,
    props: true
  },
  {
    path: "/student/register",
    name: "StudentRegister",
    component: StudentRegister,
  },
  {
    path: "/set-password/:secret",
    name: "SetPassword",
    component: SetPassword,
    props: true
  },
  {
    path: "/customer/profile",
    name: "CustomerProfile",
    component: CustomerProfile,
  },
  {
    path: "/customer/job-offers",
    name: "CustomerJobOffers",
    component: CustomerJobOffers,
  },
  {
    path: "/customer/job-offers/:jobOfferId",
    name: "CustomerJobOfferDetail",
    component: CustomerJobOfferDetail,
    props: true
  },
  {
    path: "/customer/job-offers/create",
    name: "CustomerJobOfferCreate",
    component: CustomerJobOfferCreate,
  },
  {
    path: "/operator/job-applications",
    name: "OperatorJobApplications",
    component: OperatorJobApplications,
  },
  {
    path: "/operator/job-applications/:jobApplicationId",
    name: "OperatorJobApplicationDetail",
    component: OperatorJobApplicationDetail,
    props: true
  },
  {
    path: "/operator/groups",
    name: "OperatorGroups",
    component: OperatorGroups,
  },
  {
    path: "/operator/groups/:groupId",
    name: "OperatorGroupDetail",
    component: OperatorGroupDetail,
    props: true
  },
  {
    path: "/operator/groups/create",
    name: "OperatorGroupCreate",
    component: OperatorGroupCreate,
  },
  {
    path: "/operator/groups/:groupId/create-customer",
    name: "OperatorCustomerCreate",
    component: OperatorCustomerCreate,
    props: true
  },
  {
    path: "/operator/students",
    name: "OperatorStudents",
    component: OperatorStudents,
  },
{
  path: "/operator/students/:studentId",
  name: "OperatorStudentDetail",
  component: OperatorStudentDetail,
  props: true
},
  {
    path: "/operator/job-offers",
    name: "OperatorJobOffers",
    component: OperatorJobOffers,
  },
  {
    path: "/operator/job-offers/:jobOfferId",
    name: "OperatorJobOfferDetail",
    component: OperatorJobOfferDetail,
    props: true
  },
  {
    path: "/operator/job-offers/:jobOfferId/job-applications/:jobApplicationId",
    name: "OperatorJobApplicationResult",
    component: OperatorJobApplicationResult,
    props: true
  },
  {
    path: "*",
    redirect: { name: "Home" }
  }

];

const router = new VueRouter({
  mode: "history",
  routes
});

export default router;
