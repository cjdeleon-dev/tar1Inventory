function showExportModal() {
    $('#myExportModal').modal('show');
}

function closeExportModal() {
    $('#myExportModal').modal('hide');
}

function loadreturnedheaders() {
    $('#txtConsumerName').hide();
    document.body.style.cursor = 'progress';
    $('#modalLoading').modal('show');
    document.getElementById("spintext").innerHTML = "LOADING...";

    $.ajax({
        type: 'GET',
        url: "/Return/GetAllReturnedStockHeaders",
        mimeType: 'json',
        success: function (data) {
            if (data != null) {
                $('#tblRetunedHeaders').DataTable({
                    "data": data.data,
                    "bLengthChange": false,
                    "pageLength": 15,
                    "columns": [
                        { "data": "Id", "autoWidth": true },
                        { "data": "ReturnedDate", "autoWidth": true },
                        { "data": "ReturnedBy", "autoWidth": true },
                        { "data": "Remarks", "autoWidth": true }
                    ],
                    order: [[0, "desc"]],
                    "initComplete": function (settings, json) {
                        let table = $('#tblRetunedHeaders').DataTable();

                        $('#tblRetunedHeaders tbody').on('click', 'tr', function () {

                            var data = table.row(this).data();

                            getAllReturnedMaterialDetails(data["Id"])
                        });

                        document.body.style.cursor = 'default';
                        $('#modalLoading').modal('hide');

                    },
                    "aoColumnDefs": [
                        {
                            "aTargets": [4],
                            "mData": "Id",
                            "mRender": function (data, type, full) {

                                return '<button class="btn btn-primary" style="font-size:smaller;" href="#" id="vw_' + data + '" ' +
                                    'onclick="printPreview(\'' + data + '\')">' +
                                    '<i class="glyphicon glyphicon-print"></i></button> ';
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

function getAllReturnedMaterialDetails(hdrid) {
    if (hdrid > 0) {

        $.ajax({
            url: "/Return/GetAllReturnedStockDetailsById?headerid=" + hdrid,
            type: "GET",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {

                var table = $('#tblRetunedDetails').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();
                if (data != null) {
                    $('#tblRetunedDetails').DataTable({
                        "data": data.data,
                        "bLengthChange": false,
                        "pageLength": 50,
                        "columns": [
                            { "data": "MCTNo", "autoWidth": true },
                            { "data": "IsSalvage", "autoWidth": true },
                            { "data": "Material", "autoWidth": true },
                            { "data": "SerialNo", "autoWidth": true },
                            { "data": "RateAmount", "autoWidth": true },
                            { "data": "Quantity", "autoWidth": true },
                            { "data": "TotalAmount", "autoWidth": true },
                        ]
                    });
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

        return true;
    }
}

function printPreview(rptid) {
    var parent = $('embed#mctpdf').parent();
    var newElement = '<embed src="/Charge/MCTReportView?rptid=' + parseInt(rptid) + '"  width="100%" height="800" type="application/pdf" id="mctpdf">';

    $('embed#mctpdf').remove();
    parent.append(newElement);

    //var toolbar = document.querySelector('#print');
    //console.log(toolbar);

    $('#myRptModal').modal('show');
}

function showNewRecordModal() {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    $('#ReturnedDate').val(today);

    $('#myModal').modal('show');

    initUserLoggedName();

    loadMaterialTypes();
    //loadYearValues();
    loadEmployeesAsReturnedBy();
}

function initUserLoggedName() {
    $.ajax({
        url: "/Return/GetLoggedUserName/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var loggedname = result.FirstName + ' ' + result.MiddleInitial + ' ' + result.LastName;
            $('#AcceptedBy').val(loggedname);
            $('#AcceptedById').val(result.Id);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadMaterialTypes() {
    $.ajax({
        url: "/Return/GetAllMaterialTypes/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var data = result.data;

            $('#cboMaterialType').empty();
            $('#cboMaterialType').append("<option value=0>SELECT TYPE</option>");
            for (var i = 0; i < data.length; i++) {
                var name = data[i].Description;
                var opt = new Option(name, data[i].Id);
                $('#cboMaterialType').append(opt);
            }
            $('#cboMaterialType').append("<option value=1000>OTHERS</option>");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function cboMaterialTypeOnChange() {
    var mtid = $('#cboMaterialType').val();

    if (mtid != 0) {
        $.ajax({
            url: "/Return/GetAllMaterialsByMaterialTypeId?id=" + mtid,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {

                var data = result.data;

                $('#cboMaterial').empty();
                $('#cboMaterial').append("<option value=0>SELECT MATERIAL</option>");
                for (var i = 0; i < data.length; i++) {
                    var name = data[i].Material;
                    var opt = new Option(name, data[i].Id);
                    $('#cboMaterial').append(opt);
                }

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function loadYearValues() {
    $.ajax({
        url: "/Return/GetAllYearValues/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var data = result.data;

            $('#cboYearValues').empty();
            $('#cboYearValues').append("<option value=0>SELECT YEAR CODE</option>");
            for (var i = 0; i < data.length; i++) {
                var name = data[i].Code;
                var opt = new Option(name, data[i].Id);
                $('#cboYearValues').append(opt);
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadEmployeesAsReturnedBy() {
    $.ajax({
        url: "/Return/GetEmployeesAsReturnedBy/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var data = result.data;

            $('#cboReturnedBy').empty();
            $('#cboReturnedBy').append("<option value=0>SELECT EMPLOYEE</option>");
            for (var i = 0; i < data.length; i++) {
                var name = data[i].Name;
                var opt = new Option(name, data[i].Id);
                $('#cboReturnedBy').append(opt);
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function cboMaterialOnChange(){
    loadYearValues();
}

function cboYearCodeOnChange() {
    getRateBySelectedYearCodeAndMaterialId();
}

function getRateBySelectedYearCodeAndMaterialId() {
    var matid = $('#cboMaterial').val();
    var yrcode = $('#cboYearValues').val();

    $.ajax({
        url: "/Return/GetRateByMaterialIdAndYearCode?matid=" + matid + "&yearcode=" + yrcode,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var data = result.data;

            $('#RateAmount').val(data);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function onChangeCheckBox() {
    if ($('#chkIsConsumer').is(":checked")) {
        $('#cboReturnedBy').hide();
        $('#txtConsumerName').show();
        $('#cboReturnedBy').val(0);
    } else {
        $('#cboReturnedBy').show();
        $('#txtConsumerName').hide();
        $('#txtConsumerName').val("");
    }
}

function addMaterialItem() {

    var html = '';
    html += '<tr style="background-color: white;">';
    html += '<td>' + $('#MCTNo').val() + '</td>';
    if ($('#chkIsSalvage').is(":checked"))
        html += '<td class="text-center"><input type="checkbox" checked disabled=disabled /></td>';
    else
        html += '<td class="text-center"><input type="checkbox" disabled=disabled /></td>';
    html += '<td style="display:none;">' + $('#cboMaterial').val() + '</td>';
    html += '<td>' + $("#cboMaterial option:selected").html() + '</td>';
    html += '<td style="display:none;">' + $('#cboYearValues').val() + '</td>';
    html += '<td>' + $("#cboYearValues option:selected").html() + '</td>';
    html += '<td style="text-align:center;" contenteditable="true">' + $('#SerialNo').val() + '</td>';
    html += '<td style="text-align:center;">' + $('#RateAmount').val() + '</td>';
    html += '<td style="text-align:center;">' + $('#Quantity').val() + '</td>';
    html += '<td style="text-align:center;">' + $('#TotalAmount').val() + '</td>';
    html += '<td class="text-center"><button class="btn btn-danger" onclick="deleteMaterialRow(this)"><i class="glyphicon glyphicon-trash"></i></button></td>';
    html += '</tr>';
    $('.tbodydetail').append(html);

    $('#Quantity').val("");
    $('#TotalAmount').val("");
}

function computeTAmount() {
    var rateamt = parseFloat($('#RateAmount').val());
    var qty = parseInt($('#Quantity').val());

    $('#TotalAmount').val(rateamt * qty);
}

function deleteMaterialRow(r) {
    var i = r.parentNode.parentNode.rowIndex;
    document.getElementById('tblMDetails').deleteRow(i);
}

function clearControls() {
    $('#cboReturnedBy').empty();
    $('#Remarks').val("");
    $('#MCTNo').val("");
    $('#RateAmount').val("");
    $('#Quantity').val("");
    $('#TotalAmount').val("");
    $('#SerialNo').val("");
    $('#cboMaterial').empty();
    $('#cboYearValues').empty();

    $('.tbodydetail').empty();
}

function saveReturnedHeader() {
    var cnt = $('#tblMDetails tbody tr').length;
    var isconsumer = false;
    var returnedbyid = 0;
    var returnedBy = "";
    var remarks = $('#Remarks').val();


    if ($('#chkIsConsumer').is(":checked"))
        isconsumer = true;

    //check if returned by field is supplied.
    if (isconsumer) {
        returnedBy = $('#txtConsumerName').val();
    } else {
        if ($('#cboReturnedBy').val() != 0) {
            returnedBy = $("#cboReturnedBy option:selected").html();
            returnedbyid = $("#cboReturnedBy").val();
        } else {
            returnedBy = "";
        }
    }
    if (returnedBy == "") {
        alert("Returned By is missing.");
        return;
    }

    //check if remarks field is supplied.
    if (remarks == "") {
        alert("Remarks are missing.");
        return;
    }

    //check if have row(s) in table
    if (cnt == 0) {
        alert("No Details.");
        return;
    }

    //save header first.
    var objData = {
        Id: 0,
        ReturnedDate: $('#ReturnedDate').val(),
        IsConsumer: isconsumer,
        ReturnedById: returnedbyid == 0 ? null: returnedbyid,
        ReturnedBy: returnedBy,
        CreatedById: 0,
        CreatedBy: "",
        Remarks: remarks,
        EntryDate: ""
    }

    if (objData != null) {
        $.ajax({
            type: "POST",
            url: "/Return/AddReturnedStockHeader/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(objData),
            dataType: "json",
            success: function (response) {
                console.log(response.data);
                if (response.data==1) {
                    $.ajax({
                        url: "/Return/GetLoggedUserMaxRCHId/",
                        type: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            console.log('header id: ' + result.data);
                            if (parseInt(result.data) > 0) {
                                //add returneddetails
                                addReturnedDetails(result.data);
                            }
                        },
                        error: function (errormessage) {
                            alert(errormessage.responseText);
                        }
                    });
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//FOR TESTING PURPOSES ONLY. IT MUST BE COMMENTED.
//function saveReturnedDetails() {
//    addReturnedDetails(1);
//}

function addReturnedDetails(headerid) {
    //kunin number of rows in table
    var cnt = $('#tblMDetails tbody tr').length;
    //kunin lahat ng rows
    var tblrows = $('#tblMDetails tbody tr');

    var arrayData = new Array();

    //dito iyon magloloop per item (details)
    for (var i = 0; i < cnt; i++) {

        var $tds = tblrows[i].cells;

        var mct = $tds[0].innerText;
        var mctno = "";
        var issalvage = false;
        var serialno = "";

        if ($('#tblMDetails tbody tr:eq(' + i + ')').find('td:eq(1) input').is(':checked'))
            issalvage = true;

        if (mct != "")
            mctno = parseInt(mct);
        else
            mctno = null;

        if ($tds[6].innerText != null)
            serialno = $tds[6].innerText;
        
        var detailobj = {
            Id: 0,
            MCRTNo: headerid,
            MCTNo: mctno,
            IsSalvage: issalvage,
            MaterialId: $tds[2].innerText,
            Material: "",
            Stock: $tds[3].innerText,
            YearId: $tds[4].innerText,
            YearsValue: $tds[5].innerText,
            SerialNo: serialno,
            RateAmount: $tds[7].innerText,
            Quantity: $tds[8].innerText
        };

        arrayData.push(detailobj);
    }

    $.ajax({
        url: "/Return/AddReturnedStockDetails/",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(arrayData),
        dataType: "json",
        success: function (result) {
            console.log(result.data);
            if (result.data == 1) {
                $('#myModal').modal('hide');
                window.location = "/Return/ReturnStockMaterials";
            }
            else {
                alert('An error occured.');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//onkeypress events------------------------------------------------------------------
function isNumber(evt) {
    var ch = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(ch))) {
        evt.preventDefault();
    }
    if (evt.keyCode === 13) {
        evt.preventDefault();
        addMaterialItem();
    }
}
//-----------------------------------------------------------------------------------