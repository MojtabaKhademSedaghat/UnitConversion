using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zino.Core.DI;

namespace Zino.Service.DTOs
{
    public class ErrorDTO
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class ErrorManagment
    {
        public static ActionResult CustombadRequest(Exception exception)
        {
            var logger = GetLogger();
            logger.LogError(exception, exception.ToString());
            var message = GetErrorText(exception);
            return new BadRequestObjectResult(new ErrorDTO() { Code = "400368", Message = message, Status = 400 });
        }
        
        private static ILogger GetLogger()
        {
            var loggerFactory = ServiceLocator.Current.GetInstance<ILoggerFactory>();
            return loggerFactory.CreateLogger("EntityFrameworkCoreExtensions");
        }

        public static string GetErrorText(Exception ex)
        {
            string message = "";
            message += ex.Message + "</br>";
            message += ((ex.InnerException != null) ? ex.InnerException.Message + "</br>" : "");
            message += ((ex.InnerException != null && ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : "");

            return message;
        }
    }
}
