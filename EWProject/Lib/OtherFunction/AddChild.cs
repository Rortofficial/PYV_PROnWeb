using SAPbobsCOM;
using System.Reflection;

namespace Client.Lib.OtherFunction
{
    public class AddChild
    {
        public static void AddChildRow<T>(List<T> objList, GeneralData oGeneralData, string TableName, string LineIdName)
        {
            GeneralData oChild;
            GeneralDataCollection oBookingChildrenAdd;
            oBookingChildrenAdd = oGeneralData.Child(TableName);
            Type temp = typeof(T);
            if (objList == null)
            {
                return;
            }
            foreach (var a in objList)
            {
                oChild = oBookingChildrenAdd.Add();
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name != LineIdName)
                    {
                        if (pro.PropertyType == typeof(string))
                        {
                            oChild.SetProperty("U_" + pro.Name, ClearEmptyString.clearEmptyString((string)pro.GetValue(a)));
                        }
                        else
                        {
                            oChild.SetProperty("U_" + pro.Name, pro.GetValue(a));
                        }
                    }
                }
            }
        }
    }
}
