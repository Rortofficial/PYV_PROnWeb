using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Customer
{
    public class Customers
    {
        public static Add _AddCustomer(PostSalesQuotationRequest postSalesQuotationRequest, Company _company) => new Add(postSalesQuotationRequest, _company);
        public static AddContactPerson _AddContactPersion(ContactPerson ContactPersons, Company _company, string cardCode) => new AddContactPerson(ContactPersons, _company, cardCode);
    }
}
