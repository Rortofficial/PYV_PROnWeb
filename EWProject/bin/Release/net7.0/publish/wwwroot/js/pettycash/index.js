
$("#btnAddLineInfo").click(function () {
    if ($("#txtCreditOrDebit").val() == 0) {
        alert("Please Enter Price Debit Or Credit");
        return;
    } else if ($("#txtAccountCode").attr("data-accountcode") == "") {
        alert("Please Enter Account Code");
        return;
    }
    console.log("Helo");
    //Account Code
    var obj = {};
    obj.ID = idLine;
    obj.AccountTypeCode = "-1";
    obj.AccountTypeName = "Account Code";
    obj.AccountNameOrBpName = $("#txtAccountName").val();
    obj.AccountCodeOrBpCode = $("#txtAccountCode").attr("data-accountcode");
    obj.CardCode = $("#txtBPCode").attr("data-bpcode");
    obj.CardName = $("#txtBpName").val();
    obj.Remarks = $("#txtRemarkLine").val();
    obj.DateLine = $("#txtLineDate").val();
    obj.Debit = $("#txtCreditOrDebit").val();//parseFloat($("#txtCreditOrDebit").val());
    obj.Credit = 0;
    ItemLineList.push(obj);
    idLine++;
    reRenderDataTableLine();
    $("#txtBPCode").val("");
    $("#txtBpName").val("");
    $("#txtAccountCode").val("");
    $("#txtAccountName").val("");
    $("#txtCreditOrDebit").val("");
    $("#txtRemarkLine").val("");
    $("#txtLineDate").val("");
    calculateDebitOrCredit();
});
function calculateDebitOrCredit() {
    //var DebitAvailable=0;
    //var CreditAvailable = 0;
    var TotalDebit=0.00;
    //var TotalCredit = 0;
    for (var i = 0; i < ItemLineList.length; i++) {
        //DebitAvailable = DebitAvailable +(ItemLineList[i].Debit - ItemLineList[i].Credit);
        //CreditAvailable = CreditAvailable + (ItemLineList[i].Credit - ItemLineList[i].Debit);
        TotalDebit = TotalDebit + parseFloat((parseFloat(ItemLineList[i].Debit).toString() != NaN) ? ConvertFormartNumberToNumber(ItemLineList[i].Debit) : ItemLineList[i].Debit);
        //TotalCredit = TotalCredit + ItemLineList[i].Credit;
    }
    console.log(TotalDebit);
    //if (DebitAvailable < 0) {
    //    $("#txtAvailableDebit").val(DebitAvailable);
    //} else {
    //    $("#txtAvailableDebit").val(0);
    //}
    //if (CreditAvailable < 0) {
    //    $("#txtAvailableCredit").val(CreditAvailable);
    //} else {
    //    $("#txtAvailableCredit").val(0);
    //}
    //$("#txtBalanceCredit").val(TotalCredit);
    $("#txtBalanceDebit").val(TotalDebit);
}
function reRenderDataTableLine() {
    var tbListPettyCashLine = $('#TbListPettyCashLine').DataTable();
    tbListPettyCashLine.on('order.dt search.dt', function () {
        let i = 1;
        tbListPettyCashLine.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
            this.data(i++);
        });
    });
    tbListPettyCashLine.clear();
    tbListPettyCashLine.rows.add(ItemLineList);
    tbListPettyCashLine.search("").columns().search("").draw();
}
function DeleteLineItem(id) {
    ItemLineList = ItemLineList.filter(e => e.ID !== id);
    reRenderDataTableLine();
    calculateDebitOrCredit();
}

