using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Infrastructure.Errors
{
    public class CrudDataException : Exception
    {
        public CrudStatusCode StatusCode { get; set; }

        public CrudDataException(CrudStatusCode statusCode, string message = null) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public enum CrudStatusCode
    {
        UpdateItemNotFound,
        DeleteItemNotFound
    }
}
