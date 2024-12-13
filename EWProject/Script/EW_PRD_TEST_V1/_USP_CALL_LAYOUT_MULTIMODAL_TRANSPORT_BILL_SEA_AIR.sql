ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_LAYOUT_MULTIMODAL_TRANSPORT_BILL_SEA_AIR(
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
		,'' AS "WayBillNo"
		,'' AS "ReferenceNos"
		,'' AS "ForwardingAgentReference"
		,IFNULL(A."U_FEEDER",'')||IFNULL(' V.'||A."U_FEEDERVOY",'') AS "PreCarriage"
		,IFNULL(D."PortOfReceipt",'') AS "PlaceOfReceipt"
		,IFNULL(A."U_VESSEL",'')||IFNULL(' V.'||A."U_VESSELVOY",'') AS "VesselVoyNo"
		,IFNULL(B."PlaceOfLoading",'') AS "PortOfLoading"
		,IFNULL(E."PortOfDischarge",'') AS "PortOfDischarge"
		,IFNULL(C."PlaceOfDelivery",'') AS "PlaceOfDelivery"
		,'' AS "MarkAndNumbers"
		,IFNULL(A."U_TOTALPACKAGE",'') AS "NumberOfPackages"
		,IFNULL(A."U_GOODSDESCRIPTION",'') AS "DescriptionOfGoods"
		,IFNULL(TO_DECIMAL(A."U_GROSSWEIGHT",16,2),0) AS "GrossWeight"
		,IFNULL(A."U_FREIGHT",'') AS "Freight"	
		,IFNULL(A."CreateDate",'') AS "IssueDate"
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
	WHERE A."DocEntry"=:par2;
END;
