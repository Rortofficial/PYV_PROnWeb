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
        //if (listInvoice.length == 0) {
        //    alert("Please Enter Invoice Before Add");
        //    return;
        //}
        

        //Add Commodities
        for (var i = 0; i < listInvoice.length; i++) {
            var invoice = {};
            invoice.INVOICE = listInvoice[i].InvoiceNumber;
            invoiceList.push(invoice);
        }
        //Add Volumne
        if (listVolumn.length != 0) {
            for (var i = 0; i < listVolumn.length; i++) {
                var volume = {};
                volume.QTY = $("#txtVolumQty" + listVolumn[i].ID).val();//listVolumn[i].VolumeQty;
                volume.VOLUMECODE = listVolumn[i].VolumeName;
                volume.GROSSWEIGHT = $("#txtVolumGrossWeight" + listVolumn[i].ID).val();
                volume.InvoiceList = (listVolumn[i].invoiceList.length == undefined) ? '' : listVolumn[i].invoiceList.join();
                volumeList.push(volume);
            }
        }
        //Add TruckType
        if (listTruckType.length != 0) {
            for (var i = 0; i < listTruckType.length; i++) {
                var truckType = {};
                truckType.QTY = $("#txtTruckTypeQty" + listTruckType[i].ID).val();//listTruckType[i].TruckTypeQty;
                truckType.TRUCKTYPE = listTruckType[i].TruckTypeName;
                truckType.GROSSWEIGHT = $("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val();
                truckType.InvoiceList = (listTruckType[i].invoiceList.length == undefined) ? '' : listTruckType[i].invoiceList.join();
                truckTypeList.push(truckType);
            }
        }
              

        if ($("#txtLoadingDate").val() == "") {
            alert("Please Enter LOADING DATE");
            return;
        }
        //PlaceOfDelivery
        var cboPlaceOfDelivery = $("#cboMultiPlaceOfDelivery").val();
        for (var i = 0; i < cboPlaceOfDelivery.length; i++) {
            var placeOfDeliveries = {};
            placeOfDeliveries.PLACEDELIVERY = cboPlaceOfDelivery[i];
            placeOfDeliveries.District = "";
            if ($("#cboMultiDistrictPlaceOfDelivery").val().length !== 0) {
                for (var z = 0; z < $("#cboMultiDistrictPlaceOfDelivery").val().length; z++) {
                    if (z == ($("#cboMultiDistrictPlaceOfDelivery").val().length - 1)) {
                        placeOfDeliveries.District += $("#cboMultiDistrictPlaceOfDelivery").val()[z];
                    } else {
                        placeOfDeliveries.District += $("#cboMultiDistrictPlaceOfDelivery").val()[z] + ",";
                    }
                }
            }
            placeDeliveryList.push(placeOfDeliveries);
        }
        //Add Place Of Loading
        var cboPlaceOfLoading = $("#cboMultiPlaceOfLoading").val();
        for (var i = 0; i < cboPlaceOfLoading.length; i++) {
            var placeOfLoadings = {};
            placeOfLoadings.PLACELOADING = cboPlaceOfLoading[i];
            placeOfLoadings.District = "";
            if ($("#cboMultiDistrictPlaceOfLoading").val().length !== 0) {
                for (var z = 0; z < $("#cboMultiDistrictPlaceOfLoading").val().length; z++) {
                    if (z == ($("#cboMultiDistrictPlaceOfLoading").val().length - 1)) {
                        placeOfLoadings.District += $("#cboMultiDistrictPlaceOfLoading").val()[z];
                    } else {
                        placeOfLoadings.District += $("#cboMultiDistrictPlaceOfLoading").val()[z] + ",";
                    }
                }
            }
            placeLoadingList.push(placeOfLoadings);
        }
        //Add Thai Border
        var cboThaiBorder = $("#cboThaiBorder").val();
        for (var i = 0; i < cboThaiBorder.length; i++) {
            var cboThaiBorders = {};
            cboThaiBorders.ThaiBorder = cboThaiBorder[i];
            cboThaiBorderList.push(cboThaiBorders);
        }
        //Add OverseaTrucker
        var cboOverseaTrucker = $("#cboMultiOverseaTrucker").val();
        for (var i = 0; i < cboOverseaTrucker.length; i++) {
            var cboOverseaTruckers = {};
            cboOverseaTruckers.OVERSEATRUCKERCODE = cboOverseaTrucker[i];
            cboOverseaTruckerList.push(cboOverseaTruckers);
        }
        //Add OverseaForwarder
        var cboOverseaForwarder = $("#cboOverseaForwarder").val();
        for (var i = 0; i < cboOverseaForwarder.length; i++) {
            var cboOverseaForwarders = {};
            cboOverseaForwarders.OVERSEAFORWARDERCODE = cboOverseaForwarder[i];//OVERSEAFORWARDERCODE
            cboOverForwarderList.push(cboOverseaForwarders);
        }
        //Add cboThaiForwarderList
        var cboThaiForwarder = $("#cboThaiForwarder").val();
        for (var i = 0; i < cboThaiForwarder.length; i++) {
            var cboThaiForwarders = {};
            cboThaiForwarders.THAIFORWARDER = cboThaiForwarder[i];
            cboThaiForwarderList.push(cboThaiForwarders);
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

        if ($("#cboSelectSeries").val() !== "EWST") {
           
            if ($("#txtCrossBorderDate").val() == "") {
                alert("Please Enter CROSS BORDER DATE");
                return;
            }
            if ($("#txtEtaRequirement").val() == "") {
                alert("Please Enter ETA REQUIREMENT");
                return;
            }
            if ($("#txtLoloYardRemark").val() === '') {
                alert("Please Enter Lolo/Yard first");
                return;
            }

            if (listVolumn.length == 0 && listTruckType.length == 0) {
                alert("Please Enter CONTAINER TYPE or Truck Type both of them should has One");
                return;
            } 
            //Add Place Of Delivery
            if (cboPlaceOfDelivery.length == 0) {
                alert("Please Enter PLACE OF DELIVERY");
                return;
            }
            //Add Place Of Loading
            if (cboPlaceOfLoading.length == 0) {
                alert("Please Enter PLACE OF LOADING");
                return;
            }
            //Add Thai Border
            
            if (cboThaiBorder.length == 0) {
                alert("Please Enter THAI BORDER");
                return;
            }
            //Add OverseaTrucker
            if (cboOverseaTrucker.length == 0) {
                alert("Please Enter Oversea Transport");
                return;
            }

            //Add OverseaForwarder
            if (cboOverseaForwarder.length == 0) {
                alert("Please Enter OVERSEA FORWARDER");
                return;   
            }
            //Add cboThaiForwarderList
            if (cboThaiForwarder.length == 0) {
                alert("Please Enter THAI FORWARDER");
                return;
            }
            //Add cboSHIPPERList
            if (txtShipper.length == 0) {
                alert("Please Enter Shipper");
                return;
            }
            //Add cboConsigneeList
            if (txtConsignee.length == 0) {
                alert("Please Enter Consignee");
                return;
            }
        }      
        
        
        
        //Add cboSalesQuotationList
        var txtSaleQuotations = $("#txtSaleQuotations").val();
        for (var i = 0; i < txtSaleQuotations.length; i++) {
            var cboSalesQuotations = {};
            cboSalesQuotations.DOCENTRY = txtSaleQuotations[i];
            cboSalesQuotationList.push(cboSalesQuotations);
        }
       
        bookingSheetPost.SaleQuotation = cboSalesQuotationList;
        bookingSheetPost.CreateUser = $("#UserID").attr("data-id");
        bookingSheetPost.EWSereis = $("#cboSelectSeries").val();
        bookingSheetPost.Shipper = cboSHIPPERList;
        bookingSheetPost.CO = $("#txtSaleQuotations option:selected").attr("data-cardcode");
        bookingSheetPost.Consignee = cboConsigneeList;
        bookingSheetPost.SaleEmployee = $("#cboSalePerson1").val();
        bookingSheetPost.ImportType = $("#cboImportType").val();//$('input[name="importType"]:checked').val();
        bookingSheetPost.BookingDate = $("#txtBookingDate").val();
        //bookingSheetPost.Route = '-1';//$("#cboRoute").val();
        bookingSheetPost.Origin = $("#cboOrigin").val();
        bookingSheetPost.Destination = $("#cboDestination").val();
        bookingSheetPost.GoodDescription = $("#txtGoodsDescription").val();
        bookingSheetPost.TotalPackage = $("#txtTotalPackage").val();
        bookingSheetPost.NetWeight = $("#txtNetWeight").val();
        bookingSheetPost.GrossWeight = $("#txtGrossWeight").val();
        bookingSheetPost.LoadingDate = $("#txtLoadingDate").val();
        bookingSheetPost.CorssBorderDate = $("#txtCrossBorderDate").val();
        bookingSheetPost.ETARequirement = $("#txtEtaRequirement").val();
        //bookingSheetPost.ThaiTrucker = $("#cboThaiTrucker").val()[0];
        bookingSheetPost.OverseaTrucker = cboOverseaTruckerList;
        bookingSheetPost.LoloYardOrUnloading = ($('input[name="typeloloyard/unloading"]:checked').val() == undefined) ? "" : $('input[name="typeloloyard/unloading"]:checked').val();
        bookingSheetPost.ThaiForwarder = cboThaiForwarderList;
        bookingSheetPost.OverseaForwarder = cboOverForwarderList;
        bookingSheetPost.LCLOrFCL = ($('input[name="lcl/fcl"]:checked').val() == undefined) ? "" : $('input[name="lcl/fcl"]:checked').val();
        bookingSheetPost.CYOrCFS = ($('input[name="cy/cfs"]:checked').val() == undefined) ? "" : $('input[name="cy/cfs"]:checked').val();
        bookingSheetPost.LOLOYARDRemark = $("#txtLoloYardRemark").val();
        bookingSheetPost.SpeacialRequest = $("#specialRequest").val();
        bookingSheetPost.Remarks = $("#documentRequirement").val();
        bookingSheetPost.ServiceType = $("#cboServiceType").val();
        bookingSheetPost.TruckType = truckTypeList;
        bookingSheetPost.Commodities = invoiceList;
        bookingSheetPost.PlaceOfLoadings = placeLoadingList;
        bookingSheetPost.PlaceOfDeliveries = placeDeliveryList;
        bookingSheetPost.Volumes = volumeList;
        bookingSheetPost.ThaiBorders = cboThaiBorderList;
        bookingSheetPost.DG = $("#cboDamageGood").val();
        if ($('input[name="typeloloyard/unloading"]:checked').val() == "3") {
            bookingSheetPost.OtherRemark = $("#txtLoloOtherRemark").val();
        }
        $.ajax({
            url: url,
            type: "POST",
            data: { postBookingSheetRequest: bookingSheetPost },
            dataType: "JSON",
            beforeSend: function () {
                $('#Modalloder').modal('show');
            },
            success: function (data) {
                $('#Modalloder').modal('hide');
                $('#ModalSuccess').modal('show');
                $('#ErrMsg').html("Success");
                window.open(urlPDF + "?docEntry=" + data.docEntry);
                window.location.href = window.location.href;//urlPDF + "?docEntry=" + data.docEntry;
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
        //if (listInvoice.length == 0) {
        //    alert("Please Enter Invoice Before Add");
        //    return;
        //}
        
        //Add Commodities
        for (var i = 0; i < listInvoice.length; i++) {
            var invoice = {};
            invoice.INVOICE = listInvoice[i].InvoiceNumber;
            invoiceList.push(invoice);
        }
        //Add Volumne
        if (listVolumn.length != 0) {
            for (var i = 0; i < listVolumn.length; i++) {
                var volume = {};
                volume.QTY = $("#txtVolumQty" + listVolumn[i].ID).val();//listVolumn[i].VolumeQty;
                volume.VOLUMECODE = listVolumn[i].VolumeName;
                volume.GROSSWEIGHT = $("#txtVolumGrossWeight" + listVolumn[i].ID).val();
                //volume.InvoiceList = volume[i].invoiceList.join();
                volume.InvoiceList = (listVolumn[i].invoiceList.length == 0 || listVolumn[i].invoiceList.length == undefined) ? '' : listVolumn[i].invoiceList.join(",");
                volumeList.push(volume);
            }
        }

        //Add TruckType
        if (listTruckType.length != 0) {
            for (var i = 0; i < listTruckType.length; i++) {
                var truckType = {};
                truckType.QTY = $("#txtTruckTypeQty" + listTruckType[i].ID).val();//listTruckType[i].TruckTypeQty;
                truckType.TRUCKTYPE = listTruckType[i].TruckTypeName;
                truckType.GROSSWEIGHT = $("#txtTruckTypeGrossWeight" + listTruckType[i].ID).val();
                //truckType.InvoiceList = listTruckType[i].invoiceList.join();
                truckType.InvoiceList = (listTruckType[i].invoiceList.length == 0 || listTruckType[i].invoiceList.length == undefined) ? '' : listTruckType[i].invoiceList.join(",");
                truckTypeList.push(truckType);
            }
        }   

        if ($("#txtLoadingDate").val() == "") {
            alert("Please Enter LOADING DATE");
            return;
        }
        //Add Place Of Delivery
        var cboPlaceOfDelivery = $("#cboMultiPlaceOfDelivery").val();
        for (var i = 0; i < cboPlaceOfDelivery.length; i++) {
            var placeOfDeliveries = {};
            placeOfDeliveries.PLACEDELIVERY = cboPlaceOfDelivery[i];
            placeOfDeliveries.District = "";
            if ($("#cboMultiDistrictPlaceOfDelivery").val().length !== 0) {
                for (var z = 0; z < $("#cboMultiDistrictPlaceOfDelivery").val().length; z++) {
                    if (z == ($("#cboMultiDistrictPlaceOfDelivery").val().length - 1)) {
                        placeOfDeliveries.District += $("#cboMultiDistrictPlaceOfDelivery").val()[z];
                    } else {
                        placeOfDeliveries.District += $("#cboMultiDistrictPlaceOfDelivery").val()[z] + ",";
                    }
                }
            }
            placeDeliveryList.push(placeOfDeliveries);
        }
        //Add Place Of Loading
        var cboPlaceOfLoading = $("#cboMultiPlaceOfLoading").val();
        for (var i = 0; i < cboPlaceOfLoading.length; i++) {
            var placeOfLoadings = {};
            placeOfLoadings.PLACELOADING = cboPlaceOfLoading[i];
            placeOfLoadings.District = "";
            if ($("#cboMultiDistrictPlaceOfLoading").val().length !== 0) {
                for (var z = 0; z < $("#cboMultiDistrictPlaceOfLoading").val().length; z++) {
                    if (z == ($("#cboMultiDistrictPlaceOfLoading").val().length - 1)) {
                        placeOfLoadings.District += $("#cboMultiDistrictPlaceOfLoading").val()[z];
                    } else {
                        placeOfLoadings.District += $("#cboMultiDistrictPlaceOfLoading").val()[z] + ",";
                    }
                }
            }
            placeLoadingList.push(placeOfLoadings);
        }
        //Add Thai Border
        var cboThaiBorder = $("#cboThaiBorder").val();
        for (var i = 0; i < cboThaiBorder.length; i++) {
            var cboThaiBorders = {};
            cboThaiBorders.ThaiBorder = cboThaiBorder[i];
            cboThaiBorderList.push(cboThaiBorders);
        }
        //Add OverseaTrucker
        var cboOverseaTrucker = $("#cboMultiOverseaTrucker").val();
        for (var i = 0; i < cboOverseaTrucker.length; i++) {
            var cboOverseaTruckers = {};
            cboOverseaTruckers.OVERSEATRUCKERCODE = cboOverseaTrucker[i];
            cboOverseaTruckerList.push(cboOverseaTruckers);
        }
        //Add OverseaForwarder
        var cboOverseaForwarder = $("#cboOverseaForwarder").val();
        for (var i = 0; i < cboOverseaForwarder.length; i++) {
            var cboOverseaForwarders = {};
            cboOverseaForwarders.OVERSEAFORWARDERCODE = cboOverseaForwarder[i]; //OVERSEATRUCKERCODE
            cboOverForwarderList.push(cboOverseaForwarders);
        }
        //Add cboThaiForwarderList
        var cboThaiForwarder = $("#cboThaiForwarder").val();
        for (var i = 0; i < cboThaiForwarder.length; i++) {
            var cboThaiForwarders = {};
            cboThaiForwarders.THAIFORWARDER = cboThaiForwarder[i];
            cboThaiForwarderList.push(cboThaiForwarders);
        }

        if ($("#cboSelectSeries").val() !== "EWST") {
            if ($("#txtCrossBorderDate").val() == "") {
                alert("Please Enter CROSS BORDER DATE");
                return;
            }
            if ($("#txtEtaRequirement").val() == "") {
                alert("Please Enter ETA REQUIREMENT");
                return;
            }
            if ($("#txtLoloYardRemark").val() === '') {
                alert("Please Enter Lolo/Yard first");
                return;
            }
            if (listVolumn.length == 0 && listTruckType.length == 0) {
                alert("Please Enter CONTAINER TYPE or Truck Type both of them should has One");
                return;
            }
            //Add Place Of Delivery
            if (cboPlaceOfDelivery.length == 0) {
                alert("Please Enter PLACE OF DELIVERY");
                return;
            }
            //Add Place Of Loading
            if (cboPlaceOfLoading.length == 0) {
                alert("Please Enter PLACE OF LOADING");
                return;
            } 
            //Add Thai Border            
            if (cboThaiBorder.length == 0) {
                alert("Please Enter THAI BORDER");
                return;
            } 
            //Add OverseaTrucker            
            if (cboOverseaTrucker.length == 0) {
                alert("Please Enter Oversea Transport");
                return;
            }
            //Add OverseaForwarder
            if (cboOverseaForwarder.length == 0) {
                alert("Please Enter Oversea Forwarder");
                return;
            }
            //Add cboThaiForwarderList
            if (cboThaiForwarder.length == 0) {
                alert("Please Enter THAI FORWARDER");
                return;
            }
        }
        //Add cboSalesQuotationList
        var txtSaleQuotations = $("#txtSaleQuotations").val();
        for (var i = 0; i < txtSaleQuotations.length; i++) {
            var cboSalesQuotations = {};
            cboSalesQuotations.DOCENTRY = txtSaleQuotations[i];
            cboSalesQuotationList.push(cboSalesQuotations);
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
        bookingSheetPost.SaleQuotation = cboSalesQuotationList;
        bookingSheetPost.CreateUser = $("#UserID").attr("data-id");
        bookingSheetPost.EWSereis = $("#cboSelectSeries").val();
        bookingSheetPost.Shipper = cboSHIPPERList;
        bookingSheetPost.CO = $("#txtSaleQuotations option:selected").attr("data-cardcode");
        bookingSheetPost.Consignee = cboConsigneeList;
        bookingSheetPost.SaleEmployee = $("#cboSalePerson1").val();
        bookingSheetPost.ImportType = $("#cboImportType").val();//$('input[name="importType"]:checked').val();
        bookingSheetPost.BookingDate = $("#txtBookingDate").val();
        //bookingSheetPost.Route = '-1';//$("#cboRoute").val();
        bookingSheetPost.Origin = $("#cboOrigin").val();
        bookingSheetPost.Destination = $("#cboDestination").val();
        bookingSheetPost.GoodDescription = $("#txtGoodsDescription").val();
        bookingSheetPost.TotalPackage = $("#txtTotalPackage").val();
        bookingSheetPost.NetWeight = $("#txtNetWeight").val();
        bookingSheetPost.GrossWeight = $("#txtGrossWeight").val();
        bookingSheetPost.LoadingDate = $("#txtLoadingDate").val();
        bookingSheetPost.CorssBorderDate = $("#txtCrossBorderDate").val();
        bookingSheetPost.ETARequirement = $("#txtEtaRequirement").val();
        //bookingSheetPost.ThaiTrucker = $("#cboThaiTrucker").val()[0];
        bookingSheetPost.OverseaTrucker = cboOverseaTruckerList;
        bookingSheetPost.LoloYardOrUnloading = $('input[name="typeloloyard/unloading"]:checked').val();
        bookingSheetPost.ThaiForwarder = cboThaiForwarderList;
        bookingSheetPost.OverseaForwarder = cboOverForwarderList;
        bookingSheetPost.LCLOrFCL = $('input[name="lcl/fcl"]:checked').val();
        bookingSheetPost.CYOrCFS = $('input[name="cy/cfs"]:checked').val();
        bookingSheetPost.LOLOYARDRemark = $("#txtLoloYard").val();
        bookingSheetPost.SpeacialRequest = $("#specialRequest").val();
        bookingSheetPost.Remarks = $("#documentRequirement").val();
        bookingSheetPost.ServiceType = $("#cboServiceType").val();
        bookingSheetPost.TruckType = truckTypeList;
        bookingSheetPost.Commodities = invoiceList;
        bookingSheetPost.PlaceOfLoadings = placeLoadingList;
        bookingSheetPost.PlaceOfDeliveries = placeDeliveryList;
        bookingSheetPost.Volumes = volumeList;
        bookingSheetPost.ThaiBorders = cboThaiBorderList;
        bookingSheetPost.BookingID = $("#BookingID").val();
        bookingSheetPost.JobNumber = $("#btnJobNumber").val();
        bookingSheetPost.DG = $("#cboDamageGood").val();
        if ($('input[name="typeloloyard/unloading"]:checked').val() == "3") {
            bookingSheetPost.OtherRemark = $("#txtLoloOtherRemark").val();
        }
        $.ajax({
            url: url,
            type: "PUT",
            data: { postBookingSheetRequest: bookingSheetPost },
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
