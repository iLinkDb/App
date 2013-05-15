using System;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Text;
using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;
using NLog.Config;

namespace AppCommon
{
   public class Logging
   {
      private static Logger _logger = LogManager.GetCurrentClassLogger();

      public static void ConfigureLogger(string fileName)
      {
         LoggingConfiguration config = LogManager.Configuration;

         if (config == null)
         {
            config = new LoggingConfiguration();
         }

         var logFile = new FileTarget();
         config.AddTarget("file", logFile);

         logFile.FileName = fileName;
         // logFile.Layout = "${date} | ${message}";
         logFile.Layout = "${longdate}|${level}|${message}|${exception:format=tostring}";

         var rule = new LoggingRule("*", LogLevel.Trace, logFile);
         config.LoggingRules.Add(rule);

         LogManager.Configuration = config;

         //_logger.Info("File converted!"); 
         LogManager.ReconfigExistingLoggers();
      }

      /// <summary>
      /// very detailed logs, which may include high-volume information such as protocol payloads. 
      /// This log level is typically only enabled during development 
      /// </summary>
      /// <param name="message"></param>
      public static void LogTrace(string message)
      {
         System.Diagnostics.Trace.TraceInformation("AppTrace-" + message);
         _logger.Trace(message);
      }

      public static void LogTrace(string message, string appName)
      {
         System.Diagnostics.Trace.TraceInformation("AppTrace-" + message);
         _logger.Trace(message, appName);
      }

      /// <summary>
      /// debugging information, less detailed than trace, typically not enabled in production environment.
      /// </summary>
      /// <param name="message"></param>
      public static void LogDebug(string message)
      {
         System.Diagnostics.Trace.TraceInformation("AppDebug-" + message);
         _logger.Debug(message);
      }

      public static void LogDebug(string message, string appName)
      {
         System.Diagnostics.Trace.TraceInformation("AppDebug-" + message);
         _logger.Debug(message, appName);
      }

      /// <summary>
      /// information messages, which are normally enabled in production environment
      /// </summary>
      /// <param name="message"></param>
      public static void LogInfo(string message)
      {
         System.Diagnostics.Trace.TraceInformation("AppInfo-" + message);
         _logger.Info(message);
      }

      public static void LogInfo(string message, string appName)
      {
         System.Diagnostics.Trace.TraceInformation("AppInfo-" + message);
         _logger.Info(message, appName);
      }

      /// <summary>
      /// warning messages, typically for non-critical issues, which can be recovered or which are temporary failures 
      /// </summary>
      /// <param name="message"></param>
      public static void LogWarning(string message)
      {
         System.Diagnostics.Trace.TraceWarning("AppWarn-" + message);
         _logger.Warn(message);
      }

      public static void LogWarning(string message, string appName)
      {
         System.Diagnostics.Trace.TraceWarning("AppWarn-" + message);
         _logger.Warn(message, appName);
      }

      /// <summary>
      /// error messages
      /// </summary>
      /// <param name="_logger"></param>
      /// <param name="message"></param>
      public static void LogError(string message)
      {
         System.Diagnostics.Trace.TraceError("AppError-" + message);
         _logger.Error(message);
      }

      public static void LogError(string message, string appName)
      {
         System.Diagnostics.Trace.TraceError("AppError-" + message);
         _logger.Error(message, appName);
      }

      public static void LogError(Exception ex)
      {
         System.Diagnostics.Trace.TraceError("AppError-" + ex.Message);
         _logger.ErrorException(ex.Message, ex);

         //try
         //{
         //   string appInfo = Assembly.GetCallingAssembly().GetName().FullName + " ";
         //   appInfo += Assembly.GetCallingAssembly().GetName().Version.ToString();

         //   Loggr.Events.CreateFromException(ex).Text(ex.Message).Source(appInfo).Post();
         //}
         //finally { }
      }

      public static void LogError(Exception ex, string appName)
      {
         System.Diagnostics.Trace.TraceError("AppError-" + ex.Message);
         ex.HelpLink = appName;
         _logger.ErrorException(ex.Message, ex);
      }

      public static void LogError(string message, Exception ex)
      {
         System.Diagnostics.Trace.TraceError("AppError-" + ex.Message);
         _logger.ErrorException(message, ex);
      }

