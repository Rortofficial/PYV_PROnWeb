ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_LAYOUT_SHIPPING_PARTICULAR_SEA_AIR(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	SELECT 
		IFNULL(F."Shipper",'') AS "Shipper"
		,IFNULL(G."Consignee",'') AS "Consignee"
		,'' AS "Notify"
		,IFNULL(A."CreateDate",'') AS "Dt"
		,A."DocEntry" AS "BookingNo"
		,IFNULL(H."Volume",'') AS "Volume"
		,IFNULL(TO_VARCHAR(A."U_CROSSBORDERDATE",'DD/MM/YYYY'),'') AS "ETD"
		,I."CardName" AS "TO"
		,I."ContactPersonName" AS "ATN"
		,I."Email" AS "EMAIL1"
		,'' AS "FROM"
		,'' AS "EMAIL2"
		,IFNULL(A."U_FEEDER",'')||IFNULL(' V.'||A."U_FEEDERVOY",'') AS "PRECARRIAGE"		
		,IFNULL(D."PortOfReceipt",'') AS "PORTOFRECEIPT"
		,IFNULL(A."U_VESSEL",'')||IFNULL(' V.'||A."U_VESSELVOY",'') AS "OCEANVESSELRAILTRUCK"
		,IFNULL(B."PlaceOfLoading",'') AS "PORTOFLOADING"
		,IFNULL(E."PortOfDischarge",'') AS "PORTOFDISCHARGE"
		,IFNULL(C."PlaceOfDelivery",'') AS "PORTOFDELIVERY"
		,A."U_FREIGHT" AS "FREIGHTPAYABLEAT"
		,TO_DECIMAL(A."U_GROSSWEIGHT",16,2) AS "GrossWeight"
		,A."U_GOODSDESCRIPTION" AS "GoodsDescription"
		,A."U_TOTALPACKAGE" AS "TotalPackage"
		,A."U_LCL_FCL_CY_CFS" AS "LCL_FCL_CY_CFS"
	FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A -- Get Information from BookingSheet Sea and Air
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."Name"||','||B."U_COUNTRY",',') AS "PlaceOfLoading"
		FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS A 
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFLOADING"
		GROUP BY A."DocEntry"
	)AS B ON B."DocEntry"=A."DocEntry" -- Get List of Place loading
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."Name"||','||B."U_COUNTRY",',') AS "PlaceOfDelivery"
		FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS A 
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFDELIVERY"
		GROUP BY A."DocEntry"
	)AS C ON C."DocEntry"=A."DocEntry" -- Get List of Place Delivery
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."Name"||','||B."U_COUNTRY",',') AS "PortOfReceipt"
		FROM EW_PRD_TEST."@TBPORTOFRECEIPT" AS A 
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PORTOFRECEIPT"
		GROUP BY A."DocEntry"
	)AS D ON D."DocEntry"=A."DocEntry" -- Get List of Port Receipt
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."Name"||','||B."U_COUNTRY",',') AS "PortOfDischarge"
		FROM EW_PRD_TEST."@TBPORTOFDISCHARGE" AS A 
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PORTOFDISCHARGE"
		GROUP BY A."DocEntry"
	)AS E ON E."DocEntry"=A."DocEntry" -- Get List of Port Discharge
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "Shipper"
		FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_SHIPPER"
		GROUP BY A."DocEntry"
	)AS F ON F."DocEntry"=A."DocEntry" -- Get List of Shipper
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "Consignee"
		FROM EW_PRD_TEST."@TBCONSIGNEE_SEA_AIR" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
		GROUP BY A."DocEntry"
	)AS G ON G."DocEntry"=A."DocEntry" -- Get List of Consignee
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(A."U_ContainerType",',') AS "Volume"
		FROM EW_PRD_TEST."@TB_CON_S_A" AS A 
		GROUP BY A."DocEntry"
	)AS H ON H."DocEntry"=A."DocEntry" -- Get List of Volume OR Container Type
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "CardName"
			,STRING_AGG((SELECT MAX("E_MailL") FROM EW_PRD_TEST."OCPR" WHERE "CardCode"=A."U_CUSTOMERCODE"),',') AS "Email"
			,STRING_AGG((SELECT MAX("Name") FROM EW_PRD_TEST."OCPR" WHERE "CardCode"=A."U_CUSTOMERCODE"),',') AS "ContactPersonName"
		FROM EW_PRD_TEST."@TBCUSTOMER" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CUSTOMERCODE"
		GROUP BY A."DocEntry"
	)AS I ON I."DocEntry"=A."DocEntry" -- Get Mutiple Customer
	WHERE A."DocEntry"=:par2;
END;
