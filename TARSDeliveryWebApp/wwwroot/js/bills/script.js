$(function () {

    $('#table-datatables tbody .delete').each(function (i) {
        $(this).on('click', function (e) {
            e.preventDefault();

            const packageId = $(this).attr('data-packageId');
            const billId = $(this).attr('data-billId');
            
            swal({
                title: "Are you sure?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    swal("You Are Deleted!", {
                        icon: "success",
                        buttons: false,
                    });

                    processDelete(uriBills, 'DeleteBill', +billId);
                    processDelete(uriPackages, 'DeletePackage', +packageId);

                    setTimeout(() => {
                        window.location.reload();
                    }, 500);
                }
            });
        });
    });

    // Process Delete
    function processDelete(uri, apiName, id) {
        $.ajax({
            type: 'PUT',
            url: `${uri}/${apiName}/${id}`,
            success: function (response) {
                console.log(response);
            },
            error: function () {
                console.log('Error API');
            }
        });
    }

    if ($('#selected-weight-create').val() !== undefined) {
        let weight = $('#selected-weight-create').val();
        processPriceWeight(uriServices, weight, 'Create');
    }

    $('#selected-weight-create').change(function () {
        let weight = $(this).val();
        processPriceWeight(uriServices, weight, 'Create');
    });

    $('#selected-weight').change(function () {
        let weight = $(this).val();
        processPriceWeight(uriServices, weight);
    });

    // Weight to price
    function processPriceWeight(uri, weight, keyCreate) {
        if (keyCreate === 'Create') {
            $.ajax({
                url: `${uri}/GetPriceList/Normal`,
                type: 'GET',
                success: function (response) {
                    let price = weight * response.priceWeight;
                    $('#total-price-create').val(Math.round(price * 100 + 0.5) / 100);
                    $('#price-weight-create').text('$' + Math.round(price * 100 + 0.5) / 100);
                },
                error: function () {
                    console.log('Error API');
                }
            });
        } else {
            $.ajax({
                url: `${uri}/GetPriceList/Normal`,
                type: 'GET',
                success: function (response) {
                    let price = weight * response.priceWeight;
                    $('#total-price').val(Math.round(price * 100 + 0.5) / 100);
                    $('#price-weight').text('$' + Math.round(price * 100 + 0.5) / 100);
                },
                error: function () {
                    console.log('Error API');
                }
            });
        }
        
    }

});
