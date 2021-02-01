$(function () {

    $('#table-datatables tbody .deleteBranch').each(function (i) {
        $(this).on('click', function (e) {
            e.preventDefault();

            const branchId = $(this).attr('data-branchId');


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

                    processDelete(uriBranch, 'DeleteBranch', branchId);


                    setTimeout(() => {
                        window.location.href = "/Admin/Branch";
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
