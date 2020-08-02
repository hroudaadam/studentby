import Vue from 'vue';
import Vuex from 'vuex';
import authentication from './authentication';
import student from './student';

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    baseApiUrl: 'https://localhost:5001/api'
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    authentication,
    student
  }
})
