//mousemove.draggable
$(".modal-header").on("mousedown", function (mousedownEvt) {
    var $draggable = $(this);
    var x = mousedownEvt.pageX - $draggable.offset().left,
        y = mousedownEvt.pageY - $draggable.offset().top;
    $("body").on("mousemove.draggable", function (mousemoveEvt) {
        $draggable.closest(".modal-dialog").offset({
            "left": mousemoveEvt.pageX - x,
            "top": mousemoveEvt.pageY - y
        });
    });
    $("body").one("mouseup", function () {
        $("body").off("mousemove.draggable");
    });
    $draggable.closest(".modal").one("bs.modal.hide", function () {
        $("body").off("mousemove.draggable");
    });
});

//Add Invoice
var numberEvenOrOdd = 0;
var listInvoice = [];
$("#btnAddInvoiceNumber").click(function () {
    if ($("#txtInvoiceNumber").val() != "") {
        numberEvenOrOdd++;
        var objInvoice = {};
        objInvoice.ID = numberEvenOrOdd;
        objInvoice.InvoiceNumber = $("#txtInvoiceNumber").val();
        listInvoice.push(objInvoice);
        RerenderAgain();
    }
    $("#txtInvoiceNumber").val("");
});
function RerenderAgain() {
    $("#odd").empty();
    $("#event").empty();
    var k = 1;
    for (var i = 0; i < listInvoice.length; i++) {
        var result = i % 2;
        if (result == 0) {
            $("#odd").append('<div id="btnAddInv' + listInvoice[i].ID + '" class="row mt-2"><div class="col-3">INVOICE NO' + k + ':</div><div class="col-6" id="txtInvoice' + listInvoice[i].ID + '">' + listInvoice[i].InvoiceNumber + '</div><div class="col-3"><button class="btn btn-danger" onclick="removeInvoiceAdd(' + listInvoice[i].ID + ')">DELETE</button></div></div>');
        } else {
            $("#event").append('<div id="btnAddInv' + listInvoice[i].ID + '" class="row mt-2"><div class="col-3">INVOICE NO' + k + ':</div><div class="col-6" id="txtInvoice' + listInvoice[i].ID + '">' + listInvoice[i].InvoiceNumber + '</div><div class="col-3"><button class="btn btn-danger" onclick="removeInvoiceAdd(' + listInvoice[i].ID + ')">DELETE</button></div></div>');
        }
        k++;
    }
    if (listVolumn.length != 0) {
        for (var i = 0; i < listVolumn.length; i++) {
            $('#cboVolumeInvoice' + listVolumn[i].ID).empty().trigger('change');
            for (var k = 0; k < listInvoice.length; k++) {
                $('#cboVolumeInvoice' + listVolumn[i].ID).append(new Option(listInvoice[k].InvoiceNumber, listInvoice[k].InvoiceNumber, false, false)).trigger('change');
            }
        }
    }
    if (listTruckType.length != 0) {
        for (var i = 0; i < listTruckType.length; i++) {
            $('#cboTruckInvoice' + listTruckType[i].ID).empty().trigger('change');
            for (var k = 0; k < listInvoice.length; k++) {
                $('#cboTruckInvoice' + listTruckType[i].ID).append(new Option(listInvoice[k].InvoiceNumber, listInvoice[k].InvoiceNumber, false, false)).trigger('change');
            }
        }
    }
}
function removeInvoiceAdd(numberEvent) {
    listInvoice = listInvoice.filter(e => e.ID !== numberEvent);
    RerenderAgain();
}

