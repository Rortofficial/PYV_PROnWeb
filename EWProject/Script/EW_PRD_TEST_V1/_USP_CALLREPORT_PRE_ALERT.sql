ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLREPORT_PRE_ALERT(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250),
	in par3 NVARCHAR(250)
)
AS
BEGIN
	SELECT * FROM (
		SELECT 
			 IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'DD-MM-YYYY'),'') AS "LoadingDate"
			,IFNULL(TO_VARCHAR(A."U_CROSSBORDERDATE",'DD-MM-YYYY'),'') AS "ETABorderDate"
			,IFNULL(TO_VARCHAR(A."U_ETAREQUIREMENT",'DD-MM-YYYY'),'') AS "CrossBorderDate"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBSHIPPER" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T0."U_SHIPPER"=T1."CardCode"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "Shipper"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBCONSIGNEE" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T0."U_CONSIGNEE"=T1."CardCode"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "Consignee"
			,IFNULL((SELECT 
					STRING_AGG(T0."U_INVOICE",',')
				FROM EW_PRD_TEST."@COMMODITY" AS T0
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "InvoiceNo"
			,IFNULL((SELECT STRING_AGG("Volumn",', ') FROM
				(
					SELECT
						SUM(T0."U_QTY")||' x '||T0."U_VOLUMECODE" AS "Volumn"
					FROM EW_PRD_TEST."@VOLUME" AS T0 
					WHERE T0."DocEntry"=A."DocEntry"
					GROUP BY T0."U_VOLUMECODE"
				)),'') AS "Volumn"
			,IFNULL(D."Location",'') AS "TruckProvince"
			,IFNULL(E."InventryNo",'') AS "TurckNo"
			,IFNULL(C."U_CONTAINERNO",'') AS "ContainerNo"
			,IFNULL(C."U_OWNER",'') AS "Owner"
			,IFNULL(F."firstName"||' - ','')||IFNULL(F."lastName"||' / ','')||IFNULL(C."U_TPNO1",'') AS "DriverName1"
			,IFNULL(FF."firstName"||' - ','')||IFNULL(FF."lastName"||' / ','')||IFNULL(C."U_TPNO2",'') AS "DriverName2"
			,IFNULL(G."U_ShortName",G."CardName") AS "Transporter"
			,IFNULL((SELECT 
					STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@PLACEOFLOADING" AS T0
				LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACELOADING"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "PlaceOfLoading"
			,IFNULL((SELECT 
					STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@PLACEOFDELIVERY" AS T0
				LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACEDELIVERY"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "PlaceOfDelivery"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@THAIFORWARDER" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_THAIFORWARDER"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "ThaiForwarder"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBOVERSEAFORWARDER" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_OVERSEAFORWARDERCODE"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "OverseaForwarder"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBOVERSEATRUCKER" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_OVERSEATRUCKERCODE"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "OverseaTrucker"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBSALESQUOTATION" AS T0
				LEFT JOIN EW_PRD_TEST."OQUT" AS T1 ON T1."DocEntry"=T0."U_DOCENTRY"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "CO"
			,IFNULL(A."U_LOLOYARDRemark",'') AS "LOLOYard"
			,'' AS "STUFFING_UNSTUFFING"
			,IFNULL(A."U_EWSeries",'')||IFNULL(A."U_JOBNO",'') AS "JOBNO"
			,IFNULL(I."firstName"||' - '||I."lastName",'') AS "RecordedBy"
			,IFNULL(C."U_SEALNO1",'') AS "Seal1"
			,IFNULL(C."U_SEALNO2",'') AS "Seal2"
			,IFNULL(C."U_TRAILERPROVINCE",'') AS "TrailerProvince"
			,IFNULL(C."U_TRAILERPLATE",'') AS "TrailerNo"
		FROM EW_PRD_TEST."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS B ON B."DocEntry"=A."U_CONFIRMBOOKINGSHEETID" --Get Confirm Booking Sheet
		LEFT JOIN EW_PRD_TEST."@TRUCKINFORMATION" AS C ON C."DocEntry"=B."DocEntry" -- Get Information of Truck
		LEFT JOIN EW_PRD_TEST."OLCT" AS D ON D."Code"=C."U_TRUCKPROVINCE" --Get Province Of Truck
		LEFT JOIN EW_PRD_TEST."OITM" AS E ON E."ItemCode"=C."U_TRUCKCODE" --Get Truck Number
		LEFT JOIN EW_PRD_TEST."OHEM" AS F ON F."empID"=C."U_DRIVER1" --Get Driver Name 1
		LEFT JOIN EW_PRD_TEST."OHEM" AS FF ON FF."empID"=C."U_DRIVER2" --Get Driver Name 2
		LEFT JOIN EW_PRD_TEST."OCRD" AS G ON G."CardCode"=C."U_VENDORCODE" --Get Transporter
		LEFT JOIN EW_PRD_TEST."@TBUSER" AS H ON H."Code"=A."U_UserCreate"
		LEFT JOIN EW_PRD_TEST."OHEM" AS I ON I."empID"=H."U_EMPLOYEEID"	
		--SELECT * FROM EW_PRD_TEST."@TBUSER"
		WHERE A."U_LOADINGDATE" BETWEEN :par1 AND :par2 AND 1=CASE WHEN :par3 IN ('1','-99') THEN 1 ELSE 2 END
	)
	UNION ALL
	SELECT * FROM (
		SELECT 
			 IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'DD-MM-YYYY'),'') AS "LoadingDate"
			,IFNULL(TO_VARCHAR(A."U_CROSSBORDERDATE",'DD-MM-YYYY'),'') AS "ETABorderDate"
			,IFNULL(TO_VARCHAR(A."U_ETAREQUIREMENT",'DD-MM-YYYY'),'') AS "CrossBorderDate"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T0."U_SHIPPER"=T1."CardCode"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "Shipper"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T0."U_CONSIGNEE"=T1."CardCode"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "Consignee"
			,IFNULL((SELECT 
					STRING_AGG(T0."U_Invoice",',')
				FROM EW_PRD_TEST."@COMMODITY_SEA_AIR" AS T0
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "InvoiceNo"
			,IFNULL((SELECT STRING_AGG("Volumn",', ') FROM
				(
					SELECT
						TO_DECIMAL(SUM(T0."U_Qty"),12,0)||' x '||T0."U_ContainerType" AS "Volumn"
					FROM EW_PRD_TEST."@TB_CON_S_A" AS T0 
					WHERE T0."DocEntry"=A."DocEntry"
					GROUP BY T0."U_ContainerType"
				)),'') AS "Volumn"
			,IFNULL(D."Location",'') AS "TruckProvince"
			,IFNULL(E."InventryNo",'') AS "TurckNo"
			,IFNULL(C."U_CONTAINERNO",'') AS "ContainerNo"
			,IFNULL(C."U_OWNER",'') AS "Owner"
			,IFNULL(F."firstName"||' - ','')||IFNULL(F."lastName"||' / ','')||IFNULL(C."U_TPNO1",'') AS "DriverName1"
			,IFNULL(FF."firstName"||' - ','')||IFNULL(FF."lastName"||' / ','')||IFNULL(C."U_TPNO2",'') AS "DriverName2"
			,IFNULL(G."U_ShortName",G."CardName") AS "Transporter"
			,IFNULL((SELECT 
					STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACEOFLOADING" AND T1."U_DEPARTMENT"='SEA&AIR'
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "PlaceOfLoading"
			,IFNULL((SELECT 
					STRING_AGG(T1."Name",',')
				FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACEOFDELIVERY" AND T1."U_DEPARTMENT"='SEA&AIR'
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "PlaceOfDelivery"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TB_TFW_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_THAIFORWARDER"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "ThaiForwarder"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TB_OSFW_SEA_AIR" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_OVERSEAFORWARDER"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "OverseaForwarder"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBOVERSEATRANSPORT" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_OVEARSEATRANSPORT"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "OverseaTrucker"
			,IFNULL((SELECT 
					STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBCUSTOMER" AS T0
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CUSTOMERCODE"
				WHERE T0."DocEntry"=A."DocEntry"),'') AS "CO"
			,IFNULL(A."U_REMARKLOLOYARD",'') AS "LOLOYard"
			,'' AS "STUFFING_UNSTUFFING"
			,CASE WHEN A."CreateDate"<'2024-03-06' THEN
				CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "U_BOOKINGID"=A."DocEntry" AND "Status"='O' AND "CreateDate">='2024-03-06')=0 THEN
					IFNULL(AA."U_JOBTYPE",'')||(SELECT TO_VARCHAR(A."U_BOOKINGDATE",'YYMM')
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
				ELSE IFNULL(AA."U_JOBTYPE",'')||A."DocNum" END AS "JOBNO"
			,IFNULL(I."firstName"||' - '||I."lastName",'') AS "RecordedBy"
			,IFNULL(C."U_SEALNO1",'') AS "Seal1"
			,IFNULL(C."U_SEALNO2",'') AS "Seal2"
			,IFNULL(C."U_TRAILERPROVINCE",'') AS "TrailerProvince"
			,IFNULL(C."U_TRAILERPLATE",'') AS "TrailerNo"
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS AA ON AA."Code"=A."U_IMPORTTYPE"
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS B ON B."DocEntry"=A."U_CONFIRMBOOKINGSHEETID" --Get Confirm Booking Sheet
		LEFT JOIN EW_PRD_TEST."@TRUCK_INFO_S_A" AS C ON C."DocEntry"=B."DocEntry" -- Get Information of Truck
		LEFT JOIN EW_PRD_TEST."OLCT" AS D ON TO_VARCHAR(D."Code")=C."U_TRUCKPROVINCE" --Get Province Of Truck
		LEFT JOIN EW_PRD_TEST."OITM" AS E ON E."ItemCode"=C."U_TRUCKCODE" --Get Truck Number
		LEFT JOIN EW_PRD_TEST."OHEM" AS F ON F."empID"=C."U_DRIVER1" --Get Driver Name 1
		LEFT JOIN EW_PRD_TEST."OHEM" AS FF ON FF."empID"=C."U_DRIVER2" --Get Driver Name 2
		LEFT JOIN EW_PRD_TEST."OCRD" AS G ON G."CardCode"=C."U_VENDORCODE" --Get Transporter
		LEFT JOIN EW_PRD_TEST."@TBUSER" AS H ON H."Code"=A."U_CreateBy"
		LEFT JOIN EW_PRD_TEST."OHEM" AS I ON I."empID"=H."U_EMPLOYEEID"	
		--SELECT * FROM EW_PRD_TEST."@TBUSER"
		WHERE A."U_LOADINGDATE" BETWEEN :par1 AND :par2  
			AND 1=CASE WHEN :par3 IN ('2','-99') THEN 1 ELSE 2 END
	);	
END;
	
	
		


