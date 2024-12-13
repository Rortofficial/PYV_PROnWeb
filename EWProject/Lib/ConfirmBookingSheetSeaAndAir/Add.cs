using Client.Lib.OtherFunction;
using Client.Lib.Purchase;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.ConfirmBookingSheetSeaAndAir
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _DocEntry;
        public Add(PostConfirmBookingSheetRequest postConfirmBookingSheetRequest, Company oCompany)
        {
            _Add(postConfirmBookingSheetRequest, oCompany);
        }
        private void _Add(PostConfirmBookingSheetRequest postConfirmBookingSheetRequest, Company oCompany)
        {
            oCompany.StartTransaction();
            try
            {
                #region Code
                var EwSeriesID = GetQuery.GetValueByID("GETJOBNOBYIDSEAANDAIR", "EWSeriesNo", postConfirmBookingSheetRequest.BookingID.ToString());
                #region Add Confirm Booking
                CompanyService companyService;
                companyService = oCompany.GetCompanyService();
                if (GetQuery.GetValueByID("CheckingExistingJobinFinancialProjectForCreateNew", "Condition", EwSeriesID) == "0")
                {
                    CompanyService oCmpSrv;
                    IProjectsService projectService;
                    Project project;
                    oCmpSrv = oCompany.GetCompanyService();
                    projectService = (IProjectsService)oCmpSrv.GetBusinessService(ServiceTypes.ProjectsService);
                    project = (Project)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProject);
                    project.Code = EwSeriesID;
                    project.Name = EwSeriesID;
                    var resProjectService = projectService.AddProject(project);
                    string DocEntryProject = resProjectService.Code;
                }
                else
                {
                    #region Update Financial Project
                    IProjectsService projectService;
                    IProject project;
                    projectService = (IProjectsService)companyService.GetBusinessService(ServiceTypes.ProjectsService);
                    project = (IProject)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProject);
                    ProjectParams projectParams = (ProjectParams)projectService.GetDataInterface(ProjectsServiceDataInterfaces.psProjectParams);
                    projectParams.Code = EwSeriesID;
                    Project projectUpdateProjectFinance = projectService.GetProject(projectParams);
                    projectUpdateProjectFinance.Name = EwSeriesID;
                    projectUpdateProjectFinance.Active = BoYesNoEnum.tYES;
                    projectService.UpdateProject(projectUpdateProjectFinance);
                    #endregion
                }
                #region Add Project Management
                int absEntryOfCreatedProject = -1;
                CompanyService oCompServ = null!;
                ProjectManagementService pmgService = null!;
                oCompServ = oCompany.GetCompanyService();
                pmgService = (ProjectManagementService)oCompServ.GetBusinessService(ServiceTypes.ProjectManagementService);
                PM_ProjectDocumentData projectManagement = (PM_ProjectDocumentData)pmgService.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentData);
                projectManagement.ProjectName = ClearEmptyString.clearEmptyString(EwSeriesID);
                projectManagement.SalesEmployee = Convert.ToInt32(postConfirmBookingSheetRequest.SlpCode);
                projectManagement.StartDate = DateTime.Today;
                projectManagement.DueDate = DateTime.Today;
                projectManagement.Territory = Convert.ToInt32(GetQuery.GetValueByID("GETROUTEBYIDSEAANDAIR", "ROUTE", postConfirmBookingSheetRequest.BookingID.ToString()));
                projectManagement.ProjectType = ProjectTypeEnum.pt_External;
                projectManagement.ProjectStatus = ProjectStatusTypeEnum.pst_Started;
                projectManagement.FinancialProject = ClearEmptyString.clearEmptyString(EwSeriesID);
                projectManagement.RiskLevel = RiskLevelTypeEnum.rlt_Low;
                PM_ProjectDocumentParams projectParam = pmgService.AddProject(projectManagement);
                absEntryOfCreatedProject = projectParam.AbsEntry;
                #endregion

                GeneralService oGeneralService;
                GeneralData oGeneralData;
                GeneralData oChild;
                GeneralDataCollection oChildren;

                oGeneralService = companyService.GetGeneralService("CON_BOOKING_S_A");
                oGeneralData = (GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                oGeneralData.SetProperty("U_BOOKINGID", postConfirmBookingSheetRequest.BookingID);
                oGeneralData.SetProperty("U_CreateUser", ClearEmptyString.clearEmptyString(postConfirmBookingSheetRequest.CreateUser));
                oGeneralData.SetProperty("U_CSName", ClearEmptyString.clearEmptyString(postConfirmBookingSheetRequest.CSName));
                oGeneralData.SetProperty("U_PRICELIST", ClearEmptyString.clearEmptyString(postConfirmBookingSheetRequest.PriceList));
                oGeneralData.SetProperty("Remark", (postConfirmBookingSheetRequest.Remarks == null) ? "" : postConfirmBookingSheetRequest.Remarks);
                //Add Line
                if (postConfirmBookingSheetRequest.Lines != null)
                {
                    int LineNum = 1;
                    oChildren = oGeneralData.Child("TRUCK_INFO_S_A");
                    foreach (var a in postConfirmBookingSheetRequest.Lines)
                    {
                        oChild = oChildren.Add();
                        oChild.SetProperty("U_TYPE", ClearEmptyString.clearEmptyString(a.Type));
                        oChild.SetProperty("U_ExchangeType", ClearEmptyString.clearEmptyString(a.ExchangeType));
                        oChild.SetProperty("U_CONTAINERTYPE", ClearEmptyString.clearEmptyString(a.ContainerType));
                        oChild.SetProperty("U_CONTAINERNO", ClearEmptyString.clearEmptyString(a.ContainerNo));
                        oChild.SetProperty("U_OWNER", ClearEmptyString.clearEmptyString(a.Owner));
                        oChild.SetProperty("U_CONTAINERNO2", ClearEmptyString.clearEmptyString(a.ContainerNo2));
                        oChild.SetProperty("U_OWNER2", ClearEmptyString.clearEmptyString(a.Owner2));
                        oChild.SetProperty("U_GROSSWEIGHT", ClearEmptyString.clearEmptyString(a.GrossWeight));
                        oChild.SetProperty("U_YARD", ClearEmptyString.clearEmptyString(a.Yard));
                        oChild.SetProperty("U_FCL_LCL", ClearEmptyString.clearEmptyString(a.FCL_LCL));
                        oChild.SetProperty("U_LOLO_UNLOADING", ClearEmptyString.clearEmptyString(a.LOLO_Unloading));
                        oChild.SetProperty("U_TRUCKPROVINCE", ClearEmptyString.clearEmptyString(a.TruckProvince));
                        oChild.SetProperty("U_TRUCKPLATENO", ClearEmptyString.clearEmptyString(a.TruckPlateNo));
                        oChild.SetProperty("U_TRUCKTYPE", ClearEmptyString.clearEmptyString(a.TruckType));
                        oChild.SetProperty("U_BRAND", ClearEmptyString.clearEmptyString(a.Brand));
                        oChild.SetProperty("U_TRUCKCODE", ClearEmptyString.clearEmptyString(a.TruckCode));
                        oChild.SetProperty("U_COLOR", ClearEmptyString.clearEmptyString(a.Color));
                        oChild.SetProperty("U_TRAILERPROVINCE", ClearEmptyString.clearEmptyString(a.TrailerProvince));
                        oChild.SetProperty("U_TRAILERPLATE", ClearEmptyString.clearEmptyString(a.TrailerPlate));
                        oChild.SetProperty("U_TRAILERTYPE", ClearEmptyString.clearEmptyString(a.TrailerType));
                        oChild.SetProperty("U_DRIVER1", ClearEmptyString.clearEmptyString(a.DriverCode1));
                        oChild.SetProperty("U_TPNO1", ClearEmptyString.clearEmptyString(a.TPNo1));
                        oChild.SetProperty("U_IDCARD1", ClearEmptyString.clearEmptyString(a.IDCard1));
                        oChild.SetProperty("U_DRIVERLICENSE1", ClearEmptyString.clearEmptyString(a.DriverLicense1));
                        oChild.SetProperty("U_DRIVER2", ClearEmptyString.clearEmptyString(a.DriverCode2));
                        oChild.SetProperty("U_TPNO2", ClearEmptyString.clearEmptyString(a.TPNo2));
                        oChild.SetProperty("U_IDCARD2", ClearEmptyString.clearEmptyString(a.IDCard2));
                        oChild.SetProperty("U_DRIVERLICENSE2", ClearEmptyString.clearEmptyString(a.DriverLicense2));
                        oChild.SetProperty("U_VENDORCODE", ClearEmptyString.clearEmptyString(a.VendorCode));
                        oChild.SetProperty("U_TRUCKCOSTTHB", a.TruckCostTHB);
                        oChild.SetProperty("U_SEALNO1", ClearEmptyString.clearEmptyString(a.SealNo1));
                        oChild.SetProperty("U_SEALNO2", ClearEmptyString.clearEmptyString(a.SealNo2));
                        oChild.SetProperty("U_ContainerOption", ClearEmptyString.clearEmptyString(a.ContainerOption));
                        oChild.SetProperty("U_BookingLineId", ClearEmptyString.clearEmptyString(a.BookingLineId));
                        if (a.ListAdvancePayment != null)
                        {
                            GeneralDataCollection oChildren2;
                            GeneralData oChild2;
                            oChildren2 = oGeneralData.Child("TB_ADVANCE_SEAAIR");
                            foreach (var advancePayment in a.ListAdvancePayment)
                            {
                                #region Advance Payment
                                advancePayment.JobNo = EwSeriesID;
                                var rs = new AddPurchaseOrAdvancePayment(advancePayment, oCompany, absEntryOfCreatedProject, "PRAD");
                                if (rs.ErroreCode != 0)
                                {
                                    _ErroreCode = rs.ErroreCode;
                                    _MessageErrore = rs.ErroreMessage;
                                    return;
                                }
                                #endregion
                                oChild2 = oChildren2.Add();
                                oChild2.SetProperty("U_CONTAINERLINEID", LineNum);
                                oChild2.SetProperty("U_DocEntry", rs.DocEntry);
                            }
                        }
                        if (a.ListPurchaseRequest != null)
                        {
                            GeneralDataCollection oChildren2;
                            GeneralData oChild2;
                            oChildren2 = oGeneralData.Child("TB_PURCHASE_S_A");
                            foreach (var order in a.ListPurchaseRequest)
                            {
                                var webID = (DateTime.Now.Day.ToString()
                                            + DateTime.Now.Month.ToString()
                                            + DateTime.Now.Year.ToString()
                                            + DateTime.Now.DayOfYear.ToString()
                                            + DateTime.Now.Hour.ToString()
                                            + DateTime.Now.Minute.ToString()
                                            + DateTime.Now.Second.ToString()
                                            + DateTime.Now.Millisecond).ToString();

                                #region PurchaseRequest
                                order.JobNo = EwSeriesID;
                                var rs = new AddPurchaseOrAdvancePayment(order, oCompany, absEntryOfCreatedProject, "PRCOS");
                                if (rs.ErroreCode != 0)
                                {
                                    _DocEntry = 0;
                                    _ErroreCode = rs.ErroreCode;
                                    _MessageErrore = rs.ErroreMessage;
                                    if(oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                                    return;
                                }
                                #endregion
                                oChild2 = oChildren2.Add();
                                oChild2.SetProperty("U_CONTAINERLINEID", LineNum);
                                oChild2.SetProperty("U_DOCENTRY", rs.DocEntry);
                            }
                        }
                        LineNum++;
                    }
                }
                var responseoGeneralService = oGeneralService.Add(oGeneralData);
                int DocEntryConfirmBookingSheet = (int)responseoGeneralService.GetProperty("DocEntry");
                #endregion
                #region Update DocEntry Of BookingSheet link with Confirm BookingSheet
                GeneralService oBookingGeneralServiceUpdate;
                GeneralData oBookingGeneralDataUpdate;
                GeneralDataParams oBookingGeneralParamsUpdate;
                CompanyService oBookingCmpSrvUpdate;
                oBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oBookingGeneralServiceUpdate = oBookingCmpSrvUpdate.GetGeneralService("BOOKING_SEA_AIR");
                oBookingGeneralParamsUpdate = (GeneralDataParams)oBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oBookingGeneralParamsUpdate.SetProperty("DocEntry", postConfirmBookingSheetRequest.BookingID);
                oBookingGeneralDataUpdate = oBookingGeneralServiceUpdate.GetByParams(oBookingGeneralParamsUpdate);
                oBookingGeneralDataUpdate.SetProperty("U_CONFIRMBOOKINGSHEETID", DocEntryConfirmBookingSheet);
                oBookingGeneralServiceUpdate.Update(oBookingGeneralDataUpdate);
                #endregion
                #region Update DocEntry Of ConfirmBookingSheet link with ProjectManagement
                GeneralService oConfirmBookingGeneralServiceUpdate;
                GeneralData oConfirmBookingGeneralDataUpdate;
                GeneralDataParams oConfirmBookingGeneralParamsUpdate;
                CompanyService oConfirmBookingCmpSrvUpdate;
                oConfirmBookingCmpSrvUpdate = oCompany.GetCompanyService();
                oConfirmBookingGeneralServiceUpdate = oConfirmBookingCmpSrvUpdate.GetGeneralService("CON_BOOKING_S_A");
                oConfirmBookingGeneralParamsUpdate = (GeneralDataParams)oConfirmBookingGeneralServiceUpdate.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oConfirmBookingGeneralParamsUpdate.SetProperty("DocEntry", DocEntryConfirmBookingSheet);
                oConfirmBookingGeneralDataUpdate = oConfirmBookingGeneralServiceUpdate.GetByParams(oConfirmBookingGeneralParamsUpdate);
                oConfirmBookingGeneralDataUpdate.SetProperty("U_PROJECTMANAGEMENTID", absEntryOfCreatedProject);
                oConfirmBookingGeneralServiceUpdate.Update(oConfirmBookingGeneralDataUpdate);
                #endregion
                #region AddLine Project Management
                CompanyService oCompServUpdateProject = oCompany.GetCompanyService();
                ProjectManagementService pmgServiceUpdateProjectManagement = (ProjectManagementService)oCompServUpdateProject.GetBusinessService(ServiceTypes.ProjectManagementService);
                PM_ProjectDocumentParams projectToUpdateParam = (PM_ProjectDocumentParams)pmgServiceUpdateProjectManagement.GetDataInterface(ProjectManagementServiceDataInterfaces.pmsPM_ProjectDocumentParams);
                projectToUpdateParam.AbsEntry = absEntryOfCreatedProject;
                PM_ProjectDocumentData projectUpdateProjectManagement = pmgServiceUpdateProjectManagement.GetProject(projectToUpdateParam);
                projectUpdateProjectManagement.UserFields.Item("U_CONFIRMBOOKINGSHEETID").Value = DocEntryConfirmBookingSheet;
                pmgServiceUpdateProjectManagement.UpdateProject(projectUpdateProjectManagement);
                #endregion
                #endregion
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                _ErroreCode = 0;
                _MessageErrore = "";
                _DocEntry = DocEntryConfirmBookingSheet;
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
                _DocEntry= 0;
                if(oCompany.InTransaction) oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
            }
        }
    }
}
