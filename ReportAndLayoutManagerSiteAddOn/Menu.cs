using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;

namespace ReportAndLayoutManagerSiteAddOn
{
    class Menu
    {
        Company _company = new Company();
        ReportTypesService rptTypeService = null;
        ReportTypesParams reportTypesParams = null;
        public void AddMenuItems()
        {
            try
            {
                //Properties._company = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();
                //if (Properties._company.Connected == true)
                //{
                //    Properties._company.Connect();
                //    Properties.onLoadReportObject();
                //}
                _company = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();
                if (_company.Connected == true)
                {
                    _company.Connect();
                    rptTypeService = (ReportTypesService)_company.GetCompanyService().GetBusinessService(ServiceTypes.ReportTypesService);
                    rptTypeService = (ReportTypesService)_company.GetCompanyService().GetBusinessService(ServiceTypes.ReportTypesService);
                    reportTypesParams = rptTypeService.GetReportTypeList();

                }
            }
            catch (Exception er)
            { //  Menu already exists
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists And " + er.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }
        }
        public void SBO_Application_LayoutKeyEvent(ref SAPbouiCOM.LayoutKeyInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                SAPbouiCOM.Form oForm;
                oForm = Application.SBO_Application.Forms.ActiveForm;
                switch (oForm.TypeEx)
                {
                    case "UDO_FT_JOBSHEETRUCKING":
                        eventInfo.LayoutKey = ((SAPbouiCOM.EditText)oForm.Items.Item("0_U_E").Specific).Value;
                        break;
                }
            }
            catch (Exception e)
            {

            }
        }
        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction == false)
                {
                    SAPbouiCOM.Form oForm;
                    oForm = Application.SBO_Application.Forms.ActiveForm;
                    switch (oForm.TypeEx)
                    {
                        case "UDO_FT_JOBSHEETRUCKING":

                            int i = 0;
                            while (i < reportTypesParams.Count)
                            {
                                if ((reportTypesParams.Item(i).TypeName == "JOBSHEETRUCKING"))
                                {
                                    oForm.ReportType = reportTypesParams.Item(i).TypeCode; break;
                                }
                                i = (i + 1);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
