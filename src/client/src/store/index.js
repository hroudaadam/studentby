import Vue from "vue";
import Vuex from "vuex";
import router from "../router";
import apiSevice from "../helpers/apiService";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        // base API URL
        // baseApiUrl: "/api",
        baseApiUrl: "https://localhost:44304/api",
        // access token
        accessToken: localStorage.getItem("accessToken") || null,
        // current user role
        userRole: localStorage.getItem("userRole") || null,
        activated: localStorage.getItem("activated") || false,
        // definition of user roles
        userRoles: {
            student: "Student",
            customer: "Employer",
            operator: "Operator"
        },
        // definition of job application states
        jobApplicationStates: {
            pending: "Pending",
            approved: "Approved",
            denied: "Denied",
            attended: "Attended",
            absent: "Absent"
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
            return (!!state.accessToken && state.userRole === state.userRoles.student && state.activated);
        },
        isStudentInactLogged(state) {
            return (!!state.accessToken && state.userRole === state.userRoles.student && !state.activated);
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
        setActivated(state, value) {
            state.activated = value;
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
            localStorage.removeItem("activated");
            commit("setAccessToken", null);
            commit("setUserRole", null);
            commit("setActivated", false);
            router.push({ name: "Login" });
        },
        loginStore({ state, commit }, userData) {
            commit("setAccessToken", userData.accessToken);
            commit("setUserRole", userData.role);
            
            if (userData.role === state.userRoles.student) {
                apiSevice
                    .get("/students/me")
                    .then((response) => {
                        commit("setActivated", response.activated);
                        localStorage.setItem("activated", userData.activated);
                    })
                    .catch(() => { });
            }

            localStorage.setItem("accessToken", userData.token);
            localStorage.setItem("userRole", userData.role);
            
            // catching router, because of "Avoided redundant navigation" error
            router.push({ name: "Home" }).catch(() => { });
        }
    },
    modules: {
    }
})
