ALTER PROCEDURE EW_PRD_TEST."_USP_CALL_LAYOUT_BOOKINGSHEET_SEA_AIR"(
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
			IFNULL(B."CardName",'') AS "Customer"
			,CASE WHEN IFNULL(A."U_JOBNO",'')!='' THEN
						IFNULL(C."U_JOBTYPE",'')||A."U_JOBNO"
					 ELSE 
						CASE WHEN A."CreateDate"<'2024-03-06' THEN
					 	CASE WHEN (SELECT COUNT(*) FROM EW_PRD_TEST."@CON_BOOKING_S_A" WHERE "U_BOOKINGID"=A."DocEntry" AND "Status"='O' AND "CreateDate">='2024-03-06')=0 THEN
						 		IFNULL(C."U_JOBTYPE",'')||(SELECT TO_VARCHAR(A."U_BOOKINGDATE",'YYMM')
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
						  ELSE IFNULL(C."U_JOBTYPE",'')||A."DocNum" END
					 END AS "JobNo"
			,B."ContactPersonName" AS "Attn"
			,IFNULL(A."U_BOOKINGNO",'') AS "BookingNo"
			,IFNULL(B."Tel",'') AS "Tel"
			,IFNULL(A."U_DOCUMENTREQUIREMENT",'') AS "Remark"
			,IFNULL(A."U_FREIGHT",'') AS "Freight"
			,IFNULL(D."PlaceOfLoading",'') AS "POL"
			,IFNULL(E."PlaceOfDelivery",'') AS "POD"
			,IFNULL(F."Shipper",'') AS "Shipper"
			,CASE WHEN A."U_LCL_FCL_CY_CFS"='LCL' THEN TO_VARCHAR(TO_DECIMAL(A."U_CBM",16,2))||' CBM' ELSE IFNULL(G."Volume",'') END AS "Volume"
			,IFNULL(A."U_FEEDER",'') AS "FeederVessel"
			,IFNULL(A."U_FEEDERVOY",'') AS "FEEDERVOY"
			,IFNULL(A."U_VESSEL",'') AS "MotherVessel"
			,IFNULL(A."U_VESSELVOY",'') AS "VESSELVOY"
			,IFNULL(TO_VARCHAR(A."U_ETDREQUIREMENT",'DD/MM/YYYY'),'') AS "ETD"
			,IFNULL(TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY'),'') AS "ETA"
			,IFNULL(H."ShippingLine",'') AS "Carrier"
			,IFNULL(TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY'),'') AS "DateCFS"
			,IFNULL(I."LoadingAT",'') AS "AtCFS"
			,IFNULL(CAST(A."U_CLOSINGTIME" AS TIME),'') AS "ClosingTimeCFS"
			,TO_VARCHAR(A."U_CYDATE",'DD/MM/YYYY') AS "DateCY"
			,IFNULL(J."AtCY",'') AS "AtCY"
			,IFNULL(J."ContactPersonCYAT",'') AS "ContactCY"
			,IFNULL(TO_VARCHAR(A."U_RETURNDATE",'DD/MM/YYYY'),'') AS "DateRTN"
			,IFNULL(L."ReturnAT",'') AS "AtRTN"
			,IFNULL(L."ContactPersonReturnAT",'') AS "ContactRTN"
			,IFNULL(TO_VARCHAR(A."U_LASTRETURNDATE",'DD/MM/YYYY'),'') AS "ClosingTimeRTN"
			,'' AS "PaperlessCode"
			,IFNULL(CAST(A."U_RETURNBEFORE" AS TIME),'') AS "ClosingHoursRTN"
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A -- Get Information from BookingSheet Sea and Air
		LEFT JOIN (
			SELECT 
				 MAX(A."DocEntry") AS "DocEntry"
				,STRING_AGG(B."CardName",' , ') AS "CardName"
				,STRING_AGG((SELECT MAX("Cellolar") FROM EW_PRD_TEST."OCPR" WHERE "CardCode"=A."U_CUSTOMERCODE"),',') AS "Tel"
				,STRING_AGG((SELECT MAX("Name") FROM EW_PRD_TEST."OCPR" WHERE "CardCode"=A."U_CUSTOMERCODE"),',') AS "ContactPersonName"
			FROM EW_PRD_TEST."@TBCUSTOMER" AS A 
			LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CUSTOMERCODE"
			GROUP BY A."DocEntry"
		)AS B ON B."DocEntry"=A."DocEntry" -- Get Mutiple Customer
	LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS C ON C."Code"=A."U_IMPORTTYPE" -- Get JobSeries
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."Name"||','||B."U_COUNTRY",',') AS "PlaceOfLoading"
		FROM EW_PRD_TEST."@TB_POL_SEA_AIR" AS A 
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFLOADING"
		GROUP BY A."DocEntry"
	)AS D ON D."DocEntry"=A."DocEntry" -- Get List of Place loading
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."Name"||','||B."U_COUNTRY",',') AS "PlaceOfDelivery"
		FROM EW_PRD_TEST."@TB_POD_SEA_AIR" AS A 
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACEOFDELIVERY"
		GROUP BY A."DocEntry"
	)AS E ON E."DocEntry"=A."DocEntry" -- Get List of Place Delivery
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
			,STRING_AGG(TO_DECIMAL(A."U_Qty",12,0)||' X '||A."U_ContainerType",',') AS "Volume"
		FROM EW_PRD_TEST."@TB_CON_S_A" AS A 
		GROUP BY A."DocEntry"
	)AS G ON G."DocEntry"=A."DocEntry" -- Get List of Volume OR Container Type
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "ShippingLine"
		FROM EW_PRD_TEST."@TBSHIPPINGLINE" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_SHIPPINGLINE"
		GROUP BY A."DocEntry"
	)AS H ON H."DocEntry"=A."DocEntry" -- Get List of Shipping Line
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "LoadingAT"
		FROM EW_PRD_TEST."@TB_LOADINGAT_S_A" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_LoadingAt"
		GROUP BY A."DocEntry"
	)AS I ON I."DocEntry"=A."DocEntry" -- Get List of LoadingAt
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "AtCY"
			,STRING_AGG((SELECT MIN(T0."Name") 
							FROM EW_PRD_TEST."OCPR" AS T0 
							WHERE "CardCode"=A."U_CYAt_Contact"),' , ') AS "ContactPersonCYAT"
		FROM EW_PRD_TEST."@TB_CY_AT_S_A" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_CYAt_Contact"
		GROUP BY A."DocEntry"
	)AS J ON J."DocEntry"=A."DocEntry" -- Get List of CY AT
	LEFT JOIN (
		SELECT 
			 MAX(A."DocEntry") AS "DocEntry"
			,STRING_AGG(B."CardName",' , ') AS "ReturnAT"
			,STRING_AGG((SELECT MIN(T0."Name") 
							FROM EW_PRD_TEST."OCPR" AS T0 
							WHERE "CardCode"=A."U_ReturnAt_Contact"),' , ') AS "ContactPersonReturnAT"
		FROM EW_PRD_TEST."@TB_RETURNAT_S_A" AS A 
		LEFT JOIN EW_PRD_TEST."OCRD" AS B ON B."CardCode"=A."U_ReturnAt_Contact"
		GROUP BY A."DocEntry"
	)AS L ON L."DocEntry"=A."DocEntry" -- Get List of Return AT
	WHERE A."DocEntry"=:par2;
END;

