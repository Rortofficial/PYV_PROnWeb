function onChangeContainerType(id, action) {
    var a = $("#cboSelectType-" + id).val();
    //alert(id);
    if (a === "EXCHANGE") {
        $("#exchangeTypeID-" + id).css("display", "");
        if (action != "U") {
            $("#txtContainer2-" + id).val("");
            $("#cboContainerExisting-" + id).val("").trigger("change");
        }
        
    } else {
        $("#exchangeTypeID-" + id).css("display", "none");
        if (action != "U") {
            $("#txtContainer2-" + id).val("");
            $("#cboContainerExisting-" + id).val("").trigger("change");
        }
    }
    //alert("Hello");
}
function onChangeOptionContainer(id) {
    var a = $("#cboSelectOptionContainer-" + id).val();
    if (a === "unused") {
        $("#Panel20Ft-" + id).css("display", "none");
        $("#PanelTruck20Ft-" + id).css("display", "none");
    } else {
        $("#Panel20Ft-" + id).css("display", "");
        $("#PanelTruck20Ft-" + id).css("display", "");
    }
}

function clickPRonWeb (id) {
    var url = $("#"+id+" option:selected").attr("data-url");
    var param = $("#" + id +" option:selected").attr("data-paramater");
    var lsPRID = $("#" + id +" option:selected").attr("data-listPRID");
    var lsADID = $("#" + id +" option:selected").attr("data-listADID");
    var listIDName = $("#" + id + " option:selected").attr("data-listIDName");
    var listPRTemplate = $("#" + id + " option:selected").attr("data-listPRTemplate");
    var listADtemplate = $("#" + id + " option:selected").attr("data-listADTemplate");
    var listidvendor = $("#cboVendorCode-" + $("#" + id + " option:selected").attr("data-listidvendor")).val()[0];
    var itemCosting = $("#txtTruckCostTHB-" + $("#" + id + " option:selected").attr("data-listidvendor")).val();
    if ($("#" + id +"").val() !== "-1") {
        $.ajax({
            url: url,
            data: {
                data: param,
                listPRID: lsPRID,
                listAdvanceID: lsADID,
                listIDName: listIDName,
                listPRTemplate: listPRTemplate,
                listADtemplate: listADtemplate,
                listidvendor: listidvendor,
                itemCosting: itemCosting,
                prAction: 'Add'
            },
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                if ($("#" + id +" option:selected").val() === "ADVANCE") {
                    $("#" + listADtemplate).html(data);
                } else {
                    $("#" + listPRTemplate).html(data);
                }
            },
            error: function (erro) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html(erro.responseText);
            }
        });
    } else {
        $('#PRORADVANCE').empty();
    }
}
function onChangeTruck(id, valueDriverName1, valueDriverName2, valueVendorCode, valueLocation) {
    $("#cboDriverName2" + id).val(parseInt(valueDriverName1)).trigger("change");
    $("#cboDriverName1" + id).val(valueDriverName2).trigger("change");
    $("#cboVendorCode" + id).val(valueVendorCode).trigger("change");
    $("#cboProvinceTruck" + id).val(valueLocation).trigger("change");
}

function onInitPlateNoMultiple(id, url) { 
    $('#cboProvinceTruck-'+id).val([]).trigger('change');
    //$.ajax({
    //    url: url,
    //    type: "GET",
    //    data: { Province: ($("#cboProvinceTruck-" + id).val()[0] == undefined ?'': $("#cboProvinceTruck-" + id).val()[0]) },
    //    dataType: "JSON",
    //    beforeSend: function () {
    //        $('#Modalloder').modal('show');
    //    },
    //    success: function (data) {
    //        $('#Modalloder').modal('hide');
    //        $('#cboTruckNo-'+id).empty().trigger("change");
    //        for (var i = 0; i < data.data.length; i++) {
    //            $('#cboTruckNo-'+id).append("<option data-obj='" + JSON.stringify(data.data[i]) + "' value='" + data.data[i].code + "'>" + data.data[i].name + "</option>");
    //        }
    //        $('#cboTruckNo-'+id).trigger('change');
    //        //$('#cboTruckNo-'+id).removeAttr("disabled");
    //    },
    //    error: function (erro) {
    //        $('#Modalloder').modal('hide');
    //        $('#ModalSuccess').modal('show');
    //        $('#ErrMsg').html(erro.responseText);
    //    }
    //});
}
function onEditConfirmBookingSheet(id,data) {
    var obj = data;//JSON.parse(data);

    for (var i = 0; i < obj.length; i++) {
        $("#cboSelectType-" + id).val(obj[i].type);
        $("#cboContainerNoTEST-" + id).val(obj[i].containerNo).trigger("change");
        //$("#cboTruckNo-" + id).val(obj[i].truckPlateNo).trigger("change");
        //$("#cboTruckNo-" + id).val(obj[i].truckPlateNo).trigger("change");
        $("#cboDriverName1-" + id).val(obj[i].driver1).trigger("change");
        $("#cboDriverName2-" + id).val(obj[i].driver2).trigger("change");
        $("#cboVendorCode-" + id).val(obj[i].vendorCode).trigger("change");
        $("#txtTruckCostTHB-" + id).val(obj[i].truckCostTHB);
        $("#txtSealNo1-" + id).val(obj[i].sealNo1);
        $("#txtSealNo2-" + id).val(obj[i].sealNo2);
        $("#cboTypeExchangeType-" + id).val("Existing");
        $("#cboContainerExisting-" + id).val(obj[i].containerNo2).trigger("change");
        $("#txtOwner2-" + id).val(obj[i].owner2).trigger("change");
    }
}
