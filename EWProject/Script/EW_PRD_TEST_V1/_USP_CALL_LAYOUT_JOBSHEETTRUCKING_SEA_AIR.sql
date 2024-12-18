ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_LAYOUT_JOBSHEETTRUCKING_SEA_AIR(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
USING SQLSCRIPT_STRING AS LIBRARY;
	IF :par1 = 'GetJobSheetTruckingHeadByDocEntry' THEN 
		SELECT 
			 B."DocNum" AS "EWTInvNo"
			--,C."DocNum" AS "SQRef"
			,A."U_JOBNO" AS "JobNumber"
			,F."descript"||' - '|| FF."descript" AS "ROUTE"
			,TO_VARCHAR(E."U_BOOKINGDATE",'DD-MM-YYYY') AS "BookingDate"
			,G."Name" AS "ImportType"
			,H."SlpName" AS "SaleName"
			,J."firstName"||' - '||J."lastName" AS "CSName"
			,K."CardName" AS "CO"
			,E."U_TOTALPACKAGE" AS "TotalPackage"
			,E."U_NETWEIGHT" AS "NETWEIGHT"
			,E."U_GROSSWEIGHT" AS "GROSSWEIGHT"
			,TO_VARCHAR(E."U_LOADINGDATE",'DD-MM-YYYY') AS "LOADINGDATE"
			,TO_VARCHAR(E."U_CROSSBORDERDATE",'DD-MM-YYYY') AS "CROSSBORDERDATE"
			,TO_VARCHAR(E."U_ETAREQUIREMENT",'DD-MM-YYYY') AS "ETAREQUIREMENT"
			,E."U_GOODSDESCRIPTION" AS "GOODSDESCRIPTION"
			,(SELECT 
					STRING_AGG(T1."CardName")
				FROM EW_PRD_TEST."@TBOVERSEATRANSPORT" T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_OVEARSEATRANSPORT"
				WHERE T0."DocEntry"=E."DocEntry") AS "OVERSEASTRANSPORT"
			,TO_DECIMAL(A."U_TOTALCOSTING",12,2) AS "TOTALCOSTING"
			,TO_DECIMAL(A."U_REBATE",12,2) AS "REBATE"
			,TO_DECIMAL(A."U_GRANDTOTALCOSTING",12,2) AS "GRANDTOTALCOSTING"
			,TO_DECIMAL(A."U_TOTALSELLING",12,2) AS "TOTALSELLING"
			,TO_DECIMAL(A."U_GRANDTOTALSELLING",12,2) AS "GRANDTOTALSELLING"
			,TO_DECIMAL(A."U_TOTALPROFIT",12,2) AS "TOTALPROFIT"
			,A."U_REMARKSCOSTING" AS "REMARKSCOSTING"
			,A."U_REMAKRSSELLING" AS "REMARKSELLING"
			,TO_DECIMAL(A."U_CURRENCYCOSTING",12,2) AS "CURRENCYCOSTING"
			,TO_DECIMAL(A."U_CURRENCYSELLING",12,2) AS "CURRENCYSELLING"
			,TO_DECIMAL(A."U_DutyTaxAmountCosting",12,2) AS "DUTYAMOUNTCOSTING"
			,TO_DECIMAL(A."U_DutyTaxAmountSelling",12,2) AS "DUTYAMOUNTSELLING"
			,TO_DECIMAL(A."U_AdvanceAmountCosting",12,2) AS "ADVANCEAMOUNTCOSTING"
			,TO_DECIMAL(A."U_AdvanceAmountSelling",12,2) AS "ADVANCEAMOUNTSELLING"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."ORDR" AS B ON B."DocEntry"=A."U_SALESORDERDOCNUM"
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS D ON D."DocEntry"=A."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS E ON E."DocEntry"=D."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."OTER" AS F ON CAST(F."territryID" AS VARCHAR(25))=IFNULL(E."U_ORIGIN",'') --Origin Route
		LEFT JOIN EW_PRD_TEST."OTER" AS FF ON CAST(FF."territryID" AS VARCHAR(25))=IFNULL(E."U_DESTINATION",'') --Destination Route
		LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS G ON G."Code"=E."U_IMPORTTYPE"
		LEFT JOIN EW_PRD_TEST."OSLP" AS H ON H."SlpCode"=E."U_SALEEMPLOYEE"
		LEFT JOIN EW_PRD_TEST."@TBUSER" AS I ON I."Code"=E."U_CreateBy"
		LEFT JOIN EW_PRD_TEST."OHEM" AS J ON J."empID"=I."U_EMPLOYEEID"
		LEFT JOIN EW_PRD_TEST."OCRD" AS K ON K."CardCode"=B."CardCode"
		WHERE A."DocEntry"= :par2 ;
	ELSE IF :par1='GetCommoditiesJobSheetTrucking' THEN
		SELECT 
			B."U_Invoice" AS "COMMODITIES"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@COMMODITY_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetShipperJobSheetTrucking' THEN
		SELECT 
			D."CardName" AS "SHIPPER"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"		
		LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=B."U_SHIPPER"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetConsigneeJobSheetTrucking' THEN
		SELECT 
			D."CardName" AS "Consignee"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=B."U_CONSIGNEE"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetPlaceOfloadingJobSheetTrucking' THEN
		SELECT 
			IFNULL(D."Name",'') AS "PlaceOfloading"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TB_POL_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"		
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS D ON D."Code"=B."U_PLACEOFLOADING"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetPlaceOfDeliveryJobSheetTrucking' THEN
		SELECT 
			IFNULL(D."Name",'') AS "PlaceOfDelivery"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TB_POD_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"		
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS D ON D."Code"=B."U_PLACEOFDELIVERY"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetVolumeJobSheetTrucking' THEN
		SELECT * FROM (
			SELECT 
				IFNULL(CAST(TO_DECIMAL(SUM(B."U_Qty"),12,0) AS VARCHAR),'')||IFNULL(' X '||B."U_ContainerType",'') AS "Volume"
			FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
			LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TB_CON_S_A" AS B ON B."DocEntry"=A."DocEntry"
			WHERE AA."DocEntry"=:par2 
				--AND B."U_ContainerType" IS NOT NULL  
				GROUP BY B."U_ContainerType"
			UNION
			SELECT 
				IFNULL(CAST(TO_DECIMAL(SUM(B."U_Qty"),12,0) AS VARCHAR),'')||IFNULL(' X '||B."U_ContainerType",'') AS "Volume"
			FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
			LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TB_TRUCK_S_A" AS B ON B."DocEntry"=A."DocEntry"
			WHERE 
				AA."DocEntry"=:par2 
				--AND B."U_ContainerType" IS NOT NULL 
				GROUP BY B."U_ContainerType"
		)AS A WHERE A."Volume"!='';
	ELSE IF :par1='GetThaiForwarderJobSheetTrucking' THEN
		SELECT 
			IFNULL(D."CardName",'') AS "ThaiForwarder"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TB_TFW_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=B."U_THAIFORWARDER"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetOverSeaForwarderJobSheetTrucking' THEN
		SELECT 
			IFNULL(D."CardName",'') AS "OverSeaForwarder"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TB_OSFW_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=B."U_OVERSEAFORWARDER"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetThaiBorderJobSheetTrucking' THEN
		SELECT 
			IFNULL(D."Name",'') AS "ThaiBorder"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS C ON C."DocEntry"=AA."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=C."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS B ON B."DocEntry"=A."DocEntry"
		LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS D ON D."Code"=B."U_THAIBORDER"
		WHERE AA."DocEntry"=:par2;
	ELSE IF :par1='GetContainerJobSheetTruckingOdd' THEN
		SELECT 
				*
		FROM (
			SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY B."LineId" DESC ) AS "ROWNUM"			
					,CASE WHEN B."U_TYPE"='FreeKey' THEN
						B."U_TRUCKPROVINCE"||' '||B."U_TRUCKPLATENO"||' /'||B."U_TRAILERPROVINCE"||' '||B."U_TRAILERPLATE" 
					ELSE 
						IFNULL(CAST(E."Location" AS VARCHAR(255)),'')||' '||IFNULL(E."InventryNo"||' /','')||IFNULL(B."U_TRAILERPROVINCE",'')||' '||IFNULL(B."U_TRAILERPLATE",'') 
					END AS "TruckNo"
					,IFNULL(B."U_CONTAINERNO",'') AS "ContainerNo"
					,IFNULL(IFNULL(D."U_ShortName",D."CardName"),'') AS "Transporter"
				FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
				LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS A ON A."DocEntry"=AA."U_CONFIRMBOOKINGID"
				LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS B ON B."DocEntry"=A."DocEntry"
				LEFT JOIN EW_PRD_TEST."@TB_TFW_SEA_AIR" AS C ON C."DocEntry"=A."U_BOOKINGID"
				LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=B."U_VENDORCODE"
				LEFT JOIN EW_PRD_TEST."OITM" AS E ON E."ItemCode"=B."U_TRUCKPLATENO"
				LEFT JOIN EW_PRD_TEST."OLCT" AS EE ON EE."Code"=E."Location"--Location
				WHERE  B."LineId" IS NOT NULL AND AA."DocEntry"=:par2 --AND IFNULL(B."U_CONTAINERNO",'') !=''
		)AS A WHERE MOD(A."ROWNUM",2)=1;
	ELSE IF :par1='GetContainerJobSheetTruckingEvent' THEN
		SELECT 
				*
		FROM (
			SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY B."LineId" DESC ) AS "ROWNUM"			
					,CASE WHEN B."U_TYPE"='FreeKey' THEN
						B."U_TRUCKPROVINCE"||' '||B."U_TRUCKPLATENO"||' /'||B."U_TRAILERPROVINCE"||' '||B."U_TRAILERPLATE" 
					ELSE 
						IFNULL(CAST(E."Location" AS VARCHAR(255)),'')||' '||IFNULL(E."InventryNo"||' /','')||IFNULL(B."U_TRAILERPROVINCE",'')||' '||IFNULL(B."U_TRAILERPLATE",'') 
					END AS "TruckNo"
					,IFNULL(B."U_CONTAINERNO",'') AS "ContainerNo"
					,IFNULL(IFNULL(D."U_ShortName",D."CardName"),'') AS "Transporter"
				FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS AA
				LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS A ON A."DocEntry"=AA."U_CONFIRMBOOKINGID"
				LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS B ON B."DocEntry"=A."DocEntry"
				LEFT JOIN EW_PRD_TEST."@TB_TFW_SEA_AIR" AS C ON C."DocEntry"=A."U_BOOKINGID"
				LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=B."U_VENDORCODE"
				LEFT JOIN EW_PRD_TEST."OITM" AS E ON E."ItemCode"=B."U_TRUCKPLATENO"
				LEFT JOIN EW_PRD_TEST."OLCT" AS EE ON EE."Code"=E."Location"--Location
				WHERE  B."LineId" IS NOT NULL AND AA."DocEntry"=:par2 --AND IFNULL(B."U_CONTAINERNO",'') !=''
		)AS A WHERE MOD(A."ROWNUM",2)=0;
	ELSE IF :par1='GetItemCostingJobSheetTrucking' THEN
		SELECT 
			 C."ItemName" AS "ItemCode"
			,TO_DECIMAL(B."U_TOTALPRICE",12,4) AS "UnitPrice"
			,B."U_Currency" AS "Currency"
			,TO_DECIMAL(B."U_Qty",12,4) AS "Qty"
			,IFNULL(D."UomName",'') AS "Unit"
			,TO_DECIMAL(B."U_ExchangeRate",12,2) AS "ExchangeRate"
			,LEFT(E."CardName",10) AS "CardName"
			,TO_DECIMAL(B."U_TotalBaht",12,4) AS "TotalThaiBaht"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TB_JOBSHEET_COSTING" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OITM" AS C ON C."ItemCode"=B."U_ITEMCODE"
		LEFT JOIN EW_PRD_TEST."OUOM" AS D ON D."UomEntry"=B."U_ContainerType"
		LEFT JOIN EW_PRD_TEST."OCRD" AS E ON E."CardCode"=B."U_VendorCode"
		WHERE A."DocEntry"=:par2 ORDER By B."LineId" ASC;
	ELSE IF :par1='GetItemCostingJobSheetTruckingDutyTax' THEN
		SELECT 
			 IFNULL(B."Dscription",'') AS "ItemCode"
			,IFNULL(B."LineTotal",0) AS "TotalPrice"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."RDR10" AS C ON C."DocEntry"=A."U_SALESORDERDOCNUM"
		LEFT JOIN EW_PRD_TEST."RDR1" AS B ON B."DocEntry"=C."DocEntry" AND  B."LineNum"=C."AftLineNum"+1
		WHERE A."DocEntry"=:par2;
	ELSE IF :par1='GetItemSellingJobSheetTrucking' THEN
		SELECT 
			 C."ItemName" AS "ItemCode"
			,TO_DECIMAL(B."U_TOTALPRICE",12,4) AS "UnitPrice"
			,B."U_Currency" AS "Currency"
			,TO_DECIMAL(B."U_Qty",12,4) AS "Qty"
			,IFNULL(D."UomName",'') AS "Unit"
			,TO_DECIMAL(B."U_ExchangeRate",12,2) AS "ExchangeRate"
			,LEFT(E."CardName",10) AS "CardName"
			,TO_DECIMAL(B."U_TotalBaht",12,4) AS "TotalThaiBaht"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TB_JOBSHEET_SELLING" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OITM" AS C ON C."ItemCode"=B."U_ITEMCODE"
		LEFT JOIN EW_PRD_TEST."OUOM" AS D ON D."UomEntry"=B."U_ContainerType"
		LEFT JOIN EW_PRD_TEST."OCRD" AS E ON E."CardCode"=A."U_CARDCODE"
		WHERE A."DocEntry"=:par2 AND B."U_TOTALPRICE"!=0 ORDER By B."LineId" ASC;
	ELSE IF :par1='GetItemSellingJobSheetTruckingDutyTax' THEN
		SELECT 
			 IFNULL(B."Dscription",'') AS "ItemCode"
			,IFNULL(B."LineTotal",0) AS "TotalPrice"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."RDR10" AS C ON C."DocEntry"=A."U_SALESORDERDOCNUM"
		LEFT JOIN EW_PRD_TEST."RDR1" AS B ON B."DocEntry"=C."DocEntry" AND  B."LineNum"=C."AftLineNum"+1
		WHERE A."DocEntry"=:par2;
	ELSE IF :par1='GetItemJobSheetTruckingSummary' THEN
		SELECT 
			 C."ItemName" AS "ItemCode"
			,TO_DECIMAL(B0."U_TotalBaht",12,4) AS "TotalThaiBahtBilling"
			,TO_DECIMAL(B1."U_TotalBaht",12,4) AS "TotalThaiBahtCosting"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TB_JOBSHEET_SELLING" AS B0 ON A."DocEntry"=B0."DocEntry"
		LEFT JOIN EW_PRD_TEST."@TB_JOBSHEET_COSTING" AS B1 ON A."DocEntry"=B1."DocEntry"
		LEFT JOIN EW_PRD_TEST."OITM" AS C ON C."ItemCode"=B0."U_ITEMCODE"
		LEFT JOIN EW_PRD_TEST."OCRD" AS E ON E."CardCode"=A."U_CARDCODE"
		WHERE A."DocEntry"=:par2;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
END;
