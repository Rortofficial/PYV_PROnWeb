using Client.Connection;
using Client.Repository;

namespace Client.Installer
{
    public class InstallerDependencyInjection : IInstaller
    {
        public void Installer(IServiceCollection servieCollection, IConfiguration configuration)
        {
            servieCollection.AddSingleton<IBookingSheet, BookingSheet>();
            servieCollection.AddSingleton<IConfirmBookingSheet, ConfirmBookingSheet>();
            servieCollection.AddSingleton<ISalesQuotation, SalesQuotation>();
            servieCollection.AddSingleton<IJobSheetTrucking, JobSheetTrucking>();
            servieCollection.AddSingleton<IPurchaseRequest, PurchaseRequest>();
            servieCollection.AddSingleton<IPettyCash, PettyCash>();
            servieCollection.AddSingleton<IReportLayout, ReportLayout>();
            servieCollection.AddSingleton<IAdvancePayment, AdvancePayment>();
            servieCollection.AddSingleton<IApproveDocument, ApproveDocument>();
            servieCollection.AddSingleton<ICreditNote, CreditNote>();
            servieCollection.AddSingleton<ISettlement, Settlement>();
            servieCollection.AddSingleton<IPurchaseRequestForCommission, PurchaseRequestForCommission>();
            servieCollection.AddSingleton<IDeditNote, DeditNote>();
            servieCollection.AddSingleton<IAlert, Alert>();
            servieCollection.AddSingleton<IReport, Report>();
            servieCollection.AddSingleton<IContainer, Container>();
            servieCollection.AddSingleton<IReimbursement, Reimbursement>();
            servieCollection.AddSingleton<IBookingSheetSeaAndAir, BookingSheetSeaAndAir>();
            servieCollection.AddSingleton<IConfirmBookingSeaAndAir, ConfirmBookingSeaAndAir>();
            servieCollection.AddSingleton<IJobSheetTruckingSeaAndAir, JobSheetTruckingSeaAndAir>();
            servieCollection.AddSingleton<ICreditSeaAir, CreditSeaAir>();
            servieCollection.AddTransient<Configure>();
        }
    }
}
