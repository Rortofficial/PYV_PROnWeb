function ViewBookingSheetDetailByDocEntry(docEntry, url)
{
    $.ajax({
        url: url,
        type: "GET",
        data: { docEntry: docEntry },
        dataType: "JSON",
        success: function (data) {
            $("#cboSelectSeries").val(data.data.ewSereis);
            if ($("#cboSelectSeries").val() == "EWST") {

                $("#cboMultiPlaceOfLoading").select2({
                    placeholder: "Select a Place Of Loading",
                    maximumSelectionLength: 1,
                    width: '100%'
                });
                $("#cboMultiPlaceOfDelivery").select2({
                    placeholder: "Select a Place Of Delivery",
                    maximumSelectionLength: 1
                });
                $("#plDistrictLoading").css("display", "");
                $("#plDistrictDelivery").css("display", "");
            }
            $("#BookingID").val(data.data.docEntry);
            $("#btnJobNumber").val(data.data.jobNumber);
            $("#txtBookingDate").val(data.data.bookingDate);
            $("#cboOrigin").val(data.data.origin);
            getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
            $("#cboDestination").val(data.data.destination);
            getProvinceDeliveryByCountry($("#cboDestination option:selected").text());
            overseaForwarder($("#cboDestination option:selected").text());
            $("#txtShowOrigin").val($("#cboOrigin option:selected").text());
            $("#txtShowDestination").val($("#cboDestination option:selected").text());
            $("#cboSalePerson1").val(data.data.saleEmployee);
            $("#cboImportType").val(data.data.importType);
            onSelectedSeries();
            $("#cboServiceType").val(data.data.serviceType);
            var shipper = [];
            for (var i = 0; i < data.data.shipper.length; i++) {
                shipper.push(data.data.shipper[i].shipper);
            }
            $('#txtShipper').val(shipper).trigger("change");
            var consignee = [];
            for (var i = 0; i < data.data.consignee.length; i++) {
                consignee.push(data.data.consignee[i].consignee);
            }
            $('#txtConsignee').val(consignee).trigger("change");
            var salesquotations = [];
            for (var i = 0; i < data.data.saleQuotation.length; i++) {
                salesquotations.push(data.data.saleQuotation[i].docentry);
            }
            for (var i = 0; i < data.data.commodities.length; i++) {
                numberEvenOrOdd++;
                var objInvoice = {};
                objInvoice.ID = numberEvenOrOdd;
                objInvoice.InvoiceNumber = data.data.commodities[i].invoice;
                listInvoice.push(objInvoice);
            }
            RerenderAgain();
            $("#txtGoodsDescription").val(data.data.goodDescription);
            $("#txtTotalPackage").val(data.data.totalPackage);
            $("#txtNetWeight").val(data.data.netWeight);
            $("#txtGrossWeight").val(data.data.grossWeight);
            $("#txtLoadingDate").val(new Date(data.data.loadingDate).getFullYear() + '-' + getMonth(data.data.loadingDate) + '-' + getDate(data.data.loadingDate)); //(new Date(data.data.loadingDate).getMonth()+1) new Date(data.data.loadingDate).getDate()
            $("#txtCrossBorderDate").val(new Date(data.data.corssBorderDate).getFullYear() + '-' + getMonth(data.data.corssBorderDate) + '-' + getDate(data.data.corssBorderDate));
            $("#txtEtaRequirement").val(new Date(data.data.etaRequirement).getFullYear() + '-' + getMonth(data.data.etaRequirement) + '-' + getDate(data.data.etaRequirement));
            $("#txtCrossBorderDate").prop('disabled', false);
            $("#txtEtaRequirement").prop('disabled', false);
            $("#txtCrossBorderDate").attr("min", $("#txtLoadingDate").val());
            $("#txtEtaRequirement").attr("min", $("#txtCrossBorderDate").val());
            var overseatrucker = [];
            //console.log(data.data);
            for (var i = 0; i < data.data.overseaTrucker.length; i++) {
                overseatrucker.push(data.data.overseaTrucker[i].overseatruckercode);
            }
            $('#cboMultiOverseaTrucker').val(overseatrucker).trigger("change");
            $("#txtLoloYard").val(data.data.loloyardRemark);
            $('input[name="typeloloyard/unloading"]').val([data.data.loloYardOrUnloading]);
            if ($('input[name="typeloloyard/unloading"]:checked').val() == '3') {
                $("#plLoloOther").removeAttr("style");
                $("#plLoloOtherTxt").removeAttr("style");
            }
            $("#txtLoloOtherRemark").val(data.data.otherRemark);
            $("#cboDamageGood").val(data.data.dg);
            var placeOfLoading = [];
            for (var i = 0; i < data.data.placeOfLoadings.length; i++) {
                placeOfLoading.push(data.data.placeOfLoadings[i].placeloading);
            }
            if (placeOfLoading.length != 0) {
                $('#cboMultiPlaceOfLoading').val(placeOfLoading).trigger("change");
                $('#cboMultiDistrictPlaceOfLoading').val(data.data.placeOfLoadings[0].district.split(",")).trigger("change");
            }            
            var placeOfDelivery = [];
            for (var i = 0; i < data.data.placeOfDeliveries.length; i++) {
                placeOfDelivery.push(data.data.placeOfDeliveries[i].placedelivery);
            }
            if (placeOfDelivery.length != 0) {
                $('#cboMultiPlaceOfDelivery').val(placeOfDelivery).trigger("change");
                $('#cboMultiDistrictPlaceOfDelivery').val(data.data.placeOfDeliveries[0].district.split(",")).trigger("change");
            }            
            var thaiForwarder = [];
            for (var i = 0; i < data.data.thaiForwarder.length; i++) {
                thaiForwarder.push(data.data.thaiForwarder[i].thaiforwarder);
            }
            $('#cboThaiForwarder').val(thaiForwarder).trigger("change");
            var overseaForwarders = [];
            for (var i = 0; i < data.data.overseaForwarder.length; i++) {
                overseaForwarders.push(data.data.overseaForwarder[i].overseaforwardercode);
            }
            $('#cboOverseaForwarder').val(overseaForwarders).trigger("change");
            var thaiborder = [];
            for (var i = 0; i < data.data.thaiBorders.length; i++) {
                thaiborder.push(data.data.thaiBorders[i].thaiBorder);
            }
            $('#cboThaiBorder').val(thaiborder).trigger("change");
            $('input[name="lcl/fcl"]').val([data.data.lclOrFCL]);
            $('input[name="cy/cfs"]').val([data.data.cyOrCFS]);
            $("#documentRequirement").val(data.data.remarks);
            $("#specialRequest").val(data.data.speacialRequest);
            for (var i = 0; i < data.data.volumes.length; i++) {
                numberEvenOrOddVolume++;
                var objVolumn = {};
                objVolumn.ID = numberEvenOrOddVolume;
                objVolumn.VolumeText = data.data.volumes[i].volumecode;
                objVolumn.VolumeQty = data.data.volumes[i].qty;//$("#txtVolumeQty").val();
                objVolumn.VolumeName = data.data.volumes[i].volumecode;
                objVolumn.GrossWeight = data.data.volumes[i].grossweight;//$("#txtVolumeGrossWeight").val();
                if (data.data.volumes[i].invoicelist != "") {
                    objVolumn.invoiceList = data.data.volumes[i].invoiceList.split(",");//$("#txtVolumeGrossWeight").val();.toString()
                } else {
                    objVolumn.invoiceList = [];
                }
                //console.log(objVolumn);
                listVolumn.push(objVolumn);
                //console.log(listVolumn);

            }
            RerenderVolumn();
            for (var i = 0; i < data.data.truckType.length; i++) {
                numberEvenOrOddTruckType++;
                var objTruckType = {};
                objTruckType.ID = numberEvenOrOddTruckType;
                objTruckType.TruckTypeText = data.data.truckType[i].trucktype;
                objTruckType.TruckTypeQty = data.data.truckType[i].qty;//$("#txtTruckTypeQty").val();
                objTruckType.TruckTypeName = data.data.truckType[i].trucktype;
                objTruckType.GrossWeight = data.data.truckType[i].grossweight;//$("#txtTruckTypeGrossWeight").val();
                if (data.data.truckType[i].invoicelist != "") {
                    objTruckType.invoiceList = data.data.truckType[i].invoiceList.split(",");//$("#txtVolumeGrossWeight").val();.toString()
                } else {
                    objTruckType.invoiceList = [];
                }
                listTruckType.push(objTruckType);
            }
            RerenderTruckType();
            getObjSalesQuotationByCountry($("#cboOrigin option:selected").text() + "-" + $("#cboDestination option:selected").text());
            $('#txtSaleQuotations').val(salesquotations).trigger("change");
        },
        error: function (erro) {
            //$('#Modalloder').modal('hide');
            $('#ModalSuccess').modal('show');
            $('#ErrMsg').html(erro.responseText);
        }
    });
}

function getMonth(date1) {
    var a = new Date(date1).getMonth()+1;
    return (a < 10) ? "0" + a : a;
}
function getDate(date1) {
    var a = new Date(date1).getDate();
    return (a < 10) ? "0" + a : a;
}