function dateToString(isoDate) {
    var date = new Date(isoDate);
    var strDate = 
        date.getDate() + '.' +
        date.getMonth() + '.' +
        date.getFullYear() + ' ' +
        date.getHours() + ':' +
        (date.getMinutes() < 10 ? '0':'') + date.getMinutes();
    
    return strDate;
}

export default {
    dateToString
}