using SAPbobsCOM;

namespace ReportAndLayoutManagerSiteAddOn
{
    public class Properties
    {
        public static Company _company = new Company();
        public static ReportTypesService rptTypeService = null;
        public static ReportTypesParams reportTypesParams = null;
        public static void onConnectionDB()
        {
            _company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            if (_company.Connected != true)
            {
                _company.Connect();
                onLoadReportObject();
            }
        }
        public static void onLoadReportObject()
        {
            rptTypeService = (ReportTypesService)_company.GetCompanyService().GetBusinessService(ServiceTypes.ReportTypesService);
            rptTypeService = (ReportTypesService)_company.GetCompanyService().GetBusinessService(ServiceTypes.ReportTypesService);
            reportTypesParams = rptTypeService.GetReportTypeList();
        }
    }
}
