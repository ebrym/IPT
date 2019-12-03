using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using IPT.Infrastructure.Interface;

namespace IPT.Infrastructure.Middlewares
{ 
    public class HangFireMiddleWare
    {
        private readonly RequestDelegate next;
       /// private static ILogger logger;
        public HangFireMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IBackgroundService backgroundService)
        {
            //logger = loggerFactory.CreateLogger("Recurring job scheduler");
            // Log.Information("Recurring job scheduler");
            try
            {
                RecurringJob.AddOrUpdate(() => backgroundService.ProcessDeductions(), "*/5 * * * *");
                RecurringJob.AddOrUpdate(() => backgroundService.ProcessNewOnBaording(), "*/2 * * * *");
                RecurringJob.AddOrUpdate(() => backgroundService.ProcessSubscription(), "*/5 * * * *");
                

                ////RecurringJob.AddOrUpdate(() => _ZenithJob.UpdatePendingAccountStatus(), Cron.MinuteInterval(1));
                ////RecurringJob.AddOrUpdate(() => _ZenithJob.TruncateStaticFields(), Cron.DayInterval(1));
                ////RecurringJob.AddOrUpdate(() => _ZenithJob.AccountPendingPush(), Cron.MinuteInterval(1));

                //RecurringJob.AddOrUpdate("Sync Downstream", () => syncService.SyncDown(), Cron.MinuteInterval(4));
                //RecurringJob.AddOrUpdate("Update demand notice table", () => runDemandNoticeService.ReconcileDemandNotice(), Cron.MinuteInterval(4));
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message, "Scheduler exception");
                // Log.Error(ex.Message, "Scheduler exception");
            }
            await next(context);
        }
    }
}
