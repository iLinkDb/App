using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Text;

namespace IlinkDb.WinService
{
    [RunInstaller(true)]
    public class CustomInstaller : Installer
    {
        private ProjectInstallerCmdVars _params = new ProjectInstallerCmdVars();
        public InstallContext myInstallContext;

        private System.ComponentModel.IContainer components = null;

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;

        //private string[] _dependsOn;

        //public string[] DependsOn
        //{
        //    get { return _dependsOn; }
        //    set { _dependsOn = value; }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public override void Install(IDictionary stateSaver)
        {
            //            WriteEventLog("Install");
            //CustomParameters customParameters = new CustomParameters(this.Context);

            //SaveCustomParametersInStateSaverDictionary(
            //                stateSaver, customParameters);

            //foreach (string one in this.Context.Parameters.Keys)
            //{
            //    string msg = "P: " + one.ToUpper();
            //    MessageBox.Show("Param Count: " + msg);
            //}

            SetParameters();
            Execute();

            //            WriteEventLog("Before base Install");

            base.Install(stateSaver);
        }

        public override void Uninstall(
               System.Collections.IDictionary savedState)
        {
            // Get the custom parameters from the saved state.
            //CustomParameters customParameters =
            //        new CustomParameters(savedState);

            //MessageBox.Show("The application is being uninstalled.");
            SetParameters();
            Execute();

            base.Uninstall(savedState);
        }

        //private void SaveCustomParametersInStateSaverDictionary(
        //        System.Collections.IDictionary stateSaver,
        //        CustomParameters customParameters)
        //{
        //    // Add/update the "MyCustomParameter" entry in the
        //    // state saver so that it may be accessed on uninstall.
        //    if (stateSaver.Contains(CustomParameters.Keys.MyCustomParameter) == true)
        //        stateSaver[CustomParameters.Keys.MyCustomParameter] =
        //                          customParameters.MyCustomParameter;
        //    else
        //        stateSaver.Add(CustomParameters.Keys.MyCustomParameter,
        //                       customParameters.MyCustomParameter);

        //    // Add/update the "MyOtherCustomParameter" entry in the
        //    // state saver so that it may be accessed on uninstall.
        //    if (stateSaver.Contains(
        //             CustomParameters.Keys.MyOtherCustomParameter) == true)
        //        stateSaver[CustomParameters.Keys.MyOtherCustomParameter] =
        //                   customParameters.MyOtherCustomParameter;
        //    else
        //        stateSaver.Add(CustomParameters.Keys.MyOtherCustomParameter,
        //                       customParameters.MyOtherCustomParameter);
        //}


        //public CustomInstaller(string[] dependsOn)
        //{
        //    //            WriteEventLog("utcInstall with DependsOn");
        //    DependsOn = dependsOn;
        //    SetParameters();
        //}

        public CustomInstaller()
        {
            //            WriteEventLog("utcInstall");
            SetParameters();
        }

        private void Execute()
        {
            // Make sure values were supplied.
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(_params.ServiceName))
            {
                sb.AppendLine("ServiceName Parameter missing");
            }
            if (string.IsNullOrEmpty(_params.Description))
            {
                sb.AppendLine("Description Parameter missing");
            }
            if (string.IsNullOrEmpty(_params.DependsOn))
            {
                Console.WriteLine("DependsOn Parameter missing, not critical...");
            }
            if (string.IsNullOrEmpty(_params.DisplayName))
            {
                _params.DisplayName = _params.ServiceName;
                Console.WriteLine("DisplayName param not found, defaulting to ServiceName");
            }
            if (sb.Length > 0)
            {
                Console.WriteLine();
                Console.WriteLine("*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!");
                Console.WriteLine("ERROR: ");
                Console.WriteLine(sb.ToString());
                Console.WriteLine("*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!");
                Console.WriteLine();
                throw new ArgumentNullException("Missing Parameters", sb.ToString());
            }

            // InitializeComponent();
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = _params.Description;
            this.serviceInstaller1.DisplayName = _params.DisplayName;
            this.serviceInstaller1.ServiceName = _params.ServiceName;

            if (! string.IsNullOrEmpty(_params.DependsOn))
            {
                string[] depends = _params.DependsOn.Split(',');
                this.serviceInstaller1.ServicesDependedOn = depends;
            }

            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;



            //if ((DependsOn != null) && (DependsOn.Length > 0))
            //{
            //    this.serviceInstaller1.ServicesDependedOn = DependsOn;
            //}

