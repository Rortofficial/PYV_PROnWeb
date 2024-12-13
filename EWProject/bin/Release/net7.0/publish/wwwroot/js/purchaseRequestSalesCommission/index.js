var collapsedGroups = {};
var isCommissionGroup = {};
var listprojectCommission = [];
//Consignee
$("#txtSaleEmployee").select2({
    placeholder: "Select a Sale Employee",
    maximumSelectionLength: 1
});
$('#TbListPurchaseRequestForCommission thead tr')
    .clone(true)
    .addClass('filters')
    .appendTo('#TbListPurchaseRequestForCommission thead');
var table = $('#TbListPurchaseRequestForCommission').DataTable({
    columns: [
        {
            data:"test",
            render: function (data, type, full) {
                //console.log(full);
                if (full.arBalance == 0) {
                    if (full.docStatus == "Complete") {
                        return '<button onclick="isCheckCommission(\'' + full.ARInvoice + '\',\'' + full.jobNo + '\',\'flexCheckDefault' + full.ARInvoice + '\')" class="btn" style="background-color:#01295C;color:white"><div class="form-check"><input onclick="return false" class="form-check-input" type="checkbox" value="' + full.ARInvoice + '" id="flexCheckDefault' + full.ARInvoice + '"><label class="form-check-label">Generate Commision</label></div></button>';
                    } else {
                        return '';
                    }
                } else {
                    return '';
                }
            }
        },//full.jobNo
        { data: "docStatus", autoWidth: true },
        { data: "jobNo", autoWidth: true },
        { data: "shipper", autoWidth: true },
        { data: "customerName", autoWidth: true },
        { data: "consignee", autoWidth: true },
        { data: "soNo", autoWidth: true },
        { data: "docNum", autoWidth: true },
        { data: "incomingNumber", autoWidth: true },
        { data: "grandTotalSelling", autoWidth: true },
        { data: "grandTotalCosting", autoWidth: true },
        { data: "rebate", autoWidth: true },
        { data: "grossProfit", autoWidth: true },
        { data: "commission", autoWidth: true },
        { data: "totalCommission", autoWidth: true },
        { data: "arBalance", autoWidth: true },
    ],
    order: [[2, 'asc']],
    orderCellsTop: true,
    fixedHeader: true,
    initComplete: function () {
        var api = this.api();
        // For each column
        api
            .columns()
            .eq(0)
            .each(function (colIdx) {
                // Set the header cell to contain the input element
                var cell = $('.filters th').eq(
                    $(api.column(colIdx).header()).index()
                );
                var title = $(cell).text();
                $(cell).html('<input type="text" placeholder="' + title + '" />');

                // On every keypress in this input
                $(
                    'input',
                    $('.filters th').eq($(api.column(colIdx).header()).index())
                )
                    .off('keyup change')
                    .on('change', function (e) {
                        // Get the search value
                        $(this).attr('title', $(this).val());
                        var regexr = '({search})'; //$(this).parents('th').find('select').val();

                        var cursorPosition = this.selectionStart;
                        // Search the column for that value
                        api
                            .column(colIdx)
                            .search(
                                this.value != ''
                                    ? regexr.replace('{search}', '(((' + this.value + ')))')
                                    : '',
                                this.value != '',
                                this.value == ''
                            )
                            .draw();
                    })
                    .on('keyup', function (e) {
                        e.stopPropagation();

                        $(this).trigger('change');
                        $(this)
                            .focus()[0]
                            .setSelectionRange(cursorPosition, cursorPosition);
                    });
            });
    },
    scrollCollapse: true,
    scrollX: true,
    scrollY: 500
});
//$('#TbListPurchaseRequestForCommission tbody').on('click', 'td.td1', function () {
//    var name = $(this).data('name');
//    collapsedGroups[name] = !collapsedGroups[name];
//    table.draw(false);
//});
function isCheckCommission(data, jobNo, id) {
    if ($('#' + id).is(":checked") == false) {
        $("#" + id).prop('checked', true);
        listprojectCommission.push({ "ARInvoice": data ,"JobNumber":jobNo});
    } else {
        $("#" + id).prop('checked', false);
        listprojectCommission = listprojectCommission.filter(e => e.ARInvoice !== data);
    }
    //console.log(listprojectCommission);
}
function OnSearchSaleEmployee(url) {
    $.ajax({
        url: url,
        type: "GET",
        data: { slpCode: $("#txtSaleEmployee").val()[0] },
        dataType: "JSON",
        beforeSend: function () {
            $('#Modalloder').modal('show');
        },
        success: function (data) {
            $('#Modalloder').modal('hide');
            collapsedGroups = {};
            isCommissionGroup = {};
            listprojectCommission = [];
            //console.log(data.data);
            table.clear();
            for (var i = 0; i < data.data.length; i++) {
                table.row.add({
                    "docStatus": data.data[i].docStatus,
                    "jobNo": data.data[i].jobNo,
                    "shipper": data.data[i].shipper,
                    "customerName": data.data[i].customerName,
                    "consignee": data.data[i].consignee,
                    "soNo": data.data[i].soNo,
                    "docNum": data.data[i].docNum,
                    "incomingNumber": data.data[i].incomingNumber,
                    "grandTotalSelling": data.data[i].grandTotalSelling,
                    "grandTotalCosting": data.data[i].grandTotalCosting,
                    "rebate": data.data[i].rebate,
                    "grossProfit": data.data[i].grossProfit,
                    "commission": data.data[i].commission,
                    "totalCommission": (data.data[i].arBalance == 0) ? data.data[i].totalCommission : 0,
                    "arBalance": data.data[i].arBalance,
                    "ARInvoice": data.data[i].arInvoice,
                }).draw();
            }
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}
function onConfirmTransaction(url, urlPDF) {
    $(this).prop('disabled', true);
    if (listprojectCommission.length == 0) {
        alert("Please Select Transaction Before Click Confirm.");
        $(this).prop('disabled', false);
        return;
    }
    if (confirm("Are you sure to create sales comission?")) {
        if ($("#txtSaleEmployee option:selected").attr("data-vendorCode") == '') {
            alert('Please Link Employee with BP first');
            return;
        }
        var obj = {};
        obj.SlpCode = $("#txtSaleEmployee").val()[0];
        obj.VendorCode = $("#txtSaleEmployee option:selected").attr("data-vendorCode");
        obj.UserID = $('#UserID').attr('data-empid');
        obj.Remark = $('#txtRemark').val();
        obj.DueDate = $('#txtDueDate').val();
        $.ajax({
            url: url,
            type: "POST",
            data: { invoiceList: listprojectCommission, postSalesCommissions: obj },
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
                $(this).prop('disabled', false);
            }
        });
    }
    $(this).prop('disabled', false);
}