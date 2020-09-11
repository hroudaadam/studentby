import router from '../router';
import store from './index';

export default {
    namespaced: true,
    state: {
        accessToken: null,
        userRole: null
    },
    actions: {
        logout({commit}) {
            commit('setAccessToken', null);
            commit("setUserRole", null);
            router.push({name: 'Home'});
        }
    },
    getters: {
        isUserLogged(state){
            return !!state.accessToken;
        },
        isStudentLogged(state){
            return (!!state.accessToken && state.userRole === store.state.roles.student);
        },
        isCustomerLogged(state){
            return (!!state.accessToken && state.userRole === store.state.roles.customer);
        },
        isOperatorLogged(state){
            return (!!state.accessToken && state.userRole === store.state.roles.operator);
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