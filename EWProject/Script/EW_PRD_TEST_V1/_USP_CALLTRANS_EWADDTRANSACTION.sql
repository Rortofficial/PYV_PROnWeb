ALTER PROCEDURE "EW_PRD_TEST"._USP_CALLTRANS_EWADDTRANSACTION(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	
	IF :DTYPE = 'AddAlertMessage' THEN 
		IF :par1<>'' THEN
			execute immediate :par1;
		END IF; 
	ELSE IF :DTYPE = 'UPDATENUMATCARDSQ' THEN
		execute immediate 'UPDATE 
								EW_PRD_TEST."OQUT" SET "NumAtCard"=(SELECT SUBSTRING_REGEXPR(''[^/]+'' IN '''||:par1||''' FROM 1 OCCURRENCE 1)||''/''||'''||:par2||''' from dummy) 
							WHERE "DocEntry"='''||:par2||''';';
	ELSE IF :DTYPE='AddReimbursement' THEN
		INSERT INTO EW_PRD_TEST."@TBREIMBURSEMENT"("Code","U_CardCode","U_Amount","U_Remark","U_DocStatus","U_CreateDate") 
			VALUES((EW_PRD_TEST."@TBREIMBURSEMENT_S".nextval),:par1,:par2,:par3,'P',CURRENT_DATE);
		SELECT CAST(MAX("Code") AS VARCHAR(30)) AS "DocEntry" FROM EW_PRD_TEST."@TBREIMBURSEMENT";
	ELSE IF :DTYPE='UpdateConfirmBookingStatus' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"='Y' WHERE "DocEntry"=:par1 AND "LineId"=:par2;
		--SELECT CAST(:par1 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;
		--SELECT * FROM EW_PRD_TEST."@TRUCKINFORMATION"
	ELSE IF :DTYPE='UpdateReimbursement' THEN
		UPDATE EW_PRD_TEST."@TBREIMBURSEMENT" SET "U_CardCode"=:par1,"U_Amount"=:par2,"U_Remark"=:par3 WHERE "Code"=:par4;
		SELECT CAST(:par4 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;		
	ELSE IF :DTYPE='DeleteReimbursement' THEN
		DELETE FROM EW_PRD_TEST."@TBREIMBURSEMENT" WHERE "Code"=:par1;
		SELECT CAST(:par4 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;
	ELSE IF :DTYPE='UpdateStatusReimbursement' THEN
		UPDATE EW_PRD_TEST."@TBREIMBURSEMENT" SET "U_DocStatus"=:par1,"U_Reason"=:par2,"U_UserApprove"=:par3 WHERE "Code"=:par4;
		SELECT CAST(:par3 AS VARCHAR(30)) AS "DocEntry" FROM Dummy;	
	ELSE IF :DTYPE = 'UPDATENUMATCARDSO' THEN
		UPDATE EW_PRD_TEST."ORDR" SET "NumAtCard"='' WHERE "DocEntry"=:par2;
		UPDATE EW_PRD_TEST."@JOBSHEETRUCKING" SET "U_SALESORDERDOCNUM"=NULL,"U_CONFIRMBOOKINGID"=NULL WHERE "DocEntry"=:par1;
	ELSE IF :DTYPE = 'UpdateLineStatusInactivePRCOS' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"=''
		WHERE "DocEntry"= (SELECT DISTINCT
							 (SELECT
								T0."DocEntry"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1 AND A."U_ItemCode"='01-T-1-0001')
			  AND "LineId"=:par2;
	ELSE IF :DTYPE = 'UpdateLineStatusActivePRCOS' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"='Y'
		WHERE "DocEntry"= (SELECT DISTINCT
							 (SELECT
								T0."DocEntry"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1)
			  AND "LineId"=:par2;
	ELSE IF :DTYPE = 'CancelLineStatusInActivePRCOS' THEN
		UPDATE EW_PRD_TEST."@TRUCKINFORMATION" SET "U_LineStatus"=''
		WHERE "DocEntry"= (SELECT DISTINCT
							 (SELECT
								T0."DocEntry"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1)
			  AND "LineId"=(SELECT DISTINCT
							 (SELECT
								T0."LineId"
							  FROM EW_PRD_TEST."@TRUCKINFORMATION" AS T0
							  LEFT JOIN EW_PRD_TEST."@CONFIRMBOOKINGSHEET" AS T1 ON T1."DocEntry"=T0."DocEntry"
							  WHERE T1."U_PROJECTMANAGEMENTID"=CASE WHEN IFNULL(C."U_Project",0)=0 THEN IFNULL(A."U_JobNumber",0)
							 								ELSE IFNULL(C."U_Project",0) END
							 		AND "U_TRUCKCODE"=A."U_TruckNo"
							 		AND T1."Canceled"='N') AS "BaseLineID"
						FROM EW_PRD_TEST."@ADVANCEPAYMENTROW" AS A
						LEFT JOIN EW_PRD_TEST."@ADVANCEPAYMENT" AS C ON C."DocEntry"=A."DocEntry"
						WHERE A."DocEntry"=:par1 AND A."U_ItemCode"='01-T-1-0001');
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
END
