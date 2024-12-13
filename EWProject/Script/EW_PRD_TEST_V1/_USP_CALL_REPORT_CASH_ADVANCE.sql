ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_REPORT_CASH_ADVANCE(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250)
)
AS
BEGIN
	/*Declare maxCount int;
	Declare i int=1;
	Declare totalDiff double=0;
	create column table table_clear_advance as
	(
--ROW_NUMBER() OVER (ORDER BY column_name) AS row_num
	    SELECT 
	    	 ROW_NUMBER() OVER (ORDER BY "SettlementNo") AS "row_num"
	    	,* 
	    FROM (
	    	SELECT 
				 T0."BaseDocNum" AS "SettlementNo"
				,T0."BsDocDate" AS "ClearedDate"
				,T0."DrawnSum" AS "Amount"
				,T1."Max1099"-T1."WTSum" AS "Diff"
				,0 AS "OutStanding"
				,CASE WHEN T0."ObjType"='204' THEN 'Down Payment' ELSE 'Unknow' END AS "Type"
				,T1."DocEntry" AS "InvNum"
				,true "isUpdate"
			FROM EW_PRD_TEST."PCH9" AS T0 
			LEFT JOIN EW_PRD_TEST."OPCH" AS T1 ON T1."DocEntry"=T0."DocEntry"
			WHERE --T0."DocEntry"='5231'
				T0."BaseDocNum" IS NOT NULL
			UNION ALL
			SELECT
				 T0."DocNum" AS "SettlementNo"
				,T0."DocDate" AS "ClearedDate"
				,T1."BfDcntSum" AS "Amount"
				,T2."Max1099"-T2."WTSum" AS "Diff"
				,0 AS "OutStanding"
				,'Incoming Payment' AS "Type" 
				,T2."DocEntry" AS "InvNum"
				,true "isUpdate"
			FROM EW_PRD_TEST."ORCT" T0
			INNER JOIN EW_PRD_TEST."RCT2" T1 ON T0."DocEntry" = T1."DocNum"
			INNER JOIN EW_PRD_TEST."OPCH" T2 ON T1."DocEntry" = T2."DocEntry" and T1."InvType" = '18'
			WHERE --T2."DocEntry"='5231' AND 
			T0."Canceled"!='Y'
			UNION ALL
			SELECT
				 T0."DocNum" AS "SettlementNo"
				,T0."DocDate" AS "ClearedDate"
				,T1."BfDcntSum" AS "Amount"
				,T2."Max1099"-T2."WTSum" AS "Diff"
				,0 AS "OutStanding"
				,'Outgoing Payment' AS "Type" 
				,T2."DocEntry" AS "InvNum"
				,true "isUpdate"
			FROM EW_PRD_TEST."OVPM" T0
			INNER JOIN EW_PRD_TEST."VPM2" T1 ON T0."DocEntry" = T1."DocNum"
			INNER JOIN EW_PRD_TEST."OPCH" T2 ON T1."DocEntry" = T2."DocEntry" and T1."InvType" = '18'
			WHERE --T2."DocEntry"='5231' AND 
			T0."Canceled"!='Y'
	    )
	);
	
	SELECT COUNT(*) INTO maxCount FROM table_clear_advance;
	
	WHILE  i<=maxCount DO
		SELECT "Diff"-"Amount" INTO totalDiff FROM table_clear_advance WHERE "row_num"=i;
		UPDATE table_clear_advance 
			SET  "Diff"=:totalDiff
				,"OutStanding"=:totalDiff 
				,"isUpdate"=false
		WHERE "row_num"=:i;
		UPDATE table_clear_advance 
			SET  "Diff"=:totalDiff
				,"OutStanding"=:totalDiff 
		WHERE "InvNum"=(SELECT "InvNum" FROM table_clear_advance WHERE "row_num"=:i) 
			AND "row_num" !=:i
			AND "isUpdate"=true;
		i := i + 1;
	END WHILE;
	
	SELECT 
		 B."JobNumber" AS "JobNumber"
		,C."CO" AS "CO"
		,B."Remark"
		--,C."CSName" AS "CSName"
		,B."VendorName" AS "VendorName"
		,B."DocNum" AS "APNo"
		,B."PONO" AS "PONo"
		,B."DocDate" AS "CashAdvanceDate"
		,B."Amount" AS "CashAmount"
		,D."SettlementNo" AS "ClearDocNum"
		,D."ClearedDate" AS "ClearDate"
		,D."Amount" AS "ClearAmount"
		,D."Diff" AS "Diff"
		,D."OutStanding" AS "OutStanding"
		--,D."isUpdate"
		--,D."InvNum"
		--,B."DocEntry"
		,D."Type"
		,B."APRemark" AS "Remark"
	FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS A
	LEFT JOIN (
		SELECT DISTINCT
			 T0."AbsEntry" AS "AbsEntry"
			,T0."NAME" AS "JobNumber"
			,T1."DOCNUM" AS "DocNum"
			,T1."DocEntry" AS "DocEntry"
			,T2."Comments" AS "Remark"
			,T2."DocDate" AS "DocDate"
			,T2."DocTotal" AS "Amount"
			,(SELECT STRING_AGG(ZZ."DocNum",',') FROM (
				SELECT 
					DISTINCT "DocNum" AS "DocNum" 
				FROM EW_PRD_TEST."POR1" AS Z0
				LEFT JOIN EW_PRD_TEST."OPOR" AS Z1 ON Z0."DocEntry"=Z1."DocEntry" 
				WHERE "TrgetEntry"=T2."DocEntry" 
				AND "TargetType"='18') AS ZZ
			 ) AS "PONO"
			,T2."Comments" AS "APRemark"
			,T2."CardName" AS "VendorName"
		FROM EW_PRD_TEST."OPMG" AS T0
		LEFT JOIN EW_PRD_TEST."PMG4" AS T1 ON T1."AbsEntry"=T0."AbsEntry" AND T1."TYP"='18'
		LEFT JOIN EW_PRD_TEST."OPCH" AS T2 ON T2."DocEntry"=T1."DocEntry"
		WHERE T2."CANCELED"='N'
				
	)AS B ON B."AbsEntry"=A."U_PROJECTMANAGEMENTID"
	LEFT JOIN (
		SELECT 
			 T0."DocEntry" AS "DocEntry"
			,STRING_AGG(T2."CardName") AS "CO"
			,T0."U_UserCreate" AS "CSName"
		FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0
		LEFT JOIN EW_PRD_TEST."@TBSALESQUOTATION" AS T1 ON T1."DocEntry"=T0."DocEntry"
		LEFT JOIN EW_PRD_TEST."OQUT" AS T2 ON T2."DocEntry"=T1."U_DOCENTRY"
		GROUP BY T0."DocEntry",T0."U_UserCreate"
	) AS C ON C."DocEntry"=A."U_BOOKINGID"
	LEFT JOIN (
		SELECT * FROM table_clear_advance
	)AS D ON D."InvNum"=B."DocEntry"
	WHERE 
		A."Status"='O' 
	AND D."SettlementNo" IS NOT NULL 
	AND B."DocDate" BETWEEN :par1 AND :par2
	ORDER BY B."DocNum";
	DROP TABLE table_clear_advance;*/
	SELECT DISTINCT
			 (SELECT STRING_AGG("Project",' ,') FROM (
			 	(SELECT DISTINCT "Project" 
			 		FROM EW_PRD_TEST."POR1" 
			 		WHERE "DocEntry"=A."DocEntry")
			 		)AS A) AS "JobNumber"
			,A."CardName" AS "VendorName"
			,A."Comments" AS "PORemarks"
			,(SELECT STRING_AGG(T1."firstName" || ' - ' || T1."lastName",',') 
				FROM EW_PRD_TEST."@ADVANCEPAYMENT" AS T0 
				LEFT JOIN EW_PRD_TEST."OHEM" AS T1 ON T1."empID"=T0."U_UserID"
				WHERE "U_NumAtCard"=A."U_WEBID") AS "CSNAME"
			,A."DocNum" AS "PONumber"
			,C."DocNum" AS "DownPaymentNumber"
			--,C."DocNum" AS "APDownPayment"
			,A."DocDate" AS "PaymentDate"
			--,C."DocTotal" AS "PaymentAmount"
			,B."LineTotal" AS "PaymentAmount"
			,IFNULL(TO_VARCHAR(E."DocNum"),'-') AS "ClearNumber"
			,IFNULL(TO_VARCHAR(E."DocDate",'yyyy-MM-dd'),'-') AS "ClearDate"
			,IFNULL(E."Max1099",0) AS "ClearAmount"
			--,(E."Max1099")-(E."DpmAmnt"+E."PaidSum") AS "Diff"---(F."SumApplied")
			,IFNULL(E."Max1099",0)-B."LineTotal" AS "Diff" 
			,CASE WHEN E."InvntSttus" IS NULL THEN 
				IFNULL(E."Max1099",0)-B."LineTotal"
			 ELSE 
				IFNULL((E."Max1099"),0)-(IFNULL(E."DpmAmnt",0)+IFNULL(E."PaidSum",0)) 
			 END AS "OutStanding"
			,(SELECT STRING_AGG("DocNum",' ,') FROM
			
			 (SELECT 
					T2."BeginStr"||T1."DocNum" AS "DocNum"
			  	FROM EW_PRD_TEST."RCT2" T0
			  	LEFT JOIN EW_PRD_TEST."ORCT" AS T1 ON T1."DocEntry"=T0."DocNum"
			  	LEFT JOIN EW_PRD_TEST."NNM1" AS T2 ON T2."Series"=T1."Series" AND T2."ObjectCode"=T1."ObjType"
			  	WHERE 
			  		T0."DocEntry"=E."DocEntry" 
			  		AND T0."InvType"='18'
			  UNION ALL
			  SELECT 
					T2."BeginStr"||T1."DocNum" AS "DocNum"
			  	FROM EW_PRD_TEST."VPM2" T0
			  	LEFT JOIN EW_PRD_TEST."OVPM" AS T1 ON T1."DocEntry"=T0."DocNum"
			  	LEFT JOIN EW_PRD_TEST."NNM1" AS T2 ON T2."Series"=T1."Series" AND T2."ObjectCode"=T1."ObjType"
			  	WHERE 
			  		T0."DocEntry"=E."DocEntry" 
			  		AND T0."InvType"='18'
			 )AS A) AS "ClearNumber"
			,E."Comments" AS "APRemark"
		FROM EW_PRD_TEST."OPOR" AS A
		LEFT JOIN EW_PRD_TEST."DPO1" AS B ON B."BaseEntry"=A."DocEntry" AND B."BaseType"='22'
		LEFT JOIN EW_PRD_TEST."ODPO" AS C ON C."DocEntry"=B."DocEntry"
		--LEFT JOIN EW_PRD_TEST."OVPM" AS D ON D.""
		LEFT JOIN EW_PRD_TEST."PCH1" AS D ON D."BaseEntry"=A."DocEntry" AND D."BaseType"='22'
		LEFT JOIN EW_PRD_TEST."OPCH" AS E ON E."DocEntry"=D."DocEntry"
		--LEFT JOIN EW_PRD_TEST."RCT2" AS F ON F."DocEntry"=E."DocEntry" AND F."InvType"='18'
		--LEFT JOIN EW_PRD_TEST."VPM2" AS G ON G."DocEntry"=E."DocEntry" AND F."InvType"='18'
		WHERE 
			B."BaseEntry" IS NOT NULL
			AND IFNULL(E."InvntSttus",'O') = 'O'
			AND B."DocDate" BETWEEN :par1 AND :par2
		;
END;
