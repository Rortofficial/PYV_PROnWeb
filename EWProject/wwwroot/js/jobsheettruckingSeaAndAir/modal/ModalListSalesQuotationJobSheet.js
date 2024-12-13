var tbListSalesQuotationJobSheet = $('#TbListSalesQuotationJobSheet').DataTable();
$('#TbListSalesQuotationJobSheet tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListSalesQuotationJobSheet.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseCO').click(function () {
    var index = tbListSalesQuotationJobSheet.row('.selected').index();
    var data = tbListSalesQuotationJobSheet.row(index).data();
    $('#txtSaleQuotation').val(data[1]);
    $('#txtSaleQuotation').attr("data-DocEntry", data[0]);
    var url = $("#txtSaleQuotation").attr("data-Url");
    $.ajax({
        url: url,
        type: "GET",
        data: { DocEntry: data[0] },
        dataType: "JSON",
        beforeSend: function () {
            $('#Modalloder').modal('hide');
        },
        success: function (data) {
            reRenderDataTableItemLine(data.data);
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
    tbListSalesQuotationJobSheet.$('tr.selected').removeClass('selected');
    $("#ModalSalesQuotationJobSheet").modal("hide");
});
function reRenderDataTableItemLine(data) {
    var tbListItemLine = $('#TbListItemSalesQuotationJobSheet').DataTable();
    tbListItemLine.clear();
    tbListItemLine.rows.add(data);
    tbListItemLine.search("").columns().search("").draw();
}