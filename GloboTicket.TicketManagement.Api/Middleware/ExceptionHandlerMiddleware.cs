using System;
using System.Net;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GloboTicket.TicketManagement.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ConvertException(context, e);
            }
        }

        private async Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.ValidationErrors);
                    break;                
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;                
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;                
                case Exception ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int) httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new {error = exception.Message});
            }

            await context.Response.WriteAsync(result);
        }
    }
}