var tbListServicePurchaseRequest = $('#TbListServicePurchaseRequest').DataTable();
$('#TbListServicePurchaseRequest tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListServicePurchaseRequest.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseService').click(function () {
    var index = tbListServicePurchaseRequest.row('.selected').index();
    var data = tbListServicePurchaseRequest.row(index).data();
    $('#txtServicePurchaseRequest').val(data[1]);
    $('#txtServicePurchaseRequest').attr("data-ItemCode", data[0]);
    $('#txtPrice').val(0); /*data[2]*/
    tbListServicePurchaseRequest.$('tr.selected').removeClass('selected');
    $("#ModalServicePurchaseRequest").modal("hide");
});