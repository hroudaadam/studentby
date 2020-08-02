import router from '../router'
import apiSevice from '../helpers/apiService';

export default {
    namespaced: true,
    state: {
        loginEmail: null,
        loginPassword: null,
        accessToken: null,
        loginError: null,
        userRole: null
    },
    actions: {
        login({ commit, state }) {
            commit('setLoginError', null);
            apiSevice.post('/login', { email: state.loginEmail, password: state.loginPassword })
                .then((response) => response.json())
                .then((data) => {
                    commit("setAccessToken", data.token);
                    commit("setUserRole", data.role)
                    console.log(data);
                    router.push('/');
                })
                .catch((error) => {
                    commit('setLoginError', error);
                    console.log(error);
                  });
        },
        logout({commit}) {
            commit('setAccessToken', null);
            commit("setUserRole", null)
            router.push('/');
        }
    },
    getters: {
        isUserLoggedIn(state){
            return !!state.accessToken;
        },
        isStudentLogged(state){
            return (!!state.accessToken && state.userRole === "Student");
        },
        isEmployeeLogged(state){
            return (!!state.accessToken && state.userRole === "Employee");
        },
        isOperatorLogged(state){
            return (!!state.accessToken && state.userRole === "Admin");
        }
    },
    mutations: {
        setLoginEmail(state, email) {
            state.loginEmail = email;
        },
        setLoginPassword(state, password) {
            state.loginPassword = password;
        },
        setAccessToken(state, token) {
            state.accessToken = token;
        },
        setLoginError(state, error) {
            state.loginError = error;
        },
        setUserRole(state, role) {
            state.userRole = role;
        }
    }
}