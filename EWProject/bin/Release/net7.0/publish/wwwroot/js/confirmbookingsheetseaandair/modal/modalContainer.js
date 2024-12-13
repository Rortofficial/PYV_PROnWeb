var tbContainerList = $('#TbContainerList').DataTable();
$('#TbContainerList tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        tbContainerList.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});
$('#btnChooseContainer').click(function () {
    var index = tbContainerList.row('.selected').index();
    var data = tbContainerList.row(index).data();
    $('#txtContainer-'+$("#ModalContainer").attr("data-containerCode")).val(data[1]);
    $('#txtContainer-'+$("#ModalContainer").attr("data-containerCode")).attr("data-ContainerCode", data[0]);
    $('#txtContainerCost-' + $("#ModalContainer").attr("data-containerCode")).val(data[5]);
    //Set Value to Container Display and Description
    $('#txtContainerVendor-' + $("#ModalContainer").attr("data-containerCode")).attr("data-containercode", data[2]);
    $('#txtContainerVendor-' + $("#ModalContainer").attr("data-containerCode")).val(data[3]);
    //End Set Value to Container
    tbContainerList.$('tr.selected').removeClass('selected');
    $("#ModalContainer").modal("hide");
});
function ClickShowContainer(id) {
    $('#ModalContainer').attr("data-containerCode", $("#container-type-" + id).attr("data-containertypecode"));
}