      public static void LogError(string message, Exception ex, string appName)
      {
         System.Diagnostics.Trace.TraceError("AppError-" + ex.Message);
         ex.HelpLink = appName;
         _logger.ErrorException(message, ex);
      }

      /// <summary>
      /// warning messages, typically for non-critical issues, which can be recovered or which are temporary failures 
      /// </summary>
      /// <param name="message"></param>
      public static void LogFatal(string message)
      {
         System.Diagnostics.Trace.TraceWarning("AppFatal-" + message);
         _logger.Fatal(message);
      }

      public static void LogFatal(string message, string appName)
      {
         System.Diagnostics.Trace.TraceWarning("AppFatal-" + message);
         _logger.Fatal(message, appName);
      }



      public static void LogBinary(string message)
      {
         // byte[] all = ASCIIEncoding.Default.GetBytes(message);
         // byte[] all = Encoding.GetEncoding(28591).GetBytes(message);
         // byte[] all = Encoding.GetEncoding(437).GetBytes(message);
         byte[] all = Encoding.ASCII.GetBytes(message);

         int intPos = 0;

         StringBuilder logMsg = new StringBuilder();
         StringBuilder lineHex = new StringBuilder();
         StringBuilder lineAscii = new StringBuilder();

         logMsg.AppendLine("Binary Output");
         foreach (byte one in all)
         {
            int ascii = Convert.ToInt32(one);
            string hex = ascii.ToString("X2");

            lineHex.Append(hex + " ");

            if (ascii < 32)
            {
               //                    lineAscii.Append(Encoding.ASCII.GetChars(new byte[] { 219 }));   // Square Block
               //                    lineAscii.Append(Convert.ToChar(127));   // Del Char
               lineAscii.Append(".");
            }
            else
            {
               lineAscii.Append(Convert.ToChar(one));
            }

            intPos++;
            if (intPos > 15)
            {
               logMsg.AppendLine(lineHex + " " + lineAscii);
               intPos = 0;
               lineHex.Length = 0;
               lineAscii.Length = 0;
            }
         }
         if (intPos > 0)
         {
            lineHex.Append(string.Empty.PadLeft((16 - intPos) * 3));
            logMsg.AppendLine(lineHex + " " + lineAscii);
         }
         _logger.Trace(logMsg.ToString());
      }

      ///*
      //            // This is what has been working in the past.
      //            FileTarget target = new FileTarget();
      //            target.Name = "file";

      //            // target.FileName = "${basedir}/" + fileName;
      //            target.FileName = Path.Combine(logPath, appName + ".dbg");
      //            target.Layout = "${longdate}|${level}|${message}|${exception:format=tostring}";
      //            target.ArchiveEvery = FileTarget.ArchiveEveryMode.Hour;
      //            target.ArchiveNumbering = FileTarget.ArchiveNumberingMode.Rolling;
      //            target.MaxArchiveFiles = 168;  // 24 * 7

      //            target.KeepFileOpen = false;
      //            target.Encoding = "iso-8859-2";

      //            AsyncTargetWrapper wrapper = new AsyncTargetWrapper();
      //            wrapper.WrappedTarget = target;
      //            wrapper.QueueLimit = 5000;
      //            wrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Discard;

      //            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(wrapper, LogLevel.Trace);
      //*/
      //            // LogManager.ThrowExceptions = true;

      //            LoggingConfiguration config = LogManager.Configuration; // Keep Original config.
      //            if (config == null)
      //            { config = new LoggingConfiguration(); }

      //            FileTarget target = new FileTarget();
      //            target.Name = "file";

      //            // Debug files.
      //            target.FileName = Path.Combine(logPath, appName + ".dbg");
      //            target.Layout = "${longdate}|${level}|${message}|${exception:format=type}";
      //            target.ArchiveEvery = FileTarget.ArchiveEveryMode.Hour;
      //            target.ArchiveNumbering = FileTarget.ArchiveNumberingMode.Rolling;
      //            target.MaxArchiveFiles = 168;  // 24hr * 7day

      //            target.KeepFileOpen = false;
      //            target.Encoding = "iso-8859-2";

