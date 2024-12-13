var tbTruckList = $('#TbTruckList').DataTable();
$('#TbTruckList tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbTruckList.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseTruck').click(function () {
    var index = tbTruckList.row('.selected').index();
    var data = tbTruckList.row(index).data();
    $('#txtTruckNo-'+$("#ModalTruck").attr("data-TruckCode")).val(data[2]);
    $('#txtTruckNo-' + $("#ModalTruck").attr("data-TruckCode")).attr("data-TruckCode", data[1]);
    $('#txtTruckVendor-'+$("#ModalTruck").attr("data-TruckCode")).val(data[4]);
    $('#txtTruckVendor-'+$("#ModalTruck").attr("data-TruckCode")).attr("data-TruckCode", data[3]);
    $('#txtTruckCost-'+$("#ModalTruck").attr("data-TruckCode")).val(data[6]);
    //Driver 1
    $('#txtDriver1-' + $("#ModalTruck").attr("data-TruckCode")).val(data[7]);
    $('#txtDriver1-' + $("#ModalTruck").attr("data-TruckCode")).attr("data-driver1code", $("#DriveID-" + data[0]).attr("data-driverID"));
    $('#txtHP1-'+$("#ModalTruck").attr("data-TruckCode")).val(data[8]);
    $('#txtDriverID1-'+$("#ModalTruck").attr("data-TruckCode")).val(data[9]);
    $('#txtLicenseExpDate1-' + $("#ModalTruck").attr("data-TruckCode")).val(data[10]);
    //Driver 2
    $('#txtDriver2-' + $("#ModalTruck").attr("data-TruckCode")).val(data[7]);
    $('#txtDriver2-' + $("#ModalTruck").attr("data-TruckCode")).attr("data-driver1code", $("#DriveID-" + data[0]).attr("data-driverID"));
    $('#txtHP2-' + $("#ModalTruck").attr("data-TruckCode")).val(data[8]);
    $('#txtDriverID2-' + $("#ModalTruck").attr("data-TruckCode")).val(data[9]);
    $('#txtLicenseExpDate2-' + $("#ModalTruck").attr("data-TruckCode")).val(data[10]);

    tbTruckList.$('tr.selected').removeClass('selected');
    $("#ModalTruck").modal("hide");
});
function ClickShowTruck(id) {
    $('#ModalTruck').attr("data-TruckCode", $("#txtContainer-" + id).attr("data-TruckCode"));
}