var tbListCO=$('#TbListCO').DataTable();
$('#TbListCO tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListCO.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseCO').click(function () {
    var index = tbListCO.row('.selected').index();
    var data = tbListCO.row(index).data();
    $('#txtCO').val(data[1]);
    $('#txtCO').attr("data-COCode", data[0]);
    tbListCO.$('tr.selected').removeClass('selected');
    $("#ModalCO").modal("hide");
    getCustomerAddress(data[0], $("#cboCustomerType").find(':selected').attr('data-getaddress'));
    getCustomerBpCurrency(data[0], $("#cboCustomerType").find(':selected').attr('data-bpCurrency'));
    getContactPerson(data[0]);
    getInformationCustomer(data[0])
});

function getInformationCustomer(cardCode) {
    $.ajax({
        url: $("#txtUrlGetInformationBP").val(),
        type: "GET",
        data: { cardCode: cardCode },
        dataType: "JSON",
        beforeSend: function () {
            $('#Modalloder').modal('show');
        },
        success: function (data) {
            console.log(data);
            $("#cboCreditTerm").val(data.data.creditTerm);
            $("#cboSaleEmployee").val(data.data.saleEmployee);
            $("#cboAddressExist").val(data.data.address);
            $('#Modalloder').modal('hide');
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}

function getCustomerAddress(cardCode,url) {
    $.ajax({
        url: url,
        type: "GET",
        data: { cardCode: cardCode },
        dataType: "JSON",
        beforeSend: function () {
            $('#Modalloder').modal('show');
        },
        success: function (data) {
            $("#cboAddressExist").empty();
            $("#cboAddressExist").append("<option selected value='-1'>Choose...</option>");
            for (var i = 0; i < data.data.length; i++) {
                $("#cboAddressExist").append("<option value=\"" + data.data[i].customerAddressCode + "\">" + data.data[i].customerAddressDetail +"</option>");
            }
            $('#Modalloder').modal('hide');
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}

function getContactPerson(cardCode) {
    $.ajax({
        url: $("#txtUrlGetContactPersonLink").val(),
        type: "GET",
        data: { cardCode: cardCode },
        dataType: "JSON",
        beforeSend: function () {
            $('#Modalloder').modal('show');
        },
        success: function (data) {
            $("#cboContactPerson").empty();
            $("#cboContactPerson").append("<option selected value='-99'>Select Contact Person</option>");
            for (var i = 0; i < data.data.length; i++) {
                $("#cboContactPerson").append("<option data-Phone='" + data.data[i].phone + "' data-Email='" + data.data[i].email + "' value='" + data.data[i].contactPersonID + "' >" + data.data[i].firstName + ' ' + data.data[i].lastName + "</option>");
            }

            $("#cboContactPerson").append("<option value='-1'>Create News</option>");
            $('#Modalloder').modal('hide');
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}

function getCustomerBpCurrency(cardCode, url) {
    $.ajax({
        url: url,
        type: "GET",
        data: { cardCode: cardCode },
        dataType: "JSON",
        beforeSend: function () {
            $('#Modalloder').modal('show');
        },
        success: function (data) {
            $("#cboBpCurrency").empty();
            //$("#cboAddressExist").append("<option selected value='-1'>Choose...</option>");
            for (var i = 0; i < data.data.length; i++) {
                if (data.data[i].defualt === "Y") {
                    $("#cboBpCurrency").append("<option selected value='" + data.data[i].currencyCode + "'>" + data.data[i].currencyName + "</option>");
                } else {
                    $("#cboBpCurrency").append("<option value='" + data.data[i].currencyCode + "'>" + data.data[i].currencyName + "</option>");
                }
            }
            $('#Modalloder').modal('hide');
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}