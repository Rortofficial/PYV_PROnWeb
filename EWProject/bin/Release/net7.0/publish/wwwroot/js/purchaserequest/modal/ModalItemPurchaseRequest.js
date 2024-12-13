var tbListItemPurchaseRequest = $('#TbListItemPurchaseRequest').DataTable();
$('#TbListItemPurchaseRequest tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListItemPurchaseRequest.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseItem').click(function () {
    var index = tbListItemPurchaseRequest.row('.selected').index();
    var data = tbListItemPurchaseRequest.row(index).data();
    $('#txtItemPurchaseRequest').val(data[1]);
    $('#txtItemPurchaseRequest').attr("data-ItemCode", data[0]);
    $('#txtPrice').val(data[2]);
    tbListItemPurchaseRequest.$('tr.selected').removeClass('selected');
    $("#ModalItemPurchaseRequest").modal("hide");
});