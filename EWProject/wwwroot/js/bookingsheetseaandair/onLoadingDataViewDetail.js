function ViewBookingSheetDetailByDocEntry(data)
{
    $("#cboSelectSeries").val(data.data.headerObj.ewSeries);
    $("#BookingID").val(data.data.headerObj.bookingID);
    $("#btnJobNumber").val(data.data.headerObj.jobNo);
    $("#txtBookingDate").val(data.data.headerObj.bookingDate);
    $("#cboOrigin").val(data.data.headerObj.origin);
    getProvinceLoadingByCountry($("#cboOrigin option:selected").text());
    $("#cboDestination").val(data.data.headerObj.destination);
    getProvinceDeliveryByCountry($("#cboDestination option:selected").text());
    overseaForwarder($("#cboDestination option:selected").text());
    $("#txtShowOrigin").val($("#cboOrigin option:selected").text());
    $("#txtShowDestination").val($("#cboDestination option:selected").text());
    $("#cboSalePerson1").val(data.data.headerObj.salePersonCode);
    $("#cboImportType").val(data.data.headerObj.jobType);
    onChangeJobNo();
    onSelectedSeries();
    $("#cboServiceType").val(data.data.headerObj.serviceTypeCode);
    $("#txtBookingNo").val(data.data.headerObj.bookingNo);
    var COLoader = [];
    for (var i = 0; i < data.data.listCOLoader.length; i++) {
        COLoader.push(data.data.listCOLoader[i].cardCode);
    }
    $('#txtColoader').val(COLoader).trigger("change");
    var Customer = [];
    for (var i = 0; i < data.data.listCustomer.length; i++) {
        Customer.push(data.data.listCustomer[i].cardCode);
    }
    $('#txtCustomerName').val(Customer).trigger("change");
    $('input[name="REPAID/COLLECTION"]').val([data.data.headerObj.freight]);
    var shipper = [];
    for (var i = 0; i < data.data.listShipper.length; i++) {
        shipper.push(data.data.listShipper[i].cardCode);
    }
    $('#txtShipper').val(shipper).trigger("change");
    var consignee = [];
    for (var i = 0; i < data.data.listConsignee.length; i++) {
        consignee.push(data.data.listConsignee[i].cardCode);
    }
    $('#txtConsignee').val(consignee).trigger("change");
    var shippingLine = [];
    for (var i = 0; i < data.data.listShippingLine.length; i++) {
        shippingLine.push(data.data.listShippingLine[i].cardCode);
    }
    $('#txtShippingLine').val(shippingLine).trigger("change");
    var DestAgent = [];
    for (var i = 0; i < data.data.listDestAgent.length; i++) {
        DestAgent.push(data.data.listDestAgent[i].cardCode);
    }
    $('#txtdestAgent').val(DestAgent).trigger("change");
    $('#txtFeeder').val(data.data.headerObj.feeder);
    $('#txtFeederVoy').val(data.data.headerObj.feederVoy);
    $('#txtVessel').val(data.data.headerObj.vessel);
    $('#txtVesselVoy').val(data.data.headerObj.vesselVoy);
    for (var i = 0; i < data.data.listCommodities.length; i++) {
        numberEvenOrOdd++;
        var objInvoice = {};
        objInvoice.ID = numberEvenOrOdd;
        objInvoice.InvoiceNumber = data.data.listCommodities[i].invoice;
        listInvoice.push(objInvoice);
    }
    RerenderAgain();
    $("#txtGoodsDescription").val(data.data.headerObj.goodsDescription);
    $("#txtTotalPackage").val(data.data.headerObj.totalPackage);
    $("#txtNetWeight").val(data.data.headerObj.netWeight);
    $("#txtGrossWeight").val(data.data.headerObj.grossWeight);
    $("#txtLoadingDate").val(new Date(data.data.headerObj.loadingDate).getFullYear() + '-' + getMonth(data.data.headerObj.loadingDate) + '-' + getDate(data.data.headerObj.loadingDate)); //(new Date(data.data.loadingDate).getMonth()+1) new Date(data.data.loadingDate).getDate()
    $("#txtCrossBorderDate").val(new Date(data.data.headerObj.crossBorderDate).getFullYear() + '-' + getMonth(data.data.headerObj.crossBorderDate) + '-' + getDate(data.data.headerObj.crossBorderDate));
    $("#txtEtaRequirement").val(new Date(data.data.headerObj.etaRequirement).getFullYear() + '-' + getMonth(data.data.headerObj.etaRequirement) + '-' + getDate(data.data.headerObj.etaRequirement));
    $("#txtEtdRequirement").val(new Date(data.data.headerObj.etdrequirement).getFullYear() + '-' + getMonth(data.data.headerObj.etaRequirement) + '-' + getDate(data.data.headerObj.etaRequirement));
    $("#txtCrossBorderDate").prop('disabled', false);
    $("#txtEtaRequirement").prop('disabled', false);
    $("#txtCrossBorderDate").attr("min", $("#txtLoadingDate").val());
    $("#txtEtaRequirement").attr("min", $("#txtCrossBorderDate").val());
    var overseatrucker = [];
    for (var i = 0; i < data.data.listOverseaTransportSeaAndAir.length; i++) {
        overseatrucker.push(data.data.listOverseaTransportSeaAndAir[i].cardCode);
    }
    $('#cboMultiOverseaTrucker').val(overseatrucker).trigger("change");
    $("#txtLoloYardRemark").val(data.data.headerObj.remarkLOLOYard);
    $('input[name="typeloloyard/unloading"]').val([data.data.headerObj.loloyarD_UNLOADING]);
    var placeofreceipt = [];
    for (var i = 0; i < data.data.listPortOfReceiptSeaAndAir.length; i++) {
        placeofreceipt.push(data.data.listPortOfReceiptSeaAndAir[i].code);
    }
    $('#cboMultiplePlaceOfReceipt').val(placeofreceipt).trigger("change");
    var portofdischarge = [];
    for (var i = 0; i < data.data.listPortOfDischargeSeaAndAir.length; i++) {
        portofdischarge.push(data.data.listPortOfDischargeSeaAndAir[i].code);
    }
    $('#cboMultiPortOfDischarge').val(portofdischarge).trigger("change");
    var placeOfLoading = [];
    for (var i = 0; i < data.data.listPlaceOfLoadingSeaAndAir.length; i++) {
        placeOfLoading.push(data.data.listPlaceOfLoadingSeaAndAir[i].code);
    }
    $('#cboMultiPlaceOfLoading').val(placeOfLoading).trigger("change");
    var placeOfDelivery = [];
    for (var i = 0; i < data.data.listPlaceOfDeliverySeaAndAir.length; i++) {
        placeOfDelivery.push(data.data.listPlaceOfDeliverySeaAndAir[i].code);
    }
    $('#cboMultiPlaceOfDelivery').val(placeOfDelivery).trigger("change");
    var thaiForwarder = [];
    for (var i = 0; i < data.data.listThaiForwarderSeaAndAir.length; i++) {
        thaiForwarder.push(data.data.listThaiForwarderSeaAndAir[i].cardCode);
    }
    $('#cboThaiForwarder').val(thaiForwarder).trigger("change");
    var overseaForwarders = [];
    for (var i = 0; i < data.data.listOverSeaForwarderSeaAndAir.length; i++) {
        overseaForwarders.push(data.data.listOverSeaForwarderSeaAndAir[i].cardCode);
    }
    $('#cboOverseaForwarder').val(overseaForwarders).trigger("change");
    var thaiborder = [];
    for (var i = 0; i < data.data.listThaiBorderSeaAndAir.length; i++) {
        thaiborder.push(data.data.listThaiBorderSeaAndAir[i].code);
    }
    $('#cboThaiBorder').val(thaiborder).trigger("change");
    $('input[name="lcl/fcl"]').val([data.data.headerObj.lcL_FCL_CY_CFS]);
    var cyAt = "";
    for (var i = 0; i < data.data.listCYAt_ContactSeaAndAir.length; i++) {
        cyAt=data.data.listCYAt_ContactSeaAndAir[i].cardCode;
    }
    $('#txtCYatContact').val(cyAt);
    $("#txtCYDate").val(new Date(data.data.headerObj.cyDate).getFullYear() + '-' + getMonth(data.data.headerObj.cyDate) + '-' + getDate(data.data.headerObj.cyDate));
    var returnAt = "";
    for (var i = 0; i < data.data.listReturnAt_ContactSeaAndAir.length; i++) {
        returnAt=data.data.listReturnAt_ContactSeaAndAir[i].cardCode;
    }
    $('#txtReturnAtContact').val(returnAt);
    $("#txtReturnDate").val(new Date(data.data.headerObj.returnDate).getFullYear() + '-' + getMonth(data.data.headerObj.returnDate) + '-' + getDate(data.data.headerObj.returnDate));
    $("#txtLastReturnDate").val(new Date(data.data.headerObj.lastReturnDate).getFullYear() + '-' + getMonth(data.data.headerObj.lastReturnDate) + '-' + getDate(data.data.headerObj.lastReturnDate));
    $("#txtreturnBefore").val(convertTimeTo24hour(data.data.headerObj.returnBefore));
    var loadingAt = [];
    for (var i = 0; i < data.data.listLoadingAtSeaAndAir.length; i++) {
        loadingAt.push(data.data.listLoadingAtSeaAndAir[i].cardCode);
    }
    $('#txtLoadingAt').val(loadingAt).trigger("change");
    $("#txtClosingDate").val(new Date(data.data.headerObj.closingdate).getFullYear() + '-' + getMonth(data.data.headerObj.closingdate) + '-' + getDate(data.data.headerObj.closingdate));
    $("#txtclosingBefore").val(convertTimeTo24hour(data.data.headerObj.closingtime));
    $("#cboDamgeGoods").val(data.data.headerObj.dg);
    $("#documentRequirement").val(data.data.headerObj.documentRequest);
    $("#specialRequest").val(data.data.headerObj.specialRequest);
    $("#txtMAWBAir").val(data.data.headerObj.mawb);
    $("#txtLoadingDateAir").val(new Date(data.data.headerObj.loadingDateAir).getFullYear() + '-' + getMonth(data.data.headerObj.loadingDateAir) + '-' + getDate(data.data.headerObj.loadingDateAir));
    $("#txtCutOffTimeAir").val(new Date(data.data.headerObj.closingdate).getFullYear() + '-' + getMonth(data.data.headerObj.closingdate) + '-' + getDate(data.data.headerObj.closingdate) +'T'+ convertTimeTo24hour(data.data.headerObj.closingtime));
    $("#txtLoadingPlaceAir").val(data.data.headerObj.loadingPlaceAir);
    $("#txtWarehouseAir").val(data.data.headerObj.warehouse);
    $("#txtContactAir").val(data.data.headerObj.contact);
    $("#txtAddressAir").val(data.data.headerObj.address);
    $("#txtTelAir").val(data.data.headerObj.tel);
    if (data.data.flightsInformations.length != 0) {
        for (var i = 0; i < data.data.flightsInformations.length; i++) {
            indexInforAir++;
            var objAirInformation = {};
            objAirInformation.ID = indexInforAir;
            objAirInformation.FlightFromTo = data.data.flightsInformations[i].flightFromTo;
            objAirInformation.FlightNo = data.data.flightsInformations[i].flightNo;
            objAirInformation.FlightArrival = data.data.flightsInformations[i].flightArrival;
            listInfoAir.push(objAirInformation);
            RerenderInfoAirAgain();
        }
        
    }
    for (var i = 0; i < data.data.listContainerTypeSeaAndAir.length; i++) {
        numberEvenOrOddVolume++;
        var objVolumn = {};
        objVolumn.ID = numberEvenOrOddVolume;
        objVolumn.VolumeText = data.data.listContainerTypeSeaAndAir[i].containerType;
        objVolumn.VolumeQty = data.data.listContainerTypeSeaAndAir[i].qty;//$("#txtVolumeQty").val();
        objVolumn.VolumeName = data.data.listContainerTypeSeaAndAir[i].containerType;
        objVolumn.GrossWeight = data.data.listContainerTypeSeaAndAir[i].weight;//$("#txtVolumeGrossWeight").val();
        if (data.data.listContainerTypeSeaAndAir[i].listInvoice != "") {
            objVolumn.invoiceList = data.data.listContainerTypeSeaAndAir[i].listInvoice.split(",");//$("#txtVolumeGrossWeight").val();.toString()
        } else {
            objVolumn.invoiceList = [];
        }
        //console.log(objVolumn);
        listVolumn.push(objVolumn);
        //console.log(listVolumn);

    }
    RerenderVolumn();
    for (var i = 0; i < data.data.listTruckTypeSeaAndAir.length; i++) {
        numberEvenOrOddTruckType++;
        var objTruckType = {};
        objTruckType.ID = numberEvenOrOddTruckType;
        objTruckType.TruckTypeText = data.data.listTruckTypeSeaAndAir[i].containerType;
        objTruckType.TruckTypeQty = data.data.listTruckTypeSeaAndAir[i].qty;//$("#txtTruckTypeQty").val();
        objTruckType.TruckTypeName = data.data.listTruckTypeSeaAndAir[i].containerType;
        objTruckType.GrossWeight = data.data.listTruckTypeSeaAndAir[i].weight;//$("#txtTruckTypeGrossWeight").val();
        if (data.data.listTruckTypeSeaAndAir[i].listInvoice != "") {
            objTruckType.invoiceList = data.data.listTruckTypeSeaAndAir[i].listInvoice.split(",");//$("#txtVolumeGrossWeight").val();.toString()
        } else {
            objTruckType.invoiceList = [];
        }
        listTruckType.push(objTruckType);
    }
    RerenderTruckType();
}

function getMonth(date1) {
    var a = new Date(date1).getMonth()+1;
    return (a < 10) ? "0" + a : a;
}
function getDate(date1) {
    var a = new Date(date1).getDate();
    return (a < 10) ? "0" + a : a;
}
function convertTimeTo24hour(value) {
    var time = value;
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM == "PM" && hours < 12) hours = hours + 12;
    if (AMPM == "AM" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return(sHours + ":" + sMinutes);
}