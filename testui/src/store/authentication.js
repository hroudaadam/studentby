import router from '../router'

export default {
    namespaced: true,
    state: {
        loginEmail: null,
        loginPassword: null,
        accessToken: null,
        loginError: null
    },
    actions: {
        login({ commit, state }) {
            commit('setLoginError', null);
            fetch("https://localhost:5001/api/user/login", {
                method: "POST",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ email: state.loginEmail, password: state.loginPassword }),
            })
                .then((response) => response.json())
                .then((data) => {
                    commit("setAccessToken", data.token);
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
            router.push('/');
        }
    },
    getters: {
        isLoggedIn(state){
            return !!state.accessToken;
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
        }
    }
}