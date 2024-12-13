var tbEmployeeList = $('#TbEmployeeList').DataTable();
modalEmployeeType = "";
$('#TbEmployeeList tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbEmployeeList.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseEmployee').click(function () {
    var index = tbEmployeeList.row('.selected').index();
    var data = tbEmployeeList.row(index).data();
    if (modalEmployeeType == "driver1") {
        $('#txtDriver1-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[1]);
        $('#txtDriver1-'+$("#ModalEmployeeList").attr("data-employeecode")).attr("data-Driver1Code", data[0]);
        $('#txtHP1-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[2]);
        $('#txtDriverID1-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[3]);
        $('#txtLicenseExpDate1-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[4]);
    } else if (modalEmployeeType == "driver2") {
        $('#txtDriver2-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[1]);
        $('#txtDriver2-'+$("#ModalEmployeeList").attr("data-employeecode")).attr("data-Driver2Code", data[0]);
        $('#txtHP2-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[2]);
        $('#txtDriverID2-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[3]);
        $('#txtLicenseExpDate2-'+$("#ModalEmployeeList").attr("data-employeecode")).val(data[4]);
    }
    tbEmployeeList.$('tr.selected').removeClass('selected');
    $("#ModalEmployeeList").modal("hide");
});
function ModalEmployeeShow(type,id) {
    modalEmployeeType = type;
    $('#ModalEmployeeList').attr("data-employeecode", $("#txtContainer-" + id).attr("data-TruckCode"));
    $("#ModalEmployeeList").modal("show");
}