ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_REPORT_ADVANCE_IMPORT_CONTAINER(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250)
)
AS
BEGIN
	SELECT DISTINCT
		 C."DocDate" AS "Payment"
		,A."NAME" AS "JobNo"
		,(SELECT STRING_AGG(T0."CardCode") FROM 
			 (SELECT DISTINCT
				"CardCode"
			  FROM EW_PRD_TEST."PMG4" AS T0
			  LEFT JOIN EW_PRD_TEST."ORDR" AS T1 ON T1."DocEntry"=T0."DocEntry"
			  WHERE T0."AbsEntry"=A."AbsEntry" AND T0."TYP"='17' AND T1."CANCELED"!='Y')AS T0
		 ) AS "GEOFFREY"
		,D."U_Remark" AS "BLNumber"
		,D."LineTotal" AS "Amount"
		,C."CardName" AS "VESSEL"
		,CASE WHEN IFNULL(J."RefDocEntr",0)=0 THEN 'Did not received' ELSE 'Received' END AS "STATUS"
		,M."firstName"||' - '||M."lastName" AS "CS"
		,ADD_DAYS(C."DocDate", N."ExtraDays") AS "1st-Email-Dated"
		,'' AS "2nd-Email-Dated"
		,C."Comments" AS "Remarks"
		,K."DocDate" AS "DateOfReceived"
		,K."DocTotal" AS "AmountOfReceived"
		,K."DocNum" AS "ReceivedOfReference"
	FROM EW_PRD_TEST."OPMG" AS A
	LEFT JOIN EW_PRD_TEST."PMG4" AS B ON B."AbsEntry"=A."AbsEntry" AND B."TYP"='22' --Get Purhcase Order
	LEFT JOIN EW_PRD_TEST."OPOR" AS C ON C."DocEntry"=B."DocEntry"
	LEFT JOIN EW_PRD_TEST."POR1" AS D ON D."DocEntry"=C."DocEntry" AND D."Project"=A."NAME"
	LEFT JOIN EW_PRD_TEST."OITM" AS E ON E."ItemCode"=D."ItemCode"
	--LEFT JOIN EW_PRD_TEST."PMG4" AS F ON F."AbsEntry"=A."AbsEntry" AND F."TYP"='17' -- Get Sale Order for Get CO
	--LEFT JOIN EW_PRD_TEST."ORDR" AS G ON G."DocEntry"=F."DocEntry"
	LEFT JOIN EW_PRD_TEST."OCRD" AS H ON H."CardCode"=C."CardCode"
	LEFT JOIN EW_PRD_TEST."PCH1" AS I ON I."BaseEntry"=D."DocEntry" AND I."BaseLine"=D."LineNum" AND I."BaseType"='22'
	LEFT JOIN EW_PRD_TEST."RCT9" AS J ON J."RefDocEntr"=I."DocEntry" AND J."RefObjType"='18'
	LEFT JOIN EW_PRD_TEST."ORCT" AS K ON K."DocEntry"=J."DocEntry"
	LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS L ON L."U_NumAtCard"=C."U_WEBID"
	LEFT JOIN EW_PRD_TEST."OHEM" AS M ON M."empID"=L."U_UserID"
	LEFT JOIN EW_PRD_TEST."OCTG" AS N ON N."GroupNum"=H."GroupNum"
	WHERE 
		E."ItmsGrpCod" =127 
		AND A."STATUS"!='N'
		AND C."DocDate" BETWEEN :par1 AND :par2;
END;


	
