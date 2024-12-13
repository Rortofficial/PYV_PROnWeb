ALTER PROCEDURE "EW_PRD_TEST"._USP_CALL_REPORT_ADVANCE_DUTY(
	in par1 NVARCHAR(250),
	in par2 NVARCHAR(250)
)
AS
BEGIN
	SELECT Distinct
		 AA1."Project" AS "JobNo"
		,AA."CardName" AS "Consignee"
		,AA."DocNum" AS "DocNum"
		,AA1."LineTotal"
		,AA."DocDate" AS "PODate"
		,D."PR_NO"
		,CASE WHEN IFNULL("PR_NO",0)!=0 THEN A."AmountOfDuty" ELSE NULL END
		,D."PR_DATE"
		,TO_VARCHAR(E."DocNum") AS "INV_NO"
		,TO_VARCHAR(E."DocDate",'dd-MM-YYYY') AS "INV_DATE"
		,F."RV_NO"
		,F."DocDate" AS "RV_DATE"
		,E0."SlpName" AS "SALE"
		,G."U_USERCREATE" AS "CS"
		,A."RemarkSAP"
	FROM EW_PRD_TEST."OPOR" AS AA
	LEFT JOIN EW_PRD_TEST."POR1" AA1 ON AA1."DocEntry"=AA."DocEntry" 
	LEFT JOIN EW_PRD_TEST."OITM" AA2 ON AA2."ItemCode"=AA1."ItemCode"
 	LEFT JOIN
	(
		SELECT DISTINCT
			 T0."CardCode"
			,T0."CardName"
			,T1."Project"
			,T0."DocDate"
			,T1."LineTotal" AS "AmountOfDuty"
			,T1."BaseLine"
			--,T0."Comments" AS "RemarkSAP"
			,T1."U_Remark" AS "RemarkSAP"
			,T1."BaseEntry"
			,T1."BaseType"
		FROM EW_PRD_TEST."OPCH" AS T0
		LEFT JOIN EW_PRD_TEST."PCH1" AS T1 ON T0."DocEntry"=T1."DocEntry"
		LEFT JOIN EW_PRD_TEST."OITM" AS T2 ON T2."ItemCode"=T1."ItemCode"
		WHERE T2."ItmsGrpCod"='121' AND T1."BaseType"='22'
		--GROUP BY T0."CardCode",T0."CardName",T1."Project",T1."LineTotal",T0."DocDate",T1."BaseEntry",T1."BaseType"
	) AS A ON A."BaseEntry"=AA."DocEntry" AND A."BaseType"='22' AND A."BaseLine"=AA1."LineNum"
	LEFT JOIN EW_PRD_TEST."OPMG" AS B ON B."NAME"=AA1."Project"
	--LEFT JOIN EW_PRD_TEST."PMG4" AS C ON C."AbsEntry"=B."AbsEntry"
	LEFT JOIN 
	(
		SELECT 
			 T4."DocNum" AS "PR_NO"
			,T1."DocEntry" AS "PR_ENTRY"
			,TO_VARCHAR(T3."DocDate",'dd-MM-yyyy') AS "PR_DATE"
			,T1."Project" AS "Project"
			,T1."U_RefInv" AS "RefInv"
			,T1."U_LinkToARInvoice" AS "LinkToARInvoice"
		FROM EW_PRD_TEST."VPM2" AS T0
		LEFT JOIN EW_PRD_TEST."PCH1" AS T1 ON T1."DocEntry"=T0."DocEntry"
		LEFT JOIN EW_PRD_TEST."OITM" AS T2 ON T2."ItemCode"=T1."ItemCode"
		LEFT JOIN EW_PRD_TEST."OVPM" AS T3 ON T3."DocEntry"=T0."DocNum"
		LEFT JOIN EW_PRD_TEST."OPCH" AS T4 ON T4."DocEntry"=T0."DocEntry"
		WHERE T0."InvType"='18' AND IFNULL(T1."Project",'')<>'' AND T2."ItmsGrpCod"='121'
	) AS D ON D."Project"=A."Project"
	LEFT JOIN EW_PRD_TEST."OINV" AS E ON TO_VARCHAR(E."U_AP_DUTY_ENTRY")=TO_VARCHAR(D."PR_ENTRY")
	LEFT JOIN EW_PRD_TEST."OSLP" AS E0 ON E."SlpCode"=E0."SlpCode"
	LEFT JOIN 
	(
		SELECT 
			 T0."DocEntry"
			,T1."DocDate" AS "DocDate"
			,T1."DocNum" AS "RV_NO"
		FROM EW_PRD_TEST."RCT2" AS T0
		LEFT JOIN EW_PRD_TEST."ORCT" AS T1 ON T1."DocEntry"=T0."DocNum"
	)AS F ON F."DocEntry"=E."DocEntry"
	LEFT JOIN EW_PRD_TEST."@JOBSHEETRUCKING" AS G ON G."U_JOBNO"=AA1."Project" AND G."Canceled"='N'
	WHERE IFNULL(AA1."Project",'')<>'' 
	AND AA2."ItmsGrpCod"='121'
	AND AA."DocDate" BETWEEN :par1 AND :par2
	ORDER BY AA."DocDate",AA1."Project" ASC;
END;


