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
                .then((jsonResponse) => {
                    console.log(jsonResponse);
                    commit('setOffers', jsonResponse);
                })
                .catch((error) => {
                    console.log(error.message);
                });
        },
        getOfferDetails({ commit }, offerId) {
            apiSevice.get('/student/job-offers/' + offerId.toString())
                .then((jsonResponse) => {
                    console.log(jsonResponse);
                    commit('setOfferDetail', jsonResponse);
                })
                .catch((error) => {
                    console.log(error.message);
                });
        },
        applyForJobOffer({ commit }, offerId) {
            var body = { jobofferid: offerId };

            apiSevice.post('/student/job-applications', body)
                .then((jsonResponse) => {
                    console.log(jsonResponse);
                })
                .catch((error) => {
                    commit('setApplicationError', error);
                    console.log(error.message);
                });

        },
        getStudentApplications({ commit }) {
            apiSevice.get('/student/job-applications')
                .then((jsonResponse) => {
                    console.log(jsonResponse);
                    commit('setStudentApplications', jsonResponse);
                })
                .catch((error) => {
                    console.log(error.message);
                });
        },
        cancelJobApplication({ commit }, applicationId) {
            apiSevice.deleteMehod('/student/job-applications/' + applicationId.toString())
                .then((jsonResponse) => {
                    console.log(jsonResponse);
                })
                .catch((error) => {
                    commit('setApplicationError', error);
                    console.log(error.message);
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