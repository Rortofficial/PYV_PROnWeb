var TbListBPCodePettyCash = $('#TbListBPCodePettyCash').DataTable();
$('#TbListBPCodePettyCash tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        TbListBPCodePettyCash.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseBpCodePettyCash').click(function () {
    var index = TbListBPCodePettyCash.row('.selected').index();
    var data = TbListBPCodePettyCash.row(index).data();
    $('#txtBpName').val(data[1]);
    $('#txtBPCode').val(data[0]);
    $('#txtBPCode').attr("data-BPCode", data[0]);
    $('#txtBPCode').attr("data-BPBalance", data[2]);
    TbListBPCodePettyCash.$('tr.selected').removeClass('selected');
    $("#ModalBpCodePettyCash").modal("hide");
});