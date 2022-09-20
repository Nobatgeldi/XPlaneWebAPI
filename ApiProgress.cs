namespace XPlaneDotNetCoreWebAPI
{
    public class ApiProgress
    {
        public bool ShowUser { get; set; } = true;
        public bool HasError { get; set; }
        public string Message { get; set; }
        public object ReturnObject { get; set; }

        public static ApiProgress Error(string message, bool ShowUser)
        {
            return new ApiProgress
            {
                ShowUser = ShowUser,
                HasError = true,
                Message = message
            };
        }

        public static ApiProgress Error(CovisartException ex)
        {
            return Error(ex.Message, ex.responseObject?.ShowUser ?? true);
        }

        public static ApiProgress Error(Exception ex, bool ShowUser)
        {
            return Error(ex.Message, ShowUser);
        }

        public static ApiProgress Success(object returnObject, string message = null)
        {
            return new ApiProgress
            {
                ReturnObject = returnObject,
                Message = message
            };
        }

        public static ApiProgress TokenError()
        {
            return new ApiProgress
            {
                HasError = true,
                Message = "AccessToken bulanamadı. Tekrar giriş yapınız."
            };
        }

        public static ApiProgress Success()
        {
            return new ApiProgress
            {
                Message = "Success",
            };
        }


    }
}
