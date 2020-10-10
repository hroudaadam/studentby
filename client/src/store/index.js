import Vue from 'vue';
import Vuex from 'vuex';
import router from '../router';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    // base API URL
    baseApiUrl: 'https://localhost:5001/api',
    // access token
    accessToken: null,
    // current user role
    userRole: null,
    // definition of user roles
    userRoles: {
      student: "student",
      studentInact: "studentInact",
      customer: "customer",
      operator: "operator"
    },
    // definition of job application states
    jobApplicationStates: {
      pending: "pending",
      approved: "approved",
      denied: "denied",
      attended: "attended",
      absent: "absent"
    },
    // ...
    loading: false,
    // ...
    errorMsg: null
  },
  getters: {
    isUserLogged(state) {
      return !!state.accessToken;
    },
    isStudentLogged(state) {
      return (!!state.accessToken && state.userRole === state.userRoles.student);
    },
    isStudentInactLogged(state) {
      return (!!state.accessToken && state.userRole === state.userRoles.studentInact);
    },
    isCustomerLogged(state) {
      return (!!state.accessToken && state.userRole === state.userRoles.customer);
    },
    isOperatorLogged(state) {
      return (!!state.accessToken && state.userRole === state.userRoles.operator);
    }
  },
  mutations: {
    setAccessToken(state, token) {
      state.accessToken = token;
    },
    setUserRole(state, role) {
      state.userRole = role;
    },
    setLoading(state, value) {
      state.loading = value;
    },
    setErrorMsg(state, message) {
      state.errorMsg = message;
    }
  },
  actions: {
    logoutStore({ commit }) {
      commit('setAccessToken', null);
      commit("setUserRole", null);
      router.push({ name: 'Login' });
    },
    loginStore({ commit }, userData) {
      commit('setAccessToken', userData.token);
      commit("setUserRole", userData.role);
      router.push({ name: 'Home' });
    }
  },
  modules: {
  }
})
