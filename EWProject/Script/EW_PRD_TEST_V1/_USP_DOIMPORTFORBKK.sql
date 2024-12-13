ALTER PROCEDURE "EW_PRD_TEST"._USP_DOIMPORTFORBKK(
	in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
)
AS
BEGIN
USING SQLSCRIPT_STRING AS LIBRARY;
	DECLARE TEST_OUTPUT TABLE(RESULT NVARCHAR(5000));
	TEST_OUTPUT = LIBRARY:SPLIT_TO_TABLE(:par1,',');
	--SELECT TOP 1 * FROM :TEST_OUTPUT ORDER BY RESULT DESC;
	SELECT 
		 (C."U_EWSeries"||C."U_JOBNO") AS "JOBNO"
		,'' AS "DONOEWT"
		,(SELECT   
					STRING_AGG(Z2."CardName"||IFNULL(' - '||(SELECT 
							STRING_AGG(IFNULL(T0."Address",'')
							||IFNULL(T0."Address2",'')
							||IFNULL(T0."Address3",'')
							||','||T1."Name") AS ADDRESS1
					  		FROM EW_PRD_TEST."CRD1" AS T0 
					  		LEFT JOIN EW_PRD_TEST."OCRY" AS T1 ON T1."Code"=T0."Country" 
					  		WHERE T0."CardCode"=Z1."U_SHIPPER"),'')||', ')
				FROM EW_PRD_TEST."@BOOKINGSHEET" AS Z0
				LEFT JOIN EW_PRD_TEST."@TBSHIPPER" AS Z1 ON Z1."DocEntry"=Z0."DocEntry"
				LEFT JOIN EW_PRD_TEST."OCRD" AS Z2 ON Z2."CardCode"=Z1."U_SHIPPER"
				WHERE Z0."U_CONFIRMBOOKINGSHEETID"=A."DocEntry") AS "SHIPPER"
		,(SELECT   
					STRING_AGG(Z2."CardName"||IFNULL(' - '||(SELECT 
							STRING_AGG(IFNULL(T0."Address",'')
							||IFNULL(T0."Address2",'')
							||IFNULL(T0."Address3",'')
							||','||T1."Name") AS ADDRESS1
					  		FROM EW_PRD_TEST."CRD1" AS T0 
					  		LEFT JOIN EW_PRD_TEST."OCRY" AS T1 ON T1."Code"=T0."Country" 
					  		WHERE T0."CardCode"=Z1."U_CONSIGNEE"),'')||', ')
				FROM EW_PRD_TEST."@BOOKINGSHEET" AS Z0
				LEFT JOIN EW_PRD_TEST."@TBCONSIGNEE" AS Z1 ON Z1."DocEntry"=Z0."DocEntry"
				LEFT JOIN EW_PRD_TEST."OCRD" AS Z2 ON Z2."CardCode"=Z1."U_CONSIGNEE"
				WHERE Z0."U_CONFIRMBOOKINGSHEETID"=A."DocEntry") AS "CONSIGNEE"
		,TO_DECIMAL(C."U_GROSSWEIGHT",6,2)||' KGM' AS "GROSSWEIGHT"
		,TO_DECIMAL(C."U_NETWEIGHT",6,2)||' KGM' AS "NETWEIGHT"
		,D."ContactPersonCon" AS "ContactPersonShipper"
		,D."Phone1" AS "TelephoneShipper"
		,(SELECT STRING_AGG(T0."U_INVOICE",',') 
			FROM EW_PRD_TEST."@COMMODITY" AS T0 WHERE T0."DocEntry"=C."DocEntry") AS "ListInvoice"
		,E."ContactPersonCon" AS "ContactPersonConsignee"
		,E."Phone1" AS "TelephoneConsignee"
		,'' AS "QuantityDetails"
		,'' AS "CargoDetails"
		,B."TH_DriverName" AS "TH_DriverName"
		,B."U_TRUCKCODE" AS "TH_TruckNo"
		,B."U_TRAILERPLATE" AS "TH_TrailerNo"
		,B."U_CONTAINERNO" AS "TH_ContainerNo"
		,B."TH_SealNo" AS "TH_SealNo"
		,F."PickUpPlace" AS "PickUpPlace"
		,TO_VARCHAR(C."U_LOADINGDATE",'DD-MONTH-YYYY') AS "PickDate"
		,'' AS "OverseaDriverName"
		,'' AS "OvearseaTruckNo"
		,'' AS "OvearseaTrailerNo"
		,'' AS "OverseaContainerNo"
		,'' AS "OverseaSealNo"
		,G."DeliveryPlace" AS "DeliveryPlace"
		,TO_VARCHAR(C."U_CROSSBORDERDATE",'DD-MONTH-YYYY') AS "DeliveryDate"
		,'' AS "OverseaCustom"
		,'' AS "OverseaWarehouse"
		,H."ContainerType" AS "ContainerType"
		,I."TruckType" AS "TruckType"
		,CASE WHEN C."U_LCLORFCL"='1' THEN 'LCL' ELSE 'FCL' END AS "LCL_FCL"
		,CASE WHEN C."U_LOLOYARDORUNLOADING"='1' THEN 'LOLO YARD' ELSE 'UNLOADING' END AS "LOLO_UNLOAD"
		,'' AS "Finished"
		,CASE WHEN '-2'='-2' THEN TO_VARCHAR(C."U_ETAREQUIREMENT",'DD-MONTH-YYYY')ELSE '' END AS "Date_Time"
		,C."U_SPECIALREQUEST" AS "SpecialRemarks"
	FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS A
	LEFT JOIN (
		SELECT DISTINCT
			 "DocEntry" AS "DocEntry"
			,STRING_AGG("U_TRUCKCODE",', ') AS "U_TRUCKCODE"
			,STRING_AGG("U_TRAILERPLATE",', ') AS "U_TRAILERPLATE"
			,STRING_AGG("U_CONTAINERNO",', ') AS "U_CONTAINERNO"
			,STRING_AGG(T1."firstName"||' '||T1."lastName",', ') AS "TH_DriverName"
			,STRING_AGG(T0."U_SEALNO1"||','||T0."U_SEALNO2",', ') AS "TH_SealNo"
		FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
		LEFT JOIN EW_PRD_TEST."OHEM" AS T1 ON T1."empID"=T0."U_DRIVER1"
		GROUP BY "DocEntry"
	) AS B ON B."DocEntry"=A."DocEntry"
	LEFT JOIN EW_PRD_TEST."@BOOKINGSHEET" AS C ON C."DocEntry"=A."U_BOOKINGID"
	LEFT JOIN (
		SELECT 
		    T1."DocEntry" AS "DocEntry"
		   ,MAX(T0."Name") AS "ContactPersonCon"
		   ,MAX(T0."Cellolar") AS "Phone1"
		FROM EW_PRD_TEST."OCPR" AS T0 
		LEFT JOIN EW_PRD_TEST."@TBSHIPPER" AS T1 ON T1."U_SHIPPER"=T0."CardCode"
		GROUP BY T1."DocEntry"
	)AS D ON D."DocEntry"=C."DocEntry"
	LEFT JOIN (
		SELECT 
		    T1."DocEntry" AS "DocEntry"
		   ,MAX(T0."Name") AS "ContactPersonCon"
		   ,MAX(T0."Cellolar") AS "Phone1"
		FROM EW_PRD_TEST."OCPR" AS T0 
		LEFT JOIN EW_PRD_TEST."@TBCONSIGNEE" AS T1 ON T1."U_CONSIGNEE"=T0."CardCode"
		GROUP BY T1."DocEntry"
	)AS E ON E."DocEntry"=C."DocEntry"
	LEFT JOIN (
		SELECT 
			 STRING_AGG(T1."Name",', ') AS "PickUpPlace"
			,T0."DocEntry"
		FROM EW_PRD_TEST."@PLACEOFLOADING" AS T0
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACELOADING"
		Group By T0."DocEntry"
	)AS F ON F."DocEntry"=C."DocEntry"
	LEFT JOIN (
		SELECT 
			 STRING_AGG(T1."Name",', ') AS "DeliveryPlace"
			,T0."DocEntry"
		FROM EW_PRD_TEST."@PLACEOFDELIVERY" AS T0
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS T1 ON T1."Code"=T0."U_PLACEDELIVERY"
		Group By T0."DocEntry"
	)AS G ON G."DocEntry"=C."DocEntry"
	LEFT JOIN(
		SELECT "DocEntry",STRING_AGG("U_VOLUMECODE",',') AS "ContainerType" FROM(
			SELECT DISTINCT
				  T0."U_VOLUMECODE"
				 ,"DocEntry"
			FROM EW_PRD_TEST."@VOLUME" AS T0
		)AS A GROUP BY "DocEntry"
	)AS H ON H."DocEntry"=C."DocEntry"
	LEFT JOIN(
		SELECT "DocEntry",STRING_AGG("U_TRUCKTYPE",',') AS "TruckType" FROM(
			SELECT DISTINCT
				  T0."U_TRUCKTYPE"
				 ,"DocEntry"
			FROM EW_PRD_TEST."@TBTRUCKTYPEROW" AS T0
		)AS A GROUP BY "DocEntry"
	)AS I ON I."DocEntry"=C."DocEntry"		
	WHERE A."DocEntry"=(SELECT TOP 1 * FROM :TEST_OUTPUT ORDER BY RESULT ASC) AND B."U_CONTAINERNO"=(SELECT TOP 1 * FROM :TEST_OUTPUT ORDER BY RESULT DESC);
END