//Add Volume
var numberEvenOrOddVolume = 0;
var numberEvenOrOddTruckType = 0;
var listVolumn = [];
var listTruckType = [];
$("#btnAddVolumn").click(function () {
    if ($("#txtVolumeQty").val() != "" && $("#cboVolume option:selected").val() != -1) {
        for (var i = 0; i < parseInt($("#txtVolumeQty").val()); i++) {
            numberEvenOrOddVolume++;
            var objVolumn = {};
            objVolumn.ID = numberEvenOrOddVolume;
            objVolumn.VolumeText = $("#cboVolume option:selected").text();
            objVolumn.VolumeQty = 1;//parseInt($("#txtVolumeQty").val());//$("#txtVolumeQty").val();
            objVolumn.VolumeName = $("#cboVolume").val();
            objVolumn.GrossWeight = '';//$("#txtVolumeGrossWeight").val();
            objVolumn.invoiceList = {};//$("#txtVolumeGrossWeight").val();
            listVolumn.push(objVolumn);
        }
        RerenderVolumn();
    }
    $("#txtVolumeQty").val("");
    $("#txtVolumeGrossWeight").val("");
});
$("#btnAddTruckType").click(function () {
    if ($("#txtTruckTypeQty").val() != "" && $("#cboTruckType option:selected").val() != -1) {
        for (var i = 0; i < parseInt($("#txtTruckTypeQty").val()); i++) {
            numberEvenOrOddTruckType++;
            var objTruckType = {};
            objTruckType.ID = numberEvenOrOddTruckType;
            objTruckType.TruckTypeText = $("#cboTruckType option:selected").text();
            objTruckType.TruckTypeQty = 1;//$("#txtTruckTypeQty").val();
            objTruckType.TruckTypeName = $("#cboTruckType").val();
            objTruckType.GrossWeight = '';//$("#txtTruckTypeGrossWeight").val();
            objTruckType.invoiceList = {};//$("#txtVolumeGrossWeight").val();
            listTruckType.push(objTruckType);
        }
        RerenderTruckType();
    }
    $("#txtTruckTypeQty").val("");
    $("#txtTruckTypeGrossWeight").val("");
});
$("#cboSalePerson1").change(function () {
    getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
});
function RerenderVolumn() {
    $("#rowVolume").empty();
    var k = 1;
    for (var i = 0; i < listVolumn.length; i++) {
        $("#rowVolume").append('<div class="row" id="btnDeleteVolumn' + listVolumn[i].ID + '"><div class="col-1" style="margin: auto; ">VOLUME NO' + k + ':</div><div class="col-7"><div class="row"><div class="col-4"><input type="number" class="form-control" disable value="' + listVolumn[i].VolumeQty + '" id="txtVolumQty' + listVolumn[i].ID + '" /></div><div class="col-3" style="margin: auto; "><div id="txtVolumValue' + listVolumn[i].ID + '">' + listVolumn[i].VolumeText + '</div></div><div class="col-1" style="margin:auto">&nbsp;&nbsp;(KG)</div><div class="col-4"><input type="number" class="form-control" onchange="OnchangeGorssWeightVolumeAndTruckType(' + listVolumn[i].ID + ',\'volume\')" value="' + listVolumn[i].GrossWeight + '" id="txtVolumGrossWeight' + listVolumn[i].ID + '" /></div></div></div><div class="col-2"><select class="form-control" multiple="multiple" onchange="onVolumeInvoice(' + listVolumn[i].ID + ')" id="cboVolumeInvoice' + listVolumn[i].ID + '"></select></div><div class="col-2"><button class="btn btn-danger" onclick="removeVolumn(' + listVolumn[i].ID + ')">DELETE</button></div></div>');
        k++;
        //ReCreate Multiple Combo
        $("#cboVolumeInvoice" + listVolumn[i].ID).select2({
            placeholder: "Select a Invoice List",
            maximumSelectionLength: 999999
        });
        $('#cboVolumeInvoice' + listVolumn[i].ID).empty().trigger('change');
        for (var k = 0; k < listInvoice.length; k++) {
            $('#cboVolumeInvoice' + listVolumn[i].ID).append(new Option(listInvoice[k].InvoiceNumber, listInvoice[k].InvoiceNumber, false, false)).trigger('change');
        }
        if (listVolumn.invoiceList != {}) {
            $('#cboVolumeInvoice' + listVolumn[i].ID).val(Object.values(listVolumn[i].invoiceList)).trigger('change');
        }
    }
}
function onVolumeInvoice(id) {
    if ($("#cboVolumeInvoice" + id).val().length != 0) {
        var index1 = listVolumn.map(e => e.ID).indexOf(id);
        listVolumn[index1].invoiceList = $("#cboVolumeInvoice" + id).val();
    }
}
function onTruckInvoice(id) {
    if ($("#cboTruckInvoice" + id).val().length != 0) {
        var index1 = listTruckType.map(e => e.ID).indexOf(id);
        listTruckType[index1].invoiceList = $("#cboTruckInvoice" + id).val();
    }
}
function RerenderTruckType() {
    $("#rowTruckType").empty();
    var k = 1;
    for (var i = 0; i < listTruckType.length; i++) {
        $("#rowTruckType").append('<div class="row" id="btnDeleteTruckType' + listTruckType[i].ID + '"><div class="col-1" style="margin: auto;">TruckType NO' + k + ':</div><div class="col-7"><div class="row"><div class="col-4"><input type="number" class="form-control" disable value="' + listTruckType[i].TruckTypeQty + '" id="txtTruckTypeQty' + listTruckType[i].ID + '" /></div><div class="col-4" style="margin: auto;"><div id="txtTruckTypeValue' + listTruckType[i].ID + '">' + listTruckType[i].TruckTypeText + '</div></div><div class="col-1" style="margin:auto">&nbsp;&nbsp;(KG)</div><div class="col-3"> <input type="number" class="form-control" onchange="OnchangeGorssWeightVolumeAndTruckType(' + listTruckType[i].ID + ',\'truckType\')" value="' + listTruckType[i].GrossWeight + '" id="txtTruckTypeGrossWeight' + listTruckType[i].ID + '" /></div></div></div><div class="col-2"><select class="form-control" multiple="multiple" onchange="onTruckInvoice(' + listTruckType[i].ID + ')" id="cboTruckInvoice' + listTruckType[i].ID + '"></select></div><div class="col-2"><button class="btn btn-danger" onclick="removeTruckType(' + listTruckType[i].ID + ')">DELETE</button></div></div>');
        k++;
        $("#cboTruckInvoice" + listTruckType[i].ID).select2({
            placeholder: "Select a Invoice List",
            maximumSelectionLength: 999999
        });
        $('#cboTruckInvoice' + listTruckType[i].ID).empty().trigger('change');
        for (var k = 0; k < listInvoice.length; k++) {
            $('#cboTruckInvoice' + listTruckType[i].ID).append(new Option(listInvoice[k].InvoiceNumber, listInvoice[k].InvoiceNumber, false, false)).trigger('change');
        }
        if (listTruckType.invoiceList != {}) {
            $('#cboTruckInvoice' + listTruckType[i].ID).val(Object.values(listTruckType[i].invoiceList)).trigger('change');
        }
    }
}
function removeVolumn(numberEvent) {
    listVolumn = listVolumn.filter(e => e.ID !== numberEvent);
    RerenderVolumn();
}
function removeTruckType(numberEvent) {
    listTruckType = listTruckType.filter(e => e.ID !== numberEvent);
    RerenderTruckType();
}
function getSaleEmployee(url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            alert("hello");
        },
        error: function (erro) {
        }
    });
}
function getProvinceLoadingByCountry(country) {
    $('#cboMultiPlaceOfLoading').empty().trigger("change");
    var afterSelect = objProvinceType.filter(e => e.country === country);
    var newOptionPlaceOfLoading = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfLoading.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiPlaceOfLoading').append(newOptionPlaceOfLoading).trigger('change');
}
function getProvinceLoadingByDistrict(district) {
    var codeProvince = [];
    for (var i = 0; i < district.length; i++) {
        codeProvince.push(objDistrictType.filter(e => e.code === district[i])[0].province);
    }
    $('#cboMultiPlaceOfLoading').val(codeProvince).trigger('change');
    $('#cboMultiDistrictPlaceOfLoading').val(district);
}
function getDistrictLoadingByCountry(province) {
    $('#cboMultiDistrictPlaceOfLoading').empty().trigger("change");
    var afterSelect = objDistrictType.filter(e => e.province === province);
    var newOptionPlaceOfLoading = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfLoading.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiDistrictPlaceOfLoading').append(newOptionPlaceOfLoading).trigger('change');
}
function getDistrictLoadingAll() {
    $('#cboMultiDistrictPlaceOfLoading').empty().trigger("change");
    var afterSelect = objDistrictType;
    var newOptionPlaceOfLoading = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfLoading.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiDistrictPlaceOfLoading').append(newOptionPlaceOfLoading).trigger('change');
}
let getProvince = 0;
$("#cboMultiDistrictPlaceOfLoading").change(function () {
    if ($("#cboMultiDistrictPlaceOfLoading").val()[0] !== undefined) {
        getProvinceLoadingByDistrict($("#cboMultiDistrictPlaceOfLoading").val());
        /* $("#cboMultiDistrictPlaceOfLoading").val(districttesting).trigger("change");*/
        //getProvince = 1;
    } else {
        //getProvince = 0;
    }
});

