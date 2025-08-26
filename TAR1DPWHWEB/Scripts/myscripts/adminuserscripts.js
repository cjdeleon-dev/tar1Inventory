function clearAllTextBoxes() {
    $('#Id').val("");
    $('#FirstName').val("");
    $('#MiddleInitial').val("");
    $('#LastName').val("");
    $('#Address').val("");
    $('#PositionId').val(0);
    $('#Position').val(0);
    $('#RoleId').val(0);
    $('#Role').val(0);
    $('#UserName').val("");
    $('#Password').val("");

    $('#sepline').show();
    $('#lbluname').show();
    $('#lblpass').show();
    $('#lblrepass').show();
    $('#lblIsActive').hide();
    $('#isActive').hide();
    $('#UserName').show();
    $('#Password').show();

    getAllPositions();
    getAllRoles();

    //this is to change modal title dynamically//////////
    $('#myModal').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-title').text("Adding New User");
    });
    ////////////////////////////////////////////////////
}

function addUser() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var user = {
        Id: parseInt(0),
        FirstName: $('#FirstName').val().trim(),
        MiddleInitial: $('#MiddleInitial').val(),
        LastName: $('#LastName').val(),
        Address: $('#Address').val(),
        PositionId: $('#PositionId').val(),
        RoleId: $('#RoleId').val(),
        UserName: $('#UserName').val(),
        Password: $('#Password').val()
    };

    if (user != null) {
        $.ajax({
            type: "POST",
            url: "/Users/AddUser/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "user": user }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Users/UserList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    
}

function getbyID(userid) {
    $('#Id').css('border-color', 'lightgrey');
    $('#FirstName').css('border-color', 'lightgrey');
    $('#MiddleInitial').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $('#PositionId').css('border-color', 'lightgrey');
    $('#Position').css('border-color', 'lightgrey');
    $('#RoleId').css('border-color', 'lightgrey');
    $('#Role').css('border-color', 'lightgrey');
    $('#UserName').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');

    getAllPositions();
    getAllRoles();

    $.ajax({
        url: "/Users/GetUserByID/" + userid,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsError) {
                //I just put the positionid in a variable coz sometimes the combobox doesn't manifest its display value according to a selected value.
                //var postid = result.PositionId;
                //var roleid = result.RoleId;

                $('#Id').val(userid);
                $('#FirstName').val(result.FirstName);
                $('#MiddleInitial').val(result.MiddleInitial);
                $('#LastName').val(result.LastName);
                $('#Address').val(result.Address);
                $('#PositionId').val(result.PositionId);
                $('#Position').val(result.PositionId);
                $('#RoleId').val(result.RoleId);
                $('#Role').val(result.RoleId);
                //Set the checkbox checked properties depends on the result value.
                $('#isActive').prop('checked', result.isActive);
                $('#UserName').val(result.UserName);
                $('#Password').val(result.Password);

                ///this is to change modal title dynamically//////////
                $('#myModal').on('show.bs.modal', function (event) {
                    var modal = $(this);
                    modal.find('.modal-title').text("Edit User Details");
                });
                //////////////////////////////////////////////////
                $('#sepline').hide();
                $('#lbluname').hide();
                $('#lblpass').hide();
                $('#lblrepass').hide();
                $('#lblIsActive').show();
                $('#isActive').show();
                $('#UserName').hide();
                $('#Password').hide();

                $('#myModal').modal('show');
                $('#btnUpdateUser').show();
                $('#btnAddUser').hide();
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

function updateUser() {
    var res = validate();
    if (res == false) {
        return false;
    }

    //get the checked value of checkbox
    var tsek;
    if ($('#isActive').is(":checked")) {
        tsek = true;
    } else {
        tsek = false;
    }

    var user = {
        Id: $('#Id').val(),
        FirstName: $('#FirstName').val().trim(),
        MiddleInitial: $('#MiddleInitial').val(),
        LastName: $('#LastName').val(),
        Address: $('#Address').val(),
        PositionId: $('#PositionId').val(),
        RoleId: $('#RoleId').val(),
        isActive: tsek
    };

    if (user != null) {
        $.ajax({
            type: "POST",
            url: "/Users/UpdateUser/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "user": user }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Users/UserList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function deactivateUserById(id) {
    var ans = confirm("Are you sure want to deactivate the selected user?");
    if (ans) {
        $.ajax({
            url: "/Users/DeactivateUserById/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result.ProcessMessage);
                window.location = "/Users/UserList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function getAllPositions() {
    $.ajax({
        url: "/Users/GetAllPositions/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Position').empty();
            $('#Position').val(0);
            $('#Position').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option(result[i].Description, result[i].Id);
                $('#Position').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


function getAllRoles() {
    $.ajax({
        url: "/Users/GetAllRoles/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Role').empty();
            $('#Role').val(0);
            $('#Role').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option(result[i].Role, result[i].Id);
                $('#Role').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function cboPostOnChange() {
    var pid = $('#Position').val();
    $('#PositionId').val(pid);
}

function cboRoleOnChange() {
    var pid = $('#Role').val();
    $('#RoleId').val(pid);
}

function validate() {
    var isValid = true;

    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }
    if ($('#MiddleInitial').val().trim() == "") {
        $('#MiddleInitial').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MiddleInitial').css('border-color', 'lightgrey');
    }
    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }
    if ($('#Address').val().trim() == "") {
        $('#Address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Address').css('border-color', 'lightgrey');
    }
    if ($('#Position').val().trim() == "") {
        $('#Position').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Position').css('border-color', 'lightgrey');
    }
    if ($('#UserName').val().trim() == "") {
        $('#UserName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserName').css('border-color', 'lightgrey');
    }
    if ($('#Password').val().trim() == "") {
        $('#Password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    return isValid;
}