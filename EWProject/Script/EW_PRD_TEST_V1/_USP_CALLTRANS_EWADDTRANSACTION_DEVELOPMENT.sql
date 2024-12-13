ALTER PROCEDURE "EW_PRD_TEST_NEW"._USP_CALLTRANS_EWADDTRANSACTION(
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
								EW_PRD_TEST_NEW."OQUT" SET "NumAtCard"=(SELECT SUBSTRING_REGEXPR(''[^/]+'' IN '''||:par1||''' FROM 1 OCCURRENCE 1)||''/''||'''||:par2||''' from dummy) 
							WHERE "DocEntry"='''||:par2||''';';
	ELSE IF :DTYPE='UpdateStatusContainer' THEN
		execute immediate 'UPDATE 
								EW_PRD_TEST_NEW."@TRUCKINFORMATION" SET "U_TransferStatus"=''Y''
							WHERE "U_CONTAINERNO"='''||:par1||''';';
	ELSE IF :DTYPE='UpdateCancelStatusContainer' THEN
		execute immediate 'UPDATE 
								EW_PRD_TEST_NEW."@TRUCKINFORMATION" SET "U_TransferStatus"=''N''
							WHERE "U_CONTAINERNO" IN (SELECT "ItemCode" FROM EW_PRD_TEST_NEW."WTR1" WHERE "DocEntry"='''||:par1||''');';
	END IF;
	END IF;
	END IF;
	END IF;
END
