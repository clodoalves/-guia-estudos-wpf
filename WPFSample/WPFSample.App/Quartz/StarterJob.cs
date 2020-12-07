using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using Microsoft.Practices.Unity;
using WPFSample.App.Quartz.Job;
using WPFSample.App.Quartz.Jobs;

namespace WPFSample.App.Quartz
{
    public class StarterJob
    {
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public StarterJob(IUnityContainer unityContainer)
        {
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new JobFactory(unityContainer);
            _scheduler.Start();
        }

        public void TimeoutTrigger() 
        {
            ITrigger trigger = new CronTriggerImpl("timeout-trigger", "notification", "0 0 0/2 ? * * *");

            IJobDetail jobDetail = new JobDetailImpl("notification-timeout", "notification", typeof(TimeoutJob));

            _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void UnlockWindowsTrigger() 
        {
            ITrigger trigger = new SimpleTriggerImpl("trigger-unlock-windows", "notification", new DateTimeOffset(DateTime.Now));

            IJobDetail jobDetail = new JobDetailImpl("notification-unlock", "notification", typeof(UnlockWindowsJob));

            _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void DarkModeChangeTrigger() 
        {
            ITrigger trigger = new SimpleTriggerImpl("trigger-dark-mode", "notification", new DateTimeOffset(DateTime.Now));

            IJobDetail jobDetail = new JobDetailImpl("notification-dark-mode", "notification", typeof(DarkModeChangeJob));

            _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void UpdateQuantityProductsTrigger()
        {
            ITrigger trigger = new CronTriggerImpl("trigger-update-quantity", "notification", "0 59 14 * * ?");

            IJobDetail jobDetail = new JobDetailImpl("notification-update", "notification", typeof(UpdateQuantityProductJob));

            jobDetail.JobDataMap["quantity"] = 10;

            _scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
