ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLREPORT_CONTAINER_STATUS(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250),
	in par3 NVARCHAR(250)
)
AS
BEGIN
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
				SELECT DISTINCT
					T0."U_VOLUMECODE" AS "Volumn" --SUM(T0."U_QTY")||' x '||
				FROM EW_PRD_TEST."@VOLUME" AS T0 
				WHERE T0."DocEntry"=A."DocEntry"
				--GROUP BY T0."U_VOLUMECODE"
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
		,IFNULL(A."U_LOLOYARDRemark",'') AS "LOLOYard"
		,'' AS "STUFFING_UNSTUFFING"
		,IFNULL(A."U_EWSeries",'')||IFNULL(A."U_JOBNO",'') AS "JOBNO"
		,IFNULL(I."firstName"||' - '||I."lastName",'') AS "RecordedBy"
		,IFNULL(C."U_SEALNO1",'') AS "Seal1"
		,IFNULL(C."U_SEALNO2",'') AS "Seal2"
		,IFNULL(C."U_TRAILERPROVINCE",'') AS "TrailerProvince"
		,IFNULL(C."U_TRAILERPLATE",'') AS "TrailerNo"
		,J."descript"||'-'||JJ."descript" AS "Route"
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
	LEFT JOIN EW_PRD_TEST."OTER" AS J ON J."territryID"=A."U_ORIGIN"-- Get Origin
	LEFT JOIN EW_PRD_TEST."OTER" AS JJ ON JJ."territryID"=A."U_DESTINATION"-- Get Destination
	--SELECT * FROM EW_PRD_TEST."@TBUSER"
	WHERE A."U_LOADINGDATE" BETWEEN :par1 AND :par2 
	AND IFNULL(C."U_CONTAINERNO",'')=CASE WHEN :par3='-99' THEN IFNULL(C."U_CONTAINERNO",'') ELSE :par3 END
	ORDER BY A."U_LOADINGDATE";
END;
	
	
		


