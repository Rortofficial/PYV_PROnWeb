namespace Client.Models.Gets
{
    public class GetProjectManagmentList
    {
        public string ProjectCode { get; set; } = null!;
        public string ProjectName { get; set; } = null!;
        public string DocEntryJobSheet { get; set; } = null!;
        public string Active { get; set; } = null!;
        public string AbsEntry { get; set; }
    }
}
