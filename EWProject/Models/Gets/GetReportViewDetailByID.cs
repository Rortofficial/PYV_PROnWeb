namespace Client.Models.Gets
{
    public class GetReportViewDetailByID
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string ReportFile { get; set; }
        public List<ReportViewDetailLine> Lines { get; set; }
    }
    public class ReportViewDetailLine
    {
        public string TitleName { get; set; }
        public string ObjectID { get; set; }
        public string ObjectType { get; set; }
        public string Query { get; set; }
    }
}
