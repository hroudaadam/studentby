import Vue from 'vue';
import Vuex from 'vuex';
import authentication from './authentication';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    baseApiUrl: 'https://localhost:5001/api',
    userRoles: {
      student: "student",
      studentInact: "studentInact",
      customer: "customer",
      operator: "operator"
    },
    jobApplicationStates: {
      pending: "pending",
      approved: "approved",
      denied: "denied",
      attended: "attended",
      absent: "absent"
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
