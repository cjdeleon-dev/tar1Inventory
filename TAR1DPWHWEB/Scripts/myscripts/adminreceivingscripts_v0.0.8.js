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

    $('#ReceivedDate').val(today);

    //initialize user logged name..
    initUserLoggedName();

    //initialize dropdown lists..
    loadcboMaterials();
    loadcboUnits();
    loadcboSig1();
    loadcboSig2();
    loadcboSig3();
    loadcboSig4();
    loadSuppliers();

    $('#Material').val(0);
    
    //var recby = "TULABOT, JERRICKO J. ";
    //$("#ReceivedBy option:contains(" + recby + ")").attr('selected', 'selected');


    //$('#NotedBy').val("TAL PLACIDO, RODOLFO R."); //default Rodolfo R. Tal Placido
    //$('#AuditedBy').val("ABOGADO, MELANIE A."); //default Melanie A. Abogado

    $('#ReceivedById').val(332); //default Jerricko J. Tulabot
    $('#NotedById').val(128); //default ENGR. RODOLFO R. TAL PLACIDO JR.
    $('#AuditedById').val(5); //default Melanie A. Abogado
    $('#CheckedById').val(212) //default ENGR. DANNY L. MALONZO  

    $('#Unit').val(0);
    $('#Quantity').val("");
    $('#ReceivedTotalCost').val("0.00");
    $('#TotalCost').val("");

    $('.tbodydetail').empty();

    $('#BalQty').val("");
    $('#BalRemark').val("");
}

