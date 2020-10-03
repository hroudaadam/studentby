// format date
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

// format date to ISO 
function dateToIsoString(date, time) {
    var stringDate = date + 'T' + time;
    var isoDate = new Date(stringDate);
    return isoDate.toISOString();
}

// format date of birth
function dateOfBirthToString(isoDate) {
    var date = new Date(isoDate);
    var strDate =
        date.getDate() + '. ' +
        (date.getMonth() + 1) + '. ' +
        date.getFullYear() + ' ';
    return strDate;
}

// format address
function addressToString(address) {
    var output = address.city + ', ' + address.street + ' ' +
        address.number;
    return output;
}

export default {
    dateToString,
    dateToIsoString,
    dateOfBirthToString,
    addressToString
}