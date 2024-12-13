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
        tbListItemSalesQuotationJobSheet.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
function truckChange(itemCode) {
    if ($('#cboUomCode-' + itemCode).val().length != 0) {
        var obj = {};
        for (var i = 0; i < listItemLineAdd.length; i++) {
            if (listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex === itemCode) {
                obj.itemCode = $("#lbl-txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).attr("data-itemcode");
                obj.itemName = $("#lbl-txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).text();
                obj.priceCosting = parseFloat($("#txt-c-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
                obj.priceSelling = parseFloat($("#txt-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
                obj.qtyCosting = parseInt($("#txt-q-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
                obj.qtySelling = parseInt($("#txt-s-q-" + listItemLineAdd[i].itemCode + listItemLineAdd[i].lineIndex).val());
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