SELECT * FROM EW_PRD_TEST_31."OQUT" WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-11-01'

SELECT * FROM EW_PRD_TEST_E."@BOOKINGSHEET" WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-10-31'

SELECT * FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' AND "Canceled"='N';
SELECT * FROM EW_PRD_TEST_31."@CONFIRMBOOKINGSHEET"; --WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' AND "Canceled"='N';
SELECT * FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' AND "Canceled"='N';

SELECT * FROM EW_PRD_TEST_31."ORDR" WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-11-01'

SELECT * FROM EW_PRD_TEST_E."OPOR" WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-10-31'


SELECT * FROM EW_PRD_TEST_31."ORDR" WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-10-31' AND "Status"='C'

/*SELECT "DocNum" 
	FROM EW_PRD_TEST_E."OQUT" 
WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-10-31'
	AND "U_WEBID" NOT IN (SELECT "U_WEBID" FROM EW_PRD_TEST."OQUT" WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-10-31' AND "U_WEBID" IS NOT NULL)*/
/*SELECT * FROM EW_PRD_TEST_31."@BOOKINGSHEET" WHERE "U_JOBNO" IN
(SELECT A."U_JOBNO" AS "JobNumber"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
			AND "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST_31."@BOOKINGSHEET" AS T0 
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
										AND "Canceled"='N'));*/
										
/*						
SELECT T1."NAME" FROM EW_PRD_TEST_31."@CONFIRMBOOKINGSHEET" AS T0
LEFT JOIN EW_PRD_TEST_31."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
WHERE T1."NAME" IN
(								
SELECT 
	T1."NAME"
FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
	AND "Canceled"='N'
	AND T1."NAME" NOT IN (SELECT 
								T1."NAME" 
							FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
							LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
							WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
							))AND "Canceled"='N';

SELECT T1."NAME" FROM EW_PRD_TEST_31."@CONFIRMBOOKINGSHEET" AS T0
LEFT JOIN EW_PRD_TEST_31."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
 WHERE T1."NAME" IN
(
	SELECT 
			 T1."NAME"
		FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
		LEFT JOIN EW_PRD_TEST_E."@BOOKINGSHEET" AS T2 ON T2."DocEntry"=T0."U_BOOKINGID"
		LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
		LEFT JOIN EW_PRD_TEST_31."@BOOKINGSHEET" AS T3 ON T3."U_JOBNO"=RIGHT(T1."NAME",LENGTH(T1."NAME" )-4)
		WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
			AND T0."Canceled"='N'
			AND T1."NAME" NOT IN (SELECT 
										T1."NAME" 
									FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
									LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
									WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
									) AND T0."U_BOOKINGID"!='804');
*/

/*
SELECT 
	T1."NAME" 
FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS T0 
LEFT JOIN EW_PRD_TEST_E."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
	AND "Canceled"='N'
	AND T1."NAME" NOT IN (SELECT 
								T1."NAME" 
							FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
							LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
							WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
								AND "Canceled"='N')*/
	/*							
SELECT T0."NumAtCard",T0."CANCELED"
FROM EW_PRD_TEST_E."ORDR" AS T0
WHERE "DocDate" BETWEEN '2023-10-31' AND '2023-10-31'
	AND T0."NumAtCard" NOT IN (SELECT 
								T1."NAME" 
							FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
							LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
							WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
								AND "Canceled"='N')
	AND T0."DocEntry" IN (SELECT "U_SALESORDERDOCNUM" FROM EW_PRD_TEST_E."@JOBSHEETRUCKING" WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-10-31')
	AND T0."NumAtCard" NOT IN (SELECT 
								T1."NAME"
							FROM EW_PRD_TEST."OPMG" AS T1
							WHERE T1."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31') AND T0."CANCELED"='N'*/
	
	/*
SELECT 
	"U_JOBNO"
FROM EW_PRD_TEST_E."@BOOKINGSHEET" 
WHERE "CreateDate" BETWEEN '2023-10-31' AND '2023-11-01'	
	AND "U_JOBNO" NOT IN (SELECT 
								RIGHT(T1."NAME",LENGTH(T1."NAME")-4)
							FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T0 
							LEFT JOIN EW_PRD_TEST."OPMG" AS T1 ON T1."AbsEntry"=T0."U_PROJECTMANAGEMENTID"
							WHERE T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' 
								AND "Canceled"='N')
	
								SELECT B."U_JOBNO" FROM EW_PRD_TEST_E."@CONFIRMBOOKINGSHEET" AS A
								LEFT JOIN EW_PRD_TEST_E."@BOOKINGSHEET" AS B ON B."DocEntry"=A."U_BOOKINGID"
								WHERE --A."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' AND A."Canceled"='N' AND
								 B."U_JOBNO" NOT IN (SELECT T1."U_JOBNO" FROM EW_PRD_TEST."@CONFIRMBOOKINGSHEET" T0
						LEFT JOIN EW_PRD_TEST."@BOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."U_BOOKINGID")
								;
						SELECT A."U_JOBNO" FROM EW_PRD_TEST_E."@JOBSHEETRUCKING" AS A
								--LEFT JOIN EW_PRD_TEST_E."@BOOKINGSHEET" AS B ON B."DocEntry"=A."U_BOOKINGID"
								WHERE --A."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' AND--AND A."Canceled"='N'
								 A."U_JOBNO" NOT IN (SELECT T0."U_JOBNO" FROM EW_PRD_TEST."@JOBSHEETRUCKING" T0)
								;
*/
								
								
							SELECT * FROM EW_PRD_TEST."@BOOKINGSHEET" WHERE "U_JOBNO" IN
(SELECT A."U_JOBNO" AS "JobNumber"
		FROM EW_PRD_TEST_E."@BOOKINGSHEET" AS A
		WHERE --"CreateDate" BETWEEN '2023-10-31' AND '2023-11-01' AND
			 "U_JOBNO" NOT IN (SELECT 
										T0."U_JOBNO"
									FROM EW_PRD_TEST."@BOOKINGSHEET" AS T0 
									WHERE --T0."CreateDate" BETWEEN '2023-10-31' AND '2023-10-31' AND
										 "Canceled"='N')) --AND "CreateDate" BETWEEN '2023-11-05' AND '2023-11-05' ;
								
								

	
