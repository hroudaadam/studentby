import store from '../store';

function get(specUrl) {
    var response = fetch(store.state.baseApiUrl + specUrl, {
        method: "GET",
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + store.state.authentication.accessToken
        }
    });
    return response;
}

function post(specUrl, body) {
    var response = fetch(store.state.baseApiUrl + specUrl, {
        method: "POST",
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + store.state.authentication.accessToken
        },
        body: JSON.stringify(body),
    });
    return response;
}

export default {
    get, post
}