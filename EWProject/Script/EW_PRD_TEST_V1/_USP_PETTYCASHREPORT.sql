ALTER PROCEDURE "EW_PRD_TEST"._USP_PETTYCASHREPORT(
	in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
)
AS
BEGIN
	DECLARE STORETEMPDATAGET TABLE(
		--"TransID" int,
		"NO" int,
		--"LineID" VARCHAR(5000),
		"AcctCode" VARCHAR(5000),
		"AcctName" VARCHAR(5000),
		"DateLine" VARCHAR(5000),
		"CardName" VARCHAR(5000),
		"Remarks" VARCHAR(5000),
		"Debit" float,
		"TOTALSUM" float,
		"TotalAfterCalculate" float
	);
	DECLARE STORERESULTGETFROMTEMPDATA TABLE(
		--"TransID" int,
		"NO" int,
		--"LineID" VARCHAR(5000),
		"AcctCode" VARCHAR(5000),
		"AcctName" VARCHAR(5000),
		"DateLine" VARCHAR(5000),
		"CardName" VARCHAR(5000),
		"Remarks" VARCHAR(5000),
		"Debit" float,
		"TOTALSUM" float,
		"TotalAfterCalculate" float
	);
	Declare i int=1;
	Declare totalCalculate float=0;
	STORETEMPDATAGET=SELECT 
		 --A."TransId" AS "TransID",
		 ROW_NUMBER() OVER(ORDER BY A."ShortName") AS "NO"
		--,A."Line_ID" AS "LineID"
		,A."ShortName" AS "AcctCode"
		,IFNULL(C."CardName",D."AcctName") AS "AcctName"
		,IFNULL(TO_VARCHAR(A."U_TaxDate",'DD/MM/YYYY'),'') AS "DateLine"
		,IFNULL(E."CardName",'') AS "CardName"
		,IFNULL(A."LineMemo",'') AS "Remarks"
		,A."Debit" AS "Debit"
		,(SELECT SUM(T0."Debit") FROM EW_PRD_TEST."JDT1" T0 
			LEFT JOIN EW_PRD_TEST."OJDT" AS T1 ON T1."TransId"=T0."TransId"
			LEFT JOIN EW_PRD_TEST."OCRD" AS T2 ON T2."CardCode"=T0."ShortName"
			LEFT JOIN EW_PRD_TEST."OACT" AS T3 ON T3."AcctCode"=T0."ShortName"
			LEFT JOIN EW_PRD_TEST."OCRD" AS T4 ON T4."CardCode"=T0."U_CardCode"
			WHERE T0."TransCode"='R002' AND T0."Credit"=0) AS "TOTALSUM"
		,0 AS "TotalAfterCalculate"
		--,CAST(A."Credit" AS Double) AS "Credit"
	FROM EW_PRD_TEST."JDT1" AS A
	LEFT JOIN EW_PRD_TEST."OJDT" AS AA ON AA."TransId"=A."TransId"
	LEFT JOIN EW_PRD_TEST."OCRD" AS C ON C."CardCode"=A."ShortName"
	LEFT JOIN EW_PRD_TEST."OACT" AS D ON D."AcctCode"=A."ShortName"
	LEFT JOIN EW_PRD_TEST."OCRD" AS E ON E."CardCode"=A."U_CardCode"
	WHERE AA."TransCode"='R002' AND A."Credit"=0 AND AA."TaxDate" BETWEEN :par1 AND :par2
	GROUP BY A."ShortName",C."CardName"
			,D."AcctName",A."U_TaxDate"
			,E."CardName",A."LineMemo",A."Debit";	
	WHILE i<=(SELECT MAX("NO") FROM :STORETEMPDATAGET) DO
		IF :i=1 THEN
			totalCalculate:=(SELECT "TOTALSUM" FROM :STORETEMPDATAGET WHERE "NO"=:i)-(SELECT "Debit" FROM :STORETEMPDATAGET WHERE "NO"=:i);
		ELSE
			totalCalculate:=(SELECT "TotalAfterCalculate" FROM :STORERESULTGETFROMTEMPDATA WHERE "NO"=:i-1)-(SELECT "Debit" FROM :STORETEMPDATAGET WHERE "NO"=:i);
		END IF;
	INSERT INTO :STORERESULTGETFROMTEMPDATA 
	VALUES(
		--(SELECT "TransID" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "NO" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		--(SELECT "LineID" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "AcctCode" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "AcctName" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "DateLine" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "CardName" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "Remarks" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "Debit" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		(SELECT "TOTALSUM" FROM :STORETEMPDATAGET WHERE "NO"=:i),
		:totalCalculate
	);
	i:=:i+1;
	END WHILE;
	SELECT * FROM :STORERESULTGETFROMTEMPDATA;	
END;




