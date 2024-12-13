var ItemLineList = [];
var idLine = 1;
$("#btnAddLineInfo").click(function () {
    event.preventDefault();
    if ($("#txtItemSalesQuotation").attr("data-itemcode") == "") {
        alert("Please Enter Item Code");
        return;
    } else if ($("#txtPrice").val() == 0) {
        alert("Please Enter Price");
        return;
    }
    //var tmpLineChk = ItemLineList.filter(x => x.ItemCode == $("#txtItemSalesQuotation").attr("data-itemcode"));
    //if (tmpLineChk.length !== 0) {
    //    alert("Can not add Item duplicate");
    //    return;
    //}
    var obj = {};
    obj.ID = idLine;
    obj.ItemCode = $("#txtItemSalesQuotation").attr("data-itemcode");
    obj.ItemName = $("#txtItemSalesQuotation").val();
    obj.Price = $("#txtPrice").val();
    obj.Remarks = $("#txtRemarksLine").val();
    ItemLineList.push(obj);
    idLine++;
    reRenderDataTableLine();
    $("#txtItemSalesQuotation").val("");
    $("#txtItemSalesQuotation").attr("data-itemcode","");
    $("#txtPrice").val(0);
    $("#txtRemarksLine").val("");
});
function reRenderDataTableLine() {
    var tbListItemLine = $('#TbListLineSalesQuotation').DataTable();
    tbListItemLine.clear();
    tbListItemLine.rows.add(ItemLineList);
    tbListItemLine.search("").columns().search("").draw();
}
function DeleteLineItem(id) {
    ItemLineList = ItemLineList.filter(e => e.ID !== id);
    reRenderDataTableLine();
}

