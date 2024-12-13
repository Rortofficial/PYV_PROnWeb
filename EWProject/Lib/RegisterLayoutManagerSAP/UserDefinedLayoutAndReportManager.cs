using SAPbobsCOM;

namespace Client.Lib.RegisterLayoutManagerSAP
{
    public class UserDefinedLayoutAndReportManager
    {
        public static RegisterJobSheetTruckingLayout _RegisterJobSheetTruckingLayout(string TypeName, string AddonName, string AddonFormType, string MenuID, string Name, string RepName, Company oCompany, IWebHostEnvironment _webHostEnvironment) => new RegisterJobSheetTruckingLayout(TypeName, AddonName, AddonFormType, MenuID, Name, RepName, oCompany, _webHostEnvironment);
    }
}
