


function exportmct() {

    let dateparam;
    
    var fromdate = new Date($('#dtpFrom').val());
    var todate = new Date($('#dtpTo').val());

    var mmfrom = fromdate.getMonth() + 1;
    var dayfrom = fromdate.getDate();
    var yyfrom = fromdate.getFullYear();

    var mmto = todate.getMonth() + 1;
    var dayto = todate.getDate();
    var yyto = todate.getFullYear();

    if (isNaN(fromdate) || isNaN(todate)) {
        alert('Invalid Date');
        document.body.style.cursor = 'default';
        //waitingDialog.hide();
    } else {
        if (fromdate > todate) {
            alert('Invalid date range.');
        } else {
            dateparam = mmfrom + '/' + dayfrom + '/' + yyfrom + '_' + mmto + '/' + dayto + '/' + yyto;
        }

        var objdata = {
            paramDateStr: dateparam
        };

        //document.body.style.cursor = 'progress';
        //waitingDialog.show('Please wait...');

        if (objdata != null) {
            $.ajax({
                type: "POST",
                url: "/Charge/FetchDateByDateRange/",
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify(objdata),
                success: function (data) {
                    document.body.style.cursor = 'default';
                    //waitingDialog.hide();
                    JSONToCSVConvertor(data, dateparam.replace("///g",""), true);
                },
                error: function (errormessage) {
                    document.body.style.cursor = 'default';
                    //waitingDialog.hide();
                    alert(errormessage.responseText);
                }
            });
        }
    }
    
}

//var waitingDialog = waitingDialog || (function ($) {
//    'use strict';

//    // Creating modal dialog's DOM
//    var $dialog = $(
//        '<div class="modal fade bd-example-modal-sm" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
//        '<div class="modal-dialog modal-sm">' +
//        '<div class="modal-content">' +
//        '<div class="modal-header"><h5 style="margin:0;"></h5></div>' +
//        '<div class="modal-body">' +
//        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
//        '</div>' +
//        '</div></div></div>');

//    return {
//        /**
//         * Opens our dialog
//         * @param message Custom message
//         * @param options Custom options:
//         * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
//         * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
//         */
//        show: function (message, options) {
//            // Assigning defaults
//            if (typeof options === 'undefined') {
//                options = {};
//            }
//            if (typeof message === 'undefined') {
//                message = 'Loading';
//            }
//            var settings = $.extend({
//                dialogSize: 'sm',
//                progressType: '',
//                onHide: null // This callback runs after the dialog was hidden
//            }, options);

//            // Configuring dialog
//            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
//            $dialog.find('.progress-bar').attr('class', 'progress-bar');
//            if (settings.progressType) {
//                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
//            }
//            $dialog.find('h5').text(message);
//            // Adding callbacks
//            if (typeof settings.onHide === 'function') {
//                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
//                    settings.onHide.call($dialog);
//                });
//            }
//            // Opening dialog
//            $dialog.modal();
//        },
//        /**
//         * Closes dialog
//         */
//        hide: function () {
//            $dialog.modal('hide');
//        }
//    };

//})(jQuery);

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    //CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            //Now convert each value to string and comma-seprated
            row += index + ',';
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            row += '"' + arrData[i][index] + '",';
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "MCT_";
    fileName += ReportTitle;

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}



function clearRows() {
    $('#myTable1 tbody tr').each(function () {
        $(this).removeClass("selected-item");
    });
}

function initDefaultFields() {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    $('#PostedDate').val(today);

    //initialize user logged name..
    initUserLoggedName();

    //initialize dropdown lists..
    loadcboJOWOMOs();
    loadcboMaterials();
    loadcboReceivedBy();
    loadcboSig1();
    loadcboSig2();
    loadcboSig3();
    loadcboSig4();

    
    $('#ReceivedBy').show();
    $('#ConsumerReceivedBy').hide();

    $('#ReceivedById').val(0);
    $('#IssuedById').val(332); //default Jerricko J. Tulabot
    $('#NotedById').val(317); //default Rodolfo R. Tal Placido
    $('#AuditedById').val(5); //default Melanie A. Abogado
    $('#CheckedById').val(212) //default GUBAC, IVY MARIE D.  

    $('#Project').val("");
    $('#ProjectAddress').val("");
    $('#JOWOMO').val(0);
    $('#JOWOMONumber').val("");

    $('#Material').val(0);
    $('#Unit').val("");
    $('#Quantity').val("");
    $('#SerialNo').val("");

    $('.tbodydetail').empty();

    $('#myModal').modal("show");
}

