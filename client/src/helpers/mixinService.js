// returns ISO string format of datetime
function getDateIsoString(date, time) {
    var stringDate = date + 'T' + time;
    var isoDate = new Date(stringDate);
    return isoDate.toISOString();
}

// returns string format of address
function getAddressString(address) {
    var output = address.city + ', ' + address.street + ' ' +
        address.number;
    return output;
}

// returns string format of date of birth
function getDateOfBirthString(birthDateIso) {
    var birthdate = new Date(birthDateIso);
    return getDateString(birthdate)
}

// returns string format of jobs timerange
function getJobTimeRangeString(startDateIso, endDateIso) {
    var startDate = new Date(startDateIso);
    var endDate = new Date(endDateIso);

    // short format
    if (startDate.getDate() == endDate.getDate()) {
        return getDateString(startDate) + ' ' +  getTimeString(startDate) + " - " + getTimeString(endDate);
    }
    // long format
    else {
        return getDateString(startDate) + ' ' +  getTimeString(startDate) + " - " +  getDateString(endDate) + ' ' + getTimeString(endDate);
    }
}

// returns string format of date
function getDateString(date) {
    var strDate = date.getDate() + '.' +
        (date.getMonth() + 1) + '.' +
        date.getFullYear();
    return strDate;
}

// returns string format of time
function getTimeString(date) {
    var strTime = date.getHours() + ':' +
        (date.getMinutes() < 10 ? '0' : '') + date.getMinutes();
    return strTime;
}

export default {
    getDateIsoString,
    getAddressString,    
    getDateOfBirthString,
    getJobTimeRangeString
}