using Client.Connection;
using Client.Models.Response;
using SAPbobsCOM;
using System.Reflection;

namespace Client.Lib.OtherFunction
{
    public class UpdateChild
    {
        public static void UpdateChildObject<T>(List<T> objList, GeneralData oGeneralData, string TableName, string keyPoint, string bookingID)
        {
            GeneralData oBookingChildUpdate = null;
            GeneralDataCollection oBookingChildrenUpdate;
            oBookingChildrenUpdate = oGeneralData.Child(TableName);
            //Update Record
            if (objList != null)
            {
                //Check Record Count in Here
                GetRecordCount RecordCount = QueryData.ConvertDataTable<GetRecordCount>(new GetRecordByDataTable(
                "GetRecordCountUserDefindTable",
                             TableName,
                             bookingID,
                             "",
                             "",
                             "").GetResultDataTable())[0];
                if (RecordCount.RecordCount > 0)
                {
                    for (int i = 1; i <= RecordCount.RecordCount; i++)
                    {
                        oBookingChildrenUpdate.Remove(0);
                    }
                }
                Type temp = typeof(T);
                foreach (var a in objList)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == keyPoint)
                        {
                            oBookingChildUpdate = oBookingChildrenUpdate.Add();
                        }
                        else
                        {
                            if (pro.PropertyType == typeof(string))
                            {
                                oBookingChildUpdate.SetProperty("U_" + pro.Name, ClearEmptyString.clearEmptyString((string)pro.GetValue(a)));
                            }
                            else
                            {
                                oBookingChildUpdate.SetProperty("U_" + pro.Name, pro.GetValue(a));
                            }

                        }
                    }
                }
            }
            //Delete Record
            else
            {
                //Check Record Count in Here
                GetRecordCount RecordCount = QueryData.ConvertDataTable<GetRecordCount>(new GetRecordByDataTable(
                "GetRecordCountUserDefindTable",
                             TableName,
                             bookingID,
                             "",
                             "",
                             "").GetResultDataTable())[0];
                if (RecordCount.RecordCount > 0)
                {
                    for (int i = 0; i < RecordCount.RecordCount; i++)
                    {
                        oBookingChildrenUpdate.Remove(i);
                    }
                }
            }
        }
    }
}
