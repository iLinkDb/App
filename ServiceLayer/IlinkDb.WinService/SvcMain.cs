using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Reflection;

using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

using AppCommon;

namespace IlinkDb.WinService
{
    public partial class SvcMain : ServiceBase
    {
        // Default the timer startup interval to xx milliseconds.
        private const int TIMER_STARTUP_INTERVAL = 5000;

        public HttpSelfHostServer _restServiceHost = null;
        private readonly HttpSelfHostConfiguration _config;
        private const int _restServicePort = 4000;

        private readonly Timer _timer = new Timer();
        private int _lastTimerHeartbeatMinute = 0;

        private bool _servicesInitialized = false;

        public static void Main(string[] args)
        {
            //http://stackoverflow.com/questions/1195478/how-to-make-a-net-windows-service-start-right-after-the-installation/1195621#1195621
            if (args.Length == 0)
            {
                // Run your service normally.
                ServiceBase[] ServicesToRun = new ServiceBase[] { new SvcMain() };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                ArgHelper argHelper = new ArgHelper(args);

                string serviceName = "IlinkDbMain";
                string version = assembly.GetName().Version.ToString();

                string[] serviceArgs =
                    {
                        "SERVICENAME=" + serviceName
                        , "DESCRIPTION=IlinkDb Windows Service (" + version + ")"
                       };

                if (argHelper["i"] != null || argHelper["install"] != null)
                {
                    WinServiceHelper.InstallService(typeof(SvcMain), serviceArgs, serviceName);
                }
                else if (argHelper["u"] != null || argHelper["uninstall"] != null)
                {
                    WinServiceHelper.StopService(serviceName);
                    WinServiceHelper.UninstallService(typeof(SvcMain), serviceArgs, serviceName);
                }

                if (argHelper["start"] != null)
                { WinServiceHelper.StartService(serviceName); }
                else if (argHelper["stop"] != null)
                { WinServiceHelper.StopService(serviceName); }

            }
        }

        public SvcMain()
        {
            InitializeComponent();

            Uri baseAddress = new Uri(string.Format("http://localhost:{0}/", _restServicePort));

            _config = new HttpSelfHostConfiguration(baseAddress.ToString());
            // _config.Routes.MapHttpRoute("IlinkDbRest", "{controller}/{action}/{id}", new { id = RouteParameter.Optional });

            _config.Routes.MapHttpRoute(
                name: "SvcDefault",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "V1", action = "Version", id = RouteParameter.Optional });


            // _config.Routes.MapHttpRoute("IlinkDbRest", "{controller}/{action}/{id}", new { id = RouteParameter.Optional });
        }

        protected override void OnStart(string[] args)
        {

            Logging.LogInfo("OnStart Fired");

            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Interval = TIMER_STARTUP_INTERVAL;

            Logging.LogTrace(string.Format("Service will Initialize in {0} seconds.", (_timer.Interval / 1000)));

            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            _restServiceHost.CloseAsync().Wait();
            _restServiceHost.Dispose();
            Logging.LogInfo("OnStop fired for Version: " + Computer.GetVersion());
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool restartTimer = true;
            _timer.Enabled = false;

            try
            {
                ProcessTimerEvent();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }
            finally
            {
                // No matter what happens in the ProcessQueue, we want our
                // timer to be enabled when we are done, if we don't have
                // a CancellationPending.
                _timer.Enabled = restartTimer;
            }
        }

        private void ProcessTimerEvent()
        {
            if (_servicesInitialized)
            {
                // Do some work here...
            }
            else
            {
                InitializeService();
            }

            // Check to see if the minute changes.
            if (DateTime.Now.Minute != _lastTimerHeartbeatMinute)
            {
                _lastTimerHeartbeatMinute = DateTime.Now.Minute;

                // Check for the "every five minutes" sign of life.
                if (DateTime.Now.Minute % 5 == 0)
                {
                    Logging.LogTrace("Timer heartbeat event fired");
                }
            }
        }

        private void InitializeService()
        {
            Logging.LogDebug("Initializing Service for Version: " + Computer.GetVersion());
            Logging.LogDebug("UniqueComputerKey: " + Computer.UniqueComputerKey());
            Logging.LogDebug("MachineName: " + Environment.MachineName);
            Logging.LogDebug(string.Format("LocalIpv4Address: {0}", Computer.LocalIpv4Address));

            ConfigureWcfRestService("IlinkDbMain");

            _timer.Interval = 900;

            _servicesInitialized = true;
            Logging.LogTrace("Started Assembly: " + Computer.GetFullName());
            Logging.LogTrace("ServicesInitialized: " + _servicesInitialized);
        }

        private bool ConfigureWcfRestService(string service)
        {
            bool retVal = false;

            // Excellent overview of configuring WCF (MaxConcurrentSessions and such)
            // http://www.iserviceoriented.com/blog/post/Configuring+Performance+Options+-+WCF+Gotcha+3.aspx
            // ConfigureWcfService("Bed", typeof(BedService), typeof(IBedService));
            try
            {
                Logging.LogDebug("Initializing Rest Service @ " + _config.BaseAddress);

                // Set our own assembly resolver where we add the assemblies we need
                AssembliesResolver assemblyResolver = new AssembliesResolver();
                _config.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);

                _restServiceHost = new HttpSelfHostServer(_config);

                // Start the service.
                Logging.LogDebug("Opening Rest Service: " + service);
                _restServiceHost.OpenAsync().Wait();

                retVal = true;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex.Message, ex);
                _timer.Interval = 10000;   // Setting this for 10 seconds before trying again
                Logging.LogTrace(string.Format("Changing Timer Interval to {0} seconds", _timer.Interval / 1000));
            }
            finally
            {
                if (!retVal)
                {
                    _restServiceHost.CloseAsync().Wait();
                    _restServiceHost.Dispose();
                }
            }
            //if (_restServiceHost.State == System.ServiceModel.CommunicationState.Opened)
            //{
            //    Logging.LogDebug(string.Format("Service: {0} successfully opened", service));
            //}

            return retVal;
        }

    }

    class AssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);

            assemblies.Add(Assembly.LoadFrom(@"E:\AbcDev\iLinkDb\App\ServiceLayer\IlinkDb.Service.Controllers\bin\Debug\IlinkDb.Service.Controllers.dll"));

            return assemblies;
        }
    }
}
