import store from '../store';

async function get(specUrl) {
    var response = await fetch(store.state.baseApiUrl + specUrl, {
        method: "GET",
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + store.state.authentication.accessToken
        }
    })
    .catch(() => {
        throw new Error('Vyskytla se chyba');
    });

    if (response.status >= 200 && response.status <= 299 ) {
        return response.json();
    }
    else {
        var errorMsg = await response.text();
        throw new Error(errorMsg);
    }
}

async function deleteMehod(specUrl) {
    var response = await fetch(store.state.baseApiUrl + specUrl, {
        method: "DELETE",
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + store.state.authentication.accessToken
        }
    })
    .catch(() => {
        throw new Error('Vyskytla se chyba');
    });

    if (response.status >= 200 && response.status <= 299 ) {
        return response.json();
    }
    else {
        var errorMsg = await response.text();
        throw new Error(errorMsg);
    }
}

async function post(specUrl, body) {
    var response = await fetch(store.state.baseApiUrl + specUrl, {
        method: "POST",
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + store.state.authentication.accessToken
        },
        body: JSON.stringify(body, replacer),
    })
    .catch(() => {
        throw new Error('Vyskytla se chyba');
    });

    if (response.status >= 200 && response.status <= 299 ) {
        return response.json();
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
    get, post, deleteMehod
}