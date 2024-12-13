var tbListItemSalesQuotation = $('#TbListItemSalesQuotation').DataTable();
$('#TbListItemSalesQuotation tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListItemSalesQuotation.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseItem').click(function () {
    var index = tbListItemSalesQuotation.row('.selected').index();
    var data = tbListItemSalesQuotation.row(index).data();
    $('#txtItemSalesQuotation').val(data[1]);
    $('#txtItemSalesQuotation').attr("data-ItemCode", data[0]);
    //$('#txtPrice').val(data[2]);
    tbListItemSalesQuotation.$('tr.selected').removeClass('selected');
    $("#ModalItemSalesQuotation").modal("hide");
});