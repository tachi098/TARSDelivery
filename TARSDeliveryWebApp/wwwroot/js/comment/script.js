$(function () {

    $('#table-datatables tbody .deleteComment').each(function (i) {
        $(this).on('click', function (e) {
            e.preventDefault();

            const commentId = $(this).attr('data-commentId');

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

                    processDelete(uriComment, 'DeleteComment', + commentId);

                    setTimeout(() => {
                        window.location.href = "/Admin/Comment";
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



