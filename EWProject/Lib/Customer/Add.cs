using Client.Connection;
using Client.Lib.OtherFunction;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Customer
{
    public class Add
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public string CustomerCode => _CustomerCode;
        public int ContactPersonID => _ContactPersonID;
        public string AddressID => _AddressID;
        private string _MessageErrore;
        private int _ErroreCode;
        private string _CustomerCode;
        private int _ContactPersonID;
        private string _AddressID;
        public Add(PostSalesQuotationRequest postSalesQuotationRequest, Company _company)
        {
            _Add(postSalesQuotationRequest, _company);
        }
        private void _Add(PostSalesQuotationRequest postSalesQuotationRequest, Company _company)
        {
            try
            {
                BusinessPartners oBusinessPartner = null!;
                oBusinessPartner = (BusinessPartners)_company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                oBusinessPartner.Series = Configure.PrefixLead;
                //oBusinessPartner.CardCode = customerPostRequest.CardCode;
                oBusinessPartner.CardName = postSalesQuotationRequest.CustomerCode;
                oBusinessPartner.CardType = BoCardTypes.cLid;
                oBusinessPartner.Currency = postSalesQuotationRequest.BPCurrency;
                oBusinessPartner.Phone1 = postSalesQuotationRequest.Tel;
                oBusinessPartner.EmailAddress = postSalesQuotationRequest.Email;
                oBusinessPartner.GroupCode = Configure.CustomerGroup;
                oBusinessPartner.SalesPersonCode = Convert.ToInt32(postSalesQuotationRequest.SaleEmployee);
                //oBusinessPartner.ContactEmployees.Add();
                oBusinessPartner.ContactEmployees.Name = postSalesQuotationRequest.ContactPersons.LastName;
                oBusinessPartner.ContactEmployees.LastName = postSalesQuotationRequest.ContactPersons.LastName;
                oBusinessPartner.ContactEmployees.FirstName = postSalesQuotationRequest.ContactPersons.FirstName;
                oBusinessPartner.ContactEmployees.Phone1 = postSalesQuotationRequest.Tel;
                oBusinessPartner.ContactEmployees.E_Mail = postSalesQuotationRequest.Email;
                var CheckingStringMorethan50Lenght = ClearEmptyString.SplitByLength(postSalesQuotationRequest.AddressDetail, 50).ToList();
                switch (CheckingStringMorethan50Lenght.Count())
                {
                    case 1:
                        oBusinessPartner.Addresses.AddressName = CheckingStringMorethan50Lenght.FirstOrDefault();
                        break;
                    case 2:
                        oBusinessPartner.Addresses.AddressName = CheckingStringMorethan50Lenght[0];
                        oBusinessPartner.Addresses.AddressName2 = CheckingStringMorethan50Lenght[1];
                        break;
                    case 3:
                        oBusinessPartner.Addresses.AddressName = CheckingStringMorethan50Lenght[0];
                        oBusinessPartner.Addresses.AddressName2 = CheckingStringMorethan50Lenght[1];
                        oBusinessPartner.Addresses.AddressName3 = CheckingStringMorethan50Lenght[2];
                        break;
                    default:
                        _ErroreCode = -1;
                        _MessageErrore = "Address More than 150 Lenght";
                        return;
                }
                oBusinessPartner.Addresses.AddressType = BoAddressType.bo_ShipTo;
                oBusinessPartner.Addresses.Add();
                //oBusinessPartner.Address = CheckingStringMorethan50Lenght.FirstOrDefault();
                //contactPersonId= oBusinessPartner.ContactEmployees.InternalCode;
                //oBusinessPartner.ContactEmployees.Add();
                _ContactPersonID = oBusinessPartner.ContactEmployees.InternalCode;
                _AddressID = oBusinessPartner.Address;
                int cusRetval = oBusinessPartner.Add();
                if (cusRetval != 0)
                {
                    _company.GetLastError(out _ErroreCode, out _MessageErrore);
                }
                else
                {
                    _ErroreCode = 0;
                    _MessageErrore = "";
                    _CustomerCode = _company.GetNewObjectKey();
                }
            }
            catch (Exception ex)
            {
                _ErroreCode = ex.GetHashCode();
                _MessageErrore = "Invalid Customer";
            }

        }

    }
}
