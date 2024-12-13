
alter Function EW_PRD."fnSpellInteger"(number VARCHAR(100))
RETURNS result VARCHAR(100)
language SQLSCRIPT
SQL SECURITY INVOKER AS
BEGIN
	DECLARE result VARCHAR(100);
	DECLARE word VARCHAR(100);
	DECLARE goup VARCHAR(100);  
    DECLARE	i INT;
    DECLARE j INT;
    DECLARE m INT;
    DECLARE digit VARCHAR(2);
    DECLARE cn VARCHAR(20);
    DECLARE prefix VARCHAR(20);
    IF number = 0 THEN result:='Zero'; END IF;  

     result:='';
     word:='';
     goup:='';
     cn:=:number;  
     cn:=REPLACE(:cn,',','');
     m:=mod(LENGTH(cn),3);  
	  IF :m>0 THEN 
	  	SELECT EW_PRD.replicate('0',3-:m)+:cn INTO cn from dummy;
	  END IF;       -- Left pad with zeroes to a multiple of 3  
     i:=1;  
     j:=LENGTH(:cn)-:i+1;  
     m:=mod(:i,3);  
  WHILE i<=LENGTH(:cn)  
  DO  
      -- @i is 1 origin index into numeric string while @m = @i modulo 3  
      -- If the middle digit of each group of 3 is a '1' then this is a 'Ten' or a '...teen'  
  	IF :m=2 AND SUBSTRING(:cn,:i,1)='1'   
    THEN
       digit:=SUBSTRING(:cn,:i,2);
       -- Skip rightmost digit of 3 if processing teens  
       i:=:i+1;  
    ELSE
       digit:=SUBSTRING(:cn,:i,1);
    END IF; 
   /* word: =   
      CASE   
       WHEN   :m   =   0   THEN               -- Rightmost digit of group of 3  
        CASE   :digit  
         WHEN    '0'    THEN    ''  
         WHEN    '1'    THEN    'One'  
         WHEN    '2'    THEN    'Two'  
         WHEN    '3'    THEN    'Three'  
         WHEN    '4'    THEN    'Four'  
         WHEN    '5'    THEN    'Five'  
         WHEN    '6'    THEN    'Six'  
         WHEN    '7'    THEN    'Seven'  
         WHEN    '8'    THEN    'Eight'  
         WHEN    '9'    THEN    'Nine'  
        END    +   
        CASE   
         WHEN   (:goup    <>    ''    OR   :digit   <>    '0'   )   AND   (:j   +   2)   /   3   =   2   THEN    ' Thousand'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '0'   )   AND   (:j   +   2)   /   3   =   3   THEN    ' Million'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '0'   )   AND   (:j   +   2)   /   3   =   4   THEN    ' Billion'  
         ELSE    ''  
     END  
       WHEN   LENGTH(:digit)   =   2   THEN             -- Special case when middle digit is a '1'  
        CASE   :digit  
         WHEN    '10'    THEN    'Ten'  
         WHEN    '11'    THEN    'Eleven'  
         WHEN    '12'    THEN    'Twelve'  
         WHEN    '13'    THEN    'Thirteen'  
         WHEN    '14'    THEN    'Fourteen'  
         WHEN    '15'    THEN    'Fifteen'  
         WHEN    '16'    THEN    'Sixteen'  
         WHEN    '17'    THEN    'Seventeen'  
         WHEN    '18'    THEN    'Eighteen'  
         WHEN    '19'    THEN    'Nineteen'  
     END   +  
        CASE   
         WHEN   (:goup    <>    ''    OR   :digit   <>    '00'   )   AND   (:j   +   2)   /   3   =   2   THEN    ' Thousand'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '00'   )   AND   (:j   +   2)   /   3   =   3   THEN    ' Million'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '00'   )   AND   (:j   +   2)   /   3   =   4   THEN    ' Billion'  
         ELSE    ''  
     END  
       WHEN   :m   =   2   THEN               -- Middle digit of group of 3  
        CASE   :digit  
         WHEN    '2'    THEN    'Twenty'  
         WHEN    '3'    THEN    'Thirty'  
         WHEN    '4'    THEN    'Forty'  
         WHEN    '5'    THEN    'Fifty'  
         WHEN    '6'    THEN    'Sixty'  
         WHEN    '7'    THEN    'Seventy'  
         WHEN    '8'    THEN    'Eighty'  
         WHEN    '9'    THEN    'Ninety'  
         ELSE    ''  
     END  
       WHEN   :m   =   1   THEN               -- Leftmost digit of group of 3  
        CASE   :digit  
         WHEN    '0'    THEN    ''  
         WHEN    '1'    THEN    'One'  
         WHEN    '2'    THEN    'Two'  
         WHEN    '3'    THEN    'Three'  
         WHEN    '4'    THEN    'Four'  
         WHEN    '5'    THEN    'Five'  
         WHEN    '6'    THEN    'Six'  
         WHEN    '7'    THEN    'Seven'  
         WHEN    '8'    THEN    'Eight'  
         WHEN    '9'    THEN    'Nine'  
     END   +   
        CASE    WHEN   :digit   <>    '0'    THEN    ' Hundred'    ELSE    ''   END  
   END  */
   
   SELECT CASE   
       WHEN   :m   =   0   THEN               -- Rightmost digit of group of 3  
        CASE   :digit  
         WHEN    '0'    THEN    ''  
         WHEN    '1'    THEN    'One'  
         WHEN    '2'    THEN    'Two'  
         WHEN    '3'    THEN    'Three'  
         WHEN    '4'    THEN    'Four'  
         WHEN    '5'    THEN    'Five'  
         WHEN    '6'    THEN    'Six'  
         WHEN    '7'    THEN    'Seven'  
         WHEN    '8'    THEN    'Eight'  
         WHEN    '9'    THEN    'Nine'  
        END    +   
        CASE   
         WHEN   (:goup    <>    ''    OR   :digit   <>    '0'   )   AND   (:j   +   2)   /   3   =   2   THEN    ' Thousand'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '0'   )   AND   (:j   +   2)   /   3   =   3   THEN    ' Million'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '0'   )   AND   (:j   +   2)   /   3   =   4   THEN    ' Billion'  
         ELSE    ''  
     END  
       WHEN   LENGTH(:digit)   =   2   THEN             -- Special case when middle digit is a '1'  
        CASE   :digit  
         WHEN    '10'    THEN    'Ten'  
         WHEN    '11'    THEN    'Eleven'  
         WHEN    '12'    THEN    'Twelve'  
         WHEN    '13'    THEN    'Thirteen'  
         WHEN    '14'    THEN    'Fourteen'  
         WHEN    '15'    THEN    'Fifteen'  
         WHEN    '16'    THEN    'Sixteen'  
         WHEN    '17'    THEN    'Seventeen'  
         WHEN    '18'    THEN    'Eighteen'  
         WHEN    '19'    THEN    'Nineteen'  
     END   +  
        CASE   
         WHEN   (:goup    <>    ''    OR   :digit   <>    '00'   )   AND   (:j   +   2)   /   3   =   2   THEN    ' Thousand'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '00'   )   AND   (:j   +   2)   /   3   =   3   THEN    ' Million'  
         WHEN   (:goup    <>    ''    OR   :digit   <>    '00'   )   AND   (:j   +   2)   /   3   =   4   THEN    ' Billion'  
         ELSE    ''  
     END  
       WHEN   :m   =   2   THEN               -- Middle digit of group of 3  
        CASE   :digit  
         WHEN    '2'    THEN    'Twenty'  
         WHEN    '3'    THEN    'Thirty'  
         WHEN    '4'    THEN    'Forty'  
         WHEN    '5'    THEN    'Fifty'  
         WHEN    '6'    THEN    'Sixty'  
         WHEN    '7'    THEN    'Seventy'  
         WHEN    '8'    THEN    'Eighty'  
         WHEN    '9'    THEN    'Ninety'  
         ELSE    ''  
     END  
       WHEN   :m   =   1   THEN               -- Leftmost digit of group of 3  
        CASE   :digit  
         WHEN    '0'    THEN    ''  
         WHEN    '1'    THEN    'One'  
         WHEN    '2'    THEN    'Two'  
         WHEN    '3'    THEN    'Three'  
         WHEN    '4'    THEN    'Four'  
         WHEN    '5'    THEN    'Five'  
         WHEN    '6'    THEN    'Six'  
         WHEN    '7'    THEN    'Seven'  
         WHEN    '8'    THEN    'Eight'  
         WHEN    '9'    THEN    'Nine'  
     END   +   
        CASE    WHEN   :digit   <>    '0'    THEN    ' Hundred'    ELSE    ''   END  
   END INTO word from dummy;

    goup:=:goup+RTRIM(:word);         -- Group value 
	IF :word   <>    ''   
    THEN  
      -- SELECT LOCATE ('asd asd', ' ') "locate" FROM DUMMY;
	    IF LOCATE(' ',:word)>0 THEN   
	    	prefix:=LEFT(:word,LOCATE(' ',:word)); 
	    ELSE 
	    	prefix:=:word;
	    END IF;
	    IF RIGHT(result,2)='ty' AND :prefix IN('One','Two','Three','Four','Five','Six','Seven','Eight','Nine') THEN
	        result:=:result ||'-'||LTRIM(:word);
       	ELSE  
        	result:=:result||' '||LTRIM(:word) ;
       	END IF;
   END IF;  
      -- The following needs to be outside of a UDF to work:  
      --IF @debug = 1 SELECT @cn as 'Number', @i as '@i', @j as '@j', @m as '@m', @digit as '@digit', CAST(replace(@group,' ','`') AS CHAR(30)) as '@group', @word as '@word', @result as '@result'  
      i:=   :i   +   1 ; 
      j:=   LENGTH(:cn)   -   :i   +   1  ;
      m:=  mod(:i,3)  ;
   IF :m=1 THEN   
   	goup:=''; 
   END IF;              -- Clear group value when starting a new one   
  END WHILE;
  --IF @result   =    ''    SET   @result   =    '0'  
  IF :result='' THEN result:='0'; END IF;  
  result:= LTRIM(:Result);
END