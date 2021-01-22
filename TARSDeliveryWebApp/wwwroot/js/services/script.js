$(function () {

    var origin, destination, map;
    var directionsDisplay = new google.maps.DirectionsRenderer({
        'draggable': false
    });
    var directionsService = new google.maps.DirectionsService();
    var travel_mode = 'DRIVING';

    // Init render
    $('#result > .list-group').css('display', 'none');
    $('#view-information').prop('disabled', true);

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
            }
        } catch (error) {
            console.log(error);
            $('#view-information').prop('disabled', true);
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
            } else {
                originClone = response.originAddresses[0];
                destinationClone = response.destinationAddresses[0];
                if (response.rows[0].elements[0].status === "ZERO_RESULTS") {
                    $('#result > .msg').html("There is no way between " + origin +
                        " and " + destination);
                    $('#result > .list-group').css('display', 'none');
                    $('#view-information').prop('disabled', true);
                    return;
                } else {
                    $('#result > .list-group').css('display', 'block');
                    $('#result > .msg').html('');

                    var distance = response.rows[0].elements[0].distance;
                    var duration = response.rows[0].elements[0].duration;
                    var distance_in_kilo = distance.value / 1000; // the kilom
                    var distance_in_mile = distance.value / 1609.34; // the mile
                    var duration_text = duration.text;
                    var duration_value = duration.value;
                    $('#in_mile').text(distance_in_mile.toFixed(2));
                    $('#in_kilo').text(distance_in_kilo.toFixed(2));
                    $('#duration_text').text(duration_text);
                    $('#duration_value').text(duration_value);
                    $('#from').text(originClone);
                    $('#to').text(destinationClone);

                    $('#view-information').prop('disabled', false);
                }
            }
        } catch (error) {
            console.log("Error: calculateDistance");
            $('#view-information').prop('disabled', true);
        }
    }

    // Print results on submit the form
    $('#distance_form').submit(function (e) {
        e.preventDefault();

        origin = $('#origin').val();
        destination = $('#destination').val();

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
        }
    });

    // Check button view information
    $('#destination').keyup(() => {
        if ($('#destination').val().trim().length === 0) {
            $('#view-information').prop('disabled', true);
        }
    });

    // View information
    $('#view-information').click(function (e) {
        e.preventDefault();
        $('#origin').prop('disabled', true);
        $('#destination').prop('disabled', true);

        // Delete class d-none
        $('#show-information').removeClass('d-none');
        $('#reset-information').removeClass('d-none');

        // Add class d-none
        $('#view-information').addClass('d-none');
    });

    // Reset information
    $('#reset-information').click(function (e) {
        e.preventDefault();
        $('#origin').prop('disabled', false);
        $('#destination').prop('disabled', false);

        // Delete class d-none
        $('#view-information').removeClass('d-none');

        // Add class d-none
        $('#reset-information').addClass('d-none');
        $('#show-information').addClass('d-none');
    });

});
