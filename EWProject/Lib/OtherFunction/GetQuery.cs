using Client.Connection;
using Client.Models.Gets;
using System.Data;

namespace Client.Lib.OtherFunction
{
    public class GetQuery
    {
        public static string GetValueByID(string type, string field, string par1 = "", string par2 = "", string par3 = "", string par4 = "", string par5 = "", GetRecordByDataTable.StoreType storeType= GetRecordByDataTable.StoreType.Transaction)
        {
            try
            {
                var getBookingSheetByUsersList = new List<GetBookingSheetByUser>();
                foreach (DataRow dataRow in new GetRecordByDataTable(
                    storeType:storeType,
                             type,
                             par1,
                             par2,
                             par3,
                             par4,
                             "").GetResultDataTable().Rows)
                {
                    return dataRow[field].ToString()!;
                }
                return "-1";
            }
            catch //(Exception ex)
            {
                return "-1";
            }
        }
    }
}
