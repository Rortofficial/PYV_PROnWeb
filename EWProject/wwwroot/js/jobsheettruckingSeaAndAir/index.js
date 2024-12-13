//Click Modal Item
function ClickShowSalesQuotation(value) {
    $('#ModalItemSalesQuotationJobSheet').attr("data-ListItemSalesQuotation", value);
}
//End Modal Item
$("#btnConfirmCosting").click(function () {
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        $("#btnConfirmCosting").text("UnConfirm Costing");
        confirmcosting = true;
    } else {
        $("#btnConfirmCosting").text("Confirm Costing");
        confirmcosting = false;
    }
    reRenderDataTableLine();
});

//End Set Object Item
// On Confirm Transaction
function onConfirmTransaction(url, urlPDF, urlUploadFile,draftDocEntry) {
    event.preventDefault();
    //alert("Work In Progress");
    //if ($("#txtCustomer").attr("data-SHIPPINGLINE") == "") {
    //    alert("Please Select DEST. AGENT in BookingSheet First Before Add Job Sheet Trucking");
    //    return;
    //}
    var objData = {};
    objData.ObjectHeader = {
        "CONFIRMBOOKINGID": $("#txtConfirmBookingID").val(),
        "CARDCODE": $("#txtCustomer").attr("data-cardCode"),
        "VendorCode": $("#txtCustomer").attr("data-SHIPPINGLINE"),
        "REMARKSCOSTING": $("#remarkForCosting").val(),
        "REMAKRSSELLING": $("#remarkForSelling").val(),
        "TOTALCOSTING": $("#txtTotalCosting").val(),
        "REBATE": $("#txtRebate").val(),
        "GRANDTOTALCOSTING": $("#txtGrandTotalCosting").val(),
        "TOTALSELLING": $("#txtTotalSelling").val(),
        "GRANDTOTALSELLING": $("#txtGrandTotalSelling").val(),
        "TOTALPROFIT": $("#txtProfit").val(),
        "USERCREATE": $("#UserID").attr("data-id"),
        "JOBNO": $("#txtJobNo").val(),
        "DutyTaxAmountCosting": $("#txtDutyTaxAmountCosting").val(),
        "DutyTaxAmountSelling": $("#txtDutyTaxAmountSelling").val(),
        "COSTINGCONFIRM": "N",
    };
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        objData.ObjectHeader.COSTINGCONFIRM = "N";
    } else {
        if (draftDocEntry == "-1") {
            alert("Please send to SAP first before Confirm!");
            return;
        }
        objData.ObjectHeader.COSTINGCONFIRM = "Y";
    }
    objData.ItemLineRevenue = [];
    objData.ItemLineCosting = [];
    for (var i = 0; i < listItemLineAdd.length; i++) {
        var totalSelling = 0;
        var totalCost = 0;
        if (listItemLineAdd[i].CurrencySelling === "THB") {
            totalSelling += parseFloat(listItemLineAdd[i].qtySelling * listItemLineAdd[i].priceSelling);
        } else {
            totalSelling += parseFloat(listItemLineAdd[i].qtySelling * (listItemLineAdd[i].priceSelling * listItemLineAdd[i].ExchangeRateAmountSelling));
        }
        if (listItemLineAdd[i].CurrencyCosting === "THB") {
            totalCost += parseFloat(listItemLineAdd[i].qtyCosting * listItemLineAdd[i].priceCosting);
        } else {
            totalCost += parseFloat(listItemLineAdd[i].qtyCosting * (listItemLineAdd[i].priceCosting * listItemLineAdd[i].ExchangeRateAmountCosting));
        }
        objData.ItemLineRevenue.push({
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceSelling,
            "Qty": listItemLineAdd[i].qtySelling,
            "ContainerType": listItemLineAdd[i].UomCode,
            "Currency": listItemLineAdd[i].CurrencySelling,
            "ExchangeRate": listItemLineAdd[i].ExchangeRateAmountSelling,
            "TotalBaht": totalSelling,
        });
        objData.ItemLineCosting.push({
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceCosting,
            "Qty": listItemLineAdd[i].qtyCosting,
            "ContainerType": listItemLineAdd[i].UomCodeCosting,
            "Currency": listItemLineAdd[i].CurrencyCosting,
            "ExchangeRate": listItemLineAdd[i].ExchangeRateAmountCosting,
            "TotalBaht": totalCost,
            "VendorCode": listItemLineAdd[i].vendorCode,
        });
    }
    if (confirm("Please Confirm Transaction Before Add Make sure it correct !")) {
        //alert("Work in Progress!");
        if (dataFile.getAll("MyUploader").length != 0) {
            $.ajax({
                url: urlUploadFile,
                type: "POST",
                data: dataFile,
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    objData.Attachment = data;
                    $.ajax({
                        url: url,
                        type: "POST",
                        data: {
                            postJobSheetTruckingRequest: objData,
                            draftdocument: draftDocEntry,
                        },
                        dataType: "JSON",
                        beforeSend: function () {
                            $('#Modalloder').modal('show');
                        },
                        success: function (data) {
                            $('#Modalloder').modal('hide');
                            $('#ModalSuccess').modal('show');
                            $('#ErrMsg').html("Success");
                            window.location.href = urlPDF + "?docEntry=" + data.docEntry;
                        },
                        error: function (erro) {
                            $('#Modalloder').modal('hide');
                            $('#ModalSuccess').modal('show');
                            $('#ErrMsg').html(erro.responseText);
                            return;
                        }
                    });
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    return;
                }
            });
        } else {
            $.ajax({
                url: url,
                type: "POST",
                data: {
                    postJobSheetTruckingRequest: objData,
                    draftdocument: draftDocEntry,
                },
                dataType: "JSON",
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html("Success");
                    window.location.href = urlPDF + "?docEntry=" + data.docEntry;
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    return;
                }
            });
        }
    }
}
//End On Confirm Transaction

