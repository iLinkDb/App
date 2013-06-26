using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace AppCommon
{
   public class TestRunner
   {
      #region Public Methods

      public static void Begin(string outputFolder)
      {
         RunTest(outputFolder, null);
      }

      public static void Begin(string outputFolder, string[] args)
      {
         int testNum = 0;
         if (int.TryParse(args[0], out testNum))
         {
            if (testNum <= 0)
            {
               // Run all tests
               RunTest(outputFolder, null);
            }
            else
            {
               List<string> list = AvailableTestList();
               if (testNum <= list.Count)
               {
                  list.Sort();
                  RunTest(outputFolder, list[testNum - 1]);
               }
               else
               { ShowHelp(); }
            }
         }
         else
         {
            ShowHelp();
         }
      }

      public static void ShowHelp()
      {
         List<string> list = AvailableTestList();
         list.Sort();
         Console.WriteLine("      Menu");
         Console.WriteLine("  0 = All Tests");
         for (int iLoop = 0; iLoop < list.Count; iLoop++)
         {
            Console.WriteLine("  {0:#0} = {1}", iLoop + 1, list[iLoop]);
         }
         StringBuilder help = new StringBuilder();
         help.AppendLine();
         help.AppendLine("  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
         help.AppendFormat("  Example: {0} 1  (Will run the first test)", GetExeName());
         help.AppendLine();
         help.Append("       or");
         help.AppendLine();
         help.AppendFormat("           {0} 0  (Run all tests)", GetExeName());
         help.AppendLine();
         help.AppendLine();
         help.AppendLine("  Tests with the IgnoreTest attribute will be skipped when all");
         help.AppendLine("  tests are ran.  However if you select a specific test from ");
         help.AppendLine("  the menu above, it will be ran.");
         Console.WriteLine(help.ToString());
      }

      #endregion

      #region Private Methods

      private static void RunTest(string outputFolder, string oneTest)
      {
         string logMsg = "TestRunner/Begin";

         string htmlFileName = "";

         try
         {
            HtmlLogFile html = new HtmlLogFile(outputFolder);
            htmlFileName = html.FileName;

            try
            {
               if (string.IsNullOrEmpty(oneTest))
               {
                  foreach (Type type in Assembly.GetEntryAssembly().GetTypes())
                  {
                     if (type.BaseType == typeof(TestBase) && type.GetInterfaces().Contains(typeof(ITestBase)))
                     {
                        bool ignore = (type.GetCustomAttributes(typeof(IgnoreTestAttribute), true).Length > 0);
                        if (ignore)
                        {
                           string msg = logMsg + string.Format(" skipping test: {0} (Ignore attribute found)", type.Name);
                           Console.WriteLine(msg);
                           Logging.LogTrace(msg);
                        }
                        else
                        {
                           string msg = logMsg + string.Format(" running test: {0}", type.Name);
                           Console.WriteLine(msg);
                           Logging.LogTrace(msg);

                           ITestBase runTest = (ITestBase)Activator.CreateInstance(type);
                           if (runTest != null)
                           {
                              runTest.Begin(html);
                           }
                        }
                     }
                  }
               }
               else
               {
                  string msg = logMsg + string.Format(" running specific test: {0}", oneTest);
                  Console.WriteLine(msg);
                  Logging.LogTrace(msg);

                  AssemblyName assemblyName = Assembly.GetEntryAssembly().GetName();
                  Object myTest = System.Activator.CreateInstance(assemblyName.Name, oneTest).Unwrap();
                  ITestBase runTest = (ITestBase)myTest;
                  runTest.Begin(html);
               }
            }
            catch (Exception ex)
            {
               Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
            }

            // After all Tests, close the file.
            html.WritePageFooter();
         }
         catch (Exception ex)
         {
            Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
         }
         finally
         {
            System.Threading.Thread.Sleep(2000);
            Process someProcess = new Process();
            someProcess.StartInfo.FileName = htmlFileName;
            someProcess.Start();
         }
      }

      private static List<string> AvailableTestList()
      {
         List<string> retVal = new List<string>();

         foreach (Type type in Assembly.GetEntryAssembly().GetTypes())
         {
            if (type.BaseType == typeof(TestBase) && type.GetInterfaces().Contains(typeof(ITestBase)))
            {
               retVal.Add(type.FullName);
            }
         }
         return retVal;
      }

      private static string GetExeName()
      {
         string retVal = System.AppDomain.CurrentDomain.FriendlyName;

         if (retVal.ToLower().EndsWith(".exe"))
         {
            retVal = retVal.Substring(0, retVal.Length - 4);
         }

         return retVal;
      }

      #endregion

   }
}
