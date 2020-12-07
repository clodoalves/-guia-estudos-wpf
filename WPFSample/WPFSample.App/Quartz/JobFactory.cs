using Microsoft.Practices.Unity;
using Prism.Unity;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Service.Contract;

namespace WPFSample.App.Quartz
{
    public class JobFactory : IJobFactory
    {
        private readonly IUnityContainer _container;

        public JobFactory(IUnityContainer container)
        {
            this._container = container;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)_container.Resolve(jobDetail.JobType);

            return job;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
