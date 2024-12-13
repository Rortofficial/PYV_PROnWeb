function onConfirmTransaction(url, urlPDF) {
    event.preventDefault();
    if (confirm("Please Confirm Transaction Before Add Make sure it correct !")) {
        var bookingSheetPost = {};
        var invoiceList = [];
        var placeLoadingList = [];
        var placeDeliveryList = [];
        var volumeList = [];
        var truckTypeList = [];
        var cboThaiBorderList = [];
        var cboOverseaTruckerList = [];
        var cboOverForwarderList = [];
        var cboSalesQuotationList = [];
        var cboThaiForwarderList = [];
        var cboSHIPPERList = [];
        var cboConsigneeList = [];
        var cboCOLOADER = [];
        var cboCustomer = [];
        var cboSHIPPINGLINE = [];
        var cboDESTAGENT = [];
        var cboPORTOFDISCHARGE = [];
        var cboPORTOFRECEIPT = [];
        var cboCYAtContact = [];
        var cboReturnAtContact = [];
        var cboLoadingAt = [];
        //Add COLOADER
        var txtColoader = $("#txtColoader").val();
        for (var i = 0; i < txtColoader.length; i++) {
            var Coloader = {};
            Coloader.COLOADER = txtColoader[i];
            cboCOLOADER.push(Coloader);
        }
        //Add Customer Name
        var txtCustomerName = $("#txtCustomerName").val();
        for (var i = 0; i < txtCustomerName.length; i++) {
            var CustomerName = {};
            CustomerName.CUSTOMERCODE = txtCustomerName[i];
            cboCustomer.push(CustomerName);
        }
        //Add cboSHIPPERList
        var txtShipper = $("#txtShipper").val();
        for (var i = 0; i < txtShipper.length; i++) {
            var cboShippers = {};
            cboShippers.SHIPPER = txtShipper[i];
            cboSHIPPERList.push(cboShippers);
        }
        //Add cboConsigneeList
        var txtConsignee = $("#txtConsignee").val();
        for (var i = 0; i < txtConsignee.length; i++) {
            var cboConsignees = {};
            cboConsignees.CONSIGNEE = txtConsignee[i];
            cboConsigneeList.push(cboConsignees);
        }
        //Add ShippingLine
        var txtShippingLine = $("#txtShippingLine").val();
        for (var i = 0; i < txtShippingLine.length; i++) {
            var ShippingLine = {};
            ShippingLine.SHIPPINGLINE = txtShippingLine[i];
            cboSHIPPINGLINE.push(ShippingLine);
        }
        //DEST. AGENT
        var txtdestAgent = $("#txtdestAgent").val();
        for (var i = 0; i < txtdestAgent.length; i++) {
            var destAgent = {};
            destAgent.DESTAGENT = txtdestAgent[i];
            cboDESTAGENT.push(destAgent);
        }
        //Add Commodities
        for (var i = 0; i < listInvoice.length; i++) {
            var invoice = {};
            invoice.Invoice = listInvoice[i].InvoiceNumber;
            invoiceList.push(invoice);
        }
        //Add OverseaTrucker
        var cboOverseaTrucker = $("#cboMultiOverseaTrucker").val();
        for (var i = 0; i < cboOverseaTrucker.length; i++) {
            var cboOverseaTruckers = {};
            cboOverseaTruckers.OVEARSEATRANSPORT = cboOverseaTrucker[i];
            cboOverseaTruckerList.push(cboOverseaTruckers);
        }
        //Add PORT OF RECEIPT
        var cboMultiplePlaceOfReceipt = $("#cboMultiplePlaceOfReceipt").val();
        for (var i = 0; i < cboMultiplePlaceOfReceipt.length; i++) {
            var PORTOFRECEIPT = {};
            PORTOFRECEIPT.PORTOFRECEIPT = cboMultiplePlaceOfReceipt[i];
            cboPORTOFRECEIPT.push(PORTOFRECEIPT);
        }
        //Add PORT OF DISCHARGE
        var cboMultiPortOfDischarge = $("#cboMultiPortOfDischarge").val();
        for (var i = 0; i < cboMultiPortOfDischarge.length; i++) {
            var PortOfDischarge = {};
            PortOfDischarge.PORTOFDISCHARGE = cboMultiPortOfDischarge[i];
            cboPORTOFDISCHARGE.push(PortOfDischarge);
        }
        //Add Place Of Loading
        var cboPlaceOfLoading = $("#cboMultiPlaceOfLoading").val();
        for (var i = 0; i < cboPlaceOfLoading.length; i++) {
            var placeOfLoadings = {};
            placeOfLoadings.PLACEOFLOADING = cboPlaceOfLoading[i];
            placeLoadingList.push(placeOfLoadings);
        }
        //Add Place Of Delivery
        var cboPlaceOfDelivery = $("#cboMultiPlaceOfDelivery").val();
        for (var i = 0; i < cboPlaceOfDelivery.length; i++) {
            var placeOfDeliveries = {};
            placeOfDeliveries.PLACEOFDELIVERY = cboPlaceOfDelivery[i];
            placeDeliveryList.push(placeOfDeliveries);
        }
        //Add cboThaiForwarderList
        var cboThaiForwarder = $("#cboThaiForwarder").val();
        for (var i = 0; i < cboThaiForwarder.length; i++) {
            var cboThaiForwarders = {};
            cboThaiForwarders.THAIFORWARDER = cboThaiForwarder[i];
            cboThaiForwarderList.push(cboThaiForwarders);
        }
        //Add Volumne
        if (listVolumn.length != 0) {
            for (var i = 0; i < listVolumn.length; i++) {
                var volume = {};
                volume.Qty = $("#txtVolumQty"+listVolumn[i].ID).val();//listVolumn[i].VolumeQty;
                volume.ContainerType = listVolumn[i].VolumeName;
                volume.Weight = $("#txtVolumGrossWeight" + listVolumn[i].ID).val();
                volume.ListInvoice = (listVolumn[i].invoiceList.length == undefined)?'':listVolumn[i].invoiceList.join();
                volumeList.push(volume);
            }
        }
        //Add OverseaForwarder
        var cboOverseaForwarder = $("#cboOverseaForwarder").val();
        for (var i = 0; i < cboOverseaForwarder.length; i++) {
            var cboOverseaForwarders = {};
            cboOverseaForwarders.OVERSEAFORWARDER = cboOverseaForwarder[i];//OVERSEAFORWARDERCODE
            cboOverForwarderList.push(cboOverseaForwarders);
        }
        //Add Thai Border
        var cboThaiBorder = $("#cboThaiBorder").val();
        for (var i = 0; i < cboThaiBorder.length; i++) {
            var cboThaiBorders = {};
            cboThaiBorders.THAIBORDER = cboThaiBorder[i];
            cboThaiBorderList.push(cboThaiBorders);
        }
        //Add TruckType
        if (listTruckType.length != 0) {
            for (var i = 0; i < listTruckType.length; i++) {
                var truckType = {};
                truckType.Qty = $("#txtTruckTypeQty" + listTruckType[i].ID).val();//listTruckType[i].TruckTypeQty;
                truckType.ContainerType = listTruckType[i].TruckTypeName;
                truckType.Weight = $("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val();
                truckType.ListInvoice = (listTruckType[i].invoiceList.length == undefined)?'':listTruckType[i].invoiceList.join();
                truckTypeList.push(truckType);
            }
        }
        //Add CY AT / CONTACT
        //var txtCYatContact = $("#txtCYatContact").val();
        var CYatContact = {};
        CYatContact.CYAt_Contact = $("#txtCYatContact").val();
        cboCYAtContact.push(CYatContact);
        //for (var i = 0; i < txtCYatContact.length; i++) {
        //    var CYatContact = {};
        //    CYatContact.CYAt_Contact = txtCYatContact[i];
        //    cboCYAtContact.push(CYatContact);
        //}
        //Add RETURN AT / CONTACT
        //var txtReturnAtContact = $("#txtReturnAtContact").val();
        var ReturnAtContact = {};
        ReturnAtContact.ReturnAt_Contact = $("#txtReturnAtContact").val();
        cboReturnAtContact.push(ReturnAtContact);
        //for (var i = 0; i < txtReturnAtContact.length; i++) {
        //    var ReturnAtContact = {};
        //    ReturnAtContact.ReturnAt_Contact = txtReturnAtContact[i];
        //    cboReturnAtContact.push(ReturnAtContact);
        //}
        //Add LOADING AT
        var txtLoadingAt = $("#txtLoadingAt").val();
        for (var i = 0; i < txtLoadingAt.length; i++) {
            var LoadingAt = {};
            LoadingAt.LoadingAt = txtLoadingAt[i];
            cboLoadingAt.push(LoadingAt);
        }
        var objHeader = {};
        objHeader.CreateBy = $("#UserID").attr("data-id");
        objHeader.JOBSERIES = $("#cboImportType").val();
        objHeader.BOOKINGDATE = $("#txtBookingDate").val();
        objHeader.ORIGIN = $("#cboOrigin").val();
        objHeader.DESTINATION = $("#cboDestination").val();
        objHeader.SALEEMPLOYEE = $("#cboSalePerson1").val();
        objHeader.IMPORTTYPE = $("#cboSelectSeries").val();//$("#cboSelectSeries").val();
        objHeader.SERVICETYPE = $("#cboServiceType").val();
        objHeader.BOOKINGNO = $("#txtBookingNo").val();
        objHeader.FREIGHT = $('input[name="REPAID/COLLECTION"]:checked').val(); 
        objHeader.FEEDER = $("#txtFeeder").val();
        objHeader.FEEDERVOY = $("#txtFeederVoy").val();
        objHeader.VESSEL = $("#txtVessel").val();
        objHeader.VESSELVOY = $("#txtVesselVoy").val();
        objHeader.GOODSDESCRIPTION = $("#txtGoodsDescription").val();
        objHeader.TOTALPACKAGE = $("#txtTotalPackage").val();
        objHeader.NETWEIGHT = $("#txtNetWeight").val();
        objHeader.GROSSWEIGHT = $("#txtGrossWeight").val();
        objHeader.LOADINGDATE = $("#txtLoadingDate").val();
        objHeader.CROSSBORDERDATE = $("#txtCrossBorderDate").val();
        objHeader.ETAREQUIREMENT = $("#txtEtaRequirement").val();
        objHeader.REMARKLOLOYARD = $("#txtLoloYardRemark").val();
        objHeader.LOLOYARD_UNLOADING = $('input[name="typeloloyard/unloading"]:checked').val();
        objHeader.CBM = $("#txtCBMWeight").val();
        objHeader.CYDATE = $("#txtCYDate").val();
        objHeader.LCL_FCL_CY_CFS = $('input[name="lcl/fcl"]:checked').val();
        objHeader.RETURNDATE = $("#txtReturnDate").val();
        objHeader.LASTRETURNDATE = $("#txtLastReturnDate").val();
        objHeader.RETURNBEFORE = $("#txtreturnBefore").val();
        objHeader.SPECIALREQUEST = $("#specialRequest").val();
        objHeader.DOCUMENTREQUIREMENT = $("#documentRequirement").val();
        objHeader.ETDREQUIREMENT = $("#txtEtdRequirement").val();
        objHeader.CLOSINGDATE = $("#txtClosingDate").val();
        objHeader.CLOSINGTIME = $("#txtclosingBefore").val();
        objHeader.DG = $("#cboDamgeGoods").val();
        //Air
        objHeader.MAWB = $("#txtMAWBAir").val();
        objHeader.LoadingDateAir = $("#txtLoadingDateAir").val();
        objHeader.CuffOfDateAir = $("#txtCutOffTimeAir").val();
        objHeader.CuffOfTimeAir = $("#txtCutOffTimeAir").val();
        objHeader.LoadingPlaceAir = $("#txtLoadingPlaceAir").val();
        objHeader.Warehouse = $("#txtWarehouseAir").val();
        objHeader.Contact = $("#txtContactAir").val();
        objHeader.Address = $("#txtAddressAir").val();
        objHeader.Tel = $("#txtTelAir").val();
        //End Air
        bookingSheetPost.HeaderObj = objHeader;
        bookingSheetPost.ListCOLoader = cboCOLOADER;
        bookingSheetPost.ListCustomer = cboCustomer;
        bookingSheetPost.ListShipper = cboSHIPPERList;
        bookingSheetPost.ListConsignee = cboConsigneeList;
        bookingSheetPost.ListShippingLine = cboConsigneeList;
        bookingSheetPost.ListDestAgent = cboConsigneeList;
        bookingSheetPost.ListCommodities = invoiceList;
        bookingSheetPost.ListOverseaTransportSeaAndAir = cboOverseaTruckerList;
        bookingSheetPost.ListPortOfReceiptSeaAndAir = cboPORTOFRECEIPT;
        bookingSheetPost.ListPortOfDischargeSeaAndAir = cboPORTOFDISCHARGE;
        bookingSheetPost.ListPlaceOfLoadingSeaAndAir = placeLoadingList;
        bookingSheetPost.ListPlaceOfDeliverySeaAndAir = placeDeliveryList;
        bookingSheetPost.ListThaiForwarderSeaAndAir = cboThaiForwarderList;
        bookingSheetPost.ListContainerTypeSeaAndAir = volumeList;
        bookingSheetPost.ListOverSeaForwarderSeaAndAir = cboOverForwarderList;
        bookingSheetPost.ListThaiBorderSeaAndAir = cboThaiBorderList;
        bookingSheetPost.ListTruckTypeSeaAndAir = truckTypeList;
        bookingSheetPost.ListCYAt_ContactSeaAndAir = cboCYAtContact;
        bookingSheetPost.ListReturnAt_ContactSeaAndAir = cboReturnAtContact;
        bookingSheetPost.ListLoadingAtSeaAndAir = cboLoadingAt;
        bookingSheetPost.ListDestAgent = cboDESTAGENT;
        bookingSheetPost.ListShippingLine = cboSHIPPINGLINE;
        //Air
        bookingSheetPost.FlightsInformations = listInfoAir;
        //End Air
        $.ajax({
            url: url,
            type: "POST",
            data: { addBookingSheetSeaAndAir: bookingSheetPost },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                window.location.href = urlPDF + "?docEntry=" + data.docEntry;
            },
            error: function (erro) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html(erro.responseText);
            }
        });
    }
}

