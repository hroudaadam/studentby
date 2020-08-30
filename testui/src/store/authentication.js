import router from '../router'

export default {
    namespaced: true,
    state: {
        accessToken: null,
        userRole: null
    },
    actions: {
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
        setAccessToken(state, token) {
            state.accessToken = token;
        },
        setUserRole(state, role) {
            state.userRole = role;
        }
    }
}