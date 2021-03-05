$(function () {

    try {
        document.getElementById("download").addEventListener("click", () => {
            const invoice = document.getElementById("invoice");
            console.log(invoice);
            console.log(window);
            var opt = {
                margin: 1,
                filename: 'ReportPDF.pdf',
                //image: { type: 'jpeg', quality: 0.98 },
                //html2canvas: { scale: 1 },
                //jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
            };
            html2pdf().from(invoice).set(opt).save();
        });
    } catch (e) {
    }

    //$('a.btn.btn-outline-danger.delete').each(function (i) {
    //    $(this).on('click', function (e) {
    //        e.preventDefault();

    //        const packageId = $(this).attr('data-packageId');
    //        const billId = $(this).attr('data-billId');

    //        console.log(packageId, ' - ', billId);
            
    //        swal({
    //            title: "Are you sure?",
    //            icon: "warning",
    //            buttons: true,
    //            dangerMode: true,
    //        }).then((willDelete) => {
    //            if (willDelete) {
    //                swal("You Are Deleted!", {
    //                    icon: "success",
    //                    buttons: false,
    //                });

    //                processDelete(uriBills, 'DeleteBill', +billId);
    //                processDelete(uriPackages, 'DeletePackage', +packageId);

    //                setTimeout(() => {
    //                    window.location.href = "/Admin/Bills";
    //                }, 1000);
    //            }
    //        });
    //    });
    //});

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

    $('#weightsFlowMethodCreate').keyup(function (e) {
        if (['-', '+', 'e', 'E'].includes(e.key)) {
            e.preventDefault();
        }

        let weight = $(this).val();
        let namePrice = $('#selected-pricelist-create').val();
        console.log(weight, ' - ', namePrice);

        processPriceWeight(uriServices, weight, 'Create', namePrice);
    });

    
    if (+$('#weightsFlowMethodCreate').val() > 0) {
        let weight = $('#weightsFlowMethodCreate').val();
        processPriceWeight(uriServices, weight, 'Create', 'Normal');
    }

    //$('#selected-weight-create').change(function () {
    //    let weight = $(this).val();
    //    let namePrice = $('#selected-pricelist-create').val();

    //    //console.log('namePrice: ', namePrice, ' - weight: ', weight);

    //    processPriceWeight(uriServices, weight, 'Create', namePrice);
    //});

    $('#selected-pricelist-create').change(function () {
        let weight = $('#weightsFlowMethodCreate').val();
        let namePrice = $(this).val();

        //console.log('namePrice: ', namePrice, ' - weight: ', weight);

        processPriceWeight(uriServices, weight, 'Create', namePrice);
    });
    // ================================== CREATE
    /** Start: Remove -, +, e, E */
    const invalidChars = [
        "-",
        "+",
        "e",
        "E"
    ];

    $('#weightsFlowMethod').keyup(function (e) {
        if (['-', '+', 'e', 'E'].includes(e.key)) {
            e.preventDefault();
        }

        let weight = $(this).val();
        let namePrice = $('#selected-pricelist').val();
        processPriceWeight(uriServices, weight, '', namePrice);
    });

    /** End: Remove -, +, e, E */

    //$('#selected-weight').change(function () {
    //    let weight = $(this).val();
    //    let namePrice = $('#selected-pricelist').val();
    //    processPriceWeight(uriServices, weight, '', namePrice);
    //});

    $('#selected-pricelist').change(function () {
        let weight = $('#weightsFlowMethod').val();
        let namePrice = $(this).val();

        //console.log('namePrice: ', namePrice, ' - weight: ', weight);

        processPriceWeight(uriServices, weight, '', namePrice);
    });

    // Weight to price
    function processPriceWeight(uri, weight, keyCreate, namePriceList) {
        if (keyCreate === 'Create') {
            $.ajax({
                url: `${uri}/GetPriceList/${namePriceList}`,
                type: 'GET',
                success: function (response) {
                    let price = weight / 1000 * response.priceWeight;
                    $('#total-price-create').val(round(price, 2));
                    $('#price-weight-create').text('$' + round(price, 2));
                },
                error: function () {
                    console.log('Error API');
                }
            });
        } else {
            $.ajax({
                url: `${uri}/GetPriceList/${namePriceList}`,
                type: 'GET',
                success: function (response) {
                    let price = weight / 1000 * response.priceWeight;
                    $('#total-price').val(round(price, 2));
                    $('#price-weight').text('$' + round(price, 2));
                },
                error: function () {
                    console.log('Error API');
                }
            });
        }
        
    }

});
