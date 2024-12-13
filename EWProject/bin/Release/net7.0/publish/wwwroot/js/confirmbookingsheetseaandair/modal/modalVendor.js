var tbVendorList = $('#TbVendorList').DataTable();
var modalVendorType = "";
$('#TbVendorList tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbVendorList.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseVendor').click(function () {
    var index = tbVendorList.row('.selected').index();
    var data = tbVendorList.row(index).data();
    if (modalVendorType == "TruckVendor") {
        $('#txtTruckVendor-'+$("#ModalVendor").attr("data-VendorCode")).val(data[1]);
        $('#txtTruckVendor-'+$("#ModalVendor").attr("data-VendorCode")).attr("data-TruckCode", data[0]);
    } else if (modalVendorType=="ContainerVendor") {
        $('#txtContainerVendor-'+$("#ModalVendor").attr("data-VendorCode")).val(data[1]);
        $('#txtContainerVendor-'+$("#ModalVendor").attr("data-VendorCode")).attr("data-ContainerCode", data[0]);
    }
    tbVendorList.$('tr.selected').removeClass('selected');
    $("#ModalVendor").modal("hide");
});
function ModalVendorShow(type,id) {
    modalVendorType = type;
    if (modalVendorType == "TruckVendor") {
        $('#ModalVendor').attr("data-vendorcode", $("#txtContainer-" + id).attr("data-TruckCode"));
    } else if (modalVendorType == "ContainerVendor") {
        $('#ModalVendor').attr("data-vendorcode", $("#container-type-" + id).attr("data-containertypecode"));
    }
    $("#ModalVendor").modal("show");
}