function getProvinceDeliveryByCountry(country) {
    $('#cboMultiPlaceOfDelivery').empty().trigger("change");
    var afterSelect = objProvinceType.filter(e => e.country === country);
    var newOptionPlaceOfDelivery = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfDelivery.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiPlaceOfDelivery').append(newOptionPlaceOfDelivery).trigger('change');
}
function getDistrictDeliveryAll() {
    $('#cboMultiDistrictPlaceOfDelivery').empty().trigger("change");
    var afterSelect = objDistrictType;
    var newOptionPlaceOfLoading = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfLoading.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiDistrictPlaceOfDelivery').append(newOptionPlaceOfLoading).trigger('change');
}
function getProvinceDeliveryByDistrict(district) {
    var codeProvince = [];
    for (var i = 0; i < district.length; i++) {
        codeProvince.push(objDistrictType.filter(e => e.code === district[i])[0].province);
    }
    $('#cboMultiPlaceOfDelivery').val(codeProvince).trigger('change');
    $('#cboMultiDistrictPlaceOfDelivery').val(district);
}
$("#cboMultiDistrictPlaceOfDelivery").change(function () {
    if ($("#cboMultiDistrictPlaceOfDelivery").val()[0] !== undefined) {
        getProvinceDeliveryByDistrict($("#cboMultiDistrictPlaceOfDelivery").val());
    } else {
    }
});

