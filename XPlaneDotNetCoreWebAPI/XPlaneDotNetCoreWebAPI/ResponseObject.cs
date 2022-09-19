namespace XPlaneDotNetCoreWebAPI
{
    public class ResponseObject<T> : IResponseObject
    {
        public bool HasError { get; set; }
        public bool ShowUser { get; set; }
        public string Message { get; set; }
        public T ReturnObject { get; set; }
        public string messageUniqueID { get; set; }
        public ResponseObject<T> Response { get; set; }
    }
    public class ResponseObject : ResponseObject<object>
    {

    }

    public interface IResponseObject
    {
        public bool HasError { get; set; }
        public bool ShowUser { get; set; }
        public string Message { get; set; }
    }
}
