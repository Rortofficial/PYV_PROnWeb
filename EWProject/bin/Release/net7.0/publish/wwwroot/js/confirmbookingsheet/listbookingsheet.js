$('#TbListBookingSheet thead tr')
    .clone(true)
    .addClass('filters')
    .appendTo('#TbListBookingSheet thead');
var tbEmployeeList = $('#TbListBookingSheet').DataTable({
    orderCellsTop: true,
    fixedHeader: true,
    initComplete: function () {
        var api = this.api();
        // For each column
        api
            .columns()
            .eq(0)
            .each(function (colIdx) {
                // Set the header cell to contain the input element
                var cell = $('.filters th').eq(
                    $(api.column(colIdx).header()).index()
                );
                var title = $(cell).text();
                $(cell).html('<input type="text" placeholder="' + title + '" />');

                // On every keypress in this input
                $(
                    'input',
                    $('.filters th').eq($(api.column(colIdx).header()).index())
                )
                    .off('keyup change')
                    .on('change', function (e) {
                        // Get the search value
                        $(this).attr('title', $(this).val());
                        var regexr = '({search})'; //$(this).parents('th').find('select').val();

                        var cursorPosition = this.selectionStart;
                        // Search the column for that value
                        api
                            .column(colIdx)
                            .search(
                                this.value != ''
                                    ? regexr.replace('{search}', '(((' + this.value + ')))')
                                    : '',
                                this.value != '',
                                this.value == ''
                            )
                            .draw();
                    })
                    .on('keyup', function (e) {
                        e.stopPropagation();

                        $(this).trigger('change');
                        $(this)
                            .focus()[0]
                            .setSelectionRange(cursorPosition, cursorPosition);
                    });
            });
    },
});

function onCopyListBookingSheet(url, bookingID, obj, urlBookingDetail) {
    //var a = JSON.parse(obj);
    //console.log(a);
    $.ajax({
        url: url,
        data: { BookingID: bookingID },
        beforeSend: function () {
            $('#Modalloder').modal('show');
        },
        success: function (data) {
            $("#ContainerInfo").html(data);
            $("#ModalBookingSheetList").modal("hide");
            var setobj = obj;//JSON.parse(obj);
            /*console.log(setobj)*/
            $("#txtBookingID").val(bookingID);
            $("#txtDestination").val(setobj.Destination);
            $("#txtOrigin").val(setobj.Origin);
            $("#txtjobType").val(setobj.ImportType);
            $("#txtRoute").val(setobj.Route);
            $("#txtJobNo").val(setobj.JobNo);
            $("#txtCardCode").val(setobj.COCode);
            $("#txtSplPerson").val(setobj.SaleEmployee);
            $("#txtSplPerson").attr("data-slpCode", setobj.SlpCode);
            $("#txtLoadingDate").val(setobj.LoadingDate);
            $("#txtCrossBorderDate").val(setobj.CrossBorderDate);
            $("#txtETARequirement").val(setobj.ETARequirementDate);
            $("#remarks").val(setobj.SpecialRequest);
            $("#CSName").val(setobj.CreateBy);
            $("#txtCommodities").val(setobj.COMMODITYS);
            $.ajax({
                url: urlBookingDetail,
                type: "GET",
                data: { BookingID: setobj.BookingID },
                dataType: "JSON",
                beforeSend: function () {
                    $('#Modalloder').modal('show');
                },
                success: function (dataBooking) {
                    $('#Modalloder').modal('hide');
                    $("#txtShipper").empty();
                    $("#txtShipper").attr("size", dataBooking.data[0].shipper.length);
                    for (var i = 0; i < dataBooking.data[0].shipper.length; i++) {
                        $("#txtShipper").append("<option>" + dataBooking.data[0].shipper[i] + "</option>");
                    }
                    $("#txtCO").empty();
                    $("#txtCO").attr("size", dataBooking.data[0].consignee.length);
                    for (var i = 0; i < dataBooking.data[0].consignee.length; i++) {
                        $("#txtCO").append("<option>" + dataBooking.data[0].consignee[i] + "</option>");
                    }
                    $("#txtPlaceOfLoading").empty();
                    $("#txtPlaceOfLoading").attr("size", dataBooking.data[0].placeOfLoading.length);
                    for (var i = 0; i < dataBooking.data[0].placeOfLoading.length; i++) {
                        $("#txtPlaceOfLoading").append("<option>" + dataBooking.data[0].placeOfLoading[i] + "</option>");
                    }
                    $("#txtLoadingOfDistrict").empty();
                    $("#txtLoadingOfDistrict").attr("size", dataBooking.data[0].placeOfLoadingDistrict.length);
                    for (var i = 0; i < dataBooking.data[0].placeOfLoadingDistrict.length; i++) {
                        $("#txtLoadingOfDistrict").append("<option>" + dataBooking.data[0].placeOfLoadingDistrict[i] + "</option>");
                    }
                    $("#txtPlaceOfDelivery").empty();
                    $("#txtPlaceOfDelivery").attr("size", dataBooking.data[0].placeOfDelivery.length);
                    for (var i = 0; i < dataBooking.data[0].placeOfDelivery.length; i++) {
                        $("#txtPlaceOfDelivery").append("<option>" + dataBooking.data[0].placeOfDelivery[i] + "</option>");
                    }
                    $("#txtDeliveryOfDistrict").empty();
                    $("#txtDeliveryOfDistrict").attr("size", dataBooking.data[0].placeOfDeliveryDistrict.length);
                    for (var i = 0; i < dataBooking.data[0].placeOfDeliveryDistrict.length; i++) {
                        $("#txtDeliveryOfDistrict").append("<option>" + dataBooking.data[0].placeOfDeliveryDistrict[i] + "</option>");
                    }
                    if (dataBooking.data[0].thaiBorder.length < 2) {
                        $("#txtBorder1").val(dataBooking.data[0].thaiBorder[0]);
                    } else {
                        $("#txtBorder1").val(dataBooking.data[0].thaiBorder[0]);
                        $("#txtBorder2").val(dataBooking.data[0].thaiBorder[1]);
                    }
                },
                error: function (erro) {
                    $('#Modalloder').modal('hide');
                    $('#ModalSuccess').modal('show');
                    $('#ErrMsg').html(erro.responseText);
                }
            });
            $('#Modalloder').modal('hide');
        },
        error: function (erro) {
            $('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}