function getObjSalesQuotationByCountry(route) {
    $('#txtSaleQuotations').empty().trigger("change");
    var afterSelect = objListSaleQuotationBooking.filter(e => e.route === route && e.slpCode === $("#cboSalePerson1").val());
    var newOptionPlaceOfDelivery = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfDelivery.push(new Option(afterSelect[i].customerRefNo + ' - ' + afterSelect[i].customerName, afterSelect[i].docEntry, false, false));
    }
    $('#txtSaleQuotations').append(newOptionPlaceOfDelivery).trigger('change');
}
function getDistrictDeliveryByCountry(province) {
    $('#cboMultiDistrictPlaceOfDelivery').empty().trigger("change");
    var afterSelect = objDistrictType.filter(e => e.province === province);
    var newOptionPlaceOfLoading = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfLoading.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiDistrictPlaceOfDelivery').append(newOptionPlaceOfLoading).trigger('change');
}

$("#cboMultiPlaceOfLoading").change(function () {
    if ($("#cboSelectSeries").val() == "EWST") {
        if ($("#cboMultiPlaceOfLoading").val()[0] != undefined) {
            getDistrictLoadingByCountry($("#cboMultiPlaceOfLoading").val()[0]);
        } else {
            getDistrictLoadingAll();
        }
    } else {
        if ($("#cboMultiPlaceOfLoading").val()[0] != undefined) {
            getDistrictLoadingByCountry($("#cboMultiPlaceOfLoading").val()[0]);
        } else {
            getDistrictLoadingAll();
        }
    }
});

$("#cboMultiPlaceOfDelivery").change(function () {
    if ($("#cboSelectSeries").val() == "EWST") {
        if ($("#cboMultiPlaceOfDelivery").val()[0] != undefined) {
            getDistrictDeliveryByCountry($("#cboMultiPlaceOfDelivery").val()[0]);
        } else {
            getDistrictDeliveryAll();
        }
    } else {
        if ($("#cboMultiPlaceOfDelivery").val()[0] != undefined) {
            getDistrictDeliveryByCountry($("#cboMultiPlaceOfDelivery").val()[0]);
        } else {
            getDistrictDeliveryAll();
        }
    }
});

function overseaForwarder(destination) {
    //$('#cboMultiOverseaTrucker').empty().trigger("change");
    //$('#cboOverseaForwarder').empty().trigger("change");
    //var afterSelectForwarder = [];
    //var afterSelectTrucker = [];
    //if (destination === "Thailand") {
    //    afterSelectForwarder = objOverseaForwarderType.filter(e => e.country !== destination);
    //    //afterSelectTrucker = objOverseaTruckerType.filter(e => e.country !== destination);
    //} else {
    //    afterSelectForwarder = objOverseaForwarderType.filter(e => e.country === destination);
    //    //afterSelectTrucker = objOverseaTruckerType.filter(e => e.country === destination);
    //}
    //var newOptionOverseaTrucker = [];
    //var newOptionOverseaForwarder = [];
    //for (var i = 0; i < afterSelectForwarder.length; i++) {
    //    newOptionOverseaForwarder.push(new Option(afterSelectForwarder[i].name, afterSelectForwarder[i].code, false, false));
    //}
    //for (var i = 0; i < afterSelectTrucker.length; i++) {
    //    newOptionOverseaTrucker.push(new Option(afterSelectTrucker[i].name, afterSelectTrucker[i].code, false, false));
    //}
    //$('#cboMultiOverseaTrucker').append(newOptionOverseaTrucker).trigger('change');
    //$('#cboOverseaForwarder').append(newOptionOverseaForwarder).trigger('change');
}

