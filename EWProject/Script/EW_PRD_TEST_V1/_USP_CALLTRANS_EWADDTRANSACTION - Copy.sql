ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLTRANS_EWADDTRANSACTION(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	
	IF :DTYPE = 'AddAlertMessage' THEN 
		IF :par1<>'' THEN
			execute immediate :par1;
		END IF; 
	ELSE IF :DTYPE = 'UPDATENUMATCARDSQ' THEN
		execute immediate 'UPDATE 
								EW_PRD_TEST."OQUT" SET "NumAtCard"=(SELECT SUBSTRING_REGEXPR(''[^/]+'' IN '''||:par1||''' FROM 1 OCCURRENCE 1)||''/''||'''||:par2||''' from dummy) 
							WHERE "DocEntry"='''||:par2||''';';
	ELSE IF :DTYPE='AddReimbursement' THEN
		INSERT INTO EW_PRD_TEST."@TBREIMBURSEMENT"("Code","U_CardCode","U_Amount","U_Remark","U_DocStatus","U_CreateDate") 
			VALUES((EW_PRD_TEST."@TBREIMBURSEMENT_S".nextval),:par1,:par2,:par3,'P',CURRENT_DATE);
		SELECT CAST(MAX("Code") AS VARCHAR(30)) AS "DocEntry" FROM EW_PRD_TEST."@TBREIMBURSEMENT";
	ELSE IF :DTYPE='UpdateConfirmBookingStatus' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"='Y' WHERE "DocEntry"=:par1 AND "LineId"=:par2;
		--SELECT CAST(:par1 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;
		--SELECT * FROM EW_PRD_TEST."@TRUCKINFORMATION"
	ELSE IF :DTYPE='UpdateReimbursement' THEN
		UPDATE EW_PRD_TEST."@TBREIMBURSEMENT" SET "U_CardCode"=:par1,"U_Amount"=:par2,"U_Remark"=:par3 WHERE "Code"=:par4;
		SELECT CAST(:par4 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;		
	ELSE IF :DTYPE='DeleteReimbursement' THEN
		DELETE FROM EW_PRD_TEST."@TBREIMBURSEMENT" WHERE "Code"=:par1;
		SELECT CAST(:par4 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;
	ELSE IF :DTYPE='UpdateStatusReimbursement' THEN
		UPDATE EW_PRD_TEST."@TBREIMBURSEMENT" SET "U_DocStatus"=:par1,"U_Reason"=:par2,"U_UserApprove"=:par3 WHERE "Code"=:par4;
		SELECT CAST(:par3 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;	
	ELSE IF :DTYPE = 'UPDATENUMATCARDSO' THEN
		UPDATE EW_PRD_TEST."ORDR" SET "NumAtCard"='' WHERE "DocEntry"=:par2;
		UPDATE EW_PRD_TEST."@JOBSHEETRUCKING" SET "U_SALESORDERDOCNUM"=NULL,"U_CONFIRMBOOKINGID"=NULL WHERE "DocEntry"=:par1;
	ELSE IF :DTYPE = 'UpdateLineStatusInactivePRCOS' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"=''
		WHERE "DocEntry"= (SELECT DISTINCT
							 (SELECT
								T0."DocEntry"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1 AND A."U_ItemCode"='01-T-1-0001')
			  AND "LineId"=:par2;
	ELSE IF :DTYPE = 'UpdateLineStatusActivePRCOS' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"='Y'
		WHERE "DocEntry"= (SELECT DISTINCT
							 (SELECT
								T0."DocEntry"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1)
			  AND "LineId"=:par2;
	ELSE IF :DTYPE = 'CancelLineStatusInActivePRCOS' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"=''
		WHERE "DocEntry"= (SELECT DISTINCT
							 (SELECT
								T0."DocEntry"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1)
			  AND "LineId"=(SELECT DISTINCT
							 (SELECT
								T0."LineId"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1 AND A."U_ItemCode"='01-T-1-0001');
	ELSE IF :DTYPE='ADDBOOKINGSHEET' THEN
		SELECT 
			 CAST(A."DocEntry"  AS INT) AS "DocEntry"
			,A."U_EWSeries" AS "EWSereis"
			,A."U_CO" AS "CO"
			,CAST(A."U_SALEEMPLOYEE" AS INT) AS "SaleEmployee"
			,TO_VARCHAR(A."U_IMPORTTYPE") AS "ImportType"
			,CAST(A."U_ORIGIN" AS INT) AS "Origin"
			,CAST(A."U_DESTINATION" AS INT) AS "Destination"
			,TO_VARCHAR(A."U_GOODSDESCRIPTION") AS "GoodDescription"
			,TO_VARCHAR(A."U_TOTALPACKAGE") AS "TotalPackage"
			,CAST(A."U_NETWEIGHT" AS DOUBLE) AS "NetWeight"
			,CAST(A."U_GROSSWEIGHT" AS DOUBLE) AS "GrossWeight"
			,TO_VARCHAR(A."U_JOBNO") AS "JobNumber"
			,TO_VARCHAR(A."U_LOADINGDATE",'YYYY-MM-DD') AS "LoadingDate"
			,TO_VARCHAR(A."U_CROSSBORDERDATE",'YYYY-MM-DD') AS "CorssBorderDate"
			,TO_VARCHAR(A."U_ETAREQUIREMENT",'YYYY-MM-DD') AS "ETARequirement"
			,CAST(A."U_LOLOYARDORUNLOADING" AS INT) AS "LoloYardOrUnloading"
			,CAST(A."U_LCLORFCL" AS INT) AS "LCLOrFCL"
			,CAST(A."U_CYORCFS" AS INT) AS "CYOrCFS"
			,A."U_LOLOYARDRemark" AS "LOLOYARDRemark"
			,A."U_SERVICETYPE" AS "ServiceType"
			,A."U_UserCreate" AS "CreateUser"
			,A."U_SPECIALREQUEST" AS "SpeacialRequest"
			,A."Remark" AS "Remarks"
			,A."Remark" AS "Remarks"
			,TO_VARCHAR(A."U_JOBNO") AS "JobNumber"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N');
	ELSE IF :DTYPE='Commodities' THEN
		SELECT 
			 B."U_INVOICE" AS "INVOICE"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@COMMODITY" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N') AND B."U_INVOICE" IS NOT NULL
			AND A."DocEntry"=:par1;
	ELSE IF :DTYPE='PlaceOfLoadings' THEN
		SELECT 
			  B."U_PLACELOADING" AS "PLACELOADING"
			 ,TO_VARCHAR(B."U_District") AS "District"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@PLACEOFLOADING" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1;
	ELSE IF :DTYPE='PlaceOfDeliveries' THEN
		SELECT 
			  B."U_PLACEDELIVERY" AS "PLACEDELIVERY"
			 ,TO_VARCHAR(B."U_District") AS "District"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@PLACEOFDELIVERY" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1;
	ELSE IF :DTYPE='Volumes' THEN
		SELECT 
			  CAST(IFNULL(B."U_QTY",0) AS INT) AS "QTY"
			 ,B."U_VOLUMECODE" AS "VOLUMECODE"
			 ,CAST(B."U_GROSSWEIGHT" AS double) AS "GROSSWEIGHT"
			 ,B."U_InvoiceList" AS "InvoiceList"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@VOLUME" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N') AND B."U_VOLUMECODE" IS NOT NULL
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='ThaiBorders' THEN
		SELECT 
			 B."U_ThaiBorder" AS "ThaiBorder"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@THAIBORDER" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='OverseaTrucker' THEN
		SELECT 
			 B."U_OVERSEATRUCKERCODE" AS "OVERSEATRUCKERCODE"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@TBOVERSEATRUCKER" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='OverseaForwarder' THEN
		SELECT 
			 B."U_OVERSEAFORWARDERCODE" AS "OVERSEAFORWARDERCODE"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@TBOVERSEAFORWARDER" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='SaleQuotation' THEN
		SELECT 
			 B."U_DOCENTRY" AS "DOCENTRY"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@TBSALESQUOTATION" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N') AND B."U_DOCENTRY" IS NOT NULL
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='ThaiForwarder' THEN
		SELECT 
			 B."U_THAIFORWARDER" AS "THAIFORWARDER"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@THAIFORWARDER" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1;
	ELSE IF :DTYPE='Shipper' THEN
		SELECT 
			 B."U_SHIPPER" AS "SHIPPER"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@TBSHIPPER" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='Consignee' THEN
		SELECT 
			 TO_VARCHAR(B."U_CONSIGNEE") AS "CONSIGNEE"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@TBCONSIGNEE" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N')
			AND CAST(A."DocEntry" AS VARCHAR(255))=:par1 ;
	ELSE IF :DTYPE='TruckType' THEN
		SELECT 
			  CAST(IFNULL(B."U_QTY",0) AS VARCHAR(255)) AS "QTY"
			 ,B."U_TRUCKTYPE" AS "TRUCKTYPE"
			 ,CAST(B."U_GROSSWEIGHT" AS double) AS "GROSSWEIGHT"
			 ,B."U_InvoiceList" AS "InvoiceList"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST_E."@TBTRUCKTYPEROW" AS B ON B."DocEntry"=A."DocEntry"
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N') AND (B."U_TRUCKTYPE" IS NOT NULL)
			AND A."DocEntry"=:par1 ;
	ELSE IF :DTYPE='ADDCONFIRMBOOKING' THEN
		SELECT 
			 T0."DocEntry" AS "DocEntry"
			,CAST(T3."DocEntry" AS INT) AS "BookingID"
			,TO_VARCHAR(T2."U_SALEEMPLOYEE") AS "SlpCode"
			,T0."U_CreateUser" AS "CreateUser"
			,T0."U_CSName" AS "CSName"
			,TO_VARCHAR(T0."U_PRICELIST") AS "PriceList"
			,T0."Remark" AS "Remarks"
		FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
		LEFT JOIN EW_PRD_TEST_E."@BOOKINGSHEET" AS T2 ON T2."DocEntry"=T0."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
		LEFT JOIN EW_PRD_TEST."@BOOKINGSHEET" AS T3 ON T3."U_JOBNO"=RIGHT(T1."NAME",LENGTH(T1."NAME" )-4)
		WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
			AND T0."Canceled"='N'
			AND T1."NAME" NOT IN (SELECT 
										T1."NAME" 
									FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
									LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
									--WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
									) AND T0."U_BOOKINGID"!='804';
	ELSE IF :DTYPE='TRUCKINFORMATION' THEN
		SELECT
			  TO_VARCHAR(T5."U_TYPE") AS "Type"
			 ,T5."U_ExchangeType" AS "ExchangeType"
			 ,T5."U_CONTAINERTYPE" AS "ContainerType"
			 ,T5."U_CONTAINERNO" AS "ContainerNo"
			 ,T5."U_OWNER" AS "Owner"
			 ,T5."U_OWNER2" AS "Owner2"
			 ,TO_VARCHAR(T5."U_GROSSWEIGHT") AS "GrossWeight"
			 ,T5."U_YARD" AS "Yard"
			 ,T5."U_FCL_LCL" AS "FCL_LCL"
			 ,T5."U_LOLO_UNLOADING" AS "LOLO_Unloading"
			 ,TO_VARCHAR(T5."U_TRUCKPROVINCE") AS "TruckProvince"
			 ,T5."U_TRUCKPLATENO" AS "TruckPlateNo"
			 ,T5."U_TRUCKTYPE" AS "TruckType"
			 ,T5."U_BRAND" AS "Brand"
			 ,T5."U_TRUCKCODE" AS "TruckCode"
			 ,T5."U_COLOR" AS "Color"
			 ,T5."U_TRAILERPROVINCE" AS "TrailerProvince"
			 ,T5."U_TRAILERPLATE" AS "TrailerPlate"
			 ,T5."U_TRAILERTYPE" AS "TrailerType"
			 ,TO_VARCHAR(T5."U_DRIVER1") AS "DriverCode1"
			 ,T5."U_TPNO1" AS "TPNo1"
			 ,T5."U_IDCARD1" AS "IDCard1"
			 ,TO_VARCHAR(T5."U_DRIVERLICENSE1") AS "DriverLicense1"
			 ,TO_VARCHAR(T5."U_DRIVER2") AS "DriverCode2"
			 ,T5."U_TPNO2" AS "TPNo2"
			 ,T5."U_IDCARD2" AS "IDCard2"
			 ,T5."U_DRIVERLICENSE2" AS "DriverLicense2"
			 ,T5."U_VENDORCODE" AS "VendorCode"
			 ,CAST(T5."U_TRUCKCOSTTHB" AS double) AS "TruckCostTHB"
			 ,T5."U_SEALNO1" AS "SealNo1"
			 ,T5."U_SEALNO2" AS "SealNo2"
			 ,T5."U_ContainerOption" AS "ContainerOption"
			 ,TO_VARCHAR(T5."U_BookingLineId") AS "BookingLineId"
			--,T3."DocEntry" AS ADVANCE
			--,T4."DocEntry" AS PURCHASE
		FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
		LEFT JOIN EW_PRD_TEST_E."@BOOKINGSHEET" AS T2 ON T2."DocEntry"=T0."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST_E."@TRUCKINFORMATION" AS T5 ON T5."DocEntry"=T0."DocEntry"
		LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
		WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
			AND T0."Canceled"='N'
			AND T1."NAME" NOT IN (SELECT 
										T1."NAME" 
									FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
									LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
									) AND T0."DocEntry"=:par1;
	ELSE IF :DTYPE='ADVANCEPAYMENT' THEN
		SELECT
			   TO_VARCHAR(T5."DocEntry") AS "DocEntry"
			  ,T5."Remark" AS "Remarks"
			  ,T5."U_NumAtCard" AS "RefNo"
			  ,T5."U_UserID" AS "IssueBy"
			  ,TO_VARCHAR(T5."U_IssueDate",'YYYY-MM-DD') AS "IssueDate"
			  ,TO_VARCHAR(T5."U_DueDate",'YYYY-MM-DD') AS "DueDate"
			  ,TO_VARCHAR(T5."U_PRSeries") AS "Series"
			  ,T5."U_VendorCode" AS "VendorCode"
			  ,CAST(T5."U_AmountTHB" AS decimal) AS "AmountTHB"
			  ,CAST(T5."U_Amount" AS decimal) AS "GrandTotal"
			  ,T5."U_CurrencyType" AS "AmountCurrency"
		FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
		LEFT JOIN EW_PRD_TEST_E."@TBPURCHASEREQUEST" AS T4 ON T4."DocEntry"=T0."DocEntry"
		LEFT JOIN EW_PRD_TEST_E."@ADVANCEPAYMENT" AS T5 ON T5."DocEntry"=T4."U_DOCENTRY"
		LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
		WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
			AND T0."Canceled"='N'
			AND T1."NAME" NOT IN (SELECT 
										T1."NAME" 
									FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
									LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
									) AND T4."U_DOCENTRY" IS NOT NULL
									AND T0."DocEntry"=:par1;
	ELSE IF :DTYPE='ADVANCEPAYMENTROW' THEN
		SELECT
				T5."U_ItemCode" AS "ItemCode"
			   ,CAST(T5."U_Price" AS decimal) AS "Amount"
			   ,T5."U_Origin" AS "Origin"
			   ,T5."U_Destination" AS "Destination"
			   ,T5."U_Remarks" AS "remark"
			   ,T5."U_DistributionRule" AS "DistributionRule"
			   ,T5."U_RefInv" AS "RefInv"
			   ,TO_VARCHAR(T5."U_JobNumber") AS "JobNo"
			   ,T5."U_TaxCode" AS "VatCode"
			   ,T5."U_TruckNo" AS "TruckNumber"
		FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
		LEFT JOIN EW_PRD_TEST_E."@TBPURCHASEREQUEST" AS T4 ON T4."DocEntry"=T0."DocEntry"
		LEFT JOIN EW_PRD_TEST_E."@ADVANCEPAYMENTROW" AS T5 ON T5."DocEntry"=T4."U_DOCENTRY"
		LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
		WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
			AND T0."Canceled"='N'
			AND T1."NAME" NOT IN (SELECT 
										T1."NAME" 
									FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
									LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
									) AND T4."U_DOCENTRY" IS NOT NULL
									AND T5."DocEntry"=:par1;
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
END
