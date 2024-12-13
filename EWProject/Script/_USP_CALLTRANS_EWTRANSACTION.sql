
ALTER PROCEDURE "EW_PRD"._USP_CALLTRANS_EWTRANSACTION(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	IF :DTYPE = 'GETSALEPERSON' THEN 
		SELECT 
			 A."SlpCode" AS "SlpCode"
			,A."SlpName" AS "SlpName" 
			,IFNULL(B."U_VendorCode",'') AS "VendorCode"
		FROM EW_PRD."OSLP" AS A
		LEFT JOIN EW_PRD."OHEM" AS B ON B."salesPrson"=A."SlpCode"
		ORDER BY A."SlpCode" ASC;
	ELSE IF :DTYPE='GETCHECKLOGIN' THEN
		SELECT CASE 
					WHEN :par1='admin1' and :par2='1234' THEN '1' 
					WHEN :par1='admin2' and :par2='1234' THEN '1' 
					WHEN :par1='Parichai' and :par2='1234' THEN '1' 
					WHEN :par1='Pummarin' and :par2='1234' THEN '1' 
					WHEN :par1='Surasaek' and :par2='1234' THEN '1' 
					WHEN :par1='Thitipong' and :par2='1234' THEN '1' 
					ELSE 0 END FROM dummy;
	ELSE IF :DTYPE='GETUSERINFO' THEN
		SELECT  CASE 
					WHEN :par1='admin1' and :par2='1234' THEN 'manager' 
					WHEN :par1='admin2' and :par2='1234' THEN 'EWL02' 
					WHEN :par1='Parichai' and :par2='1234' THEN 'EWL03' 
					WHEN :par1='Pummarin' and :par2='1234' THEN 'EWL04'
					WHEN :par1='Surasaek' and :par2='1234' THEN 'EWL05' 
					WHEN :par1='Thitipong' and :par2='1234' THEN 'EWL06' ELSE '0' END AS USERIDNAME
				,CASE 
					WHEN :par1='admin1' and :par2='1234' THEN '5' 
					WHEN :par1='admin2' and :par2='1234' THEN '101' 
					WHEN :par1='Parichai' and :par2='1234' THEN '209' 
					WHEN :par1='Pummarin' and :par2='1234' THEN '181'
					WHEN :par1='Surasaek' and :par2='1234' THEN '138' 
					WHEN :par1='Thitipong' and :par2='1234' THEN '92' END AS EMPID
				,CASE 
					WHEN :par1='admin1' and :par2='1234' THEN '1' 
					WHEN :par1='admin2' and :par2='1234' THEN '101' 
					WHEN :par1='Parichai' and :par2='1234' THEN '23' 
					WHEN :par1='Pummarin' and :par2='1234' THEN '25' 
					WHEN :par1='Surasaek' and :par2='1234' THEN '29' 
					WHEN :par1='Thitipong' and :par2='1234' THEN '30' END AS ID
				,CASE 
					WHEN :par1='admin1' and :par2='1234' THEN 'Lina Lina' 
					WHEN :par1='admin2' and :par2='1234' THEN 'AUTTHAPHON KALONG' 
					WHEN :par1='Parichai' and :par2='1234' THEN 'Parichai' 
					WHEN :par1='Pummarin' and :par2='1234' THEN 'Pummarin' 
					WHEN :par1='Surasaek' and :par2='1234' THEN 'Surasaek' 
					WHEN :par1='Thitipong' and :par2='1234' THEN 'Thitipong' END AS UserName
				,CASE 
					WHEN :par1='admin1' and :par2='1234' THEN 'EWL02-' 
					WHEN :par1='admin2' and :par2='1234' THEN 'EWL02-' 
					WHEN :par1='Parichai' and :par2='1234' THEN 'EWL02-' 
					WHEN :par1='Pummarin' and :par2='1234' THEN 'EWL02-' 
					WHEN :par1='Surasaek' and :par2='1234' THEN 'EWL02-' 
					WHEN :par1='Thitipong' and :par2='1234' THEN 'EWL02-' END AS Sereis
		FROM dummy;
	ELSE IF :DTYPE='GETROUTE' THEN
		SELECT A."territryID" AS CODE,A."descript" AS NAME FROM EW_PRD."OTER" AS A;
	ELSE IF :DTYPE='GETIMPORTTYPE' THEN
		SELECT A."Code" AS CODE,A."Name" AS NAME FROM EW_PRD."@TBJOBTYPE" AS A;
	ELSE IF :DTYPE='GETORIGIN' THEN
		SELECT A."territryID" AS "Code",A."descript" AS "Name" FROM EW_PRD."OTER" AS A;
	ELSE IF :DTYPE='GETDESTINATION' THEN
		SELECT A."territryID" AS "Code",A."descript" AS "Name" FROM EW_PRD."OTER" AS A;
	ELSE IF :DTYPE='GETSHIPPER' THEN
		SELECT A."CardCode" AS CUSTOMERCODE
			  ,A."CardName" AS CUSTOMERNAME
			  ,A."Balance" AS BALANCE
			  ,IFNULL(A."LicTradNum",'')AS TAXID
			  ,(SELECT 
					(SELECT IFNULL("Name",'') FROM EW_PRD."OCRY" WHERE "Code"="Country") 
				FROM EW_PRD."CRD1" WHERE "CardCode"=A."CardCode" AND "AdresType"='S') AS COUNTRY
		FROM EW_PRD."OCRD" AS A WHERE A."CardType"='C' AND  LEFT(A."CardCode",2)='SP';
	ELSE IF :DTYPE='GETSERVICETYPE' THEN
		SELECT A."Code" AS CODE
			  ,A."Name" AS NAME 
			  ,A."U_JOBTYPE" AS IMPORTTYPE 
		FROM EW_PRD."@TBSERVICETYPE" AS A;
	ELSE IF :DTYPE='GETCO' THEN
		SELECT A."CardCode" AS "CardCode"
			  ,A."CardName" AS "CardName"
			  ,A."Balance" AS "Balance"
			  ,IFNULL(A."LicTradNum",'')AS "TaxID"
		FROM EW_PRD."OCRD" AS A 
		WHERE A."CardType"='C' 
		  --AND A."CardCode"='VEUK00001' 
		  AND A."validFor"!='N' 
		  And A."frozenFor"!='Y';
	ELSE IF :DTYPE='CONSIGNEE' THEN
		SELECT A."CardCode" AS CUSTOMERCODE
			  ,A."CardName" AS CUSTOMERNAME
			  ,A."Balance" AS BALANCE
			  ,IFNULL(A."LicTradNum",'')AS TAXID 
			  ,(SELECT 
					(SELECT IFNULL("Name",'') FROM EW_PRD."OCRY" WHERE "Code"="Country") 
				FROM EW_PRD."CRD1" WHERE "CardCode"=A."CardCode" AND "AdresType"='S') AS COUNTRY
		FROM EW_PRD."OCRD" AS A WHERE A."CardType"='C' AND LEFT(A."CardCode",2)='CS';
	ELSE IF :DTYPE='THAITRUCKER' THEN
		/*SELECT A."Code" AS CODE
			  ,A."Name" AS NAME
		FROM EW_PRD."@TBTHAITRUCKER" AS A;*/
		SELECT A."CardCode" AS CODE
			  ,A."CardName" AS NAME 
		FROM EW_PRD."OCRD" AS A WHERE "CardType"='S';
	ELSE IF :DTYPE='OVERSEATRUCKER' THEN
		/*SELECT A."Code" AS CODE
			  ,A."Name" AS NAME
		FROM EW_PRD."@TBOVERSEATRUCKER" AS A;*/
		SELECT A."CardCode" AS CODE
			  ,A."CardName" AS NAME
			  ,(SELECT DISTINCT (SELECT TT0."Name" FROM EW_PRD."OCRY" AS TT0 WHERE TT0."Code"=T0."Country") 
			  		FROM EW_PRD."CRD1" AS T0 
			  	WHERE T0."Address"=A."ShipToDef" 
			  		  AND T0."AdresType"='S') AS COUNTRY
		FROM EW_PRD."OCRD" AS A WHERE "CardType"='S';
	ELSE IF :DTYPE='LOLOYARDORUNLOADING' THEN
		SELECT A."FldValue" AS CODE
			  ,A."Descr" AS NAME 
		FROM EW_PRD."UFD1" AS A 
		WHERE A."FieldID"='18' AND A."TableID"='@BOOKINGSHEET';
	ELSE IF :DTYPE='THAIFORWARDER' THEN
		/*SELECT A."Code" AS CODE
			  ,A."Name" AS NAME
		FROM EW_PRD."@TBTHAIFORWARDER" AS A;*/
		SELECT A."CardCode" AS CODE
			  ,A."CardName" AS NAME 
		FROM EW_PRD."OCRD" AS A WHERE "CardType"='S' 
						AND (SELECT "Country" FROM EW_PRD."CRD1" WHERE "Address"=A."ShipToDef" AND "CardCode"=A."CardCode")='TH';
	ELSE IF :DTYPE='OVERSEAFORWARDER' THEN
		/*SELECT A."Code" AS CODE
			  ,A."Name" AS NAME
		FROM EW_PRD."@TBOVERSEAFORWARDER" AS A;*/
		SELECT A."CardCode" AS CODE
			  ,A."CardName" AS NAME
			  ,(SELECT DISTINCT (SELECT TT0."Name" FROM EW_PRD."OCRY" AS TT0 WHERE TT0."Code"=T0."Country") 
			  		FROM EW_PRD."CRD1" AS T0 
			  	WHERE T0."Address"=A."ShipToDef" 
			  		  AND T0."AdresType"='S') AS COUNTRY
		FROM EW_PRD."OCRD" AS A WHERE "CardType"='S';
	ELSE IF :DTYPE='THAIBORDER' THEN
		SELECT A."Code" AS CODE
			  ,A."Name" AS NAME
		FROM EW_PRD."@TBTHAIBORDER" AS A;
	ELSE IF :DTYPE='LCLORFCL' THEN
		SELECT A."FldValue" AS CODE
			  ,A."Descr"  AS NAME
		FROM EW_PRD."UFD1" AS A WHERE A."FieldID"='21' AND A."TableID"='@BOOKINGSHEET';
	ELSE IF :DTYPE='CYORCFS' THEN
		SELECT A."FldValue" AS CODE
			  ,A."Descr"  AS NAME
		FROM EW_PRD."UFD1" AS A WHERE A."FieldID"='22' AND A."TableID"='@BOOKINGSHEET';
	ELSE IF :DTYPE='CONTAINERTYPE' THEN
		SELECT A."FldValue" AS CODE
			  ,A."Descr"  AS NAME
		FROM EW_PRD."UFD1" AS A WHERE A."FieldID"='23' AND A."TableID"='@BOOKINGSHEET';
	ELSE IF :DTYPE='PLACEOFLOADING' THEN
		SELECT A."Code" AS CODE
			  ,A."Name"||', '||A."U_COUNTRY" AS NAME
			  ,A."U_COUNTRY" AS COUNTRY
		FROM EW_PRD."@TBPLACEOFLOADING" AS A;
	ELSE IF :DTYPE='PLACEOFDELIVERY' THEN
		SELECT A."Code" AS CODE
			  ,A."Name"||', '||A."U_COUNTRY" AS NAME
		FROM EW_PRD."@TBPLACEOFDELIVERY" AS A;
	ELSE IF :DTYPE='VOLUME' THEN
		SELECT A."Code" AS CODE
			  ,A."Name" AS NAME
		FROM EW_PRD."@TBVOLUME" AS A;
		/*UNION ALL 
		SELECT 'NONE' AS CODE, 'NONE' AS NAME FROM DUMMY;*/
	ELSE IF :DTYPE='GETBOOKINGDOCNUMMAX' THEN
		--totext(Year({Subscriptions.BILL_BEGIN}),0,'','')
		SELECT IFNULL(MAX(A."DocEntry")+1,1) AS MAXDOCNUM
			  ,TO_VARCHAR(CURRENT_DATE,'YY')||MONTH(CURRENT_DATE)||CASE WHEN LENGTH(IFNULL(MAX(A."DocEntry"),1))>=4 
			  					THEN 
			  						CAST(MAX(A."DocEntry"+1) AS NVARCHAR(100))
			  					ELSE 
			  						CASE WHEN LENGTH(IFNULL(MAX(A."DocEntry"),1))=1
			  						THEN
			  							'000'||CAST(IFNULL(MAX(A."DocEntry"+1),1) AS NVARCHAR(100))
			  						WHEN LENGTH(MAX(A."DocEntry"))=2
			  						THEN
			  							'00'||CAST(MAX(A."DocEntry"+1) AS NVARCHAR(100))
			  						WHEN LENGTH(MAX(A."DocEntry"))=3
			  						THEN
			  							'0'||CAST(MAX(A."DocEntry"+1) AS NVARCHAR(100))
			  						END
			  					END AS JOBNO
		FROM EW_PRD."@BOOKINGSHEET" AS A;
	ELSE IF :DTYPE='CONVERTBOOKINGDOCNUMTOJOBNO' THEN
		--totext(Year({Subscriptions.BILL_BEGIN}),0,'','')
		SELECT TO_VARCHAR(CURRENT_DATE,'YY')||MONTH(CURRENT_DATE)||CASE WHEN LENGTH(IFNULL(MAX(:par1),1))>=4 
			  					THEN 
			  						CAST(MAX(:par1) AS NVARCHAR(100))
			  					ELSE 
			  						CASE WHEN LENGTH(IFNULL(MAX(:par1),1))=1
			  						THEN
			  							'000'||CAST(IFNULL(MAX(:par1),1) AS NVARCHAR(100))
			  						WHEN LENGTH(MAX(:par1))=2
			  						THEN
			  							'00'||CAST(MAX(:par1) AS NVARCHAR(100))
			  						WHEN LENGTH(MAX(:par1))=3
			  						THEN
			  							'0'||CAST(MAX(:par1) AS NVARCHAR(100))
			  						END
			  					END AS JOBNO
		FROM dummy;
	ELSE IF :DTYPE='ListBookingSheet' THEN
		IF :par1='BookingSheet' THEN
			SELECT 
				  A."DocEntry" AS BOOKINGID
				 ,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
				 ,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				 ,B."Name" AS IMPORTYPE
				 ,C."CardName" AS CO
				 ,DD."descript"||'-'|| D."descript" AS ROUTE
				 ,E."U_NAME" AS CREATEBY
				 ,CASE WHEN A."Status"='C' THEN 
				 	'Cancel'
				  WHEN A."Status"='O' THEN
				  	CASE WHEN (SELECT COUNT(*)
				  				FROM EW_PRD."@JOBSHEETRUCKING" 
				  				WHERE "U_CONFIRMBOOKINGID"=(SELECT T0."DocEntry" 
				  											 FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS T0 
				 											 	WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O'))=1 THEN
				  	'JOB SHEET TRUCKING'
				  	WHEN (SELECT COUNT(T0."DocEntry") 
				  			FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS T0 
				 				WHERE T0."U_BOOKINGID"=A."DocEntry")=1 THEN
				 		'CONFIRM BOOKING SHEET'
				 	ELSE
				 		'BOOKING SHEET'
				  	END
				  END AS DOCSTATUS
				 ,A."Status" AS DOCSTATUSCANCEL
				 ,CASE WHEN 
				 	(SELECT COUNT(*) FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS T0 WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')=0 THEN 
				 		'O' 
				 	ELSE 
				 	'C' END AS "UPDATESTATUS"
				 ,A."DocEntry" AS DOCENTRY
			FROM EW_PRD."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
			LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=A."U_UserCreate"
			WHERE A."U_UserCreate"=CASE WHEN '-1'='-1' THEN A."U_UserCreate" ELSE A."U_UserCreate" END;--:par1='-1'
		ELSE IF :par1='ConfirmBookingSheet' THEN
			SELECT 
				  A."DocEntry" AS BOOKINGID
				 ,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
				 ,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS LOADINGDATE
				 ,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				 ,B."Name" AS IMPORTYPE
				 ,C."CardName" AS CO
				 ,C."CardCode" AS COCODE
				 ,DD."descript"||'-'|| D."descript" AS ROUTE
				 ,F."SlpName" AS SALENAME
				 ,A."U_SALEEMPLOYEE" AS "SlpCode"
				 ,E."U_NAME" AS CREATEBY
			FROM EW_PRD."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
			LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=A."U_UserCreate"
			LEFT JOIN EW_PRD."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
			WHERE   A."U_UserCreate"=CASE WHEN '-1'='-1' THEN A."U_UserCreate" ELSE A."U_UserCreate" END 
					AND A."DocEntry" NOT IN (SELECT IFNULL(AA."U_BOOKINGID",0) FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS AA WHERE AA."Status"='O')
					AND A."Status"!='C';					 
		ELSE IF :par1='ConfirmBookingSheetJobSheet' THEN
			SELECT 
				  AA."DocEntry" AS BOOKINGID
				 ,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
				 ,A."U_BOOKINGDATE" AS BOOKINGDATE
				 ,B."Name" AS IMPORTYPE
				 ,C."CardName" AS CO
				 ,D."descript" AS ROUTE
				 ,E."U_NAME" AS CREATEBY
			FROM EW_PRD."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD."@CONFIRMBOOKINGSHEET" AS AA ON A."DocEntry"=AA."U_BOOKINGID"
			LEFT JOIN EW_PRD."@CONFIRMBOOKINGSHEET" AS BB ON BB."U_BOOKINGID"=A."DocEntry"
			LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=A."U_UserCreate"
			WHERE   A."U_UserCreate"=CASE WHEN '-1'='-1' THEN A."U_UserCreate" ELSE A."U_UserCreate" END 
					AND A."DocEntry" IN (SELECT IFNULL(AA."U_BOOKINGID",0) FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS AA)
					AND A."Status"!='C'
					AND BB."Status"!='C'
					--AND A."DocEntry"='170'
					AND (SELECT COUNT(T0."U_DOCENTRY") FROM EW_PRD."@TBSALESQUOTATION" AS T0 WHERE T0."DocEntry"=A."DocEntry") 
							!= (SELECT COUNT(T0."U_SALEQUOTATIONDOCNUM") FROM EW_PRD."@JOBSHEETRUCKING" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry");
		END IF;
		END IF;
		END IF;
		
	ELSE IF :DTYPE='GetDetailBookingSheet' THEN
		SELECT 
			  A."DocEntry" AS BOOKINGID
			 ,F."SlpName" AS SALEEMPLOYEE
			 ,F."SlpCode" AS "SlpCode"
			 ,D."descript" AS ROUTE
			 ,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
			 ,DD."descript" AS ORIGIN
			 ,DDD."descript" AS DESTINATION
			 ,CC."CardName" AS SHIPPER
			 ,CCC."CardName" AS CONSIGNEE
			 ,C."CardName" AS CO
			 ,B."Name" AS IMPORTYPE
			 ,A."U_LOLOYARDRemark" AS LOLOYARDREMARK
			 ,CASE WHEN A."U_LOLOYARDORUNLOADING"=1 THEN 'LOLOYARD' ELSE 'UNLOADING' END AS LOLOUNLOADING
			 ,CASE WHEN A."U_LCLORFCL"=1 THEN 'LCL' ELSE 'FCL' END AS LCLORFCL
		FROM EW_PRD."@BOOKINGSHEET" AS A
		--LEFT JOIN EW_PRD."UFD1" AS B ON B."FldValue"=A."U_IMPORTTYPE" AND B."FieldID"='1' AND B."TableID"='@BOOKINGSHEET'
		LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
		LEFT JOIN EW_PRD."OCRD" AS CC ON CC."CardCode"=A."U_SHIPPER"
		LEFT JOIN EW_PRD."OCRD" AS CCC ON CCC."CardCode"=A."U_CONSIGNEE"
		LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
		LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
		LEFT JOIN EW_PRD."OTER" AS DDD ON DDD."territryID"=A."U_DESTINATION"
		LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=A."U_UserCreate"
		LEFT JOIN EW_PRD."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetPlaceOfLoadingBookingSheet' THEN
		SELECT 
			 B."Name" AS PLACEOFLOADING
		FROM EW_PRD."@PLACEOFLOADING" AS A
		LEFT JOIN EW_PRD."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACELOADING"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetPlaceOfDeliveryBookingSheet' THEN
		SELECT 
			 B."Name" AS PLACEOFDELIVERY
		FROM EW_PRD."@PLACEOFDELIVERY" AS A
		LEFT JOIN EW_PRD."@TBPLACEOFDELIVERY" AS B ON B."Code"=A."U_PLACEDELIVERY"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetVolumeBookingSheet' THEN
		SELECT 
			 A."U_QTY" AS QTY
			,B."Code" AS VOLUMECODE
			,B."Name" AS VOLUME
			,B."U_CONTAINERNUMBER" AS CONTAINERNUMBER
			,B."DocEntry" AS DOCENTRY
			,A."U_GROSSWEIGHT" AS GROSSWEIGHT
			,'C' AS TYPEOFCONTAINER
		FROM EW_PRD."@VOLUME" AS A
		LEFT JOIN EW_PRD."@TBVOLUME" AS B ON B."Code"=A."U_VOLUMECODE"
		WHERE A."DocEntry"=:par1--;
		UNION ALL
		SELECT 
			 CAST(A."U_QTY" AS INT) AS QTY
			,A."U_TRUCKTYPE" AS VOLUMECODE
			,A."U_TRUCKTYPE" AS VOLUME
			,1 AS CONTAINERNUMBER
			,A."DocEntry" AS DOCENTRY
			,A."U_GROSSWEIGHT" AS GROSSWEIGHT
			,'T' AS TYPEOFCONTAINER
		FROM EW_PRD."@TBTRUCKTYPEROW" AS A
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetContianerList' THEN
		SELECT 
			 A."ItemCode" AS ITEMCODE
			,A."ItemName" AS ITEMNAME
			,A."DeprGroup" AS ITEMTYPE
			,B."Price" AS PRICE 
			,A."CardCode" AS VENDORCODE
			,C."CardName" AS VENDORNAME
			,A."AssetGroup" AS OWNER
			,A."U_YARD" AS YARD
			,A."U_FCL" AS LCLORFLC
			,A."U_LOLO" AS LOLOORUNLOADING
		FROM EW_PRD."OITM" AS A 
		LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."CardCode"
		WHERE A."ItemType"='F' AND A."AssetClass"='1710-006'; --AND A."ItmsGrpCod"='111' AND A."ItmsGrpCod"='111' AND A."ItmsGrpCod"='107'
		--UNION ALL
		--SELECT 
		--	 A."ItemCode" AS ITEMCODE
		--	,A."ItemName" AS ITEMNAME
		--	,'Rental Container' AS ITEMTYPE
		--	,B."Price" AS PRICE 
		--FROM EW_PRD."OITM" AS A 
		--LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1
		--WHERE A."ItemType"='F'; --AND A."ItmsGrpCod"='107';
	ELSE IF :DTYPE='GetEmployee' THEN
		SELECT 
			 A."empID" AS DriverCode
			,A."firstName"||'-'||A."lastName" AS DriverName
			,IFNULL(A."mobile",'')||IFNULL('/'|| A."homeTel",'') AS "HP"
			,A."U_DriverID" AS DriverID
			,A."govID" AS CARDID
			,TO_VARCHAR (TO_DATE(A."U_LicenseExp"), 'YYYY/MM/DD') AS LICENSEEXPDATE 
		FROM EW_PRD."OHEM" AS A; --WHERE A."position"=1;
	ELSE IF :DTYPE='GetTruckNo' THEN
		IF :par1='InformationTruck' THEN
			SELECT 
				 A."ItemCode" AS ITEMCODE
				,A."ItemName" AS ITEMNAME
				,A."CardCode" AS VENDORCODE
				,C."CardName" AS VENDORNAME
				,'Truck' AS ITEMTYPE
				,B."Price" AS PRICE
				,D."empID" AS DRIVERID
				,D."firstName"||'-'||D."lastName" AS DRIVERNAME
				,IFNULL(D."mobile",'')|| IFNULL('/'||D."homeTel",'') AS "HP"
				,D."U_DriverID" AS DRIVERCARDID
				,TO_VARCHAR (TO_DATE(D."U_LicenseExp"), 'YYYY/MM/DD') AS LICENSEEXPDATE 
				,DD."empID" AS DRIVERID1
				,DD."firstName"||'-'||DD."lastName" AS DRIVERNAME1
				,IFNULL(DD."mobile",'')|| IFNULL('/'||DD."homeTel",'') AS "HP1"
				,DD."U_DriverID" AS DRIVERCARDID1
				,TO_VARCHAR (TO_DATE(DD."U_LicenseExp"), 'YYYY/MM/DD') AS LICENSEEXPDATE1
			FROM EW_PRD."OITM" AS A 
			LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."CardCode"
			LEFT JOIN EW_PRD."OHEM" AS D ON D."empID"=A."U_DRIVER1"
			LEFT JOIN EW_PRD."OHEM" AS DD ON DD."empID"=A."U_DRIVER2"
			WHERE A."ItemType"='F' AND A."AssetClass"='1710-007';
		ELSE IF :par1='Province' THEN
			SELECT 
				  A."Code" AS CODE
				 ,A."Location" AS NAME
			FROM EW_PRD."OLCT" AS A;
		ELSE IF :par1='PLATENO' THEN
			SELECT 
				  A."ItemCode" AS CODE
				 ,A."InventryNo" AS NAME
				 ,'' AS TRUCKTYPE
				 ,B."AttriTxt3" AS BRAND
				 ,B."AttriTxt4" AS COLOR
				 ,B."AttriTxt2" AS TRAILERPROVINCE
				 ,B."AttriTxt1" AS TRAILERPLATENO
				 ,B."AttriTxt8"||' - '|| B."AttriTxt9" ||' - '||B."AttriTxt10" AS TRAILERTYPE
			FROM EW_PRD."OITM" AS A 
			LEFT JOIN EW_PRD."ITM13" AS B ON B."ItemCode"=A."ItemCode" 
			WHERE A."ItemType"='F' AND A."Location"=:par2;
		END IF;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetVendor' THEN
		IF :par1='Normal' THEN
			SELECT A."CardCode" AS CUSTOMERCODE
				  ,A."CardName" AS CUSTOMERNAME
				  ,A."Balance" AS BALANCE
				  ,IFNULL(A."LicTradNum",'')AS TAXID 	
				  ,A."DflAccount" AS BANKACCOUNT
				  ,A."DflBranch" AS BRANCH	  
				  ,(SELECT "OCRY"."Name" FROM EW_PRD."OCRY" WHERE "OCRY"."Code"=A."BankCountr") AS BANKCOUNTRY
				  ,(SELECT "ODSC"."BankName" FROM EW_PRD."ODSC" WHERE "ODSC"."BankCode"=A."BankCode") AS BANKNAME
				  ,A."DflSwift" AS SWIFTCODE		  
			FROM EW_PRD."OCRD" AS A 
			WHERE A."CardType"='S';
		ELSE IF :par1='Advance' THEN
			SELECT A."CardCode" AS CUSTOMERCODE
				  ,A."CardName" AS CUSTOMERNAME
				  ,A."Balance" AS BALANCE
				  ,IFNULL(A."LicTradNum",'')AS TAXID 	
				  ,A."DflAccount" AS BANKACCOUNT
				  ,A."DflBranch" AS BRANCH	  
				  ,(SELECT "OCRY"."Name" FROM EW_PRD."OCRY" WHERE "OCRY"."Code"=A."BankCountr") AS BANKCOUNTRY
				  ,(SELECT "ODSC"."BankName" FROM EW_PRD."ODSC" WHERE "ODSC"."BankCode"=A."BankCode") AS BANKNAME
				  ,A."DflSwift" AS SWIFTCODE		  
			FROM EW_PRD."OCRD" AS A 
			WHERE A."CardType"='S' AND A."U_BP_TYPE"='E';
		END IF;
		END IF;
			  --AND A."CardCode"='VEUK00001';
	ELSE IF :DTYPE='GetInvoice' THEN
		SELECT 
			A."U_INVOICE" AS INVOICE
		FROM EW_PRD."@COMMODITY" AS A WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETJOBNOBYID' THEN
		SELECT A."U_EWSeries"||A."U_JOBNO" AS EWSeriesNo FROM EW_PRD."@BOOKINGSHEET" AS A WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETROUTEBYID' THEN
		SELECT "U_DESTINATION" AS ROUTE FROM EW_PRD."@BOOKINGSHEET" AS A WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='ListConfirmBookingSheet' THEN
		SELECT 
			 AA."DocEntry" AS CONFIRMBOOKINGID
			,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
			,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
			,B."Name" AS IMPORTYPE
			,C."CardName" AS CO
			,D."descript" AS ROUTE
			,E."U_NAME" AS CREATEBY
			,AA."DocEntry" AS DOCENTRY
			,CASE WHEN (SELECT COUNT(*) FROM EW_PRD."@JOBSHEETRUCKING" AS T0 WHERE T0."U_CONFIRMBOOKINGID"=AA."DocEntry")=0 THEN 
				AA."Status" 
			 ELSE 'C' END  AS DOCSTATUSCANCEL
			,AA."U_BOOKINGID" AS BOOKINGDOCENTRY
			,AA."U_PROJECTMANAGEMENTID" AS PROJECTDOCENTRY
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS AA
		LEFT JOIN EW_PRD."@BOOKINGSHEET" AS A ON A."DocEntry"=AA."U_BOOKINGID"
		LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
		LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
		LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=AA."U_CreateUser"
		WHERE A."U_UserCreate"=CASE WHEN '-1'='-1' THEN A."U_UserCreate" ELSE A."U_UserCreate" END
			  AND AA."U_BOOKINGID" IS NOT NULL
			  AND AA."U_PROJECTMANAGEMENTID" IS NOT NULL;
	ELSE IF :DTYPE='GetCurrencyByCardCode' THEN
		IF(SELECT "Currency" FROM EW_PRD."OCRD" WHERE "CardCode"=:par1)='##' THEN
			SELECT 
				 A."CurrCode" AS "CurrencyCode"
				,B."ChkName" AS "CurrencyName"
				,IFNULL(C."Rate",0) AS "ExchangeRateSystemCurrency" --USS
				,IFNULL(CC."Rate",0) AS "ExchangeRateLocalCurrency" --THS
				,IFNULL(CCC."Rate",0) AS "EXCHANGERATE" --Base Currency
				,A."Locked" AS "Defualt"
			FROM EW_PRD."CRD13" AS A
			LEFT JOIN EW_PRD."OCRN" AS B ON A."CurrCode"=B."CurrCode"
			LEFT JOIN EW_PRD."ORTT" AS C ON C."Currency"='USS' AND C."RateDate"=CURRENT_DATE --USD
			LEFT JOIN EW_PRD."ORTT" AS CC ON CC."Currency"='THB' AND CC."RateDate"=CURRENT_DATE --THB
			LEFT JOIN EW_PRD."ORTT" AS CCC ON CCC."Currency"=A."CurrCode" AND CCC."RateDate"=CURRENT_DATE --Currency Base
			WHERE "CardCode"=CASE WHEN :par1='' THEN "CardCode" ELSE :par1 END AND RIGHT(B."CurrCode",1)!='S';
		ELSE
			IF :par1='' THEN
				SELECT
					 B."CurrCode" AS "CurrencyCode"
					,B."ChkName" AS "CurrencyName"
					,0.00 AS "ExchangeRateSystemCurrency" --USS
					,0.00 AS "ExchangeRateLocalCurrency" --THS
					,0.00 AS "EXCHANGERATE"
					,'Y' AS "Defualt"
				FROM EW_PRD."OCRN" AS B WHERE RIGHT(B."CurrCode",1)!='S'
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
					,IFNULL(CCC."Rate",0) AS "EXCHANGERATE"
					,'Y' AS "Defualt"
				FROM EW_PRD."OCRD" AS A
				LEFT JOIN EW_PRD."OCRN" AS B ON A."Currency"=B."CurrCode"
				LEFT JOIN EW_PRD."ORTT" AS C ON C."Currency"='USS' AND C."RateDate"=CURRENT_DATE --C."Currency"
				LEFT JOIN EW_PRD."ORTT" AS CC ON CC."Currency"='THB' AND CC."RateDate"=CURRENT_DATE
				LEFT JOIN EW_PRD."ORTT" AS CCC ON CCC."Currency"=A."Currency" AND CCC."RateDate"=CURRENT_DATE --Currency Base
				WHERE "CardCode"=:par1 ;--AND RIGHT(B."CurrCode",1)!='S';
			END IF;
		END IF;
	ELSE IF :DTYPE='GetItemDetailByType' THEN
		SELECT 
			 ROW_NUMBER ( ) OVER( ORDER BY A."ItemCode" DESC ) AS ROWNUM
			,A."ItemCode" AS ITEMCODE
			,A."ItemName" AS ITEMNAME
			,IFNULL(E."Price",0) AS SELLINGPRICE
			,IFNULL(D."Price",0) AS COSTINGPRICE
			,'I' AS ITEMTYPE
		FROM EW_PRD."OITM" AS A
		LEFT JOIN EW_PRD."OHEM" AS B ON B."empID"=:par1
		LEFT JOIN EW_PRD."OSLP" AS C ON C."SlpCode"=B."salesPrson"
		LEFT JOIN EW_PRD."ITM1" AS D ON D."ItemCode"=A."ItemCode" AND D."PriceList"=C."U_PriceList"
		LEFT JOIN EW_PRD."ITM1" AS E ON E."ItemCode"=A."ItemCode" AND E."PriceList"='1';
	ELSE IF :DTYPE='GetConfirmBookingSheetDetail' THEN
		SELECT 
			  AA."DocEntry" AS "BOOKINGID"
			 ,A."DocEntry" AS "CONFIRMBOOKINGID"
			 ,AA."U_EWSeries"||AA."U_JOBNO" AS "JOBNO"
			 ,DD."descript"||'-'||D."descript" AS "ROUTE"
			 ,TO_VARCHAR (AA."U_BOOKINGDATE", 'YYYY/MM/DD') AS "BOOKINGDATE"
			 ,F."SlpName" AS "SALEEMPLOYEE"
			 ,B."Name" AS "IMPORTTYPE"
			 ,CC."CardName" AS "SHIPPER"
			 ,AA."U_TOTALPACKAGE" AS "TOTALPACKAGE"
			 ,C."CardName" AS "CO"
			 ,C."CardCode" AS "COCODE"
			 ,AA."U_NETWEIGHT" AS "NETWEIGHT"
			 ,AA."U_GROSSWEIGHT" AS "GROSSWEIGHT"
			 ,CCC."CardName" AS "CONSIGNEE"
			 ,TO_VARCHAR (AA."U_LOADINGDATE", 'YYYY/MM/DD') AS "LOADINGDATE"
			 ,TO_VARCHAR (AA."U_CROSSBORDERDATE", 'YYYY/MM/DD') AS "CROSSBORDERDATE"
			 ,TO_VARCHAR (AA."U_ETAREQUIREMENT", 'YYYY/MM/DD') AS "ETAREQUIREMENT"
			 ,(SELECT 
			 	STRING_AGG(Z1."Name", ',') 
			 	FROM EW_PRD."@PLACEOFLOADING" AS Z0 
			 	LEFT JOIN EW_PRD."@TBPLACEOFLOADING" AS Z1 ON Z0."U_PLACELOADING"=Z1."Code"
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "PLACEOFLOADING"
			 ,(SELECT 
			 	STRING_AGG(Z1."Name", ',') 
			 	FROM EW_PRD."@PLACEOFDELIVERY" AS Z0
			 	LEFT JOIN EW_PRD."@TBPLACEOFDELIVERY" AS Z1 ON Z1."Code"=Z0."U_PLACEDELIVERY" 
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "PLACEOFDELIVERY"
			 ,AA."U_GOODSDESCRIPTION" AS "GOODSDESCRIPTION"
			 ,(SELECT 
			 	STRING_AGG(Z0."U_QTY"||Z1."Name", ',') 
			 	FROM EW_PRD."@VOLUME" AS Z0
			 	LEFT JOIN EW_PRD."@TBVOLUME" AS Z1 ON Z1."Code"=Z0."U_VOLUMECODE" 
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "VOLUME"
			 ,'' AS "THAIFORWARDER" --G."Name"
			 ,'' AS "OVERSEAFORWARDER" --H."Name"
			 ,(SELECT 
			 	STRING_AGG(Z1."Name", ',') 
			 	FROM EW_PRD."@THAIBORDER" AS Z0
			 	LEFT JOIN EW_PRD."@TBTHAIBORDER" AS Z1 ON Z1."Code"=Z0."U_ThaiBorder" 
			 	WHERE Z0."DocEntry"=AA."DocEntry") AS "THAIBORDER"
			 ,'' AS "OVERSEATRANSPORT" 
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@BOOKINGSHEET" AS AA ON AA."U_CONFIRMBOOKINGSHEETID"=A."DocEntry"
		LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=AA."U_IMPORTTYPE"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=AA."U_CO"
		LEFT JOIN EW_PRD."OCRD" AS CC ON CC."CardCode"=AA."U_SHIPPER"
		LEFT JOIN EW_PRD."OCRD" AS CCC ON CCC."CardCode"=AA."U_CONSIGNEE"
		LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=AA."U_DESTINATION"
		LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=AA."U_ORIGIN"
		--LEFT JOIN EW_PRD."OTER" AS DDD ON DDD."territryID"=AA."U_DESTINATION"
		LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=AA."U_UserCreate"
		LEFT JOIN EW_PRD."OSLP" AS F ON F."SlpCode"=AA."U_SALEEMPLOYEE"
		--LEFT JOIN EW_PRD."@TBTHAIFORWARDER" AS G ON G."Code"=AA."U_THAIFORWARDER"
		--LEFT JOIN EW_PRD."@TBOVERSEAFORWARDER" AS H ON H."Code"=AA."U_OVERSEAFORWARDER"
		--LEFT JOIN EW_PRD."@TBTHAIBORDER" AS I ON I."Code"=AA."U_THAIBORDER"
		--LEFT JOIN EW_PRD."@TBOVERSEATRUCKER" AS J ON J."Code"=AA."U_OVERSEATRUCKER"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetConfirmBookingSheetListInvoice' THEN
		SELECT 
			B."U_INVOICE" AS CODE
		FROM EW_PRD."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@COMMODITY" AS B ON A."DocEntry"=B."DocEntry"
		WHERE A."U_CONFIRMBOOKINGSHEETID"=:par1;
	ELSE IF :DTYPE='GetConfirmBookingSheetListTruck' THEN
		SELECT
			 A."U_TRUCKCODE" AS "TRUCKNO"
			,B."AttriTxt7" AS "TRUCKWEIGHT"
			,A."U_CONTAINERNO" AS "CONTAINERNO"
			,TO_VARCHAR(A."U_GROSSWEIGHT",'00 KG') AS "CONTAINERWEIGHT"
			,A."U_VENDORCODE" AS "TRANSPORTER"
		FROM EW_PRD."@TRUCKINFORMATION" AS A 
		LEFT JOIN EW_PRD."ITM13" AS B ON A."U_TRUCKCODE"=B."ItemCode"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetCostType' THEN
		SELECT A."ItemCode" ITEMCODE
			  ,A."ItemName" AS ITEMNAME
			  ,IFNULL(B."Price",0) AS PRICE
		FROM EW_PRD."OITM" AS A 
		LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1;
		--WHERE A."ItmsGrpCod"='102';
	ELSE IF :DTYPE='GETUSERBYID' THEN
		SELECT "USER_CODE" AS USERCODE FROM EW_PRD."OUSR" AS A WHERE A."USERID"=:par1;
	ELSE IF :DTYPE='GETSTATUSCANCELBOOKINGSHEET' THEN
		SELECT COUNT(*) AS ValueValid FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A WHERE A."U_BOOKINGID"=:par1 AND A."Status"='O';
	ELSE IF :DTYPE='GETSTATUSCANCELCONFRIMBOOKINGSHEET' THEN
		SELECT COUNT(*) AS ValueValid FROM EW_PRD."@JOBSHEETRUCKING" AS A WHERE A."U_CONFIRMBOOKINGID"=:par1 AND A."Status"='O';
	ELSE IF :DTYPE='GETSTATUSPURCHASEREQUESTAPPROVE' THEN
		SELECT COUNT(*) AS ValueValid FROM EW_PRD."@ADVANCEPAYMENT" AS A WHERE A."DocEntry"=:par1 AND A."U_ApproveStatus" IN ('A','R');
	ELSE IF :DTYPE='GETPROJECTNUMBERBYDOCENTRY' THEN
		SELECT "NAME" AS DOCENTRY FROM EW_PRD."OPMG" WHERE "AbsEntry"=:par1;
	ELSE IF :DTYPE='GetPurchaseRequestByDocEntry' THEN
		SELECT "DocNum" AS "DOCNUM" FROM EW_PRD."OPRQ" WHERE "DocEntry"=:par1;
	ELSE IF :DTYPE='GETPROJECTNUMBERBYDOCENTRYAPPROVE' THEN
		SELECT "AbsEntry" AS DOCENTRY FROM EW_PRD."OPMG" WHERE "NAME"=:par1;
	ELSE IF :DTYPE='GETREFTNOSALEQUOTATION' THEN
		SELECT 
			YEAR(CURRENT_DATE)||MONTH(CURRENT_DATE)||'-'||B."SlpCode"||'/'||IFNULL((SELECT MAX("DocEntry") FROM EW_PRD."OQUT"),1) AS "RefNo"
		FROM EW_PRD."OHEM" AS A 
		LEFT JOIN EW_PRD."OSLP" AS B ON A."salesPrson"=B."SlpCode"
		WHERE A."userId"=:par1;
	ELSE IF :DTYPE='GETCREDITTERM' THEN
		SELECT TO_VARCHAR(A."GroupNum") AS "Code",A."PymntGroup" AS "Name" FROM EW_PRD."OCTG" AS A;
	ELSE IF :DTYPE='GETSERVICESALESQUOTATION' THEN
		SELECT A."Code" AS "Code",A."Name" AS "Name" FROM EW_PRD."@TBSERVICE" AS A;
	ELSE IF :DTYPE='GETITEMSALESQUOTATION' THEN
		execute immediate 'SELECT 
			 A."ItemCode" AS "ItemCode"
			,A."ItemName" AS "ItemName"
			,TO_DECIMAL(IFNULL(B."Price",0),6,2) AS "Price" 
		FROM EW_PRD."OITM" AS A 
		LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1
		WHERE A."U_Dept_item" IN ('||:par1||');';
	ELSE IF :DTYPE='GETLISTSALEQUOTATION' THEN
		IF :par1='SALEQUOTATION' THEN
			SELECT A."CardName" AS "CustomerName"
				  ,A."CardCode" AS "CustomerCode"
				  ,TO_VARCHAR (A."DocDate", 'DD/MM/YYYY') AS "QuotationDate"
				  ,A."U_TEL" AS "Tel"
				  ,A."NumAtCard" AS "CustomerRefNo"
				  ,A."U_EMAIL" AS "Email"
				  ,IFNULL(DD."descript",'NONE')||'-'||IFNULL(D."descript",'NONE') AS "Route"
				  ,A."DocTotal" AS "DocTotal"
				  ,CASE WHEN (SELECT COUNT("U_SALESQUOTATION") 
				  		FROM EW_PRD."@BOOKINGSHEET" WHERE "U_SALESQUOTATION"=A."DocEntry")<>0
				  		THEN 
				  			'C' 
				  		ELSE 
				  			A."DocStatus" 
				  		END AS "DocStatus"
				  ,TO_VARCHAR(A."DocEntry") AS "DocEntry"
				  ,TO_VARCHAR(A."DocNum") AS "DocNum"
				  ,A."DocDueDate"
				  ,TO_VARCHAR(A."SlpCode") AS "SlpCode"
				  ,B."SlpName" AS "SlpName"
			FROM EW_PRD."OQUT" AS A 
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
			LEFT JOIN EW_PRD."OSLP" AS B ON A."SlpCode"=B."SlpCode"
			WHERE IFNULL(A."U_WEBID",'')!=''; --AND A."DocDueDate">=CURRENT_DATE;
		ELSE IF :par1='ADDBOOKINGSHEET' THEN
			SELECT A."U_CustomerName" AS CUSTOMERNAME 
				  ,TO_VARCHAR (A."DocDate", 'DD/MM/YYYY') AS QUOTATIONDATE
				  ,A."U_TEL" AS TEL
				  ,A."NumAtCard" AS CustomerRefNo
				  ,A."U_EMAIL" AS EMAIL
				  ,IFNULL(DD."descript",'NONE')||'-'||IFNULL(D."descript",'NONE') AS ROUTE
				  ,A."DocTotal" AS TOTAL
				  ,A."DocStatus" AS DOCSTATUS
				  ,A."DocEntry" AS DOCENTRY
			FROM EW_PRD."OQUT" AS A 
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
			WHERE IFNULL(A."U_WEBID",'')!='' 
			  AND "DocEntry" NOT IN(SELECT "U_SALESQUOTATION" 
			  							FROM EW_PRD."@BOOKINGSHEET" WHERE IFNULL("U_SALESQUOTATION",0)<>0)
			  AND A."DocStatus"<>'C';
		END IF;
		END IF;
	ELSE IF :DTYPE='GETSALEQUOTATIONJOBSHEET' THEN
		SELECT A."U_CustomerName" AS CUSTOMERNAME 
			  ,TO_VARCHAR (A."DocDate", 'YYYY/MM/DD') AS QUOTATIONDATE
			  ,A."U_TEL" AS TEL
			  ,A."NumAtCard" AS CustomerRefNo
			  ,A."U_EMAIL" AS EMAIL
			  ,IFNULL(DD."descript",'NONE')||'-'||IFNULL(D."descript",'NONE') AS ROUTE
			  ,A."DocTotal" AS TOTAL
			  ,A."DocNum" AS DOCUMENT
			  ,A."DocEntry" AS DOCENTRY
			  ,A."CardCode" AS CARDCODE
		FROM EW_PRD."OQUT" AS A 
		LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
		LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
		WHERE IFNULL(A."U_WEBID",'')!='' 
		AND A."DocEntry" IN (SELECT "U_DOCENTRY" FROM EW_PRD."@BOOKINGSHEET" T0 
												 LEFT JOIN EW_PRD."@TBSALESQUOTATION" T1 ON T0."DocEntry"=T1."DocEntry" 
												 WHERE T0."DocEntry"=
		(SELECT "U_BOOKINGID" FROM EW_PRD."@CONFIRMBOOKINGSHEET" WHERE "DocEntry"=:par1)
		AND "U_DOCENTRY" NOT IN (SELECT IFNULL("U_SALEQUOTATIONDOCNUM",0) FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS T0 
													  LEFT JOIN EW_PRD."@JOBSHEETRUCKING" AS T1 ON T0."DocEntry"=T1."U_CONFIRMBOOKINGID" 
													  WHERE T0."DocEntry"=:par1));
	ELSE IF :DTYPE='GETITEMSALEQUOTATIONJOBSHEET' THEN
		SELECT 
			 A."ItemCode" AS ITEMCODE
			,A."Dscription" AS ITEMNAME 
			,IFNULL(A."LineTotal",0) AS PRICESELLING
			,IFNULL(D."Price",0) AS PRICECOSTING
			,A."LineNum"+1 AS ROWNUM
			,'Q' AS ITEMTYPE
		FROM EW_PRD."QUT1" AS A 
		LEFT JOIN EW_PRD."OHEM" AS B ON B."empID"=:par2
		LEFT JOIN EW_PRD."OSLP" AS C ON C."SlpCode"=B."salesPrson"
		LEFT JOIN EW_PRD."ITM1" AS D ON D."ItemCode"=A."ItemCode" AND D."PriceList"=C."U_PriceList"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetAbsEntryProjectManagement' THEN
		SELECT 
			"AbsEntry" AS ABSENTRY
		FROM EW_PRD."OPMG" 
		WHERE "NAME"=:par1;
	ELSE IF :DTYPE='GETLASTROWSTAGE' THEN
		SELECT 
			IFNULL(MAX("POS")+1,1) AS LASTROWSTAGE 
		FROM EW_PRD."PMG1" WHERE "AbsEntry"=:par1;
	ELSE IF :DTYPE='GETSTAGEBYJOBNUMBER' THEN
		SELECT DISTINCT 
			"StageID" AS STAGEID
		FROM EW_PRD."PMG4" 
		WHERE "DocEntry"=:par1;
	ELSE IF :DTYPE='GetListJobSheet' THEN
		SELECT 
			 T0."DocEntry" AS JOBSHEETID
			,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
			,TO_VARCHAR(T0."CreateDate",'DD/MM/YYYY') AS JOBSHEETDATE
			,B."Name" AS IMPORTYPE
			,C."CardName" AS CO
			,D."descript" AS ROUTE
			,E."U_NAME" AS CREATEBY
			,T0."U_CONFIRMBOOKINGID" AS CONFIRMBOOKINGID
			,T0."U_COSTINGCONFIRM" AS COSTINGCONFIRMSTATUS
		FROM EW_PRD."@JOBSHEETRUCKING" AS T0
		LEFT JOIN EW_PRD."@CONFIRMBOOKINGSHEET" AS AA ON AA."DocEntry"=T0."U_CONFIRMBOOKINGID"
		LEFT JOIN EW_PRD."@BOOKINGSHEET" AS A ON A."DocEntry"=AA."U_BOOKINGID"
		LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
		LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
		LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=AA."U_CreateUser"
		WHERE A."U_UserCreate"=CASE WHEN '-1'='-1' THEN A."U_UserCreate" ELSE A."U_UserCreate" END;
	ELSE IF :DTYPE='GetAccountCodePurchaseRequest' THEN
		SELECT A."AcctCode" AS ACCOUNTCODE
			  ,A."AcctName" AS ACCOUNTNAME
			  ,A."CurrTotal" AS ACCOUNTBALANCE
		FROM EW_PRD."OACT" AS A 
		WHERE A."FatherNum" IS NOT NULL AND A."Postable"='Y';
	ELSE IF :DTYPE='GetItemCodePurchaseRequest' THEN
		IF :par1='Normal' THEN
			SELECT A."ItemCode" ITEMCODE
				  ,A."ItemName" AS ITEMNAME
				  ,IFNULL(B."Price",0) AS PRICE --A."UgpEntry"
				  ,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=A."UgpEntry") AS SERVICETYPE
			FROM EW_PRD."OITM" AS A 
			LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1
			WHERE A."U_Dept_item"='CBT';
		ELSE IF :par1='AdvanceForCustomer' THEN
			SELECT A."ItemCode" ITEMCODE
				  ,A."ItemName" AS ITEMNAME
				  ,IFNULL(B."Price",0) AS PRICE --A."UgpEntry"
				  ,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=A."UgpEntry") AS SERVICETYPE
			FROM EW_PRD."OITM" AS A 
			LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1
			WHERE A."ItmsGrpCod" IN ('113','114','115','116');
		ELSE IF :par1='ITEM-ALL' THEN
			SELECT A."ItemCode" ITEMCODE
				  ,A."ItemName" AS ITEMNAME
				  ,IFNULL(B."Price",0) AS PRICE --A."UgpEntry"
				  ,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=A."UgpEntry") AS SERVICETYPE
			FROM EW_PRD."OITM" AS A 
			LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1;
		END IF;
		END IF;
		END IF;		
	ELSE IF :DTYPE='GetMaxDocNumPurchaseRequest' THEN
		SELECT 
			IFNULL("NextNumber","InitialNum") AS MAXDOCNUM
		FROM EW_PRD."NNM1" AS A
		WHERE A."Indicator" IN (SELECT "Indicator" 
									FROM EW_PRD."OFPR" AS A 
								WHERE TO_VARCHAR(A."F_RefDate", 'YYYY/MM')=TO_VARCHAR(CURRENT_DATE, 'YYYY/MM'))
		AND A."ObjectCode"='1470000113';
	ELSE IF :DTYPE='GetSeriesDocNumPurchaseRequest' THEN
		SELECT 
			A."Series" AS SERIES
			,A."SeriesName" AS SERIESNAME
			,A."NextNumber" AS MAXDOCNUM
		FROM EW_PRD."NNM1" AS A
		WHERE A."Indicator" IN (SELECT "Indicator" 
									FROM EW_PRD."OFPR" AS A 
								WHERE TO_VARCHAR(A."F_RefDate", 'YYYY/MM')=TO_VARCHAR(CURRENT_DATE, 'YYYY/MM'))
		AND A."ObjectCode"='1470000113';
	ELSE IF :DTYPE='GetMaxDocNumIncomingPayment' THEN
		SELECT 
			IFNULL("NextNumber","InitialNum") AS MAXDOCNUM
			,A."Series" AS CODE
			,A."SeriesName" AS NAME
		FROM EW_PRD."NNM1" AS A
		WHERE A."Indicator" IN (SELECT "Indicator" 
									FROM EW_PRD."OFPR" AS A 
								WHERE TO_VARCHAR(A."F_RefDate", 'YYYY/MM')=TO_VARCHAR(CURRENT_DATE, 'YYYY/MM'))
		AND A."ObjectCode"='30';
	ELSE IF :DTYPE='GetListPurchaseRequest' THEN
		SELECT 
			 A."DocEntry" AS DOCENTRY
			,A."U_NumAtCard" AS DOCNUM
			,CASE 
				WHEN A."U_ApproveStatus"='P' THEN 'Pending' 
				WHEN A."U_ApproveStatus"='A' THEN 'Approve' 
				WHEN A."U_ApproveStatus"='R' THEN 'Reject' END AS STATUS
			,A."U_UserID" AS USERID
			,C."firstName"||'-'||C."lastName" AS USERNAME
			,A."U_IssueDate" AS CREATEDATE
			,A."U_DueDate" AS REQUIREDATE
			,A."Remark" AS REMARK
			,TO_DECIMAL(A."U_Amount",3,2) AS DOCTOTAL
			,D."NAME" AS JOBNUMBER
			,A."DocNum" AS DOCNUMPR
			,A."U_VendorCode" AS VENDORCODE
			,E."CardName" AS VENDORNAME
		FROM EW_PRD."@ADVANCEPAYMENT" AS A
		--LEFT JOIN EW_PRD."@ADVANCEPAYMENTROW" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD."OHEM" AS C ON C."empID"=A."U_UserID"
		LEFT JOIN EW_PRD."OPMG" AS D ON D."AbsEntry"=A."U_Project"
		LEFT JOIN EW_PRD."OCRD" AS E ON E."CardCode"=A."U_VendorCode"
		WHERE A."U_AdvanceType"=:par1;
	ELSE IF :DTYPE='GetAccountCodeJournalVoucherEntry' THEN
		SELECT A."AcctCode" AS ACCOUNTCODE
			  ,A."AcctName" AS ACCOUNTNAME
			  ,A."CurrTotal" AS ACCOUNTBALANCE
		FROM EW_PRD."OACT" AS A 
		WHERE A."FatherNum" IS NOT NULL AND A."Postable"='Y';
	ELSE IF :DTYPE='GetBpCodeJournalVoucherEntry' THEN
		SELECT A."CardCode" AS CUSTOMERCODE
			  ,A."CardName" AS CUSTOMERNAME
			  ,A."Balance" AS BALANCE
		FROM EW_PRD."OCRD" AS A WHERE A."CardType"='C' AND A."validFor"!='N' And A."frozenFor"!='Y';
	ELSE IF :DTYPE='GetMaxJournalVoucherNumber' THEN
		SELECT MAX(IFNULL("BatchNum"+1,1)) AS MAXDOCNUM FROM EW_PRD."OBTF";
	ELSE IF :DTYPE='GetBpCodeJournalVoucherEntry' THEN
		SELECT A."CardCode" AS CUSTOMERCODE
			  ,A."CardName" AS CUSTOMERNAME
			  ,A."Balance" AS BALANCE
		FROM EW_PRD."OCRD" AS A WHERE A."CardType"='C' AND A."validFor"!='N' And A."frozenFor"!='Y';
	ELSE IF :DTYPE='GetMaxJournalVoucherNumber' THEN
		SELECT MAX("BatchNum") AS MAXDOCNUM FROM EW_PRD."OBTF";
	ELSE IF :DTYPE='GetSeriesJournalVoucherNumber' THEN
		SELECT 
			 A."Series" AS SERIES
			,A."SeriesName" AS SERIESNAME
			,A."NextNumber" AS MAXDOCNUM
		FROM EW_PRD."NNM1" AS A
		WHERE A."Indicator" IN (SELECT "Indicator" 
									FROM EW_PRD."OFPR" AS A 
								WHERE TO_VARCHAR(A."F_RefDate", 'YYYY/MM')=TO_VARCHAR(CURRENT_DATE, 'YYYY/MM'))
		AND A."ObjectCode"='30';
		--AND A."BeginStr"='JV';
	ELSE IF :DTYPE='GetSeriesJournalEntryNumber' THEN
		SELECT 
			 A."Series" AS SERIES
			,A."SeriesName" AS SERIESNAME
			,A."NextNumber" AS MAXDOCNUM
		FROM EW_PRD."NNM1" AS A
		WHERE A."Indicator" IN (SELECT "Indicator" 
									FROM EW_PRD."OFPR" AS A 
								WHERE TO_VARCHAR(A."F_RefDate", 'YYYY/MM')=TO_VARCHAR(CURRENT_DATE, 'YYYY/MM'))
		AND A."ObjectCode"='30';
		--AND A."BeginStr"!='JV';
	ELSE IF :DTYPE='GetVatGroupJournalVoucherNumber' THEN
		SELECT 
			 A."Code" AS CODE
			,A."Code"||'-'||A."Name" AS NAME
			,A."Rate" AS RATE
		FROM EW_PRD."OVTG" AS A WHERE A."Inactive"='N' AND A."Account" IS NOT NULL;
	ELSE IF :DTYPE='GetListJournalVoucher' THEN
		SELECT 
			 A."BatchNum" AS "DOCENTRY"
			,A."RefDate" AS "DOCDATE"
			,A."Memo" AS "REMARKS"
			,A."Ref1" AS "REF1"
			,A."LocTotal" AS "TOTAL"
			,B."U_NAME" AS "CREATEBY"
		FROM EW_PRD."OBTF" AS A
		LEFT JOIN EW_PRD."OUSR" AS B ON A."U_USERID"=B."USERID"
		WHERE A."U_WEBID" IS NOT NULL;
	ELSE IF :DTYPE='GETJOBNUMBERFORPURCHASEREQUEST' THEN
		SELECT 
			  "PrjCode" AS CODE
			 ,"PrjName" AS NAME
		FROM EW_PRD."OPRJ" AS A;
	ELSE IF :DTYPE='GETVIEWDETAILSALESQUOTATIONHEADER' THEN
		SELECT 	   TO_VARCHAR(A."DocEntry") AS "DocEntry"
				  ,IFNULL(A."U_CUSTOMERTYPE",'') AS "CustomerType" 
				  ,A."CardName" AS "CustomerName" 
				  ,A."NumAtCard" AS "ReftNo"
				  ,A."U_ATTN" AS "ATTN"
				  ,TO_VARCHAR (A."DocDate", 'DD/MM/YYYY') AS "Date"
				  ,A."U_EMAIL" AS "Email"
				  ,TO_VARCHAR (A."DocDueDate", 'DD/MM/YYYY') AS "Validity"
				  ,A."U_TEL" AS "Tel"
				  ,(SELECT "PymntGroup" FROM EW_PRD."OCTG" WHERE "GroupNum"=A."GroupNum") AS "PaymentTerm"				  
				  ,IFNULL(DD."descript",'NONE') AS "Origin"
				  ,IFNULL(D."descript",'NONE') AS "Destination"
				  ,(SELECT IFNULL("Name",'') FROM EW_PRD."@TBSERVICE" WHERE "Code"=A."U_SERVICE") AS "ServiceType"
				  ,A."Comments" AS "Remarks"
				  ,B."SlpName" AS "SalePerson"
			FROM EW_PRD."OQUT" AS A 
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
			LEFT JOIN EW_PRD."OSLP" AS B ON B."SlpCode"=A."SlpCode"
			WHERE IFNULL(A."U_WEBID",'')!='' AND A."DocEntry"=:par1; 
	ELSE IF :DTYPE='GETVIEWDETAILSALESQUOTATIONLINE' THEN
		SELECT 
			 A."ItemCode" AS "ItemCode"
			,A."Dscription" AS "ItemName"
			,TO_DECIMAL(A."LineTotal",16,2) AS "Price"			
			,A."U_Remark" AS "Remarks" 
		FROM EW_PRD."QUT1" AS A 
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'SALESQUOTATIONLAYOUT' THEN 
		SELECT 
			 A."Code" AS "Code"
			,A."Name" AS "Name"
		FROM EW_PRD."@TBREPORT" AS A WHERE A."U_LAYOUTMODULE"='SALES QUOTATION';
	ELSE IF :DTYPE = 'GETTRUCKTYPEBOOKINGSHEET' THEN 
		/*SELECT DISTINCT 
			 A."AttriTxt6" AS CODE
			,A."AttriTxt6" AS NAME
		FROM EW_PRD."ITM13" AS A 
		WHERE IFNULL(A."AttriTxt6",'')<>'';*/
		SELECT 'OPEN TRUCK' AS CODE
			  ,'OPEN TRUCK' AS NAME FROM DUMMY
		UNION ALL
		SELECT 'FLATBED' AS CODE
			  ,'FLATBED' AS NAME FROM DUMMY
		UNION ALL
		SELECT 'LOW BED' AS CODE
			  ,'LOW BED' AS NAME FROM DUMMY
		UNION ALL
		SELECT 'REFERR' AS CODE
			  ,'REFERR' AS NAME FROM DUMMY
		UNION ALL
		SELECT '4 W' AS CODE
			  ,'4 W' AS NAME FROM DUMMY
		UNION ALL
		SELECT '6 W' AS CODE
			  ,'6 W' AS NAME FROM DUMMY
		UNION ALL
		SELECT '10 W' AS CODE
			  ,'10 W' AS NAME FROM DUMMY
		UNION ALL
		SELECT '10 WD' AS CODE
			  ,'10 WD' AS NAME FROM DUMMY
		UNION ALL
		SELECT 'Other' AS CODE
			  ,'Other' AS NAME FROM DUMMY;
	ELSE IF :DTYPE='GETVIEWDETAILSALESQUOTATIONLINE' THEN
		SELECT 
				  A."DocEntry" AS BOOKINGENTRY
				 ,A."DocNum" AS BOOKINGID
				 ,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
				 ,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				 ,DD."descript" AS ROUTEFROM
				 ,D."descript" AS ROUTETO
				 ,F."SlpName" AS SALEEMPLOYEENAME
				 ,B."Name" AS JOBTYPE
				 ,G."Name" AS SERVICETYPE
				 ,A."U_SHIPPER" AS SHIPPER
				 ,A."U_CONSIGNEE" AS CONSIGNEE
				 ,A."U_GOODSDESCRIPTION" AS DESCRIPTION
				 ,A."U_TOTALPACKAGE" AS TOTALPACKAGE
				 ,A."U_NETWEIGHT" AS NETWEIGHT
				 ,A."U_GROSSWEIGHT" AS GROSSWEIGHT
				 ,A."U_LOADINGDATE" AS LOADINGDATE
				 ,A."U_CROSSBORDERDATE" AS CROSSBORDERDATE
				 ,A."U_ETAREQUIREMENT" AS ETAREQUIREMENT
				 ,A."U_THAIFORWARDER" AS THAIFORWARDER
				 ,A."U_TRUCKTYPE" AS TRUCKTYPE
				 ,A."Remark" AS REMARK
				 ,A."U_SPECIALREQUEST" AS SPECIALREQUEST
				 --,A."DocEntry" AS DOCENTRY
			FROM EW_PRD."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON B."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_CO"
			LEFT JOIN EW_PRD."OTER" AS D ON D."territryID"=A."U_DESTINATION"
			LEFT JOIN EW_PRD."OTER" AS DD ON DD."territryID"=A."U_ORIGIN"
			LEFT JOIN EW_PRD."OUSR" AS E ON E."USERID"=A."U_UserCreate"
			LEFT JOIN EW_PRD."OSLP" AS F ON F."SlpCode"=A."U_SALEEMPLOYEE"
			LEFT JOIN EW_PRD."@TBSERVICETYPE" AS G ON G."Code"=A."U_SERVICETYPE"
			WHERE A."DocNum"=19;--:par1='-1'
	ELSE IF :DTYPE='PLACEOFLOADINGBYDOCENTRY' THEN
		SELECT B."Code" AS CODE
			  ,B."Name"||', '||B."U_COUNTRY" AS NAME
			  ,B."U_COUNTRY" AS COUNTRY
		FROM EW_PRD."@PLACEOFLOADING" AS A
		LEFT JOIN EW_PRD."@TBPLACEOFLOADING" AS B ON A."U_PLACELOADING"=B."Code" 
		WHERE A."DocEntry"=par1;
	ELSE IF :DTYPE='PLACEOFDELIVERYBYDOCENTRY' THEN
		SELECT B."Code" AS CODE
			  ,B."Name"||', '||B."U_COUNTRY" AS NAME
		FROM EW_PRD."@PLACEOFDELIVERY" AS A
		LEFT JOIN EW_PRD."@TBPLACEOFDELIVERY" AS B ON A."U_PLACEDELIVERY"=B."Code" 
		WHERE A."DocEntry"=par1;
	ELSE IF :DTYPE='VOLUMEBYDOCENTRY' THEN
		SELECT B."Code" AS CODE
			  ,B."Name" AS NAME
		FROM EW_PRD."@VOLUME" AS A
		LEFT JOIN EW_PRD."@TBVOLUME" AS B ON B."Code"=A."U_VOLUMECODE"
		WHERE A."DocEntry"=par1;
		
	ELSE IF :DTYPE='THAIBORDERBYDOCENTRY' THEN
		SELECT B."Code" AS CODE
			  ,B."Name" AS NAME
		FROM EW_PRD."@THAIBORDER" AS A
		LEFT JOIN EW_PRD."@TBTHAIBORDER" AS B ON A."U_ThaiBorder"=B."Code"
		WHERE A."DocEntry"=par1;
	ELSE IF :DTYPE='OverseaTruckersByDocEntry' THEN
		SELECT 
			A."U_OVERSEATRUCKERCODE" AS CODE
		FROM EW_PRD."@TBOVERSEATRUCKER" AS A
		WHERE A."DocEntry"=par1;
	ELSE IF :DTYPE='OverseaForwardersByDocEntry' THEN
		SELECT 
			A."U_OVERSEAFORWARDERCODE" AS CODE
		FROM EW_PRD."@TBOVERSEAFORWARDER" AS A
		WHERE A."DocEntry"=par1;
	ELSE IF :DTYPE='SaleQuotationsByDocEntry' THEN
		SELECT 
			 A."U_DOCENTRY"  AS DOCENTRY
			,B."DocNum" AS DOCNUM
		FROM EW_PRD."@TBSALESQUOTATION" AS A
		lEFT JOIN EW_PRD."OQUT" AS B ON A."U_DOCENTRY"=B."DocEntry"
		WHERE A."DocEntry"=par1;
	ELSE IF :DTYPE='GetAddressByCustomer' THEN
		SELECT "Address" AS "CustomerAddressCode"
			   ,"Address" AS "CustomerAddressDetail"
			  --,"Address2"
			  --	||IFNULL(','||"Address3",'')
			  --	||IFNULL("County",'')
			  --	||(SELECT IFNULL(','||"Name",'') FROM EW_PRD."OCRY" WHERE "Code"="Country") AS CUSTOMERADRESSDETAIL
		FROM EW_PRD."CRD1" WHERE "CardCode"=:par1;
	ELSE IF :DTYPE = 'BOOKINGSHEETLAYOUT' THEN 
		SELECT 
			 A."Code" AS CODE
			,A."Name" AS NAME
		FROM EW_PRD."@TBREPORT" AS A WHERE A."U_LAYOUTMODULE"='BOOKING-SHEET';
	ELSE IF :DTYPE = 'CONFIRMBOOKINGSHEETLAYOUT' THEN 
		SELECT 
			 A."Code" AS CODE
			,A."Name" AS NAME
		FROM EW_PRD."@TBREPORT" AS A WHERE A."U_LAYOUTMODULE"='CONFIRM-BOOKING-SHEET';
	ELSE IF :DTYPE = 'JOBSHEETRUCKINGLAYOUT' THEN 
		SELECT 
			 A."Code" AS CODE
			,A."Name" AS NAME
		FROM EW_PRD."@TBREPORT" AS A WHERE A."U_LAYOUTMODULE"='JOB-SHEET-TRUCKING';
	ELSE IF :DTYPE = 'PURCHASEREQUESTLAYOUT' THEN 
		SELECT 
			 A."Code" AS CODE
			,A."Name" AS NAME
		FROM EW_PRD."@TBREPORT" AS A WHERE A."U_LAYOUTMODULE"='PURCHASEREQUEST';
	ELSE IF :DTYPE = 'TRUCKWAYBILLLAYOUT' THEN 
		SELECT 
			 A."Code" AS CODE
			,A."Name" AS NAME
		FROM EW_PRD."@TBREPORT" AS A WHERE A."U_LAYOUTMODULE"='TRUCK-WAY-BILL';
	ELSE IF :DTYPE='GetDetailBookingByDocEntry' THEN
		IF :par1='Header' THEN
			SELECT 
				 A."DocEntry" AS DOCENTRY
				,A."DocEntry" AS BOOKINGID
				,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
				,TO_VARCHAR(A."U_BOOKINGDATE",'DD/MM/YYYY') AS BOOKINGDATE
				,B."descript"||'-'||BB."descript" AS ROUTE
				,B."descript" AS ORIGIN
				,B."descript" AS DESTINATION
				,IFNULL(C."SlpName",'') AS SALESEMPLOYEE	
				,D."Name" AS JOBTYPE
				,E."Name" AS SERVICETYPE
				,A."U_GOODSDESCRIPTION" AS GOODSDESCRIPTION
				,A."U_TOTALPACKAGE" AS TOTALPACKAGE
				,A."U_NETWEIGHT" AS NETWEIGHT
				,A."U_GROSSWEIGHT" AS GROSSWEIGHT
				,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS LOADINGDATE
				,TO_VARCHAR(A."U_CROSSBORDERDATE",'DD/MM/YYYY') AS CROSSBORDERDATE
				,TO_VARCHAR(A."U_ETAREQUIREMENT",'DD/MM/YYYY') AS ETAREQUIREMENT
				,A."Remark" REMARK
				,A."U_SPECIALREQUEST" AS SPECIALREQUEST
				,A."DocNum"
				,A."U_LOLOYARDORUNLOADING" AS LOLOYARD
				,A."U_LCLORFCL" AS LCLFCL
				,A."U_CYORCFS" AS CYCFS
				,A."U_LOLOYARDRemark" AS LOLOYARDREMARK
				,A."U_EWSeries" AS SERIES
				,A."U_SALEEMPLOYEE" AS SlpCode
				,A."U_IMPORTTYPE" AS IMPORTTYPE
				,A."U_SERVICETYPE" AS SERVICETYPECODE
				,A."DocNum" AS "DOCNUM"
				,A."U_JOBNO" AS JOBNUMBER
				,A."U_JOBNO" AS U_JONO
				,A."U_ORIGIN" AS ORIGINCODE
				,A."U_DESTINATION" AS DESTINATIONCODE
			FROM EW_PRD."@BOOKINGSHEET" AS A 
			LEFT JOIN EW_PRD."OTER" AS B ON A."U_ORIGIN"=B."territryID" --ORIGIN
			LEFT JOIN EW_PRD."OTER" AS BB ON BB."territryID"=A."U_DESTINATION" --DESTINATION
			LEFT JOIN EW_PRD."OSLP" AS C ON C."SlpCode"=A."U_SALEEMPLOYEE"
			LEFT JOIN EW_PRD."@TBJOBTYPE" AS D ON D."Code"=A."U_IMPORTTYPE"
			LEFT JOIN EW_PRD."@TBSERVICETYPE"AS E ON E."Code"=A."U_SERVICETYPE"
			WHERE A."DocEntry"=:par2; --'54';--
		ELSE IF :par1='SHIPPER' THEN
			SELECT 
				 B."CardName" AS SHIPPER
				,A."U_SHIPPER" AS CARDCODE
				,A."LineId" AS LINEID 
			FROM EW_PRD."@TBSHIPPER" AS A 
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_SHIPPER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='CONSIGNEE' THEN
			SELECT 
				 B."CardName" AS CONSIGNEE
				,A."LineId" AS LINEID 
				,A."U_CONSIGNEE" AS CARDCODE
			FROM EW_PRD."@TBCONSIGNEE" AS A 
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='SALESQUOTATION' THEN
			SELECT 
				 B."DocNum" || ' - ' ||B."CardName" AS SALESQUOTATION 
				,A."U_DOCENTRY" AS DOCENTRY
				,A."LineId" AS LINEID
				,B."CardCode" AS CARDCODE
			FROM EW_PRD."@TBSALESQUOTATION" AS A 
			LEFT JOIN EW_PRD."OQUT" AS B ON A."U_DOCENTRY"=B."DocEntry"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='COMMODITY' THEN
			SELECT 
				 A."U_INVOICE" AS INVOICE 
				,A."LineId" AS LINEID
			FROM EW_PRD."@COMMODITY" AS A 
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='OVERSEATRUCKER' THEN
			SELECT 
				 B."CardName" AS OVERSEATRUCKERCODE 
				,A."U_OVERSEATRUCKERCODE" AS CARDCODE
				,A."LineId" AS LINEID
			FROM EW_PRD."@TBOVERSEATRUCKER" AS A 
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_OVERSEATRUCKERCODE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='PLACEOFLOADING' THEN
			SELECT 
				 B."Name"||', '||B."U_COUNTRY" AS PLACELOADING
				,A."U_PLACELOADING" AS CODE
				,A."LineId" AS LINEID
			FROM EW_PRD."@PLACEOFLOADING" AS A 
			LEFT JOIN EW_PRD."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACELOADING"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='PLACEOFDELIVERY' THEN
			SELECT 
				 B."Name"||', '||B."U_COUNTRY" AS PLACEOFDELIVERY 
				,A."U_PLACEDELIVERY" AS Code
				,A."LineId" AS LINEID
			FROM EW_PRD."@PLACEOFDELIVERY" AS A 
			LEFT JOIN EW_PRD."@TBPLACEOFDELIVERY" AS B ON B."Code"=A."U_PLACEDELIVERY"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='THAIFORWARDER' THEN
			SELECT 
				 B."CardName" AS THAIFORWARDER 
				,A."U_THAIFORWARDER" AS CardCode
				,A."LineId" AS LINEID
			FROM EW_PRD."@THAIFORWARDER" AS A 
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_THAIFORWARDER"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='VOLUME' THEN
			SELECT 
				 A."U_QTY" AS QTY
				,A."U_VOLUMECODE" AS VOLUMECODE 
				,IFNULL(A."U_GROSSWEIGHT",0) AS GROSSWEIGHT
				,A."LineId" AS LINEID
			FROM EW_PRD."@VOLUME" AS A 
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='TBOVERSEAFORWARDER' THEN
			SELECT 
				 B."CardName" AS OVERSEAFORWARDERCODE 
				,A."U_OVERSEAFORWARDERCODE" AS CARDCODE
				,A."LineId" AS LINEID
			FROM EW_PRD."@TBOVERSEAFORWARDER" AS A 
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_OVERSEAFORWARDERCODE"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='TBTRUCKTYPEROW' THEN
			SELECT 
				--
				 TO_VARCHAR(TO_DECIMAL(A."U_QTY",3,0)) AS QTY 
				,A."U_TRUCKTYPE" AS TRUCKTYPECODE
				,IFNULL(A."U_GROSSWEIGHT",0) AS GROSSWEIGHT
				,A."LineId" AS LINEID
			FROM EW_PRD."@TBTRUCKTYPEROW" AS A 
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='THAIBORDER' THEN
			SELECT 
				 B."Name" AS THAIBORDER 
				,A."U_ThaiBorder" AS CODE
				,A."LineId" AS LINEID
			FROM EW_PRD."@THAIBORDER" AS A 
			LEFT JOIN EW_PRD."@TBTHAIBORDER" AS B ON A."U_ThaiBorder"=B."Code"
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
	ELSE IF :DTYPE = 'GETPROJECTMANAGEMENTLIST' THEN 
		SELECT A."PrjCode" AS PROJECTCODE
			  ,A."PrjName" AS PROJECTNAME
			  ,A."Active"  AS ACTIVE
		FROM EW_PRD."OPRJ" AS A
		WHERE A."Active"='Y';
	ELSE IF :DTYPE = 'GETPRICELISTCONFIRMBOOKING' THEN 
		SELECT 
			 A."ListNum" AS PRICELISTCODE
			,A."ListName" AS PRICELISTNAME
		FROM EW_PRD."OPLN" AS A 
		WHERE A."CreateDate"<>'';
	ELSE IF :DTYPE = 'GetPriceByItem' THEN 
		IF (SELECT COUNT(*) AS ISENABLE FROM EW_PRD."OSPP" AS A 
					WHERE 	  A."ItemCode"=:par1 
							  AND A."CardCode"=:par2 
							  AND TO_VARCHAR(A."ListNum")=:par3)!=0 THEN
			SELECT A."ItemCode" AS ITEMCODE
					,A."Price" AS PRICE
					,'true' AS ISENABLE 
				FROM EW_PRD."OSPP" AS A
				WHERE A."ItemCode"=:par1 
					AND A."CardCode"=:par2 
					AND A."ListNum"=:par3;
			ELSE 
				SELECT A."ItemCode" ITEMCODE
				  ,IFNULL(B."Price",0) AS PRICE
				  ,'false' AS ISENABLE
				FROM EW_PRD."OITM" AS A 
				LEFT JOIN EW_PRD."ITM1" AS B ON A."ItemCode"=B."ItemCode" AND B."PriceList"=1--:par3
				WHERE A."U_Dept_item"='CBT'
					AND A."ItemCode"=:par1 
					AND A."CardCode"=:par2;
			END IF;
	ELSE IF :DTYPE='GETSHIPPERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS SHIPPER
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@TBSHIPPER" AS B ON A."U_BOOKINGID"=B."DocEntry"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=B."U_SHIPPER"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETCONSIGNEECONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardName" AS CONSIGNEE
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@TBCONSIGNEE" AS B ON A."U_BOOKINGID"=B."DocEntry"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=B."U_CONSIGNEE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETTHAIFORWARDERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardCode" AS FORWARDER
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@BOOKINGSHEET" AS AA ON AA."DocEntry"=A."U_BOOKINGID"
		LEFT JOIN EW_PRD."@THAIFORWARDER" AS CC ON AA."DocEntry"=CC."DocEntry"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=CC."U_THAIFORWARDER"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETOVERSEAFORWARDERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."CardCode" AS FORWARDER
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@BOOKINGSHEET" AS AA ON AA."DocEntry"=A."U_BOOKINGID"
		LEFT JOIN EW_PRD."@TBOVERSEAFORWARDER" AS CC ON AA."DocEntry"=CC."DocEntry"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=CC."U_OVERSEAFORWARDERCODE"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETTHAIBORDERCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			C."Name" AS ThaiBorder
		FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@BOOKINGSHEET" AS AA ON AA."DocEntry"=A."U_BOOKINGID"
		LEFT JOIN EW_PRD."@THAIBORDER" AS CC ON AA."DocEntry"=CC."DocEntry"
		LEFT JOIN EW_PRD."@TBTHAIBORDER" AS C ON C."Code"=CC."U_ThaiBorder"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GETSALESQUOTATIONCONFIRMBOOKINGDETAIL' THEN
		SELECT 
			 B."U_DOCENTRY" AS DOCENTRY
			,C."NumAtCard" AS REFNO
			,C."CardCode" AS CARDCODE
		FROM EW_PRD."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD."@TBSALESQUOTATION" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD."OQUT" AS C ON B."U_DOCENTRY"=C."DocEntry"
		WHERE A."U_CONFIRMBOOKINGSHEETID"=:par1;
	ELSE IF :DTYPE='GetDetailConfirmBookingByDocEntry' THEN
		IF :par1='BookingSheetInformation' THEN
			SELECT 
				 B."Name" AS JOBTYPE
				,C."descript"||' - '|| CC."descript" AS ROUTE
				,D."SlpName" AS SALENAME
				,A."U_EWSeries"||A."U_JOBNO" AS JOBNO
				,E."ListName" AS PRICELIST
				,A."Remark" AS REMARK
				,AA."DocEntry" AS DOCENTRY
			FROM EW_PRD."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD."@CONFIRMBOOKINGSHEET" AS AA ON AA."DocEntry"=:par2
			LEFT JOIN EW_PRD."@TBJOBTYPE" AS B ON A."U_IMPORTTYPE"=B."Code"
			LEFT JOIN EW_PRD."OTER" AS C ON C."territryID"=A."U_ORIGIN" --ORIGIN
			LEFT JOIN EW_PRD."OTER" AS CC ON CC."territryID"=A."U_DESTINATION" --DESTINATION
			LEFT JOIN EW_PRD."OSLP" AS D ON D."SlpCode"=A."U_SALEEMPLOYEE"
			LEFT JOIN EW_PRD."OPLN" AS E ON E."ListNum"=AA."U_PRICELIST"
			WHERE A."U_CONFIRMBOOKINGSHEETID"=:par2;
		ELSE IF :par1='GetBorderBookingSheet' THEN
			SELECT 
				 IFNULL(C."Code",0) AS CODE
				,IFNULL(C."Name",'') AS NAME
			FROM EW_PRD."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD."@THAIBORDER" AS B ON A."DocEntry"=B."DocEntry"
			LEFT JOIN EW_PRD."@TBTHAIBORDER" AS C ON C."Code"=B."U_ThaiBorder"
			WHERE A."U_CONFIRMBOOKINGSHEETID"=:par2;
		ELSE IF :par1='PLACEOFLOADING' THEN
			SELECT 
				B."Name"||', '||B."U_COUNTRY" AS PLACELOADING
			FROM EW_PRD."@BOOKINGSHEET" AS AA
			LEFT JOIN EW_PRD."@PLACEOFLOADING" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD."@TBPLACEOFLOADING" AS B ON B."Code"=A."U_PLACELOADING"			
			WHERE AA."U_CONFIRMBOOKINGSHEETID"=:par2;
		ELSE IF :par1='PLACEOFDELIVERY' THEN
			SELECT 
				B."Name"||', '||B."U_COUNTRY" AS PLACEOFDELIVERY 
			FROM EW_PRD."@BOOKINGSHEET" AS AA
			LEFT JOIN EW_PRD."@PLACEOFDELIVERY" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD."@TBPLACEOFDELIVERY" AS B ON B."Code"=A."U_PLACEDELIVERY"
			WHERE AA."U_CONFIRMBOOKINGSHEETID"=:par2;
		ELSE IF :par1='SHIPPER' THEN
			SELECT 
				B."CardName" AS SHIPPER 
			FROM EW_PRD."@BOOKINGSHEET" AS AA
			LEFT JOIN EW_PRD."@TBSHIPPER" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_SHIPPER"
			WHERE AA."U_CONFIRMBOOKINGSHEETID"=:par2;
		ELSE IF :par1='CONSIGNEE' THEN
			SELECT 
				B."CardName" AS CONSIGNEE 
			FROM EW_PRD."@BOOKINGSHEET" AS AA
			LEFT JOIN EW_PRD."@TBCONSIGNEE" AS A ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=A."U_CONSIGNEE"
			WHERE AA."U_CONFIRMBOOKINGSHEETID"=:par2;
		ELSE IF :par1='GetListOfContainerInformation' THEN
			SELECT 
				 A."U_TYPE" AS "TYPE"
				,A."U_CONTAINERTYPE" AS CONTAINERTYPE
				,A."U_CONTAINERNO" AS CONTAINERNO
				,A."U_OWNER" AS OWNER
				,A."U_GROSSWEIGHT" AS GROSSWEIGHT
				,A."U_YARD" AS YARD
				,A."U_FCL_LCL" AS FCL_LCL
				,A."U_LOLO_UNLOADING" AS LOLO_UNLOADING
				,B."Location" AS TRUCKPROVINCE
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
				,A."U_VENDORCODE" AS VENDOR
				,A."U_TRUCKCOSTTHB" AS TRUCKCOSTTHB
				,A."U_SEALNO1" AS SEALNO1
				,A."U_SEALNO2" AS SEALNO2
				,A."LineId" AS LINEID
			FROM EW_PRD."@TRUCKINFORMATION" AS A 
			LEFT JOIN EW_PRD."OLCT" AS B ON B."Code"=A."U_TRUCKPROVINCE"
			LEFT JOIN EW_PRD."OHEM" AS C ON C."empID"=A."U_DRIVER1"
			LEFT JOIN EW_PRD."OHEM" AS CC ON CC."empID"=A."U_DRIVER2"
			WHERE A."DocEntry"=:par2;
		/*ELSE IF :par1='ListOfAdvancePayment' THEN
			SELECT --DISTINCT
				 A."U_WebID" AS ADVANCEPAYMENTID --A."U_WEBID"
				,B."SeriesName" AS SERIES
				,TO_NVARCHAR(A."U_PostingDate",'dd-MM-yyyy') AS POSTINGDATE
				,IFNULL(A."Remark",'') AS REMARK
				,A."U_Ref1" AS REF1
				,A."U_Ref2" AS REF2
				,A."U_Ref3" AS REF3
				,D."NAME" AS PROJECTMANAGEMENT
				,AA."U_DocEntry"
			FROM EW_PRD."@ADVANCEPAYMENT" AS A
			LEFT JOIN EW_PRD."NNM1" AS B ON B."Series"=A."Series"
			LEFT JOIN EW_PRD."@TBADVANCEPAYMENT" AS AA ON AA."U_DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD."@CONFIRMBOOKINGSHEET" AS C ON C."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD."OPMG" AS D ON D."AbsEntry"=C."U_PROJECTMANAGEMENTID"
			WHERE A."DocEntry"=:par2;
			select 'heng' from dummy;*/
		/*ELSE IF :par1='ListLineAdvancePayment' THEN
			SELECT DISTINCT
				 AA."U_EmployeeCode"||' - '||B."CardName" AS EMPLOYEECODEANDEMPLOYEENAME
				,AA."U_AccountCode"||' - '||C."AcctName" AS ACCOUNTCODEANDACCOUNTNAME
				,AA."U_Amount" AS AMOUNT
			FROM EW_PRD."@ADVANCEPAYMENT" AS A
			LEFT JOIN EW_PRD."@ADVANCEPAYMENTROW" AS AA ON A."DocEntry"=AA."DocEntry"
			LEFT JOIN EW_PRD."OCRD" AS B ON B."CardCode"=AA."U_EmployeeCode"
			LEFT JOIN EW_PRD."OACT" AS C ON C."AcctCode"=AA."U_AccountCode"
			WHERE A."DocEntry"=:par2;
			select 'Heng' from dummy;*/
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
				,(SELECT IFNULL("OCRY"."Name",'') FROM EW_PRD."OCRY" WHERE "OCRY"."Code"=B."BankCountr") AS BANKCOUNTRY
				,(SELECT IFNULL("ODSC"."BankName",'') FROM EW_PRD."ODSC" WHERE "ODSC"."BankCode"=B."BankCode") AS BANKNAME
				,IFNULL(B."DflSwift",'') AS SWIFTCODE	
			FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS F
			LEFT JOIN (
				SELECT * FROM EW_PRD."@TBADVANCEPAYMENT"
				UNION ALL
				SELECT * FROM EW_PRD."@TBPURCHASEREQUEST"
			) AS E ON E."DocEntry"=F."DocEntry"
			LEFT JOIN EW_PRD."@ADVANCEPAYMENT" AS A ON E."U_DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD."OCRD" AS B ON A."U_VendorCode"=B."CardCode"
			LEFT JOIN EW_PRD."OHEM" AS C ON A."U_UserID"=C."empID"
			LEFT JOIN EW_PRD."OCRN" AS D ON A."U_CurrencyType"=D."CurrCode"
			WHERE F."DocEntry"=:par2 AND A."U_AdvanceType"=:par3 AND E."U_CONTAINERLINEID"=:par4;
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
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE
			FROM EW_PRD."@CONFIRMBOOKINGSHEET" AS F
			LEFT JOIN (
				SELECT * FROM EW_PRD."@TBADVANCEPAYMENT"
				UNION ALL
				SELECT * FROM EW_PRD."@TBPURCHASEREQUEST"
			) AS E ON E."DocEntry"=F."DocEntry"
			LEFT JOIN EW_PRD."@ADVANCEPAYMENTROW" AS A ON E."U_DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD."OITM" AS B ON A."U_ItemCode"=B."ItemCode"
			WHERE A."DocEntry"=:par2;
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
	ELSE IF :DTYPE='GETADVANCEPAYMENTLIST' THEN
		SELECT 
			 A."DocEntry" AS DOCENTRY
			,A."U_NumAtCard" AS DOCNUM
			,CASE 
				WHEN A."U_ApproveStatus"='P' THEN 'Pending' 
				WHEN A."U_ApproveStatus"='A' THEN 'Approve' 
				WHEN A."U_ApproveStatus"='R' THEN 'Reject' END AS STATUS
			,A."U_UserID" AS USERID
			,C."firstName"||'-'||C."lastName" AS USERNAME
			,A."U_IssueDate" AS CREATEDATE
			,A."U_DueDate" AS REQUIREDATE
			,A."Remark" AS REMARK
			,TO_DECIMAL(A."U_Amount",10,2) AS DOCTOTAL
			,D."NAME" AS JOBNUMBER
			,A."DocNum" AS DOCNUMPR
			,A."U_VendorCode" AS VENDORCODE
			,E."CardName" AS VENDORNAME
		FROM EW_PRD."@ADVANCEPAYMENT" AS A
		--LEFT JOIN EW_PRD."@ADVANCEPAYMENTROW" AS B ON A."DocEntry"=B."DocEntry"
		LEFT JOIN EW_PRD."OHEM" AS C ON C."empID"=A."U_UserID"
		LEFT JOIN EW_PRD."OPMG" AS D ON D."AbsEntry"=A."U_Project"
		LEFT JOIN EW_PRD."OCRD" AS E ON E."CardCode"=A."U_VendorCode"
		WHERE A."U_AdvanceType"='PRAD' AND (SELECT COUNT("U_Paid") FROM EW_PRD."@ADVANCEPAYMENTROW" WHERE "DocEntry"=A."DocEntry")<>0;
	ELSE IF :DTYPE='GETSTATUSCANCELADVANCEPAYMENT' THEN
		/*IF (SELECT "U_Status" FROM EW_PRD."@ADVANCEPAYMENT" WHERE "DocEntry"=:par1)='P' THEN
			SELECT '0' AS "Condition" from dummy;
		ELSE
			SELECT '1' AS "Condition" from dummy;
		END IF;*/
		select 'heng' from dummy;
	ELSE IF :DTYPE='GetDetailAdvancePaymentByDocEntry' THEN
		IF :par1='Header' THEN
			SELECT 
			 A."U_NumAtCard" AS DOCNUM
			,A."DocNum" AS DOCNUMPR
			,A."DocEntry" AS DOCENTRY
			,CASE WHEN TO_VARCHAR(A."U_Project")='0' THEN '' ELSE TO_VARCHAR(D."NAME") END AS PROJECTNUMBER
			,A."U_IssueDate" AS ISSUEDATE
			,A."U_DueDate" AS DUEDATE
			,A."U_VendorCode" AS VENDORCODE
			,B."CardName" AS VENDORNAME
			,A."U_CurrencyType" AS CURRENCY
			,A."U_UserID" AS EMPLOYEEID
			,C."firstName" ||' '|| C."lastName" AS EMPLOYEENAME
			,A."U_Amount" AS AMOUNT
			,A."U_AmountTHB" AS AMOUNTTHB
			,A."Remark" AS REMARKS
			,B."DflAccount" AS BANKACCOUNT
			,B."DflBranch" AS BRANCH	  
			,(SELECT "OCRY"."Name" FROM EW_PRD."OCRY" WHERE "OCRY"."Code"=B."BankCountr") AS BANKCOUNTRY
			,(SELECT "ODSC"."BankName" FROM EW_PRD."ODSC" WHERE "ODSC"."BankCode"=B."BankCode") AS BANKNAME
			,B."DflSwift" AS SWIFTCODE	
			,A."U_REASON" AS REASON
			,(SELECT DISTINCT T2."DocNum" AS DOCNUM 
				FROM EW_PRD."OPRQ" AS T0 
				LEFT JOIN EW_PRD."POR1" AS T1 ON T0."DocEntry"=T1."BaseEntry" AND T1."BaseType"=1470000113  
				LEFT JOIN EW_PRD."OPOR" AS T2 ON T2."DocEntry"=T1."DocEntry"
				WHERE T0."DocEntry"=A."U_PRDocEntry") AS DOCNUMPRAD
			,(SELECT DISTINCT T1."DocEntry" AS DOCENTRY 
				FROM EW_PRD."OPRQ" AS T0 
				LEFT JOIN EW_PRD."POR1" AS T1 ON T0."DocEntry"=T1."BaseEntry" AND T1."BaseType"=1470000113  
				WHERE T0."DocEntry"=A."U_PRDocEntry") AS DOCENTRYPR
			,IFNULL((SELECT MAX(T0."LineNum") FROM EW_PRD."POR1" AS T0 WHERE T0."BaseEntry"=A."U_PRDocEntry" AND T0."BaseType"=1470000113),0) AS MAXROWPO
			,IFNULL(A."U_PRDocEntry",0) AS DOCENTRYPURCHASEREQUEST
		FROM EW_PRD."@ADVANCEPAYMENT" AS A
		LEFT JOIN EW_PRD."OCRD" AS B ON A."U_VendorCode"=B."CardCode"
		LEFT JOIN EW_PRD."OPMG" AS D ON D."AbsEntry"=A."U_Project"
		LEFT JOIN EW_PRD."OHEM" AS C ON A."U_UserID"=C."empID"
		WHERE A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."U_ItemCode" AS ITEMCODE
				,B."ItemName" AS ITEMNAME
				,A."U_Qty" AS QTY
				,A."U_Price" AS PRICE
				,A."U_Origin" AS ORIGIN
				,A."U_Destination" AS DESTINATION
				,A."U_Remarks" AS REMARKS
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE
				,IFNULL((SELECT T1."LineNum" AS LINENUM 
					FROM EW_PRD."OPRQ" AS T0 
					LEFT JOIN EW_PRD."POR1" AS T1 ON T0."DocEntry"=T1."BaseEntry" AND T1."BaseType"=1470000113  
					WHERE T0."DocEntry"=C."U_PRDocEntry" AND T1."ItemCode"=A."U_ItemCode"),0) AS LINENUMPO
				,IFNULL((SELECT T1."LineNum" AS LINENUM 
					FROM EW_PRD."OPRQ" AS T0 
					LEFT JOIN EW_PRD."PRQ1" AS T1 ON T0."DocEntry"=T1."DocEntry"  
					WHERE T0."DocEntry"=C."U_PRDocEntry" AND T1."ItemCode"=A."U_ItemCode"),0) AS LINENUMPR
				,IFNULL(A."VisOrder",0) AS LINENUMAD
				,A."U_DistributionRule" AS 	DistributionRule		
			FROM EW_PRD."@ADVANCEPAYMENTROW" AS A
			LEFT JOIN EW_PRD."OITM" AS B ON A."U_ItemCode"=B."ItemCode"
			LEFT JOIN EW_PRD."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='GETAPPROVALLISTADVANCEPAYMENT' THEN
		IF :par3='PRCommission' THEN
			SELECT 
				 '' AS DOCNUM
				,(SELECT STRING_AGG(Z1."NAME", ',') 
					FROM EW_PRD."@TBCOMMISSIONROW" AS Z0
					LEFT JOIN EW_PRD."OPMG" AS Z1 ON Z1."AbsEntry"=Z0."U_JobNumber" WHERE Z0."DocEntry"=A."DocEntry") AS PROJECTNUMBER
				,TO_VARCHAR(A."U_IssueDate",'dd-MM-YYYY') AS ISSUEDATE
				,B."BPLink" AS VENDORCODE
				,C."CardName" AS VENDORNAME
				,C."Currency" AS CURRENCY
				,A."U_UserID" AS EMPLOYEEID
				,B."firstName" ||' '|| B."lastName" AS EMPLOYEENAME
				,D."GrandTotalCommission" AS AMOUNT
				,A."Remark" AS REMARK
				,A."DocEntry" AS DOCENTRY
				,CASE 
					WHEN A."U_ApproveStatus"='R' THEN 'Reject' 
					WHEN A."U_ApproveStatus"='A' THEN 'Approve' 
					WHEN A."U_ApproveStatus"='P' THEN 'Pending' END AS DOCUMENTSTATUS
				,A."DocNum" AS DOCNUMPR
				,'Commission' AS "Type"
			FROM EW_PRD."@TBCOMMISSION" AS A
			LEFT JOIN EW_PRD."OHEM" AS B ON A."U_UserID"=B."empID"
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=B."BPLink"
			LEFT JOIN (
					SELECT 
						 T0."DocEntry"
						,SUM(T0."U_GrandTotalCosting") AS "GrandTotalCosting"
						,SUM(T0."U_GrandTotalSelling") AS "GrandTotalSelling"
						,SUM(T0."U_GrossProfit") AS "GrandTotalGrossProfit"
						,SUM(T0."U_TotalSaleCommission") AS "GrandTotalCommission"
					FROM EW_PRD."@TBCOMMISSIONROW" AS T0 GROUP BY T0."DocEntry"
				)AS D ON D."DocEntry"=A."DocEntry"
			WHERE A."U_IssueDate" BETWEEN :par1 AND :par2 And A."Status"='O';
		ELSE
			SELECT 
				 A."U_NumAtCard" AS DOCNUM
				,CASE WHEN TO_VARCHAR(A."U_Project")='0' THEN '' ELSE TO_VARCHAR(D."NAME") END AS PROJECTNUMBER
				,TO_VARCHAR(A."U_IssueDate",'dd-MM-YYYY') AS ISSUEDATE
				,A."U_VendorCode" AS VENDORCODE
				,B."CardName" AS VENDORNAME
				,A."U_CurrencyType" AS CURRENCY
				,A."U_UserID" AS EMPLOYEEID
				,C."firstName" ||' '|| C."lastName" AS EMPLOYEENAME
				,A."U_Amount" AS AMOUNT
				,A."Remark" AS REMARK
				,A."DocEntry" AS DOCENTRY
				,CASE 
					WHEN A."U_ApproveStatus"='R' THEN 'Reject' 
					WHEN A."U_ApproveStatus"='A' THEN 'Approve' 
					WHEN A."U_ApproveStatus"='P' THEN 'Pending' END AS DOCUMENTSTATUS
				,A."DocNum" AS DOCNUMPR
				,'Normal' AS "Type"
			FROM EW_PRD."@ADVANCEPAYMENT" AS A
			LEFT JOIN EW_PRD."OCRD" AS B ON A."U_VendorCode"=B."CardCode"
			LEFT JOIN EW_PRD."OHEM" AS C ON A."U_UserID"=C."empID"
			LEFT JOIN EW_PRD."OPMG" AS D ON D."AbsEntry"=A."U_Project"
			WHERE A."U_IssueDate" BETWEEN :par1 AND :par2
				  And A."U_AdvanceType"=:par3;
		END IF;
	ELSE IF :DTYPE='GetListClearingAdvancePayment' THEN
		SELECT 
			 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
			,A."DocEntry" AS DOCENTRY
			,A."DocNum" AS DOCNUM
			,A."U_NumAtCard" AS REFNO
			,B."NAME" AS JOBNUMBER
			,A."U_VendorCode" AS VENDORCODE
			,C."CardName" AS VENDORNAME
			,TO_VARCHAR(A."U_IssueDate",'dd-mm-YYYY') AS ISSUEDATE
			,A."U_Amount" AS DOCTOTAL
			,A."Remark" AS REMARKS
		FROM EW_PRD."@ADVANCEPAYMENT" AS A
		LEFT JOIN EW_PRD."OPMG" AS B ON B."AbsEntry"=A."U_Project"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_VendorCode"
		LEFT JOIN EW_PRD."OPRQ" AS E ON E."DocEntry"=A."U_PRDocEntry"
		LEFT JOIN EW_PRD."OPOR" AS D ON D."DocEntry"=(SELECT DISTINCT "DocEntry" FROM EW_PRD."POR1" AS T0 WHERE T0."BaseEntry"=E."DocEntry" AND T0."BaseType"='1470000113')
		WHERE 	  A."U_AdvanceType"='PRAD' 
			  AND A."U_PRDocEntry" IS NOT NULL 
			  AND A."U_ApproveStatus"='A'
			  AND IFNULL(D."U_IsUpdate",'')!='Y'
			  AND (SELECT COUNT(*) FROM EW_PRD."@SETTLEMENTHEADER" AS T0 WHERE T0."U_PODocEntry"=D."DocEntry" AND T0."Status"!='C')=0;
	ELSE IF :DTYPE='GetListClearingAdvancePaymentByTransId' THEN
		IF :par1='Header' THEN
			SELECT 
			 A."U_NumAtCard" AS DOCNUM
			,A."DocNum" AS DOCNUMPR
			,A."DocEntry" AS DOCENTRY
			,CASE WHEN TO_VARCHAR(A."U_Project")='0' THEN '' ELSE TO_VARCHAR(D."NAME") END AS PROJECTNUMBER
			,A."U_IssueDate" AS ISSUEDATE
			,A."U_DueDate" AS DUEDATE
			,A."U_VendorCode" AS VENDORCODE
			,B."CardName" AS VENDORNAME
			,A."U_CurrencyType" AS CURRENCY
			,A."U_UserID" AS EMPLOYEEID
			,C."firstName" ||' '|| C."lastName" AS EMPLOYEENAME
			,A."U_Amount" AS AMOUNT
			,A."U_AmountTHB" AS AMOUNTTHB
			,A."Remark" AS REMARKS
			,B."DflAccount" AS BANKACCOUNT
			,B."DflBranch" AS BRANCH	  
			,(SELECT "OCRY"."Name" FROM EW_PRD."OCRY" WHERE "OCRY"."Code"=B."BankCountr") AS BANKCOUNTRY
			,(SELECT "ODSC"."BankName" FROM EW_PRD."ODSC" WHERE "ODSC"."BankCode"=B."BankCode") AS BANKNAME
			,B."DflSwift" AS SWIFTCODE	
			,A."U_REASON" AS REASON
		FROM EW_PRD."@ADVANCEPAYMENT" AS A
		LEFT JOIN EW_PRD."OCRD" AS B ON A."U_VendorCode"=B."CardCode"
		LEFT JOIN EW_PRD."OPMG" AS D ON D."AbsEntry"=A."U_Project"
		LEFT JOIN EW_PRD."OHEM" AS C ON A."U_UserID"=C."empID"
		WHERE A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."U_ItemCode" AS ITEMCODE
				,B."ItemName" AS ITEMNAME
				,A."U_Qty" AS QTY
				,A."U_Price" AS PRICE
				,A."U_Origin" AS ORIGIN
				,A."U_Destination" AS DESTINATION
				,A."U_Remarks" AS REMARKS
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE
			FROM EW_PRD."@ADVANCEPAYMENTROW" AS A
			LEFT JOIN EW_PRD."OITM" AS B ON A."U_ItemCode"=B."ItemCode"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetClearingAdvanceType' THEN
		SELECT 
			 A."CreditCard" AS CLEARINGCODE
			,A."CardName" AS CLEARINGNAME
			,A."AcctCode" AS ACCOUNTNAME
		FROM EW_PRD."OCRC" AS A;
	ELSE IF :DTYPE='GetDetailJobSheetByDocEntry' THEN
		SELECT 
			 T0."DocEntry" AS DOCENTRY
			,T0."DocNum" AS DOCNUM
			,T0."U_CONFIRMBOOKINGID" AS CONFIRMBOOKINGID
			,T0."U_SALEQUOTATIONDOCNUM" AS SALEQUOTATIONDOCNUM
			,T0."U_CURRENCYCOSTING" AS CURRENCYCOSTING
			,T0."U_CURRENCYSELLING" AS CURRENCYSELLING
			,T0."U_REMARKSCOSTING" AS REMARKSCOSTING
			,T0."U_REMAKRSSELLING" AS REMARKSSELLING
			,T0."U_TOTALCOSTING" AS TOTALCOSTING
			,T0."U_REBATE" AS REBATE
			,T0."U_GRANDTOTALCOSTING" AS GRANDTOTALCOSTING
			,T0."U_GRANDTOTALCOSTINGUSD" GRANDTOTALCOSTINGUSD
			,T0."U_TOTALSELLING" AS TOTALSELLING
			,T0."U_GRANDTOTALSELLING" AS GRANDTOTALSELLING
			,T0."U_GRANDTOTALSELLINGUSD" AS GRANDTOTALSELLINGUSD
			,T0."U_TOTALPROFIT" AS TOTALPROFIT
			,T0."U_USERCREATE" AS USERCREATE
			,T0."U_CARDCODE" AS CARDCODE
			,T0."U_JOBNO" AS JOBNO
		FROM EW_PRD."@JOBSHEETRUCKING" AS T0
		WHERE T0."DocEntry"=:par1;
	ELSE IF :DTYPE='GetDetailJobSheetLineByDocEntry' THEN
		SELECT 
			 A."U_ITEMCODE" AS ITEMCODE
			,IFNULL(A."U_TOTALPRICE",0) AS COSTINGTOTALPRICE
			,IFNULL(A."U_Qty",0) AS COSTINGQTY
			,IFNULL(B."U_TOTALPRICE",0) AS SELLINGTOTALPRICE
			,IFNULL(B."U_Qty",0) AS SELLINGQTY
		FROM EW_PRD."@JOBTRUCKCOSTING" AS A
		LEFT JOIN EW_PRD."@JOBTRUCKINGSELLING" AS B ON A."U_ITEMCODE"=B."U_ITEMCODE" AND A."DocEntry"=B."DocEntry" 
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetContactPersonByCardCode' THEN
		 SELECT 
		 	 IFNULL(CAST(A."CntctCode" AS NVARCHAR(250)),'0' ) AS "ContactPersonID"
		 	,IFNULL(A."FirstName",'') AS "FirstName"
		 	,IFNULL(A."LastName",'') AS "LastName"
		 	,IFNULL(A."Tel1",'') AS "Phone"
		 	,IFNULL(A."E_MailL",'') AS "Email"
		 FROM EW_PRD."OCPR" AS A
		 WHERE A."CardCode"=:par1;
	ELSE IF :DTYPE='GetARInvoiceWithJobNumber' THEN
		 SELECT 
		 	 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
		 	,A."DocNum" AS DOCNUM
		 	,A."DocEntry" AS DOCENTRY
		 	,A."U_WEBID" AS WEBID
		 	,(SELECT DISTINCT INV1."Project" FROM EW_PRD."INV1" WHERE INV1."DocEntry"=A."DocEntry") AS JOBNUMBER
		 	,A."CardCode" AS CustomerCode
		 	,B."CardName" AS CustomerName
		 	,TO_VARCHAR(A."DocDate",'dd-mm-YYYY') AS ISSUEDATE
		 	,A."DocTotal" AS DOCTOTAL
		 	,A."Comments" AS REMARK
		 FROM EW_PRD."OINV" AS A 
		 LEFT JOIN EW_PRD."OCRD" AS B ON A."CardCode"=B."CardCode"
		 WHERE 		A."U_WEBID" IS NOT NULL 
		 	   --AND (A."DocTotal"-A."PaidToDate")<>0 
		 	   AND A."PaidToDate"=0 
		 	   AND IFNULL((SELECT DISTINCT INV1."Project" FROM EW_PRD."INV1" WHERE INV1."DocEntry"=A."DocEntry"),'')<>'';--A."DocNum"='21' AND 
	ELSE IF :DTYPE='GetARInvoiceWithJobNumberByDocEntry' THEN
		IF :par1='Header' THEN
			SELECT 
			 	 A."DocEntry" AS DOCENTRY
			 	,A."DocNum" AS DOCNUM
			 	,A."NumAtCard" AS CUSTOMERREF
			 	,(SELECT DISTINCT INV1."Project" FROM EW_PRD."INV1" WHERE INV1."DocEntry"=A."DocEntry") AS JOBNUMBER
			 	,A."CardCode" AS CustomerCode
			 	,B."CardName" AS CustomerName
			 	,TO_VARCHAR(A."DocDate",'dd-mm-YYYY') AS ISSUEDATE
			 	,A."DocTotal" AS DOCTOTAL
			 	,A."Comments" AS REMARK
			 	,A."DocStatus" AS STATUS
			 	,IFNULL(TO_VARCHAR(A."CntctCode"),'') AS CONTACTPERSON
			 	,B."Currency" AS CURRENCY
			 FROM EW_PRD."OINV" AS A 
			 LEFT JOIN EW_PRD."OCRD" AS B ON A."CardCode"=B."CardCode"
			 WHERE 		A."U_WEBID" IS NOT NULL 
			 	   --AND (A."DocTotal"-A."PaidToDate")<>0 
			 	   AND IFNULL((SELECT DISTINCT INV1."Project" FROM EW_PRD."INV1" WHERE INV1."DocEntry"=A."DocEntry"),'')<>''
			 	   AND A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 ROW_NUMBER ( ) OVER( ORDER BY A."ItemCode" DESC ) AS ROWNUM
				,A."LineNum" AS LINENUM
				,A."ItemCode" AS ITEMCODE
				,A."Dscription" AS ITEMNAME
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE
				,A."LineTotal" AS LINETOTAL
				,IFNULL(A."U_Remark",'') AS REMARK
			FROM EW_PRD."INV1" AS A
			LEFT JOIN EW_PRD."OITM" AS B ON B."ItemCode"=A."ItemCode"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetListCustomerJobSheetTrucking' THEN
		SELECT DISTINCT
			 A."U_CARDCODE" AS CUSTOMERCODE
			,B."CardName" AS CUSTOMERNAME
		FROM EW_PRD."@JOBSHEETRUCKING" AS A 
		LEFT JOIN EW_PRD."OCRD" AS B ON A."U_CARDCODE"=B."CardCode" 
		WHERE A."U_JOBNO"=:par1;
	ELSE IF :DTYPE = 'GETPROJECTMANAGEMENTLISTJOBSHEETTRUCKING' THEN 
		SELECT A."PrjCode" AS PROJECTCODE
			  ,A."PrjName" AS PROJECTNAME
			  ,A."Active"  AS ACTIVE
		FROM EW_PRD."OPRJ" AS A
		WHERE A."Active"='Y' AND A."PrjName" IN (SELECT "U_JOBNO" FROM EW_PRD."@JOBSHEETRUCKING");
	ELSE IF :DTYPE = 'GetListInvoiceRefBaseOnCustomerAndJobNumber' THEN 
		SELECT 
			 B."DocNum" AS DOCNUM
			,B."DocEntry" AS DOCENTRY
		FROM EW_PRD."@JOBSHEETRUCKING" AS A
		LEFT JOIN EW_PRD."ORDR" AS B ON B."DocEntry"=A."U_SALESORDERDOCNUM"
		WHERE A."U_CARDCODE"=:par1 AND A."U_JOBNO"=:par2 And A."U_SALESORDERDOCNUM" IS NOT NULL;
	ELSE IF :DTYPE='GetDetailPurchaseOrderByDocEntry' THEN
		IF :par1='Header' THEN
			SELECT 
				 A."NumAtCard" AS DOCNUM
				,D."DocNum" AS DOCNUMPR
				,A."DocEntry" AS DOCENTRY
				,TO_VARCHAR(A."DocDate",'dd-MM-yyyy') AS ISSUEDATE
				,TO_VARCHAR(A."TaxDate",'dd-MM-yyyy') AS DUEDATE
				,A."CardCode" AS VENDORCODE
				,B."CardName" AS VENDORNAME
				,A."DocCur" AS CURRENCY
				,A."UserSign" AS EMPLOYEEID
				,C."firstName" ||' '|| C."lastName" AS EMPLOYEENAME
				,TO_DECIMAL(A."DocTotal",10,2) AS AMOUNT
				,A."Comments" AS REMARKS
			FROM EW_PRD."OPOR" AS A
			LEFT JOIN EW_PRD."OCRD" AS B ON A."CardCode"=B."CardCode"
			LEFT JOIN EW_PRD."OHEM" AS C ON A."UserSign"=C."userId"
			LEFT JOIN (
				SELECT DISTINCT
					 T1."DocNum"
					,T0."DocEntry"
				FROM EW_PRD."POR1" AS T0 
				LEFT JOIN EW_PRD."@ADVANCEPAYMENT" AS T1 ON T1."U_PRDocEntry"=T0."BaseEntry" 
				WHERE T0."BaseType"='1470000113'
			) AS D ON D."DocEntry"=A."DocEntry"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."ItemCode" AS ITEMCODE
				,B."ItemName" AS ITEMNAME
				,A."Quantity" AS QTY
				,TO_DECIMAL(A."LineTotal",10,2) AS PRICE
				,A."U_Remark" AS REMARKS
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE				
			FROM EW_PRD."POR1" AS A
			LEFT JOIN EW_PRD."OITM" AS B ON A."ItemCode"=B."ItemCode"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetPurchaseOrderListSettlement' THEN
		SELECT 
			 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
			,(SELECT DISTINCT T1."TrgetEntry" FROM EW_PRD."OPRQ" AS T0 LEFT JOIN EW_PRD."PRQ1" AS T1 ON T0."DocEntry"=T1."DocEntry" WHERE T0."DocEntry"=A."U_PRDocEntry" AND T1."TargetType"='22') AS DOCENTRY
			,A."DocNum" AS DOCNUM
			,A."U_NumAtCard" AS REFNO
			,B."NAME" AS JOBNUMBER
			,A."U_VendorCode" AS VENDORCODE
			,C."CardName" AS VENDORNAME
			,TO_VARCHAR(A."U_IssueDate",'dd-mm-YYYY') AS ISSUEDATE
			,A."U_Amount" AS DOCTOTAL
			,A."Remark" AS REMARKS
			,AA."DocEntry" AS PODOCENTRY
			,AA."Status" AS "Status"
			,IFNULL(D."U_IsUpdate",'') AS "IsUpdate"
		FROM EW_PRD."@ADVANCEPAYMENT" AS A
		LEFT JOIN EW_PRD."@SETTLEMENTHEADER" AS AA ON A."U_NumAtCard"=AA."U_RefNo"
		LEFT JOIN EW_PRD."OPMG" AS B ON B."AbsEntry"=A."U_Project"
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."U_VendorCode"
		LEFT JOIN EW_PRD."OPRQ" AS E ON E."DocEntry"=A."U_PRDocEntry"
		LEFT JOIN EW_PRD."OPOR" AS D ON D."DocEntry"=(SELECT DISTINCT "DocEntry" FROM EW_PRD."POR1" AS T0 WHERE T0."BaseEntry"=E."DocEntry" AND T0."BaseType"='1470000113')
		WHERE A."U_AdvanceType"='PRAD' AND A."U_PRDocEntry" IS NOT NULL
		AND (SELECT DISTINCT T1."TrgetEntry" 
					FROM EW_PRD."OPRQ" AS T0 
					LEFT JOIN EW_PRD."PRQ1" AS T1 ON T0."DocEntry"=T1."DocEntry" 
					WHERE T0."DocEntry"=A."U_PRDocEntry" AND T1."TargetType"='22') IS NOT NULL
		AND (AA."Status" IS NOT NULL OR IFNULL(D."U_IsUpdate",'')!='');
	ELSE IF :DTYPE='GetARCreditMemoListOnReduceInvoice' THEN
		SELECT 
			 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
			,A."DocEntry" AS DOCENTRY
			,A."DocNum" AS DOCNUM
			,A."NumAtCard" AS REFNO
			,(SELECT DISTINCT T0."Project" FROM EW_PRD."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
			,A."CardCode" AS VENDORCODE
			,C."CardName" AS VENDORNAME
			,TO_VARCHAR(A."DocDate",'dd-mm-YYYY') AS ISSUEDATE
			,A."DocTotal" AS DOCTOTAL
			,A."Comments" AS REMARKS
		FROM EW_PRD."ORIN" AS A
		--LEFT JOIN EW_PRD."OPMG" AS B ON B."AbsEntry"=
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."CardCode"
		WHERE A."U_WEBID" IS NOT NULL AND IFNULL(A."U_CreditMemoType",'')<>'Debit';
	ELSE IF :DTYPE='GetDetailARCreditMemoByDocEntry' THEN
		IF :par1='Header' THEN
			SELECT 
				 A."NumAtCard" AS DOCNUM
				,A."DocNum" AS DOCNUMPR
				,A."DocEntry" AS DOCENTRY
				,A."DocDate" AS ISSUEDATE
				,A."TaxDate" AS DUEDATE
				,A."CardCode" AS VENDORCODE
				,B."CardName" AS VENDORNAME
				,A."DocCur" AS CURRENCY
				,A."UserSign" AS EMPLOYEEID
				,C."firstName" ||' '|| C."lastName" AS EMPLOYEENAME
				,A."DocTotal" AS AMOUNT
				,A."Comments" AS REMARKS  
			FROM EW_PRD."ORIN" AS A
			LEFT JOIN EW_PRD."OCRD" AS B ON A."CardCode"=B."CardCode"
			LEFT JOIN EW_PRD."OHEM" AS C ON A."UserSign"=C."userId"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."ItemCode" AS ITEMCODE
				,B."ItemName" AS ITEMNAME
				,A."Quantity" AS QTY
				,A."LineTotal" AS PRICE
				,A."U_Remark" AS REMARKS
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS SERVICETYPE				
			FROM EW_PRD."RIN1" AS A
			LEFT JOIN EW_PRD."OITM" AS B ON A."ItemCode"=B."ItemCode"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetRecordCountUserDefindTable' THEN
		execute immediate 'SELECT 
			CAST(COUNT(*) AS INT) AS "RecordCount" FROM (
			SELECT DISTINCT "VisOrder" FROM EW_PRD."@'|| :par1 ||'" WHERE "DocEntry"='|| :par2 ||') AS A';
	ELSE IF :DTYPE='GetDetailJobSheetTruckingDetail' THEN
		Select 
			 C."SlpName" AS "SalesPerson"
			,CAST(A."DocEntry" AS INT) AS "DocNum"
			,CAST(A."DocEntry" AS INT) AS "DocEntry"
			,A."U_CURRENCYCOSTING" AS "CurrencyCosting"
			,A."U_CURRENCYSELLING" AS "CurrencySelling"
			,TO_DECIMAL(A."U_TOTALCOSTING",10,2) AS "TotalCosting"
			,TO_DECIMAL(A."U_TOTALSELLING",10,2) AS "TotalSelling"
			,TO_DECIMAL(A."U_REBATE",10,2) AS "Rebate"
			,TO_DECIMAL(A."U_GRANDTOTALCOSTING",10,2) AS "GrandTotalCosting"
			,TO_DECIMAL(A."U_GRANDTOTALSELLING",10,2) AS "GrandTotalSelling"
			,TO_DECIMAL(A."U_GRANDTOTALCOSTINGUSD",10,2) AS "GrandTotalCostingUSD"
			,TO_DECIMAL(A."U_GRANDTOTALSELLINGUSD",10,2) AS "GrandTotalSellingUSD"
			,TO_DECIMAL(A."U_TOTALPROFIT",10,2) AS "Profit"
			,A."U_REMARKSCOSTING" AS "RemarkForCosting"
			,A."U_REMAKRSSELLING" AS "RemarkForSelling"
			,(SELECT "Rate" FROM EW_PRD."ORTT" AS T0 WHERE T0."Currency"='USS' AND T0."RateDate"=A."CreateDate") AS "ExchangeRate"
		FROM EW_PRD."@JOBSHEETRUCKING" AS A
		LEFT JOIN EW_PRD."OQUT" AS B ON A."U_SALEQUOTATIONDOCNUM"=B."DocEntry"
		LEFT JOIN EW_PRD."OSLP" AS C ON C."SlpCode"=B."SlpCode"
		WHERE A."U_CONFIRMBOOKINGID"=:par1 AND A."DocEntry"=:par2;		
	ELSE IF :DTYPE='GetAttachmentJobSheetTruckingEdit' THEN
		SELECT 
			 IFNULL(A."U_ATTACHMENTNAME",'') AS "ATTACHMENTNAME"
			,IFNULL(A."LineId",0) AS "LineId"
		FROM EW_PRD."@ATTACHMENT" AS A 
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetLineCostingJobSheetTruckingEdit' THEN
		SELECT 
			 A."U_ITEMCODE" AS "ITEMCODE"
			,B."ItemName" AS "ITEMNAME"
			,TO_DECIMAL(A."U_TOTALPRICE",10,2) AS "TOTALPRICE"
			,TO_DECIMAL(A."U_Qty",10,0) AS "Qty"
		FROM EW_PRD."@JOBTRUCKCOSTING" AS A 
		LEFT JOIN EW_PRD."OITM" AS B ON A."U_ITEMCODE"=B."ItemCode"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetLineSellingJobSheetTruckingEdit' THEN
		SELECT 
			 A."U_ITEMCODE" AS "ITEMCODE"
			,B."ItemName" AS "ITEMNAME"
			,TO_DECIMAL(A."U_TOTALPRICE",10,2) AS "TOTALPRICE"
			,TO_DECIMAL(A."U_Qty",10,0) AS "Qty"
		FROM EW_PRD."@JOBTRUCKINGSELLING" AS A 
		LEFT JOIN EW_PRD."OITM" AS B ON A."U_ITEMCODE"=B."ItemCode"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE='GetDistributionRules' THEN
		SELECT "OcrCode" AS "Code","OcrName" AS "Name" FROM EW_PRD."OOCR";
	ELSE IF :DTYPE='SETTLEMENTDETAILBYDOCENTRY' THEN
		IF :par1='Header' THEN
			SELECT 
				 TO_VARCHAR(A."U_PODocEntry") AS "PRDocNum"
				,TO_VARCHAR(A."DocEntry") AS "DocEntry"
				,A."U_RefNo" AS "RefNo"
				,TO_VARCHAR(A."DocNum") AS "DocNum"
				,TO_VARCHAR(B."DocDate",'dd-mm-YYYY') AS "IssueDate"
				,C."CardCode" AS "VendorCode"
				,C."CardName" AS "VendorName"
				,TO_VARCHAR(B."DocDueDate",'dd-mm-YYYY') AS "DueDate"
				,IFNULL(E."firstName",'') ||' '|| IFNULL(E."lastName",'') AS "IssueByName"
				,CASE WHEN TO_VARCHAR(D."U_Project")='0' THEN '' ELSE IFNULL(TO_VARCHAR(F."NAME"),'') END AS "JobNo"
				,D."U_CurrencyType" AS "AmountCurrency"
				,TO_DECIMAL(B."DocTotal",10,2) AS "GrandTotal"
				,TO_DECIMAL(A."U_TotalPaid",10,2) AS "TotalPaid"				
				,IFNULL(C."DflAccount",'') AS "BankAccount"
				,IFNULL(C."DflBranch",'') AS "BranchName"
				,IFNULL((SELECT "OCRY"."Name" FROM EW_PRD."OCRY" WHERE "OCRY"."Code"=C."BankCountr"),'') AS "Country"
				,IFNULL((SELECT IFNULL("ODSC"."BankName",'') FROM EW_PRD."ODSC" WHERE "ODSC"."BankCode"=C."BankCode"),'') AS "BankName"
				,IFNULL(C."DflSwift",'') AS "SwiftCode"	
				,IFNULL(D."U_REASON",'') AS "Reason"
				,IFNULL((SELECT MAX(T0."LineNum") FROM EW_PRD."POR1" AS T0 WHERE T0."DocEntry"=A."U_PODocEntry"),0) AS MaxLineNum
			FROM EW_PRD."@SETTLEMENTHEADER" AS A
			LEFT JOIN EW_PRD."OPOR" AS B ON A."U_PODocEntry"=B."DocEntry"
			LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=B."CardCode"
			LEFT JOIN EW_PRD."@ADVANCEPAYMENT" AS D ON D."U_NumAtCard"=A."U_RefNo"
			LEFT JOIN EW_PRD."OHEM" AS E ON D."U_UserID"=E."empID"
			LEFT JOIN EW_PRD."OPMG" AS F ON F."AbsEntry"=D."U_Project"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."U_ItemCode" AS "ItemCode"
				,TO_INT(ROW_NUMBER() OVER(ORDER BY D."LineId")) AS ID 
				,B."ItemName" AS "ItemName"
				,A."U_Qty" AS "Qty"
				,TO_DECIMAL(D."U_Paid",10,2) AS "Paid"
				,TO_DECIMAL(D."U_Paid",10,2) AS "Amount"
				,IFNULL(A."U_Origin",'') AS "Origin"
				,IFNULL(A."U_Destination",'') AS "Destination"
				,IFNULL(A."U_Remarks",'') AS "remark"
				,IFNULL(D."U_JobNumber",'') AS "JobNo"
				,IFNULL(E."CardName",'') AS "CustomerName"
				,D."U_CustomerCode" AS "CustomerCode"
				,IFNULL(D."U_InvNumber",'') AS "RefInv"
				,IFNULL(D."U_DeclarationNo",'') AS "TransportationNo"
				,(SELECT "OUOM"."UomCode" FROM EW_PRD."OUOM" WHERE "OUOM"."UomEntry"=B."UgpEntry") AS "ServiceType"
				,IFNULL((SELECT T1."LineNum" AS "LineNum" 
					FROM EW_PRD."OPRQ" AS T0 
					LEFT JOIN EW_PRD."POR1" AS T1 ON T0."DocEntry"=T1."BaseEntry" AND T1."BaseType"=1470000113  
					WHERE T0."DocEntry"=C."U_PRDocEntry" AND T1."ItemCode"=A."U_ItemCode"),0) AS "LineNumPO"
				,IFNULL((SELECT T1."LineNum" AS "LineNum"
					FROM EW_PRD."OPRQ" AS T0 
					LEFT JOIN EW_PRD."PRQ1" AS T1 ON T0."DocEntry"=T1."DocEntry"  
					WHERE T0."DocEntry"=C."U_PRDocEntry" AND T1."ItemCode"=A."U_ItemCode"),0) AS "LineNumPR"
				,IFNULL(A."VisOrder",0) AS "LineNumAD"
				,A."U_DistributionRule" AS 	"DistributionRule"
			FROM EW_PRD."@SETTLEMENTROW" AS D
			LEFT JOIN EW_PRD."@SETTLEMENTHEADER" AS DD ON D."DocEntry"=DD."DocEntry"
			LEFT JOIN EW_PRD."@ADVANCEPAYMENT" AS C ON C."U_NumAtCard"=DD."U_RefNo"
			LEFT JOIN EW_PRD."@ADVANCEPAYMENTROW" AS A ON C."DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD."OITM" AS B ON D."U_ItemCode"=B."ItemCode"			
			LEFT JOIN EW_PRD."OCRD" AS E ON E."CardCode"=D."U_CustomerCode"
			WHERE D."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='GetCommissionBySaleEmployee' THEN
		SELECT 
			 A."U_JOBNO" AS "JobNo"
			,B."DocNum" AS "SONo"
			,C."DocNum"
			,Case WHEN C."PaidToDate"=C."DocTotal" THEN 
				CASE WHEN IFNULL(A."U_COSTINGCONFIRM",'N')='N' 
				THEN 'Paid Complete and Pending Confrim Costing' 
				ELSE  
					'Complete'
				END
			 ELSE 
			 	CASE WHEN IFNULL(A."U_COSTINGCONFIRM",'N')='Y' 
				THEN 'Confrim Complete and Pending Payment' 
				ELSE  
					'Pending'
				END
			 END AS "DocStatus"
			,CASE WHEN IFNULL(A."U_COSTINGCONFIRM",'N')='N' 
				THEN 0 
			 ELSE  
				D."CostingTotal"
			 END AS "GrandTotalCosting"
			,C."DocTotal" AS "GrandTotalSelling"
			,C."PaidToDate" AS "TotalPayment"
			,CASE WHEN IFNULL(A."U_COSTINGCONFIRM",'N')='N' 
				THEN 0 
			 ELSE  
				C."DocTotal"-D."CostingTotal"
			 END AS "GrossProfit"
			,E."Commission" AS "Commission"
			,CASE WHEN IFNULL(A."U_COSTINGCONFIRM",'N')='N' 
				THEN 0 
			 ELSE  
				((C."DocTotal"-D."CostingTotal")*E."Commission"/100)
			 END AS "TotalCommission"
			,C."DocTotal"-C."PaidToDate" AS "ARBalance"
			,F."SaleEmployee" AS "SaleEmployee"
		FROM EW_PRD."@JOBSHEETRUCKING" AS A
		LEFT JOIN EW_PRD."ORDR" AS B ON A."U_SALESORDERDOCNUM"=B."DocEntry"
		LEFT JOIN (
			SELECT DISTINCT
				 T1."BaseType"
				,T1."BaseEntry"
				,T0."DocEntry"
				,T0."DocNum"
				,T0."PaidToDate"
				,T0."DocTotal"
				,T0."SlpCode"
			FROM EW_PRD."OINV" AS T0 
			LEFT JOIN EW_PRD."INV1" AS T1 ON T0."DocEntry"=T1."DocEntry"
			WHERE T1."BaseType"='17' AND T1."Project" IS NOT NULL
		)AS C ON C."BaseEntry"=A."U_SALESORDERDOCNUM"
		LEFT JOIN (
			SELECT SUM(T0."U_TOTALPRICE")AS "CostingTotal",T0."DocEntry" FROM EW_PRD."@JOBTRUCKCOSTING" AS T0 GROUP BY T0."DocEntry"
		)AS D ON D."DocEntry"=A."DocEntry" 
		LEFT JOIN (
			SELECT 
				 T0."NAME" AS "ProjectName"
				,T0."EMPLOYEE" AS "SaleEmployee" 
				,T0."AbsEntry" AS "ProjectCode"
			FROM EW_PRD."OPMG" T0
		)AS F ON F."ProjectName"=A."U_JOBNO"
		LEFT JOIN (
			SELECT 
				 T0."Commission" AS "Commission"
				,T0."SlpCode" AS "SlpCode"
			FROM EW_PRD."OSLP" AS T0
		)AS E ON E."SlpCode"=F."SaleEmployee"
		WHERE C."BaseEntry" IS NOT NULL AND F."SaleEmployee"=:par1
		AND (
			SELECT COUNT(*) 
				FROM (
				SELECT DISTINCT T1."BaseEntry"
				FROM EW_PRD."OINV" AS T0 
				LEFT JOIN EW_PRD."INV1" AS T1 ON T0."DocEntry"=T1."DocEntry"
				WHERE T1."BaseType"='17' AND T1."Project"=A."U_JOBNO")
			)=(SELECT COUNT(*) FROM EW_PRD."@JOBSHEETRUCKING" AS T0 WHERE T0."U_JOBNO"=A."U_JOBNO")
		AND F."ProjectCode" NOT IN 
									(SELECT DISTINCT "U_JobNumber" 
										FROM EW_PRD."@TBCOMMISSIONROW" AS T0 
										LEFT JOIN EW_PRD."@TBCOMMISSION" AS T1 ON T0."DocEntry"=T1."DocEntry" 
										WHERE T1."Status"='O' AND T1."U_ApproveStatus"<>'R');
	ELSE IF :DTYPE='GetCommissionDetailByDocEntry' THEN
		IF :par1='Header' THEN
			SELECT 
				 A."DocEntry" AS "DocEntry"
				,CASE 
					WHEN A."U_ApproveStatus"='R' THEN 'Reject' 
					WHEN A."U_ApproveStatus"='A' THEN 'Approve' 
					WHEN A."U_ApproveStatus"='P' THEN 'Pending' END AS "ApproveStatus"
				,A."Status" AS "IsCancel"
				,A."U_UserID" AS "UserID"
				,D."firstName"||' '||D."lastName" AS "UserName"
				,IFNULL(A."Remark",'') AS "Reason"
				,A."U_SlpCode" AS "SlpCode"
				,E."SlpName" AS "SlpName"
				,IFNULL(B."U_VendorCode",'') AS "VendorCode"
				,TO_VARCHAR(A."U_IssueDate",'yyyy-MM-dd') AS "IssueDate"
				,CAST(C."GrandTotalCosting" AS double) AS "GrandTotalCosting"
				,CAST(C."GrandTotalSelling" AS double) AS "GrandTotalSelling"
				,CAST(C."GrandTotalGrossProfit" AS double) AS "GrandTotalGrossProfit"
				,CAST(C."GrandTotalCommission" AS double) AS "GrandTotalCommission"
				,F."Currency" AS "Currency"
			FROM EW_PRD."@TBCOMMISSION" AS A 
			LEFT JOIN EW_PRD."OHEM" AS B ON B."salesPrson"=A."U_SlpCode"
			LEFT JOIN (
				SELECT 
					 T0."DocEntry"
					,SUM(T0."U_GrandTotalCosting") AS "GrandTotalCosting"
					,SUM(T0."U_GrandTotalSelling") AS "GrandTotalSelling"
					,SUM(T0."U_GrossProfit") AS "GrandTotalGrossProfit"
					,SUM(T0."U_TotalSaleCommission") AS "GrandTotalCommission"
				FROM EW_PRD."@TBCOMMISSIONROW" AS T0 GROUP BY T0."DocEntry"
			)AS C ON C."DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD."OHEM" AS D ON D."empID"=A."U_UserID"
			LEFT JOIN EW_PRD."OSLP" AS E ON E."SlpCode"=A."U_SlpCode"
			LEFT JOIN EW_PRD."OCRD" AS F ON F."CardCode"=B."U_VendorCode"
			WHERE A."DocEntry"=:par2;
		ELSE IF :par1='Lines' THEN
			SELECT 
				 A."U_AccountCode" AS "AccountCode"
				,TO_VARCHAR(B."NAME") AS "JobNumber"
				,CAST(A."U_GrandTotalCosting" as double) AS "GrandTotalCosting"
				,CAST(A."U_GrandTotalSelling" AS double) AS "GrandTotalSelling"
				,CAST(A."U_GrossProfit" AS double) AS "GrossProfit"
				,CAST(A."U_Percentage" AS double) AS "Percentage"
				,CAST(A."U_TotalSaleCommission" As double) AS "TotalSaleCommission"
			FROM EW_PRD."@TBCOMMISSIONROW" AS A 
			LEFT JOIN EW_PRD."OPMG" AS B ON B."AbsEntry"=A."U_JobNumber"
			WHERE A."DocEntry"=:par2;
		END IF;
		END IF;
	ELSE IF :DTYPE='CheckCommissionDetailByJobNumber' THEN
		SELECT 
			 '5210-004' AS "AccountCode"
			,TO_VARCHAR(MAX(A."ProjectCode")) AS "JobNumber"
			,CAST(MAX(A."TotalCostingNotAccurate") AS double) AS "GrandTotalCosting"
			,CAST(SUM(A."DocTotal") AS double) AS "GrandTotalSelling"
			,CAST(SUM(A."DocTotal")-MAX(A."TotalCostingNotAccurate") AS double) AS "GrossProfit"
			,CAST(MAX(A."Commission") AS double) AS "Percentage"
			,CAST((SUM(A."DocTotal")-SUM(A."PaidToDate")) AS double) AS "ARBalance" 
			,CAST(((SUM(A."DocTotal")-MAX(A."TotalCostingNotAccurate"))* MAX(A."Commission"))/100 AS double) AS "TotalSaleCommission"
		FROM (SELECT Distinct
						 A."DocTotal"
						,A."DocEntry"
						,A."PaidToDate"
						,D."Commission"
						,(SELECT 
								SUM(B."U_TOTALPRICE")
							FROM EW_PRD."@JOBSHEETRUCKING" AS A
							LEFT JOIN EW_PRD."@JOBTRUCKCOSTING" AS B ON A."DocEntry"=B."DocEntry"
							WHERE A."U_JOBNO"=:par1) AS "TotalCostingNotAccurate"
						,C."ProjectCode"
					FROM EW_PRD."OINV" AS A 
					LEFT JOIN EW_PRD."INV1" AS B ON A."DocEntry"=B."DocEntry"
					LEFT JOIN (
						SELECT 
							 T0."NAME" AS "ProjectName"
							,T0."EMPLOYEE" AS "SaleEmployee"
							,T0."AbsEntry" AS "ProjectCode"
						FROM EW_PRD."OPMG" T0
					)AS C ON C."ProjectName"=B."Project"
					LEFT JOIN (
						SELECT 
							 T0."Commission" AS "Commission"
							,T0."SlpCode" AS "SlpCode"
						FROM EW_PRD."OSLP" AS T0
					)AS D ON D."SlpCode"=C."SaleEmployee"
					WHERE B."BaseType"='17' 
					AND B."Project"=:par1)AS A;
	ELSE IF :DTYPE='GetListSalesCommission' THEN
		SELECT 
			 CAST(A."DocEntry" AS int) AS "DocEntry"
			,CASE 
					WHEN A."U_ApproveStatus"='R' THEN 'Reject' 
					WHEN A."U_ApproveStatus"='A' THEN 'Approve' 
					WHEN A."U_ApproveStatus"='P' THEN 'Pending' END AS "ApproveStatus"
			,A."Status" AS "DocumentCancel"
			,CAST(A."DocNum" AS int) AS "DocNum"
			,B."SlpName" AS "SaleEmployee"
			,C."firstName"||' '||C."lastName" AS "UserCreated"
			,CAST(D."TotalCommission" as double) AS "TotalSalesComission"
		FROM EW_PRD."@TBCOMMISSION" AS A
		LEFT JOIN EW_PRD."OSLP" AS B ON A."U_SlpCode"=B."SlpCode"
		LEFT JOIN EW_PRD."OHEM" AS C ON C."empID"=A."U_UserID"
		LEFT JOIN (
		 SELECT 
		 	 T0."DocEntry" AS "DocEntry"
		 	,SUM(T0."U_TotalSaleCommission") AS "TotalCommission" 
		 FROM EW_PRD."@TBCOMMISSIONROW" AS T0 GROUP BY T0."DocEntry"
		)AS D ON D."DocEntry"=A."DocEntry";
	ELSE IF :DTYPE='GetARInvoiceWithJobNumberForDebit' THEN
		SELECT 
			 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
		 	,A."DocNum" AS DOCNUM
		 	,A."DocEntry" AS DOCENTRY
		 	,A."U_WEBID" AS WEBID
		 	,(SELECT DISTINCT INV1."Project" FROM EW_PRD."INV1" WHERE INV1."DocEntry"=A."DocEntry") AS JOBNUMBER
		 	,A."CardCode" AS CustomerCode
		 	,B."CardName" AS CustomerName
		 	,TO_VARCHAR(A."DocDate",'dd-mm-YYYY') AS ISSUEDATE
		 	,A."DocTotal" AS DOCTOTAL
		 	,A."Comments" AS REMARK
		 FROM EW_PRD."OINV" AS A 
		 LEFT JOIN EW_PRD."OCRD" AS B ON A."CardCode"=B."CardCode"
		 WHERE 		A."U_WEBID" IS NOT NULL 
		 	   --AND (A."DocTotal"-A."PaidToDate")<>0 
		 	   AND IFNULL((SELECT DISTINCT INV1."Project" FROM EW_PRD."INV1" WHERE INV1."DocEntry"=A."DocEntry"),'')<>'';
	ELSE IF :DTYPE='GetARDeditMemoListOnReduceInvoice' THEN
		SELECT 
			 ROW_NUMBER ( ) OVER( ORDER BY A."DocNum" DESC ) AS ROWNUM
			,A."DocEntry" AS DOCENTRY
			,A."DocNum" AS DOCNUM
			,A."NumAtCard" AS REFNO
			,(SELECT DISTINCT T0."Project" FROM EW_PRD."RIN1" T0 WHERE T0."DocEntry"=A."DocEntry") AS JOBNUMBER
			,A."CardCode" AS VENDORCODE
			,C."CardName" AS VENDORNAME
			,TO_VARCHAR(A."DocDate",'dd-mm-YYYY') AS ISSUEDATE
			,A."DocTotal" AS DOCTOTAL
			,A."Comments" AS REMARKS
		FROM EW_PRD."ORIN" AS A
		--LEFT JOIN EW_PRD."OPMG" AS B ON B."AbsEntry"=
		LEFT JOIN EW_PRD."OCRD" AS C ON C."CardCode"=A."CardCode"
		WHERE A."U_CreditMemoType"='Debit';
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
