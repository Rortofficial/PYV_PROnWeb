var TbListAccountCodePettyCash = $('#TbListAccountCodePettyCash').DataTable();
$('#TbListAccountCodePettyCash tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        TbListAccountCodePettyCash.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseAccountCode').click(function () {
    var index = TbListAccountCodePettyCash.row('.selected').index();
    var data = TbListAccountCodePettyCash.row(index).data();
    $('#txtAccountName').val(data[1]);
    $('#txtAccountCode').val(data[0]);
    $('#txtAccountCode').attr("data-AccountCode", data[0]);
    TbListAccountCodePettyCash.$('tr.selected').removeClass('selected');
    $("#ModalAccountCodePettyCash").modal("hide");
});