//Select Values Change In Origin
$('#cboOrigin').on('change', function () {
    $("#txtShowOrigin").val($("#cboOrigin option:selected").text());
    getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
    var tmpSelectedValue = $("#cboDestination").val();
    var newObjGetDestinations = objGetDestinations.filter(x => x.code.toString() !== $("#cboOrigin").val());
    OnRendercbo(newObjGetDestinations, "cboDestination");
    $("#cboDestination").val(tmpSelectedValue);
    getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
    $('#txtSaleQuotations').val([]).trigger("change");
});
//Select Values Change In Destination
$('#cboDestination').on('change', function () {
    $("#txtShowDestination").val($("#cboDestination option:selected").text());
    overseaForwarder($("#cboDestination option:selected").text());
    var tmpSelectedValue = $("#cboOrigin").val();
    var newObjGetOrigins = objGetOrigins.filter(x => x.code.toString() !== $("#cboDestination").val());
    OnRendercbo(newObjGetOrigins, "cboOrigin");
    $("#cboOrigin").val(tmpSelectedValue);
    getProvinceDeliveryByCountry($("#cboDestination option:selected").text())
    getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
    $('#txtSaleQuotations').val([]).trigger("change");
});
//On DateChange Loading
$('#txtLoadingDate').on('change', function () {
    if (DateDiff.inDays(new Date($('#txtLoadingDate').val()), new Date()) > 15) {
        alert("Please Selected Loading Date!");
        $("#txtLoadingDate").val("");
    } else {
        $("#txtCrossBorderDate").prop('disabled', false);
        $("#txtCrossBorderDate").attr("min", $("#txtLoadingDate").val());
        $("#txtCrossBorderDate").val("");
        $("#txtEtaRequirement").val("");
    }
});
//On DateChange Delivery
$('#txtCrossBorderDate').on('change', function () {
    if (DateDiff.inDays(new Date($('#txtCrossBorderDate').val()), new Date($('#txtLoadingDate').val())) > 0) {
        alert("Please Selected CrossBorderDate!");
        $("#txtCrossBorderDate").val("");
    } else {
        $("#txtEtaRequirement").prop('disabled', false);
        $("#txtEtaRequirement").attr("min", $("#txtCrossBorderDate").val());
        $("#txtEtaRequirement").val("");
    }
});
//On DateChange Delivery
$('#txtEtaRequirement').on('change', function () {
    if (DateDiff.inDays(new Date($('#txtEtaRequirement').val()), new Date($('#txtCrossBorderDate').val())) > 0) {
        alert("Please Selected ETADate!");
        $("#txtEtaRequirement").val("");
    }
});
//Select Values Change In JobNo.
var objServiceType = [];
var objProvinceType = [];
var objOverseaForwarderType = [];
var objOverseaTruckerType = [];
var objGetOrigins = [];
var objGetDestinations = [];
var objGetShippers = [];
var objGetConsignees = [];
var objDistrictType = [];
var objDistrictType = [];
var objListSaleQuotationBooking = [];
function setterObjectType(obj, type) {
    if (type === 'Service') {
        objServiceType = obj;
    } else if (type === 'Province') {
        console.log(obj);
        objProvinceType = obj;
    } else if (type === 'OverseaForwarder') {
        objOverseaForwarderType = obj;
    } else if (type === 'OverseaTrucker') {
        objOverseaTruckerType = obj;
    } else if (type === 'GetOrigins') {
        objGetOrigins = obj;
    } else if (type === 'GetDestinations') {
        objGetDestinations = obj;
    } else if (type === 'GetShippers') {
        objGetShippers = obj;
    } else if (type === 'GetConsignees') {
        objGetConsignees = obj;
    } else if (type === 'GetDistrict') {
        objDistrictType = obj;
    } else if (type === 'GetSalesQuotation') {
        objListSaleQuotationBooking = obj;
    }
}

