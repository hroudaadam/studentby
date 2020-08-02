//import router from '../router'
//import store from '../store'
import apiSevice from '../helpers/apiService';

export default {
    namespaced: true,
    state: {
        offers: null,
        offerDetail: null,
        applications: null,
        applicationError: null
    },
    actions: {
        getAllOffers({ commit }) {
            apiSevice.get('/student/job-offers')
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    commit("setOffers", data)
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        getOfferDetails({ commit }, offerId) {
            apiSevice.get('/student/job-offers/' + offerId.toString())
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    commit('setOfferDetail', data);
                })
                .catch((error) => {                    
                    console.log(error);
                });
        },
        applyForJobOffer({ commit },offerId) {
            var body = {jobofferid: offerId};
            apiSevice.post('/student/job-applications', body)
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                })
                .catch((error) => {
                    commit('setApplicationError', error);                   
                    console.log(error);
                });
        },
        getStudentApplications({ commit }) {
            apiSevice.get('/student/job-applications')
            .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    commit('setStudentApplications', data);  
                })
                .catch((error) => {                                    
                    console.log(error);
                });
        }   
    },
    getters: {
        offerDetailTimeString(state) {
            var startDate = new Date(state.offerDetail.start);
            var endDate = new Date(state.offerDetail.end);
            return startDate.toString() + ' - ' + endDate.toString();
        },
        anyApplications(state) {
            if (!state.applications) {
                return false;
            }
            return state.applications.length > 0;
        }
    },
    mutations: {
        setOffers(state, offers) {
            state.offers = offers;
        },
        setOfferDetail(state, offer) {
            state.offerDetail = offer;
        },
        setApplicationError(state, error) {
            state.applicationError = error;
        },
        setStudentApplications(state, applications) {
            state.applications = applications;
        }
    }
}