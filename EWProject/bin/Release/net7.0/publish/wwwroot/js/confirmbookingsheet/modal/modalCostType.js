var tbCostTypeList = $('#TbListCostType').DataTable();
$('#TbListCostType tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbCostTypeList.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseCostType').click(function () {
    var index = tbCostTypeList.row('.selected').index();
    var data = tbCostTypeList.row(index).data();
    $('#txtCostType').val(data[1]);
    $('#txtCostType').attr("data-CostTypeCode", data[0]);
    $('#txtAmountCostType').val(data[2]);
    tbCostTypeList.$('tr.selected').removeClass('selected');
    $("#ModalCostType").modal("hide");
});