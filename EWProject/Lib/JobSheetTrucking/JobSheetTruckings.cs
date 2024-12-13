using Client.Models.Gets;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.JobSheetTrucking
{
    public class JobSheetTruckings
    {
        public static Update _Update(GetJobSheetTruckingEdit updateJobSheetTruckingEdit, Company _company) => new Update(updateJobSheetTruckingEdit, _company);
        public static Cancel _Cancel(int DocEntry, int docEntrySO, string updateBy, Company _company) => new Cancel(DocEntry, docEntrySO, updateBy, _company);
        public static UpdateAttachment _UpdateAttachment(GetJobSheetTruckingEdit updateJobSheetTruckingEdit, Company _company) => new UpdateAttachment(updateJobSheetTruckingEdit, _company);
        public static UpdateDraft _UpdateDraft(UpdateDraftJobSheetTrucking updateJobSheetTruckingEdit, string draftDocument, Company _company) => new UpdateDraft(updateJobSheetTruckingEdit, draftDocument, _company);
    }
}
