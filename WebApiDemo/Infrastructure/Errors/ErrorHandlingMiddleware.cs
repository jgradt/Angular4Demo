using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiDemo.Infrastructure.Errors
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is CrudDataException cde)
            {
                var errors = new Dictionary<string, string[]>();

                switch (cde.StatusCode)
                {
                    case CrudStatusCode.UpdateItemNotFound:
                    case CrudStatusCode.DeleteItemNotFound:

                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                        errors.Add("_", new[] { "Unable to process request because item does not exist on the server" });
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new { errors },
                            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        await context.Response.WriteAsync(result);

                        break;
                }
                
            }
            else
            {
                var errors = new Dictionary<string, string[]>();

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (!string.IsNullOrWhiteSpace(exception.Message))
                {
                    errors.Add("_", new[] { exception.Message });
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new { errors }, 
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    await context.Response.WriteAsync(result);
                }
            }

        }
    }

}
