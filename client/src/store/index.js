import Vue from 'vue';
import Vuex from 'vuex';
import authentication from './authentication';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    baseApiUrl: 'https://localhost:5001/api',
    userRoles: {
      student: "Student",
      studentUnver: "StudentUnver",
      customer: "Customer",
      operator: "Operator"
    },
    jobApplicationStates: {
      pending: "Pending",
      approved: "Approved",
      denied: "Denied"
    }
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    authentication
  }
})
