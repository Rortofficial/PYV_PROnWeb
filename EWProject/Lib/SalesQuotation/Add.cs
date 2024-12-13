using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.SalesQuotation
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Add(PostSalesQuotationRequest postSalesQuotationRequest, Company _company)
        {
            _Add(postSalesQuotationRequest, _company);
        }
        private void _Add(PostSalesQuotationRequest postSalesQuotationRequest, Company _company)
        {
            try
            {
                Documents SalesQuotation = (Documents)_company.GetBusinessObject(BoObjectTypes.oQuotations);
                SalesQuotation.UserFields.Fields.Item("U_CUSTOMERTYPE").Value = "NEW";
                SalesQuotation.Address2 = postSalesQuotationRequest.AddressDetail;
                SalesQuotation.DocCurrency = postSalesQuotationRequest.BPCurrency;
                SalesQuotation.ContactPersonCode = Convert.ToInt32(postSalesQuotationRequest.ContactPersons.Code);
                SalesQuotation.CardCode = postSalesQuotationRequest.CustomerCode;
                if (postSalesQuotationRequest.AddressCode != "-1")
                {
                    SalesQuotation.ShipToCode = postSalesQuotationRequest.AddressCode;
                }
                SalesQuotation.ContactPersonCode = Convert.ToInt32(postSalesQuotationRequest.ContactPersons.Code);
                SalesQuotation.DocType = BoDocumentTypes.dDocument_Items;
                SalesQuotation.DocDate = Convert.ToDateTime(postSalesQuotationRequest.Date);
                SalesQuotation.DocDueDate = Convert.ToDateTime(postSalesQuotationRequest.Validity);
                SalesQuotation.TaxDate = Convert.ToDateTime(postSalesQuotationRequest.Date);
                SalesQuotation.CardCode = postSalesQuotationRequest.CustomerCode;
                //SalesQuotation.DocRate = 35;
                //SalesQuotation.DocCurrency = "THS";
                SalesQuotation.UserFields.Fields.Item("U_COMMODITYTYPE").Value = postSalesQuotationRequest.CommodityType;
                SalesQuotation.UserFields.Fields.Item("U_CustomerName").Value = postSalesQuotationRequest.CustomerCode;
                //SalesQuotation.NumAtCard = postSalesQuotationRequest.ReftNo;
                SalesQuotation.UserFields.Fields.Item("U_ATTN").Value = ClearEmptyString.clearEmptyString(postSalesQuotationRequest.ATTN);
                SalesQuotation.UserFields.Fields.Item("U_EMAIL").Value = ClearEmptyString.clearEmptyString(postSalesQuotationRequest.Email);
                //SalesQuotation.UserFields.Fields.Item("U_VALIDITY").Value = (postSalesQuotationRequest.Validity==null)?"":postSalesQuotationRequest.Validity;
                SalesQuotation.UserFields.Fields.Item("U_TEL").Value = ClearEmptyString.clearEmptyString(postSalesQuotationRequest.Tel);
                SalesQuotation.UserFields.Fields.Item("U_SERVICE").Value = postSalesQuotationRequest.ServiceType;
                SalesQuotation.UserFields.Fields.Item("U_ORIGIN").Value = postSalesQuotationRequest.Origin;
                SalesQuotation.UserFields.Fields.Item("U_DESTINATION").Value = postSalesQuotationRequest.Destination;
                SalesQuotation.UserFields.Fields.Item("U_UserCreate").Value = ClearEmptyString.clearEmptyString(postSalesQuotationRequest.CreateBy);
                SalesQuotation.UserFields.Fields.Item("U_DG").Value = ClearEmptyString.clearEmptyString(postSalesQuotationRequest.DG);
                SalesQuotation.PaymentGroupCode = postSalesQuotationRequest.CreditTerm;
                //SalesQuotation.Comments = postSalesQuotationRequest.Remarks;
                SalesQuotation.UserFields.Fields.Item("U_Remarks").Value = ClearEmptyString.clearEmptyString(postSalesQuotationRequest.Remarks);
                SalesQuotation.SalesPersonCode = Convert.ToInt32(postSalesQuotationRequest.SaleEmployee);
                SalesQuotation.UserFields.Fields.Item("U_WEBID").Value = (DateTime.Now.Day.ToString()
                                        + DateTime.Now.Month.ToString()
                                        + DateTime.Now.Year.ToString()
                                        + DateTime.Now.DayOfYear.ToString()
                                        + DateTime.Now.Hour.ToString()
                                        + DateTime.Now.Minute.ToString()
                                        + DateTime.Now.Second.ToString()
                                        + DateTime.Now.Millisecond.ToString()).ToString();
                foreach (var a in postSalesQuotationRequest.Lines)
                {
                    SalesQuotation.Lines.ItemCode = a.ItemCode;
                    SalesQuotation.Lines.Quantity = 1;
                    SalesQuotation.Lines.Price = a.Price;
                    SalesQuotation.Lines.VatGroup = "S00";
                    SalesQuotation.Lines.COGSCostingCode = postSalesQuotationRequest.CostCenter;
                    SalesQuotation.Lines.UserFields.Fields.Item("U_Remark").Value = (a.Remarks == null) ? "" : a.Remarks;
                    SalesQuotation.Lines.Add();
                }
                int RetVal = SalesQuotation.Add();
                if (RetVal != 0)
                {
                    _company.GetLastError(out _ErroreCode, out _MessageErrore);
                }
                else
                {
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _DocEntry = _company.GetNewObjectKey();
                }
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = ex.Message;
            }
        }
    }
}

