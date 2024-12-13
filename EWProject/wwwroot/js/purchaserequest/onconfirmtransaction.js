function onConfirmTransaction(url, urlPDF) {
    if ($("#txtTel").val() == 0) {
        alert("Please Enter Tel");
        return;
    } else if ($("#cboCreditTerm").val() == "-99") {
        alert("Please Enter Credit Term");
        return;
    } else if ($("#cboOrigin").val() == "-1") {
        alert("Please Enter Origin");
        return;
    } else if ($("#cboDestination").val() == "-1") {
        alert("Please Enter Destination");
        return;
    } else if ($("#cboItemOrServiceType").val() === '-1') {
        if (ItemLineList.length == 0) {
            alert("Please Enter Add Item");
            return;
        }
    } else if ($("#cboItemOrServiceType").val() === '1') {
        if (ItemLineListService.length == 0) {
            alert("Please Enter Service Item");
            return;
        }
    }
    if (confirm("Please confirm transaction make the data is correct !")) {
        var purchaseRequest = {};
        purchaseRequest.ServiceOrItemType = $("#cboItemOrServiceType").val();
        purchaseRequest.RequestBy = $("#UserID").attr("data-empid");
        purchaseRequest.RequireDate = $("#txtDateRequire").val();
        purchaseRequest.Remarks = $("#remarks").val();
        if ($("#cboItemOrServiceType").val() === '-1') {
            purchaseRequest.Lines = ItemLineList;
        } else if ($("#cboItemOrServiceType").val() === '1') {
            purchaseRequest.Lines = ItemLineListService;
        }
        $.ajax({
            url: url,
            type: "POST",
            data: { postPurchaseRequestRequest: purchaseRequest },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                window.location.href = urlPDF;
            },
            error: function (erro) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html(erro.responseText);
            }
        });
    }
}