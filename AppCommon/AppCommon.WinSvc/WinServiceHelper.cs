using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Microsoft.Win32;

namespace AppCommon
{
    public class WinServiceHelper
    {
        public static bool IsInstalled(string serviceName)
        {
            using (ServiceController controller = new ServiceController(serviceName))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static void StopService(string serviceName)
        {
            if (!IsInstalled(serviceName))
            { return; }

            using (ServiceController controller = new ServiceController(serviceName))
            {
                if (controller.Status != ServiceControllerStatus.Stopped)
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                }
            }
        }

        public static bool IsRunning(string serviceName)
        {
            using (ServiceController controller = new ServiceController(serviceName))
            {
                if (!WinServiceHelper.IsInstalled(serviceName))
                { return false; }
                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        public static string EnsureWatchDogRunning()
        {
            string retVal = "";
            try
            {
                ServiceController watchDogSvc = new ServiceController();
                watchDogSvc.ServiceName = "IlinkDbWatchDog";

                retVal = "IlinkDbWatchDog prior status: " + watchDogSvc.Status;

                if (watchDogSvc.Status != ServiceControllerStatus.Running)
                {
                    bool autoStart = IsAutoStart(watchDogSvc.ServiceName);

                    if (autoStart)
                    {
                        retVal += ", Starting WatchDog";
                        watchDogSvc.Start();
                    }
                    else
                    {
                        retVal += ", Skipping WatchDog (not AutoStart)";
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = "ERROR: " + ex.Message;
            }
            return retVal;
        }

        private static AssemblyInstaller GetInstaller(Type type)
        {
            // AssemblyInstaller installer = new AssemblyInstaller(typeof(IlinkDbWatchDog).Assembly, null);
            AssemblyInstaller installer = new AssemblyInstaller(type.Assembly, null);
            installer.UseNewContext = true;
            return installer;
        }

        public static void UninstallService(Type type, string[] args, string serviceName)
        {
            if (!WinServiceHelper.IsInstalled(serviceName))
            { return; }

            using (AssemblyInstaller installer = GetInstaller(type))
            {
                IDictionary state = new Hashtable();

                installer.CommandLine = args;

                installer.Uninstall(state);
            }
        }

        public static void StartService(string serviceName)
        {
            if (!WinServiceHelper.IsInstalled(serviceName))
            { return; }

            using (ServiceController controller = new ServiceController(serviceName))
            {
                if (controller.Status != ServiceControllerStatus.Running)
                {
                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                }
            }
        }

        public static void InstallService(Type type, string[] args, string serviceName)
        {
            if (WinServiceHelper.IsInstalled(serviceName))
            { return; }

            using (AssemblyInstaller installer = GetInstaller(type))
            {
                IDictionary state = new Hashtable();
                try
                {
                    installer.CommandLine = args;
                    installer.Install(state);
                    installer.Commit(state);
                }
                catch (Exception)
                {
                    installer.Rollback(state);
                    throw;
                }
            }
        }

        public static bool IsAutoStart(string serviceName)
        {
            const string startupKeyName = "Start";
            const string startupType_Automatic = "2";
            // const string startupType_Manual = "3";

            bool retVal = false;

            string key = "SYSTEM\\CurrentControlSet\\Services\\" + serviceName;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key, false);
            if (registryKey != null)
            {
                object regValue = registryKey.GetValue(startupKeyName);
                if (regValue != null)
                {
                    retVal = regValue.ToString() == startupType_Automatic;
                }
            }

            return retVal;
        }

    }
}
