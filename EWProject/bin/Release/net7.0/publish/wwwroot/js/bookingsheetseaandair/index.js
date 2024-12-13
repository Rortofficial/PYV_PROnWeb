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
//Add Flight Information
var listInfoAir = [];
var indexInforAir = 0;
$("#btnAddInforAir").click(function () {
    if ($("#txtFlightsNumber").val() != "") {
        indexInforAir++;
        var objAirInformation = {};
        objAirInformation.ID = indexInforAir;
        objAirInformation.FlightFromTo = $("#txtFlightsFromToAir").val();
        objAirInformation.FlightNo = $("#txtFlightsNumberAir").val();
        objAirInformation.FlightArrival = $("#txtFlightArrivalAir").val();
        listInfoAir.push(objAirInformation);
        RerenderInfoAirAgain();
    }
    $("#txtFlightsFromToAir").val("");
    $("#txtFlightsNumberAir").val("");
    $("#txtFlightArrivalAir").val("");
});
function RerenderInfoAirAgain() {
    var tbListItemLine = $('#TbListAirInformation').DataTable();
    tbListItemLine.clear();
    tbListItemLine.rows.add(listInfoAir);
    tbListItemLine.search("").columns().search("").draw();
}
function removeInfoAirAdd(numberEvent) {
    listInfoAir = listInfoAir.filter(e => e.ID !== numberEvent);
    RerenderInfoAirAgain();
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
            objVolumn.VolumeQty = 1;//$("#txtVolumeQty").val();
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
function RerenderVolumn() {
    $("#rowVolume").empty();
    var k = 1;
    for (var i = 0; i < listVolumn.length; i++) {
        $("#rowVolume").append('<div class="row" id="btnDeleteVolumn' + listVolumn[i].ID + '"><div class="col-1" style="margin: auto; ">VOLUME NO' + k + ':</div><div class="col-7"><div class="row"><div class="col-4"><input type="number" class="form-control" disable value="' + listVolumn[i].VolumeQty + '" id="txtVolumQty' + listVolumn[i].ID + '" /></div><div class="col-3" style="margin: auto; "><div id="txtVolumValue' + listVolumn[i].ID + '">' + listVolumn[i].VolumeText + '</div></div><div class="col-2" style="margin:auto">&nbsp;&nbsp;TARE W.(KG)</div><div class="col-3"><input type="number" class="form-control" onchange="OnchangeGorssWeightVolumeAndTruckType(' + listVolumn[i].ID + ',\'volume\')" value="' + listVolumn[i].GrossWeight + '" id="txtVolumGrossWeight' + listVolumn[i].ID + '" /></div></div></div><div class="col-2"><select class="form-control" multiple="multiple" onchange="onVolumeInvoice(' + listVolumn[i].ID + ')" id="cboVolumeInvoice' + listVolumn[i].ID + '"></select></div><div class="col-2"><button class="btn btn-danger" onclick="removeVolumn(' + listVolumn[i].ID + ')">DELETE</button></div></div>');
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
        //console.log(listVolumn);
        if (listVolumn.invoiceList != {}) {
            //console.log(Object.values(listVolumn[i].invoiceList));//Object.assign({}, listVolumn[i].invoiceList)
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
        $("#rowTruckType").append('<div class="row" id="btnDeleteTruckType' + listTruckType[i].ID + '"><div class="col-1" style="margin: auto;">TruckType NO' + k + ':</div><div class="col-7"><div class="row"><div class="col-4"><input type="number" class="form-control" disable value="' + listTruckType[i].TruckTypeQty + '" id="txtTruckTypeQty' + listTruckType[i].ID + '" /></div><div class="col-3" style="margin: auto;"><div id="txtTruckTypeValue' + listTruckType[i].ID + '">' + listTruckType[i].TruckTypeText + '</div></div><div class="col-2" style="margin:auto">&nbsp;&nbsp;TARE W.(KG)</div><div class="col-3"> <input type="number" class="form-control" onchange="OnchangeGorssWeightVolumeAndTruckType(' + listTruckType[i].ID + ',\'truckType\')" value="' + listTruckType[i].GrossWeight + '" id="txtTruckTypeGrossWeight' + listTruckType[i].ID + '" /></div></div></div><div class="col-2"><select class="form-control" multiple="multiple" onchange="onTruckInvoice(' + listTruckType[i].ID + ')" id="cboTruckInvoice' + listTruckType[i].ID + '"></select></div><div class="col-2"><button class="btn btn-danger" onclick="removeTruckType(' + listTruckType[i].ID + ')">DELETE</button></div></div>');
        k++;
        //ReCreate Multiple Combo
        $("#cboTruckInvoice" + listTruckType[i].ID).select2({
            placeholder: "Select a Invoice List",
            maximumSelectionLength: 999999
        });
        $('#cboTruckInvoice' + listTruckType[i].ID).empty().trigger('change');
        for (var k = 0; k < listInvoice.length; k++) {
            $('#cboTruckInvoice' + listTruckType[i].ID).append(new Option(listInvoice[k].InvoiceNumber, listInvoice[k].InvoiceNumber, false, false)).trigger('change');
        }
        //console.log(listVolumn);
        if (listTruckType.invoiceList != {}) {
            //console.log(Object.values(listVolumn[i].invoiceList));//Object.assign({}, listVolumn[i].invoiceList)
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
        //data: { objectCode: objectCode, dateOfMonth: dateOfMonth },
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
    $('#cboMultiplePlaceOfReceipt').empty().trigger("change");
    //$('#cboMultiPlaceOfDelivery').empty().trigger("change");
    var afterSelect = objProvinceType.filter(e => e.country === country);
    var newOptionPlaceOfLoading = [];
    var newOptionPlaceOfReceipt = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfLoading.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
        newOptionPlaceOfReceipt.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiPlaceOfLoading').append(newOptionPlaceOfLoading).trigger('change');
    $('#cboMultiplePlaceOfReceipt').append(newOptionPlaceOfReceipt).trigger('change');
    //$('#cboMultiPlaceOfDelivery').append(newOptionPlaceOfDelivery).trigger('change');
}

function getProvinceDeliveryByCountry(country) {
    $('#cboMultiPlaceOfDelivery').empty().trigger("change");
    $('#cboMultiPortOfDischarge').empty().trigger("change");
    var afterSelect = objProvinceType.filter(e => e.country === country);
    var newOptionPlaceOfDelivery = [];
    var newOptionPortOfDischarge = [];
    for (var i = 0; i < afterSelect.length; i++) {
        newOptionPlaceOfDelivery.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
        newOptionPortOfDischarge.push(new Option(afterSelect[i].name, afterSelect[i].code, false, false));
    }
    $('#cboMultiPlaceOfDelivery').append(newOptionPlaceOfDelivery).trigger('change');
    $('#cboMultiPortOfDischarge').append(newOptionPortOfDischarge).trigger('change');
}

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
    //var newTmpShipper = objGetShippers.filter(x => x.country.toString() === $("#cboOrigin option:selected").text());
    //OnRendercboSelect2(newTmpShipper, "txtShipper");
});
//Select Values Change In Destination
$('#cboDestination').on('change', function () {
    $("#txtShowDestination").val($("#cboDestination option:selected").text());
    overseaForwarder($("#cboDestination option:selected").text());
    var tmpSelectedValue = $("#cboOrigin").val();
    var newObjGetOrigins = objGetOrigins.filter(x => x.code.toString() !== $("#cboDestination").val());
    OnRendercbo(newObjGetOrigins, "cboOrigin");
    $("#cboOrigin").val(tmpSelectedValue);
    //var newTmpConsignees = objGetConsignees.filter(x => x.country.toString() === $("#cboDestination option:selected").text());
    //OnRendercboSelect2(newTmpConsignees, "txtConsignee");
    getProvinceDeliveryByCountry($("#cboDestination option:selected").text())
});
//On DateChange Loading
//$('#txtLoadingDate').on('change', function () {
//    $("#txtCrossBorderDate").prop('disabled', false);
//    $("#txtCrossBorderDate").attr("min", $("#txtLoadingDate").val());
//});
//On DateChange Delivery
//$('#txtCrossBorderDate').on('change', function () {
//    $("#txtEtaRequirement").prop('disabled', false);
//    $("#txtEtaRequirement").attr("min", $("#txtCrossBorderDate").val());
//});
//Select Values Change In JobNo.
var objServiceType = [];
var objProvinceType = [];
var objOverseaForwarderType = [];
var objOverseaTruckerType = [];
var objGetOrigins = [];
var objGetDestinations = [];
var objGetShippers = [];
var objGetConsignees = [];
function setterObjectType(obj, type) {
    if (type === 'Service') {
        objServiceType = obj;
    } else if (type === 'Province') {
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
        //var tmpOrigin=objGetOrigins.filter(x => x.code)
        //var newTmpConsignees = objGetConsignees.filter(x => x.country.toString() === $("#cboDestination option:selected").text());
        //OnRendercboSelect2(newTmpConsignees, "txtConsignee");
        getProvinceDeliveryByCountry($("#cboDestination option:selected").text());
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
    }
    else if ($("#cboSelectSeries").val() == 'EWTE') {
        $("#cboImportType").val("TE");
        var lsServiceType = objServiceType.filter(x => x.importType == "TE");
        $("#cboServiceType").empty();
        for (var i = 0; i < lsServiceType.length; i++) {
            $("#cboServiceType").append(new Option(lsServiceType[i].name, lsServiceType[i].code));
        }
        getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
        //var newTmpShipper = objGetShippers.filter(x => x.country.toString() === $("#cboOrigin option:selected").text());
        //OnRendercboSelect2(newTmpShipper, "txtShipper");
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
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
        //var newTmpShipper = objGetShippers.filter(x => x.country.toString() === $("#cboOrigin option:selected").text());
        //OnRendercboSelect2(newTmpShipper, "txtShipper");
        //var newTmpConsignees = objGetConsignees.filter(x => x.country.toString() === $("#cboDestination option:selected").text());
        //OnRendercboSelect2(newTmpConsignees, "txtConsignee");
        //Thai Border
        $('#cboThaiBorder').val([]).trigger("change");
        $("#cboThaiBorder").select2({
            placeholder: "Select a Thai Border",
            maximumSelectionLength: 1,
            width: '100%'
        });
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
        //$('#txtShipper').empty().trigger("change");
        //$('#txtConsignee').empty().trigger("change");

    }
}
function onChangeJobNo() {
    var value = $("#cboSelectSeries").val();
    var jobTypeName = $("#cboSelectSeries option:selected").attr("data-JobTypeName");
    var objServiceType = JSON.parse($("#cboSelectSeries option:selected").attr("data-obj"));
    $("#cboServiceType").empty();
    $("#cboServiceType").append('<option selected value="-1">Choose...</option>');
    for (var i = 0; i < objServiceType.length; i++) {
        $("#cboServiceType").append('<option selected value="' + objServiceType[i].code + '">' + objServiceType[i].name + '</option>');
    }
    $("#cboImportType").val(jobTypeName);
}

//Inits Select2
//Colorader
$("#txtColoader").select2({
    placeholder: "Select a Coloader",
    maximumSelectionLength: 1,
    width: '100%'
});
//CUSTOMER NAME
$("#txtCustomerName").select2({
    placeholder: "Select a Customer Name",
    maximumSelectionLength: 1,
    width: '100%'
});
//SHIPPER
$("#txtShipper").select2({
    placeholder: "Select a SHIPPER",
    maximumSelectionLength: 99999,
    width: '100%'
});
//CONSIGNEE
$("#txtConsignee").select2({
    placeholder: "Select a CONSIGNEE",
    maximumSelectionLength: 99999,
    width: '100%'
});
//SHIPPER LINE
$("#txtShippingLine").select2({
    placeholder: "Select a SHIPPER LINE",
    maximumSelectionLength: 99999,
    width: '100%'
});
//DEST. AGENT
$("#txtdestAgent").select2({
    placeholder: "Select a DEST. AGENT",
    maximumSelectionLength: 1,
    width: '100%'
});
//OVERSEA TRANSPORT
$("#cboMultiOverseaTrucker").select2({
    placeholder: "Select a OVERSEA TRANSPORT",
    maximumSelectionLength: 99999,
    width: '100%'
});
//PORT OF RECEIPT
$("#cboMultiplePlaceOfReceipt").select2({
    placeholder: "Select a PORT OF RECEIPT",
    maximumSelectionLength: 99999,
    width: '100%'
});
//PORT OF DISCHARGE
$("#cboMultiPortOfDischarge").select2({
    placeholder: "Select a PORT OF DISCHARGE",
    maximumSelectionLength: 99999,
    width: '100%'
});
//PLACE OF LOADING
$("#cboMultiPlaceOfLoading").select2({
    placeholder: "Select a PLACE OF LOADING",
    maximumSelectionLength: 99999,
    width: '100%'
});
//THAI FORWARDER
$("#cboThaiForwarder").select2({
    placeholder: "Select a THAI FORWARDER",
    maximumSelectionLength: 99999,
    width: '100%'
});
//PLACE OF DELIVERY
$("#cboMultiPlaceOfDelivery").select2({
    placeholder: "Select a PLACE OF DELIVERY",
    maximumSelectionLength: 99999,
    width: '100%'
});
//OVERSEA FORWARDER
$("#cboOverseaForwarder").select2({
    placeholder: "Select a OVERSEA FORWARDER",
    maximumSelectionLength: 99999,
    width: '100%'
});
//THAI BORDER
$("#cboThaiBorder").select2({
    placeholder: "Select a THAI BORDER",
    maximumSelectionLength: 99999,
    width: '100%'
});
//CY AT / CONTACT
//$("#txtCYatContact").select2({
//    placeholder: "Select a CY AT / CONTACT",
//    maximumSelectionLength: 99999,
//    width: '100%'
//});
//RETURN AT / CONTACT
//$("#txtReturnAtContact").select2({
//    placeholder: "Select a CY AT / CONTACT",
//    maximumSelectionLength: 99999,
//    width: '100%'
//});
//LOADING AT
$("#txtLoadingAt").select2({
    placeholder: "Select a LOADING AT",
    maximumSelectionLength: 99999,
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

//$("#txtSaleQuotations").change(function () {
//    var a = $("#txtSaleQuotations").val();
//    var selectCon = $('#txtSaleQuotations').find('option[value="' + a[0] + '"]').attr('data-CardName');
//    console.log(selectCon);

//    for (var i = 0; i < a.length; i++) {
//        if (selectCon !== $('#txtSaleQuotations').find('option[value="' + a[i] + '"]').attr('data-CardName')) {
//            alert("Can not select difference Customer Name");
//            a = a.filter(x => x !== a[i]);
//        }
//        $("#cboSalePerson").val($('#txtSaleQuotations').find('option[value="' + a[0] + '"]').attr('data-slpName'));
//        $("#cboSalePerson").attr('data-slpCode', $('#txtSaleQuotations').find('option[value="' + a[0] + '"]').attr('data-slpCode'))
//    }
//    $("#txtSaleQuotations").val(a).trigger('change.select2');
//});

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
//function formartDate(Newdate) {
//    let d = new Date(Newdate);
//    let ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(d);
//    let mo = new Intl.DateTimeFormat('en', { month: '2-digit' }).format(d);
//    let da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(d);
//    return `${ye}-${mo}-${da}`;
//}