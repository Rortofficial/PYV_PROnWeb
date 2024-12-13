using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Gets;
using SAPbobsCOM;

namespace Client.Lib.SalesQuotation
{
    public class Cancel
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string DocEntry => _DocEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _DocEntry;
        public Cancel(int DocEntry, Company _company)
        {
            _Cancel(DocEntry, _company);
        }
        private void _Cancel(int DocEntry, Company _company)
        {
            #region Cancel Sales Quotation
            int RetVal = -1;
            Documents SalesQuotation = (Documents)_company.GetBusinessObject(BoObjectTypes.oQuotations);
            if (!SalesQuotation.GetByKey(DocEntry))
            {
                _company.GetLastError(out _ErroreCode, out _MessageErrore);
                _ErroreCode = 141;
                _MessageErrore = "Invalid DocEntry";
            }
            var condition = QueryData.ConvertDataTable<GetStatusSalesQuotation>(new GetRecordByDataTable(
                             "GetStatusCancelSalesQuotation",
                             DocEntry.ToString(),
                             "",
                             "",
                             "",
                             "").GetResultDataTable()).FirstOrDefault();
            if (condition.Status == "Cancel") RetVal = SalesQuotation.Cancel(); else RetVal = SalesQuotation.Close();
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
            #endregion
        }
    }
}
