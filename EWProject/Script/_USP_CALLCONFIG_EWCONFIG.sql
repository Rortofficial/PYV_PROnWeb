
ALTER PROCEDURE "EW_PRD"._USP_CALLCONFIG_EWCONFIG(
	in DTYPE NVARCHAR(250)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	
	IF :DTYPE = 'LAYOUTPRINTER' THEN 
		SELECT 
			  A."U_FILENAME" AS FILENAME
			 ,A."U_EXPORTTYPE" AS EXPORTTYPE
			 ,A."U_STOREPROCEDURE" AS STOREPROCEDURE
			 ,A."U_PROPERTIES" AS PROPERTIES
		FROM EW_PRD."@TBREPORT" AS A WHERE A."Code"=:par1;
	END IF;
END
