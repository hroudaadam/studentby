import Vue from 'vue';
import Vuex from 'vuex';
import authentication from './authentication';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    baseApiUrl: 'https://192.168.0.103:5001/api',
    roles: {
      student: "Student",
      emplyoee: "Employee",
      operator: "Operator"
    },
    states: {
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