function initUserLoggedName() {
    $.ajax({
        url: "/Charge/GetLoggedUserName/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var loggedname = result.FirstName + ' ' + result.MiddleInitial + ' ' + result.LastName;
            $('#PostedBy').val(loggedname);
            $('#PostedById').val(result.Id);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboMaterials() {
    $.ajax({
        url: "/Charge/GetMaterials/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Material').empty();
            $('#Material').val(0);
            $('#Material').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var Desc = result[i].Material + " - " + result[i].Description
                var opt = new Option(Desc, result[i].Id);
                $('#Material').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboJOWOMOs() {
    $.ajax({
        url: "/Charge/GetJOWOMOs/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#JOWOMO').empty();
            $('#JOWOMO').val(0);
            $('#JOWOMO').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option((result[i].Code + " - " + result[i].Description), result[i].Id);
                $('#JOWOMO').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboReceivedBy() {
    $.ajax({
        url: "/Charge/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ReceivedBy').empty();
            $('#ReceivedBy').val(0);
            $('#ReceivedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#ReceivedBy').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig1() {
    $.ajax({
        url: "/Charge/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#IssuedBy').empty();
            $('#IssuedBy').val(0);
            $('#IssuedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#IssuedBy').append(opt);
            }
            $('#IssuedBy option[value=332]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig2() {
    $.ajax({
        url: "/Charge/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#NotedBy').empty();
            $('#NotedBy').val(0);
            $('#NotedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#NotedBy').append(opt);
            }
            $('#NotedBy option[value=317]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig3() {
    $.ajax({
        url: "/Charge/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#AuditedBy').empty();
            $('#AuditedBy').val(0);
            $('#AuditedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#AuditedBy').append(opt);
            }
            $('#AuditedBy option[value=5]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig4() {
    $.ajax({
        url: "/Receive/GetEmployees/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CheckedBy').empty();
            $('#CheckedBy').val(0);
            $('#CheckedBy').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var name = result[i].Name;
                var opt = new Option(name, result[i].Id);
                $('#CheckedBy').append(opt);
            }
            $('#CheckedBy option[value=212]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function cboMaterialOnChange() {
    var matid = $('#Material').val();
    $('#MaterialId').val(matid);
    //get unitid, unit, and onhand
    $.ajax({
        url: "/Charge/GetUnitAndOnHandByMaterialId/",
        data: "matid=" + parseInt(matid),
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var unitonhand = result;
            var splittedvalues = unitonhand.split("#");
            var unitid = splittedvalues[0];
            var unitcode = splittedvalues[1];
            var onhand = splittedvalues[2];
            $('#UnitId').val(unitid);
            $('#Unit').val(unitcode);
            $('#OnHand').val(onhand);

            var selMaterial = $("#Material option:selected").html();
            var mat = selMaterial.split('-');

            if (mat[0].trim().toUpperCase().substring(0, 5) == "KWH M" || mat[0].trim().toUpperCase().substring(0, 5) == "TRANS"
                || mat[0].trim().toUpperCase().substring(0, 4) == "C.T." || mat[0].trim().toUpperCase().substring(0, 4) == "P.T."
                || mat[0].trim().toUpperCase().substring(0, 5) == "CAPAC") {
                document.myform.txtserialno.focus();
            } else {
                document.myform.txtquantity.focus();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function cboJOWOMOOnChange() {
    var jwmid = $('#JOWOMO').val();
    $('#JOWOMOId').val(jwmid);
}

function cboRecOnChange() {
    var recbyid = $('#ReceivedBy').val();
    $('#ReceivedById').val(recbyid);
}

function cboSig1OnChange() {
    var sigid1 = $('#IssuedBy').val();
    $('#IssuedById').val(sigid1);
}

function cboSig2OnChange() {
    var sigid2 = $('#NotedBy').val();
    $('#NotedById').val(sigid2);
}

function cboSig3OnChange() {
    var sigid3 = $('#AuditedBy').val();
    $('#AuditedById').val(sigid3);
}

function cboSig4OnChange() {
    var sigid4 = $('#CheckedBy').val();
    $('#CheckedById').val(sigid4);
}

function addMaterialItem() {
    //for validation-----------------------------------------------------------
    if ($('#Project').val().trim() == "") {
        alert('Project field is required.');
        return false;
    }
    if ($('#ProjectAddress').val().trim() == "") {
        alert('Project Address field is required.');
        return false;
    }
    if (parseInt($('#JOWOMO').val()) == 0) {
        alert('Please select job order/work order/maintenance order.');
        return false;
    }
    if ($('#JOWOMONumber').val().trim() == "") {
        alert('Job Order/Work Order/Maintenance Order Number is required.');
        return false;
    }
    if (parseInt($('#Material').val()) == 0) {
        alert('Please select Material.');
        return false;
    }
    if (parseInt($('#Quantity').val()) == 0 || $('#Quantity').val() == "") {
        alert('Quantity should be greater than zero (0).');
        return false;
    }
    if (parseInt($('#Quantity').val()) > 0) {
        //check if quantity is less than or equal to onhand.
        if (parseInt($('#Quantity').val()) > parseInt($('#OnHand').val())) {
            alert('Quantity should be less than or equal to OnHand value.');
            return false;
        }
    }
    //end of validation---------------------------------------------------------------
    
    var selMaterial = $("#Material option:selected").html();
    var selUnit = $("#Unit").val();
    var selectedJOWOMO = $("#JOWOMO option:selected").html();
    var selJOWOMO = selectedJOWOMO.split('-');
    var JOWOMOCode = selJOWOMO[0];
    

    var html = '';
    html += '<tr style="background-color: white;">';
    html += '<td style="display:none;">' + $('#MaterialId').val() + '</td>';
    html += '<td>' + selMaterial + '</td>';
    html += '<td style="display:none;">' + $('#UnitId').val() + '</td>';
    html += '<td>' + selUnit + '</td>';
    html += '<td style="text-align:center;">' + $('#Quantity').val() + '</td>';
    html += '<td style="text-align:center;" contenteditable="true">' + $('#SerialNo').val() + '</td>';
    html += '<td style="text-align:center;display:none;">' + $('#JOWOMO').val() + '</td>';
    html += '<td style="text-align:center;">' + JOWOMOCode + '</td>';
    html += '<td style="text-align:center;">' + $('#JOWOMONumber').val() + '</td>';
    html += '<td class="text-center"><button class="btn btn-danger" onclick="deleteMaterialRow(this)"><i class="glyphicon glyphicon-trash"></i></button></td>';
    html += '</tr>';
    $('.tbodydetail').append(html);

    //reset required fields after adding the selected material
    var mat = selMaterial.split('-');

    $('#SerialNo').val("");
    $('#Quantity').val("");

    if (mat[0].trim().toUpperCase().substring(0, 5) != "KWH M" && mat[0].trim().toUpperCase().substring(0, 5) != "TRANS") {
        document.myform.cbomaterial.focus();
    } else {
        document.myform.txtserialno.focus();
    }

}

function addChargedMaterials() {

    var tsek;
    if ($('#isConsumer').is(":checked")) {
        tsek = true;
    } else {
        tsek = false;
    }

    if ($('#tblMDetails tbody tr').length == 0) {
        alert("Invalid Entry. Please fill all required fields.");
        return false;
    }

    if (tsek) {
        if ($('#ConsumerReceivedBy').val().trim() == "") {
            alert("Invalid Received By.");
            return false;
        }
    } else {
        if ($('#ReceivedById').val() == 0) {
            alert("Invalid Selected Received By.");
            return false;
        }
    }
    
    if ($('#Project').val().trim() == "") {
        alert("Project Field is required.");
        return false;
    }

    if ($('#ProjectAddress').val().trim() == "") {
        alert("Project Address Field is required.");
        return false;
    }

    if ($('#JOWOMOId').val() == 0) {
        alert("Invalid Selected JO/WO/MO.");
        return false;
    }

    if ($('#JOWOMONumber').val().trim() == "") {
        alert("JO/WO/MO Number Field is required.");
        return false;
    }

    var objdata = {
        Id: parseInt(0),
        PostedDate: $('#PostedDate').val(),
        PostedById: $('#PostedById').val(),
        IssuedById: $('#IssuedById').val(),
        IsConsumerReceived: tsek,
        ReceivedById: $('#ReceivedById').val(),
        ConsumerReceivedBy: $('#ConsumerReceivedBy').val(),
        CheckedById: $('#CheckedById').val(),
        AuditedById: $('#AuditedById').val(),
        NotedById: $('#NotedById').val(),
        Project: $('#Project').val(),
        ProjectAddress: $('#ProjectAddress').val(),
        JOWOMOId: $('#JOWOMOId').val(),
        JOWOMONumber: $('#JOWOMONumber').val()
    };

    if (objdata != null) {
        $.ajax({
            type: "POST",
            url: "/Charge/AddChargedHeader/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(objdata),
            dataType: "json",
            success: function (response) {
                if (!response.IsError) {
                    $.ajax({
                        url: "/Charge/GetLoggedUserMaxCMId/",
                        type: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            if (parseInt(result.Id) > 0) {

                                addChargedMaterialDetails(result.Id);
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

function addChargedMaterialDetails(id) {

    //kunin number of rows in table
    var cnt = $('#tblMDetails tbody tr').length;
    //kunin lahat ng rows
    var tblrows = $('#tblMDetails tbody tr');

    var arrayData = new Array();

    //dito iyon magloloop per item (details)
    for (var i = 0; i < cnt; i++) {

        var $tds = tblrows[i].cells;

        var detailobj = {
            ChargeMaterialHeaderId: id,
            MaterialId: parseInt($tds[0].innerText),
            Quantity: parseInt($tds[4].innerText),
            UnitId: parseInt($tds[2].innerText),
            SerialNo: $tds[5].innerText,
            JOWOMOId: parseInt($tds[6].innerText),
            JOWOMONumber: $tds[8].innerText
        };

        arrayData.push(detailobj);
    }

    $.ajax({
        url: "/Charge/AddChargedMaterialDetail/",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(arrayData),
        dataType: "json",
        success: function (result) {
            //alert(result.ProcessMessage);
            $('#myModal').modal('hide');
            window.location = "/Charge/ChargedMaterials";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getAllChargedMaterialDetail(chargemathdrid) {
    if (chargemathdrid > 0) {

        $.ajax({
            url: "/Charge/GetChargedMaterialDetailByHeaderId?rmdm=" + chargemathdrid,
            type: "GET",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {

                var table = $('#myTable2').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();
                if (data != null) {
                    $('#myTable2').DataTable({
                        "data": data.data,
                        "bLengthChange": false,
                        "pageLength": 50,
                        "columns": [
                            { "data": "Material", "autoWidth": true },
                            { "data": "Quantity", "autoWidth": true },
                            { "data": "Unit", "autoWidth": true },
                            { "data": "SerialNo", "autoWidth": true }
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

function onChangeCheckBox() {
    if ($('#isConsumer').is(":checked")) {
        $('#ReceivedBy').hide();
        $('#ConsumerReceivedBy').show();
    } else {
        $('#ReceivedBy').show();
        $('#ConsumerReceivedBy').hide();
    }
}

function txtSerialOnKeyPress(evt) {
    if (evt.keyCode === 13) {
        evt.preventDefault();
        document.myform.txtquantity.focus();
    }
}

function deleteMaterialRow(r) {
    var i = r.parentNode.parentNode.rowIndex;
    document.getElementById('tblMDetails').deleteRow(i);
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

//String.prototype.padRight = function (l, c) {
//    return this + Array(l - this.length + 1).join(c || " ");
//}




function loadAllMCTHeaders() {
    
    document.body.style.cursor = 'progress';
    $('#modalLoading').modal('show');
    document.getElementById("spintext").innerHTML = "LOADING...";

    $.ajax({
        type: 'GET',
        url: "/Charge/GetAllMCTHeaders",
        mimeType: 'json',
        success: function (data) {
            if (data != null) {
                $('#myTable1').DataTable({
                    "data": data.data,
                    "bLengthChange": false,
                    "pageLength": 15,
                    "columns": [
                        { "data": "Id", "autoWidth": true },
                        { "data": "PostedDate", "autoWidth": true },
                        { "data": "PostedBy", "autoWidth": true },
                        { "data": "Project", "autoWidth": true },
                        { "data": "JOWOMOCode", "autoWidth": true },
                        { "data": "JOWOMONumber", "autoWidth": true },
                        { "data": "ReceivedBy", "autoWidth": true }
                    ],
                    order: [[0, "desc"]],
                    "initComplete": function (settings, json) {
                        let table = $('#myTable1').DataTable();

                        $('#myTable1 tbody').on('click', 'tr', function () {

                            var data = table.row(this).data();

                            getAllChargedMaterialDetail(data["Id"])
                        });

                        document.body.style.cursor = 'default';
                        $('#modalLoading').modal('hide');

                    },
                    "aoColumnDefs": [
                        {
                            "aTargets": [7],
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

function showExportModal() {
    $('#myExportModal').modal('show');
    $('#tblExport').hide();
}


function fetchdata() {

    var dateFr = $('#dtpDateFr').val();
    var dateTo = $('#dtpDateTo').val();

    if (dateFr != "" && dateTo != "") {
        //check if valid date range.
        if (dateFr < dateTo) {
            //fetch

            var table = $('#tblExport').DataTable();

            //clear datatable
            table.clear().draw();

            //destroy datatable
            table.destroy();

            $('#tblExport').hide();

            document.body.style.cursor = 'progress';
            $('#modalLoading').modal('show');
            document.getElementById("spintext").innerHTML = "LOADING...";

            var fileName = "MCT_" + dateFr + "_" + dateTo;

            $.ajax({
                type: "GET",
                url: "/Charge/FetchDateByDateRange?datefr=" + dateFr + "&dateto=" + dateTo,
                contentType: 'application/json; charset=UTF-8',
                success: function (data) {
                    if (data != null) {
                        $('#tblExport').show();

                        $('#tblExport').DataTable({
                            "data": data.data,
                            "pageLength": 5,
                            "dom": "Bfrtip",
                            "bUseRendered": false,
                            "buttons": [
                                {
                                    extend: "excel",
                                    filename: fileName,
                                    text: "EXPORT TO EXCEL",
                                }
                            ],
                            "initComplete": function (settings, json) {
                                document.body.style.cursor = 'default';
                                $('#modalLoading').modal('hide');
                            },
                            "columns": [
                                { "data": "MaterialId", "autoWidth": true },
                                { "data": "StockName", "autoWidth": true },
                                { "data": "StockDescription", "autoWidth": true },
                                { "data": "SerialNo", "autoWidth": true },
                                { "data": "PostedDate", "autoWidth": true },
                                { "data": "MCTNo", "autoWidth": true },
                                { "data": "Unit", "autoWidth": true },
                                { "data": "Quantity", "autoWidth": true },
                                { "data": "UnitCost", "autoWidth": true },
                                { "data": "TotalCost", "autoWidth": true },
                                { "data": "WOCode", "autoWidth": true },
                                { "data": "WOAccount", "autoWidth": true },
                                { "data": "WONumber", "autoWidth": true },
                                { "data": "Project", "autoWidth": true },
                                { "data": "ProjectAddress", "autoWidth": true },
                            ],
                            order: [[4,'desc']]
                        });
                    } else {
                        document.body.style.cursor = 'default';
                        $('#modalLoading').modal('hide');
                        alert("No records to display");
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });

        } else {
            alert("Invalid Date Range.");
            return false;
        }
    }
}

function closeExportModal() {
    $('#dtpDateFr').val("");
    $('#dtpDateTo').val("");

    $('#tblExport').hide();
    var table = $('#tblExport').DataTable();

    //clear datatable
    table.clear().draw();

    //destroy datatable
    table.destroy();

}