using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace webapi1.Filters
{
    public class ActionInfoAttribute : ActionFilterAttribute
    {
        private string actionName;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionName = actionContext.ActionDescriptor.ActionName;
            Trace.WriteLine($"{actionName} is starting...");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Trace.WriteLine($"{actionName} ended...");
        }

    }
}