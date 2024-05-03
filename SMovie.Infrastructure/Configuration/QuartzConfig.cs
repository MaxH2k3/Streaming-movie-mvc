using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SMovie.Application.Service.QuartzTask;
using SMovie.Domain.Models;

namespace SMovie.Infrastructure.Configuration
{
    public static class QuartzConfig
    {
        public static void AddQuartzConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddQuartz(q =>
            {
                var quartzSetting = configuration.GetSection("QuartzSetting").Get<QuartzSetting>();

                q.SchedulerId = quartzSetting.SchedulerId;
                q.SchedulerName = quartzSetting.SchedulerName;
                //q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = quartzSetting.MaxConcurrency;
                });
                q.MaxBatchSize = quartzSetting.MaxBatchSize;
                q.MisfireThreshold = TimeSpan.FromSeconds(quartzSetting.MisfireThreshold);
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
                options.AwaitApplicationStarted = true;
            });

            services.ConfigureOptions<CreateQuartzTask>();
            services.AddSingleton<StdSchedulerFactory>();

        }
    }
}
