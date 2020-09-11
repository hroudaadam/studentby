import Vue from 'vue';
import Vuex from 'vuex';
import authentication from './authentication';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    baseApiUrl: 'https://localhost:5001/api',
    roles: {
      student: "Student",
      customer: "Customer",
      operator: "Operator"
    },
    jobApplicationState: {
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
