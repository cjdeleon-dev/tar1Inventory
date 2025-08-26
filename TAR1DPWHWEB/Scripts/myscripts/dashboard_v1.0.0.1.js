function emptyUploadFile() {
    $('#ImageFile').val("");
}

function deletePhoto(id) {
    var ans = confirm("Are you sure want to delete your primary photo?");
    if (ans) {
        $.ajax({
            url: "/Account/DeletePhoto/" + id,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                alert("Successfully deleted.");
                $('#img1').attr('src', window.location.protocol + "//" + window.location.host + "/Content/images/userdefaultjpg.jpg");
                $('#dphoto').hide();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function reseedNow() {
    $.ajax({
        url: "/Account/ReseedNow/",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert(result.ProcessMessage);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}