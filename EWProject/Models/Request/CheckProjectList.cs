namespace Client.Models.Request
{
    public class CheckProjectList
    {
        public int Count { get; set; }
        public string ProjectManagment { get; set; }
        public List<int> Lines { get; set; }
    }
}
