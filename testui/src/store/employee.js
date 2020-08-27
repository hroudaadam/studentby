//import router from '../router'
//import store from '../store'
import apiSevice from '../helpers/apiService';

export default {
    namespaced: true,
    state: {
        offers: null,
        newOffer: {
            title: null,
            description: null,
            wage: null,
            spaces: null,
            start: { date: null, time: null },
            end: { date: null, time: null }
        },
        createOfferError: null

    },
    actions: {
        createOffer({ commit, state, getters }) {
            var body = {
                title: state.newOffer.title,
                description: state.newOffer.description,
                wage: state.newOffer.wage,
                spaces: state.newOffer.spaces,
                start: getters.startISOString,
                end: getters.endISOString
            };
            apiSevice.post('/employee/job-offers', body)
                .then((jsonResponse) => {
                    console.log(jsonResponse);
                    commit('setCreateOfferError', jsonResponse);
                })
                .catch((error) => {
                    console.log(error.message);
                });
        }
    },
    getters: {
        startISOString(state) {
            var stringDate = state.newOffer.start.date + 'T' + state.newOffer.start.time;
            var date = new Date(stringDate);
            return date.toISOString();
        },
        endISOString(state) {
            var stringDate = state.newOffer.end.date + 'T' + state.newOffer.end.time;
            var date = new Date(stringDate);
            return date.toISOString();
        },
    },
    mutations: {
        setOffers(state, offers) {
            state.offers = offers;
        },
        setCreateOfferError(state, error) {
            state.createOfferError = error;
        },
        setNewTitle(state, title) {
            state.newOffer.title = title;
        },
        setNewDescription(state, desc) {
            state.newOffer.description = desc;
        },
        setNewWage(state, wage) {
            state.newOffer.wage = wage;
        },
        setNewSpaces(state, spaces) {
            state.newOffer.spaces = spaces;
        },
        setNewStartDate(state, startDate) {
            state.newOffer.start.date = startDate;
        },
        setNewStartTime(state, startTime) {
            state.newOffer.start.time = startTime;
        },
        setNewEndDate(state, endDate) {
            state.newOffer.end.date = endDate;
        },
        setNewEndTime(state, endTime) {
            state.newOffer.end.time = endTime;
        }
    }
}