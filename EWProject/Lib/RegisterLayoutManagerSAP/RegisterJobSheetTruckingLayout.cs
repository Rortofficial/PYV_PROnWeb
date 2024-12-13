using SAPbobsCOM;

namespace Client.Lib.RegisterLayoutManagerSAP
{
    public class RegisterJobSheetTruckingLayout
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public RegisterJobSheetTruckingLayout(string TypeName, string AddonName, string AddonFormType, string MenuID, string Name, string RepName, Company oCompany, IWebHostEnvironment _webHostEnvironment)
        {
            Add_ReportToForm(TypeName, AddonName, AddonFormType, MenuID, Name, RepName, oCompany, _webHostEnvironment);
        }
        private void Add_ReportToForm(string TypeName, string AddonName, string AddonFormType, string MenuID, string Name, string RepName, Company oCompany, IWebHostEnvironment _webHostEnvironment)
        {
            try
            {
                ReportTypesService rptTypeService = (ReportTypesService)oCompany.GetCompanyService().GetBusinessService(ServiceTypes.ReportTypesService);
                ReportType newType = (ReportType)rptTypeService.GetDataInterface(ReportTypesServiceDataInterfaces.rtsReportType);
                ReportTypeParams newTypeParam = null/* TODO Change to default(_) if this is not a reference type */;
                ReportLayoutsService rptService = (ReportLayoutsService)oCompany.GetCompanyService().GetBusinessService(ServiceTypes.ReportLayoutsService);
                ReportLayout newReport = (ReportLayout)rptService.GetDataInterface(ReportLayoutsServiceDataInterfaces.rlsdiReportLayout);
                ReportLayoutParams newReportParam = null/* TODO Change to default(_) if this is not a reference type */;
                // Add new Report
                newType.TypeName = TypeName;
                newType.AddonName = AddonName;
                newType.AddonFormType = AddonFormType;
                newType.MenuID = MenuID;

                newTypeParam = rptTypeService.AddReportType(newType);

                newReport.Author = oCompany.UserName;
                newReport.Category = ReportLayoutCategoryEnum.rlcCrystal;
                newReport.Name = Name;
                newReport.TypeCode = newTypeParam.TypeCode;

                newReportParam = rptService.AddReportLayout(newReport);

                newType = rptTypeService.GetReportType(newTypeParam);
                newType.DefaultReportLayout = newReportParam.LayoutCode;
                rptTypeService.UpdateReportType(newType);
                //_DocEntry = oCompany.GetNewObjectKey();
                #region Create or Import Layout Comment
                BlobParams oBlobParams = (BlobParams)oCompany.GetCompanyService().GetDataInterface(CompanyServiceDataInterfaces.csdiBlobParams);
                BlobTableKeySegment oKeySegment = null/* TODO Change to default(_) if this is not a reference type */;
                FileStream oFile = null;
                int fileSize = 0;
                byte[] buf = null;
                Blob oBlob = (Blob)oCompany.GetCompanyService().GetDataInterface(CompanyServiceDataInterfaces.csdiBlob);
                oBlobParams.Table = "RDOC";
                oBlobParams.Field = "Template";
                oKeySegment = oBlobParams.BlobTableKeySegments.Add();
                oKeySegment.Name = "DocCode";
                oKeySegment.Value = newReportParam.LayoutCode;

                oFile = new System.IO.FileStream($"{_webHostEnvironment.WebRootPath}\\Report\\tes.rpt", System.IO.FileMode.Open);
                fileSize = System.Convert.ToInt32(oFile.Length);
                buf = new byte[(fileSize) - 1 + 1];
                oFile.Read(buf, 0, fileSize);
                oFile.Dispose();

                oBlob.Content = Convert.ToBase64String(buf, 0, fileSize);
                oCompany.GetCompanyService().SetBlob(oBlobParams, oBlob);
                //How to Call Function
                //Add_ReportToForm("Billing", "Billing Form", "FrmBilling", "FrmBilling", "Billing", "Form_Billing.rpt")
                #endregion
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }

    }
}
