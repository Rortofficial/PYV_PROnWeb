using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Customer
{
    public class AddContactPerson
    {
        public string ErroreMessage => _MessageErrore;
        public int ErroreCode => _ErroreCode;
        public int DocEntry => _docEntry;
        private string _MessageErrore;
        private int _ErroreCode;
        private int _docEntry;
        public AddContactPerson(ContactPerson ContactPersons, Company _company, string cardCode)
        {
            _Add(ContactPersons, _company, cardCode);
        }
        private void _Add(ContactPerson ContactPersons, Company _company, string cardCode)
        {
            try
            {
                BusinessPartners oBusinessPartner = null!;
                oBusinessPartner = (BusinessPartners)_company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                if (oBusinessPartner.GetByKey(cardCode))
                {
                    //oBusinessPartner.ContactEmployees.Add();
                    oBusinessPartner.ContactEmployees.Name = ContactPersons.LastName + "-" + ContactPersons.FirstName;
                    oBusinessPartner.ContactEmployees.LastName = ContactPersons.LastName;
                    oBusinessPartner.ContactEmployees.FirstName = ContactPersons.FirstName;
                    oBusinessPartner.ContactEmployees.Phone1 = ContactPersons.Phone;
                    oBusinessPartner.ContactEmployees.E_Mail = ContactPersons.Email;
                    _docEntry = oBusinessPartner.ContactEmployees.InternalCode;
                    int Retval;
                    Retval = oBusinessPartner.Update();
                    if (Retval != 0)
                    {
                        _company.GetLastError(out _ErroreCode, out _MessageErrore);
                    }
                    else
                    {
                        _ErroreCode = 0;
                        _MessageErrore = "Successs";
                    }
                }
                else
                {
                    _ErroreCode = 141;
                    _MessageErrore = "Invalid Customer";
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
