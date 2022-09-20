using System;
using System.Runtime.Serialization;

namespace XPlaneDotNetCoreWebAPI
{
    [Serializable]
    public class CovisartException : Exception
    {
        public CovisartException()
        {
        }
        public IResponseObject responseObject;
        public CovisartException(IResponseObject ResponseObject) : base(ResponseObject.Message)
        {
            responseObject = ResponseObject;
        }

        public CovisartException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CovisartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
