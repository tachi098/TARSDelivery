$(function () {

    $('#table-datatables tbody .deleteAccount').each(function (i) {
        $(this).on('click', function (e) {
            e.preventDefault();

            const accountId = $(this).attr('data-accountId');

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

                    processDelete(uriAccounts, 'DeleteAccount', + accountId);

                    setTimeout(() => {
                        window.location.href = "/Admin/Account";
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
