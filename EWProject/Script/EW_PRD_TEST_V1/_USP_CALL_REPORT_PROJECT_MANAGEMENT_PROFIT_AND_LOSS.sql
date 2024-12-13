ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_REPORT_PROJECT_MANAGEMENT_PROFIT_AND_LOSS(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250)
	/*,
	in par3 NVARCHAR(250),
	in par4 NVARCHAR(250)*/
)
AS
BEGIN
	SELECT 
		 "ID" AS "ID"
		,"SALESNAME"
		,"Route"
		,"JOBNO"
		,"LOADINGDATE"
		,"CO"
		,"SHIPPER"
		,"CONSIGNEE"
		,"COMMODITY"
		,"VOLUME"
		/* Costing */
		,"Credit/Debit_Note_in_A/P_Invoice"
		,"Total_COGS_in_Purchase_Order"
		,"Total_COGS_in_A/P_Invoice"
		,"Total_Duty_Tax_in_Purchase_Order"
		,"Total_Duty_Tax_in_A/P_Invoice"
		,"Total_Advance_in_Purchase_Order"
		,"Total_Advance_in_A/P_Invoice"
		,"Total_Sale_Coms_in_Purchase_Order"
		,"Total_Sale_Coms_in_A/P_Invoice"
		,"Total_Sale_Cost_Not_Actual_Cost"
		,"Rebate"
		/* End Costing */
		/* Selling */
		,"Credit/Debit_Note_in_A/R_Invoice"
		,"Total_COGS_in_Sale_Order"
		,"Total_COGS_in_A/R_Invoice"
		,"Total_Duty_Tax_in_Sale_Order"
		,"Total_Duty_Tax_in_A/R_Invoice"
		,"Total_Advance_in_Sale_Order"
		,"Total_Advance_in_A/R_Invoice"
		/* End Selling */
		/* Summary Profit and Lost */
		,("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") 
			+ 
		 ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice") AS "Gross/Profit_in_Advance"
		,(("Total_COGS_in_A/R_Invoice" + (("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))) 
			-
		 ("Total_COGS_in_A/P_Invoice" - "Total_Sale_Coms_in_Purchase_Order")  
			- 
		 ("Total_COGS_in_A/R_Invoice" - "Total_Sale_Cost_Not_Actual_Cost")
			+ 
		 ("Rebate" + ("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))) AS "Diff_Gross/Profit_of_Sale"
		,("Total_COGS_in_A/R_Invoice" + (("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))) 
			-
		 ("Total_COGS_in_A/P_Invoice" - "Total_Sale_Coms_in_Purchase_Order") AS "Gross/Profit"
		,("Total_COGS_in_A/R_Invoice" - "Total_Sale_Cost_Not_Actual_Cost")
			+ 
		 ("Rebate" + ("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))  AS "Gross/Profit_for_Sale"
		,("Total_COGS_in_A/R_Invoice" - "Total_Sale_Cost_Not_Actual_Cost")
			+ 
		 ("Rebate" + ("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice")) AS "Total_Gross/Profit_for_Sale"
		/* End Summary Profit and Lost*/
		/* Other */
		,"CS"
		,"DocType"
		/* End Other */
	FROM (
			SELECT 
				/* Job Number Information */
				 A."DocEntry" AS "ID"
				,B."SlpName" AS "SALESNAME"
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
				,(SELECT STRING_AGG(T0."U_INVOICE"||',') 
					FROM EW_PRD_TEST."@COMMODITY" AS T0 WHERE T0."DocEntry"=A."DocEntry") AS "COMMODITY"
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
				/* End Job Number Information */
				/* Costing Price */
				,IFNULL((SELECT 
						SUM(T1."LineTotal")
					FROM EW_PRD_TEST."ORPC" T0  
					INNER JOIN EW_PRD_TEST."RPC1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
					T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
					AND T0."CANCELED" <>'C' 
					AND T0."CANCELED" <> 'Y' 
					AND T0."DocStatus" = 'C'),0) AS "Credit/Debit_Note_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
				  FROM EW_PRD_TEST."OPOR" T0  
				  INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
				  WHERE 
					T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
					AND T0."CANCELED" <>'C' 
					AND T0."CANCELED" <> 'Y' 
					AND T0."DocStatus" <> 'C' 
					AND T1."LineStatus" <> 'C'
					AND T0."U_PRAD_TYPE" = 'COS' 
					AND T1."ItemCode"!='03-ADV-1-001'
					AND T1."ItemCode" NOT IN(SELECT 
												"ItemCode" 
												FROM EW_PRD_TEST."PCH1" AS A
												WHERE 
													A."BaseEntry"=T1."DocEntry"
													AND A."BaseType"='22'
													AND A."TargetType"='19')
				 ),0) AS "Total_COGS_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" AS T0 
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T1."LineStatus" <> 'C' 
						AND T0."U_PRAD_TYPE" = 'COS' 
						AND T1."ItemCode"!='03-ADV-1-001' 
						AND T1."ItemCode" NOT LIKE '21224104' ),0) AS "Total_COGS_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPOR" T0
					INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" = '03-ADV-1-001'
						AND T1."ItemCode" NOT IN(SELECT 
													"ItemCode" 
												  FROM EW_PRD_TEST."PCH1" AS A
												  WHERE 
												  	A."BaseEntry"=T1."DocEntry"
												  	AND A."BaseType"='22'
												  	AND A."TargetType"='19')
				 ),0) AS "Total_Duty_Tax_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" T0  
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND  T1."ItemCode" = '03-ADV-1-001'),0) AS "Total_Duty_Tax_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPOR" T0  
					INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <> 'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T0."U_PRAD_TYPE" = 'ADV' 
						AND T1."ItemCode"!='03-ADV-1-001'),0) AS "Total_Advance_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" T0  
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE  
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."U_PRAD_TYPE" = 'ADV' 
						AND T1."ItemCode" <> '03-ADV-1-001'),0) AS "Total_Advance_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPOR" T0  
					INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <> 'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND IFNULL(TO_VARCHAR(T0."U_Commission"),'') <> ''),0) AS "Total_Sale_Coms_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" T0
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE  
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND IFNULL(TO_VARCHAR(T0."U_Commission"),'') <> ''),0) AS "Total_Sale_Coms_in_A/P_Invoice"
				,IFNULL(E."U_TOTALCOSTING",0) AS "Total_Sale_Cost_Not_Actual_Cost"
				,IFNULL(E."U_REBATE",0) AS "Rebate"
				/* End Costing Price */
				/* Selling Price */
				,IFNULL((SELECT 
						SUM(T1."LineTotal")  
					FROM EW_PRD_TEST."ORIN" T0  
					INNER JOIN EW_PRD_TEST."RIN1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" = 'O'),0) AS "Credit/Debit_Note_in_A/R_Invoice"
				,IFNULL((SELECT 
						SUM(T1."LineTotal") 
					FROM EW_PRD_TEST."ORDR" T0  
					INNER JOIN EW_PRD_TEST."RDR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y'
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" NOT Like '%%03-ADV%%'
						AND T1."ItemCode" <> '03-ADV-1-001'),0) AS "Total_COGS_in_Sale_Order"
				,IFNULL((SELECT  
					(SELECT 
						SUM(T1."LineTotal") 
					 FROM EW_PRD_TEST."OINV" T0  
					 INNER JOIN EW_PRD_TEST."INV1" T1 ON T0."DocEntry" = T1."DocEntry" 
					 WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						/*AND T0."DocStatus" = 'C'*/ 
						AND T1."ItemCode"  NOT Like '%%03-ADV%%'
						AND T1."ItemCode" <> '03-ADV-1-001') 
					 - 
				 	(SELECT 
						SUM(T1."LineTotal")  
					FROM EW_PRD_TEST."ORIN" T0  
					INNER JOIN EW_PRD_TEST."RIN1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" = 'O') 
				 FROM DUMMY ),0) AS "Total_COGS_in_A/R_Invoice"
				,IFNULL((SELECT 
					SUM(T1."OpenSum") 
				  FROM EW_PRD_TEST."ORDR" T0  
				  INNER JOIN EW_PRD_TEST."RDR1" T1 ON T0."DocEntry" = T1."DocEntry" 
				  WHERE 
					T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
					AND T0."CANCELED" <>'Y' 
					AND T0."DocStatus" <> 'C' 
					AND T1."LineStatus" <> 'C' 
					AND T1."ItemCode" = '03-ADV-1-001'),0) AS "Total_Duty_Tax_in_Sale_Order"
				,IFNULL((SELECT 
					SUM(T1."OpenSum") 
				  FROM EW_PRD_TEST."OINV" T0  
				  INNER JOIN EW_PRD_TEST."INV1" T1 ON T0."DocEntry" = T1."DocEntry" 
				  WHERE 
					T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
					AND T0."CANCELED" <>'Y' 
					AND T0."DocStatus" <> 'C' 
					AND T1."LineStatus" <> 'C' 
					AND T1."ItemCode" = '03-ADV-1-001'),0) AS "Total_Duty_Tax_in_A/R_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."ORDR" T0  
					INNER JOIN EW_PRD_TEST."RDR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" Like '%%03-ADV%%' 
						--AND T1."ItemCode" NOT Like '%%03-ADV-1-001%%'
						AND T1."ItemCode" <> '03-ADV-1-001'),0) AS "Total_Advance_in_Sale_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OINV" T0  
					INNER JOIN EW_PRD_TEST."INV1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."U_EWSeries"||A."U_JOBNO")
						AND T0."CANCELED" <>'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" Like '%%03-ADV%%' 
						--AND T1."ItemCode" NOT Like '%%03-ADV-1-001%%'
						AND T1."ItemCode"!='03-ADV-1-001'),0) AS "Total_Advance_in_A/R_Invoice"
				/* End Selling Price */
				/* Other */
				,IFNULL(E."U_USERCREATE",'') AS "CS"
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
				/* End Other */
			FROM EW_PRD_TEST."@BOOKINGSHEET" AS A
			LEFT JOIN EW_PRD_TEST."OSLP" AS B ON B."SlpCode"=A."U_SALEEMPLOYEE"
			LEFT JOIN EW_PRD_TEST."OTER" AS C ON C."territryID"=A."U_ORIGIN" -- Origin
			LEFT JOIN EW_PRD_TEST."OTER" AS CC ON CC."territryID"=A."U_DESTINATION" -- Origin
			LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS D ON D."U_BOOKINGID"=A."DocEntry" AND D."Status"='O'
			LEFT JOIN EW_PRD_TEST."@JOBSHEETRUCKING" AS E ON E."U_CONFIRMBOOKINGID"=D."DocEntry"
			LEFT JOIN EW_PRD_TEST."OCRD" AS F ON F."CardCode"=E."U_CARDCODE"
			WHERE 
			--A."CreateDate" BETWEEN :par1 AND :par2 
			A."U_LOADINGDATE" BETWEEN :par1 AND :par2 
			--AND A."U_SALEEMPLOYEE"=CASE WHEN '-99'='-99' THEN A."U_SALEEMPLOYEE" ELSE '-99' END
			AND A."Status"='O'
			AND IFNULL(E."Status",'O')='O'
			--AND 1=CASE WHEN 1 IN ('1','-99') THEN 1 ELSE 2 END
			ORDER BY A."DocEntry" ASC
	)AS A --WHERE A."JOBNO"='EWTT24030473'--;
	
	UNION ALL
	
	SELECT
		 "ID" AS "ID"
		,"SALESNAME"
		,"Route"
		,"JOBNO"
		,"LOADINGDATE"
		,"CO"
		,"SHIPPER"
		,"CONSIGNEE"
		,"COMMODITY"
		,"VOLUME"
		/* Costing */
		,"Credit/Debit_Note_in_A/P_Invoice"
		,"Total_COGS_in_Purchase_Order"
		,"Total_COGS_in_A/P_Invoice"
		,"Total_Duty_Tax_in_Purchase_Order"
		,"Total_Duty_Tax_in_A/P_Invoice"
		,"Total_Advance_in_Purchase_Order"
		,"Total_Advance_in_A/P_Invoice"
		,"Total_Sale_Coms_in_Purchase_Order"
		,"Total_Sale_Coms_in_A/P_Invoice"
		,"Total_Sale_Cost_Not_Actual_Cost"
		,"Rebate"
		/* End Costing */
		/* Selling */
		,"Credit/Debit_Note_in_A/R_Invoice"
		,"Total_COGS_in_Sale_Order"
		,"Total_COGS_in_A/R_Invoice"
		,"Total_Duty_Tax_in_Sale_Order"
		,"Total_Duty_Tax_in_A/R_Invoice"
		,"Total_Advance_in_Sale_Order"
		,"Total_Advance_in_A/R_Invoice"
		/* End Selling */
		/* Summary Profit and Lost */
		,("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") 
			+ 
		 ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice") AS "Gross/Profit_in_Advance"
		,(("Total_COGS_in_A/R_Invoice" + (("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))) 
			-
		 ("Total_COGS_in_A/P_Invoice" - "Total_Sale_Coms_in_Purchase_Order")  
			- 
		 ("Total_COGS_in_A/R_Invoice" - "Total_Sale_Cost_Not_Actual_Cost")
			+ 
		 ("Rebate" + ("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))) AS "Diff_Gross/Profit_of_Sale"
		,("Total_COGS_in_A/R_Invoice" + (("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))) 
			-
		 ("Total_COGS_in_A/P_Invoice" - "Total_Sale_Coms_in_Purchase_Order") AS "Gross/Profit"
		,("Total_COGS_in_A/R_Invoice" - "Total_Sale_Cost_Not_Actual_Cost")
			+ 
		 ("Rebate" + ("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice"))  AS "Gross/Profit_for_Sale"
		,("Total_COGS_in_A/R_Invoice" - "Total_Sale_Cost_Not_Actual_Cost")
			+ 
		 ("Rebate" + ("Total_Duty_Tax_in_A/R_Invoice" - "Total_Duty_Tax_in_A/P_Invoice") + ("Total_Advance_in_A/R_Invoice" - "Total_Advance_in_A/P_Invoice")) AS "Total_Gross/Profit_for_Sale"
		/* End Summary Profit and Lost*/
		/* Other */
		,"CS"
		,"DocType"
		/* End Other */ 
	 FROM (
			SELECT *
				/* Costing Price */
				,IFNULL((SELECT 
						SUM(T1."LineTotal")
					FROM EW_PRD_TEST."ORPC" T0  
					INNER JOIN EW_PRD_TEST."RPC1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
					T1."Project"=(A."JOBNO")
					AND T0."CANCELED" <>'C' 
					AND T0."CANCELED" <> 'Y' 
					AND T0."DocStatus" = 'C'),0) AS "Credit/Debit_Note_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
				  FROM EW_PRD_TEST."OPOR" T0  
				  INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
				  WHERE 
					T1."Project"=(A."JOBNO")
					AND T0."CANCELED" <>'C' 
					AND T0."CANCELED" <> 'Y' 
					AND T0."DocStatus" <> 'C' 
					AND T1."LineStatus" <> 'C'
					AND T0."U_PRAD_TYPE" = 'COS' 
					AND T1."ItemCode"!='03-ADV-1-001'
					AND T1."ItemCode" NOT IN(SELECT 
												"ItemCode" 
												FROM EW_PRD_TEST."PCH1" AS A
												WHERE 
													A."BaseEntry"=T1."DocEntry"
													AND A."BaseType"='22'
													AND A."TargetType"='19')
				 ),0) AS "Total_COGS_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" AS T0 
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T1."LineStatus" <> 'C' 
						AND T0."U_PRAD_TYPE" = 'COS' 
						AND T1."ItemCode"!='03-ADV-1-001' 
						AND T1."ItemCode" NOT LIKE '21224104' ),0) AS "Total_COGS_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPOR" T0
					INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" = '03-ADV-1-001'
						AND T1."ItemCode" NOT IN(SELECT 
													"ItemCode" 
												  FROM EW_PRD_TEST."PCH1" AS A
												  WHERE 
												  	A."BaseEntry"=T1."DocEntry"
												  	AND A."BaseType"='22'
												  	AND A."TargetType"='19')
				 ),0) AS "Total_Duty_Tax_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" T0  
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND  T1."ItemCode" = '03-ADV-1-001'),0) AS "Total_Duty_Tax_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPOR" T0  
					INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <> 'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T0."U_PRAD_TYPE" = 'ADV' 
						AND T1."ItemCode"!='03-ADV-1-001'),0) AS "Total_Advance_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" T0  
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE  
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."U_PRAD_TYPE" = 'ADV' 
						AND T1."ItemCode" <> '03-ADV-1-001'),0) AS "Total_Advance_in_A/P_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPOR" T0  
					INNER JOIN EW_PRD_TEST."POR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <> 'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND IFNULL(TO_VARCHAR(T0."U_Commission"),'') <> ''),0) AS "Total_Sale_Coms_in_Purchase_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OPCH" T0
					INNER JOIN EW_PRD_TEST."PCH1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE  
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND IFNULL(TO_VARCHAR(T0."U_Commission"),'') <> ''),0) AS "Total_Sale_Coms_in_A/P_Invoice"
				/* End Costing Price */
				
				/* Selling Price */
				,IFNULL((SELECT 
						SUM(T1."LineTotal")  
					FROM EW_PRD_TEST."ORIN" T0  
					INNER JOIN EW_PRD_TEST."RIN1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" = 'O'),0) AS "Credit/Debit_Note_in_A/R_Invoice"
				,IFNULL((SELECT 
						SUM(T1."LineTotal") 
					FROM EW_PRD_TEST."ORDR" T0  
					INNER JOIN EW_PRD_TEST."RDR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y'
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" NOT Like '%%03-ADV%%'
						AND T1."ItemCode" <> '03-ADV-1-001'),0) AS "Total_COGS_in_Sale_Order"
				,IFNULL((SELECT  
					(SELECT 
						IFNULL(SUM(T1."LineTotal"),0)
					 FROM EW_PRD_TEST."OINV" T0  
					 INNER JOIN EW_PRD_TEST."INV1" T1 ON T0."DocEntry" = T1."DocEntry" 
					 WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						/*AND T0."DocStatus" = 'C'*/ 
						AND T1."ItemCode"  NOT Like '%%03-ADV%%'
						AND T1."ItemCode" <> '03-ADV-1-001') 
					 - 
				 	(SELECT 
						IFNULL(SUM(T1."LineTotal"),0) 
					FROM EW_PRD_TEST."ORIN" T0  
					INNER JOIN EW_PRD_TEST."RIN1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'C' 
						AND T0."CANCELED" <> 'Y' 
						AND T0."DocStatus" = 'O') 
				 FROM DUMMY ),0) AS "Total_COGS_in_A/R_Invoice"
				,IFNULL((SELECT 
					SUM(T1."OpenSum") 
				  FROM EW_PRD_TEST."ORDR" T0  
				  INNER JOIN EW_PRD_TEST."RDR1" T1 ON T0."DocEntry" = T1."DocEntry" 
				  WHERE 
					T1."Project"=(A."JOBNO")
					AND T0."CANCELED" <>'Y' 
					AND T0."DocStatus" <> 'C' 
					AND T1."LineStatus" <> 'C' 
					AND T1."ItemCode" = '03-ADV-1-001'),0) AS "Total_Duty_Tax_in_Sale_Order"
				,IFNULL((SELECT 
					SUM(T1."OpenSum") 
				  FROM EW_PRD_TEST."OINV" T0  
				  INNER JOIN EW_PRD_TEST."INV1" T1 ON T0."DocEntry" = T1."DocEntry" 
				  WHERE 
					T1."Project"=(A."JOBNO")
					AND T0."CANCELED" <>'Y' 
					AND T0."DocStatus" <> 'C' 
					AND T1."LineStatus" <> 'C' 
					AND T1."ItemCode" = '03-ADV-1-001'),0) AS "Total_Duty_Tax_in_A/R_Invoice"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."ORDR" T0  
					INNER JOIN EW_PRD_TEST."RDR1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" Like '%%03-ADV%%' 
						--AND T1."ItemCode" NOT Like '%%03-ADV-1-001%%'
						AND T1."ItemCode" <> '03-ADV-1-001'),0) AS "Total_Advance_in_Sale_Order"
				,IFNULL((SELECT 
						SUM(T1."OpenSum") 
					FROM EW_PRD_TEST."OINV" T0  
					INNER JOIN EW_PRD_TEST."INV1" T1 ON T0."DocEntry" = T1."DocEntry" 
					WHERE 
						T1."Project"=(A."JOBNO")
						AND T0."CANCELED" <>'Y' 
						AND T0."DocStatus" <> 'C' 
						AND T1."LineStatus" <> 'C' 
						AND T1."ItemCode" Like '%%03-ADV%%' 
						--AND T1."ItemCode" NOT Like '%%03-ADV-1-001%%'
						AND T1."ItemCode"!='03-ADV-1-001'),0) AS "Total_Advance_in_A/R_Invoice"
				/* End Selling Price */
			
			FROM (
				SELECT 
				/* Job Number Information */
				 A."DocEntry" AS "ID"
				,B."SlpName" AS "SALESNAME"
				,'' AS "EWTINVNO"
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
				,IFNULL(F."CardName",'') AS "CO"
				,(SELECT STRING_AGG(T1."CardName",',')
					FROM EW_PRD_TEST."@TBSHIPPER_SEA_AIR" AS T0 
					LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_SHIPPER"
					WHERE T0."DocEntry"=A."DocEntry") AS "SHIPPER"
				,(SELECT STRING_AGG(T1."CardName",',')
					FROM EW_PRD_TEST."@TBCONSIGNEE" AS T0 
					LEFT JOIN EW_PRD_TEST."OCRD" AS T1 ON T1."CardCode"=T0."U_CONSIGNEE"
					WHERE T0."DocEntry"=A."DocEntry") AS "CONSIGNEE"
				,(SELECT STRING_AGG(T0."U_INVOICE"||',') 
					FROM EW_PRD_TEST."@COMMODITY" AS T0 WHERE T0."DocEntry"=A."DocEntry") AS "COMMODITY"
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
				/* End Job Number Information */
				/* Costing */
				,E."U_TOTALCOSTING" AS "Total_Sale_Cost_Not_Actual_Cost"
				,IFNULL(E."U_REBATE",0) AS "Rebate"
				/* End Costing*/
				/* Other */
				,IFNULL(E."U_USERCREATE",'') AS "CS"
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
				/* End Other */
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
			--AND A."U_SALEEMPLOYEE"=CASE WHEN '-99'='-99' THEN A."U_SALEEMPLOYEE" ELSE '-99' END
			AND A."Status"='O'
			AND IFNULL(E."Status",'O')='O'
			--AND 1=CASE WHEN '-99' IN ('2','-99') THEN 1 ELSE 2 END
			--AND 
			--A."DocEntry"='1079'
			ORDER BY A."DocEntry" ASC
			) AS A
	)AS B; --WHERE B."JOBNO"='EWAI24040094';
END;
