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
        throw new Error('Vyskytla se chyba');
    });

    if (response.status == 200 || response.status == 201 ) {
        return response.json();
    }
    else if (response.status == 204) {
        return response.text();
    }
    else if (response.status == 401) {
        store.dispatch('logoutStore');
        throw new Error("Neautorizovaný požadavek");
    }
    else if (response.status == 403) {
        throw new Error("Nedostatečná oprávnění");
    }
    else {
        var errorMsg = await response.text();
        throw new Error(errorMsg);
    }
}

function replacer(key, value) {
    switch (key) {
        case "wage": case "spaces":
            return +value  
        default:
            return value
    }
}

export default {
    get, post, put, del
}