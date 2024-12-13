var tbListShipper =$('#TbListShipper').DataTable();
$('#TbListShipper tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbListShipper.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseShipper').click(function () {
    var index = tbListCO.row('.selected').index();
    var data = tbListCO.row(index).data();
    $('#txtShipper').val(data[1]);
    $('#txtShipper').attr("data-ShipperCode", data[0]);
    tbListShipper.$('tr.selected').removeClass('selected');
    $("#ModalShipper").modal("hide");
});