function onConfirmTransactionUpdate(url, urlPDF) {
    if (confirm("Please Confirm Transaction Before Add Make sure it correct !")) {
        var bookingSheetPost = {};
        var invoiceList = [];
        var placeLoadingList = [];
        var placeDeliveryList = [];
        var volumeList = [];
        var truckTypeList = [];
        var cboThaiBorderList = [];
        var cboOverseaTruckerList = [];
        var cboOverForwarderList = [];
        var cboSalesQuotationList = [];
        var cboThaiForwarderList = [];
        var cboSHIPPERList = [];
        var cboConsigneeList = [];
        var cboCOLOADER = [];
        var cboCustomer = [];
        var cboSHIPPINGLINE = [];
        var cboDESTAGENT = [];
        var cboPORTOFDISCHARGE = [];
        var cboPORTOFRECEIPT = [];
        var cboCYAtContact = [];
        var cboReturnAtContact = [];
        var cboLoadingAt = [];
        //Add COLOADER
        var txtColoader = $("#txtColoader").val();
        for (var i = 0; i < txtColoader.length; i++) {
            var Coloader = {};
            Coloader.COLOADER = txtColoader[i];
            cboCOLOADER.push(Coloader);
        }
        //Add Customer Name
        var txtCustomerName = $("#txtCustomerName").val();
        for (var i = 0; i < txtCustomerName.length; i++) {
            var CustomerName = {};
            CustomerName.CUSTOMERCODE = txtCustomerName[i];
            cboCustomer.push(CustomerName);
        }
        //Add cboSHIPPERList
        var txtShipper = $("#txtShipper").val();
        for (var i = 0; i < txtShipper.length; i++) {
            var cboShippers = {};
            cboShippers.SHIPPER = txtShipper[i];
            cboSHIPPERList.push(cboShippers);
        }
        //Add cboConsigneeList
        var txtConsignee = $("#txtConsignee").val();
        for (var i = 0; i < txtConsignee.length; i++) {
            var cboConsignees = {};
            cboConsignees.CONSIGNEE = txtConsignee[i];
            cboConsigneeList.push(cboConsignees);
        }
        //Add ShippingLine
        var txtShippingLine = $("#txtShippingLine").val();
        for (var i = 0; i < txtShippingLine.length; i++) {
            var ShippingLine = {};
            ShippingLine.SHIPPINGLINE = txtShippingLine[i];
            cboSHIPPINGLINE.push(ShippingLine);
        }
        //DEST. AGENT
        var txtdestAgent = $("#txtdestAgent").val();
        for (var i = 0; i < txtdestAgent.length; i++) {
            var destAgent = {};
            destAgent.DESTAGENT = txtdestAgent[i];
            cboDESTAGENT.push(destAgent);
        }
        //Add Commodities
        for (var i = 0; i < listInvoice.length; i++) {
            var invoice = {};
            invoice.Invoice = listInvoice[i].InvoiceNumber;
            invoiceList.push(invoice);
        }
        //Add OverseaTrucker
        var cboOverseaTrucker = $("#cboMultiOverseaTrucker").val();
        for (var i = 0; i < cboOverseaTrucker.length; i++) {
            var cboOverseaTruckers = {};
            cboOverseaTruckers.OVEARSEATRANSPORT = cboOverseaTrucker[i];
            cboOverseaTruckerList.push(cboOverseaTruckers);
        }
        //Add PORT OF RECEIPT
        var cboMultiplePlaceOfReceipt = $("#cboMultiplePlaceOfReceipt").val();
        for (var i = 0; i < cboMultiplePlaceOfReceipt.length; i++) {
            var PORTOFRECEIPT = {};
            PORTOFRECEIPT.PORTOFRECEIPT = cboMultiplePlaceOfReceipt[i];
            cboPORTOFRECEIPT.push(PORTOFRECEIPT);
        }
        //Add PORT OF DISCHARGE
        var cboMultiPortOfDischarge = $("#cboMultiPortOfDischarge").val();
        for (var i = 0; i < cboMultiPortOfDischarge.length; i++) {
            var PortOfDischarge = {};
            PortOfDischarge.PORTOFDISCHARGE = cboMultiPortOfDischarge[i];
            cboPORTOFDISCHARGE.push(PortOfDischarge);
        }
        //Add Place Of Loading
        var cboPlaceOfLoading = $("#cboMultiPlaceOfLoading").val();
        for (var i = 0; i < cboPlaceOfLoading.length; i++) {
            var placeOfLoadings = {};
            placeOfLoadings.PLACEOFLOADING = cboPlaceOfLoading[i];
            placeLoadingList.push(placeOfLoadings);
        }
        //Add Place Of Delivery
        var cboPlaceOfDelivery = $("#cboMultiPlaceOfDelivery").val();
        for (var i = 0; i < cboPlaceOfDelivery.length; i++) {
            var placeOfDeliveries = {};
            placeOfDeliveries.PLACEOFDELIVERY = cboPlaceOfDelivery[i];
            placeDeliveryList.push(placeOfDeliveries);
        }
        //Add cboThaiForwarderList
        var cboThaiForwarder = $("#cboThaiForwarder").val();
        for (var i = 0; i < cboThaiForwarder.length; i++) {
            var cboThaiForwarders = {};
            cboThaiForwarders.THAIFORWARDER = cboThaiForwarder[i];
            cboThaiForwarderList.push(cboThaiForwarders);
        }
        //Add Volumne
        if (listVolumn.length != 0) {
            for (var i = 0; i < listVolumn.length; i++) {
                var volume = {};
                volume.Qty = $("#txtVolumQty" + listVolumn[i].ID).val();//listVolumn[i].VolumeQty;
                volume.ContainerType = listVolumn[i].VolumeName;
                volume.Weight = $("#txtVolumGrossWeight" + listVolumn[i].ID).val();
                volume.ListInvoice = (listVolumn[i].invoiceList.length == undefined) ? '' : listVolumn[i].invoiceList.join();
                volumeList.push(volume);
            }
        }
        //Add OverseaForwarder
        var cboOverseaForwarder = $("#cboOverseaForwarder").val();
        for (var i = 0; i < cboOverseaForwarder.length; i++) {
            var cboOverseaForwarders = {};
            cboOverseaForwarders.OVERSEAFORWARDER = cboOverseaForwarder[i];//OVERSEAFORWARDERCODE
            cboOverForwarderList.push(cboOverseaForwarders);
        }
        //Add Thai Border
        var cboThaiBorder = $("#cboThaiBorder").val();
        for (var i = 0; i < cboThaiBorder.length; i++) {
            var cboThaiBorders = {};
            cboThaiBorders.THAIBORDER = cboThaiBorder[i];
            cboThaiBorderList.push(cboThaiBorders);
        }
        //Add TruckType
        if (listTruckType.length != 0) {
            for (var i = 0; i < listTruckType.length; i++) {
                var truckType = {};
                truckType.Qty = $("#txtTruckTypeQty" + listTruckType[i].ID).val();//listTruckType[i].TruckTypeQty;
                truckType.ContainerType = listTruckType[i].TruckTypeName;
                truckType.Weight = $("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val();
                truckType.ListInvoice = (listTruckType[i].invoiceList.length == undefined) ? '' : listTruckType[i].invoiceList.join();
                truckTypeList.push(truckType);
            }
        }
        //Add CY AT / CONTACT
        //var txtCYatContact = $("#txtCYatContact").val();
        var CYatContact= {};
        CYatContact.CYAt_Contact = $("#txtCYatContact").val();
        cboCYAtContact.push(CYatContact);
        //for (var i = 0; i < txtCYatContact.length; i++) {
        //    var CYatContact = {};
        //    CYatContact.CYAt_Contact = txtCYatContact[i];
        //    cboCYAtContact.push(CYatContact);
        //}
        //Add RETURN AT / CONTACT
        //var txtReturnAtContact = $("#txtReturnAtContact").val();
        var ReturnAtContact = {};
        ReturnAtContact.ReturnAt_Contact = $("#txtReturnAtContact").val();
        cboReturnAtContact.push(ReturnAtContact);
        //for (var i = 0; i < txtReturnAtContact.length; i++) {
        //    var ReturnAtContact = {};
        //    ReturnAtContact.ReturnAt_Contact = txtReturnAtContact[i];
        //    cboReturnAtContact.push(ReturnAtContact);
        //}
        //Add LOADING AT
        var txtLoadingAt = $("#txtLoadingAt").val();
        for (var i = 0; i < txtLoadingAt.length; i++) {
            var LoadingAt = {};
            LoadingAt.LoadingAt = txtLoadingAt[i];
            cboLoadingAt.push(LoadingAt);
        }
        var objHeader = {};
        objHeader.CreateBy = $("#UserID").attr("data-id");
        objHeader.JOBSERIES = $("#cboImportType").val();
        objHeader.BOOKINGDATE = $("#txtBookingDate").val();
        objHeader.ORIGIN = $("#cboOrigin").val();
        objHeader.DESTINATION = $("#cboDestination").val();
        objHeader.SALEEMPLOYEE = $("#cboSalePerson1").val();
        objHeader.IMPORTTYPE = $("#cboSelectSeries").val();//$("#cboSelectSeries").val();
        objHeader.SERVICETYPE = $("#cboServiceType").val();
        objHeader.BOOKINGNO = $("#txtBookingNo").val();
        objHeader.FREIGHT = $('input[name="REPAID/COLLECTION"]:checked').val();
        objHeader.FEEDER = $("#txtFeeder").val();
        objHeader.FEEDERVOY = $("#txtFeederVoy").val();
        objHeader.VESSEL = $("#txtVessel").val();
        objHeader.VESSELVOY = $("#txtVesselVoy").val();
        objHeader.GOODSDESCRIPTION = $("#txtGoodsDescription").val();
        objHeader.TOTALPACKAGE = $("#txtTotalPackage").val();
        objHeader.NETWEIGHT = $("#txtNetWeight").val();
        objHeader.GROSSWEIGHT = $("#txtGrossWeight").val();
        objHeader.LOADINGDATE = $("#txtLoadingDate").val();
        objHeader.CROSSBORDERDATE = $("#txtCrossBorderDate").val();
        objHeader.ETAREQUIREMENT = $("#txtEtaRequirement").val();
        objHeader.REMARKLOLOYARD = $("#txtLoloYardRemark").val();
        objHeader.LOLOYARD_UNLOADING = $('input[name="typeloloyard/unloading"]:checked').val();
        objHeader.CBM = $("#txtCBMWeight").val();
        objHeader.CYDATE = $("#txtCYDate").val();
        objHeader.LCL_FCL_CY_CFS = $('input[name="lcl/fcl"]:checked').val();
        objHeader.RETURNDATE = $("#txtReturnDate").val();
        objHeader.LASTRETURNDATE = $("#txtLastReturnDate").val();
        objHeader.RETURNBEFORE = $("#txtreturnBefore").val();
        objHeader.SPECIALREQUEST = $("#specialRequest").val();
        objHeader.DOCUMENTREQUIREMENT = $("#documentRequirement").val();
        objHeader.ETDREQUIREMENT = $("#txtEtdRequirement").val();
        objHeader.CLOSINGDATE = $("#txtClosingDate").val();
        objHeader.CLOSINGTIME = $("#txtclosingBefore").val();
        objHeader.DG = $("#cboDamgeGoods").val();
        //Air
        objHeader.MAWB = $("#txtMAWBAir").val();
        objHeader.LoadingDateAir = $("#txtLoadingDateAir").val();
        objHeader.CuffOfDateAir = $("#txtCutOffTimeAir").val();
        objHeader.CuffOfTimeAir = $("#txtCutOffTimeAir").val();
        objHeader.LoadingPlaceAir = $("#txtLoadingPlaceAir").val();
        objHeader.Warehouse = $("#txtWarehouseAir").val();
        objHeader.Contact = $("#txtContactAir").val();
        objHeader.Address = $("#txtAddressAir").val();
        objHeader.Tel = $("#txtTelAir").val();
        //End Air
        bookingSheetPost.HeaderObj = objHeader;
        bookingSheetPost.ListCOLoader = cboCOLOADER;
        bookingSheetPost.ListCustomer = cboCustomer;
        bookingSheetPost.ListShipper = cboSHIPPERList;
        bookingSheetPost.ListConsignee = cboConsigneeList;
        bookingSheetPost.ListShippingLine = cboConsigneeList;
        bookingSheetPost.ListDestAgent = cboConsigneeList;
        bookingSheetPost.ListCommodities = invoiceList;
        bookingSheetPost.ListOverseaTransportSeaAndAir = cboOverseaTruckerList;
        bookingSheetPost.ListPortOfReceiptSeaAndAir = cboPORTOFRECEIPT;
        bookingSheetPost.ListPortOfDischargeSeaAndAir = cboPORTOFDISCHARGE;
        bookingSheetPost.ListPlaceOfLoadingSeaAndAir = placeLoadingList;
        bookingSheetPost.ListPlaceOfDeliverySeaAndAir = placeDeliveryList;
        bookingSheetPost.ListThaiForwarderSeaAndAir = cboThaiForwarderList;
        bookingSheetPost.ListContainerTypeSeaAndAir = volumeList;
        bookingSheetPost.ListOverSeaForwarderSeaAndAir = cboOverForwarderList;
        bookingSheetPost.ListThaiBorderSeaAndAir = cboThaiBorderList;
        bookingSheetPost.ListTruckTypeSeaAndAir = truckTypeList;
        bookingSheetPost.ListCYAt_ContactSeaAndAir = cboCYAtContact;
        bookingSheetPost.ListReturnAt_ContactSeaAndAir = cboReturnAtContact;
        bookingSheetPost.ListLoadingAtSeaAndAir = cboLoadingAt;
        bookingSheetPost.ListDestAgent = cboDESTAGENT;
        bookingSheetPost.ListShippingLine = cboSHIPPINGLINE;
        //Air
        bookingSheetPost.FlightsInformations = listInfoAir;
        //End Air
        $.ajax({
            url: url,
            type: "PUT",
            data: { addBookingSheetSeaAndAir: bookingSheetPost, docEntry: docEntry },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                window.location.href = urlPDF + "?docEntry=" + data.docEntry;
            },
            error: function (erro) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html(erro.responseText);
            }
        });
    }
}