            //else if (_params.ServiceName.ToUpper() != "UTCMAIN")
            //{
            //    string[] addDepend = {"UtcMain"};
            //    this.serviceInstaller1.ServicesDependedOn = addDepend;
            //}

            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1});
        }

        private void SetParameters()
        {
            if (this.Context != null && this.Context.Parameters != null && this.Context.Parameters.Count > 0)
            {
                // If ran from the installer, we will fall into this block.
                StringDictionary myStringDictionary = this.Context.Parameters;
                foreach (string one in this.Context.Parameters.Keys)
                {
                    if (one.ToUpper() == "SERVICENAME")
                    {
                        _params.ServiceName = this.Context.Parameters[one].ToString();
                    }
                    if (one.ToUpper() == "DESCRIPTION")
                    {
                        _params.Description = this.Context.Parameters[one].ToString();
                    }
                    if (one.ToUpper() == "DISPLAYNAME")
                    {
                        _params.DisplayName = this.Context.Parameters[one].ToString();
                    }
                    if (one.ToUpper() == "DEPENDSON")
                    {
                        _params.DependsOn = this.Context.Parameters[one].ToString();
                    }
                }
            }
            else
            {
                // If ran from the command line, we will fall in here.
                String[] args = System.Environment.GetCommandLineArgs();
                String no_log_file = null;
                InstallContext tmp_ctx = new InstallContext(no_log_file, args);

                foreach (DictionaryEntry de in tmp_ctx.Parameters)
                {
                    //                Console.WriteLine("Param Key: {0}, Value: {1}", de.Key.ToString(), de.Value.ToString());

                    if (de.Value.ToString() != string.Empty)
                    {
                        if (de.Key.ToString().ToUpper() == "SERVICENAME")
                        {
                            _params.ServiceName = de.Value.ToString();
                            Console.WriteLine("Found Command Line Parameter ServiceName=" + _params.ServiceName);
                        }
                        if (de.Key.ToString().ToUpper() == "DESCRIPTION")
                        {
                            _params.Description = de.Value.ToString();
                            Console.WriteLine("Found Command Line Parameter Description=" + _params.Description);
                        }
                        if (de.Key.ToString().ToUpper() == "DISPLAYNAME")
                        {
                            _params.DisplayName = de.Value.ToString();
                            Console.WriteLine("Found Command Line Parameter DisplayName=" + _params.DisplayName);
                        }
                        if (de.Key.ToString().ToUpper() == "DEPENDSON")
                        {
                            _params.DependsOn = de.Value.ToString();
                            Console.WriteLine("Found Command Line Parameter DependsOn=" + _params.DependsOn);
                        }
                    }
                }
            }
        }

        private class ProjectInstallerCmdVars
        {
            private string _serviceName;
            private string _description;
            private string _displayName;
            private string _dependsOn;

            public string ServiceName
            {
                get { return _serviceName; }
                set { _serviceName = value; }
            }

            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }

            public string DisplayName
            {
                get { return _displayName; }
                set { _displayName = value; }
            }

            public string DependsOn
            {
                get { return _dependsOn; }
                set { _dependsOn = value; }
            }

        }

        //public void WriteEventLog(string message)
        //{
        //    string source;
        //    string log;

        //    source = "utcInstallTest";
        //    log = "utcMainEventLog";

        //    try
        //    {
        //        if (!EventLog.SourceExists(source))
        //        {
        //            EventLog.CreateEventSource(source, log);
        //        }

        //        EventLog.WriteEntry(source, "CODE: " + message);
        //        //            EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Warning, 234);
        //    }
        //    finally
        //    {
        //        // Not much I can do here...
        //    }

        //}

    }

    //public class CustomParameters
    //{
    //    /// <summary>
    //    /// This inner class maintains the key names
    //    /// for the parameter values that may be passed on the 
    //    /// command line.
    //    /// </summary>
    //    public class Keys
    //    {
    //        public const string MyCustomParameter =
    //                           "MyCustomParameter";
    //        public const string MyOtherCustomParameter =
    //                           "MyOtherCustomParameter";
    //    }

    //    private string _myCustomParameter = null;
    //    public string MyCustomParameter
    //    {
    //        get { return _myCustomParameter; }
    //    }

    //    private string _myOtherCustomParameter = null;
    //    public string MyOtherCustomParameter
    //    {
    //        get { return _myOtherCustomParameter; }
    //    }

    //    /// <summary>
    //    /// This constructor is invoked by Install class
    //    /// methods that have an Install Context built from 
    //    /// parameters specified in the command line.
    //    /// Rollback, Install, Commit, and intermediate methods like
    //    /// OnAfterInstall will all be able to use this constructor.
    //    /// </summary>
    //    /// <param name="installContext">The install context
    //    /// containing the command line parameters to set
    //    /// the strong types variables to.</param>
    //    public CustomParameters(InstallContext installContext)
    //    {
    //        this._myCustomParameter =
    //          installContext.Parameters[Keys.MyCustomParameter];
    //        this._myOtherCustomParameter =
    //              installContext.Parameters[Keys.MyOtherCustomParameter];
    //    }

    //    /// <summary>
    //    /// This constructor is used by the Install class
    //    /// methods that don't have an Install Context built
    //    /// from the command line. This method is primarily
    //    /// used by the Uninstall method.
    //    /// </summary>
    //    /// <param name="savedState">An IDictionary object
    //    /// that contains the parameters that were
    //    /// saved from a prior installation.</param>
    //    public CustomParameters(IDictionary savedState)
    //    {
    //        if (savedState.Contains(Keys.MyCustomParameter) == true)
    //            this._myCustomParameter =
    //              (string)savedState[Keys.MyCustomParameter];

    //        if (savedState.Contains(Keys.MyOtherCustomParameter) == true)
    //            this._myOtherCustomParameter =
    //              (string)savedState[Keys.MyOtherCustomParameter];
    //    }
    //}
}
