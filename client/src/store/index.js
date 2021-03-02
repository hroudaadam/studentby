import Vue from "vue";
import Vuex from "vuex";
import router from "../router";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    // base API URL
    baseApiUrl: "/api",
    // access token
    accessToken: localStorage.getItem("accessToken") || null,
    // current user role
    userRole: localStorage.getItem("userRole") || null,
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
    // loading overlay
    loading: false,
    // error box
    errorMsg: null,
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
      localStorage.removeItem("accessToken");
      localStorage.removeItem("userRole");
      commit("setAccessToken", null);
      commit("setUserRole", null);
      router.push({ name: "Login" });
    },
    loginStore({ commit }, userData) {
      commit("setAccessToken", userData.token);
      commit("setUserRole", userData.role);
      localStorage.setItem("accessToken", userData.token);
      localStorage.setItem("userRole", userData.role);
      // catching router, because of "Avoided redundant navigation" error
      router.push({ name: "Home" }).catch(()=>{});
    }
  },
  modules: {
  }
})