function initUserLoggedName() {
    $.ajax({
        url: "/Receive/GetLoggedUserName/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var loggedname = result.FirstName + ' ' + result.MiddleInitial + ' ' + result.LastName;
            $('#PreparedBy').val(loggedname);
            $('#PreparedById').val(result.Id);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboMaterials() {
    $.ajax({
        url: "/Receive/GetMaterials/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Material').empty();
            $('#Material').val(0);
            $('#Material').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var Desc = result[i].Material + " - " + result[i].Description;
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

function loadcboUnits() {
    $.ajax({
        url: "/Receive/GetUnits/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Unit').empty();
            $('#Unit').val(0);
            $('#Unit').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var opt = new Option(result[i].Description, result[i].Id);
                $('#Unit').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig1() //RECIEVED BY
{
    $.ajax({
        url: "/Receive/GetEmployees/",
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
            $('#ReceivedBy option[value=332]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig2() //NOTED BY
{
    $.ajax({
        url: "/Receive/GetEmployees/",
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
            $('#NotedBy option[value=128]').attr('selected', 'selected');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function loadcboSig3() //AUDITED BY
{
    $.ajax({
        url: "/Receive/GetEmployees/",
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

function loadcboSig4() //CHECKED BY
{
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

function loadSuppliers() {
    $.ajax({
        url: "/Receive/GetSuppliers/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Supplier').empty();
            $('#Supplier').val(0);
            $('#Supplier').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var supplier = result[i].Supplier;
                var opt = new Option(supplier, result[i].Id);
                $('#Supplier').append(opt);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function cboUnitOnChange() {
    var unitid = $('#Unit').val();
    $('#UnitId').val(unitid);
}

function cboMaterialOnChange() {
    var matid = $('#Material').val();
    $('#MaterialId').val(matid);

    $.ajax({
        url: "/Receive/GetUnitByMatarialId/",
        data: "materialid=" + parseInt(matid),
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Unit').val(result.unitid);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function setCBOUnitById(materialid) {
//    //get unitid to display
//    $.ajax({
//        url: "/Receive/GetUnitByMatarialId",
//        data: "materialid=" + parseInt(materialid),
//        type: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            $('#Unit').val(result.unitid);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

function cboSig1OnChange() {
    var sigid1 = $('#ReceivedBy').val();
    $('#ReceivedById').val(sigid1);
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

function cboSupplierOnChange() {
    var suppid = $('#Supplier').val();
    $('#SupplierId').val(suppid);
}

function addMaterialItem() {

    //for validation-----------------------------------------------------------
    if (parseInt($('#Material').val()) == 0) {
        alert('Please select Material.');
        return false;
    }
    if (parseInt($('#Unit').val()) == 0) {
        alert('Please select Unit.');
        return false;
    }
    if (parseInt($('#Quantity').val()) == 0 || $('#Quantity').val() == '' ) {
        alert('Quantity should be greater than zero (0).');
        return false;
    }
    if (parseFloat($('#TotalCost').val()) == 0 || $('#TotalCost').val() == '') {
        alert('Total Cost should be greater than zero (0).');
        return false;
    }
    //end of validation---------------------------------------------------------------

    //var ttlcost = parseInt($('#Quantity').val()) * parseFloat($('#UnitCost').val());
    var selMaterial = $("#Material option:selected").html();
    var selUnit = $("#Unit option:selected").html();
    var totalcost = $('#TotalCost').val().replace(/,/g, "");
    var invcost = parseFloat(totalcost) / (1.12);
    var vat = parseFloat(invcost) * 0.12;
    var unitcost = parseFloat(invcost) / parseFloat($('#Quantity').val());
    var balqty = 0;

    if ($('#BalQty').val() == '')
        balqty = 0;
    else
        balqty = parseInt($('#BalQty').val());

    var balrem = $('#BalRemark').val();


    var html = '';
    html += '<tr style="background-color: white;">';
    html += '<td style="display:none;">' + $('#MaterialId').val() + '</td>';
    html += '<td>' + selMaterial + '</td>';
    html += '<td style="display:none;">' + $('#Unit').val() + '</td>';
    html += '<td>' + selUnit + '</td>';
    html += '<td style="text-align:center;">' + $('#Quantity').val() + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(unitcost) + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(totalcost) + '</td>';
    html += '<td style="text-align:right;display:none;">' + Intl.NumberFormat().format(invcost) + '</td>';
    html += '<td style="text-align:right;display:none;">' + Intl.NumberFormat().format(vat) + '</td>';
    html += '<td style="text-align:center;">' + balqty + '</td>';
    html += '<td>' + balrem + '</td>';
    html += '</tr>';
    $('.tbodydetail').append(html);

    //reset required fields after adding the selected material
    $('#Material').val(0);
    //$('#Unit').val(0);
    $('#Quantity').val("");
    $('#TotalCost').val("");

    $('#BalQty').val("");
    $('#BalRemark').val("");

    var rttlcost = getReceivedTotalCost();

    $('#ReceivedTotalCost').val(Intl.NumberFormat().format(rttlcost));
}

function getReceivedTotalCost() {
    var ttlcost = 0;

    var table = $(".tbodydetail");
    table.find('tr').each(function (i) {
        var $tds = $(this).find('td');
        var tcost = $tds.eq(6).text().replace(/,/g, "");

        ttlcost = parseFloat(ttlcost) + parseFloat(tcost.replace(/,/g, ""));
        
    });

    return ttlcost;
}

function addReceivingMaterials() {

    var isnonvat = false;

    if (parseFloat($('#ReceivedTotalCost').val().replace(',', '')) == 0) {
        alert("Invalid Entry. Please fill all required fields.");
        //swal({
        //    title: "INVALID!",
        //    text: "Invalid Entry. Please fill all required fields",
        //    type: "warning",
        //    showCancelButton: false,
        //    confirmButtonClass: "btn-warning",
        //    confirmButtonText: "OK",
        //    closeOnConfirm: true
        //});
        return false;
    }

    if (confirm('Is NON-VAT?') == true) {
        isnonvat = true;
    }

    //get the checked value of checkbox
    var tsek;
    if ($('#isOld').is(":checked")) {
        tsek = true;
    } else {
        tsek = false;
    }

    var objdata = {
        Id: parseInt(0),
        ReceivedDate: $('#ReceivedDate').val(),
        PreparedById: $('#PreparedById').val(),
        PreparedBy: $('#uname').text,
        ReceivedTotalCost: parseFloat($('#ReceivedTotalCost').val().replace(/,/g, "")),
        ReceivedById: $('#ReceivedById').val(),
        CheckedById: $('#CheckedById').val(),
        NotedById: $('#NotedById').val(),
        AuditedById: $('#AuditedById').val(),
        IsOld: tsek,
        SupplierId: $('#SupplierId').val(),
        PO1: $('#PO1').val() === null ? '' : $('#PO1').val(),
        PO2: $('#PO2').val(),
        PO3: $('#PO3').val(),
        PO4: $('#PO4').val(),
        PO5: $('#PO5').val(),
        SI1: $('#SI1').val(),
        SI2: $('#SI2').val(),
        SI3: $('#SI3').val(),
        SI4: $('#SI4').val(),
        SI5: $('#SI5').val(),
        DR1: $('#DR1').val(),
        DR2: $('#DR2').val(),
        DR3: $('#DR3').val(),
        DR4: $('#DR4').val(),
        DR5: $('#DR5').val(),
        DeliveryDate: $('#drDate').val(),
        Remark: $('#Remark').val()

    };

    if (objdata != null) {
        $.ajax({
            type: "POST",
            url: "/Receive/AddReceivedHeader/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(objdata),
            dataType: "json",
            success: function (response) {
                if (!response.IsError) {
                    $.ajax({
                        url: "/Receive/GetLoggedUserMaxRMId/",
                        type: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {

                            if (parseInt(result.Id) > 0) {

                                    addReceivingMaterialDetails(result.Id);
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

function addReceivingMaterialDetails(id,isnonvat) {

    //kunin number of rows in table
    var cnt = $('#tblMDetails tbody tr').length;
    //kunin lahat ng rows
    var tblrows = $('#tblMDetails tbody tr');

    var arrayData= new Array();

    //dito iyon magloloop per item (details)
    for (var i = 0; i < cnt; i++) {

        var $tds = tblrows[i].cells;

        var invcost = parseFloat($tds[7].innerText.replace(/,/g, ""));
        var vat = parseFloat($tds[8].innerText.replace(/,/g, ""));

        if (isnonvat) {
            invcost = 0;
            vat = 0;
        }

        var detailobj = {
            ReceivedMaterialHeaderId: id,
            MaterialId: parseInt($tds[0].innerText),
            Quantity: parseInt($tds[4].innerText),
            UnitId: parseInt($tds[2].innerText),
            UnitCost: parseFloat($tds[5].innerText.replace(/,/g, "")),
            TotalCost: parseFloat($tds[6].innerText.replace(/,/g, "")),
            InventorialCost: invcost,
            VAT: vat,
            OnHand: parseInt($tds[4].innerText),
            BalanceQty: parseInt($tds[9].innerText),
            Remark: $tds[10].innerText
        };

        arrayData.push(detailobj);
    }

    $.ajax({
        url: "/Receive/AddReceiveMaterialDetail/",
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        data: JSON.stringify(arrayData),
        dataType: "json",
        success: function (result) {
            //alert(result.ProcessMessage);
            $('#myModal').modal('hide');
            window.location = "/Receive/ReceivedMaterials";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getAllReceivedMaterialDetail(recmathdrid) {
    var rmhid = parseInt(recmathdrid);

    if (rmhid > 0) {
        var data1 = {
            Id: 0,
            ReceivedMaterialHeaderId: rmhid,
            MaterialId: 0,
            Quantity: 0,
            UnitId: 0,
            UnitCost: 0,
            OnHand: 0
        };

        $.ajax({
            url: "/Receive/GetRecMaterialDetailByHeaderId/",
            data: JSON.stringify(data1),
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            success: function (result) {
                if (!result.IsError) {
                    var html = '';
                    $.each(result.ReceivedMaterialDetails, function (key, item) {
                        html += '<tr style="background-color: white;">';
                        html += '<td style="text-align:center;display:none;">' + item.MaterialId + '</td>';
                        html += '<td>' + item.Material + '</td>';
                        html += '<td style="text-align:center;display:none;">' + item.UnitId + '</td>';
                        html += '<td style="text-align:right;">' + item.Quantity + '</td>';
                        html += '<td>' + item.Unit + '</td>';
                        html += '<td style="text-align:right;">' + Intl.NumberFormat().format(item.UnitCost) + '</td>';
                        html += '<td style="text-align:right;">' + Intl.NumberFormat().format(item.TotalCost) + '</td>';
                        html += '</tr>';
                    });
                    $('.tbodyright').html(html);
                } else {
                    alert(result.ProcessMessage);
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

    var parent = $('embed#rrpdf').parent();
    var newElement = '<embed src="/Receive/RRReportView?rptid=' + parseInt(rptid) + '"  width="100%" height="800" type="application/pdf" id="rrpdf">';

    $('embed#rrpdf').remove();
    parent.append(newElement);

    $('#myRptModal').modal('show');

}

function showExportModal() {
    $('#myExportModal').modal('show');
    $('#tblExport').hide();
}

function loadAllRRHeaders() {

    document.body.style.cursor = 'progress';
    $('#modalLoading').modal('show');
    document.getElementById("spintext").innerHTML = "LOADING...";

    $.ajax({
        type: 'GET',
        url: "/Receive/GetAllRRHeaders",
        mimeType: 'json',
        success: function (data) {
            if (data != null) {
                $('#myTable1').DataTable({
                    "data": data.data,
                    "bLengthChange": false,
                    "pageLength": 15,
                    "columns": [
                        { "data": "Id", "autoWidth": true },
                        { "data": "ReceivedDate", "autoWidth": true },
                        { "data": "PreparedBy", "autoWidth": true },
                        { "data": "ReceivedTotalCost", "autoWidth": true }
                    ],
                    order: [[0, "desc"]],
                    "initComplete": function (settings, json) {
                        let table = $('#myTable1').DataTable();

                        $('#myTable1 tbody').on('click', 'tr', function () {

                            var data = table.row(this).data();

                            getAllReceivedMaterialDetail(data["Id"])
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

            var fileName = "RR_" + dateFr + "_" + dateTo;

            $.ajax({
                type: "GET",
                url: "/Receive/FetchDateByDateRange?datefr=" + dateFr + "&dateto=" + dateTo,
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
                                { "data": "RRNo", "autoWidth": true },
                                { "data": "ReceivedDate", "autoWidth": true },
                                { "data": "PreparedBy", "autoWidth": true },
                                { "data": "ReceivedTotalCost", "autoWidth": true },
                                { "data": "Supplier", "autoWidth": true },
                                { "data": "PONos", "autoWidth": true },
                                { "data": "SINos", "autoWidth": true },
                                { "data": "DRNos", "autoWidth": true },
                                { "data": "DeliveryDate", "autoWidth": true },
                                { "data": "Remark", "autoWidth": true },
                                { "data": "Material", "autoWidth": true },
                                { "data": "Description", "autoWidth": true },
                                { "data": "Quantity", "autoWidth": true },
                                { "data": "Unit", "autoWidth": true },
                                { "data": "UnitCost", "autoWidth": true },
                                { "data": "TotalCost", "autoWidth": true },
                                { "data": "InventorialCost", "autoWidth": true },
                                { "data": "VAT", "autoWidth": true },
                                { "data": "OnHand", "autoWidth": true },
                                { "data": "BalanceQty", "autoWidth": true },
                                { "data": "BalanceRemark", "autoWidth": true }
                            ],
                            order: [[0, 'desc']]
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

//validation


//onkeypress events------------------------------------------------------------------
function isNumber(evt) {
    var ch = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(ch))) {
        evt.preventDefault();
    }
}

function validateFloatKeyPress(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}
//-----------------------------------------------------------------------------------

function numberWithCommas(x) {
    setTimeout(function () {
        if (x.value.lastIndexOf(".") != x.value.length - 1) {
            var dec = x.value.split(".", 2);

            var a;

            a = dec[0].replace(/,/g, "");

            //if (dec.length == 2)
                
            //else
            //    a = dec[0].replace(/,/g, "");

            var nf = new Intl.NumberFormat();

            if (dec.length == 2)
                x.value = nf.format(a) + "." + dec[1];
            else
                x.value = nf.format(a);
        } else {
            return false;
        }
    }, 0);
}