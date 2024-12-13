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
});