function onConfirmTransaction(url, urlPDF) {
    //if ($("#txtCustomerCode").val() == "") {
    //    alert("Please Enter Customer Code");
    //    return;
    //} else    
    if ($("#cboCreditTerm").val() == "-99") {
        alert("Please Enter Credit Term");
        return;
    } else if ($("#cboOrigin").val() == "-1") {
        alert("Please Enter Origin");
        return;
    } else if ($("#cboDestination").val() == "-1") {
        alert("Please Enter Destination");
        return;
    } else if (ItemLineList.length == 0) {
        alert("Please Enter Add Item");
        return;
    } else if ($("#txtValidity").val() === "") {
        alert("Please Enter Add Validity");
        return;
    } else if ($("#cboCustomerType").val() === '-1') {
        if ($("#cboBpCurrency").val() === "-1") {
            alert("Please Select Currency for new Customer Before Add");
            return;
        } else if (onCheckEmail("txtEmail") === false) {
            return;
        } else if (onCheckPhoneNumber('txtTel') === false) {
            return;
        }
    } else if ($("#txtValidityCount").val() === "") {
        alert("Please Enter Validity Count");
        return;
    }
    //else if ($("#cboCustomerType").val() == "-1") {
    //    alert("Please Enter ServiceType");
    //    return;
    //}
    if (confirm("Please confirm transaction make the data is correct !")) {
        var salesQuotation = {};
        salesQuotation.CustomerType = $("#cboCustomerType").val();
        if ($("#cboCustomerType").val() === "-1") {
            salesQuotation.CustomerCode = $("#txtCustomerCode").val();
        } else {
            salesQuotation.CustomerCode = $("#txtCO").attr("data-cocode");
        }
        salesQuotation.ReftNo = $("#txtRefNo").val();
        salesQuotation.ATTN = $("#txtAttn").val();
        salesQuotation.Date = $("#txtDate").val();
        salesQuotation.CreateBy = $("#UserID").attr("data-id");
        salesQuotation.Email = $("#txtEmail").val();
        salesQuotation.Validity = $("#txtValidity").val();
        salesQuotation.Tel = $("#txtTel").val();
        salesQuotation.CreditTerm = $("#cboCreditTerm").val();
        salesQuotation.Origin = $("#cboOrigin").val();
        salesQuotation.Destination = $("#cboDestination").val();
        salesQuotation.ServiceType = $("#cboServiceType").val();
        salesQuotation.Remarks = $("#remarks").val();
        salesQuotation.SaleEmployee = $("#cboSaleEmployee").val();
        salesQuotation.CommodityType = ($("#cboCommodityType").val() === "Others") ? $("#cboCommodityType").val() +" : "+$("#txtOtherCommodityType").val() :$("#cboCommodityType").val();
        salesQuotation.BPCurrency = $("#cboBpCurrency").val();
        salesQuotation.AddressDetail = $("#txtAddressNew").val();
        salesQuotation.AddressCode = $("#cboAddressExist").val();
        salesQuotation.DG = $("#cboDamgeGoods").val();
        salesQuotation.ContactPersons = {
            "Code": $("#cboContactPerson").val(),
            "LastName": $("#txtFirstName").val(),
            "FirstName": $("#txtLastName").val(),
            "Phone": $("#txtTel").val(),
            "Email": $("#txtEmail").val(),
        };
        for (var i = 0; i < ItemLineList.length; i++) {
            ItemLineList[i].Price = ItemLineList[i].Price.replace(",","");
        }
        salesQuotation.Lines = ItemLineList;
        $.ajax({
            url: url,
            type: "POST",
            data: { postSalesQuotationRequest: salesQuotation },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                clearText();
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
function clearText() {
    $("#cboCustomerType").val("-1");
    $("#cboBpCurrency").val("-1");
    $("#txtAttn").val("");
    $("#txtEmail").val("");
    $("#txtValidityCount").val("");
    $("#txtValidity").val("");
    $("#txtTel").val("");
    $("#cboServiceType").val("-1");
    $("#cboCreditTerm").val("-99");
    $("#cboOrigin").val("-1");
    $("#cboDestination").val("-1");
    $("#cboSaleEmployee").val("-1");
    $("#cboCommodityType").val("-1");
    $("#addressExist").val("");
    $("#remarks").val("");
}
$("#cboCustomerType").change(function () {
    if ($("#cboCustomerType").val() === '-1') {
        $("#showCustomerNew").attr("style", "display:block");
        $("#existingCustomer").attr("style", "display:none");
        $("#addressExist").attr("style", "display:none");
        $("#addressNew").attr("style", "display:block");
        $("#showContactPerson").attr("style", "display:none");
        $("#showAddNewContactPerson").attr("style", "");
        $("#txtEmail").removeAttr('disabled');
        $("#txtTel").removeAttr('disabled');
        $("#cboBpCurrency").empty();
        $("#cboBpCurrency").append("<option selected value='-1'>Select Currency</option>");
        for (var i = 0; i < objGetCurrencies.length; i++) {
            $("#cboBpCurrency").append("<option value='" + objGetCurrencies[i].currencyCode + "'>" + objGetCurrencies[i].currencyName + "</option>");
        }
        $("#cboBpCurrency").prop('disabled', false);
        $("#txtCustomerCode").val("");
        $("#cboContactPerson").empty();
    } else if ($("#cboCustomerType").val() === '1') {
        $("#existingCustomer").attr("style", "display:block");
        $("#showCustomerNew").attr("style", "display:none");
        $("#addressNew").attr("style", "display:none");
        $("#addressExist").attr("style", "display:block");
        $("#txtEmail").attr('disabled', 'disabled');
        $("#txtTel").attr('disabled', 'disabled');
        $("#showContactPerson").attr("style", "");
        $("#showAddNewContactPerson").attr("style", "display:none");
        $("#cboBpCurrency").prop('disabled', false);
        $("#txtCO").val("");
        $("#cboContactPerson").empty();
    }
});
$("#cboServiceType").change(function () {
    $("#cboOrigin").empty();
    $("#cboDestination").empty();
    $("#cboOrigin").append("<option selected value='-1'>Choose...</option>");
    $("#cboDestination").append("<option selected value='-1'>Choose...</option>");
    for (var i = 0; i < objGetOrigins.length; i++) {
        $("#cboOrigin").append("<option value='" + objGetOrigins[i].code + "'>" + objGetOrigins[i].name + "</option>");
    }
    for (var i = 0; i < objGetDestinations.length; i++) {
        $("#cboDestination").append("<option value='" + objGetDestinations[i].code + "'>" + objGetOrigins[i].name + "</option>");
    }
    if ($("#cboServiceType").val() === "Domestic Trucking") {
        $("#cboOrigin").val(selectOrigin);
        $("#cboDestination").val(selectOrigin);
        $("#cboOrigin").prop('disabled', true);
        $("#cboDestination").prop('disabled', true);
    } else {
        $("#cboOrigin").val("-1");
        $("#cboDestination").val("-1");
        $("#cboOrigin").prop('disabled', false);
        $("#cboDestination").prop('disabled', false);
    }
});
$("#cboOrigin").change(function () {
    var tmp = objGetDestinations.filter(x => x.code.toString() !== $("#cboOrigin").val());
    reRenderCbo(tmp, "cboDestination");
});
$("#cboDestination").change(function () {
    var tmp = objGetDestinations.filter(x => x.code.toString() !== $("#cboDestination").val());
    reRenderCbo(tmp, "cboOrigin");
});
$("#txtValidityCount").change(function () {
    const date = new Date($("#txtDate").val());
    date.setDate(date.getDate() + parseInt($("#txtValidityCount").val()))
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    var today = (date.getFullYear()) + "-" + (month) + "-" + (day);
    if (today !== "NaN-aN-aN") {
        $("#txtValidity").val(today);
    } else {
        $("#txtValidity").val("");
    }
});
function onCheckValueNumber(id) {
    var number = $("#" + id).val();
    var conditionNumber = number.substring(1, 0);
    if (conditionNumber === "0") {
        $("#" + id).val(number.substring(1));
    }
    addCommas(number);
}


function onCheckEmail(id) {
    var email = $("#" + id).val();
    if (email === "") {
        alert("Email should not be empty!");
        return false;
    }
    if (IsEmail(email) === false) {
        alert("Email is valid");
        return false;
    }
    return true;
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
function IsPhoneNumber(PhoneNumber) {
    var regex = /([0-9]{9})|(\([0-9]{3}\)\s+[0-9]{3}\-[0-9]{4})/;
    if (!regex.test(PhoneNumber)) {
        return false;
    } else {
        return true;
    }
}

function onCheckPhoneNumber(id) {
    var phoneNumber = $("#" + id).val();
    if (phoneNumber === "") {
        alert("Phone number should not be empty!");
        return false;
    } else if (phoneNumber.length > 51) {
        alert("Phone number should not more than 14 digit!");
        return false;
    }
    //if (IsPhoneNumber(phoneNumber) === false) {
    //    alert("Phone number is valid");
    //    return false;
    //}
    return true;
}

var objGetOrigins = [];
var objGetDestinations = [];
var objGetCurrencies = [];
function setterObjectType(obj, type) {
    if (type === 'GetOrigins') {
        objGetOrigins = obj;
    } else if (type === 'GetDestinations') {
        objGetDestinations = obj;
    } else if (type === 'GetCurrencyByCustomers') {
        objGetCurrencies = obj;
    }
}

function reRenderCbo(obj, id) {
    var tmpval = $("#" + id).val();
    $("#" + id).empty();
    $("#" + id).append("<option selected value='-1'>Choose...</option>");
    for (var i = 0; i < obj.length; i++) {
        $("#" + id).append("<option value='" + obj[i].code + "'>" + obj[i].name + "</option>");
    }
    if (tmpval !== "-1") {
        $("#" + id).val(tmpval);
    }
}
$("#cboContactPerson").change(function () {
    if ($("#cboContactPerson").val() == "-1") {
        $("#showAddNewContactPerson").attr("style", "");
        $("#txtEmail").removeAttr('disabled');
        $("#txtTel").removeAttr('disabled');
    } else {
        $("#txtEmail").attr('disabled', 'disabled');
        $("#txtTel").attr('disabled', 'disabled');
        $("#showAddNewContactPerson").attr("style", "display:none");
        if ($("#cboContactPerson").val() != "-99") {
            $("#txtTel").val($("#cboContactPerson option:selected").attr("data-phone"));
            $("#txtEmail").val($("#cboContactPerson option:selected").attr("data-email"));
        }
    }
});

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

$("#cboCommodityType").change(function () {
    if ($("#cboCommodityType").val() === "Others") {
        $("#plOther").removeAttr("style");
    } else {
        $("#plOther").attr("style","display:none;");
    }
});