//OnRender CBO
function OnRendercbo(obj, name) {
    $("#" + name).empty();
    $("#" + name).append("<option selected value='None'>Choose...</option>");
    for (var i = 0; i < obj.length; i++) {
        $("#" + name).append("<option value='" + obj[i].code + "'>" + obj[i].name + "</option>");
    }
}
function OnRendercboSelect2(obj, name) {
    $('#' + name).empty().trigger("change");
    var newOption = [];
    for (var i = 0; i < obj.length; i++) {
        newOption.push(new Option(obj[i].cardName, obj[i].cardCode, false, false));
    }
    $('#' + name).append(newOption).trigger('change');
}

$('#cboSelectSeries').on('change', function () {
    onSelectedSeries();
});
function onSelectedSeries() {
    if ($("#cboSelectSeries").val() == 'EWTI') {
        $("#cboImportType").val("TI");
        var lsServiceType = objServiceType.filter(x => x.importType == "TI");
        $("#cboServiceType").empty();
        for (var i = 0; i < lsServiceType.length; i++) {
            $("#cboServiceType").append(new Option(lsServiceType[i].name, lsServiceType[i].code));
        }
        overseaForwarder($("#cboDestination option:selected").text());
        getProvinceDeliveryByCountry($("#cboDestination option:selected").text());
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "none");
        $("#plDistrictDelivery").css("display", "none");
        $('#txtSaleQuotations').empty().trigger("change");
        $("#cboMultiPlaceOfLoading").select2({
            placeholder: "Select a Place Of Loading",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#cboMultiPlaceOfDelivery").select2({
            placeholder: "Select a Place Of Delivery",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "");
        $("#plDistrictDelivery").css("display", "");
    }
    else if ($("#cboSelectSeries").val() == 'EWTE') {
        $("#cboImportType").val("TE");
        var lsServiceType = objServiceType.filter(x => x.importType == "TE");
        $("#cboServiceType").empty();
        for (var i = 0; i < lsServiceType.length; i++) {
            $("#cboServiceType").append(new Option(lsServiceType[i].name, lsServiceType[i].code));
        }
        getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "none");
        $("#plDistrictDelivery").css("display", "none");
        $('#txtSaleQuotations').empty().trigger("change");
        $("#cboMultiPlaceOfLoading").select2({
            placeholder: "Select a Place Of Loading",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#cboMultiPlaceOfDelivery").select2({
            placeholder: "Select a Place Of Delivery",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "");
        $("#plDistrictDelivery").css("display", "");
    }
    else if ($("#cboSelectSeries").val() == 'EWTD') {
        $("#cboImportType").val("TD");
        var lsServiceType = objServiceType.filter(x => x.importType == "TD");
        $("#cboServiceType").empty();
        for (var i = 0; i < lsServiceType.length; i++) {
            $("#cboServiceType").append(new Option(lsServiceType[i].name, lsServiceType[i].code));
        }
        getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
        getProvinceDeliveryByCountry($("#cboDestination option:selected").text());
        overseaForwarder($("#cboDestination option:selected").text());
        //Thai Border
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "none");
        $("#plDistrictDelivery").css("display", "none");
        getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
        $('#txtSaleQuotations').val([]).trigger("change");
        $("#cboMultiPlaceOfLoading").select2({
            placeholder: "Select a Place Of Loading",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#cboMultiPlaceOfDelivery").select2({
            placeholder: "Select a Place Of Delivery",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "");
        $("#plDistrictDelivery").css("display", "");
    }
    else if ($("#cboSelectSeries").val() == 'EWTT') {
        $("#cboImportType").val("TT");
        var lsServiceType = objServiceType.filter(x => x.importType == "TT");
        $("#cboServiceType").empty();
        for (var i = 0; i < lsServiceType.length; i++) {
            $("#cboServiceType").append(new Option(lsServiceType[i].name, lsServiceType[i].code));
        }
        //Thai Border
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 2,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "none");
        $("#plDistrictDelivery").css("display", "none");
        $('#txtSaleQuotations').empty().trigger("change");
        $("#cboMultiPlaceOfLoading").select2({
            placeholder: "Select a Place Of Loading",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#cboMultiPlaceOfDelivery").select2({
            placeholder: "Select a Place Of Delivery",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "");
        $("#plDistrictDelivery").css("display", "");
    }
    else if ($("#cboSelectSeries").val() == 'EWST') {
        $("#cboImportType").val("ST");
        var lsServiceType = objServiceType.filter(x => x.importType == "ST");
        $("#cboServiceType").empty();
        for (var i = 0; i < lsServiceType.length; i++) {
            $("#cboServiceType").append(new Option(lsServiceType[i].name, lsServiceType[i].code));
        }
        getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
        getProvinceDeliveryByCountry($("#cboDestination option:selected").text());
        overseaForwarder($("#cboDestination option:selected").text());
        //Thai Border
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
        $("#cboMultiPlaceOfLoading").select2({
            placeholder: "Select a Place Of Loading",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#cboMultiPlaceOfDelivery").select2({
            placeholder: "Select a Place Of Delivery",
            maximumSelectionLength: 999999,
            width: '100%'
        });
        $("#plDistrictLoading").css("display", "");
        $("#plDistrictDelivery").css("display", "");
        getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
        $('#txtSaleQuotations').val([]).trigger("change");
    }
}
function onChangeJobNo() {
    var value = $("#cboSelectSeries").val();
    if (value === "EWTD") {
        OnRendercbo(objGetDestinations, "cboDestination");
        OnRendercbo(objGetOrigins, "cboOrigin");
        $("#cboOrigin").val(selectOrigin);
        $("#cboDestination").val(selectOrigin);
        $("#cboOrigin").attr("disabled", true);
        $("#cboDestination").attr("disabled", true);
        $("#txtShowOrigin").val($("#cboOrigin option:selected").text());
        $("#txtShowDestination").val($("#cboDestination option:selected").text());
        getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
        $('#txtSaleQuotations').val([]).trigger("change");
    } else if (value === "EWST") {
        OnRendercbo(objGetDestinations, "cboDestination");
        OnRendercbo(objGetOrigins, "cboOrigin");
        $("#cboOrigin").val(selectOrigin);
        $("#cboDestination").val(selectOrigin);
        $("#cboOrigin").attr("disabled", true);
        $("#cboDestination").attr("disabled", true);
        $("#txtShowOrigin").val($("#cboOrigin option:selected").text());
        $("#txtShowDestination").val($("#cboDestination option:selected").text());
        getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
        getDistrictDeliveryAll();
        getDistrictLoadingAll();
        $('#txtSaleQuotations').val([]).trigger("change");
    } else if (value === "EWTE") {
        OnRendercbo(objGetOrigins, "cboOrigin");
        $("#cboOrigin").val(selectOrigin);
        $("#cboOrigin").attr("disabled", true);
        $("#cboDestination").val("None");
        $("#cboDestination").attr("disabled", false);
        $("#txtShowOrigin").val($("#cboOrigin option:selected").text());
        var newObjGetDestinations = objGetDestinations.filter(x => x.code.toString() !== selectOrigin);
        getDistrictDeliveryAll();
        getDistrictLoadingAll();
        OnRendercbo(newObjGetDestinations, "cboDestination");
    } else if (value === "EWTI") {
        OnRendercbo(objGetDestinations, "cboDestination");
        $("#cboDestination").val(selectOrigin);
        $("#cboDestination").attr("disabled", true);
        $("#cboOrigin").val("None");
        $("#cboOrigin").attr("disabled", false);
        $("#txtShowDestination").val($("#cboDestination option:selected").text());
        var newObjOrigin = objGetOrigins.filter(x => x.code.toString() !== selectOrigin);
        getDistrictDeliveryAll();
        getDistrictLoadingAll();
        OnRendercbo(newObjOrigin, "cboOrigin");
    } else if (value === "EWTT") {
        $("#cboOrigin").val("None");
        $("#cboOrigin").attr("disabled", false)
        $("#cboDestination").val("None");
        $("#cboDestination").attr("disabled", false)
        getDistrictDeliveryAll();
        getDistrictLoadingAll();
    }
}

