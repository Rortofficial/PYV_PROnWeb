var tbListConsignee =$('#TbListConsignee').DataTable();

$('#TbListConsignee tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListConsignee.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseConsignee').click(function () {
    var index = tbListConsignee.row('.selected').index();
    var data = tbListConsignee.row(index).data();
    $('#txtConsignee').val(data[1]);
    $('#txtConsignee').attr("data-ConsigneeCode", data[0]);
    tbListConsignee.$('tr.selected').removeClass('selected');
    $("#ModalConsignee").modal("hide");
});