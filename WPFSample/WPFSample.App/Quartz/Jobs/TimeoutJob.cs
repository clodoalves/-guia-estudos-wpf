using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.App.Quartz.Job
{
    public class TimeoutJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Timeout! The wallpaper will be changed");
            Console.ReadLine();

            return Task.CompletedTask;
        }
    }
}