//Inits Select2

//Place Of Delivery
$("#cboMultiPlaceOfDelivery").select2({
    placeholder: "Select a Place Of Delivery",
    maximumSelectionLength: 9999999,
    width: '100%'
});

//Place Of Loading
$("#cboMultiPlaceOfLoading").select2({
    placeholder: "Select a Place Of Loading",
    maximumSelectionLength: 9999999,
    width: '100%'
});

//Oversea Trucker
$("#cboMultiOverseaTrucker").select2({
    placeholder: "Select a Oversea Trucker",
    maximumSelectionLength: 99999999,
    width: '100%'
});

//Thai Forwarder
$("#cboThaiForwarder").select2({
    placeholder: "Select a Thai Forwarder",
    maximumSelectionLength: 999999,
    width: '100%'
});

//Over Forwarder
$("#cboOverseaForwarder").select2({
    placeholder: "Select a Oversea Forwarder",
    maximumSelectionLength: 999999,
    width: '100%'
});

//Thai Border
$("#cboThaiBorder").select2({
    placeholder: "Select a Thai Border",
    maximumSelectionLength: 1,
    width: '100%'
});

//Shipper
$("#txtShipper").select2({
    placeholder: "Select a Shipper",
    maximumSelectionLength: 999999,
    width: '100%'
});

