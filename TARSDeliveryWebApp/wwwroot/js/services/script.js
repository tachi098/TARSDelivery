$(function () {

    var origin, destination, map, distanceKilo, distancePrice = 0, totalPricePaypal = 0;
    var directionsDisplay = new google.maps.DirectionsRenderer({
        'draggable': false
    });
    var directionsService = new google.maps.DirectionsService();
    var travel_mode = 'DRIVING';

    // Init render
    $('#result > .list-group').css('display', 'none');
    $('#view-information').prop('disabled', true);
    //document.getElementById('details_from').reset();

    // Add input listeners
    google.maps.event.addDomListener(window, 'load', function () {
        setDestination();
        initMap();
    });

    function setDestination() {
        var from_places = new google.maps.places.Autocomplete(document.getElementById('origin'));
        var to_places = new google.maps.places.Autocomplete(document.getElementById('destination'));

        google.maps.event.addListener(from_places, 'place_changed', function () {
            var from_place = from_places.getPlace();
            var from_address = from_place.formatted_address;
            $('#origin').val(from_address);
        });

        google.maps.event.addListener(to_places, 'place_changed', function () {
            var to_place = to_places.getPlace();
            var to_address = to_place.formatted_address;
            $('#destination').val(to_address);
        });

    }

    // init or load map
    function initMap() {

        var myLatLng = {
            lat: 10.786377,
            lng: 106.6641083
        };
        map = new google.maps.Map(document.getElementById('map'), {
            zoom: 16,
            center: myLatLng,
        });

    }

    function displayRoute() {
        // Reset
        initMap();

        return new Promise((resolve, reject) => {
            resolve(displayRouteProcess(callbackDisplayRoute));
        });

    }

    // DisplayRoute process
    function displayRouteProcess(callback) {
        directionsService.route({
            origin: origin,
            destination: destination,
            travelMode: travel_mode,
            avoidTolls: true
        }, callback);
    }

    // Callback displayRoute
    function callbackDisplayRoute(response, status) {
        try {
            if (status === 'OK') {
                directionsDisplay.setMap(map);
                directionsDisplay.setDirections(response);
                $('#view-information').prop('disabled', false);
            } else {
                directionsDisplay.setMap(null);
                directionsDisplay.setDirections(null);
                $('#view-information').prop('disabled', true);
                $('#price-distance').text('');
            }
        } catch (error) {
            console.log(error);
            $('#view-information').prop('disabled', true);
            $('#price-distance').text('');
        }
    }

    // Calculate distance
    function calculateDistance() {
        return new Promise((resolve, reject) => {

            origin = $('#origin').val();
            destination = $('#destination').val();

            if ($('#origin').val().trim().length === 0 || $('#destination').val().trim().length === 0) {
                reject('Error: calculateDistance');
                $('#view-information').prop('disabled', true);
                $('#price-distance').text('');
            } else {
                resolve(serviceProcess(origin, destination, callback));
                $('#view-information').prop('disabled', false);
            }
        });
    }

    // ServiceProcess
    function serviceProcess(originClone, destinationClone, callback) {
        var service = new google.maps.DistanceMatrixService();
        service.getDistanceMatrix({
            origins: [originClone],
            destinations: [destinationClone],
            travelMode: google.maps.TravelMode.DRIVING,
            unitSystem: google.maps.UnitSystem.IMPERIAL, // miles and feet.
            avoidHighways: false,
            avoidTolls: false
        }, callback);
    }

    // Get distance results
    function callback(response, status) {
        try {
            if (status != google.maps.DistanceMatrixStatus.OK) {
                $('#result > .msg').html('Error: status');
                $('#view-information').prop('disabled', true);
                $('#price-distance').text('');
            } else {
                originClone = response.originAddresses[0];
                destinationClone = response.destinationAddresses[0];
                if (response.rows[0].elements[0].status === "ZERO_RESULTS") {
                    $('#result > .msg').html("There is no way between " + origin +
                        " and " + destination);
                    $('#result > .list-group').css('display', 'none');
                    $('#view-information').prop('disabled', true);
                    $('#price-distance').text('');
                    return;
                } else {
                    $('#result > .list-group').css('display', 'block');
                    $('#result > .msg').html('');

                    var distance = response.rows[0].elements[0].distance;
                    var duration = response.rows[0].elements[0].duration;
                    var distance_in_kilo = distance.value / 1000; // the kilom
                    var duration_value = duration.value;
                    distanceKilo = distance_in_kilo;
                    $('#in_kilo').text(distance_in_kilo.toFixed(2));
                    $('#duration_value').text(duration_value);
                    $('#from').text(originClone);
                    $('#to').text(destinationClone);

                    $('#view-information').prop('disabled', false);
                    processPriceDistance(uriServices, distanceKilo);
                }
            }
        } catch (error) {
            console.log("Error: calculateDistance");
            $('#view-information').prop('disabled', true);
            $('#price-distance').text('');
        }
    }

    // Print results on submit the form
    $('#distance_form').submit(function (e) {
        e.preventDefault();

        origin = $('#origin').val();
        destination = $('#destination').val();

        if (Object.is(origin, destination)) {
            $('#view-information').prop('disabled', true);
            $('#price-distance').text('');
            $('#result > .msg').html("Same location ???");
            $('#result > .list-group').css('display', 'none');
            return;
        }

        // Process
        (async () => {
            try {
                await displayRoute();
                await calculateDistance();
            } catch (error) {
                console.log(error);
                $('#view-information').prop('disabled', true);
            }
        })();

    });

    // Check button view information
    $('#origin').keyup(() => {
        if ($('#origin').val().trim().length === 0) {
            $('#view-information').prop('disabled', true);
            $('#price-distance').text('');
        }
    });

    // Check button view information
    $('#destination').keyup(() => {
        if ($('#destination').val().trim().length === 0) {
            $('#view-information').prop('disabled', true);
            $('#price-distance').text('');
        }
    });

    // Get TitlePackage
    function getTitlePackageView(typePackage='P', methodPackage='V') {
        let addressFrom = $('#origin').val();
        let addressTo = $('#destination').val();

        $('#Title').val(getTitlePackage(addressFrom, addressTo, typePackage, methodPackage));
    }


    // View information
    $('#view-information').click(function (e) {
        e.preventDefault();
        $('#origin').prop('disabled', true);
        $('#destination').prop('disabled', true);

        $('#weightsFlowDistance').prop('disabled', true);


        // Delete class d-none
        $('#show-information').removeClass('d-none');
        $('#reset-information').removeClass('d-none');

        // Add class d-none
        $('#view-information').addClass('d-none');

        // Get TitlePackage
        getTitlePackageView();
    });

    $('#Type').change(() => {
        let type = $('#Type').val();

        if (type === 'Package') {
            getTitlePackageView('P');
        } else {
            getTitlePackageView('M');
        }
    });

    // Reset information
    $('#reset-information').click(function (e) {
        e.preventDefault();
        $('#origin').prop('disabled', false);
        $('#destination').prop('disabled', false);

        $('#weightsFlowDistance').prop('disabled', false);

        // Delete class d-none
        $('#view-information').removeClass('d-none');

        // Add class d-none
        $('#reset-information').addClass('d-none');
        $('#show-information').addClass('d-none');

        // Reset details
        document.getElementById('details_from').reset();
        
    });

    /** Start: Remove -, +, e, E */
    const invalidChars = [
        "-",
        "+",
        "e",
        "E"
    ];

    document.getElementById('weightsFlowDistance').addEventListener('keyup', (e) => {
        if (invalidChars.includes(e.key)) {
            e.preventDefault();
        }

        let price = (priceDistance / 100) * $('#weightsFlowDistance').val();
        price += (0.1 * priceDistance) + (0.2 * priceDistance * distanceKilo) + (0.1 * priceDistance);
        distancePrice = price;
        $('#price-distance').text('Price: ' + round(price, 2) + ' USD');
    });
    /** End: Remove -, +, e, E */

    var priceDistance = 0;
    // Distance to price
    function processPriceDistance(uri, distance) {
        $.ajax({
            url: `${uri}/GetPriceList/VPP`,
            type: 'GET',
            success: function (response) {
                // 1gr = price / 100gr

                //let price = distance * response.priceDistance;
                priceDistance = response.priceDistance;
                let price = (priceDistance / 100) * $('#weightsFlowDistance').val();
                price += (0.1 * priceDistance) + (0.2 * priceDistance * distance) + (0.1 * priceDistance);
                distancePrice = price;
                $('#price-distance').text('Price: ' + round(price, 2) + ' USD');

                if (price === 0) {
                    $('#view-information').prop('disabled', true);
                } else {
                    $('#view-information').prop('disabled', false);
                }
            },
            error: function () {
                console.log('Error API');
            }
        });
    }
    // Insert into table
    function processInsert(uri, Status) {
        const Title = $('#Title').val();
        const NameTo = $('#NameTo').val();
        const Email = $('#Email').val();
        const AddressFrom = $('#origin').val();
        const Type = $('#Type').val();
        const ZipCode = $('#ZipCode').val();
        const NameFrom = $('#NameFrom').val();
        const AddressTo = $('#destination').val();
        const Weight = $('#weightsFlowDistance').val();
        const Distance = distanceKilo;
        const Message = $('#Message').val();
        const TotalPrice = round(distancePrice, 2) ?? 0;
        const BranchId = null;
        const AccountId = $('#AccountId').val();
        const PriceListName = 'VPP';



        const model = new Object();
        model.Title = Title;
        model.NameTo = NameTo;
        model.Email = Email;
        model.AddressFrom = AddressFrom;
        model.Type = Type;
        model.ZipCode = ZipCode;
        model.NameFrom = NameFrom;
        model.AddressTo = AddressTo;
        model.Weight = Weight;
        model.Distance = Distance;
        model.Message = Message;
        model.TotalPrice = +TotalPrice;
        model.Status = Status;
        model.PriceListName = PriceListName;
        model.BranchId = BranchId;
        if (AccountId.trim().length > 0) {
            model.AccountId = parseInt(AccountId);
        }

        $.ajax({
            url: `${uri}/CreatePackage`,
            type: 'POST',
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //console.log(response);
                processGetNewPackage(uri);
            },
            error: function () {
                console.log('Error API');
            }
        });
    }

    // Get new package
    function processGetNewPackage(uri) {
        $.ajax({
            url: `${uri}/GetNewPackage`,
            type: 'GET',
            success: function (response) {
                //console.log(response);
                let id = response.id;
                processInsertBill(uri, id);
            },
            error: function () {
                console.log('Error API');
            }
        });
    }

    // Insert into table
    function processInsertBill(uri, packageId) {
        $.ajax({
            url: `${uri}/CreateBill`,
            type: 'POST',
            data: JSON.stringify({
                PackageId: packageId
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //console.log(response);
            },
            error: function () {
                console.log('Error API');
            }
        });
    }

    $('#details_from').submit(function (e) {
        e.preventDefault();

        const Title = $('#Title').val();
        const NameTo = $('#NameTo').val();
        const Email = $('#Email').val();
        const AddressFrom = $('#origin').val();
        const Type = $('#Type').val();
        const ZipCode = $('#ZipCode').val();
        const NameFrom = $('#NameFrom').val();
        const AddressTo = $('#destination').val();
        const Weight = 0;
        const Distance = distanceKilo;
        const Message = $('#Message').val();
        const TotalPrice = round(distancePrice, 2);
        totalPricePaypal = TotalPrice;

        if (Title.trim() !== '' && NameFrom.trim() !== '' && Email.trim() !== '' && NameTo.trim() !== '' && ZipCode.trim() !== '' && Message.trim() !== '') {
            $('#show-information').addClass('d-none');
            $('#paypal-information').removeClass('d-none');
            $('#reset-information').addClass('d-none');

            $('#TitlePaypal').text(Title);
            $('#NameFromPaypal').text(NameFrom);
            $('#EmailPaypal').text(Email);
            $('#NameToPaypal').text(NameTo);
            $('#TypePaypal').text(Type);
            $('#ZipCodePaypal').text(ZipCode);
            $('#MessagePaypal').text(Message);
            $('#total-distance').text(TotalPrice);
        }
    });

    $('#send-package').click(function () {
        window.location.href = "/User/Services";
    });

    // Paypal
    // Render the PayPal button
    paypal.Button.render({

        // Set your environment

        env: 'sandbox', // sandbox | production

        // Specify the style of the button

        style: {
            label: 'paypal',
            size: 'large',    // small | medium | large | responsive
            shape: 'rect',     // pill | rect
            color: 'blue',     // gold | blue | silver | black
            tagline: false
        },

        // PayPal Client IDs - replace with your own
        // Create a PayPal app: https://developer.paypal.com/developer/applications/create

        client: {
            sandbox: 'AeziYRAGktWGm1vfRMAo7aWJvO6oZ79-TQC-1kU24GUjza7ADyE2cyrVOl5mBSwDbLV4zcCOhCPpQHnK',
            production: '<insert production client id>'
        },

        payment: function (data, actions) {
            return actions.payment.create({
                payment: {
                    transactions: [
                        {
                            amount: { total: '' + totalPricePaypal, currency: 'USD' }
                        }
                    ]
                }
            });
        },

        onAuthorize: function (data, actions) {
            return actions.payment.execute().then(function () {
                $('#paypal-button-container').addClass('d-none');
                $("#payment-complete").css("opacity", 0.5);
                $('#send-package').removeClass('d-none');
                processInsert(uriServices, 2);
            });
        },

        onCancel: function (data, actions) {
            processInsert(uriServices, 4);

            setTimeout(() => {
                window.location.href = "/User/Services";
            }, 1000);
            
        }

    }, '#paypal-button-container');

});
