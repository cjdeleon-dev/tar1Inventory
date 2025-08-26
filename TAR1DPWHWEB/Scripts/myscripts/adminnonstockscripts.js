function clearAllTextBoxes() {
    $('#Id').val("");
    $('#Material').val("");
    $('#Description').val("");
    //this is to change modal title dynamically//////////
    $('#myModal').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-title').text("Adding Non-Stock Material");
    });
    ////////////////////////////////////////////////////
}

function addNonStock() {
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
            url: "/Materials/AddNonStock/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "material": material }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Materials/NonStockList";
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
        url: "/Materials/GetNonStockByID/" + materialid,
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
                    modal.find('.modal-title').text("Edit Non-Stock Details");
                });
                //////////////////////////////////////////////////
                $('#myModal').modal('show');
                $('#btnUpdateNonStock').show();
                $('#btnAddNonStock').hide();
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

function updateNonStock() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var material = {
        Id: $('#Id').val(),
        Material: $('#Material').val().trim(),
        Description: $('#Description').val(),
    };

    if (material != null) {
        $.ajax({
            type: "POST",
            url: "/Materials/UpdateNonStock/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "material": material }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Materials/NonStockList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function removeNonStockById(id) {
    var ans = confirm("Are you sure want to delete the selected Non-Stock Material?");
    if (ans) {
        $.ajax({
            url: "/Materials/RemoveNonStock/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result.ProcessMessage);
                window.location = "/Materials/NonStockList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

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