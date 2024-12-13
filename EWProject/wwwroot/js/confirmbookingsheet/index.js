var ListTruckInformation = [];
function setterListTruckInformation(obj) {
    for (var i = 1; i <= obj.length; i++) {
        for (var j = 1; j <= obj[i - 1].qty; j++) {
            var objTruckInformation = {};
            var listContainer = [];
            objTruckInformation.No = j + "-" + i;
            for (var k = 1; k <= obj[i - 1].containerNumber; k++) {
                var objContainer = {};
                objContainer.ContainerNo = j + '-' + k + '-' + i;
                listContainer.push(objContainer);
            }
            objTruckInformation.ListContainer = listContainer;
            ListTruckInformation.push(objTruckInformation);
        }
    }
}
function OnSubitConfirmBookingSheet(url, urlPDF) {
    event.preventDefault();
    //Add Line List Of Truck Line
    var ListOfTruckLine = [];
    var k = 1;
    for (var i = 0; i < containerType.length; i++) {
        //for (var k = 1; k <= containerType[i].ContainerQty; k++) {
        if ($("#hideTypeID-" + containerType[i].Volumne + '-' + k).val() === "C") {
            if ($("#cboSelectType-" + containerType[i].Volumne + '-' + k).val() == '') {
                alert("Please Select Type of Container!!");
                return;
            } else if ($("#cboContainerNoTEST-" + containerType[i].Volumne + '-' + k).val() == '') {
                alert("Please Select ContainerNo!!");
                return;
            }
        }
        if ($("#cboProvinceTruck-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Truck Province!!");
                return;
            }
        } else if ($("#TruckPlateNo-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Truck Plate No!!");
                return;
            }
        } else if ($("#cboDriverName1-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Driver1!");
                return;
            }
        } else if ($("#cboVendorCode-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Vendor!");
                return;
            }
        } else {                       
        }
        var objPurchaseRequest;
        var objAdvancePayment;
        try {
            eval("objPurchaseRequest = pr" + containerType[i].Volumne + '' + k + ";");
            eval("objAdvancePayment = ad" + containerType[i].Volumne + '' + k + ";");
        } catch (e) {
            eval("objPurchaseRequest = [];");
            eval("objAdvancePayment = [];");
        }
        if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "unused") {
            ListOfTruckLine.push({
                "ContainerType": $("#txtContainerType-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerOption": $("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val(),
                "BookingLineId": containerType[i].LineId,
            });
        } else {
            for (var objectPurchaseIndex = 0; objectPurchaseIndex < objPurchaseRequest.length; objectPurchaseIndex++) {
                for (var indexPurchase = 0; indexPurchase < objPurchaseRequest[objectPurchaseIndex].Lines.length; indexPurchase++) {
                    try {
                        objPurchaseRequest[objectPurchaseIndex].Lines[indexPurchase].Amount.replace(",", "");
                    } catch {
                        objPurchaseRequest[objectPurchaseIndex].Lines[indexPurchase].amount.replace(",", "");
                    }
                }
            }
            for (var objAdvancePaymentIndex = 0; objAdvancePaymentIndex < objAdvancePayment.length; objAdvancePaymentIndex++) {
                for (var indexAdvancePayment = 0; indexAdvancePayment < objAdvancePayment[objAdvancePaymentIndex].Lines.length; indexAdvancePayment++) {
                    try {
                        objAdvancePayment[objAdvancePaymentIndex].Lines[indexAdvancePayment].Amount.replace(",", "");
                    } catch {
                        objAdvancePayment[objAdvancePaymentIndex].Lines[indexAdvancePayment].amount.replace(",", "");
                    }
                }
            }
            ListOfTruckLine.push({
                "Type": $("#cboSelectType-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerType": $("#txtContainerType-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerNo": ($("#cboContainerNoTEST-" + containerType[i].Volumne + '-' + k).val() === undefined) ? '' : $("#cboContainerNoTEST-" + containerType[i].Volumne + '-' + k).val()[0],
                "Owner": $("#txtOwner-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerNo2": ($("#cboTypeExchangeType-" + containerType[i].Volumne + '-' + k).val() == "New") ? $("#txtContainer2-" + containerType[i].Volumne + '-' + k).val() : $("#cboContainerExisting-" + containerType[i].Volumne + '-' + k).val(),
                "ExchangeType": $("#cboTypeExchangeType-" + containerType[i].Volumne + '-' + k).val(),
                "Owner2": $("#txtOwner2-" + containerType[i].Volumne + '-' + k).val(),
                "GrossWeight": $("#txtGW-" + containerType[i].Volumne + '-' + k).val(),
                "Yard": $("#txtYard-" + containerType[i].Volumne + '-' + k).val(),
                "FCL_LCL": $("#txtFCLorLCL-" + containerType[i].Volumne + '-' + k).val(),
                "LOLO_Unloading": $("#txtLOLOorUNLOADING-" + containerType[i].Volumne + '-' + k).val(),
                "TruckProvince": $("#cboProvinceTruck-" + containerType[i].Volumne + '-' + k).val(),
                "TruckPlateNo": $("#cboTruckNo-" + containerType[i].Volumne + '-' + k).val(),
                "TruckType": $("#txtTrcukType-" + containerType[i].Volumne + '-' + k).val(),
                "Brand": $("#txtBrand-" + containerType[i].Volumne + '-' + k).val(),
                "TruckCode": $("#cboTruckNo-" + containerType[i].Volumne + '-' + k).val(),
                "Color": $("#txtColor-" + containerType[i].Volumne + '-' + k).val(),
                "TrailerProvince": $("#txtTralerProvince-" + containerType[i].Volumne + '-' + k).val(),
                "TrailerPlate": $("#txtTralerNo-" + containerType[i].Volumne + '-' + k).val()[0],
                "TrailerType": $("#txtTrailerType-" + containerType[i].Volumne + '-' + k).val(),
                "DriverCode1": $("#cboDriverName1-" + containerType[i].Volumne + '-' + k).val()[0],
                "TPNo1": $("#txtTPNo1-" + containerType[i].Volumne + '-' + k).val(),
                "IDCard1": $("#txtIDCard1-" + containerType[i].Volumne + '-' + k).val(),
                "DriverLicense1": $("#txtDriverLicense1-" + containerType[i].Volumne + '-' + k).val(),
                "DriverCode2": $("#cboDriverName2-" + containerType[i].Volumne + '-' + k).val()[0],
                "TPNo2": $("#txtTPNo2-" + containerType[i].Volumne + '-' + k).val(),
                "IDCard2": $("#txtIDCard2-" + containerType[i].Volumne + '-' + k).val(),
                "DriverLicense2": $("#txtDriverLicense2-" + containerType[i].Volumne + '-' + k).val(),
                "VendorCode": $("#cboVendorCode-" + containerType[i].Volumne + '-' + k).val(),
                "TruckCostTHB": $("#txtTruckCostTHB-" + containerType[i].Volumne + '-' + k).val().replace(",",""),
                "SealNo1": $("#txtSealNo1-" + containerType[i].Volumne + '-' + k).val(),
                "SealNo2": $("#txtSealNo2-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerOption": $("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val(),
                "BookingLineId": containerType[i].LineId,
                "ListPurchaseRequest": objPurchaseRequest,
                "ListAdvancePayment": objAdvancePayment
            });
        }
        k++;
        //}
    }
    if (confirm("Please confirm transaction make sure your data is correct before send to SAP123")) {
        var data = {};
        data.BookingID = $("#txtBookingID").val();
        data.CreateUser = $("#UserID").attr("data-id");
        data.CSName = $("#CSName").val();
        data.Remarks = $("#remarks").val();
        data.CardCode = $("#txtCardCode").val();
        data.PriceList = $("#cboPriceList").val()[0];
        data.SlpCode = $("#txtSplPerson").attr("data-slpCode");
        data.Destination = $("#txtDestination").val();
        data.Lines = ListOfTruckLine;
        $.ajax({
            url: url,
            type: "POST",
            data: { postBookingSheetRequest: data },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                window.location.href = urlPDF;
                //window.location.href = urlPDF + "?docEntry=" + data.docEntry;
            },
            error: function (erro) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html(erro.responseText);
            }
        });
    }
}
//Cost Type Information
var ListCostType = [];
var k = 1;
$("#btnAddCostType").click(function () {
    if ($("#txtCostType").attr("data-CostTypeCode") != '' && $("#txtAmountCostType").val() != '') {
        objCostType = {};
        objCostType.No = k;
        objCostType.ItemCode = $("#txtCostType").attr("data-CostTypeCode");
        objCostType.ItemNameCost = $("#txtCostType").val();
        objCostType.ItemPrice = $("#txtAmountCostType").val();
        ListCostType.push(objCostType);
        k++;
        $("#txtCostType").val("");
        $("#txtCostType").attr("data-CostTypeCode", "");
        $("#txtAmountCostType").val(0);
        reRenderCostType();
    }
});
function reRenderCostType() {
    $("#ListCostType").empty();
    for (var i = 0; i < ListCostType.length; i++) {
        $("#ListCostType").append('<div class="row">' +
            '<div class="col-4">' +
            ListCostType[i].ItemCode +
            '</div>' +
            '<div class="col-4">' +
            ListCostType[i].ItemNameCost +
            '</div>' +
            '<div class="col-2">' +
            ListCostType[i].ItemPrice +
            '</div>' +
            '<div class="col-2">' +
            '<button id="btnAddCostType" onclick="DeleteCostType(' + ListCostType[i].No + ')" class="float-end btn btn-danger">Delete</button>' +
            '</div>' +
            '</div>');
    }
}
function DeleteCostType(Id) {
    ListCostType = ListCostType.filter(e => e.No !== Id);
    reRenderCostType();
}
function OnchangeCboEdit(id,value) {
    $("#cboContainerNoTEST-" + id).val(value).trigger('change');
}
//End Cost Type



function OnUpdateConfirmBookingSheet(url, urlPDF) {
    //Add Line List Of Truck Line
    var ListOfTruckLine = [];
    var k = 1;
    for (var i = 0; i < containerType.length; i++) {
        //for (var k = 1; k <= containerType[i].ContainerQty; k++) {
        if ($("#hideTypeID-" + containerType[i].Volumne + '-' + k).val() === "C") {
            if ($("#cboSelectType-" + containerType[i].Volumne + '-' + k).val() == '') {
                alert("Please Select Type of Container!!");
                return;
            } else if ($("#cboContainerNoTEST-" + containerType[i].Volumne + '-' + k).val() == '') {
                alert("Please Select ContainerNo!!");
                return;
            }
        }
        if ($("#cboProvinceTruck-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Truck Province!!");
                return;
            }
        } else if ($("#TruckPlateNo-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Truck Plate No!!");
                return;
            }
        } else if ($("#cboDriverName1-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Driver1!");
                return;
            }
        } else if ($("#cboVendorCode-" + containerType[i].Volumne + '-' + k).val() == '') {
            if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "-1") {
                alert("Please Select Vendor!");
                return;
            }
        } else {
        }
        //var objPurchaseRequest;
        //var objAdvancePayment;
        //try {
        //    eval("objPurchaseRequest = pr" + containerType[i].Volumne + '' + k + ";");
        //    eval("objAdvancePayment = ad" + containerType[i].Volumne + '' + k + ";");
        //} catch (e) {
        //    //console.log(e);
        //    eval("objPurchaseRequest = [];");
        //    eval("objAdvancePayment = [];");
        //}
        if ($("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val() == "unused") {
            ListOfTruckLine.push({
                "CONTAINERTYPE": $("#txtContainerType-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerOption": $("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val(),
                "BookingLineId": containerType[i].LineId,
            });
        } else {
            ListOfTruckLine.push({
                "TYPE": $("#cboSelectType-" + containerType[i].Volumne + '-' + k).val(),
                "CONTAINERTYPE": $("#txtContainerType-" + containerType[i].Volumne + '-' + k).val(),
                "CONTAINERNO": ($("#cboContainerNoTEST-" + containerType[i].Volumne + '-' + k).val() === undefined) ? '' : $("#cboContainerNoTEST-" + containerType[i].Volumne + '-' + k).val()[0],
                "OWNER": $("#txtOwner-" + containerType[i].Volumne + '-' + k).val(),
                "ExchangeType": $("#cboTypeExchangeType-" + containerType[i].Volumne + '-' + k).val(),
                "CONTAINERNO2": ($("#cboTypeExchangeType-" + containerType[i].Volumne + '-' + k).val() == "New") ? $("#txtContainer2-" + containerType[i].Volumne + '-' + k).val() : $("#cboContainerExisting-" + containerType[i].Volumne + '-' + k).val(),
                "OWNER2": $("#txtOwner2-" + containerType[i].Volumne + '-' + k).val(),
                "GROSSWEIGHT": $("#txtGW-" + containerType[i].Volumne + '-' + k).val(),
                "YARD": $("#txtYard-" + containerType[i].Volumne + '-' + k).val(),
                "FCL_LCL": $("#txtFCLorLCL-" + containerType[i].Volumne + '-' + k).val(),
                "LOLO_UNLOADING": $("#txtLOLOorUNLOADING-" + containerType[i].Volumne + '-' + k).val(),
                "TRUCKPROVINCE": $("#cboProvinceTruck-" + containerType[i].Volumne + '-' + k).val(),
                "TRUCKPLATENO": $("#cboTruckNo-" + containerType[i].Volumne + '-' + k).val(),
                "TRUCKTYPE": $("#txtTrcukType-" + containerType[i].Volumne + '-' + k).val(),
                "BRAND": $("#txtBrand-" + containerType[i].Volumne + '-' + k).val(),
                "TRUCKCODE": $("#cboTruckNo-" + containerType[i].Volumne + '-' + k).val(),
                "COLOR": $("#txtColor-" + containerType[i].Volumne + '-' + k).val(),
                "TRAILERPROVINCE": $("#txtTralerProvince-" + containerType[i].Volumne + '-' + k).val(),
                "TRAILERPLATE": $("#txtTralerNo-" + containerType[i].Volumne + '-' + k).val()[0],
                "TRAILERTYPE": $("#txtTrailerType-" + containerType[i].Volumne + '-' + k).val(),
                "DRIVER1": $("#cboDriverName1-" + containerType[i].Volumne + '-' + k).val()[0],
                "TPNO1": $("#txtTPNo1-" + containerType[i].Volumne + '-' + k).val(),
                "IDCARD1": $("#txtIDCard1-" + containerType[i].Volumne + '-' + k).val(),
                "DRIVERLICENSE1": $("#txtDriverLicense1-" + containerType[i].Volumne + '-' + k).val(),
                "DRIVER2": $("#cboDriverName2-" + containerType[i].Volumne + '-' + k).val()[0],
                "TPNO2": $("#txtTPNo2-" + containerType[i].Volumne + '-' + k).val(),
                "IDCARD2": $("#txtIDCard2-" + containerType[i].Volumne + '-' + k).val(),
                "DRIVERLICENSE2": $("#txtDriverLicense2-" + containerType[i].Volumne + '-' + k).val(),
                "VENDORCODE": $("#cboVendorCode-" + containerType[i].Volumne + '-' + k).val(),
                "TRUCKCOSTTHB": $("#txtTruckCostTHB-" + containerType[i].Volumne + '-' + k).val().replace(",",""),
                "SEALNO1": $("#txtSealNo1-" + containerType[i].Volumne + '-' + k).val(),
                "SEALNO2": $("#txtSealNo2-" + containerType[i].Volumne + '-' + k).val(),
                "ContainerOption": $("#cboSelectOptionContainer-" + containerType[i].Volumne + '-' + k).val(),
                "BookingLineId": containerType[i].LineId,
                //"ListPurchaseRequest": objPurchaseRequest,
                //"ListAdvancePayment": objAdvancePayment
            });
        }
        k++;
        //}
    }
    if (confirm("Please confirm transaction make sure your data is correct before send to SAP")) {
        var data = {};
        data.ConfirmBookingID = $("#txtBookingID").val();
        data.CreateUser = $("#UserID").attr("data-id");
        data.CSName = $("#CSName").val();
        data.Remarks = $("#remarks").val();
        data.PriceList = $("#cboPriceList").val();
        data.Destination = $("#txtDestination").val();
        data.Lines = ListOfTruckLine;
        $.ajax({
            url: url,
            type: "PUT",
            data: { putBookingSheetRequest: data },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                //window.location.href = urlPDF;
                window.location.href = urlPDF + "?docEntry=" + data.docEntry;
            },
            error: function (erro) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html(erro.responseText);
            }
        });
    }
}