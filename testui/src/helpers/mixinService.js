function dateToString(isoDate) {
    var date = new Date(isoDate);
    var strDate =
        date.getDate() + '.' +
        date.getMonth() + '.' +
        date.getFullYear() + ' ' +
        date.getHours() + ':' +
        (date.getMinutes() < 10 ? '0' : '') + date.getMinutes();

    return strDate;
}

function dateToIsoString(date, time) {
    var stringDate = date + 'T' + time;
    var isoDate = new Date(stringDate);
    return isoDate.toISOString();
}

function dateOfBirthToString(isoDate) {
    var date = new Date(isoDate);
    var strDate =
        date.getDate() + '.' +
        date.getMonth() + '.' +
        date.getFullYear() + ' ';
    return strDate;
}


export default {
    dateToString,
    dateToIsoString,
    dateOfBirthToString
}