//Consignee
$("#txtConsignee").select2({
    placeholder: "Select a Consignee",
    maximumSelectionLength: 99999,
    width: '100%'
});

//Sales Quotation
$("#txtSaleQuotations").select2({
    placeholder: "Select list Sales Quotation",
    maximumSelectionLength: 9999999,
    width: '100%'
});

//Net Weight
$("#txtNetWeight").change(function () {
    if (parseFloat($("#txtGrossWeight").val()) !== 0 && parseFloat($("#txtGrossWeight").val()) !== 0) {
        if (parseFloat($("#txtNetWeight").val()) > parseFloat($("#txtGrossWeight").val())) {
            alert("Net weight can not bigger than gross weight");
            $("#txtNetWeight").val("");
        }
    }
});
//Net Weight
$("#txtGrossWeight").change(function () {
    if ($("#txtNetWeight").val() !== 0) {
        if (parseFloat($("#txtNetWeight").val()) > parseFloat($("#txtGrossWeight").val())) {
            alert("Net weight can not bigger than gross weight");
            $("#txtNetWeight").val("");
        }
    }
});
//District Of Delivery
$("#cboMultiDistrictPlaceOfDelivery").select2({
    placeholder: "Select a District Of Delivery",
    maximumSelectionLength: 99999,
    width: '100%'
});
//District Of Loading
$("#cboMultiDistrictPlaceOfLoading").select2({
    placeholder: "Select a District Of Loading",
    maximumSelectionLength: 99999,
    width: '100%'
});

function OnchangeGorssWeightVolumeAndTruckType(id, type) {
    //Add Volumne
    if (type === "volume") {
        if (listVolumn.length != 0) {
            var grossWieght = 0;
            for (var i = 0; i < listTruckType.length; i++) {
                grossWieght = grossWieght + parseFloat($("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val() == '' ? 0 : $("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val());
            }
            for (var i = 0; i < listVolumn.length; i++) {
                grossWieght = grossWieght + parseFloat($("#txtVolumGrossWeight" + listVolumn[i].ID).val() == '' ? 0 : $("#txtVolumGrossWeight" + listVolumn[i].ID).val());
            }
            if (grossWieght > $("#txtGrossWeight").val()) {
                alert("Gorss Weight bigger then Total Gross Weight");
                $("#txtVolumGrossWeight" + id).val("");
            } else {
                objIndex = listVolumn.findIndex((obj => obj.ID === id));
                listVolumn[objIndex].GrossWeight = $("#txtVolumGrossWeight" + id).val();
            }
        }
    }

    //Truck Type
    if (type === "truckType") {
        if (listTruckType.length != 0) {
            var grossWieghtTruck = 0;
            for (var i = 0; i < listVolumn.length; i++) {
                grossWieghtTruck = grossWieghtTruck + parseFloat($("#txtVolumGrossWeight" + listVolumn[i].ID).val() == '' ? 0 : $("#txtVolumGrossWeight" + listVolumn[i].ID).val());
            }
            for (var i = 0; i < listTruckType.length; i++) {
                grossWieghtTruck = grossWieghtTruck + parseFloat($("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val() == '' ? 0 : $("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val());
            }
            if (grossWieghtTruck > $("#txtGrossWeight").val()) {
                alert("Gorss Weight bigger then Total Gross Weight");
                $("#txtTruckTypeGrossWeight" + id).val("");
            } else {
                objIndex = listTruckType.findIndex((obj => obj.ID === id));
                listTruckType[objIndex].GrossWeight = $("#txtTruckTypeGrossWeight" + id).val();
            }
        }
    }

}


var DateDiff = {

    inDays: function (d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000));
    },

    inWeeks: function (d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000 * 7));
    },

    inMonths: function (d1, d2) {
        var d1Y = d1.getFullYear();
        var d2Y = d2.getFullYear();
        var d1M = d1.getMonth();
        var d2M = d2.getMonth();

        return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
    },

    inYears: function (d1, d2) {
        return d2.getFullYear() - d1.getFullYear();
    }
}