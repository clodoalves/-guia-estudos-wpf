using Microsoft.Win32;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.App.Quartz.Job
{
    public class UnlockWindowsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEventsSessionUnlock);

            return Task.CompletedTask;
        }

        private void SystemEventsSessionUnlock(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Console.WriteLine("Unlock computer");
                Console.ReadLine();
            }
        }
    }
}
