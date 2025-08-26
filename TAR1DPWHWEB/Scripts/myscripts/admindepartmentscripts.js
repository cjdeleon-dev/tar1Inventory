function clearAllTextBoxes() {
    $('#Id').val("");
    $('#Code').val("");
    $('#Description').val("");
    //this is to change modal title dynamically//////////
    $('#myModal').on('show.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.modal-title').text("Adding New Department");
    });
    ////////////////////////////////////////////////////
}

function addDepartment() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var dept = {
        Id: parseInt(0),
        Code: $('#Code').val().trim(),
        Description: $('#Description').val(),
    };

    if (dept != null) {
        $.ajax({
            type: "POST",
            url: "/Departments/AddDepartment/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "dept": dept }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Departments/DepartmentList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    
}

function getbyID(deptid) {


    $('#Id').css('border-color', 'lightgrey');
    $('#Code').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Departments/GetDepartmentByID/" + deptid,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsError) {
                $('#Id').val(deptid);
                $('#Code').val(result.Code);
                $('#Description').val(result.Description);

                ///this is to change modal title dynamically//////////
                $('#myModal').on('show.bs.modal', function (event) {
                    var modal = $(this);
                    modal.find('.modal-title').text("Edit Department Details");
                });
                //////////////////////////////////////////////////
                $('#myModal').modal('show');
                $('#btnUpdateDepartment').show();
                $('#btnAddDepartment').hide();
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

function updateDepartment() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var dept = {
        Id: $('#Id').val(),
        Code: $('#Code').val().trim(),
        Description: $('#Description').val(),
    };

    if (dept != null) {
        $.ajax({
            type: "POST",
            url: "/Departments/UpdateDepartment/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ "dept": dept }),
            dataType: "json",
            success: function (response) {
                $('#myModal').modal('hide');
                alert(response.ProcessMessage);
                window.location = "/Departments/DepartmentList";
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function removeDepartmentById(id) {
    var ans = confirm("Are you sure want to delete the selected department?");
    if (ans) {
        $.ajax({
            url: "/Departments/RemoveDepartment/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert(result.ProcessMessage);
                window.location = "/Departments/DepartmentList";
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