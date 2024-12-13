using Client.Models.Gets;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTruckingSeaAir
{
    public class JobSheetTruckingSeaAirs
    {
        public static Add _Add(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string draftdocument, string CostCenter, Company _company) => new Add(postJobSheetTruckingSeaAirRequestAirAndSea,draftdocument, CostCenter, _company);
        public static UpdateDraft _UpdateDraft(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string draftdocument,string jobSheetDocEntry, string CostCenter, Company _company) => new UpdateDraft(postJobSheetTruckingSeaAirRequestAirAndSea,draftdocument, jobSheetDocEntry, CostCenter, _company);
        public static Update _Update(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingSeaAirRequestAirAndSea, string jobDocEntry, string CostCenter, Company _company) => new Update(postJobSheetTruckingSeaAirRequestAirAndSea, jobDocEntry,CostCenter, _company);
        public static Cancel _Cancel(int jobDocEntry,int docEntrySO,List<GetPurchaseOrderCancelJobSheetSeaAir> docEntryPO,string updateBy, Company _company) => new Cancel(jobDocEntry,docEntrySO,docEntryPO, updateBy, _company);
    }
}
