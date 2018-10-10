using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace GMSDashboard.Scheduler
{
    public static class JobScheduler
    {
        public static async Task Start()
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            var factory = new StdSchedulerFactory(props);
            var scheduler = await factory.GetScheduler();
            await scheduler.Start();

            var checkJob = JobBuilder.Create<CheckJob>().Build();

            var checkTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(checkJob, checkTrigger);
        }

        public static DateTimeOffset GetNextStartingStime()
        {
            var timeOfDay = DateTime.Now.TimeOfDay;
            var nextFullHour = TimeSpan.FromHours(Math.Ceiling(timeOfDay.TotalHours));
            return DateTimeOffset.Parse(nextFullHour.ToString());
        }
    }
}