// On Update Confirm Transaction
function onUpdateTransaction(url, urlPDF, urlUploadFile, docEntry) {
    //alert("Work In Progress");
    //if ($("#txtCustomer").attr("data-SHIPPINGLINE") == "") {
    //    alert("Please Select DEST. AGENT in BookingSheet First Before Add Job Sheet Trucking");
    //    return;
    //}
    var objData = {};
    objData.ObjectHeader = {
        "CONFIRMBOOKINGID": $("#txtConfirmBookingID").val(),
        "CARDCODE": $("#txtCustomer").attr("data-cardCode"),
        "SALESORDERDOCNUM": $("#txtCustomer").attr("data-SO"),
        "VendorCode": $("#txtCustomer").attr("data-SHIPPINGLINE"),
        "REMARKSCOSTING": $("#remarkForCosting").val(),
        "REMAKRSSELLING": $("#remarkForSelling").val(),
        "TOTALCOSTING": $("#txtTotalCosting").val(),
        "REBATE": $("#txtRebate").val(),
        "GRANDTOTALCOSTING": $("#txtGrandTotalCosting").val(),
        "TOTALSELLING": $("#txtTotalSelling").val(),
        "GRANDTOTALSELLING": $("#txtGrandTotalSelling").val(),
        "TOTALPROFIT": $("#txtProfit").val(),
        "USERCREATE": $("#UserID").attr("data-id"),
        "JOBNO": $("#txtJobNo").val(),
        "DutyTaxAmountCosting": $("#txtDutyTaxAmountCosting").val(),
        "DutyTaxAmountSelling": $("#txtDutyTaxAmountSelling").val(),
        "COSTINGCONFIRM": "N",
    };
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        objData.ObjectHeader.COSTINGCONFIRM = "N";
    } else {
        objData.ObjectHeader.COSTINGCONFIRM = "Y";
    }
    objData.ItemLineRevenue = [];
    objData.ItemLineCosting = [];
    for (var i = 0; i < listItemLineAdd.length; i++) {
        var totalSelling = 0;
        var totalCost = 0;
        if (listItemLineAdd[i].CurrencySelling === "THB") {
            totalSelling += parseFloat(listItemLineAdd[i].qtySelling * listItemLineAdd[i].priceSelling);
        } else {
            totalSelling += parseFloat(listItemLineAdd[i].qtySelling * (listItemLineAdd[i].priceSelling * listItemLineAdd[i].ExchangeRateAmountSelling));
        }
        if (listItemLineAdd[i].CurrencyCosting === "THB") {
            totalCost += parseFloat(listItemLineAdd[i].qtyCosting * listItemLineAdd[i].priceCosting);
        } else {
            totalCost += parseFloat(listItemLineAdd[i].qtyCosting * (listItemLineAdd[i].priceCosting * listItemLineAdd[i].ExchangeRateAmountCosting));
        }
        objData.ItemLineRevenue.push({
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceSelling,
            "Qty": listItemLineAdd[i].qtySelling,
            "ContainerType": listItemLineAdd[i].UomCode,
            "Currency": listItemLineAdd[i].CurrencySelling,
            "ExchangeRate": listItemLineAdd[i].ExchangeRateAmountSelling,
            "TotalBaht": totalSelling,
            "RefLineId": listItemLineAdd[i].RefLineId,
        });
        objData.ItemLineCosting.push({
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceCosting,
            "Qty": listItemLineAdd[i].qtyCosting,
            "ContainerType": listItemLineAdd[i].UomCodeCosting,
            "Currency": listItemLineAdd[i].CurrencyCosting,
            "ExchangeRate": listItemLineAdd[i].ExchangeRateAmountCosting,
            "TotalBaht": totalCost,
            "RefLineId": listItemLineAdd[i].RefLineId,
            "VendorCode": listItemLineAdd[i].vendorCode,
        });
    }
    if (confirm("Please Confirm Transaction Before Add Make sure it correct !")) {
        //alert("Work in Progress!");
        if (dataFile.getAll("MyUploader").length != 0) {
            $.ajax({
                url: urlUploadFile,
                type: "POST",
                data: dataFile,
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    objData.Attachment = data;
                    $.ajax({
                        url: url,
                        type: "PUT",
                        data: { updateJobSheetTruckingEdit: objData, jobDocEntry: docEntry },
                        dataType: "JSON",
                        beforeSend: function () {
                            $('#Modalloder').modal('show');
                        },
                        success: function (data) {
                            $('#Modalloder').modal('hide');
                            $('#ModalSuccess').modal('show');
                            $('#ErrMsg').html("Success");
                            window.location.href = urlPDF + "?docEntry=" + data.docEntry;
                        },
                        error: function (erro) {
                            $('#Modalloder').modal('hide');
                            $('#ModalSuccess').modal('show');
                            $('#ErrMsg').html(erro.responseText);
                            return;
                        }
                    });
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    return;
                }
            });
        } else {
            $.ajax({
                url: url,
                type: "PUT",
                data: { updateJobSheetTruckingEdit: objData, jobDocEntry: docEntry },
                dataType: "JSON",
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html("Success");
                    window.location.href = urlPDF + "?docEntry=" + data.docEntry;
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    return;
                }
            });
        }
    }
}
//End On Update Confirm Transaction

