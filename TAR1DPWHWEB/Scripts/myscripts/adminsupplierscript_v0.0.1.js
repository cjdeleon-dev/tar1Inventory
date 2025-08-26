function clearAllTextBoxes() {
    $('#Id').val("");
    $('#Supplier').val("");
    $('#Address').val("");

    $('#btnAddSupplier').html("Save");
}

function loadsuppliers() {
    document.body.style.cursor = 'progress';
    $('#modalLoading').modal('show');
    document.getElementById("spintext").innerHTML = "LOADING...";

    $.ajax({
        type: 'GET',
        url: "/Supplier/GetAllSuppliers",
        mimeType: 'json',
        success: function (data) {
            if (data != null) {
                $('#myTable').DataTable({
                    "data": data.data,
                    "bLengthChange": false,
                    "pageLength": 10,
                    "columns": [
                        { "data": "Id", "autoWidth": true },
                        { "data": "Supplier", "autoWidth": true },
                        { "data": "Address", "autoWidth": true },
                    ],
                    "initComplete": function (settings, json) {
                        
                        document.body.style.cursor = 'default';
                        $('#modalLoading').modal('hide');

                    },
                    "aoColumnDefs": [
                        {
                            "aTargets": [3],
                            "mData": "Id",
                            "mRender": function (data, type, full) {

                                return '<button class="btn btn-primary" style="font-size:smaller;" href="#" id="vw_' + data + '" ' +
                                    'onclick="editdata(\'' + full.Id +'~' + full.Supplier + '~' + full.Address + '\')">' +
                                    '<i class="glyphicon glyphicon-edit"></i></button> ';
                            },
                            "className": "text-center"
                        }
                    ]
                });
            } else {
                document.body.style.cursor = 'default';
                $('#modalLoading').modal('hide');
                alert("No data to be displayed.");
            }

        },
        error: function () {
            //document.body.style.cursor = 'progress';
            //$('#modalLoading').modal('show');
            //document.getElementById("spintext").innerHTML = "LOADING...";
        }
    });
}

function editdata(supplier) {

    clearAllTextBoxes();

    var sup = supplier.split('~');

    $('#Id').val(sup[0]);
    $('#Supplier').val(sup[1]);
    $('#Address').val(sup[2]);

    $('#myModal').modal('show');

    $('#btnAddSupplier').html("Update");
}

function addSupplier() {
    var supp = $('#Supplier').val();
    var add = $('#Address').val();

    if (supp == "") {
        alert('Supplier Name is required.');
        return;
    }
        
    if (add == "") {
        alert('Address is required.');
        return;
    }
        
    objdata = {
        Id: $('#Id').val() == "" ? 0 : $('#Id').val(),
        Supplier: supp,
        Address: add,
        CreateById: 0,
        CreatedDate: "",
        UpdatedById: 0,
        UpdatedBy: "",
    }

    if (objdata != null) {

        if ($('#Id').val() == "") {
            $.ajax({
                type: "POST",
                url: "/Supplier/AddNewSupplier/",
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify(objdata),
                dataType: "json",
                success: function (response) {
                    console.log(response.data);
                    if (response.data == 1) {
                        alert("Succesfully Saved.");
                        window.location = "/Supplier/Suppliers";
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/Supplier/UpdateSupplier/",
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify(objdata),
                dataType: "json",
                success: function (response) {
                    console.log(response.data);
                    if (response.data == 1) {
                        alert("Succesfully Updated.");
                        window.location = "/Supplier/Suppliers";
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
    }
}