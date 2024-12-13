/*import { event } from "jquery";*/

//Click Modal Item
function ClickShowSalesQuotation(value) {
    $('#ModalItemSalesQuotationJobSheet').attr("data-ListItemSalesQuotation", value);
}
//End Modal Item
//Set Object Item to Array
//var objIDItem = [];
//function setterListItemSalesQuotation(obj) {
//    for (var i = 0; i < obj.length; i++) {
//        objIDItem.push({
//            "id": (obj[i].itemType == "COSTING") ? "txt-c-" + obj[i].itemCode :"txt-" + obj[i].itemCode,
//            "type": obj[i].itemType
//        });
//    }
//}
function reCalculate(itemCode) {
    var obj = {};
    for (var i = 0; i < listItemLineAdd.length; i++) {
        if (listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex === itemCode) {
            obj.itemCode = $("#lbl-txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).attr("data-itemcode");
            obj.itemName = $("#lbl-txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).text();
            obj.priceCosting = $("#txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val().replaceAll(",", "");//parseFloat()
            obj.priceSelling = $("#txt-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val().replaceAll(",", "");//parseFloat()
            obj.qtyCosting = parseInt($("#txt-q-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
            obj.qtySelling = parseInt($("#txt-s-q-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
            obj.lineIndex = listItemLineAdd[i].lineIndex;
            obj.itemType = listItemLineAdd[i].itemType;
            obj.UomCodeList = listItemLineAdd[i].UomCodeList;
            obj.UomCode = $('#cboUomCode-' + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val()[0],
            obj.UomGroupList = listItemLineAdd[i].UomGroupList;
            obj.LineNum = listItemLineAdd[i].LineNum;
            obj.lineNum = listItemLineAdd[i].lineNum;
            listItemLineAdd[i] = obj;
        }
    }
    //console.log(listItemLineAdd);
    //Costing
    var totalCost = 0;
    //var objCosting = objIDItem.filter(e => e.type === 'COSTING');
    for (var i = 0; i < listItemLineAdd.length; i++) {
        if ($("#txt-q-" + itemCode + listItemLineAdd[i].lineIndex).val() !== 0) {
            totalCost = totalCost + (listItemLineAdd[i].qtyCosting * listItemLineAdd[i].priceCosting);
        }
    }
    var totalAfterDiscountCost = 0;
    totalAfterDiscountCost = totalCost + parseFloat(($("#txtRebate").val().replaceAll(",", "") == "") ? 0 : $("#txtRebate").val().replaceAll(",", ""));
    $("#txtTotalCosting").val(totalCost);
    $("#txtGrandTotalCosting").val(totalAfterDiscountCost);
    $("#txtTotalCostingTHBBase").val((totalCost * $("#cboCurrencyItemCosting option:selected").attr("data-exchangebase")).toFixed(2));
    $("#txtGrandTotalCostingUSD").val((totalAfterDiscountCost * $("#cboCurrencyItemCosting option:selected").attr("data-exchangebase")).toFixed(2));

    //Selling
    var totalSelling = 0;
    for (var i = 0; i < listItemLineAdd.length; i++) {
        if ($("#txt-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val() !== 0) {
            totalSelling = totalSelling + (listItemLineAdd[i].qtySelling * listItemLineAdd[i].priceSelling);
        }
    }
    $("#txtTotalSelling").val(totalSelling.toFixed(2));
    $("#txtGrandTotalSelling").val(totalSelling.toFixed(2));
    $("#txtTotalSellingTHBBase").val((totalSelling * $("#cboCurrencyItemSelling option:selected").attr("data-exchangebase")).toFixed(2));
    $("#txtGrandTotalSellingUSD").val((totalSelling * $("#cboCurrencyItemSelling option:selected").attr("data-exchangebase")).toFixed(2));
    $("#txtProfit").val(($("#txtGrandTotalSellingUSD").val() - $("#txtGrandTotalCostingUSD").val()).toFixed(2));
    formartNumber('txtTotalCosting');
    formartNumber('txtTotalSelling');
    formartNumber('txtTotalCostingTHBBase');
    formartNumber('txtTotalSellingTHBBase');
    formartNumber('txtRebate');
    formartNumber('txtGrandTotalCosting');
    formartNumber('txtGrandTotalSelling');
    formartNumber('txtGrandTotalCostingUSD');
    formartNumber('txtGrandTotalSellingUSD');
    formartNumber('txtProfit');
    renderItemList();
}
$("#btnConfirmCosting").click(function () {
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        $("#btnConfirmCosting").text("UnConfirm Costing");
        renderItemList("1");
    } else {
        $("#btnConfirmCosting").text("Confirm Costing");
        renderItemList();
    }
});

$("#btnConfirmSelling").click(function () {
    if ($("#btnConfirmSelling").text() == "Confirm Selling") {
        $("#btnConfirmSelling").text("UnConfirm Selling");
    } else {
        $("#btnConfirmSelling").text("Confirm Selling");
    }
});

//End Set Object Item
// On Confirm Transaction
function onConfirmTransaction(url, urlPDF, urlUploadFile, draftdocument) {
    event.preventDefault();
    //alert("Work In Progress");
    var objData = {};
    var objItemSellLine = [];
    var objItemCostLine = [];
    if ($("#cboCurrencyItemCosting").val()[0] === undefined) {
        alert("Select a Currency For Costing!")
        return;
    }
    if ($("#cboCurrencyItemSelling").val()[0] === undefined) {
        alert("Select a Currency For Selling!")
        return;
    }
    //End AddValue to ItemCosting Line
    objData.ConfirmBookingID = $("#txtConfirmBookingID").val();
    objData.CardCode = $("#txtSaleQuotation option:selected").attr("data-cardCode");//$("#txtCO").attr("data-cocode");
    objData.SaleQuotationDocNum = $("#txtSaleQuotation").val();
    objData.TuckWayBillBy = $("#txtTruckWayBillBy").val();
    objData.CurrencyCosting = $("#cboCurrencyItemCosting").val()[0];
    objData.CurrencySelling = $("#cboCurrencyItemSelling").val()[0];
    objData.RemarksCosting = $("#remarkForCosting").val();
    objData.RemarksSelling = $("#remarkForSelling").val();
    objData.TotalCosting = $("#txtTotalCosting").val().replaceAll(",", "");
    objData.Rebate = $("#txtRebate").val().replaceAll(",", "");
    objData.GrandTotalCosting = $("#txtGrandTotalCosting").val().replaceAll(",", "");
    objData.GrandTotalCostingUSD = $("#txtGrandTotalCostingUSD").val().replaceAll(",", "");
    objData.TotalSelling = $("#txtTotalSelling").val().replaceAll(",", "");
    objData.GrandTotalSelling = $("#txtGrandTotalSelling").val().replaceAll(",", "");
    objData.GrandTotalSellingUSD = $("#txtGrandTotalSellingUSD").val().replaceAll(",", "");
    objData.TotalProfit = $("#txtProfit").val().replaceAll(",", "");
    objData.UserCreate = $("#UserID").attr("data-id");
    objData.JobNo = $("#txtJobNo").val();
    objData.DutyTaxAmountCosting = $("#txtDutyTaxAmountCosting").val().replaceAll(",", "");
    objData.DutyTaxAmountSelling = $("#txtDutyTaxAmountSelling").val().replaceAll(",", "");
    objData.AdvanceAmountCosting = $("#txtAdvanceAmountCosting").val();
    objData.AdvanceAmountSelling = $("#txtAdvanceAmountSelling").val();
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        objData.ConfirmCostingButton = "N";
    } else {
        objData.ConfirmCostingButton = "Y";
    }
    var objRow = [];
    for (var i = 0; i < listItemLineAdd.length; i++) {        
        objRow.push({
            "LineNum": listItemLineAdd[i].LineNum,
            "UomCode": listItemLineAdd[i].UomCode,
            "UomCodeList": listItemLineAdd[i].UomCodeList,
            "itemCode": listItemLineAdd[i].itemCode,
            "itemName": listItemLineAdd[i].itemName,
            "itemType": listItemLineAdd[i].itemType,
            "lineIndex": listItemLineAdd[i].lineIndex,
            "lineNum": listItemLineAdd[i].lineNum,
            "priceCosting": listItemLineAdd[i].priceCosting,
            "priceSelling": listItemLineAdd[i].priceSelling,
            "qtyCosting": listItemLineAdd[i].qtyCosting,
            "qtySelling": listItemLineAdd[i].qtySelling,
        });
    }
    objData.ItemLine = objRow;
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
                            draftdocument: draftdocument,
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
                type: "POST",
                url: url,
                data: {postJobSheetTruckingRequest:objData,draftdocument: draftdocument},
                //dataType: "JSON",
                //contentType: "application/json",
                dataType: "json",  
                async:true,
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

// On Update Transaction
function onUpdateTransaction(url, urlPDF, urlUploadFile, draftdocument) {
    //alert("Work In Progress");
    var objData = {};
    var objItemSellLine = [];
    var objItemCostLine = [];
    if ($("#cboCurrencyItemCosting").val()[0] === undefined) {
        alert("Select a Currency For Costing!")
        return;
    }
    if ($("#cboCurrencyItemSelling").val()[0] === undefined) {
        alert("Select a Currency For Selling!")
        return;
    }
    //End AddValue to ItemCosting Line
    objData.DocEntry = jobSheetDocEntry;
    objData.ConfirmBookingID = $("#txtConfirmBookingID").val();
    objData.CardCode = $("#txtSaleQuotation option:selected").attr("data-cardCode");//$("#txtCO").attr("data-cocode");
    objData.SaleQuotationDocNum = $("#txtSaleQuotation").val();
    objData.TuckWayBillBy = $("#txtTruckWayBillBy").val();
    objData.CurrencyCosting = $("#cboCurrencyItemCosting").val()[0];
    objData.CurrencySelling = $("#cboCurrencyItemSelling").val()[0];
    objData.RemarkForCosting = $("#remarkForCosting").val();
    objData.RemarkForSelling = $("#remarkForSelling").val();
    objData.TotalCosting = $("#txtTotalCosting").val().replaceAll(",", "");
    objData.Rebate = $("#txtRebate").val().replaceAll(",", "");
    objData.GrandTotalCosting = $("#txtGrandTotalCosting").val().replaceAll(",", "");
    objData.GrandTotalCostingUSD = $("#txtGrandTotalCostingUSD").val().replaceAll(",", "");
    objData.TotalSelling = $("#txtTotalSelling").val().replaceAll(",", "");
    objData.GrandTotalSelling = $("#txtGrandTotalSelling").val().replaceAll(",", "");
    objData.GrandTotalSellingUSD = $("#txtGrandTotalSellingUSD").val().replaceAll(",", "");
    objData.TotalProfit = $("#txtProfit").val().replaceAll(",", "");
    objData.UpdateBy = $("#UserID").attr("data-id");
    objData.JobNo = $("#txtJobNo").val();
    objData.DutyTaxAmountCosting = $("#txtDutyTaxAmountCosting").val().replaceAll(",", "");
    objData.DutyTaxAmountSelling = $("#txtDutyTaxAmountSelling").val().replaceAll(",", "");
    objData.AdvanceAmountCosting = $("#txtAdvanceAmountCosting").val();
    objData.AdvanceAmountSelling = $("#txtAdvanceAmountSelling").val();
    if ($("#btnConfirmCosting").text() == "Confirm Costing") {
        objData.CostingConfirm = "N";
    } else {
        objData.CostingConfirm = "Y";
    }
    //for (var i = 0; i < listItemLineAdd.length; i++) {
    //    if (listItemLineAdd.filter(e => e.lineNum === listItemLineAdd[i].lineNum && listItemLineAdd[i].itemType === "Q").length > 1) {
    //        listItemLineAdd[i].lineNum = 0;
    //        listItemLineAdd[i].itemType = "";
    //    }
    //}
    //objData.ItemLine = listItemLineAdd;
    var jobSheetTurckingCosting = [];
    for (var i = 0; i < listItemLineAdd.length; i++) {
        jobSheetTurckingCosting.push({
            "LineId": 0,
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceCosting,
            "ContainerType": listItemLineAdd[i].UomCode,
            "Qty": listItemLineAdd[i].qtyCosting,
        });
    }
    var jobSheetTurckingSelling = [];
    //console.log(listItemLineAdd);
    for (var i = 0; i < listItemLineAdd.length; i++) {
        jobSheetTurckingSelling.push({
            "LineId": 0,
            "ITEMCODE": listItemLineAdd[i].itemCode,
            "TOTALPRICE": listItemLineAdd[i].priceSelling,
            "ContainerType": listItemLineAdd[i].UomCode,
            "ItemType": listItemLineAdd[i].itemType,
            "LineNum": listItemLineAdd[i].LineNum,
            "Qty": listItemLineAdd[i].qtySelling,
        });
    }
    objData.JobSheetTruckingCostings = jobSheetTurckingCosting;
    objData.JobSheetTruckingSellings = jobSheetTurckingSelling;
    objData.Attachments = attachment;
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
                    for (var i = 0; i < data.length; i++) {
                        objData.Attachments.push({ "attachmentname": data[i].attachmentname, "lineId": 0 })
                    }
                    //objData.Attachment = data;
                    //console.log(objData);
                    $.ajax({
                        url: url,
                        type: "PUT",
                        data: {
                            updateJobSheetTruckingEdit: objData,
                            draftdocument: draftdocument,
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
                    updateJobSheetTruckingEdit: objData,
                    draftdocument: draftdocument,
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
//End On Update Transaction