import store from '../store/index';

async function get(specUrl) {
    return httpRequest('GET', specUrl);
}

async function del(specUrl) {
    return httpRequest('DELETE', specUrl);
}

async function post(specUrl, body) {
    return httpRequest('POST', specUrl, body)
}

async function put(specUrl, body) {
    return httpRequest('PUT', specUrl, body);
}

// general HTTP request
async function httpRequest(method, specUrl, body=null)
{
    store.commit('setLoading', true);

    var stringBody = null;
    if (body) {
        stringBody = JSON.stringify(body, replacer)
    }

    var response = await fetch(store.state.baseApiUrl + specUrl, {
        method: method,
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + store.state.accessToken
        },
        body: stringBody,
    })
    .catch(() => {
        raiseError({error: 'Vyskytla se chyba', detail: null});
    })
    .finally(() => {
        store.commit('setLoading', false);
    });

    switch (response.status) {
        case 200: case 201: 
            return response.json();           
        case 204:
            return null;
        case 401:
            store.dispatch("logoutStore");
            raiseError({error: 'Neautorizovaný požadavek', detail: null});
            break;
        case 403:
            raiseError({error: 'Nedostatečná oprávnění', detail: null});
            break
        case 404:
            raiseError({error: 'Položka nenalazena', detail: null});
            break
        default:
            var error = await response.json();
            raiseError(error);
            break;
    }
}

function raiseError(errObj) {
    store.commit('setErrorMsg', errObj);
    throw new Error();
}

function replacer(key, value) {
    switch (key) {
        case "wage": case "spaces":
            return +value;  
        default:
            return value;
    }
}

export default {
    get, post, put, del
}