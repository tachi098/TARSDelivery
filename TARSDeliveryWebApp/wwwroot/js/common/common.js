/** Uri API Services */
const uriServices = 'http://localhost:50354/api/Services';
const uriPackages = 'http://localhost:50354/api/Packages';
const uriBills = 'http://localhost:50354/api/Bills';
const uriAccounts = 'http://localhost:50354/api/Account';
const uriPriceList = 'http://localhost:50354/api/PriceList';

function round(number, precision) {
    var shift = function (number, exponent) {
        var numArray = ("" + number).split("e");
        return +(numArray[0] + "e" + (numArray[1] ? (+numArray[1] + exponent) : exponent));
    };
    return shift(Math.round(shift(number, +precision)), -precision);
}

