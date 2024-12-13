using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ResponseData<T> where T : class
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; }
    }
}
