//import router from '../router'
//import store from '../store'
import apiSevice from '../helpers/apiService';

export default {
    namespaced: true,
    state: {
        offers: null,
        offerDetail: null
    },
    actions: {
        getAllOffers({commit}){            
            apiSevice.get('/job')
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    commit("setOffers", data)
                })
                .catch((error) => {
                    console.log(error);
                  });
        },
        getOfferDetails({commit},offerId){ 
            console.log(offerId);           
            apiSevice.get('/job/' + offerId.toString())
                .then((response) => response.json())
                .then((data) => {
                    console.log(data);
                    commit('setOfferDetail', data);
                })
                .catch((error) => {
                    console.log(error);
                  });
        },      
    },
    getters: {
        
    },
    mutations: {
        setOffers(state, offers) {
            state.offers = offers;
        },
        setOfferDetail(state, offer) {
            state.offerDetail = offer;
        }
    }
}