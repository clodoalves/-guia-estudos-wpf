using Microsoft.Win32;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WPFSample.App.Quartz.Jobs
{
    public class DarkModeChangeJob : IJob
    {
        private const string REGISTRY_KEY_PATH = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string REGISTRY_VALUE_NAME = "SystemUsesLightTheme";
        private WindowsTheme _currentTheme { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            _currentTheme = GetWindowsTheme();

            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(ChangeToDarkMode);

            return Task.CompletedTask;
        }
   
        private void ChangeToDarkMode(object sender, UserPreferenceChangedEventArgs e)
        {
            WindowsTheme selectedTheme = GetWindowsTheme();

            if (_currentTheme != selectedTheme)
            {
                _currentTheme = selectedTheme;

                if (_currentTheme == WindowsTheme.Dark) 
                {
                    Console.WriteLine("Dark theme selected");
                    Console.ReadLine();
                }
            }

        }

        private WindowsTheme GetWindowsTheme()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH))
            {
                object registryValueObject = key?.GetValue(REGISTRY_VALUE_NAME);

                if (registryValueObject == null)
                {
                    return WindowsTheme.Light;
                }

                int registryValue = (int)registryValueObject;

                return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
            }
        }

        public DarkModeChangeJob()
        {
      
        }
    }

    internal enum WindowsTheme
    {
        Light,
        Dark
    }
}