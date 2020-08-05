async function httpGet() {
    var response = await fetch("https://jsonplaceholder.typicode.com/todos/1");
    return response;
}

export default {
    httpGet
}