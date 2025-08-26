function clearAllTextBoxes() {
    $('#Id').val("");
    $('#Code').val("");
    $('#Description').val("");
    //this is to change modal title dynamically//////////
    $('#myModal').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-title').text("Adding New Unit");
    });
    ////////////////////////////////////////////////////
}

function addUnit() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var unit = {
        Id: parseInt(0),
        Code: $('#Code').val().trim(),
        Description: $('#Description').val(),
    };

    if (unit != null) {
        $.ajax({
            type: "POST",
            url: "/Units/AddUnit/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "unit": unit }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Units/UnitList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    
}

function getbyID(unitid) {


    $('#Id').css('border-color', 'lightgrey');
    $('#Code').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Units/GetUnitByID/" + unitid,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsError) {
                $('#Id').val(unitid);
                $('#Code').val(result.Code);
                $('#Description').val(result.Description);

                ///this is to change modal title dynamically//////////
                $('#myModal').on('show.bs.modal', function (event) {
                    var modal = $(this);
                    modal.find('.modal-title').text("Edit Unit Details");
                });
                //////////////////////////////////////////////////
                $('#myModal').modal('show');
                $('#btnUpdateUnit').show();
                $('#btnAddUnit').hide();
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

function updateUnit() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var unit = {
        Id: $('#Id').val(),
        Code: $('#Code').val().trim(),
        Description: $('#Description').val(),
    };

    if (unit != null) {
        $.ajax({
            type: "POST",
            url: "/Units/UpdateUnit/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "unit": unit }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Units/UnitList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function removeUnitById(id) {
    var ans = confirm("Are you sure want to delete the selected unit?");
    if (ans) {
        $.ajax({
            url: "/Units/RemoveUnit/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result.ProcessMessage);
                window.location = "/Units/UnitList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function validate() {
    var isValid = true;

    if ($('#Code').val().trim() == "") {
        $('#Code').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Code').css('border-color', 'lightgrey');
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