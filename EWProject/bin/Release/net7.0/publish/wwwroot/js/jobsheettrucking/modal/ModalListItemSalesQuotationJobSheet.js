$('#TbListItemSalesQuotationJobSheet').DataTable({
    columns:
        [
            { data: "lineNum", autoWidth: true },
            { data: "itemCode", autoWidth: true },
            { data: "itemName", autoWidth: true },
            { data: "priceSelling", autoWidth: true },
            { data: "priceCosting", autoWidth: true },
            { data: "itemType", visible: false },
            { data: "remarks", visible: true },
        ],
});
var tbListItemSalesQuotationJobSheet = $('#TbListItemSalesQuotationJobSheet').DataTable();
$('#TbListItemSalesQuotationJobSheet tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        //tbListItemSalesQuotationJobSheet.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});

function renderItemList(condition) {
    $("#tbListItemCostingAndSelling").empty();
    for (var k = 0; k < listItemLineAdd.length; k++) {
        if (condition === "1") {
            $("#tbListItemCostingAndSelling").append('<div class="row mt-2"><div class="col-4"><!--ITEM COSTING--><div class="row mt-2"><div class="col-4" style="margin:auto;text-align:left;font-weight:bold;font-size:14px;padding:0px;" data-ItemCode="' + listItemLineAdd[k].itemCode + '" id="lbl-txt-c-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '">' + listItemLineAdd[k].itemName + '</div><div class="col-2" style="margin:auto;"><input type="number" disabled onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" class="form-control" value="' + listItemLineAdd[k].qtyCosting + '" placeholder="Qty" id="txt-q-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value=""></div><div class="col-4" style="margin:auto;"><input type="text" disabled class="form-control" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" placeholder="Amount" id="txt-c-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value="' + listItemLineAdd[k].priceCosting + '"></div><div class="col-2" style="margin:auto;">=' + (listItemLineAdd[k].qtyCosting * listItemLineAdd[k].priceCosting) + '</div></div></div><div class="col-4"><!--ITEM SELLING--><div class="row mt-2"><div class="col-4" style="margin:auto;text-align:left;font-weight:bold;font-size:14px;padding:0px;" data-ItemCode="' + listItemLineAdd[k].itemCode + '" id="lbl-txt-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '">' + listItemLineAdd[k].itemName + '</div><div class="col-2" style="margin:auto;"><input type="number" disabled onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" value="' + listItemLineAdd[k].qtySelling + '" class="form-control" placeholder="Qty"id="txt-s-q-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value=""></div><div class="col-4" style="margin:auto;"><div class="row"><div class="col-12"><input type="text" class="form-control" placeholder="Amount" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" data-ItemCode="" id="txt-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value="' + listItemLineAdd[k].priceSelling + '"></div></div></div><div class="col-2" style="margin:auto;">=' + (listItemLineAdd[k].qtySelling * listItemLineAdd[k].priceSelling) + '</div></div></div><div class="col-4" style="margin:auto;"><div class="row"><div class="col-8"><select class="form-control" multiple="multiple" onchange="truckChange(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" id="cboUomCode-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '"></select></div><div class="col-4"><button class="btn btn-danger" onclick="deleteItemListLine(\'' + listItemLineAdd[k].itemCode + '\',' + listItemLineAdd[k].lineIndex + ')">Delete</button></div></div></div></div>');
        } else {
            //$("#tbListItemCostingAndSelling").append('<div class="row mt-2"><div class="col-5"><!--ITEM COSTING--><div class="row mt-2"><div class="col-3" style="margin:auto;text-align:left;font-weight:bold;font-size:14px;padding:0px;"data-ItemCode="' + listItemLineAdd[k].itemCode + '" id="lbl-txt-c-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '">' + listItemLineAdd[k].itemName + '</div><div class="col-2" style="margin:auto;"><input type="number" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')"class="form-control" value="' + listItemLineAdd[k].qtyCosting + '" placeholder="Qty"id="txt-q-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value=""></div><div class="col-4" style="margin:auto;"><input type="number" class="form-control"onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" placeholder="Amount"id="txt-c-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value="' + listItemLineAdd[k].priceCosting + '"></div><div class="col-3" style="margin:auto;">=' + (listItemLineAdd[k].qtyCosting * listItemLineAdd[k].priceCosting) + '</div></div></div><div class="col-5"><!--ITEM SELLING--><div class="row mt-2"><div class="col-3" style="margin:auto;text-align:left;font-weight:bold;font-size:14px;padding:0px;"data-ItemCode="' + listItemLineAdd[k].itemCode + '" id="lbl-txt-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '">' + listItemLineAdd[k].itemName + '</div><div class="col-2" style="margin:auto;"><input type="number" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')"value="' + listItemLineAdd[k].qtySelling + '" class="form-control" placeholder="Qty"id="txt-s-q-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value=""></div><div class="col-4" style="margin:auto;"><div class="row"><div class="col-12"><input type="number" class="form-control" placeholder="Amount"onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" data-ItemCode=""id="txt-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '"value="' + listItemLineAdd[k].priceSelling + '"></div></div></div><div class="col-3" style="margin:auto;">=' + (listItemLineAdd[k].qtySelling * listItemLineAdd[k].priceSelling) + '</div></div></div><div class="col-2" style="margin:auto;"><button class="btn btn-danger"onclick="deleteItemListLine(\'' + listItemLineAdd[k].itemCode + '\',' + listItemLineAdd[k].lineIndex +')">Delete</button></div></div>');
            $("#tbListItemCostingAndSelling").append('<div class="row mt-2"><div class="col-4"><!--ITEM COSTING--><div class="row mt-2"><div class="col-4" style="margin:auto;text-align:left;font-weight:bold;font-size:14px;padding:0px;" data-ItemCode="' + listItemLineAdd[k].itemCode + '" id="lbl-txt-c-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '">' + listItemLineAdd[k].itemName + '</div><div class="col-2" style="margin:auto;"><input type="number" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" class="form-control" value="' + listItemLineAdd[k].qtyCosting + '" placeholder="Qty" id="txt-q-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value=""></div><div class="col-4" style="margin:auto;"><input type="text" class="form-control" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" placeholder="Amount" id="txt-c-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value="' + listItemLineAdd[k].priceCosting + '"></div><div class="col-2" style="margin:auto;">=' + (listItemLineAdd[k].qtyCosting * listItemLineAdd[k].priceCosting) + '</div></div></div><div class="col-4"><!--ITEM SELLING--><div class="row mt-2"><div class="col-4" style="margin:auto;text-align:left;font-weight:bold;font-size:14px;padding:0px;" data-ItemCode="' + listItemLineAdd[k].itemCode + '" id="lbl-txt-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '">' + listItemLineAdd[k].itemName + '</div><div class="col-2" style="margin:auto;"><input type="number" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" value="' + listItemLineAdd[k].qtySelling + '" class="form-control" placeholder="Qty" id="txt-s-q-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value=""></div><div class="col-4" style="margin:auto;"><div class="row"><div class="col-12"><input type="text" class="form-control" placeholder="Amount" onchange="reCalculate(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" data-ItemCode="" id="txt-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '" value="' + listItemLineAdd[k].priceSelling + '"></div></div></div><div class="col-2" style="margin:auto;">=' + (listItemLineAdd[k].qtySelling * listItemLineAdd[k].priceSelling) + '</div></div></div><div class="col-4" style="margin:auto;"><div class="row"><div class="col-8"><select class="form-control" multiple="multiple" onchange="truckChange(\'' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '\')" id="cboUomCode-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex + '"></select></div><div class="col-4"><button class="btn btn-danger" onclick="deleteItemListLine(\'' + listItemLineAdd[k].itemCode + '\',' + listItemLineAdd[k].lineIndex + ')">Delete</button></div></div></div></div>');
        }
        formartNumber("txt-c-" + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex);
        formartNumber("txt-" + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex);

        $('#cboUomCode-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex).select2({
            placeholder: "Select a Truck Type Or Container Type",
            maximumSelectionLength: 1
        });
        //console.log(listItemLineAdd[k].UomGroupList.length);
        //$('#cboUomCode-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex).empty();
        var newOptionPlaceOfDelivery = [];
        for (var j = 0; j < listItemLineAdd[k].UomGroupList.length; j++) {
            //console.log(listItemLineAdd[k].UomGroupList[j].name);
            //console.log(listItemLineAdd[k].UomGroupList[j].code);
            newOptionPlaceOfDelivery.push(new Option(listItemLineAdd[k].UomGroupList[j].name, listItemLineAdd[k].UomGroupList[j].code, false, false));
        }
        $('#cboUomCode-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex).append(newOptionPlaceOfDelivery).trigger('change');
        $('#cboUomCode-' + listItemLineAdd[k].itemCode + listItemLineAdd[k].lineIndex).val(listItemLineAdd[k].UomCode).trigger("change");
    }
}
function deleteItemListLine(itemCode, lineID) {
    listItemLineAdd = listItemLineAdd.filter(e => e.lineIndex !== lineID);//e.itemCode !== itemCode &&
    renderItemList();
    reCalculate();
}
function truckChange(itemCode) {
    if ($('#cboUomCode-' + itemCode).val().length != 0) {
        var obj = {};
        for (var i = 0; i < listItemLineAdd.length; i++) {
            if (listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex === itemCode) {
                obj.itemCode = $("#lbl-txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).attr("data-itemcode");
                obj.itemName = $("#lbl-txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).text();
                obj.priceCosting = $("#txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val().replace(",", "");//parseFloat()
                obj.priceSelling = $("#txt-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val().replace(",", "");//parseFloat()
                obj.qtyCosting = parseInt($("#txt-q-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
                obj.qtySelling = parseInt($("#txt-s-q-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
                obj.LineNum = listItemLineAdd[i].LineNum;
                obj.lineNum = listItemLineAdd[i].lineNum;
                obj.lineIndex = listItemLineAdd[i].lineIndex;
                obj.itemType = listItemLineAdd[i].itemType;
                obj.UomCodeList = listItemLineAdd[i].UomCodeList;
                obj.UomCode = $('#cboUomCode-' + itemCode).val()[0],
                obj.UomGroupList = listItemLineAdd[i].UomGroupList;
                listItemLineAdd[i] = obj;
            }
        }
    }
}