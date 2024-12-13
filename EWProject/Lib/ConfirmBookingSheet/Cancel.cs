using Client.Lib.OtherFunction;
using SAPbobsCOM;

namespace Client.Lib.ConfirmBookingSheet
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Cancel(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries, Company oCompany)
        {
            _Cancel(docNum, bookingDocEntry, projectManagementEntry, projectSeries, oCompany);
        }
        private void _Cancel(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries, Company oCompany)
        {
            
            try
            {
                if (oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                oCompany.StartTransaction();
                GeneralService oGeneralService;
                //GeneralData oGeneralData;
                GeneralDataParams oGeneralParams;
                //GeneralDataParams oGeneralParams;
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                #region Update User Define Field of ConfirmBookingSheet to Blank
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                oConfirmBookingGeneralServiceUpdate = companyService.GetGeneralService("BOOKINGSHEET");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", bookingDocEntry);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_CONFIRMBOOKINGSHEETID", "");
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion
                #region Cancel ConfrimBookingSheet
                oGeneralService = companyService.GetGeneralService("CONFIRMBOOKINGSHEET");
                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", docNum);
                oGeneralService.Cancel(oGeneralParams);
                #endregion
                #region Cancel Project Management
                ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)companyService.GetBusinessService(ServiceTypes.ProjectManagementService);
                PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                projectToUpdateParam.AbsEntry = Convert.ToInt32(projectManagementEntry);
                PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                projectUpdateProjectManagement.ProjectName = projectSeries + "-" + projectManagementEntry + "-C";
                pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                pmgServiceUpdateProjectManagement.CancelProject(projectToUpdateParam);
                #endregion
                if (GetQuery.GetValueByID("CountingJobforDeleteFinancialProject", "Condition", projectManagementEntry) == "0")
                {
                    #region Delete Financial Project
                    IProjectsService projectService;
                    IProject project;
                    projectService = (IProjectsService)companyService.GetBusinessService(ServiceTypes.ProjectsService);
                    project = (IProject)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProject);
                    ProjectParams projectParams = (ProjectParams)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProjectParams);
                    projectParams.Code = projectSeries;
                    projectService.DeleteProject(projectParams);
                    #endregion
                }
                else
                {
                    #region Update Financial Project
                    IProjectsService projectService;
                    IProject project;
                    projectService = (IProjectsService)companyService.GetBusinessService(ServiceTypes.ProjectsService);
                    project = (IProject)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProject);
                    ProjectParams projectParams = (ProjectParams)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProjectParams);
                    projectParams.Code = projectSeries;
                    Project projectUpdateProjectFinance = projectService.GetProject(projectParams);
                    projectUpdateProjectFinance.Name = projectSeries + "-C";
                    projectUpdateProjectFinance.Active = BoYesNoEnum.tNO;
                    projectService.UpdateProject(projectUpdateProjectFinance);
                    #endregion
                }
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = docNum;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                if(oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
