var ItemLineList = [];
var ItemLineListService = [];
var idLine = 1;
var idLineService = 1;
$("#btnAddLineInfo").click(function () {
    if ($("#txtPrice").val() == 0) {
        alert("Please Enter Price");
        return;
    }
    if ($("#cboItemOrServiceType").val() === '-1') { //Item
        if ($("#txtItemPurchaseRequest").attr("data-itemcode") == "") {
            alert("Please Enter Item Code");
            return;
        }
        var obj = {};
        obj.ID = idLine;
        obj.ItemCode = $("#txtItemPurchaseRequest").attr("data-itemcode");
        obj.ItemName = $("#txtItemPurchaseRequest").val();
        obj.Price = $("#txtPrice").val();
        obj.Remarks = $("#txtRemarksLine").val();
        ItemLineList.push(obj);
        idLine++;
        $("#txtItemPurchaseRequest").val("");
    } else if ($("#cboItemOrServiceType").val() === '1') { //Service
        if ($("#txtServicePurchaseRequest").attr("data-itemcode") == "") {
            alert("Please Enter Service Code");
            return;
        }
        var obj = {};
        obj.ID = idLineService;
        obj.ItemCode = $("#txtServicePurchaseRequest").attr("data-itemcode");
        obj.ItemName = $("#txtServicePurchaseRequest").val();
        obj.Price = $("#txtPrice").val();
        obj.Remarks = $("#txtRemarksLine").val();
        ItemLineListService.push(obj);
        idLineService++;
        $("#txtServicePurchaseRequest").val("");
    }
    reRenderDataTableLine();
    $("#txtPrice").val(0);
    $("#txtRemarksLine").val("");
});
function reRenderDataTableLine() {
    if ($("#cboItemOrServiceType").val() === '-1') { //Item
        var tbListItemLine = $('#TbListLineItemPurchaseRequest').DataTable();
        tbListItemLine.clear();
        tbListItemLine.rows.add(ItemLineList);
        tbListItemLine.search("").columns().search("").draw();
    } else if ($("#cboItemOrServiceType").val() === '1') { //Service
        var tbListServiceLine = $('#TbListLineServicePurchaseRequest').DataTable();
        tbListServiceLine.clear();
        tbListServiceLine.rows.add(ItemLineListService);
        tbListServiceLine.search("").columns().search("").draw();
    }
}
function DeleteLineItem(id) {
    if ($("#cboItemOrServiceType").val() === '-1') { //Item
        ItemLineList = ItemLineList.filter(e => e.ID !== id);
        reRenderDataTableLine();
    } else if ($("#cboItemOrServiceType").val() === '1') { //Service
        ItemLineListService = ItemLineListService.filter(e => e.ID !== id);
        reRenderDataTableLine();
    }
}
$("#cboItemOrServiceType").change(function () {
    if ($("#cboItemOrServiceType").val() === '-1') {
        $("#showCustomerNew").attr("style", "display:block");
        $("#existingCustomer").attr("style", "display:none");
        $("#itemType").attr("style", "display:block");
        $("#serviceType").attr("style", "display:none");
    } else if ($("#cboItemOrServiceType").val() === '1') {
        $("#existingCustomer").attr("style", "display:block");
        $("#showCustomerNew").attr("style", "display:none");
        $("#serviceType").attr("style", "display:block");
        $("#itemType").attr("style", "display:none");
    }
});

//cboProjectManagement
$("#cboProjectManagement").select2({
    placeholder: "Select a Project Management",
    maximumSelectionLength: 1
});


