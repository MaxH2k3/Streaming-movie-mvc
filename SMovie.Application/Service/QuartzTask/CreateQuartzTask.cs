using Microsoft.Extensions.Options;
using Quartz;
using SMovie.Domain.Constants;

namespace SMovie.Application.Service.QuartzTask
{
    public class CreateQuartzTask : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobkey = JobKey.Create(TaskConstant.TransferMovie.JobKey);
            options.AddJob<TransferMovie>(jobBuilder => jobBuilder.WithIdentity(jobkey))
                .AddTrigger(trigger => trigger.ForJob(jobkey)
                    .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Friday, 0, 0)
                        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById(SystemConstant.TimeArea)))
                    .WithDescription(TaskConstant.TransferMovie.Description)
                    .Build());
        }
    }
}
