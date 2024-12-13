--DROP FUNCTION "replicate"
alter FUNCTION EW_PRD."replicate"(IN checkString nvarchar(5000), IN length1 INT)
RETURNS replicate nvarchar(5000)
LANGUAGE SQLSCRIPT AS
BEGIN

  DECLARE v_index INT;
  DECLARE tmp_string nvarchar(5000) := :checkString;

  FOR v_index IN 1 .. length1 DO
    tmp_string := :tmp_string || :checkString;  
  END FOR;

  replicate := tmp_string;

END;

/*
DO
BEGIN
 IF RIGHT('tyaasdty',2)='ty' THEN
 	SELECT 'asd' from dummy;
 END IF;
END;*/