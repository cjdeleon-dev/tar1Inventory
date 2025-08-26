function clearAllTextBoxes() {
    $('#Id').val("");
    $('#Code').val("");
    $('#Description').val("");
    $('#Department').val("");

    getAllDepartments();

    //this is to change modal title dynamically//////////
    $('#myModal').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-title').text("Adding New Position");
    });
    ////////////////////////////////////////////////////
}

function addPosition() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var post = {
        Id: parseInt(0),
        Code: $('#Code').val().trim(),
        Description: $('#Description').val(),
        DepartmentId: $('#DepartmentId').val()
    };

    if (post != null) {
        $.ajax({
            type: "POST",
            url: "/Positions/AddPosition/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "post": post }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Positions/PositionList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    
}

function getbyID(postid) {


    $('#Id').css('border-color', 'lightgrey');
    $('#Code').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#Department').css('border-color', 'lightgrey');

    getAllDepartments();

    $.ajax({
        url: "/Positions/GetPositionByID/" + postid,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsError) {
                $('#Id').val(postid);
                $('#Code').val(result.Code);
                $('#Description').val(result.Description);
                $('#DepartmentId').val(result.DepartmentId);
                $('#Department').val(result.DepartmentId);

                ///this is to change modal title dynamically//////////
                $('#myModal').on('show.bs.modal', function (event) {
                    var modal = $(this);
                    modal.find('.modal-title').text("Edit Position Details");
                });
                //////////////////////////////////////////////////
                $('#myModal').modal('show');
                $('#btnUpdatePosition').show();
                $('#btnAddPosition').hide();
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

function updatePosition() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var post = {
        Id: $('#Id').val(),
        Code: $('#Code').val().trim(),
        Description: $('#Description').val(),
        DepartmentId: $('#DepartmentId').val()
    };

    if (post != null) {
        $.ajax({
            type: "POST",
            url: "/Positions/UpdatePosition/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "post": post }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Positions/PositionList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function removePositionById(id) {
    var ans = confirm("Are you sure want to delete the selected position?");
    if (ans) {
        $.ajax({
            url: "/Positions/RemovePosition/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result.ProcessMessage);
                window.location = "/Positions/PositionList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function getAllDepartments() {
    $.ajax({
        url: "/Positions/GetAllDepartments/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Department').empty();
            $('#Department').val(0);
            $('#Department').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option(result[i].Description, result[i].Id);
                $('#Department').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function cboDeptOnChange() {
    var did = $('#Department').val();
    $('#DepartmentId').val(did);
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
    if ($('#Department').val().trim() == "") {
        $('#Department').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Department').css('border-color', 'lightgrey');
    }
    return isValid;
}