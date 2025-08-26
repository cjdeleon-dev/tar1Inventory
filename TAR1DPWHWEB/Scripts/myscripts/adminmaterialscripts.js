function loadProcess() {
    $('#myTable1').DataTable({
        "ajax": {
            "url": "/Materials/loadfordata",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Material", "autoWidth": true },
            { "data": "Description", "autoWidth": true },
            { "data": "OnHand", "autoWidth": true }
        ]
    });
}


function clearAllTextBoxes() {
    $('#Id').val("");
    $('#Material').val("");
    $('#Description').val("");
    //this is to change modal title dynamically//////////
    $('#myModal').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-title').text("Adding New Material");
    });
    ////////////////////////////////////////////////////
}

function addMaterial() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var material = {
        Id: parseInt(0),
        Material: $('#Material').val().trim(),
        Description: $('#Description').val(),
    };

    if (material != null) {
        $.ajax({
            type: "POST",
            url: "/Materials/AddMaterial/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "material": material }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Materials/MaterialList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    
}

function getbyID(materialid) {


    $('#Id').css('border-color', 'lightgrey');
    $('#Material').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Materials/GetMaterialByID/" + materialid,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsError) {
                $('#Id').val(materialid);
                $('#Material').val(result.Material);
                $('#Description').val(result.Description);

                ///this is to change modal title dynamically//////////
                $('#myModal').on('show.bs.modal', function (event) {
                    var modal = $(this);
                    modal.find('.modal-title').text("Edit Material Details");
                });
                //////////////////////////////////////////////////
                $('#myModal').modal('show');
                $('#btnUpdateMaterial').show();
                $('#btnAddMaterial').hide();
            } else {
                alert(result.ProcessMessage);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function updateMaterial() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }

//    var material = {
//        Id: $('#Id').val(),
//        Material: $('#Material').val().trim(),
//        Description: $('#Description').val(),
//    };

//    if (material != null) {
//        $.ajax({
//            type: "POST",
//            url: "/Materials/UpdateMaterial/",
//            contentType: 'application/json; charset=UTF-8',
//            data: JSON.stringify({ "material": material }),
//            dataType: "json",
//            success: function (response) {
//                $('#myModal').modal('hide');
//                alert(response.ProcessMessage);
//                window.location = "/Materials/MaterialList";
//            },
//            error: function (errormessage) {
//                alert(errormessage.responseText);
//            }
//        });
//    }
//}

//function removeMaterialById(id) {
//    var ans = confirm("Are you sure want to delete the selected material?");
//    if (ans) {
//        $.ajax({
//            url: "/Materials/RemoveMaterial/" + id,
//            type: "POST",
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                alert(result.ProcessMessage);
//                window.location = "/Materials/MaterialList";
//            },
//            error: function (errormessage) {
//                alert(errormessage.responseText);
//            }
//        });
//    }
//}

function validate() {
    var isValid = true;

    if ($('#Material').val().trim() == "") {
        $('#Material').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Material').css('border-color', 'lightgrey');
    }
    if ($('#Description').val().trim() == "") {
        $('#Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    
    return isValid;
}