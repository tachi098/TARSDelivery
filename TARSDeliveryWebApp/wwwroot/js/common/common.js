/** Uri API Services */
const uriServices = 'http://localhost:50354/api/Services';
const uriPackages = 'http://localhost:50354/api/Packages';
const uriBills = 'http://localhost:50354/api/Bills';
const uriAccounts = 'http://localhost:50354/api/Account';
const uriPriceList = 'http://localhost:50354/api/PriceList';
const uriBranch = 'http://localhost:50354/api/Branch';
const uriComment = "http://localhost:50354/api/Comment";

function round(number, precision) {
    var shift = function (number, exponent) {
        var numArray = ("" + number).split("e");
        return +(numArray[0] + "e" + (numArray[1] ? (+numArray[1] + exponent) : exponent));
    };
    return shift(Math.round(shift(number, +precision)), -precision);
}


function getAddressPackage(addressPackage) {
    return addressPackage.split(/[\s0-9,]/).join('').substr(0, 3).toUpperCase().normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '')
        .replace(/đ/g, 'd').replace(/Đ/g, 'D')
}

function getTitlePackage(addressFrom, addressTo, typePackage='P', methodPackage='V') {
    let titlePackage = '';

    titlePackage += getAddressPackage(addressFrom);
    titlePackage += getAddressPackage(addressTo);
    titlePackage += (new Date().getTime() / 1000 | 0);
    titlePackage += typePackage;
    titlePackage += methodPackage;

    return titlePackage;
}
