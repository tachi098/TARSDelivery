$(function () {

    $('#table-datatables tbody .deletePriceList').each(function (i) {
        $(this).on('click', function (e) {
            e.preventDefault();

            const priceListId = $(this).attr('data-priceListId');

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

                    processDelete(uriPriceList, 'DeletePriceList', + priceListId);

                    setTimeout(() => {
                        window.location.href = "/Admin/PriceList";
                    }, 1000);
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
});



