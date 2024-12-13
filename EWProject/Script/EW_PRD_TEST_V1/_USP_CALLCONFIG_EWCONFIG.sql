--CALL "EW_PRD_TEST"._USP_CALLCONFIG_EWCONFIG ('GetPlaceOfLoadingBookingSheet','1099','','','','')
ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLCONFIG_EWCONFIG(
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
	IF :DTYPE = 'LAYOUTPRINTER' THEN 
		SELECT 
			  A."U_FILENAME" AS FILENAME
			 ,A."U_EXPORTTYPE" AS EXPORTTYPE
			 ,A."U_STOREPROCEDURE" AS STOREPROCEDURE
			 ,A."U_PROPERTIES" AS PROPERTIES
			 ,IFNULL(A."U_LayoutPrintName",'') AS LAYOUTPRINTNAME
		FROM EW_PRD_TEST."@TBREPORT" AS A WHERE A."Code"=:par1;
	ELSE IF :DTYPE = 'GETPERMISSIONBYUSER' THEN 
		SELECT 
			 A."Code" AS "Code"
			,IFNULL(C."U_MODULETYPE",'') AS "IsPermission"
			,IFNULL(C."U_READONLY",'') AS "IsAllowReadOnly"
			,IFNULL(C."U_ADD",'') AS "IsAllowAdd"
			,IFNULL(C."U_UPDATE",'') AS "IsAllowUpdate"
			,IFNULL(C."U_CANCEL",'') AS "IsAllowCancel"
			,(SELECT CAST(COUNT(*) AS INT) FROM EW_PRD_TEST."@TBUSER_ROLE_LINE" AS T0 
				WHERE T0."Code"=A."U_USERROLE" 
					AND T0."U_MODULETYPE" LIKE '%(CBT)%'
					OR T0."U_MODULETYPE" = 'SalesQuotation') AS "CBTCount"
			,(SELECT CAST(COUNT(*) AS INT) FROM EW_PRD_TEST."@TBUSER_ROLE_LINE" AS T0 
				WHERE T0."Code"=A."U_USERROLE" 
					AND T0."U_MODULETYPE" LIKE '%(S&A)%') AS "S_A_Count"
			,(SELECT CAST(COUNT(*) AS INT) FROM EW_PRD_TEST."@TBUSER_ROLE_LINE" AS T0 
				WHERE T0."Code"=A."U_USERROLE" 
					AND T0."U_MODULETYPE" NOT LIKE '%(S&A)%' 
					AND T0."U_MODULETYPE" NOT LIKE '%(CBT)%'
					AND T0."U_MODULETYPE" != 'SalesQuotation') AS "OtherCount"
		FROM EW_PRD_TEST."@TBUSER" AS A 
		LEFT JOIN EW_PRD_TEST."@TBUSER_ROLE" AS B ON B."Code"=A."U_USERROLE"
		LEFT JOIN EW_PRD_TEST."@TBUSER_ROLE_LINE" AS C ON C."Code"=B."Code"
		WHERE A."Code"=CASE WHEN :par1='-99' THEN A."Code" ELSE :par1 END;
	ELSE IF :DTYPE='GetPlaceOfLoadingBookingSheet' THEN
		DECLARE OUTPUT_SPLITSTRING NVARCHAR(255);
		--LIBRARY:SPLIT_TO_TABLE(A."U_District",',')
		SELECT 
			 STRING_AGG(A."U_District",',') INTO OUTPUT_SPLITSTRING
		FROM EW_PRD_TEST."@PLACEOFLOADING" AS A
		WHERE A."DocEntry"=:par1 AND A."U_District" IS NOT NULL;
		SELECT 
			 B."Name" AS PLACEOFLOADING
			,IFNULL((SELECT STRING_AGG("Name",',') 
				FROM EW_PRD_TEST."@TBDISTRICT" 
				WHERE "Code" IN 
					(SELECT * FROM LIBRARY:SPLIT_TO_TABLE(OUTPUT_SPLITSTRING,','))),'') AS "District"
		FROM EW_PRD_TEST."@PLACEOFLOADING" AS A
		LEFT JOIN EW_PRD_TEST."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACELOADING"
		WHERE A."DocEntry"=:par1;
	END IF;
	END IF;
	END IF;
END 