      //            config.AddTarget(target.Name, target);

      //            LoggingRule rule = new LoggingRule("*", LogLevel.Trace, target);
      //            config.LoggingRules.Add(rule);

      //            //DatabaseTarget targetDb = new DatabaseTarget();
      //            //DatabaseParameterInfo parameterInfo;

      //            //targetDb.Name = "DbLog";
      //            //targetDb.DBProvider = "mssql";
      //            //targetDb.DBHost = "DbServer";
      //            //targetDb.DBDatabase = "SqlData";
      //            //targetDb.DBUserName = "dev";
      //            //targetDb.DBPassword = "PASSWORD";
      //            //targetDb.CommandText = "EXEC v1_LogDebug_spi @logLevel, @oaGuid, @userName, @logMessage;";

      //            //targetDb.Parameters.Add(new DatabaseParameterInfo("@logLevel", "${level}"));
      //            //targetDb.Parameters.Add(new DatabaseParameterInfo("@oaGuid", "${aspnet-session:variable=OaGuid}"));
      //            //targetDb.Parameters.Add(new DatabaseParameterInfo("@userName", "${aspnet-session:variable=userName}"));
      //            //targetDb.Parameters.Add(new DatabaseParameterInfo("@logMessage", "${message}"));
      //            //config.AddTarget(targetDb.Name, targetDb);

      //            //LoggingRule ruleDb = new LoggingRule("*", LogLevel.Trace, targetDb);
      //            //config.LoggingRules.Add(ruleDb);

      //            FileTarget targetErr = new FileTarget();
      //            targetErr.Name = "file";

      //            // target.FileName = "${basedir}/" + fileName;
      //            targetErr.FileName = Path.Combine(logPath, appName + ".err");
      //            targetErr.Layout = "${longdate}|${machinename}|${level}|${message}|${exception:format=method}|${exception:format=type}|${exception:format=stacktrace}";
      //            targetErr.ArchiveEvery = FileTarget.ArchiveEveryMode.Minute;
      //            targetErr.ArchiveNumbering = FileTarget.ArchiveNumberingMode.Rolling;
      //            targetErr.ArchiveAboveSize = 10;    // Should cause every error to be in a sep file.
      //            targetErr.MaxArchiveFiles = 10080;  // 60min x 24hr x 7day

      //            targetErr.KeepFileOpen = false;
      //            targetErr.Encoding = "iso-8859-2";

      //            config.AddTarget(targetErr.Name, targetErr);

      //            LoggingRule ruleErr = new LoggingRule("*", LogLevel.Error, targetErr);
      //            config.LoggingRules.Add(ruleErr);

      //            LogManager.Configuration = config;

      //            _logger.Trace("Logger Initialized to path: " + logPath
      //                + " for AppName: " + appName
      //                + " Version: " + version);
      //        }

      //public static string RunFolder(string appName)
      //{
      //    string path =
      //         System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

      //    string logPath = path;

      //    int pos = logPath.ToUpper().IndexOf(appName.ToUpper());

      //    if (pos > 0)
      //    {
      //        logPath = logPath.Substring(0, (pos + appName.Length)) + "\\Log";
      //    }
      //    if (logPath.ToUpper().StartsWith(@"FILE:\"))
      //    {
      //        logPath = logPath.Substring(6);
      //    }
      //    return logPath;
      //}


      public static void WriteConfigToLog(Logger logger)
      {
         int count = ConfigurationManager.AppSettings.Count;

         logger.Trace("Configuration Settings Begin");
         for (int intLoop = 0; intLoop < count; intLoop++)
         {
            string name = ConfigurationManager.AppSettings.GetKey(intLoop);
            string[] values = ConfigurationManager.AppSettings.GetValues(intLoop);
            for (int valueLoop = 0; valueLoop < values.Length; valueLoop++)
            {
               string oneValue = values[valueLoop];
               if (name.ToUpper().EndsWith("PWD"))
               {
                  oneValue = "******";
               }
               string line = string.Format("Setting: {0}, Value {1}", name, oneValue);
               logger.Trace(line);
            }

         }
         logger.Trace("Configuration Settings End");

      }

   }
}