function onConfirmTransaction(url, urlPDF, type) {
    event.preventDefault();
    if (confirm("Please confirm transaction make the data is correct !")) {
        if (ItemLineList.length==0) {
            alert("Please Select Account Code Before Confirm!");
            return;
        } else if ($("#txtBudgetOnHand").val()<=0) {
            alert("Budget is not enough!");
            return;
        } else if (parseFloat($("#txtBudgetOnHand").val()) < parseFloat($("#txtBalanceDebit").val())) {            
            alert("Debit is bigger than budget");
            return;
        } else {          
            var Header = {};
            var postPettyCashRequest = {};
            Header.Series = $("#cboItemOrServiceType").val();
            Header.PostingDate = $("#txtPostingDate").val();
            Header.Remarks = $("#remarks").val();
            Header.Ref1 = $("#txtRef1").val();
            Header.Ref2 = $("#txtRef2").val();
            Header.Ref3 = $("#txtRef3").val();
            Header.UserID = $("#UserID").attr("data-id");
            Header.UpdateBy ="";
            //Header.UserID = $("#txtEmployeeNameCreate").attr("data-cardcode");
            //postPettyCashRequest.AccountCode = $("#txtAccountCode").attr("data-accountcode");
            //postPettyCashRequest.BpCode = $("#txtBPCode").attr("data-bpcode");
            //postPettyCashRequest.TotalExspenAmount = $("#txtCreditOrDebit").val();
            postPettyCashRequest.Header = Header;
            for (var i = 0; i < ItemLineList.length; i++) {
                //DebitAvailable = DebitAvailable +(ItemLineList[i].Debit - ItemLineList[i].Credit);
                //CreditAvailable = CreditAvailable + (ItemLineList[i].Credit - ItemLineList[i].Debit);
                ItemLineList[i].Debit =parseFloat(ConvertFormartNumberToNumber(ItemLineList[i].Debit));
                //TotalCredit = TotalCredit + ItemLineList[i].Credit;
            }
            if (type == "JE") {
                //CardCode
                var obj = {};
                obj.ID = idLine;
                obj.AccountTypeCode = "1";
                obj.AccountTypeName = "Account Code";
                obj.AccountNameOrBpName = $("#txtEmployeeNameCreate").val();
                obj.AccountCodeOrBpCode = $("#txtEmployeeNameCreate").attr("data-cardcode");
                obj.Credit = parseFloat(ConvertFormartNumberToNumber($("#txtBalanceDebit").val()));
                obj.Debit = 0;
                ItemLineList.push(obj);
            }  
            postPettyCashRequest.Lines = ItemLineList;
            $.ajax({
                url: url,
                type: "POST",
                data: { postPettyCashRequest: postPettyCashRequest },
                dataType: "JSON",
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html("Success");
                    window.location.href = urlPDF + "?docEntry=" + data.docEntry + "&type=" + type;
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                    ItemLineList = ItemLineList.filter(e => e.Credit > 0);
                }
            });
        }
    }
}
function onUpdateTransaction(url, urlPDF, type) {
    if (confirm("Please confirm transaction make the data is correct !")) {
        if (ItemLineList.length == 0) {
            alert("Please Select Account Code Before Confirm!");
            return;
        } else if ($("#txtBudgetOnHand").val() <= 0) {
            alert("Budget is not enough!");
            return;
        } else if (parseFloat($("#txtBudgetOnHand").val()) < parseFloat($("#txtBalanceDebit").val())) {
            alert("Debit is bigger than budget");
            return;
        } else {
            if (type == "JE") {
                //CardCode
                var obj = {};
                obj.ID = idLine;
                obj.AccountTypeCode = "1";
                obj.AccountTypeName = "Account Code";
                obj.AccountNameOrBpName = $("#txtEmployeeNameCreate").val();
                obj.AccountCodeOrBpCode = $("#txtEmployeeNameCreate").attr("data-cardcode");
                obj.Credit = parseFloat($("#txtBalanceDebit").val());
                obj.Debit = 0;
                ItemLineList.push(obj);
            }
            var Header = {};
            var postPettyCashRequest = {};
            Header.Series = $("#cboItemOrServiceType").val();
            Header.PostingDate = $("#txtPostingDate").val();
            Header.Remarks = $("#remarks").val();
            Header.WebID = webID;
            Header.Ref1 = $("#txtRef1").val();
            Header.Ref2 = $("#txtRef2").val();
            Header.Ref3 = $("#txtRef3").val();
            Header.UserID = userID;
            //Header.UserID = $("#txtEmployeeNameCreate").attr("data-cardcode");
            //postPettyCashRequest.AccountCode = $("#txtAccountCode").attr("data-accountcode");
            //postPettyCashRequest.BpCode = $("#txtBPCode").attr("data-bpcode");
            //postPettyCashRequest.TotalExspenAmount = $("#txtCreditOrDebit").val();
            postPettyCashRequest.Header = Header;
            postPettyCashRequest.Lines = ItemLineList;
            $.ajax({
                url: url,
                type: "PUT",
                data: { docEntry: docEntry,postPettyCashRequest: postPettyCashRequest },
                dataType: "JSON",
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (data) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html("Success");
                    window.location.href = urlPDF + "?docEntry=" + data.docEntry + "&type=" + type;
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                }
            });
        }
    }
}
$("#cboAccountType").change(function () { 
    onChangeAccountType();
});
function onChangeAccountType() {
    $("#txtAccountNameOrBpName").val("");
    if ($("#cboAccountType").val() === '-1') {//Account Code
        $("#showAccountCode").attr("style", "display:block");
        $("#showBpCode").attr("style", "display:none");
        $("#lblAccountCode").attr("style", "display:block");
        $("#lblBpCode").attr("style", "display:none");
        $("#lblAccountName").attr("style", "display:block");
        $("#lblBpName").attr("style", "display:none");
        $("#cboVatGroup").prop("disabled", false);
    } else if ($("#cboAccountType").val() === '1') { //BP Code
        $("#showBpCode").attr("style", "display:block");
        $("#showAccountCode").attr("style", "display:none");
        $("#lblBpCode").attr("style", "display:block");
        $("#lblAccountCode").attr("style", "display:none");
        $("#lblBpName").attr("style", "display:block");
        $("#lblAccountName").attr("style", "display:none");
        $("#cboVatGroup").prop("disabled", true);
        $("#cboVatGroup").val("-1");
    }
}