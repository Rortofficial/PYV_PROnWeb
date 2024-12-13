ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLREPORT_SUMMARY_SELLING_MONTHLY_REPORT(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250),
	in par3 NVARCHAR(250),
	in par4 NVARCHAR(250)
)
AS
BEGIN
	SELECT * FROM (
		SELECT 
			 A."DocEntry" AS "ID"
			,B."SlpName" AS "SALESNAME"
			,(SELECT STRING_AGG("DOCNUM",',') FROM EW_PRD_TEST."PMG4" WHERE "AbsEntry"=D."U_PROJECTMANAGEMENTID" AND "TYP"='13') AS "EWTINVNO"
			,C."descript"||' - '||CC."descript" AS "Route"
			,A."U_EWSeries"||A."U_JOBNO" AS "JOBNO"
			,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS "LOADINGDATE"
			,IFNULL(F."CardName",'') AS "CO"
			,(SELECT STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBSHIPPER" AS T0 
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
				WHERE T0."DocEntry"=A."DocEntry") AS "SHIPPER"
			,(SELECT STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBCONSIGNEE" AS T0 
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
				WHERE T0."DocEntry"=A."DocEntry") AS "CONSIGNEE"
			,(SELECT STRING_AGG(T0."QTY"||'x'||T0."VOLUMECODE",',') 
				FROM (SELECT SUM(T0."U_QTY") AS "QTY",T0."U_VOLUMECODE" AS "VOLUMECODE"
						FROM EW_PRD_TEST."@VOLUME" AS T0 
						WHERE T0."DocEntry"=A."DocEntry" GROUP BY T0."U_VOLUMECODE"
					  UNION ALL	
						SELECT CAST(SUM(T0."U_QTY") AS INT) AS "QTY",T0."U_TRUCKTYPE" AS "VOLUMECODE"
						FROM EW_PRD_TEST."@TBTRUCKTYPEROW" AS T0 
						WHERE T0."DocEntry"=A."DocEntry" GROUP BY T0."U_TRUCKTYPE"
					) AS T0
					) AS "VOLUME"
			,--CASE WHEN IFNULL(E."U_SALESORDERDOCNUM",0)!=0 THEN
				TO_DECIMAL(
					CASE WHEN E."U_CURRENCYSELLING"IN ('THB','THS') THEN 
						H."TOTALSELLING" 
					ELSE 
						IFNULL(H."TOTALSELLING",0) *  IFNULL((SELECT "DocRate" FROM EW_PRD_TEST."ORDR" AS T0 WHERE T0."DocEntry"=IFNULL(E."U_SALESORDERDOCNUM",0)),0)
					END
					,12,2) AS "SELLING"
			 --ELSE 0 END 
			,--CASE WHEN IFNULL(E."U_SALESORDERDOCNUM",0)!=0 THEN
				TO_DECIMAL(CASE WHEN E."U_CURRENCYCOSTING"IN ('THB','THS') THEN 
					G."TOTALCOSTING"
				ELSE 
					IFNULL(G."TOTALCOSTING",0) * IFNULL((SELECT "Rate" FROM EW_PRD_TEST."ORTT" AS T0 WHERE T0."Currency"=E."U_CURRENCYCOSTING" AND T0."RateDate"=E."CreateDate"),1)
				END,12,2) AS "COSTING"
			 --ELSE 0 END 
			,IFNULL(J."Total",0)*-1 AS "CreditNote"
			,IFNULL(I."Total",0)*-1 AS "DebitNote"
			,IFNULL(E."U_REBATE",0) AS "REBATE"
			,IFNULL(E."U_USERCREATE",'') AS "CS"
			,(SELECT STRING_AGG(T0."U_INVOICE"||',') 
				FROM EW_PRD_TEST."@COMMODITY" AS T0 WHERE T0."DocEntry"=A."DocEntry") AS "COMMODITY"
			,(SELECT STRING_AGG(IFNULL(T1."InventryNo"||' / ','')||IFNULL(T2."AttriTxt1",''),',')
				FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
				LEFT JOIN EW_PRD_TEST."OITM" AS T1 ON T1."ItemCode"=T0."U_TRUCKCODE"
				LEFT JOIN EW_PRD_TEST."ITM13" AS T2 ON T2."ItemCode"=T1."ItemCode" WHERE T0."DocEntry"=D."DocEntry") AS "TruckNo"
			,CASE WHEN A."Status"='C' THEN 
						 	'Cancel'
						  WHEN A."Status"='O' THEN
						  	CASE WHEN (SELECT COUNT(*)
						  				FROM EW_PRD_TEST."@JOBSHEETRUCKING" 
						  				WHERE "U_CONFIRMBOOKINGID"=(SELECT T0."DocEntry" 
						  											 FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
						 											 	WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O'))=1 THEN
						  	CASE WHEN IFNULL(E."U_SALESORDERDOCNUM",0)<>0 THEN
						  		'JOB SHEET TRUCKING'
						  	ELSE 'JOB SHEET TRUCKING DRAFT' END
						  	WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')=1 THEN
						 		'CONFIRM BOOKING SHEET'
						 	ELSE
						 		'BOOKING SHEET'
						  	END
						  END AS "DocType"
		FROM EW_PRD_TEST."@BOOKINGSHEET" AS A
		LEFT JOIN EW_PRD_TEST."OSLP" AS B ON B."SlpCode"=A."U_SALEEMPLOYEE"
		LEFT JOIN EW_PRD_TEST."OTER" AS C ON C."territryID"=A."U_ORIGIN" -- Origin
		LEFT JOIN EW_PRD_TEST."OTER" AS CC ON CC."territryID"=A."U_DESTINATION" -- Origin
		LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS D ON D."U_BOOKINGID"=A."DocEntry" AND D."Status"='O'
		LEFT JOIN EW_PRD_TEST."@JOBSHEETRUCKING" AS E ON E."U_CONFIRMBOOKINGID"=D."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS F ON F."CardCode"=E."U_CARDCODE"
		LEFT JOIN (
			SELECT 
				SUM("U_Qty"*"U_TOTALPRICE") AS "TOTALCOSTING"
			  ,"DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@JOBTRUCKCOSTING" 
			GROUP BY "DocEntry"
		) AS G ON G."DocEntry"=E."DocEntry" -- Costing
		LEFT JOIN (
			SELECT 
				SUM("U_Qty"*"U_TOTALPRICE") AS "TOTALSELLING"
			  ,"DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@JOBTRUCKINGSELLING" 
			GROUP BY "DocEntry"
		) AS H ON H."DocEntry"=E."DocEntry" --Selling
		LEFT JOIN (
			SELECT 
				 SUM(C."DocTotal"*C."DocRate") AS "Total"
				,B."NAME" AS "JobNo"
			FROM EW_PRD_TEST."PMG4" AS A
			LEFT JOIN EW_PRD_TEST."OPMG" AS B ON B."AbsEntry"=A."AbsEntry"
			LEFT JOIN EW_PRD_TEST."ORIN" AS C ON C."DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."NNM1" AS D ON D."Series"=C."Series" AND D."ObjectCode"=14
			WHERE 
				/* A."AbsEntry"='5832''5703'  
				AND */
				A."TYP"=14 
				AND C."CANCELED"='N'
				AND D."BeginStr"='DN'
			GROUP BY B."NAME"
		)AS I ON I."JobNo"=A."U_EWSeries"||A."U_JOBNO"
		LEFT JOIN (
			SELECT 
				 SUM(C."DocTotal"*C."DocRate") AS "Total"
				,B."NAME" AS "JobNo"
			FROM EW_PRD_TEST."PMG4" AS A
			LEFT JOIN EW_PRD_TEST."OPMG" AS B ON B."AbsEntry"=A."AbsEntry"
			LEFT JOIN EW_PRD_TEST."ORIN" AS C ON C."DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."NNM1" AS D ON D."Series"=C."Series" AND D."ObjectCode"=14
			WHERE 
				/* A."AbsEntry"='5832''5703'  
				AND */
				A."TYP"=14 
				AND C."CANCELED"='N'
				AND D."BeginStr"='CN'
			GROUP BY B."NAME"
		)AS J ON J."JobNo"=A."U_EWSeries"||A."U_JOBNO"
		WHERE 
		--A."CreateDate" BETWEEN :par1 AND :par2 
		A."U_LOADINGDATE" BETWEEN :par1 AND :par2 
		AND A."U_SALEEMPLOYEE"=CASE WHEN :par3='-99' THEN A."U_SALEEMPLOYEE" ELSE :par3 END
		AND A."Status"='O'
		AND IFNULL(E."Status",'O')='O'
		AND 1=CASE WHEN :par4 IN ('1','-99') THEN 1 ELSE 2 END
		ORDER BY A."DocEntry" ASC
	)

UNION ALL
	SELECT * FROM (
		SELECT 
			 A."DocEntry" AS "ID"
			,B."SlpName" AS "SALESNAME"
			,(SELECT STRING_AGG("DOCNUM",',') FROM EW_PRD_TEST."PMG4" WHERE "AbsEntry"=D."U_PROJECTMANAGEMENTID" AND "TYP"='13') AS "EWTINVNO"
			,C."descript"||' - '||CC."descript" AS "Route"
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
			,TO_VARCHAR(A."U_LOADINGDATE",'DD/MM/YYYY') AS "LOADINGDATE"
			/*,CASE WHEN IFNULL(E."U_SALESORDERDOCNUM",0)<>0 THEN
						  		IFNULL(F."CardName",'')
			 ELSE '' END AS "CO"*/
			,IFNULL(F."CardName",'') AS "CO"
			,(SELECT STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0 
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
				WHERE T0."DocEntry"=A."DocEntry") AS "SHIPPER"
			,(SELECT STRING_AGG(T1."CardName",',')
				FROM EW_PRD_TEST."@TBCONSIGNEE" AS T0 
				LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
				WHERE T0."DocEntry"=A."DocEntry") AS "CONSIGNEE"
			,(SELECT STRING_AGG(T0."QTY"||'x'||T0."VOLUMECODE",',') 
				FROM (SELECT CAST(SUM(T0."U_Qty") AS INT) AS "QTY",T0."U_ContainerType" AS "VOLUMECODE"
						FROM EW_PRD_TEST."@TB_CON_S_A" AS T0 
						WHERE T0."DocEntry"=A."DocEntry" GROUP BY T0."U_ContainerType"
					  UNION ALL	
						SELECT CAST(SUM(T0."U_Qty") AS INT) AS "QTY",T0."U_ContainerType" AS "VOLUMECODE"
						FROM EW_PRD_TEST."@TB_TRUCK_S_A" AS T0 
						WHERE T0."DocEntry"=A."DocEntry" GROUP BY T0."U_ContainerType"
					) AS T0
					) AS "VOLUME"
			,TO_DECIMAL(IFNULL(H."TOTALSELLING",0),12,2) AS "SELLING"
			,TO_DECIMAL(IFNULL(G."TOTALCOSTING",0),12,2) AS "COSTING"
			,IFNULL(J."Total",0)*-1 AS "CreditNote"
			,IFNULL(I."Total",0)*-1 AS "DebitNote"
			,IFNULL(E."U_REBATE",0) AS "REBATE"
			,IFNULL(E."U_USERCREATE",'') AS "CS"
			,(SELECT STRING_AGG(T0."U_INVOICE"||',') 
				FROM EW_PRD_TEST."@COMMODITY" AS T0 WHERE T0."DocEntry"=A."DocEntry") AS "COMMODITY"
			,(SELECT STRING_AGG(IFNULL(T1."InventryNo"||' / ','')||IFNULL(T2."AttriTxt1",''),',')
				FROM EW_PRD_TEST."@TRUCK_INFO_S_A" AS T0
				LEFT JOIN EW_PRD_TEST."OITM" AS T1 ON T1."ItemCode"=T0."U_TRUCKCODE"
				LEFT JOIN EW_PRD_TEST."ITM13" AS T2 ON T2."ItemCode"=T1."ItemCode" WHERE T0."DocEntry"=D."DocEntry") AS "TruckNo"
			,CASE WHEN A."Status"='C' THEN 
						 	'Cancel'
						  WHEN A."Status"='O' THEN
						  	CASE WHEN (SELECT COUNT(*)
						  				FROM EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" 
						  				WHERE "U_CONFIRMBOOKINGID"=(SELECT T0."DocEntry" 
						  											 FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 											 	WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O'))=1 THEN
						  	CASE WHEN IFNULL(E."U_SALESORDERDOCNUM",0)<>0 THEN
						  		'JOB SHEET TRUCKING'
						  	ELSE 'JOB SHEET TRUCKING DRAFT' END
						  	WHEN (SELECT COUNT(T0."DocEntry") 
						  			FROM EW_PRD_TEST."@CON_BOOKING_S_A" AS T0 
						 				WHERE T0."U_BOOKINGID"=A."DocEntry" AND T0."Status"='O')=1 THEN
						 		'CONFIRM BOOKING SHEET'
						 	ELSE
						 		'BOOKING SHEET'
						  	END
						  END AS "DocType"
		FROM EW_PRD_TEST."@BOOKING_SEA_AIR" AS A
		LEFT JOIN EW_PRD_TEST."@TBJOBTYPE" AS AA ON AA."Code"=A."U_IMPORTTYPE"
		LEFT JOIN EW_PRD_TEST."OSLP" AS B ON B."SlpCode"=A."U_SALEEMPLOYEE"
		LEFT JOIN EW_PRD_TEST."OTER" AS C ON TO_VARCHAR(C."territryID")=A."U_ORIGIN" -- Origin
		LEFT JOIN EW_PRD_TEST."OTER" AS CC ON TO_VARCHAR(CC."territryID")=A."U_DESTINATION" -- Origin
		LEFT JOIN EW_PRD_TEST."@CON_BOOKING_S_A" AS D ON D."U_BOOKINGID"=A."DocEntry" AND D."Status"='O'
		LEFT JOIN EW_PRD_TEST."@TB_JOBSHEET_SEA_AIR" AS E ON E."U_CONFIRMBOOKINGID"=D."DocEntry"
		LEFT JOIN EW_PRD_TEST."OCRD" AS F ON F."CardCode"=E."U_CARDCODE"
		LEFT JOIN (
			SELECT 
			   SUM(IFNULL("U_TotalBaht",0)) AS "TOTALCOSTING"
			  ,"DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@TB_JOBSHEET_COSTING" 
			GROUP BY "DocEntry"
		) AS G ON G."DocEntry"=E."DocEntry" -- Costing
		LEFT JOIN (
			SELECT 
				SUM(IFNULL("U_TotalBaht",0)) AS "TOTALSELLING"
			  ,"DocEntry" AS "DocEntry"
			FROM EW_PRD_TEST."@TB_JOBSHEET_SELLING" 
			GROUP BY "DocEntry"
		) AS H ON H."DocEntry"=E."DocEntry" --Selling
		LEFT JOIN (
			SELECT 
				 SUM(C."DocTotal"*C."DocRate") AS "Total"
				,B."NAME" AS "JobNo"
			FROM EW_PRD_TEST."PMG4" AS A
			LEFT JOIN EW_PRD_TEST."OPMG" AS B ON B."AbsEntry"=A."AbsEntry"
			LEFT JOIN EW_PRD_TEST."ORIN" AS C ON C."DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."NNM1" AS D ON D."Series"=C."Series" AND D."ObjectCode"=14
			WHERE 
				/* A."AbsEntry"='5832''5703'  
				AND */
				A."TYP"=14 
				AND C."CANCELED"='N'
				AND D."BeginStr"='DN'
			GROUP BY B."NAME"
		)AS I ON I."JobNo"=CASE WHEN A."CreateDate"<'2024-03-06' THEN
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
						  ELSE IFNULL(AA."U_JOBTYPE",'')||A."DocNum" END
		LEFT JOIN (
			SELECT 
				 SUM(C."DocTotal"*C."DocRate") AS "Total"
				,B."NAME" AS "JobNo"
			FROM EW_PRD_TEST."PMG4" AS A
			LEFT JOIN EW_PRD_TEST."OPMG" AS B ON B."AbsEntry"=A."AbsEntry"
			LEFT JOIN EW_PRD_TEST."ORIN" AS C ON C."DocEntry"=A."DocEntry"
			LEFT JOIN EW_PRD_TEST."NNM1" AS D ON D."Series"=C."Series" AND D."ObjectCode"=14
			WHERE 
				/* A."AbsEntry"='5832''5703'  
				AND */
				A."TYP"=14 
				AND C."CANCELED"='N'
				AND D."BeginStr"='CN'
			GROUP BY B."NAME"
		)AS J ON J."JobNo"=CASE WHEN A."CreateDate"<'2024-03-06' THEN
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
						  ELSE IFNULL(AA."U_JOBTYPE",'')||A."DocNum" END
		WHERE
		--A."U_LOADINGDATE" BETWEEN :par1 AND :par2 
		A."CreateDate" BETWEEN :par1 AND :par2 
		AND A."U_SALEEMPLOYEE"=CASE WHEN :par3='-99' THEN A."U_SALEEMPLOYEE" ELSE :par3 END
		AND A."Status"='O'
		AND IFNULL(E."Status",'O')='O'
		AND 1=CASE WHEN :par4 IN ('2','-99') THEN 1 ELSE 2 END
		--AND 
		--A."DocEntry"='1079'
		ORDER BY A."DocEntry" ASC
	);

END;
