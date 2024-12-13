--CALL "EW_PRD_TEST"._USP_CALLTRANS_EWTRANSACTION_SEA_AIR ('GetDetailJobSheetByDocEntry','3','','','','')
ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLTRANS_EWTRANSACTION_SEA_AIR(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	
	IF :DTYPE='GetListConfirmBookingSheetSeaAndAir' THEN
		IF :par3='ALL' THEN
			SELECT 
				 AA."DocEntry" AS CONFIRMBOOKINGID
				,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" AS T0 WHERE T0."AbsEntry"=AA."U_PROJECTMANAGEMENTID") AS JOBNO
				,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				,CASE WHEN A."CreateTime"=A."UpdateTime" THEN 
				     	'' 
				     	ELSE  TO_VARCHAR(AA."UpdateDate",'dd/MM/YYYY') END AS UPDATEDATE
					 ,LEFT(CASE
							WHEN LENGTH(AA."CreateTime") = 3  THEN CAST('0' || AA."CreateTime"AS TIME) 
							WHEN LENGTH(AA."CreateTime") = 2 THEN CAST('00' || AA."CreateTime"AS TIME)
							WHEN LENGTH(AA."CreateTime") = 1 THEN CAST('000' || AA."CreateTime"AS TIME) 
							ELSE CAST(AA."CreateTime"AS TIME) END
				     ,5) AS "CREATETIME"
				     ,CASE WHEN A."CreateTime"=A."UpdateTime" THEN 
				     	'' 
				     	ELSE 
				     	LEFT(CASE
							WHEN LENGTH(AA."UpdateTime") = 3  THEN CAST('0' || AA."UpdateTime" AS TIME) 
							WHEN LENGTH(AA."UpdateTime") = 2 THEN CAST('00' || AA."UpdateTime" AS TIME)
							WHEN LENGTH(AA."UpdateTime") = 1 THEN CAST('000' || AA."UpdateTime" AS TIME) 
							ELSE CAST(AA."UpdateTime" AS TIME) END ,5) 
						END
				      AS "UPDATETIME"
				,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS LOADINGDATE
				,TO_VARCHAR(A."U_CROSSBORDERDATE",'DD/MM/YYYY') AS "CrossBorderDate"
				,TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY') AS "ETARequirementDate"
				,B."Name" AS IMPORTYPE
				,'' AS CO
				,D."descript" AS ROUTE
				,E."Code" AS CREATEBY
				,AA."DocEntry" AS DOCENTRY
				,CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 THEN 
					CASE WHEN AA."U_CreateUser" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_CANCEL" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR AA."U_CreateUser"=:par4 THEN
						AA."Status"
					ELSE 'C' END
				 ELSE 'C' END  AS DOCSTATUSCANCEL
				/*,CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 THEN 
					CASE WHEN AA."U_CreateUser" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR AA."U_CreateUser"=:par4 THEN
						AA."Status"
					ELSE 'C' END
				 ELSE 'C' END  AS "DocStatusUpdate"*/
				,CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 THEN 
					CASE WHEN AA."U_CreateUser" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR AA."U_CreateUser"=:par4 THEN
						AA."Status"
					ELSE 'C' END
				ELSE 
				 	CASE WHEN (SELECT MAX(T0."U_SALESORDERDOCNUM") 
				 					FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 
					THEN AA."Status" ELSE 'C' END 
				END  AS "DocStatusUpdate"
				,AA."U_BOOKINGID" AS BOOKINGDOCENTRY
				,AA."U_PROJECTMANAGEMENTID" AS PROJECTDOCENTRY
				,(SELECT STRING_AGG(T1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
					 	WHERE T0."DocEntry"=A."DocEntry") AS "Shipper"
				,(SELECT STRING_AGG(T1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS T0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
					 	WHERE T0."DocEntry"=A."DocEntry") AS "Consignee"
				,IFNULL(AA."U_UpdateBy",'') AS "UpdateBy"
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=A."U_DESTINATION"
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=AA."U_CreateUser"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS F ON F."Code"=A."U_IMPORTTYPE"
			WHERE A."U_CreateBy"=CASE WHEN '-1'='-1' THEN A."U_CreateBy" ELSE A."U_CreateBy" END
				  AND IFNULL(AA."U_BOOKINGID",0) !=0
				  AND AA."U_PROJECTMANAGEMENTID" IS NOT NULL 
				  AND AA."CreateDate" BETWEEN :par1 AND :par2 
				  AND (AA."U_CreateUser"=:par4
					OR AA."U_CreateUser" IN (
								SELECT "U_User" 
								FROM EW_PRD_TEST."@TB_P_READ" AS T0
								LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
								WHERE T1."Code"=:par4
							))
				  ORDER BY AA."DocEntry" DESC;
		ELSE IF :par3='DEFAULT' THEN
			SELECT TOP 1000
				 AA."DocEntry" AS CONFIRMBOOKINGID
				,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" AS T0 WHERE T0."AbsEntry"=AA."U_PROJECTMANAGEMENTID") AS JOBNO
				,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				,CASE WHEN A."CreateTime"=A."UpdateTime" THEN 
				     	'' 
				     	ELSE  TO_VARCHAR(AA."UpdateDate",'dd/MM/YYYY') END AS UPDATEDATE
					 ,LEFT(CASE
							WHEN LENGTH(AA."CreateTime") = 3  THEN CAST('0' || AA."CreateTime"AS TIME) 
							WHEN LENGTH(AA."CreateTime") = 2 THEN CAST('00' || AA."CreateTime"AS TIME)
							WHEN LENGTH(AA."CreateTime") = 1 THEN CAST('000' || AA."CreateTime"AS TIME) 
							ELSE CAST(AA."CreateTime"AS TIME) END
				     ,5) AS "CREATETIME"
				     ,CASE WHEN A."CreateTime"=A."UpdateTime" THEN 
				     	'' 
				     	ELSE 
				     	LEFT(CASE
							WHEN LENGTH(AA."UpdateTime") = 3  THEN CAST('0' || AA."UpdateTime" AS TIME) 
							WHEN LENGTH(AA."UpdateTime") = 2 THEN CAST('00' || AA."UpdateTime" AS TIME)
							WHEN LENGTH(AA."UpdateTime") = 1 THEN CAST('000' || AA."UpdateTime" AS TIME) 
							ELSE CAST(AA."UpdateTime" AS TIME) END ,5) 
						END
				      AS "UPDATETIME"
				,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS LOADINGDATE
				,TO_VARCHAR(A."U_CROSSBORDERDATE",'DD/MM/YYYY') AS "CrossBorderDate"
				,TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY') AS "ETARequirementDate"
				,B."Name" AS IMPORTYPE
				,'' AS CO
				,D."descript" AS ROUTE
				,E."Code" AS CREATEBY
				,AA."DocEntry" AS DOCENTRY
				,CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 THEN 
					CASE WHEN AA."U_CreateUser" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_CANCEL" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR AA."U_CreateUser"=:par4 THEN
						AA."Status"
					ELSE 'C' END
				 ELSE 'C' END  AS DOCSTATUSCANCEL
				/*,CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 THEN 
					CASE WHEN AA."U_CreateUser" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR AA."U_CreateUser"=:par4 THEN
						AA."Status"
					ELSE 'C' END
				 ELSE 'C' END  AS "DocStatusUpdate"*/
				,CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 THEN 
					CASE WHEN AA."U_CreateUser" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR AA."U_CreateUser"=:par4 THEN
						AA."Status"
					ELSE 'C' END
				ELSE 
				 	CASE WHEN (SELECT MAX(T0."U_SALESORDERDOCNUM") 
				 					FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O')=0 
					THEN AA."Status" ELSE 'C' END 
				END  AS "DocStatusUpdate"
				,AA."U_BOOKINGID" AS BOOKINGDOCENTRY
				,AA."U_PROJECTMANAGEMENTID" AS PROJECTDOCENTRY
				,(SELECT STRING_AGG(T1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
					 	WHERE T0."DocEntry"=A."DocEntry") AS "Shipper"
				,(SELECT STRING_AGG(T1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS T0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
					 	WHERE T0."DocEntry"=A."DocEntry") AS "Consignee"
				,IFNULL(AA."U_UpdateBy",'') AS "UpdateBy"
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=A."U_DESTINATION"
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=AA."U_CreateUser"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS F ON F."Code"=A."U_IMPORTTYPE"
			WHERE A."U_CreateBy"=CASE WHEN '-1'='-1' THEN A."U_CreateBy" ELSE A."U_CreateBy" END
				  AND IFNULL(AA."U_BOOKINGID",0) !=0
				  AND AA."U_PROJECTMANAGEMENTID" IS NOT NULL 
				  AND AA."CreateDate" BETWEEN :par1 AND :par2 
				  AND (AA."U_CreateUser"=:par4
					OR AA."U_CreateUser" IN (
								SELECT "U_User" 
								FROM EW_PRD_TEST."@TB_P_READ" AS T0
								LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
								WHERE T1."Code"=:par4
							))
				  ORDER BY AA."DocEntry" DESC;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetDetailConfirmBookingSeaAndAirByDocEntry' THEN
		IF :par1='BookingSheetInformation' THEN
			SELECT 
				 B."Name" AS JOBTYPE
				,IFNULL(CC."descript",'')||' - '|| IFNULL(C."descript",'') AS ROUTE
				,D."SlpName" AS SALENAME
				,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" T0 WHERE T0."AbsEntry"=AA."U_PROJECTMANAGEMENTID") AS JOBNO
				,IFNULL(E."ListName",'') AS PRICELIST--IFNULL(E."ListName",'')
				,AA."Remark" AS REMARK
				,AA."DocEntry" AS DOCENTRY
				,IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY'),'') AS "LoadingDate"
				,IFNULL(TO_VARCHAR(A."U_CROSSBORDERDATE",'DD/MM/YYYY'),'') AS "CrossBorderDate"
				,IFNULL(TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY'),'') AS "ETARequirementDate"
				,A."U_CreateBy" AS "CSName"
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA 
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON A."U_IMPORTTYPE"=B."Code"
			LEFT JOIN EW_PRD_TEST."OTER" AS C ON CAST(C."territryID" AS VARCHAR(25))=IFNULL(A."U_DESTINATION",'')
			LEFT JOIN EW_PRD_TEST."OTER" AS CC ON CAST(CC."territryID" AS VARCHAR(25))=IFNULL(A."U_ORIGIN",'')
			LEFT JOIN EW_PRD_TEST."OSLP" AS D ON D."SlpCode"=A."U_SALEEMPLOYEE"
			LEFT JOIN EW_PRD_TEST."OPLN" AS E ON CAST(E."ListNum" AS VARCHAR(25))=IFNULL(AA."U_PRICELIST",'0')
			WHERE AA."DocEntry"=:par2;
		ELSE IF :par1='GetBorderBookingSheet' THEN
			SELECT 
				 IFNULL(C."Code",0) AS CODE
				,IFNULL(C."Name",'') AS NAME
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS B ON A."DocEntry"=B."DocEntry"
			LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS C ON C."Code"=B."U_THAIBORDER"
			WHERE AA."DocEntry"=:par2;
		ELSE IF :par1='PLACEOFLOADING' THEN
			SELECT 
				IFNULL(B."Name"||', '||B."U_COUNTRY",'') AS PLACELOADING
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS ConBookingID
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS AA ON AA."DocEntry"=ConBookingID."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TB_POL_SEA_AIR" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFLOADING"			
			WHERE ConBookingID."DocEntry"=:par2;
		ELSE IF :par1='PLACEOFDELIVERY' THEN
			SELECT 
				IFNULL(B."Name"||', '||B."U_COUNTRY",'') AS PLACEOFDELIVERY 
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS ConBookingID
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS AA ON AA."DocEntry"=ConBookingID."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TB_POD_SEA_AIR" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFDELIVERY"
			WHERE ConBookingID."DocEntry"=:par2;
		ELSE IF :par1='SHIPPER' THEN
			SELECT 
				IFNULL(B."CardName",'') AS SHIPPER 
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS ConBookingID
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS AA ON AA."DocEntry"=ConBookingID."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_SHIPPER"
			WHERE ConBookingID."DocEntry"=:par2;
		ELSE IF :par1='CONSIGNEE' THEN
			SELECT 
				IFNULL(B."CardName",'') AS CONSIGNEE 
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS ConBookingID
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS AA ON AA."DocEntry"=ConBookingID."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
			WHERE ConBookingID."DocEntry"=:par2;
		ELSE IF :par1='GetListOfContainerInformation' THEN
			SELECT 
				 A."U_TYPE" AS "TYPE"
				,A."U_CONTAINERTYPE" AS CONTAINERTYPE
				,A."U_CONTAINERNO" AS CONTAINERNO
				,A."U_OWNER" AS OWNER
				,IFNULL(A."U_GROSSWEIGHT",0) AS GROSSWEIGHT
				,A."U_YARD" AS YARD
				,A."U_FCL_LCL" AS FCL_LCL
				,A."U_LOLO_UNLOADING" AS LOLO_UNLOADING
				,IFNULL(B."Location",A."U_TRUCKPROVINCE") AS TRUCKPROVINCE
				,A."U_TRUCKPLATENO" AS TRUCKPLATENO
				,A."U_TRUCKTYPE" AS TRUCKTYPE
				,A."U_BRAND" AS BRAND
				,A."U_COLOR" AS COLOR
				,A."U_TRAILERPROVINCE" AS TRAILERPROVINCE
				,A."U_TRAILERPLATE" AS TRAILERPLATE
				,A."U_TRAILERTYPE" AS TRAILERTYPE
				,C."firstName" ||' '|| C."lastName" AS DriverName1
				,C."mobile" AS TPNO1
				,C."govID" AS IDCARD1
				,C."U_DriverID" AS DRIVERLICENSE1
				,CC."firstName" ||' '|| C."lastName" AS DriverName2
				,CC."mobile" AS TPNO2
				,CC."govID" AS IDCARD2
				,CC."U_DriverID" AS DRIVERLICENSE2
				,D."CardName" AS VENDOR
				,IFNULL(A."U_TRUCKCOSTTHB",0) AS TRUCKCOSTTHB
				,A."U_SEALNO1" AS SEALNO1
				,A."U_SEALNO2" AS SEALNO2
				,IFNULL(CAST(A."LineId" AS VARCHAR(255)),'') AS LINEID
				,A."U_ContainerOption" AS "ContainerOptionType"
				,IFNULL((
						SELECT
							  IFNULL(T1."U_InvoiceList",'') AS "InvoiceList"
						FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
						LEFT JOIN EW_PRD_TEST."@VOLUME" AS T1 ON T1."DocEntry"=T0."U_BOOKINGID"
						WHERE 	  T0."DocEntry"=A."DocEntry" 
							  AND T1."U_VOLUMECODE"=A."U_CONTAINERTYPE" 
							  AND T1."LineId"=A."U_BookingLineId"
						UNION 
						SELECT 
							  IFNULL(T1."U_InvoiceList",'') AS "InvoiceList"
						FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
						LEFT JOIN EW_PRD_TEST."@TBTRUCKTYPEROW" AS T1 ON T1."DocEntry"=T0."U_BOOKINGID"
						WHERE 	  T0."DocEntry"=A."DocEntry" 
							  AND T1."U_TRUCKTYPE"=A."U_CONTAINERTYPE" 
							  AND T1."LineId"=A."U_BookingLineId"
					),'') AS ListInvoice
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA
			LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS A ON A."DocEntry"=AA."DocEntry" 
			LEFT JOIN EW_PRD_TEST."OLCT" AS B ON CAST(B."Code" AS VARCHAR(255))=IFNULL(A."U_TRUCKPROVINCE",'')
			LEFT JOIN EW_PRD_TEST."OHEM" AS C ON C."empID"=IFNULL(A."U_DRIVER1",0)
			LEFT JOIN EW_PRD_TEST."OHEM" AS CC ON CC."empID"=IFNULL(A."U_DRIVER2",0)
			LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=IFNULL(A."U_VENDORCODE",'')
			WHERE AA."DocEntry"=:par2;
		ELSE IF :par1='ListOfPurchaseRequest' THEN
			SELECT 
				 A."U_NumAtCard" AS DOCNUM
				,A."DocEntry" AS DOCENTRY
				,CASE WHEN TO_VARCHAR(A."U_Project")='0' THEN '' ELSE TO_VARCHAR(A."U_Project") END AS PROJECTNUMBER
				,TO_VARCHAR(A."U_IssueDate",'dd-mm-YYYY') AS ISSUEDATE
				,TO_VARCHAR(A."U_DueDate",'dd-mm-YYYY') AS DUEDATE
				,A."U_VendorCode" AS VENDORCODE
				,B."CardName" AS VENDORNAME
				,D."ChkName" AS CURRENCY
				,A."U_UserID" AS EMPLOYEEID
				,C."firstName" ||' '|| C."lastName" AS EMPLOYEENAME
				,TO_DECIMAL(A."U_Amount",3,2) AS AMOUNT
				,TO_DECIMAL(A."U_AmountTHB",3,2) AS AMOUNTTHB
				,IFNULL(A."Remark",'') AS REMARKS
				,IFNULL(B."DflAccount",'') AS BANKACCOUNT
				,IFNULL(B."DflBranch",'') AS BRANCH	  
				,IFNULL((SELECT IFNULL("OCRY"."Name",'') FROM EW_PRD_TEST."OCRY" WHERE "OCRY"."Code"=B."BankCountr"),'') AS BANKCOUNTRY
				,IFNULL((SELECT IFNULL("ODSC"."BankName",'') FROM EW_PRD_TEST."ODSC" WHERE "ODSC"."BankCode"=B."BankCode"),'') AS BANKNAME
				,IFNULL(B."DflSwift",'') AS SWIFTCODE	
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS F
			LEFT JOIN (
				SELECT * FROM EW_PRD_TEST."@TB_ADVANCE_SEAAIR"
				UNION ALL
				SELECT * FROM EW_PRD_TEST."@TB_PURCHASE_S_A"
			) AS E ON E."DocEntry"=F."DocEntry"
			LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS A ON E."U_DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON A."U_VendorCode"=B."CardCode"
			LEFT JOIN EW_PRD_TEST."OHEM" AS C ON A."U_UserID"=C."empID"
			LEFT JOIN EW_PRD_TEST."OCRN" AS D ON A."U_CurrencyType"=D."CurrCode"
			WHERE F."DocEntry"=:par2 AND A."U_AdvanceType"=:par3 AND CAST(E."U_CONTAINERLINEID" AS NVARCHAR(255))=:par4;
			--WHERE F."DocEntry"='131' AND A."U_AdvanceType"='PRAD' AND CAST(E."U_CONTAINERLINEID" AS NVARCHAR(255))='';
		ELSE IF :par1='ListOfPurchaseRequestLine' THEN
			SELECT 
				 A."U_ItemCode" AS ITEMCODE
				,B."ItemName" AS ITEMNAME
				,A."U_Qty" AS QTY
				,TO_DECIMAL(A."U_Price",3,2) AS PRICE
				,A."U_Origin"||'-'|| A."U_Destination" AS ORIGIN
				,A."U_Origin"||'-'|| A."U_Destination" AS DESTINATION
				,TO_DECIMAL(A."U_Price",3,2) AS AMOUNT
				,A."U_Remarks" AS REMARKS
				,(SELECT "OUOM"."UomCode" FROM EW_PRD_TEST."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS F
			LEFT JOIN (
				SELECT * FROM EW_PRD_TEST."@TB_ADVANCE_SEAAIR"
				UNION ALL
				SELECT * FROM EW_PRD_TEST."@TB_PURCHASE_S_A"
			) AS E ON E."DocEntry"=F."DocEntry"
			LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A ON E."U_DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."OITM" AS B ON A."U_ItemCode"=B."ItemCode"
			WHERE IFNULL(CAST(F."DocEntry" AS VARCHAR(255)),'')=:par2;
		--END IF;
		--END IF;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetEditConfirmBookingSheetSeaAndAir' THEN
		SELECT 
			 IFNULL(A."Remark",'') AS "Remark"
			,CAST(CASE WHEN A."U_PRICELIST"='' THEN '0' ELSE A."U_PRICELIST" END AS INT) AS "PriceList"
			,IFNULL(B."U_TYPE",'') AS "Type"
			,IFNULL(B."U_CONTAINERTYPE",'') AS "ContainerType"
			,IFNULL(B."U_CONTAINERNO",'') AS "ContainerNo"
			,IFNULL(B."U_OWNER",'') AS "Owner"
			,IFNULL(B."U_CONTAINERNO2",'') AS "ContainerNo2"
			,IFNULL(B."U_OWNER2",'') AS "Owner2"
			,IFNULL(B."U_GROSSWEIGHT",0.00) AS "GrossWeight"
			,IFNULL(B."U_YARD",'') AS "Yard"
			,IFNULL(B."U_FCL_LCL",'') AS "FCL_LCL"
			,IFNULL(B."U_LOLO_UNLOADING",'') AS "LOLO_UNLOADING"
			,IFNULL(B."U_TRUCKPROVINCE",'') AS "TruckProvince"
			,IFNULL(B."U_TRUCKPLATENO",'') AS "TruckPlateNo"
			,IFNULL(B."U_TRUCKTYPE",'') AS "TruckType"
			,IFNULL(B."U_BRAND",'') AS "Brand"
			,IFNULL(B."U_TRUCKCODE",'') AS "TruckCode"
			,IFNULL(B."U_COLOR",'') AS "Color"
			,IFNULL(B."U_TRAILERPROVINCE",'') AS "TrailerProvince"
			,IFNULL(B."U_TRAILERPLATE",'') AS "TrailerPlate"
			,IFNULL(B."U_TRAILERTYPE",'') AS "TrailerType"
			,IFNULL(B."U_DRIVER1",0) AS "Driver1"
			,IFNULL(B."U_TPNO1",'') AS "TPNO1"
			,IFNULL(B."U_IDCARD1",'') AS "IDCard1"
			,IFNULL(B."U_DRIVERLICENSE1",'') AS "DriverLicense1"
			,IFNULL(B."U_DRIVER2",0) AS "Driver2"
			,IFNULL(B."U_TPNO2",'') AS "TPNO2"
			,IFNULL(B."U_IDCARD2",'') AS "IDCard2"
			,IFNULL(B."U_DRIVERLICENSE2",'') AS "DriverLicense2"
			,IFNULL(B."U_VENDORCODE",'') AS "VendorCode" --B."U_VENDORCODE"
			,IFNULL(B."U_TRUCKCOSTTHB",0.00) AS "TruckCostTHB"
			,IFNULL(B."U_SEALNO1",'') AS "SealNo1"
			,IFNULL(B."U_SEALNO2",'') AS "SealNo2"
			,IFNULL(B."U_ContainerOption",'') AS "ContainerOption"
			,IFNULL(B."U_BookingLineId",'0') AS "BookingLineId"
			,IFNULL(B."LineId",0) AS "LineId"
			,A."U_BOOKINGID" AS "BookingDocEntry"
			,A."DocEntry" AS "ConfirmBookingID"
			,E."Name" AS "ImportType"
			,IFNULL(F."descript"||' - '||FF."descript",'') AS "Route"
			,IFNULL((SELECT T0."WhsCode" FROM EW_PRD_TEST."OWHS" AS T0 WHERE  T0."U_TerritoryID"=FF."territryID"),'') AS "Destination"
			,G."SlpName" AS "SalePerson"
			,IFNULL((SELECT STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS T1 ON T1."Code"=T0."U_THAIBORDER"
				WHERE T0."DocEntry"=D."DocEntry"),'') AS "ThaiBorder"
			,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" WHERE "AbsEntry"=A."U_PROJECTMANAGEMENTID") AS "JobNumber"
			,IFNULL((SELECT STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACEOFLOADING"
				WHERE T0."DocEntry"=D."DocEntry"),'') AS "PlaceOfLoading"
			,IFNULL((SELECT STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
				WHERE T0."DocEntry"=D."DocEntry"),'') AS "Shipper"
			,IFNULL((SELECT STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACEOFDELIVERY"
				WHERE T0."DocEntry"=D."DocEntry"),'') AS "PlaceOfDelivery"
			,IFNULL((SELECT STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
				WHERE T0."DocEntry"=D."DocEntry"),'') AS "Consignee"
			,IFNULL(A."U_CSName",'') AS "CSName"
			,IFNULL(TO_VARCHAR(D."U_LOADINGDATE",'DD/MM/YYYY'),'') AS "LoadingDate"
			,IFNULL(TO_VARCHAR(D."U_CROSSBORDERDATE",'DD/MM/YYYY'),'') AS "CrossBorderDate"
			,IFNULL(TO_VARCHAR(D."U_ETAREQUIREMENT",'DD/MM/YYYY'),'') AS "ETARequirementDate"
		FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS A
		LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OLCT" AS C ON C."Code"=B."U_TRUCKPROVINCE"
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS D ON D."DocEntry"=A."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS E ON E."Code"=D."U_IMPORTTYPE"
		LEFT JOIN EW_PRD_TEST."OTER" AS F ON F."territryID"=D."U_ORIGIN" --Origin
		LEFT JOIN EW_PRD_TEST."OTER" AS FF ON FF."territryID"=D."U_DESTINATION" --Destination
		LEFT JOIN EW_PRD_TEST."OSLP" AS G ON G."SlpCode"=D."U_SALEEMPLOYEE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='ListBookingSheet' THEN
		IF :par1='ConfirmBookingSheetSeaAndAir' THEN
			SELECT 
				  A."DocEntry" AS BOOKINGID
				 ,CASE WHEN IFNULL(A."U_JOBNO",'')!='' THEN
						IFNULL(B."U_JOBTYPE",'')||A."U_JOBNO"
					 ELSE 
						CASE WHEN A."CreateDate"<'2024-03-06' THEN
					 	CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "U_BOOKINGID"=A."DocEntry" AND "Status"='O' AND "CreateDate">='2024-03-06')=0 THEN
						 		IFNULL(B."U_JOBTYPE",'')||(SELECT TO_VARCHAR(A."U_BOOKINGDATE",'YYMM')
							 	||CASE WHEN LENGTH(IFNULL(A."DocEntry",1))>=4 
									THEN 
										CAST(A."DocEntry" AS NVARCHAR(100))
									ELSE 
										CASE WHEN LENGTH(IFNULL(A."DocEntry",1))=1
											THEN
										  		'000'||CAST(IFNULL(A."DocEntry",1) AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=2 THEN
										  		'00'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=3 THEN
										  		'0'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	END
								END FROM dummy)
						 	ELSE
						 		(SELECT "NAME"
						 			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
						 			LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
						 		 WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O' AND T0."CreateDate">='2024-03-06') 
						 	END
						  ELSE IFNULL(B."U_JOBTYPE",'')||A."DocNum" END
					 END AS JOBNO
				 ,IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY'),'') AS LOADINGDATE
				 ,IFNULL(TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY'),'') AS BOOKINGDATE
				 ,IFNULL(TO_VARCHAR(A."U_CROSSBORDERDATE",'DD/MM/YYYY'),'') AS "CrossBorderDate"
				 ,IFNULL(TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY'),'') AS "ETARequirementDate"
				 ,B."Name" AS IMPORTYPE
				 ,'' AS CO
				 ,'' AS COCODE
				 ,DD."descript"||'-'||D."descript" AS ROUTE
				 ,'' AS "DESTINATION" --(SELECT T0."WhsCode" FROM EW_PRD_TEST."OWHS" AS T0 WHERE  T0."U_TerritoryID"=D."territryID")
				 ,'' AS "ORIGIN" --(SELECT T0."WhsCode" FROM EW_PRD_TEST."OWHS" AS T0 WHERE  T0."U_TerritoryID"=DD."territryID")
				 ,F."SlpName" AS SALENAME
				 ,A."U_SALEEMPLOYEE" AS "SlpCode"
				 ,E."Code" AS CREATEBY
				 ,(SELECT STRING_AGG(T1."CardName",' - ') 
				 	FROM EW_PRD_TEST."@TBSHIPPER" AS T0 
				 	LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
				 	WHERE T0."DocEntry"=A."DocEntry") AS "Shipper"
				 ,(SELECT STRING_AGG(REPLACE_REGEXPR('''' IN T1."CardName" WITH ',' OCCURRENCE ALL),' - ')
				 	FROM EW_PRD_TEST."@TBCONSIGNEE" AS T0 
				 	LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
				 	WHERE T0."DocEntry"=A."DocEntry") AS "Consignee"
			FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			--LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=CASE WHEN A."U_DESTINATION"='None' THEN '0' ELSE A."U_DESTINATION" END
			LEFT JOIN EW_PRD_TEST."OTER" AS DD ON TO_VARCHAR(DD."territryID")=CASE WHEN A."U_ORIGIN"='None' THEN '0' ELSE A."U_ORIGIN" END
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=A."U_CreateBy"
			LEFT JOIN EW_PRD_TEST."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
			WHERE   A."U_CreateBy"=CASE WHEN '-1'='-1' THEN A."U_CreateBy" ELSE A."U_CreateBy" END 
					AND A."DocEntry" NOT IN (SELECT IFNULL(AA."U_BOOKINGID",0) FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA WHERE AA."Status"='O')
					AND A."Status"!='C' 
					/*AND (SELECT COUNT(*) 
								FROM EW_PRD_TEST."@TBSALESQUOTATION" T0 
								WHERE T0."DocEntry"=A."DocEntry")!=0*/
			ORDER BY A."DocEntry" DESC;	
		ELSE IF :par1='ConfirmBookingSheetJobSheet' THEN
			SELECT 
				  AA."DocEntry" AS BOOKINGID
				  ,F."NAME" AS JOBNO
				 ,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				 ,B."Name" AS IMPORTYPE
				 ,/*C."CardName"*/'' AS CO
				 ,D."descript" AS ROUTE
				 ,E."Code" AS CREATEBY
			FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS AA ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS BB ON BB."U_BOOKINGID"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=A."U_DESTINATION"
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=A."U_CreateBy"
			LEFT JOIN EW_PRD_TEST."OPMG" AS F ON F."AbsEntry"=AA."U_PROJECTMANAGEMENTID"
			WHERE   A."U_CreateBy"=CASE WHEN '-1'='-1' THEN A."U_CreateBy" ELSE A."U_CreateBy" END 
					AND A."DocEntry" IN (SELECT IFNULL(AA."U_BOOKINGID",0) FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS AA)
					AND A."Status"!='C'
					AND BB."Status"!='C'
					AND F."STATUS"!='N'
					--AND A."DocEntry"='170'
					--AND IFNULL(A."U_SALESORDERDOCNUM",'')!=''
					AND IFNULL((SELECT DISTINCT MAX(TO_VARCHAR(T0."U_SALESORDERDOCNUM")) FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry" AND T0."Status"='O'),'')=''
					ORDER BY AA."DocEntry" DESC;
		END IF;				 
		END IF;
	ELSE IF :DTYPE='GetVolumeBookingSheetConfirmBookingEditSeaAndAir' THEN
		SELECT * FROM (
			SELECT DISTINCT
					 CAST(A."U_Qty" AS INT) AS QTY
					,IFNULL(A."U_ContainerType",'') AS VOLUMECODE
					,CAST(A."U_ContainerType" AS VARCHAR) AS VOLUME
					,1 AS CONTAINERNUMBER --B."U_CONTAINERNUMBER"
					,A."DocEntry" AS DOCENTRY
					,A."U_Weight" AS GROSSWEIGHT
					,'C' AS TYPEOFCONTAINER
					,IFNULL(CAST(A."U_ListInvoice" AS VARCHAR),'') AS INVOICELIST
					,A."LineId" AS "LineId"
					,IFNULL(C."LineId",0) AS "ConfirmBookingID"
				FROM EW_PRD_TEST."@TB_CON_S_A" AS A
				LEFT JOIN (
					SELECT 
						 T0."U_BOOKINGID" AS "DocEntry"
						,T1."LineId" AS "LineId"
						,T1."U_BookingLineId" AS "BookingLineId"
						,T0."DocEntry" AS "ConfirmBookingID"
					FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
					LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS T1 ON T1."DocEntry"=T0."DocEntry"
					WHERE IFNULL(T1."U_CONTAINERNO",'')!=''
				)AS C ON C."DocEntry"=A."DocEntry" AND C."BookingLineId"=A."LineId"
				WHERE A."DocEntry"=:par1 --AND A."U_Qty" !=0
				UNION ALL				
				SELECT 
					 CAST(A."U_Qty" AS INT) AS QTY
					,IFNULL(A."U_ContainerType",'') AS VOLUMECODE
					,CAST(A."U_ContainerType" AS VARCHAR) AS VOLUME
					,1 AS CONTAINERNUMBER
					,A."DocEntry" AS DOCENTRY
					,A."U_Weight" AS GROSSWEIGHT
					,'T' AS TYPEOFCONTAINER
					,IFNULL(A."U_ListInvoice",'') AS INVOICELIST
					,A."LineId" AS "LineId"
					,IFNULL(B."LineId",0) AS "ConfirmBookingID"
				FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS A 
				LEFT JOIN (
					SELECT 
						 T0."U_BOOKINGID" AS "DocEntry"
						,T1."LineId" AS "LineId"
						,T1."U_BookingLineId" AS "BookingLineId"
						,T0."DocEntry" AS "ConfirmBookingID"
					FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
					LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS T1 ON T1."DocEntry"=T0."DocEntry"
					WHERE IFNULL(T1."U_CONTAINERNO",'')=''
				)AS B ON B."DocEntry"=A."DocEntry" AND B."BookingLineId"=A."LineId"
				WHERE A."DocEntry"=:par1
			/*UNION ALL
			SELECT 
					 A."U_Qty" AS QTY
					,A."U_ContainerType" AS VOLUMECODE
					,CAST(A."U_ContainerType" AS VARCHAR) AS VOLUME
					,1 AS CONTAINERNUMBER --B."U_CONTAINERNUMBER"
					,A."DocEntry" AS DOCENTRY
					,A."U_Weight" AS GROSSWEIGHT
					,'C' AS TYPEOFCONTAINER
					,IFNULL(A."U_ListInvoice",'') AS INVOICELIST
					,A."LineId" AS "LineId"
					,IFNULL(C."LineId",0) AS "ConfirmBookingID"
				FROM EW_PRD_TEST."@TB_CON_S_A" AS A
				LEFT JOIN (
					SELECT 
						 T0."U_BOOKINGID" AS "DocEntry"
						,T1."LineId" AS "LineId"
						,T1."U_BookingLineId" AS "BookingLineId"
						,T0."DocEntry" AS "ConfirmBookingID"
					FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
					LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS T1 ON T1."DocEntry"=T0."DocEntry"
					WHERE IFNULL(T1."U_CONTAINERNO",'')=''
				)AS C ON C."DocEntry"=A."DocEntry" AND C."BookingLineId"=A."LineId"
				WHERE A."DocEntry"=:par1 AND C."LineId" IS NOT NULL*/
		)AS A ORDER BY A."ConfirmBookingID";
	ELSE IF :DTYPE='GetDetailBookingSheetSeaAndAir' THEN
		SELECT 
			  A."DocEntry" AS BOOKINGID
			 ,F."SlpName" AS SALEEMPLOYEE
			 ,F."SlpCode" AS "SlpCode"
			 ,'' AS ROUTE --D."descript"
			 ,'' AS JOBNO --A."U_EWSeries"||A."U_JOBNO"
			 ,'' AS ORIGIN --DD."descript"
			 ,'' AS DESTINATION --DDD."descript"
			 ,'' AS SHIPPER --CC."CardName"
			 ,'' AS CONSIGNEE --CCC."CardName"
			 ,'' AS CO
			 ,B."Name" AS IMPORTYPE
			 ,A."U_REMARKLOLOYARD" AS LOLOYARDREMARK
			 ,A."U_LOLOYARD_UNLOADING" AS LOLOUNLOADING
			 ,A."U_LCL_FCL_CY_CFS" AS LCLORFCL
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
		--LEFT JOIN EW_PRD_TEST."UFD1" AS B ON B."FldValue"=A."U_IMPORTTYPE" AND B."FieldID"='1' AND B."TableID"='@BOOKINGSHEET'
		LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
		--LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."U_CO"
		--LEFT JOIN EW_PRD_TEST."OCRD" AS CC ON CC."CardCode"=A."U_SHIPPER"
		--LEFT JOIN EW_PRD_TEST."OCRD" AS CCC ON CCC."CardCode"=A."U_CONSIGNEE"
		--LEFT JOIN EW_PRD_TEST."OTER" AS D ON D."territryID"=A."U_DESTINATION"
		--LEFT JOIN EW_PRD_TEST."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
		--LEFT JOIN EW_PRD_TEST."OTER" AS DDD ON DDD."territryID"=A."U_DESTINATION"
		LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=A."U_CreateBy"
		LEFT JOIN EW_PRD_TEST."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetViewDetail_Sea_Air' THEN
		IF :par1='GetHeaderResponse' THEN
			SELECT 
				 CAST(A."DocEntry" AS VARCHAR(30)) AS "BookingID"
				,CASE WHEN IFNULL(A."U_JOBNO",'')!='' THEN
						IFNULL(B."U_JOBTYPE",'')||A."U_JOBNO"
					 ELSE 
						CASE WHEN A."CreateDate"<'2024-03-06' THEN
					 	CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "U_BOOKINGID"=A."DocEntry" AND "Status"='O' AND "CreateDate">='2024-03-06')=0 THEN
						 		IFNULL(B."U_JOBTYPE",'')||(SELECT TO_VARCHAR(A."U_BOOKINGDATE",'YYMM')
							 	||CASE WHEN LENGTH(IFNULL(A."DocEntry",1))>=4 
									THEN 
										CAST(A."DocEntry" AS NVARCHAR(100))
									ELSE 
										CASE WHEN LENGTH(IFNULL(A."DocEntry",1))=1
											THEN
										  		'000'||CAST(IFNULL(A."DocEntry",1) AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=2 THEN
										  		'00'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=3 THEN
										  		'0'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	END
								END FROM dummy)
						 	ELSE
						 		(SELECT "NAME"
						 			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
						 			LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
						 		 WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O' AND T0."CreateDate">='2024-03-06') 
						 	END
						  ELSE IFNULL(B."U_JOBTYPE",'')||A."DocNum" END
					 END AS "JobNo"
				,TO_VARCHAR(A."U_BOOKINGDATE",'yyyy-MM-dd') AS "BookingDate"
				,IFNULL(DD."descript",'')||'-'|| IFNULL(D."descript",'') AS "Route"
				,C."SlpName" AS "SalePerson"
				,TO_VARCHAR(C."SlpCode") AS "SalePersonCode"
				,IFNULL(F."Name",'') AS "JobType"
				,IFNULL(G."Name",'') AS "ServiceType"
				,IFNULL(G."Code",'') AS "ServiceTypeCode"
				,IFNULL(A."U_BOOKINGNO",'') AS "BookingNo"
				,IFNULL(A."U_FREIGHT",'') AS "Freight"
				,IFNULL(A."U_FEEDER",'') AS "Feeder"
				,IFNULL(A."U_FEEDERVOY",'')AS "FeederVoy"
				,IFNULL(A."U_VESSEL",'') AS "Vessel"
				,IFNULL(A."U_VESSELVOY",'') AS "VesselVoy"
				,IFNULL(A."U_GOODSDESCRIPTION",'') AS "GoodsDescription"
				,IFNULL(A."U_TOTALPACKAGE",'') AS "TotalPackage"
				,CAST(TO_DECIMAL(A."U_NETWEIGHT",12,2) AS double) AS "NetWeight"
				,CAST(A."U_GROSSWEIGHT" AS double) AS "GrossWeight"
				,IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'yyyy-MM-dd'),'') AS "LoadingDate"
				,IFNULL(TO_VARCHAR(A."U_CROSSBORDERDATE",'yyyy-MM-dd'),'') AS "CrossBorderDate"
				,IFNULL(TO_VARCHAR(A."U_ETAREQUIREMENT",'yyyy-MM-dd'),'') AS "ETARequirement"
				,IFNULL(A."U_REMARKLOLOYARD",'') AS "RemarkLOLOYard"
				,IFNULL(A."U_LOLOYARD_UNLOADING",'') AS "LOLOYARD_UNLOADING"
				,CAST(A."U_CBM" AS varchar(254)) AS "CBM"
				,IFNULL(TO_VARCHAR(A."U_CYDATE",'yyyy-MM-dd'),'') AS "CYDate"
				,IFNULL(A."U_LCL_FCL_CY_CFS",'') AS "LCL_FCL_CY_CFS"
				,IFNULL(TO_VARCHAR(A."U_RETURNDATE",'yyyy-MM-dd'),'') AS "ReturnDate"
				,IFNULL(TO_VARCHAR(A."U_LASTRETURNDATE",'yyyy-MM-dd'),'') AS "LastReturnDate"
				,IFNULL(TO_VARCHAR(CAST(A."U_RETURNBEFORE" AS TIME),'HH:MI AM'),'') AS "ReturnBefore"
				,IFNULL(A."U_DOCUMENTREQUIREMENT",'') AS "DocumentRequest"
				,IFNULL(A."U_SPECIALREQUEST",'') AS "SpecialRequest"
				,IFNULL(A."U_MAWB",'') AS "MAWB"
				,IFNULL(TO_VARCHAR(A."U_LoadingDateAir",'yyyy-MM-dd'),'') AS "LoadingDateAir"
				,IFNULL(TO_VARCHAR(CAST(A."U_CuffOfTimeAir" AS TIME),'HH:MI AM'),'') AS "CutOffTime"
				,IFNULL(TO_VARCHAR(A."U_CuffOfDateAir",'yyyy-MM-dd'),'') AS "CutOffDate"
				,IFNULL(A."U_LoadingPlaceAir",'') AS "LoadingPlaceAir"
				,IFNULL(A."U_Warehouse",'') AS "Warehouse"
				,IFNULL(A."U_Contact",'') AS "Contact"
				,IFNULL(A."U_Address",'') AS "Address"
				,IFNULL(A."U_Tel",'') AS "Tel"
				,IFNULL(TO_VARCHAR(A."U_ETDREQUIREMENT",'yyyy-MM-dd'),'') AS "ETDREQUIREMENT"
				,IFNULL(TO_VARCHAR(A."U_CLOSINGDATE",'yyyy-MM-dd'),'') AS "CLOSINGDATE"
				,IFNULL(TO_VARCHAR(CAST(A."U_CLOSINGTIME" AS TIME),'HH:MI AM'),'') AS "CLOSINGTIME"
				,IFNULL(F."Code",'') AS "EWSeries"
				,IFNULL(A."U_ORIGIN",'') AS "Origin"
				,IFNULL(A."U_DESTINATION",'') AS "Destination"
				,IFNULL(A."U_DG",'') AS "DG"
			FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."OSLP" AS C ON C."SlpCode"=A."U_SALEEMPLOYEE"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON CAST(D."territryID" AS VARCHAR(25))=IFNULL(A."U_DESTINATION",'')
			LEFT JOIN EW_PRD_TEST."OTER" AS DD ON CAST(DD."territryID" AS VARCHAR(25))=IFNULL(A."U_ORIGIN",'')
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=A."U_CreateBy"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS F ON F."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."@TBSERVICETYPE" AS G ON G."Code"=A."U_SERVICETYPE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetCOLoaderSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "COLOADER" --A."U_COLOADER"||'-'||
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBCOLOADER" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_COLOADER"
			WHERE A."DocEntry"=:par2; 
		ELSE IF :par1='GetCustomerSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "CUSTOMERCODE" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBCUSTOMER" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CUSTOMERCODE"
			WHERE A."DocEntry"=:par2; 
		ELSE IF :par1='GetShipperSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "SHIPPER" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_SHIPPER"
			WHERE A."DocEntry"=:par2; 
		ELSE IF :par1='GetConsigneeSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "CONSIGNEE" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
			WHERE A."DocEntry"=:par2; 
		ELSE IF :par1='GetShippingLineSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "SHIPPINGLINE" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBSHIPPINGLINE" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_SHIPPINGLINE"
			WHERE A."DocEntry"=:par2; 
		ELSE IF :par1='GetDestAgentSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "DESTAGENT" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBDESTAGENT" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_DESTAGENT"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetCommoditiesSeaAndAirResponse' THEN
			SELECT 
				IFNULL(A."U_Invoice",'') AS "Invoice" 
			FROM EW_PRD_TEST."@COMMODITY_SEA_AIR" AS A 
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetOverseaTransportSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "OVEARSEATRANSPORT" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TBOVERSEATRANSPORT" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_OVEARSEATRANSPORT"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetPortOfReceiptSeaAndAirResponse' THEN
			SELECT 
				IFNULL(B."Name"||','||B."U_COUNTRY",'') AS "PORTOFRECEIPT" 
				,IFNULL(B."Code",'') AS "Code"
			FROM EW_PRD_TEST."@TBPORTOFRECEIPT" AS A 
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PORTOFRECEIPT"
			WHERE A."DocEntry"=:par2; 
		ELSE IF :par1='GetPortOfDischargeSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."Name",'')||','||IFNULL(B."U_COUNTRY",'') AS "PORTOFDISCHARGE"
				,IFNULL(B."Code",'') AS "Code"
			FROM EW_PRD_TEST."@TBPORTOFDISCHARGE" AS A 
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PORTOFDISCHARGE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetPlaceOfLoadingSeaAndAirResponse' THEN
			SELECT 
				IFNULL(B."Name"||','||B."U_COUNTRY",'') AS "PLACEOFLOADING" 
				,IFNULL(B."Code",'') AS "Code"
			FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS A 
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFLOADING"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetPlaceOfDeliverySeaAndAirResponse' THEN
			SELECT 
				IFNULL(B."Name",'')||','||IFNULL(B."U_COUNTRY",'') AS "PLACEOFDELIVERY" 
				,IFNULL(B."Code",'') AS "Code"
			FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS A 
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFDELIVERY"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetThaiForwarderSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."CardName",'') AS "THAIFORWARDER" 
				,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TB_TFW_SEA_AIR" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_THAIFORWARDER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetContainerTypeSeaAndAirResponse' THEN
			SELECT 
				 CAST(A."U_Qty" AS INT) AS "Qty"
				,IFNULL(A."U_ContainerType",'') AS "ContainerType"
				,CAST(A."U_Weight" AS INT) AS "Weight"
				,IFNULL(A."U_ListInvoice",'') AS "ListInvoice"
			FROM EW_PRD_TEST."@TB_CON_S_A" AS A
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetOverSeaForwarderSeaAndAirResponse' THEN
			SELECT 
				  IFNULL(B."CardName",'') AS "OVERSEAFORWARDER"
				 ,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TB_OSFW_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_OVERSEAFORWARDER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetThaiBorderSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(B."Name",'') AS "THAIBORDER"
				 ,B."Code" AS "Code"
			FROM EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS B ON B."Code"=A."U_THAIBORDER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetTruckTypeSeaAndAirResponse' THEN
			SELECT 
				 CAST(A."U_Qty" AS INT) AS "Qty"
				,IFNULL(A."U_ContainerType",'') AS "ContainerType"
				,CAST(A."U_Weight" AS INT) AS "Weight"
				,IFNULL(A."U_ListInvoice",'') AS "ListInvoice"
			FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS A
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetCYAt_ContactSeaAndAirResponse' THEN
			SELECT 
				  IFNULL(A."U_CYAt_Contact",'') AS "CYAt_Contact"
				 ,IFNULL(A."U_CYAt_Contact",'') AS "CardCode"
			FROM EW_PRD_TEST."@TB_CY_AT_S_A" AS A
			--LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CYAt_Contact"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetReturnAt_ContactSeaAndAirResponse' THEN
			SELECT 
				  IFNULL(A."U_ReturnAt_Contact",'') AS "ReturnAt_Contact"
				 ,IFNULL(A."U_ReturnAt_Contact",'') AS "CardCode"
			FROM EW_PRD_TEST."@TB_RETURNAT_S_A" AS A
			--LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_ReturnAt_Contact"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetLoadingAtSeaAndAirResponse' THEN
			SELECT 
				  IFNULL(B."CardName",'') AS "LoadingAt"
				 ,IFNULL(B."CardCode",'') AS "CardCode"
			FROM EW_PRD_TEST."@TB_LOADINGAT_S_A" AS A
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_LoadingAt"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetFlightsInformationSeaAndAirResponse' THEN
			SELECT 
				 IFNULL(A."U_FlightFromTo",'') AS "FlightFromTo"
				,IFNULL(A."U_FlightNo",'') AS "FlightNo"
				,IFNULL(A."U_FlightArrival",'') AS "FlightArrival"
			FROM EW_PRD_TEST."@TB_FLY_INFO" AS A
			WHERE A."DocEntry"=:par2 ORDER BY A."LineId" ASC;
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
	ELSE IF :DTYPE='GetListBookingSheetSea_Air' THEN
		IF:par3='ALL' THEN
				SELECT
					  CAST(A."DocEntry" AS VARCHAR(50)) AS "BookingID"
					,CASE WHEN IFNULL(A."U_JOBNO",'')!='' THEN
						IFNULL(B."U_JOBTYPE",'')||A."U_JOBNO"
					 ELSE 
						CASE WHEN A."CreateDate"<'2024-03-06' THEN
					 	CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "U_BOOKINGID"=A."DocEntry" AND "Status"='O' AND "CreateDate">='2024-03-06')=0 THEN
						 		IFNULL(B."U_JOBTYPE",'')||(SELECT TO_VARCHAR(A."U_BOOKINGDATE",'YYMM')
							 	||CASE WHEN LENGTH(IFNULL(A."DocEntry",1))>=4 
									THEN 
										CAST(A."DocEntry" AS NVARCHAR(100))
									ELSE 
										CASE WHEN LENGTH(IFNULL(A."DocEntry",1))=1
											THEN
										  		'000'||CAST(IFNULL(A."DocEntry",1) AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=2 THEN
										  		'00'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=3 THEN
										  		'0'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	END
								END FROM dummy)
						 	ELSE
						 		(SELECT "NAME"
						 			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
						 			LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
						 		 WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O' AND T0."CreateDate">='2024-03-06') 
						 	END
						  ELSE IFNULL(B."U_JOBTYPE",'')||A."DocNum" END
					 END AS "JobNo"
					,TO_VARCHAR(A."U_BOOKINGDATE",'dd/MM/YYYY') AS "BookingDate"
					,IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'dd/MM/YYYY'),'') AS "LoadingDate"
					,IFNULL(B."Name",'') AS "ImportType"
					,'' AS CO --C."CardName"
					,IFNULL(DD."descript",'')||'-'|| IFNULL(D."descript",'') AS "Route"
					,E."Code" AS "CreateBy"
					,CASE WHEN A."Status"='C' THEN 
						 	'Cancel'
						  	WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')>=1 THEN
						 		'CONFIRM BOOKING SHEET'
						 	ELSE
						 		'BOOKING SHEET'
						  	END AS "DocStatus"
					,CASE WHEN A."U_CreateBy" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_CANCEL" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR A."U_CreateBy"=:par4 THEN
						CASE WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')>=1 THEN
						 	'C'
						 ELSE
							A."Status"
						END
					 ELSE 'C' END AS "DocStatusCancel"
					,CASE WHEN A."Status"='C' THEN 
						 	'C'
						  WHEN A."Status"='O' THEN
						  	CASE WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')>=1 THEN
						 		CASE WHEN A."U_CreateBy" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR A."U_CreateBy"=:par4 THEN
					  				'I'
					  			 ELSE 'C' END
						 	ELSE
						 		CASE WHEN A."U_CreateBy" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR A."U_CreateBy"=:par4 THEN
					  				'O'
					  			 ELSE 'C' END
						  	END END AS "StatusUpdate"
					,CAST(A."DocEntry" AS VARCHAR(50)) AS "DocEntry"
					,IFNULL((SELECT STRING_AGG(T1."CardName",' - ') 
						FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0 
						LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
						WHERE T0."DocEntry"=A."DocEntry"),'') AS "Shipper"
					,IFNULL((SELECT STRING_AGG(T1."CardName",' - ') 
						FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS T0 
						LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
						WHERE T0."DocEntry"=A."DocEntry"),'') AS "Consignee"
					,(SELECT IFNULL(STRING_AGG("U_ContainerType",','),'') FROM (SELECT Distinct T0."U_ContainerType" 
						FROM EW_PRD_TEST."@TB_CON_S_A" AS T0 
						WHERE T0."DocEntry"=A."DocEntry")) AS "Volumn"
					,(SELECT IFNULL(STRING_AGG("U_ContainerType",','),'')  FROM (SELECT DISTINCT T0."U_ContainerType"
						FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS T0 
						WHERE T0."DocEntry"=A."DocEntry")) AS "ContainerType"
					,IFNULL(A."U_UpdateBy",'') AS "UpdateBy"
					,(SELECT IFNULL(STRING_AGG(T0."U_Invoice",','),'') FROM EW_PRD_TEST."@COMMODITY_SEA_AIR" AS T0 WHERE T0."DocEntry"=A."DocEntry") AS "COMMODITYS"
					--,F."SlpName" AS "SaleEmployee"
				FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
		--	LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON CAST(D."territryID" AS VARCHAR(25))=IFNULL(A."U_DESTINATION",'')
			LEFT JOIN EW_PRD_TEST."OTER" AS DD ON CAST(DD."territryID" AS VARCHAR(25))=IFNULL(A."U_ORIGIN",'')
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=A."U_CreateBy"
			LEFT JOIN EW_PRD_TEST."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
				WHERE A."U_BOOKINGDATE" BETWEEN :par1 AND :par2 
				AND (A."U_CreateBy"=:par4
					OR A."U_CreateBy" IN (
						SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_READ" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
						))
				ORDER BY A."DocEntry" DESC;
			ELSE IF :par3='DEFAULT' THEN
				SELECT TOP 1000
				    CAST(A."DocEntry" AS VARCHAR(50)) AS "BookingID"
					,CASE WHEN IFNULL(A."U_JOBNO",'')!='' THEN
						IFNULL(B."U_JOBTYPE",'')||A."U_JOBNO"
					 ELSE 
						CASE WHEN A."CreateDate"<'2024-03-06' THEN
					 	CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "U_BOOKINGID"=A."DocEntry" AND "Status"='O' AND "CreateDate">='2024-03-06')=0 THEN
						 		IFNULL(B."U_JOBTYPE",'')||(SELECT TO_VARCHAR(A."U_BOOKINGDATE",'YYMM')
							 	||CASE WHEN LENGTH(IFNULL(A."DocEntry",1))>=4 
									THEN 
										CAST(A."DocEntry" AS NVARCHAR(100))
									ELSE 
										CASE WHEN LENGTH(IFNULL(A."DocEntry",1))=1
											THEN
										  		'000'||CAST(IFNULL(A."DocEntry",1) AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=2 THEN
										  		'00'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	WHEN LENGTH(A."DocEntry")=3 THEN
										  		'0'||CAST(A."DocEntry" AS NVARCHAR(100))
										  	END
								END FROM dummy)
						 	ELSE
						 		(SELECT "NAME"
						 			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0
						 			LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
						 		 WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O' AND T0."CreateDate">='2024-03-06') 
						 	END
						  ELSE IFNULL(B."U_JOBTYPE",'')||A."DocNum" END
					 END AS "JobNo"
					,TO_VARCHAR(A."U_BOOKINGDATE",'dd/MM/YYYY') AS "BookingDate"
					,IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'dd/MM/YYYY'),'') AS "LoadingDate"
					,IFNULL(B."Name",'') AS "ImportType"
					,'' AS CO --C."CardName"
					,IFNULL(DD."descript",'')||'-'|| IFNULL(D."descript",'') AS "Route"
					,E."Code" AS "CreateBy"
					,CASE WHEN A."Status"='C' THEN 
						 	'Cancel'
						  	WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')>=1 THEN
						 		'CONFIRM BOOKING SHEET'
						 	ELSE
						 		'BOOKING SHEET'
						  	END AS "DocStatus"
					,CASE WHEN A."U_CreateBy" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_CANCEL" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR A."U_CreateBy"=:par4 THEN
						CASE WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')>=1 THEN
						 	'C'
						ELSE
							A."Status"
						END
					 ELSE 'C' END AS "DocStatusCancel"
					,CASE WHEN A."Status"='C' THEN 
						 	'C'
						  WHEN A."Status"='O' THEN
						  	CASE WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')>=1 THEN
						 		CASE WHEN A."U_CreateBy" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR A."U_CreateBy"=:par4 THEN
					  				'I'
					  			 ELSE 'C' END
						 	ELSE
						 		CASE WHEN A."U_CreateBy" IN (
										SELECT "U_User" 
										FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
										LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									) OR A."U_CreateBy"=:par4 THEN
					  				'O'
					  			 ELSE 'C' END
						  	END END AS "StatusUpdate"
					,CAST(A."DocEntry" AS VARCHAR(50)) AS "DocEntry"
					,IFNULL((SELECT STRING_AGG(T1."CardName",' - ') 
						FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0 
						LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
						WHERE T0."DocEntry"=A."DocEntry"),'') AS "Shipper"
					,IFNULL((SELECT STRING_AGG(T1."CardName",' - ') 
						FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS T0 
						LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
						WHERE T0."DocEntry"=A."DocEntry"),'') AS "Consignee"
					,(SELECT IFNULL(STRING_AGG("U_ContainerType",','),'') FROM (SELECT Distinct T0."U_ContainerType" 
						FROM EW_PRD_TEST."@TB_CON_S_A" AS T0 
						WHERE T0."DocEntry"=A."DocEntry")) AS "Volumn"
					,(SELECT IFNULL(STRING_AGG("U_ContainerType",','),'')  FROM (SELECT DISTINCT T0."U_ContainerType"
						FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS T0 
						WHERE T0."DocEntry"=A."DocEntry")) AS "ContainerType"
					,IFNULL(A."U_UpdateBy",'') AS "UpdateBy"
					,(SELECT IFNULL(STRING_AGG(T0."U_Invoice",','),'') FROM EW_PRD_TEST."@COMMODITY_SEA_AIR" AS T0 WHERE T0."DocEntry"=A."DocEntry") AS "COMMODITYS"
					--,F."SlpName" AS "SaleEmployee"
				FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
		--	LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON CAST(D."territryID" AS VARCHAR(25))=IFNULL(A."U_DESTINATION",'')
			LEFT JOIN EW_PRD_TEST."OTER" AS DD ON CAST(DD."territryID" AS VARCHAR(25))=IFNULL(A."U_ORIGIN",'')
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=A."U_CreateBy"
			LEFT JOIN EW_PRD_TEST."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
				WHERE A."U_BOOKINGDATE" BETWEEN :par1 AND :par2 
				AND (A."U_CreateBy"=:par4
					OR A."U_CreateBy" IN (
						SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_READ" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
						))
				ORDER BY A."DocEntry" DESC;--:par1='-1'
			END IF;
			END IF;
	ELSE IF :DTYPE='GETORIGIN_SEA_AIR' THEN
		SELECT A."territryID" AS "Code"
		      ,A."descript" AS "Name" 
		FROM EW_PRD_TEST."OTER" AS A 
		WHERE "parent"=8;
	ELSE IF :DTYPE='GETDESTINATION_SEA_AIR' THEN
		SELECT 
			  A."territryID" AS "Code"
			  ,A."descript" AS "Name" 
		FROM EW_PRD_TEST."OTER" AS "A" WHERE "parent"=8;
	ELSE IF :DTYPE='GetJobNumberSeaAndAir' THEN
		SELECT 
			 A."Code" AS "Code"
			,A."U_JOBTYPE" AS "Name"
			,A."Name" AS "JobTypeName"
		FROM EW_PRD_TEST."@TBJOBTYPE" AS A
		--LEFT JOIN EW_PRD_TEST."@TBSERVICETYPE" AS B ON A."Code"=B."U_JOBTYPE" 
		WHERE "U_DEPARTMENT"IN ('SEA','AIR');
	ELSE IF :DTYPE='GetCOLoaderSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A" 
		WHERE LEFT(A."CardCode",2)='CO';
	ELSE IF :DTYPE='GetCustomerSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A" 
		WHERE A."CardType"='C' AND A."U_BP_TYPE"='C';
	ELSE IF :DTYPE='GetShippingLineSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A" WHERE LEFT(A."CardCode",2)='SL';
	ELSE IF :DTYPE='Get_DEST_AGENTSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A"
		WHERE A."CardType"='S';
	ELSE IF :DTYPE='GetPortOfReceiptSeaAndAir' THEN
		SELECT A."Code" AS "Code"
			  ,A."Name"||', '||A."U_COUNTRY" AS "Name"
		 	  ,A."U_COUNTRY" AS "Country"
		FROM EW_PRD_TEST."@TBPLACEOFLOADING" AS A;
	ELSE IF :DTYPE='GetPortOfDischargeSeaAndAir' THEN
		SELECT A."Code" AS "Code"
			  ,A."Name"||', '||A."U_COUNTRY" AS "Name"
			  ,A."U_COUNTRY" AS "Country"
		FROM EW_PRD_TEST."@TBPLACEOFLOADING" AS A;
	ELSE IF :DTYPE='GetCYAtOrContactPersonSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A";
	ELSE IF :DTYPE='GetReturnAtOrContactPersonSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A"; 
	ELSE IF :DTYPE='GetLoadingAtSeaAndAir' THEN
		SELECT 
			 "CardCode" AS "CustomerCode"
			,IFNULL("CardName",'') AS "CustomerName" 
		FROM EW_PRD_TEST."OCRD" AS "A"; 
	ELSE IF :DTYPE='GetConfirmBookingSheetDetail' THEN
		SELECT 
			  AA."DocEntry" AS "BOOKINGID"
			 ,A."DocEntry" AS "CONFIRMBOOKINGID"
			 ,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" WHERE "AbsEntry"=A."U_PROJECTMANAGEMENTID")AS "JOBNO"
			 ,DD."descript"||'-'||D."descript" AS "ROUTE"
			 ,TO_VARCHAR (AA."U_BOOKINGDATE", 'DD/MM/YYYY') AS "BOOKINGDATE"
			 ,F."SlpName" AS "SALEEMPLOYEE"
			 ,B."Name" AS "IMPORTTYPE"
			 ,'' AS "SHIPPER"
			 ,IFNULL((SELECT "U_DESTAGENT" FROM EW_PRD_TEST."@TBDESTAGENT" WHERE "DocEntry"=AA."DocEntry"),'') AS "ShippingLine"
			 ,AA."U_TOTALPACKAGE" AS "TOTALPACKAGE"
			 ,(SELECT 
					STRING_AGG(T3."CardName",',') AS CARDNAME
				FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0
				LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS TT0 ON TT0."DocEntry"=T0."U_CONFIRMBOOKINGSHEETID"
				LEFT JOIN EW_PRD_TEST."@TBSALESQUOTATION" AS T1 ON T1."DocEntry"=T0."DocEntry"
				LEFT JOIN EW_PRD_TEST."OQUT" AS T2 ON T1."U_DOCENTRY"=T2."DocEntry"
				LEFT JOIN EW_PRD_TEST."OCRD" AS T3 ON T3."CardCode"=T2."CardCode"
				WHERE T0."U_CONFIRMBOOKINGSHEETID"=A."DocEntry" 
				AND T2."CardCode" /*NOT*/ IN 
										(SELECT 
											Z1."CardCode"
										FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS Z0 
										LEFT JOIN EW_PRD_TEST."ORDR" AS Z1 ON Z1."DocEntry"=Z0."U_SALESORDERDOCNUM"
										WHERE Z0."U_CONFIRMBOOKINGID"=A."DocEntry")) AS "CO"
			 ,''/*C."CardCode"*/ AS "COCODE"
			 ,AA."U_NETWEIGHT" AS "NETWEIGHT"
			 ,AA."U_GROSSWEIGHT" AS "GROSSWEIGHT"
			 ,/*CCC."CardName"*/'' AS "CONSIGNEE"
			 ,TO_VARCHAR (AA."U_LOADINGDATE", 'DD/MM/YYYY') AS "LOADINGDATE"
			 ,IFNULL(TO_VARCHAR (AA."U_CROSSBORDERDATE", 'DD/MM/YYYY'),'') AS "CROSSBORDERDATE"
			 ,TO_VARCHAR (AA."U_ETAREQUIREMENT", 'DD/MM/YYYY') AS "ETAREQUIREMENT"
			 ,(SELECT 
			 	STRING_AGG(Z1."Name", ',') 
			 	FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS Z0 
			 	LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS Z1 ON Z0."U_PLACEOFLOADING"=Z1."Code"
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "PLACEOFLOADING"
			 ,(SELECT 
			 	STRING_AGG(Z1."Name", ',') 
			 	FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS Z0
			 	LEFT JOIN EW_PRD_TEST."@TBPLACEOFDELIVERY" AS Z1 ON Z1."Code"=Z0."U_PLACEOFDELIVERY" 
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "PLACEOFDELIVERY"
			 ,AA."U_GOODSDESCRIPTION" AS "GOODSDESCRIPTION"
			 ,IFNULL((SELECT STRING_AGG(TO_INT(QTY)||'x'||VOLUME, ',') AS VOLUME FROM (
					SELECT 
				 	SUM(Z0."U_Qty") AS QTY,Z1."Name" AS VOLUME 
				 	FROM EW_PRD_TEST."@TB_CON_S_A" AS Z0
				 	LEFT JOIN EW_PRD_TEST."@TBVOLUME" AS Z1 ON Z1."Code"=Z0."U_ContainerType" 
				 	WHERE Z0."DocEntry"=AA."DocEntry" GROUP BY Z1."Name"))||',','')||
				  IFNULL((SELECT STRING_AGG(TO_INT("QTY")||'x'||"TRUCKTYPE", ',') FROM (SELECT 
				 	 SUM(Z0."U_Qty") AS QTY
				 	,Z0."U_ContainerType" AS TRUCKTYPE
				 	FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS Z0 
				 	WHERE Z0."DocEntry"=AA."DocEntry"
				 	GROUP BY Z0."U_ContainerType")
				 ),'') AS "VOLUME"
			 ,'' AS "THAIFORWARDER" --G."Name"
			 ,'' AS "OVERSEAFORWARDER" --H."Name"
			 ,(SELECT 
			 	STRING_AGG(Z1."Name", ',') 
			 	FROM EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS Z0
			 	LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS Z1 ON Z1."Code"=Z0."U_THAIBORDER" 
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "THAIBORDER"
			 ,'' AS "OVERSEATRANSPORT" 
		FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS A
		LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS AA ON AA."U_CONFIRMBOOKINGSHEETID"=A."DocEntry"
		LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=AA."U_IMPORTTYPE"
		--LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=AA."U_CO"
		--LEFT JOIN EW_PRD_TEST."OCRD" AS CC ON CC."CardCode"=AA."U_SHIPPER"
		--LEFT JOIN EW_PRD_TEST."OCRD" AS CCC ON CCC."CardCode"=AA."U_CONSIGNEE"
		LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=AA."U_DESTINATION"
		LEFT JOIN EW_PRD_TEST."OTER" AS DD ON TO_VARCHAR(DD."territryID")=AA."U_ORIGIN"
		LEFT JOIN EW_PRD_TEST."OUSR" AS E ON E."USERID"=AA."U_CreateBy"
		LEFT JOIN EW_PRD_TEST."OSLP" AS F ON F."SlpCode"=AA."U_SALEEMPLOYEE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetConfirmBookingSheetListInvoice' THEN
		SELECT 
			B."U_Invoice" AS CODE
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@COMMODITY_SEA_AIR" AS B ON A."DocEntry"=B."DocEntry"
		WHERE A."U_CONFIRMBOOKINGSHEETID"=:par1;
	ELSE IF :DTYPE='GetConfirmBookingSheetListTruck' THEN
		SELECT
			CASE WHEN A."U_TYPE"='FreeKey' THEN
				A."U_TRUCKPROVINCE"||' '||A."U_TRUCKPLATENO"||' /'||A."U_TRAILERPROVINCE"||' '||A."U_TRAILERPLATE" 
			ELSE 
				BBB."Location"||' '||IFNULL(BB."InventryNo",'')||' /'||A."U_TRAILERPROVINCE"||' '||A."U_TRAILERPLATE" 
			END AS "TRUCKNO" --A."U_TRUCKCODE"
			,B."AttriTxt7" AS "TRUCKWEIGHT"
			,A."U_CONTAINERNO" AS "CONTAINERNO"
			,TO_VARCHAR(A."U_GROSSWEIGHT",'00 KG') AS "CONTAINERWEIGHT"
			,IFNULL(C."U_ShortName",C."CardName") AS "TRANSPORTER"
		FROM EW_PRD_TEST."@TRUCK_INFO_S_A" AS A 
		LEFT JOIN EW_PRD_TEST."ITM13" AS B ON A."U_TRUCKCODE"=B."ItemCode"
		LEFT JOIN EW_PRD_TEST."OITM" AS BB ON A."U_TRUCKCODE"=BB."ItemCode"
		LEFT JOIN EW_PRD_TEST."OLCT" AS BBB ON BBB."Code"=BB."Location"--Location
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."U_VENDORCODE"
		WHERE A."DocEntry"=:par1; --AND IFNULL(A."U_CONTAINERNO",'')!='';
	ELSE IF :DTYPE='GETSHIPPERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS SHIPPER
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS E
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS A ON A."DocEntry"=E."U_CONFIRMBOOKINGSHEETID"
		LEFT JOIN EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS B ON E."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=B."U_SHIPPER"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETCONSIGNEECONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS CONSIGNEE
		FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS A
		LEFT JOIN EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS B ON A."U_BOOKINGID"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=B."U_CONSIGNEE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETTHAIFORWARDERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS FORWARDER
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS E
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS A ON A."DocEntry"=E."U_CONFIRMBOOKINGSHEETID"
		LEFT JOIN EW_PRD_TEST."@TB_TFW_SEA_AIR" AS CC ON E."DocEntry"=CC."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=CC."U_THAIFORWARDER"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETOVERSEAFORWARDERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS FORWARDER
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS E 
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS A ON A."DocEntry"=E."U_CONFIRMBOOKINGSHEETID"
		LEFT JOIN EW_PRD_TEST."@TB_OSFW_SEA_AIR" AS CC ON E."DocEntry"=CC."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=CC."U_OVERSEAFORWARDER"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETTHAIBORDERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."Name" AS ThaiBorder
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS E 
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS A ON A."DocEntry"=E."U_CONFIRMBOOKINGSHEETID"
		LEFT JOIN EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS CC ON E."DocEntry"=CC."DocEntry"
		LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS C ON C."Code"=CC."U_THAIBORDER"
		WHERE A."DocEntry"=:par1;
	/*ELSE IF :DTYPE='GETSALESQUOTATIONCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			 B."U_DOCENTRY" AS DOCENTRY
			,C."NumAtCard" AS REFNO
			,C."CardCode" AS CARDCODE
			,D."CardName" AS CARDNAME
		FROM EW_PRD_TEST."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST."@TBSALESQUOTATION" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OQUT" AS C ON B."U_DOCENTRY"=C."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=C."CardCode"
		WHERE A."U_CONFIRMBOOKINGSHEETID"=:par1;
	END IF;*/
	ELSE IF :DTYPE='GETOverseaTruckerCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS "OverseaTrucker"
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS E 
		LEFT JOIN EW_PRD_TEST."@TBOVERSEATRANSPORT" AS A ON A."DocEntry"=E."U_CONFIRMBOOKINGSHEETID"
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS B ON B."U_BOOKINGID"=A."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."U_OVEARSEATRANSPORT"
		WHERE B."DocEntry"=par1;
	ELSE IF :DTYPE='GetItemDetailByType' THEN
		IF :par2='CBT' THEN
			SELECT 
				 ROW_NUMBER ( ) OVER( ORDER BY A."ItemCode" DESC ) AS ROWNUM
				,A."ItemCode" AS ITEMCODE
				,A."ItemName" AS ITEMNAME
				,IFNULL(E."Price",0) AS SELLINGPRICE
				,IFNULL(D."Price",0) AS COSTINGPRICE
				,'I' AS ITEMTYPE
				,A."UgpEntry" AS "UomEntry"
			FROM EW_PRD_TEST."OITM" AS A
			LEFT JOIN EW_PRD_TEST."OHEM" AS B ON B."empID"=:par1
			LEFT JOIN EW_PRD_TEST."OSLP" AS C ON C."SlpCode"=B."salesPrson"
			LEFT JOIN EW_PRD_TEST."ITM1" AS D ON D."ItemCode"=A."ItemCode" AND D."PriceList"=C."U_PriceList"
			LEFT JOIN EW_PRD_TEST."ITM1" AS E ON E."ItemCode"=A."ItemCode" AND E."PriceList"='1'
			WHERE A."ItmsGrpCod" NOT IN(130) AND A."ItemType" IN('I') AND (A."PrchseItem"='Y' AND A."SellItem"='Y') AND A."U_Dept_item"IN ('CBT','ALL');
		ELSE IF :par2='S&A' THEN
			SELECT 
				 ROW_NUMBER ( ) OVER( ORDER BY A."ItemCode" DESC ) AS ROWNUM
				,A."ItemCode" AS ITEMCODE
				,A."ItemName" AS ITEMNAME
				,IFNULL(E."Price",0) AS SELLINGPRICE
				,IFNULL(D."Price",0) AS COSTINGPRICE
				,'I' AS ITEMTYPE
				,A."UgpEntry" AS "UomEntry"
			FROM EW_PRD_TEST."OITM" AS A
			LEFT JOIN EW_PRD_TEST."OHEM" AS B ON B."empID"=:par1
			LEFT JOIN EW_PRD_TEST."OSLP" AS C ON C."SlpCode"=B."salesPrson"
			LEFT JOIN EW_PRD_TEST."ITM1" AS D ON D."ItemCode"=A."ItemCode" AND D."PriceList"=C."U_PriceList"
			LEFT JOIN EW_PRD_TEST."ITM1" AS E ON E."ItemCode"=A."ItemCode" AND E."PriceList"='1'
			WHERE A."ItmsGrpCod" NOT IN(130) AND A."ItemType" IN('I') AND (A."PrchseItem"='Y' AND A."SellItem"='Y') AND A."U_Dept_item"IN ('SA','SF','ALL');
		ELSE
			SELECT 
				 ROW_NUMBER ( ) OVER( ORDER BY A."ItemCode" DESC ) AS ROWNUM
				,A."ItemCode" AS ITEMCODE
				,A."ItemName" AS ITEMNAME
				,IFNULL(E."Price",0) AS SELLINGPRICE
				,IFNULL(D."Price",0) AS COSTINGPRICE
				,'I' AS ITEMTYPE
				,A."UgpEntry" AS "UomEntry"
			FROM EW_PRD_TEST."OITM" AS A
			LEFT JOIN EW_PRD_TEST."OHEM" AS B ON B."empID"=:par1
			LEFT JOIN EW_PRD_TEST."OSLP" AS C ON C."SlpCode"=B."salesPrson"
			LEFT JOIN EW_PRD_TEST."ITM1" AS D ON D."ItemCode"=A."ItemCode" AND D."PriceList"=C."U_PriceList"
			LEFT JOIN EW_PRD_TEST."ITM1" AS E ON E."ItemCode"=A."ItemCode" AND E."PriceList"='1'
			WHERE A."ItmsGrpCod" NOT IN(130) AND A."ItemType" IN('I') AND (A."PrchseItem"='Y' AND A."SellItem"='Y');
		END IF;
		END IF;
	ELSE IF :DTYPE='GetCurrencyByCardCode' THEN
		IF(SELECT "Currency" FROM EW_PRD_TEST."OCRD" WHERE "CardCode"=:par1)='##' THEN
			SELECT 
				 A."CurrCode" AS "CurrencyCode"
				,B."ChkName" AS "CurrencyName"
				,IFNULL(C."Rate",0) AS "ExchangeRateSystemCurrency" --USS
				,IFNULL(CC."Rate",0) AS "ExchangeRateLocalCurrency" --THS
				,IFNULL(CCC."Rate",0) AS "EXCHANGERATE" --Base Currency
				,A."Locked" AS "Defualt"
			FROM EW_PRD_TEST."CRD13" AS A
			LEFT JOIN EW_PRD_TEST."OCRN" AS B ON A."CurrCode"=B."CurrCode"
			LEFT JOIN EW_PRD_TEST."ORTT" AS C ON C."Currency"='USS' AND C."RateDate"=CURRENT_DATE --USD
			LEFT JOIN EW_PRD_TEST."ORTT" AS CC ON CC."Currency"='THB' AND CC."RateDate"=CURRENT_DATE --THB
			LEFT JOIN EW_PRD_TEST."ORTT" AS CCC ON CCC."Currency"=A."CurrCode" AND CCC."RateDate"=CURRENT_DATE --Currency Base
			WHERE "CardCode"=CASE WHEN :par1='' THEN "CardCode" ELSE :par1 END AND RIGHT(B."CurrCode",1)!='S';
		ELSE
			IF :par1='' THEN
				SELECT
					 B."CurrCode" AS "CurrencyCode"
					,B."ChkName" AS "CurrencyName"
					,IFNULL(C."Rate",0) AS "ExchangeRateSystemCurrency" --USS
					,IFNULL(CC."Rate",0) AS "ExchangeRateLocalCurrency" --THS
					,IFNULL(CCC."Rate",0) AS "EXCHANGERATE" --Base Currency
					,'N' AS "Defualt"
				FROM EW_PRD_TEST."OCRN" AS B 
				LEFT JOIN EW_PRD_TEST."ORTT" AS C ON C."Currency"='USS' AND C."RateDate"=CURRENT_DATE --USD
				LEFT JOIN EW_PRD_TEST."ORTT" AS CC ON CC."Currency"='THB' AND CC."RateDate"=CURRENT_DATE --THB
				LEFT JOIN EW_PRD_TEST."ORTT" AS CCC ON CCC."Currency"=B."CurrCode" AND CCC."RateDate"=CURRENT_DATE --Currency Base
				WHERE RIGHT(B."CurrCode",1)!='S'
				UNION ALL
				SELECT   '##' AS "CurrencyCode"
						,'All Currencies' AS "CurrencyName" 
						,0.00 AS "ExchangeRateSystemCurrency" --USS
						,0.00 AS "ExchangeRateLocalCurrency" --THS
						,0.00 AS "EXCHANGERATE"
						,'Y' AS "Defualt"
				FROM DUMMY;
			ELSE
				SELECT
					 A."Currency" AS "CurrencyCode"
					,B."ChkName" AS "CurrencyName"
					,IFNULL(C."Rate",0) AS "ExchangeRateSystemCurrency" --USS
					,IFNULL(CC."Rate",0) AS "ExchangeRateLocalCurrency" --THS
					,IFNULL(CASE WHEN A."Currency"='THS' THEN 1 ELSE CCC."Rate" END,0) AS "EXCHANGERATE"
					,'Y' AS "Defualt"
				FROM EW_PRD_TEST."OCRD" AS A
				LEFT JOIN EW_PRD_TEST."OCRN" AS B ON A."Currency"=B."CurrCode"
				LEFT JOIN EW_PRD_TEST."ORTT" AS C ON C."Currency"='USS' AND C."RateDate"=CURRENT_DATE --C."Currency"
				LEFT JOIN EW_PRD_TEST."ORTT" AS CC ON CC."Currency"='THB' AND CC."RateDate"=CURRENT_DATE
				LEFT JOIN EW_PRD_TEST."ORTT" AS CCC ON CCC."Currency"=A."Currency" AND CCC."RateDate"=CURRENT_DATE --Currency Base
				WHERE "CardCode"=:par1 ;--AND RIGHT(B."CurrCode",1)!='S';
			END IF;
		END IF;
	ELSE IF :DTYPE='GetVolumeBookingSheetSeaAndAir' THEN
		SELECT 
			 CAST(A."U_Qty" AS INT) AS QTY
			,B."Code" AS VOLUMECODE
			,B."Name" AS VOLUME
			,B."U_CONTAINERNUMBER" AS CONTAINERNUMBER
			,B."DocEntry" AS DOCENTRY
			,A."U_Weight" AS GROSSWEIGHT
			,'C' AS TYPEOFCONTAINER
			,A."U_ListInvoice" AS INVOICELIST
			,A."LineId" AS "LineId"
		FROM EW_PRD_TEST."@TB_CON_S_A" AS A
		LEFT JOIN EW_PRD_TEST."@TBVOLUME" AS B ON B."Code"=A."U_ContainerType"
		WHERE A."DocEntry"=:par1--;
		UNION ALL
		SELECT 
			 CAST(A."U_Qty" AS INT) AS QTY
			,A."U_ContainerType" AS VOLUMECODE
			,A."U_ContainerType" AS VOLUME
			,1 AS CONTAINERNUMBER
			,A."DocEntry" AS DOCENTRY
			,A."U_Weight" AS GROSSWEIGHT
			,'T' AS TYPEOFCONTAINER
			,A."U_ListInvoice" AS INVOICELIST
			,A."LineId" AS "LineId"
		FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS A
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetDetailJobSheetByDocEntry' THEN
		SELECT 
			  TO_VARCHAR(T0."U_CONFIRMBOOKINGID") AS "CONFIRMBOOKINGID"
			 ,TO_VARCHAR(IFNULL(T0."U_SALESORDERDOCNUM",0)) AS "SALESORDERDOCNUM"
			 ,TO_VARCHAR(T0."U_PurchaseOrder") AS "PurchaseOrder"
			 ,IFNULL(T0."U_CARDCODE",'') AS "CARDCODE"
			 ,IFNULL((SELECT "CardName" FROM EW_PRD_TEST."ORDR" WHERE "DocEntry"=T0."U_SALESORDERDOCNUM"),'') AS CARDNAME
			 ,T0."U_VendorCode" AS "VendorCode"
			 ,IFNULL(T0."U_REMARKSCOSTING",'') AS "REMARKSCOSTING"
			 ,IFNULL(T0."U_REMAKRSSELLING",'') AS "REMAKRSSELLING"
			 ,CAST(T0."U_TOTALCOSTING" AS float) AS "TOTALCOSTING"
			 ,CAST(T0."U_REBATE" AS float) AS "REBATE"
			 ,CAST(T0."U_GRANDTOTALCOSTING" AS float) AS "GRANDTOTALCOSTING"
			 ,CAST(T0."U_TOTALSELLING" AS float) AS "TOTALSELLING"
			 ,CAST(T0."U_GRANDTOTALSELLING" AS float) AS "GRANDTOTALSELLING"
			 ,CAST(T0."U_DutyTaxAmountCosting" AS float) AS "DutyTaxAmountCosting"
			 ,CAST(T0."U_DutyTaxAmountSelling" AS float) AS "DutyTaxAmountSelling"
			 ,CAST(T0."U_TOTALPROFIT" AS float) AS "TOTALPROFIT"
			 ,T0."U_USERCREATE" AS "USERCREATE"
			 ,T0."U_JOBNO" AS "JOBNO"
			 ,T0."U_COSTINGCONFIRM" AS "COSTINGCONFIRM"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0
		WHERE T0."DocEntry"=:par1;
	ELSE IF :DTYPE='GetDetailJobSheetLineRevenueByDocEntry' THEN
		SELECT
			 0 AS "LineId"
			,A."U_ITEMCODE"||'_*_'||C."ItemName" AS "ITEMCODE"
			,CAST(A."U_TOTALPRICE" AS float) AS "TOTALPRICE"
			,CAST(A."U_Qty" AS float) AS "Qty"
			,TO_VARCHAR(CASE WHEN A."U_ContainerType"=0 THEN C."UgpEntry" ELSE A."U_ContainerType" END) AS "ContainerType"
			,IFNULL(A."U_Currency",'') AS "Currency"
			,CAST(A."U_ExchangeRate" AS float) AS "ExchangeRate"
			,CAST(A."U_TotalBaht" AS float) AS "TotalBaht"
			,IFNULL(TO_VARCHAR(A."U_RefLineId"),'') AS "RefLineId"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SELLING" AS A
		LEFT JOIN EW_PRD_TEST."OITM" AS C ON C."ItemCode"=A."U_ITEMCODE"
		WHERE A."DocEntry"=:par1 ORDER By A."LineId" ASC;
	ELSE IF :DTYPE='GetDetailJobSheetLineCostingByDocEntry' THEN
		SELECT
			 0 AS "LineId"
			,A."U_ITEMCODE"||'_*_'||C."ItemName" AS "ITEMCODE"
			,CAST(A."U_TOTALPRICE" AS float) AS "TOTALPRICE"
			,CAST(A."U_Qty" AS float) AS "Qty"
			,TO_VARCHAR(CASE WHEN A."U_ContainerType"=0 THEN C."UgpEntry" ELSE A."U_ContainerType" END) AS "ContainerType"
			,IFNULL(A."U_Currency",'') AS "Currency"
			,CAST(A."U_ExchangeRate" AS float) AS "ExchangeRate"
			,CAST(A."U_TotalBaht" AS float) AS "TotalBaht"
			,IFNULL(TO_VARCHAR(A."U_RefLineId"),'') AS "RefLineId"
			,IFNULL(TO_VARCHAR(A."U_VendorCode"),'') AS "VendorCode"
			,IFNULL(TO_VARCHAR(A."U_PurchaseOrder"),'') AS "PurchaseOrder"
		FROM EW_PRD_TEST."@TB_JOBSHEET_COSTING" AS A
		LEFT JOIN EW_PRD_TEST."OITM" AS C ON C."ItemCode"=A."U_ITEMCODE"
		WHERE A."DocEntry"=:par1 ORDER By A."LineId" ASC;
	ELSE IF :DTYPE='GetDetailJobSheetLineAttachmentByDocEntry' THEN
		SELECT
			 0 AS "LineId"
			,A."U_ATTACHMENTNAME" AS "ATTACHMENTNAME"
		FROM EW_PRD_TEST."@ATTACHMENT_SEA_AIR" AS A
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetDetailJobSheetTruckingDetail' THEN
		Select 
			 '' AS "SalesPerson" --C."SlpName"
			,CAST(A."DocEntry" AS INT) AS "DocNum"
			,CAST(A."DocEntry" AS INT) AS "DocEntry"
			,A."U_CURRENCYCOSTING" AS "CurrencyCosting"
			,A."U_CURRENCYSELLING" AS "CurrencySelling"
			,TO_DECIMAL(A."U_TOTALCOSTING",10,2) AS "TotalCosting"
			,TO_DECIMAL(A."U_TOTALSELLING",10,2) AS "TotalSelling"
			,TO_DECIMAL(A."U_REBATE",10,2) AS "Rebate"
			,TO_DECIMAL((A."U_TOTALCOSTING"+A."U_REBATE")/(SELECT "Rate" FROM EW_PRD_TEST."ORTT" AS T0 WHERE T0."Currency"='THB' AND T0."RateDate"=A."CreateDate"),10,2) AS "GrandTotalCosting"
			,TO_DECIMAL(A."U_TOTALSELLING"/(SELECT "Rate" FROM EW_PRD_TEST."ORTT" AS T0 WHERE T0."Currency"='THB' AND T0."RateDate"=A."CreateDate"),10,2) AS "GrandTotalSelling"
			,IFNULL(TO_DECIMAL((A."U_TOTALCOSTING"+A."U_REBATE")*(SELECT "Rate" FROM EW_PRD_TEST."ORTT" WHERE "RateDate"=A."CreateDate" AND "Currency"=A."U_CURRENCYCOSTING"),12,2),0) AS "GrandTotalCostingUSD"
			,TO_DECIMAL(A."U_GRANDTOTALSELLINGUSD",10,2) AS "GrandTotalSellingUSD"
			,TO_DECIMAL(A."U_GRANDTOTALSELLINGUSD",10,2)-(IFNULL(TO_DECIMAL((A."U_TOTALCOSTING"+A."U_REBATE")*(SELECT "Rate" FROM EW_PRD_TEST."ORTT" WHERE "RateDate"=A."CreateDate" AND "Currency"=A."U_CURRENCYCOSTING"),12,2),0)) AS "Profit"
			,IFNULL(A."U_REMARKSCOSTING",'') AS "RemarkForCosting"
			,IFNULL(A."U_REMAKRSSELLING",'') AS "RemarkForSelling"
			,(SELECT "Rate" FROM EW_PRD_TEST."ORTT" AS T0 WHERE T0."Currency"='THB' AND T0."RateDate"=A."CreateDate") AS "ExchangeRate"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		--LEFT JOIN EW_PRD_TEST."OQUT" AS B ON A."U_SALEQUOTATIONDOCNUM"=B."DocEntry"
		--LEFT JOIN EW_PRD_TEST."OSLP" AS C ON C."SlpCode"=B."SlpCode"
		WHERE A."U_CONFIRMBOOKINGID"=:par1 AND A."DocEntry"=:par2;
	ELSE IF :DTYPE='GetConfirmBookingSheetListCustomer' THEN
		SELECT 
			 B."U_CUSTOMERCODE" AS CODE
			,C."CardName" AS "CARDNAME"
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TBCUSTOMER" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=B."U_CUSTOMERCODE"
		WHERE A."U_CONFIRMBOOKINGSHEETID"=:par1;
	ELSE IF :DTYPE='GetListJobSheetSeaAir' THEN
		IF :par3='ALL' THEN
			SELECT 
				 T0."DocEntry" AS JOBSHEETID
				,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" WHERE "AbsEntry"=AA."U_PROJECTMANAGEMENTID") AS JOBNO
				,TO_VARCHAR(T0."CreateDate",'DD/MM/YYYY') AS JOBSHEETDATE
				,CASE WHEN T0."CreateTime"=T0."UpdateTime" THEN 
				     	'' 
				     	ELSE TO_VARCHAR(T0."UpdateDate",'DD/MM/YYYY') END AS UPDATEDATE
					 ,LEFT(CASE
							WHEN LENGTH(T0."CreateTime") = 3  THEN CAST('0' || T0."CreateTime"AS TIME) 
							WHEN LENGTH(T0."CreateTime") = 2 THEN CAST('00' || T0."CreateTime"AS TIME)
							WHEN LENGTH(T0."CreateTime") = 1 THEN CAST('000' || T0."CreateTime"AS TIME) 
							ELSE CAST(T0."CreateTime"AS TIME) END
				     ,5) AS "CREATETIME"
				     ,CASE WHEN T0."CreateTime"=T0."UpdateTime" THEN 
				     	'' 
				     	ELSE 
				     	LEFT(CASE
							WHEN LENGTH(T0."UpdateTime") = 3  THEN CAST('0' || T0."UpdateTime" AS TIME) 
							WHEN LENGTH(T0."UpdateTime") = 2 THEN CAST('00' || T0."UpdateTime" AS TIME)
							WHEN LENGTH(T0."UpdateTime") = 1 THEN CAST('000' || T0."UpdateTime" AS TIME) 
							ELSE CAST(T0."UpdateTime" AS TIME) END ,5) 
						END
				      AS "UPDATETIME"
				,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS "LoadingDate"
				,TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY') AS "ETADate"
				,B."Name" AS IMPORTYPE
				,C."CardName" AS CO
				,D."descript" AS ROUTE
				,E."Code" AS CREATEBY
				,T0."U_CONFIRMBOOKINGID" AS CONFIRMBOOKINGID
				,CASE WHEN IFNULL(T0."U_SALESORDERDOCNUM",0)=0 THEN '' ELSE TO_VARCHAR(T0."U_SALESORDERDOCNUM") END AS "DocEntrySO"
				,CASE WHEN T0."U_USERCREATE" IN (
					SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
					) OR T0."U_USERCREATE"=:par4 THEN
						T0."U_COSTINGCONFIRM"
				 ELSE 'Y' END AS COSTINGCONFIRMSTATUS
				,(SELECT STRING_AGG(K1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS K0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS K1 ON K1."CardCode"=K0."U_SHIPPER"
					 	WHERE K0."DocEntry"=A."DocEntry") AS "Shipper"
				,(SELECT STRING_AGG(K1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS K0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS K1 ON K1."CardCode"=K0."U_CONSIGNEE"
					 	WHERE K0."DocEntry"=A."DocEntry") AS "Consignee"
				,T0."U_UpdateBy" AS "UpdateBy"
				,CASE WHEN T0."U_USERCREATE" IN (
					SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
					) OR T0."U_USERCREATE"=:par4 THEN
						CASE WHEN T0."Status"='C' THEN 
							'C' 
						ELSE 
							CASE WHEN IFNULL(T0."U_SALESORDERDOCNUM",0)=0 THEN
						 		'N'
						 	ELSE 
						 		'C'
						 	END
						END
				 ELSE 'Y' END AS "Status"
				,CASE WHEN T0."U_USERCREATE" IN (
					SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_CANCEL" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
					) OR T0."U_USERCREATE"=:par4 THEN
						CASE WHEN IFNULL(T0."U_SALESORDERDOCNUM",0)=0 THEN
							T0."Status"
						ELSE
							CASE WHEN
							(SELECT 
								"DocStatus" 
							 FROM EW_PRD_TEST."ORDR" 
							 WHERE "DocEntry"=T0."U_SALESORDERDOCNUM")='O' 
								AND 
							 (SELECT 
								COUNT(Z3."LineStatus")
							  FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS Z0
							  LEFT JOIN EW_PRD_TEST."ORDR" AS Z1 ON Z1."DocEntry"=Z0."U_SALESORDERDOCNUM"
							  LEFT JOIN EW_PRD_TEST."PRQ1" AS Z2 ON Z2."BaseEntry"=Z1."DocEntry" AND Z2."BaseType"=Z1."ObjType"
							  LEFT JOIN EW_PRD_TEST."POR1" AS Z3 ON Z3."BaseEntry"=Z2."DocEntry" AND Z3."BaseType"=Z2."ObjType"
							  WHERE Z0."DocEntry"=T0."DocEntry" AND Z3."LineStatus"='C')=0 THEN 
									T0."Status"
							  ELSE 'Y' END
						END
				 ELSE 'Y' END AS "CancelStatus"
			FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0
			LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS AA ON AA."DocEntry"=T0."U_CONFIRMBOOKINGID"
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=T0."U_CARDCODE"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=A."U_DESTINATION"
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=T0."U_USERCREATE"
			WHERE A."U_CreateBy"=CASE WHEN '-1'='-1' THEN A."U_CreateBy" ELSE A."U_CreateBy" END 
				  AND A."CreateDate" BETWEEN :par1 AND :par2
				  AND T0."Status"='O'
				  AND (T0."U_USERCREATE"=:par4
						OR T0."U_USERCREATE" IN (
									SELECT "U_User" 
									FROM EW_PRD_TEST."@TB_P_READ" AS T0
									LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
									WHERE T1."Code"=:par4
								))
			ORDER BY T0."DocEntry" DESC;
		ELSE IF :par3='DEFAULT' THEN
			SELECT TOP 1000
				 T0."DocEntry" AS JOBSHEETID
				,(SELECT "NAME" FROM EW_PRD_TEST."OPMG" WHERE "AbsEntry"=AA."U_PROJECTMANAGEMENTID") AS JOBNO
				,TO_VARCHAR(T0."CreateDate",'DD/MM/YYYY') AS JOBSHEETDATE
				,CASE WHEN T0."CreateTime"=T0."UpdateTime" THEN 
				     	'' 
				     	ELSE TO_VARCHAR(T0."UpdateDate",'dd/MM/YYYY') END AS UPDATEDATE
					 ,LEFT(CASE
							WHEN LENGTH(T0."CreateTime") = 3  THEN CAST('0' || T0."CreateTime"AS TIME) 
							WHEN LENGTH(T0."CreateTime") = 2 THEN CAST('00' || T0."CreateTime"AS TIME)
							WHEN LENGTH(T0."CreateTime") = 1 THEN CAST('000' || T0."CreateTime"AS TIME) 
							ELSE CAST(T0."CreateTime"AS TIME) END
				     ,5) AS "CREATETIME"
				     ,CASE WHEN T0."CreateTime"=T0."UpdateTime" THEN 
				     	'' 
				     	ELSE 
				     	LEFT(CASE
							WHEN LENGTH(T0."UpdateTime") = 3  THEN CAST('0' || T0."UpdateTime" AS TIME) 
							WHEN LENGTH(T0."UpdateTime") = 2 THEN CAST('00' || T0."UpdateTime" AS TIME)
							WHEN LENGTH(T0."UpdateTime") = 1 THEN CAST('000' || T0."UpdateTime" AS TIME) 
							ELSE CAST(T0."UpdateTime" AS TIME) END ,5) 
						END
				      AS "UPDATETIME"
				,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS "LoadingDate"
				,TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY') AS "ETADate"
				,B."Name" AS IMPORTYPE
				,C."CardName" AS CO
				,D."descript" AS ROUTE
				,E."Code" AS CREATEBY
				,CASE WHEN IFNULL(T0."U_SALESORDERDOCNUM",0)=0 THEN '' ELSE TO_VARCHAR(T0."U_SALESORDERDOCNUM") END AS "DocEntrySO"
				,T0."U_CONFIRMBOOKINGID" AS CONFIRMBOOKINGID
				,CASE WHEN T0."U_USERCREATE" IN (
					SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
					) OR T0."U_USERCREATE"=:par4 THEN
						CASE WHEN T0."Status"='O' THEN T0."U_COSTINGCONFIRM" ELSE 'Y' END
				 ELSE 'Y' END AS COSTINGCONFIRMSTATUS
				,(SELECT STRING_AGG(K1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS K0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS K1 ON K1."CardCode"=K0."U_SHIPPER"
					 	WHERE K0."DocEntry"=A."DocEntry") AS "Shipper"
				,(SELECT STRING_AGG(K1."CardName",' - ') 
					 	FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS K0 
					 	LEFT JOIN EW_PRD_TEST."OCRD" AS K1 ON K1."CardCode"=K0."U_CONSIGNEE"
					 	WHERE K0."DocEntry"=A."DocEntry") AS "Consignee"
				,T0."U_UpdateBy" AS "UpdateBy"
				,CASE WHEN T0."U_USERCREATE" IN (
					SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_UPDATE" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
					) OR T0."U_USERCREATE"=:par4 THEN
						CASE WHEN T0."Status"='C' THEN 
							'C' 
						ELSE 
							CASE WHEN IFNULL(T0."U_SALESORDERDOCNUM",0)=0 THEN
						 		'N'
						 	ELSE 
						 		'C'
						 	END
						END
				 ELSE 'Y' END AS "Status"
				,CASE WHEN T0."U_USERCREATE" IN (
					SELECT "U_User" 
						FROM EW_PRD_TEST."@TB_P_CANCEL" AS T0
						LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
						WHERE T1."Code"=:par4
					) OR T0."U_USERCREATE"=:par4 THEN
						CASE WHEN IFNULL(T0."U_SALESORDERDOCNUM",0)=0 THEN
							T0."Status"
						ELSE
							CASE WHEN
							(SELECT 
								"DocStatus" 
							 FROM EW_PRD_TEST."ORDR" 
							 WHERE "DocEntry"=T0."U_SALESORDERDOCNUM")='O' 
								AND 
							 (SELECT 
								COUNT(Z3."LineStatus")
							  FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS Z0
							  LEFT JOIN EW_PRD_TEST."ORDR" AS Z1 ON Z1."DocEntry"=Z0."U_SALESORDERDOCNUM"
							  LEFT JOIN EW_PRD_TEST."PRQ1" AS Z2 ON Z2."BaseEntry"=Z1."DocEntry" AND Z2."BaseType"=Z1."ObjType"
							  LEFT JOIN EW_PRD_TEST."POR1" AS Z3 ON Z3."BaseEntry"=Z2."DocEntry" AND Z3."BaseType"=Z2."ObjType"
							  WHERE Z0."DocEntry"=T0."DocEntry" AND Z3."LineStatus"='C')=0 THEN 
									T0."Status"
							  ELSE 'Y' END
						END
				 ELSE 'Y' END AS "CancelStatus"
			FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS T0
			LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS AA ON AA."DocEntry"=T0."U_CONFIRMBOOKINGID"
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS A ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=T0."U_CARDCODE"
			LEFT JOIN EW_PRD_TEST."OTER" AS D ON TO_VARCHAR(D."territryID")=A."U_DESTINATION"
			LEFT JOIN EW_PRD_TEST."@TBUSER" AS E ON E."Code"=T0."U_USERCREATE"
			WHERE A."U_CreateBy"=CASE WHEN '-1'='-1' THEN A."U_CreateBy" ELSE A."U_CreateBy" END
				  AND A."CreateDate" BETWEEN :par1 AND :par2
				  AND T0."Status"='O'
				  AND (T0."U_USERCREATE"=:par4
						OR T0."U_USERCREATE" IN (
									SELECT "U_User" 
									FROM EW_PRD_TEST."@TB_P_READ" AS T0
									LEFT JOIN EW_PRD_TEST."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
									WHERE T1."Code"=:par4
								))
			ORDER BY T0."DocEntry" DESC;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetItemCodePurchaseRequest' THEN
		IF :par1='GetItemJournalVoucher' THEN
			SELECT 	A."ItemCode" "ItemCode"
				   ,A."ItemCode"||' - '||A."ItemName" AS "ItemName"
				   ,IFNULL(CAST(B."Price" AS float),0) AS "Price"
				   ,(SELECT "RevenuesAc" FROM EW_PRD_TEST."OITB" WHERE "ItmsGrpCod"=A."ItmsGrpCod") AS "ServiceType"
			FROM EW_PRD_TEST."OITM" AS A 
			LEFT JOIN EW_PRD_TEST."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1  
			WHERE 
				A."ItmsGrpCod"NOT IN('130') 
				AND A."U_Dept_item"IN ('CBT','ALL') 
				AND (SELECT "RevenuesAc" FROM EW_PRD_TEST."OITB" WHERE "ItmsGrpCod"=A."ItmsGrpCod")!='';
		END IF;	
	ELSE IF :DTYPE = 'GETPROJECTMANAGEMENTLIST' THEN 
		SELECT A."PrjCode" AS "ProjectCode"
			  ,A."PrjName" AS "ProjectName"
			  ,A."Active"  AS "Active"
			  ,IFNULL((SELECT STRING_AGG("DocEntry",',') FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" WHERE "U_JOBNO"=A."PrjName"),'') "DocEntryJobSheet"
			  ,(SELECT TO_VARCHAR("AbsEntry") FROM EW_PRD_TEST."OPMG" WHERE "NAME"=A."PrjCode") AS "AbsEntry"
		FROM EW_PRD_TEST."OPRJ" AS A
		WHERE A."Active"='Y' 
		AND (SELECT "AbsEntry" FROM EW_PRD_TEST."OPMG" WHERE "NAME"=A."PrjCode") 
				IN (SELECT "U_PROJECTMANAGEMENTID" FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "Canceled"='N');
	ELSE IF :DTYPE='GetTaxCode' THEN
		IF :par1='Purchase' THEN
			SELECT 
				 A."Code" AS "Code"
				,A."Name" AS "Name"
				,TO_DECIMAL(IFNULL(A."Rate",0),6,2) AS "Rate"
			FROM EW_PRD_TEST."OVTG" AS A 
			WHERE A."Inactive"='N' AND A."Account" IS NOT NULL AND A."Category"='I';
		ELSE IF :par1='Sales' THEN
			SELECT 
				 A."Code" AS "Code"
				,A."Name" AS "Name"
				,TO_DECIMAL(IFNULL(A."Rate",0),6,2) AS "Rate"
			FROM EW_PRD_TEST."OVTG" AS A 
			WHERE A."Inactive"='N' AND A."Account" IS NOT NULL AND A."Category"='O';
		END IF;
		END IF;
	ELSE IF :DTYPE='GetListOfCreditNoteSeaAir' THEN
		IF :par3='ALL' THEN
			SELECT * FROM (
			
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE --'dd/mm/YYYY'
					,TO_DECIMAL(A."DocTotal"*-1,12,2) AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ORIN" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."NumAtCard" IS NOT NULL AND IFNULL(A."U_CreditMemoType",'')!='Debit'
				AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    AND D."STATUS"!='N'
				AND A."DocDate" BETWEEN :par1 AND :par2
				AND (A."U_UserCreate"=:par4
						OR A."U_UserCreate" IN (
							SELECT "U_User" 
							FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
							LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
							WHERE T1."Code"=:par4
						)) 
				
				UNION ALL
				
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE --'dd/mm/YYYY'
					,TO_DECIMAL(A."DocTotal"*-1,12,2) AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ODRF" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."NumAtCard" IS NOT NULL AND IFNULL(A."U_CreditMemoType",'')!='Debit'
				AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    AND D."STATUS"!='N'
				AND A."DocDate" BETWEEN :par1 AND :par2
				AND (A."U_UserCreate"=:par4
						OR A."U_UserCreate" IN (
							SELECT "U_User" 
							FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
							LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
							WHERE T1."Code"=:par4
						)) 
				AND A."ObjType"=14 
				AND A."DocEntry" NOT IN (SELECT "draftKey" FROM EW_PRD_TEST."ORIN" WHERE "draftKey" IS NOT NULL)
			)ORDER BY "DOCENTRY" DESC;		 	
		ELSE IF :par3='DEFAULT' THEN
			SELECT TOP 1000 * FROM (
			
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE --'dd/mm/YYYY'
					,TO_DECIMAL(A."DocTotal"*-1,12,2) AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ORIN" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."NumAtCard" IS NOT NULL AND IFNULL(A."U_CreditMemoType",'')!='Debit'
				AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    AND D."STATUS"!='N'
				AND A."DocDate" BETWEEN :par1 AND :par2
				AND (A."U_UserCreate"=:par4
						OR A."U_UserCreate" IN (
							SELECT "U_User" 
							FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
							LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
							WHERE T1."Code"=:par4
						)) 
				
				UNION ALL
				
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE --'dd/mm/YYYY'
					,TO_DECIMAL(A."DocTotal"*-1,12,2) AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ODRF" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."NumAtCard" IS NOT NULL AND IFNULL(A."U_CreditMemoType",'')!='Debit'
				AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    AND D."STATUS"!='N'
				AND A."DocDate" BETWEEN :par1 AND :par2
				AND (A."U_UserCreate"=:par4
						OR A."U_UserCreate" IN (
							SELECT "U_User" 
							FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
							LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
							WHERE T1."Code"=:par4
						)) 
				AND A."ObjType"=14 
				AND A."DocEntry" NOT IN (SELECT "draftKey" FROM EW_PRD_TEST."ORIN" WHERE "draftKey" IS NOT NULL)
			)ORDER BY "DOCENTRY" DESC;
			
		END IF;
		END IF;
	ELSE IF :DTYPE='GetCreditSeaAirDetailByDocEntry' THEN
		IF :par1='HeaderCredit' THEN 
			SELECT
				 TO_VARCHAR(A."BatchNum") AS "DocNum"
				,TO_VARCHAR(A."DueDate",'DD/MM/YYYY') AS "IssueDate"
				,C."CardCode" AS "VendorCode"
				,C."CardName" AS "VendorName"
				,'THB' AS "AmountCurrency"
				,B."Project" AS "JobNo"
				,CAST(A."LocTotal" AS decimal) AS "GrandTotal"
				,A."Memo" AS "Remarks"
			FROM EW_PRD_TEST."OBTF" AS A 
			LEFT JOIN EW_PRD_TEST."BTF1" AS B ON B."BatchNum"=A."BatchNum" AND B."DebCred"='C'
			LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=B."ShortName"
			WHERE A."BatchNum"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."U_CardCode" AS "ItemCode"
				,B."ItemCode" AS "ItemName"
				,C."OcrName" AS "DistributionRule"
				,A."Project" AS "JobNo"
				,A."VatGroup" AS "VatCode"
				,Cast(A."Debit" AS decimal) AS "Amount"
				,A."LineMemo" AS "remark"
			FROM EW_PRD_TEST."BTF1" AS A
			LEFT JOIN EW_PRD_TEST."OITM" AS B ON B."ItemCode"=A."U_CardCode"
			LEFT JOIN EW_PRD_TEST."OOCR" AS C ON C."OcrCode"=A."ProfitCode"
			WHERE A."BatchNum"=:par2 AND A."U_CardCode"IS NOT NULL;
		ELSE IF :par1='HeaderDebit' THEN 
			SELECT
				 TO_VARCHAR(A."BatchNum") AS "DocNum"
				,TO_VARCHAR(A."DueDate",'DD/MM/YYYY') AS "IssueDate"
				,C."CardCode" AS "VendorCode"
				,C."CardName" AS "VendorName"
				,'THB' AS "AmountCurrency"
				,B."Project" AS "JobNo"
				,CAST(A."LocTotal" AS decimal) AS "GrandTotal"
				,A."Memo" AS "Remarks"
			FROM EW_PRD_TEST."OBTF" AS A 
			LEFT JOIN EW_PRD_TEST."BTF1" AS B ON B."BatchNum"=A."BatchNum" AND B."DebCred"='D'
			LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=B."ShortName"
			WHERE A."BatchNum"=:par2;
		END IF;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetListOfDebitNoteSeaAir' THEN
		IF :par3='ALL' THEN
			SELECT * FROM (
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT MAX(T0."Project") FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE
					,A."DocTotal"*-1 AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ORIN" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."U_CreditMemoType"='Debit'
					AND A."DocDate" BETWEEN :par1 AND :par2
					AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    	AND D."STATUS"!='N'
				 	AND (A."U_UserCreate"=:par4
							OR A."U_UserCreate" IN (
										SELECT "U_User" 
										FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
										LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									))
					AND IFNULL((SELECT DISTINCT COUNT(RIN1."Project") FROM "EW_PRD_TEST"."RIN1" WHERE RIN1."DocEntry"=A."DocEntry"),1)<2
					AND A."CANCELED"='N'
				
				UNION ALL
				
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT MAX(T0."Project") FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE
					,A."DocTotal"*-1 AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ODRF" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."U_CreditMemoType"='Debit'
					AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    	AND D."STATUS"!='N'
					AND A."DocDate" BETWEEN :par1 AND :par2
				 	AND (A."U_UserCreate"=:par4
							OR A."U_UserCreate" IN (
										SELECT "U_User" 
										FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
										LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									))
					AND A."ObjType"='14'
					AND A."DocEntry" NOT IN (SELECT "draftKey" FROM EW_PRD_TEST."ORIN" WHERE "draftKey" IS NOT NULL)
			)AS A ORDER BY A."DOCENTRY" DESC;
		ELSE IF :par3='DEFAULT' THEN
			SELECT TOP 1000 * FROM (
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT MAX(T0."Project") FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE
					,A."DocTotal"*-1 AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ORIN" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."U_CreditMemoType"='Debit'
					AND A."DocDate" BETWEEN :par1 AND :par2
					AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    	AND D."STATUS"!='N'
				 	AND (A."U_UserCreate"=:par4
							OR A."U_UserCreate" IN (
										SELECT "U_User" 
										FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
										LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									))
					AND IFNULL((SELECT DISTINCT COUNT(RIN1."Project") FROM "EW_PRD_TEST"."RIN1" WHERE RIN1."DocEntry"=A."DocEntry"),1)<2
					AND A."CANCELED"='N'
				
				UNION ALL
				
				SELECT 
					 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
					,A."DocEntry" AS DOCENTRY
					,A."DocNum" AS DOCNUM
					,A."NumAtCard" AS REFNO
					,(SELECT DISTINCT MAX(T0."Project") FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
					,A."CardCode" AS VENDORCODE
					,C."CardName" AS VENDORNAME
					,TO_VARCHAR(A."DocDate",'YYYYMMDD') AS ISSUEDATE
					,A."DocTotal"*-1 AS DOCTOTAL
					,A."Comments" AS REMARKS
				FROM "EW_PRD_TEST"."ODRF" AS A
				LEFT JOIN "EW_PRD_TEST"."OCRD" AS C ON C."CardCode"=A."CardCode"
				LEFT JOIN EW_PRD_TEST."OPMG" AS D ON D."NAME"=(SELECT DISTINCT T0."Project" FROM "EW_PRD_TEST"."DRF1" T0 WHERE T0."DocEntry"=A."DocEntry")
		 		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS E ON E."U_PROJECTMANAGEMENTID"=D."AbsEntry"
				WHERE A."U_CreditMemoType"='Debit'
					AND E."U_PROJECTMANAGEMENTID" IS NOT NULL
		 	    	AND D."STATUS"!='N'
					AND A."DocDate" BETWEEN :par1 AND :par2
				 	AND (A."U_UserCreate"=:par4
							OR A."U_UserCreate" IN (
										SELECT "U_User" 
										FROM "EW_PRD_TEST"."@TB_P_READ" AS T0
										LEFT JOIN "EW_PRD_TEST"."@TBUSER" AS T1 ON T1."U_USERROLE"=T0."Code"
										WHERE T1."Code"=:par4
									))
					AND A."ObjType"='14'
					AND A."DocEntry" NOT IN (SELECT "draftKey" FROM EW_PRD_TEST."ORIN" WHERE "draftKey" IS NOT NULL)
			)AS A ORDER BY A."DOCENTRY" DESC;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetDetailBookingByDocEntry' THEN
		IF :par1='THAIBORDER' THEN
			SELECT 
				 A."U_THAIBORDER" AS "Code"
				,B."Name" AS "THAIBORDER"
			FROM EW_PRD_TEST."@TB_THAI_B_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBTHAIBORDER" AS B ON B."Code"=A."U_THAIBORDER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='SHIPPER' THEN
			SELECT 
				 A."U_SHIPPER" AS "CardCode"
				,B."CardName" AS "SHIPPER"
			FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_SHIPPER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='CONSIGNEE' THEN
			SELECT 
				 A."U_CONSIGNEE" AS "CardCode"
				,B."CardName" AS "CONSIGNEE"
			FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='CONSIGNEE' THEN
			SELECT 
				 A."U_CONSIGNEE" AS "CardCode"
				,B."CardName" AS "CONSIGNEE"
			FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetPlaceOfLoadingBookingSheet' THEN
			SELECT 
				 A."U_PLACEOFLOADING" AS "Code"
				,B."Name"||', '||B."U_COUNTRY" AS "PLACEOFLOADING"
			FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFLOADING"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='GetPlaceOfDeliveryBookingSheet' THEN
			SELECT 
				 A."U_PLACEOFDELIVERY" AS "Code"
				,B."Name"||', '||B."U_COUNTRY" AS "PLACEOFDELIVERY"
			FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS A
			LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFDELIVERY"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetListPurchaseOrderCancelJobSheetTrucking' THEN
		SELECT DISTINCT
			IFNULL(B."U_PurchaseOrder",0) AS "PurchaseOrderDocEntry"
		FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TB_JOBSHEET_COSTING" AS B ON B."DocEntry"=A."DocEntry"
		/*LEFT JOIN EW_PRD_TEST."ORDR" AS B ON B."DocEntry"=A."U_SALESORDERDOCNUM"
		LEFT JOIN EW_PRD_TEST."PRQ1" AS C ON C."BaseEntry"=B."DocEntry" AND C."BaseType"=B."ObjType"
		LEFT JOIN EW_PRD_TEST."POR1" AS D ON D."BaseEntry"=C."DocEntry" AND D."BaseType"=C."ObjType"*/
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'UPDATENUMATCARDSOSA' THEN
		UPDATE EW_PRD_TEST."ORDR" SET "NumAtCard"='' WHERE "DocEntry"=:par1;
		UPDATE EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" SET "U_SALESORDERDOCNUM"=0,"U_CONFIRMBOOKINGID"=0 WHERE "DocEntry"=:par2;
	ELSE IF :DTYPE='GetVendorByConfirmJobSheetSeaAir' THEN
		SELECT "CardCode","CardName" FROM (
			SELECT 
				 D."CardCode" AS "CardCode"
				,D."CardCode"||' - '||D."CardName" AS "CardName"
				,A."DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS A
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS B ON B."DocEntry"=A."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBCOLOADER" AS C ON C."DocEntry"=B."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=C."U_COLOADER"
			UNION
			SELECT 
				 D."CardCode" AS "CardCode"
				,D."CardCode"||' - '||D."CardName" AS "CardName"
				,A."DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS A
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS B ON B."DocEntry"=A."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBSHIPPINGLINE" AS C ON C."DocEntry"=B."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=C."U_SHIPPINGLINE"
			UNION
			SELECT 
				 D."CardCode" AS "CardCode"
				,D."CardCode"||' - '||D."CardName" AS "CardName"
				,A."DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS A
			LEFT JOIN EW_PRD_TEST."@BOOKING_SEA_AIR" AS B ON B."DocEntry"=A."U_BOOKINGID"
			LEFT JOIN EW_PRD_TEST."@TBDESTAGENT" AS C ON C."DocEntry"=B."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS D ON D."CardCode"=C."U_DESTAGENT"
		)AS A WHERE A."DocEntry"=:par1 AND "CardCode" IS NOT NULL;
	ELSE IF :DTYPE='GetStatusSOForAddPRByDocEntry' THEN
		SELECT "DocStatus" AS "DocStatus" FROM EW_PRD_TEST."ORDR" WHERE "DocEntry"=:par1;
	ELSE IF :DTYPE='GETEWSERIESBYLOADINGDATE' THEN
		SELECT TOP 1 
			"Series" AS "GetSeries"
		FROM EW_PRD_TEST."NNM1" 
		WHERE "ObjectCode"='BOOKING_SEA_AIR' 
			  AND "BeginStr"='SA'
			  AND "Indicator"=TO_VARCHAR(CAST(:par1 AS DATE),'yyyy-MM');
	ELSE IF :DTYPE='CHECKINGLOADINGDATEHASCHANGEORNOT' THEN
		SELECT 
			TO_VARCHAR("U_BOOKINGDATE",'yyyy-MM') AS "Condition"
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" WHERE "DocEntry"=:par1;
	ELSE IF :DTYPE='GetCurrencyByVendorCode' THEN
		IF (SELECT "Currency" FROM EW_PRD_TEST."OCRD" WHERE "CardCode"=:par1)='##' THEN
			SELECT 		
				"CurrCode"  AS "Currency"
			FROM EW_PRD_TEST."CRD13"
			WHERE "CardCode"=:par1 
			  AND "Locked"='Y';
		ELSE
			SELECT 
				"Currency" AS "Currency"
			FROM EW_PRD_TEST."OCRD" 
			WHERE "CardCode"=:par1;
		END IF;
	ELSE IF :DTYPE='GetArCreditNoteRefInvSeaAndAir' THEN
		SELECT DISTINCT
			 TO_VARCHAR(A."DocEntry") AS "DocEntry"
			,TO_VARCHAR(A."DocNum") AS "DocNum" 
		FROM EW_PRD_TEST."OINV" AS A
		LEFT JOIN EW_PRD_TEST."INV1" AS B ON B."DocEntry"=A."DocEntry"
		WHERE B."Project"
			IN (SELECT 
					T1."NAME"
				FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
				LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID");
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
END;
