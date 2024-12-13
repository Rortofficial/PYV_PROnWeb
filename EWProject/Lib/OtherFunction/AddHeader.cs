using SAPbobsCOM;
using System.Reflection;

namespace Client.Lib.OtherFunction
{
    public class AddHeader
    {
        public static void AddHeaderObject<T>(T objList, GeneralData oGeneralData)
        {
            Type temp = typeof(T);
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.PropertyType == typeof(string))
                {
                    oGeneralData.SetProperty("U_" + pro.Name, ClearEmptyString.clearEmptyString((string)pro.GetValue(objList)));
                }
                else
                {
                    oGeneralData.SetProperty("U_" + pro.Name, pro.GetValue(objList));
                }
            }
        }
    }
}
