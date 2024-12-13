namespace Client.Lib.OtherFunction
{
    public class ConvertNumberToWord
    {
        // Ones
        private static string ones(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = "";
            switch (_Number)
            {

                case 1:
                    name = "ONE";
                    break;
                case 2:
                    name = "TWO";
                    break;
                case 3:
                    name = "THREE";
                    break;
                case 4:
                    name = "FOUR";
                    break;
                case 5:
                    name = "FIVE";
                    break;
                case 6:
                    name = "SIX";
                    break;
                case 7:
                    name = "SEVEN";
                    break;
                case 8:
                    name = "EIGHT";
                    break;
                case 9:
                    name = "NINE";
                    break;
            }
            return name;
        }

        // Tens
        private static string tens(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = null;
            switch (_Number)
            {
                case 10:
                    name = "TEN";
                    break;
                case 11:
                    name = "ELEVEN";
                    break;
                case 12:
                    name = "TWELVE";
                    break;
                case 13:
                    name = "THIRTEEN";
                    break;
                case 14:
                    name = "FOURTEEN";
                    break;
                case 15:
                    name = "FIFTEEN";
                    break;
                case 16:
                    name = "SIXTEEN";
                    break;
                case 17:
                    name = "SEVENTEEN";
                    break;
                case 18:
                    name = "EIGHTEEN";
                    break;
                case 19:
                    name = "NINETEEN";
                    break;
                case 20:
                    name = "TWENTY";
                    break;
                case 30:
                    name = "THIRTY";
                    break;
                case 40:
                    name = "FOURTY";
                    break;
                case 50:
                    name = "FIFTY";
                    break;
                case 60:
                    name = "SIXTY";
                    break;
                case 70:
                    name = "SEVENTY";
                    break;
                case 80:
                    name = "EIGHTY";
                    break;
                case 90:
                    name = "NINETY";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static string ConvertWholeNumber(string Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = Convert.ToDouble(Number);
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = Number.StartsWith("0");

                    int numDigits = Number.Length;
                    int pos = 0;//store digit grouping    
                    string place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = ones(Number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = tens(Number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = numDigits % 3 + 1;
                            place = " HUNDRED ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = numDigits % 4 + 1;
                            place = " THOUSAND ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = numDigits % 7 + 1;
                            place = " MILLION ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = numDigits % 10 + 1;
                            place = " BILLION ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        private static string ConvertToWords(string numb)
        {
            string val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            string endStr = "Bath";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "AND";// just to separate whole numbers from points/cents    
                        endStr = " " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = string.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }

        private static string ConvertDecimals(string number)
        {
            string cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "ZERO";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }

        public static string _ConvertToWords(string number)
        {
            string Words = "";
            if (number.Contains("-"))
            {
                Words = "MINUS ";
            }
            return Words + ConvertToWords(number);
        }
    }
}
