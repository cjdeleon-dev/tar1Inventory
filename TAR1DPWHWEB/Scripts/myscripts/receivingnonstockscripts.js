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
    loadSuppliers();

    $('#Material').val(0);

    //var recby = "TULABOT, JERRICKO J. ";
    //$("#ReceivedBy option:contains(" + recby + ")").attr('selected', 'selected');


    $('#NotedBy').val("TAL PLACIDO, RODOLFO R."); //default Rodolfo R. Tal Placido
    $('#AuditedBy').val("ABOGADO, MELANIE A."); //default Melanie A. Abogado

    $('#ReceivedById').val(332); //default Jerricko J. Tulabot
    $('#NotedById').val(317); //default Rodolfo R. Tal Placido
    $('#AuditedById').val(5); //default Melanie A. Abogado


    $('#Unit').val(0);
    $('#Quantity').val("");
    $('#ReceivedTotalCost').val("0.00");
    $('#TotalCost').val("");

    $('.tbodydetail').empty();
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
        url: "/Receive/GetNonStockMaterials/",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Material').empty();
            $('#Material').val(0);
            $('#Material').append("<option value=0>Select</option>");
            for (var i = 0; i < result.length; i++) {
                var Desc = result[i].Description;
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

function loadcboSig1() {
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

function loadcboSig2() {
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
}

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

function cboSupplierOnChange() {
    var suppid = $('#Supplier').val();
    $('#SupplierId').val(suppid);
}

function addNonStockMaterialItem() {
    //for validation-----------------------------------------------------------
    if (parseInt($('#Material').val()) == 0) {
        alert('Please select Material.');
        return false;
    }
    if (parseInt($('#Unit').val()) == 0) {
        alert('Please select Unit.');
        return false;
    }
    if (parseInt($('#Quantity').val()) == 0) {
        alert('Quantity should be greater than zero (0).');
        return false;
    }
    if (parseFloat($('#TotalCost').val()) == 0) {
        alert('Total Cost should be greater than zero (0).');
        return false;
    }
    //end of validation---------------------------------------------------------------

    //var ttlcost = parseInt($('#Quantity').val()) * parseFloat($('#UnitCost').val());
    var selMaterial = $("#Material option:selected").html();
    var selUnit = $("#Unit option:selected").html();
    var totalcost = $('#TotalCost').val();
    var invcost = parseFloat(totalcost) - (parseFloat(totalcost) * 0.12);
    var vat = parseFloat(totalcost) * 0.12;
    var unitcost = parseFloat(invcost) / parseFloat($('#Quantity').val());

    var html = '';
    html += '<tr style="background-color: white;">';
    html += '<td style="display:none;">' + $('#MaterialId').val() + '</td>';
    html += '<td>' + selMaterial + '</td>';
    html += '<td style="display:none;">' + $('#UnitId').val() + '</td>';
    html += '<td>' + selUnit + '</td>';
    html += '<td style="text-align:center;">' + $('#Quantity').val() + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(unitcost) + '</td>';
    html += '<td style="text-align:right;">' + $('#TotalCost').val() + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(invcost) + '</td>';
    html += '<td style="text-align:right;">' + Intl.NumberFormat().format(vat) + '</td>';
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
        var $tds = $(this).find('td'),
            tcost = $tds.eq(6).text();

        ttlcost = parseFloat(tcost.replace(',', '')) + parseFloat(ttlcost);

    });

    return ttlcost;
}

function addReceivingNonStockMaterials() {
    if (parseFloat($('#ReceivedTotalCost').val().replace(',', '')) == 0) {
        alert("Invalid Entry. Please fill all required fields.");
        return false;
    }

    var objdata = {
        Id: parseInt(0),
        ReceivedDate: $('#ReceivedDate').val(),
        PreparedById: $('#PreparedById').val(),
        PreparedBy: $('#PreparedBy').val(),
        ReceivedTotalCost: parseFloat($('#ReceivedTotalCost').val().replace(',', '')),
        ReceivedById: $('#ReceivedById').val(),
        NotedById: $('#NotedById').val(),
        AuditedById: $('#AuditedById').val(),
        SupplierId: $('#SupplierId').val(),
        PO1: $('#PO1').val(),
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
        DR5: $('#DR5').val()

    };

    if (objdata != null) {
        $.ajax({
            type: "POST",
            url: "/Receive/AddReceivedNonStockHeader/",
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(objdata),
            dataType: "json",
            success: function (response) {
                if (!response.IsError) {
                    $.ajax({
                        url: "/Receive/GetLoggedUserMaxRMNSId/",
                        type: "GET",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            if (parseInt(result.Id) > 0) {
                                var table = $(".tbodydetail");

                                table.find('tr').each(function (i) {
                                    var $tds = $(this).find('td'),
                                        materialId = $tds.eq(0).text(),
                                        material = $tds.eq(1).text(),
                                        unitid = $tds.eq(2).text(),
                                        unit = $tds.eq(3).text(),
                                        qty = $tds.eq(4).text(),
                                        ucost = $tds.eq(5).text().replace(',', ''),
                                        tcost = $tds.eq(6).text().replace(',', '');
                                    incost = $tds.eq(7).text().replace(',', '');
                                    vcost = $tds.eq(8).text().replace(',', '');

                                    var detailobj = {
                                        ReceivedMaterialHeaderId: result.Id,
                                        MaterialId: parseInt(materialId),
                                        Quantity: parseInt(qty),
                                        UnitId: parseInt(unitid),
                                        UnitCost: parseFloat(ucost),
                                        TotalCost: parseFloat(tcost),
                                        InventorialCost: parseFloat(incost),
                                        VAT: parseFloat(vcost),
                                        OnHand: parseInt(qty)
                                    };

                                    $.ajax({
                                        url: "/Receive/AddReceiveNonStockMaterialDetail/",
                                        type: "POST",
                                        contentType: "application/json;charset=UTF-8",
                                        data: JSON.stringify(detailobj),
                                        dataType: "json",
                                        success: function (result) {
                                            //alert(result.ProcessMessage);
                                            $('#myModal').modal('hide');
                                            window.location = "/Receive/ReceivedNonStockMaterials";
                                        },
                                        error: function (errormessage) {
                                            alert(errormessage.responseText);
                                        }
                                    });

                                });

                            }
                        },
                        error: function (errormessage) {
                            alert(errormessage.responseText);
                        }
                    });
                }
                //printPreview();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }

}

function getAllReceivedNonStockMaterialDetail(recmathdrid) {
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
            url: "/Receive/GetRecNonStockMaterialDetailByHeaderId/",
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

    //var parent = $('embed#rrpdf').parent();
    //var newElement = '<embed src="/Receive/RRReportView?rptid=' + parseInt(rptid) + '"  width="100%" height="800" type="application/pdf" id="rrpdf">';

    //$('embed#rrpdf').remove();
    //parent.append(newElement);

    $('#myRptModal').modal('show');

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