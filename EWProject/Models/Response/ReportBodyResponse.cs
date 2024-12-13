namespace Client.Models.Response
{
    public class ReportBodyResponse
    {
        public string TypeOfParameter { get; set; } = null!;
        public string DataSetName { get; set; } = null!;
        public List<SubReportBody> ListOfSubReportBody { get; set; } = null!;
    }
    public class SubReportBody
    {
        public string SubReportNamePath { get; set; } = null!;
        public string DataSetName { get; set; } = null!;
        public string SubTypeOfParameter { get; set; } = null!;
    }
    #region Sample Json Request
    //    [
    //	{
    //		"TypeOfParameter":"STORETYPE",
    //		"DataSetName":"DataSource"
    //		"SubReportSource":

    //                [
    //					{
    //						"SubReportNamePath": "",//Name Of SubReport
    //						"DataSetName":"",
    //						"SubTypeOfParameter":""

    //                    }

    //				]
    //	},
    //	{
    //    "TypeOfParameter":"STORETYPE",
    //		"DataSetName":"DataSource"

    //        "SubReportSource"::[]

    //    }
    //]
    #endregion
}
