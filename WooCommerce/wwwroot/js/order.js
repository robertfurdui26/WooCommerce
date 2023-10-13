﻿var dataTable;

$(document).ready(function () {
    var url = new URL(window.location.href);
    var status = url.searchParams.get("status");

    // Set a default status if it's not provided in the URL
    status = status || "all";

    loadDataTable(status);

});
   

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },

            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/order/details?orderId=${data}" class="btn btn-dark mx-2"> <i class="bi bi-pencil-square"></i> </a>               
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}



