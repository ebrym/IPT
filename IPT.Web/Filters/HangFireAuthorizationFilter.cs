using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace IPT.Web.Filters
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {

   
        public bool Authorize(DashboardContext context)
        {
          
            // In case you need an OWIN context, use the next line, `OwinContext` class
            // is the part of the `Microsoft.Owin` package.
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            // return httpContext.User.Identity.IsAuthenticated;
           
            return true;
           
        }
    }
}
