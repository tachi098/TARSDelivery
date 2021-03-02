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

    // Get TitlePackage
    function getTitlePackageView(typePackage, methodPackage) {

        let addressFrom = $('#origin').val();
        let addressTo = $('#destination').val();
        let methodCreate = $('#selected-pricelist-create').val();
        let methodUpdate = $('#selected-pricelist').val();
        let type = $('#Type').val();

        

        if (methodCreate !== undefined) {
            methodPackage = methodCreate[0];
            typePackage = type[0];
            $('#Title').val(getTitlePackage(addressFrom, addressTo, typePackage, methodPackage));
        }

        if (methodUpdate !== undefined) {
            methodPackage = methodUpdate[0];
            typePackage = type[0];
            $('#Title').val(getTitlePackage(addressFrom, addressTo, typePackage, methodPackage));
        }

        
    }
    getTitlePackageView();

    $('#Type').change(() => {
        let type = $('#Type').val();

        if (type === 'Package') {
            getTitlePackageView('P');
        } else {
            getTitlePackageView('M');
        }
    });

    $('#selected-pricelist-create').change(() => {
        let method = $('#selected-pricelist-create').val();
        let type = $('#Type').val();

        getTitlePackageView(type[0], method[0]);
    });

    $('#selected-pricelist').change(() => {
        let method = $('#selected-pricelist').val();
        let type = $('#Type').val();

        getTitlePackageView(type[0], method[0]);
    });


    // Check button view information
    $('#origin').keyup(() => {
        let methodPackageCreate = $('#selected-pricelist-create').val();
        let methodPackageUpdate = $('#selected-pricelist').val();
        let typePackage = $('#Type').val();

        if (methodPackageCreate !== undefined) {
            getTitlePackageView(methodPackageCreate[0], typePackage[0]);
        }

        if (methodPackageUpdate !== undefined) {
            getTitlePackageView(methodPackageUpdate[0], typePackage[0]);
        }
    });

    // Check button view information
    $('#destination').keyup(() => {
        let methodPackageCreate = $('#selected-pricelist-create').val();
        let methodPackageUpdate = $('#selected-pricelist').val();
        let typePackage = $('#Type').val();

        if (methodPackageCreate !== undefined) {
            getTitlePackageView(methodPackageCreate[0], typePackage[0]);
        }

        if (methodPackageUpdate !== undefined) {
            getTitlePackageView(methodPackageUpdate[0], typePackage[0]);
        }
    });
});
