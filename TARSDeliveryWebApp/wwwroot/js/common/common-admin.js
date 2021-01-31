$(document).ready(function () {

    $('#table-datatables').DataTable();
    $('#table-datatables-clone').DataTable();

    if ($('#origin').val() !== undefined && $('#destination').val() !== undefined) {
        // Add input listeners
        google.maps.event.addDomListener(window, 'load', function () {
            setDestination();
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
    }


});