// On Update Draft Transaction
function onUpdateDraftTransaction(url, urlPDF, urlUploadFile,draftType,jobSheetDocEntry) {
    event.preventDefault();
    //alert("Work In Progress");
    //if ($("#txtCustomer").attr("data-SHIPPINGLINE") == "") {
    //    alert("Please Select DEST. AGENT in BookingSheet First Before Add Job Sheet Trucking");
    //    return;
    //}
    var objData = {};
    objData.ObjectHeader = {
        "CONFIRMBOOKINGID": $("#txtConfirmBookingID").val(),
        "CARDCODE": $("#txtCustomer").attr("data-cardCode"),
        "VendorCode": $("#txtCustomer").attr("data-SHIPPINGLINE"),
        "REMARKSCOSTING": $("#remarkForCosting").val(),
        "REMAKRSSELLING": $("#remarkForSelling").val(),
        "TOTALCOSTING": $("#txtTotalCosting").val(),
        "REBATE": $("#txtRebate").val(),
        "GRANDTOTALCOSTING": $("#txtGrandTotalCosting").val(),
        "TOTALSELLING": $("#txtTotalSelling").val(),
        "GRANDTOTALSELLING": $("#txtGrandTotalSelling").val(),
        "TOTALPROFIT": $("#txtProfit").val(),
        "USERCREATE": $("#UserID").attr("data-id"),
        "JOBNO": $("#txtJobNo").val(),
        "DutyTaxAmountCosting": $("#txtDutyTaxAmountCosting").val(),
        "DutyTaxAmountSelling": $("#txtDutyTaxAmountSelling").val(),
        "COSTINGCONFIRM": "N",
        "SALESORDERDOCNUM": soDocEntrty,
    };
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        objData.ObjectHeader.COSTINGCONFIRM = "N";
    } else {
        objData.ObjectHeader.COSTINGCONFIRM = "Y";
    }
    objData.ItemLineRevenue = [];
    objData.ItemLineCosting = [];
    for (var i = 0; i < listItemLineAdd.length; i++) {
        var totalSelling = 0;
        var totalCost = 0;
        if (listItemLineAdd[i].CurrencySelling === "THB") {
            totalSelling += parseFloat(listItemLineAdd[i].qtySelling * listItemLineAdd[i].priceSelling);
        } else {
            totalSelling += parseFloat(listItemLineAdd[i].qtySelling * (listItemLineAdd[i].priceSelling * listItemLineAdd[i].ExchangeRateAmountSelling));
        }
        if (listItemLineAdd[i].CurrencyCosting === "THB") {
            totalCost += parseFloat(listItemLineAdd[i].qtyCosting * listItemLineAdd[i].priceCosting);
        } else {
            totalCost += parseFloat(listItemLineAdd[i].qtyCosting * (listItemLineAdd[i].priceCosting * listItemLineAdd[i].ExchangeRateAmountCosting));
        }
        objData.ItemLineRevenue.push({
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceSelling,
            "Qty": listItemLineAdd[i].qtySelling,
            "ContainerType": listItemLineAdd[i].UomCode,
            "Currency": listItemLineAdd[i].CurrencySelling,
            "ExchangeRate": listItemLineAdd[i].ExchangeRateAmountSelling,
            "TotalBaht": totalSelling,
        });
        objData.ItemLineCosting.push({
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceCosting,
            "Qty": listItemLineAdd[i].qtyCosting,
            "ContainerType": listItemLineAdd[i].UomCodeCosting,
            "Currency": listItemLineAdd[i].CurrencyCosting,
            "ExchangeRate": listItemLineAdd[i].ExchangeRateAmountCosting,
            "TotalBaht": totalCost,
            "VendorCode": listItemLineAdd[i].vendorCode,
        });
    }
    if (confirm("Please Confirm Transaction Before Add Make sure it correct !")) {
        //alert("Work in Progress!");
        if (dataFile.getAll("MyUploader").length != 0) {
            $.ajax({
                url: urlUploadFile,
                type: "POST",
                data: dataFile,
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    objData.Attachment = data;
                    $.ajax({
                        url: url,
                        type: "PUT",
                        data: {
                            postJobSheetTruckingRequest: objData,
                            draftdocument: draftType,
                            jobSheetDocEntry: jobSheetDocEntry,
                        },
                        dataType: "JSON",
                        beforeSend: function () {
                            $('#Modalloder').modal('show');
                        },
                        success: function (data) {
                            $('#Modalloder').modal('hide');
                            $('#ModalSuccess').modal('show');
                            $('#ErrMsg').html("Success");
                            window.location.href = urlPDF + "?docEntry=" + data.docEntry;
                        },
                        error: function (erro) {
                            $('#Modalloder').modal('hide');
                            $('#ModalSuccess').modal('show');
                            $('#ErrMsg').html(erro.responseText);
                            return;
                        }
                    });
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    return;
                }
            });
        } else {
            $.ajax({
                url: url,
                type: "PUT",
                data: {
                    postJobSheetTruckingRequest: objData,
                    draftdocument: draftType,
                    jobSheetDocEntry: jobSheetDocEntry,
                },
                dataType: "JSON",
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html("Success");
                    window.location.href = urlPDF + "?docEntry=" + data.docEntry;
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    return;
                }
            });
        }
    }
}
//End On Update Draft Transaction