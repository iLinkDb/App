using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using System.Reflection;

namespace AppCommon
{
   public class Computer
   {
      public static string GetMD5HashFromFile(string fileName)
      {
         FileStream file = new FileStream(fileName, FileMode.Open);
         MD5 md5 = new MD5CryptoServiceProvider();
         byte[] retVal = md5.ComputeHash(file);
         file.Close();

         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < retVal.Length; i++)
         {
            sb.Append(retVal[i].ToString("x2"));
         }
         return sb.ToString();
      }

      public static string CalculateHash(string strInput)
      {
         MD5 md5 = System.Security.Cryptography.MD5.Create();
         byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strInput);
         byte[] hash = md5.ComputeHash(inputBytes);

         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < hash.Length; i++)
         {
            sb.Append(hash[i].ToString("x2"));
         }
         return sb.ToString();
      }


      public static string UniqueComputerKey()
      {
         string retVal = "";

         retVal += CpuId();
         retVal += "-" + HardDriveId("c");

         return retVal;
      }

      public static string CpuId()
      {
         string cpuInfo = string.Empty;
         ManagementClass mc = new ManagementClass("win32_processor");
         ManagementObjectCollection moc = mc.GetInstances();

         foreach (ManagementObject mo in moc)
         {
            if (cpuInfo == "")
            {
               //Get only the first CPU's ID
               cpuInfo = mo.Properties["processorID"].Value.ToString();
               break;
            }
         }
         return cpuInfo;
      }

      public static string HardDriveId(string drive)
      {
         ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
         dsk.Get();
         string volumeSerial = dsk["VolumeSerialNumber"].ToString();
         return volumeSerial;
      }

      public static string GetPassword()
      {
         string pwd = "";
         ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

         while (true)
         {
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
               break;
            }
            pwd += keyInfo.KeyChar;
            Console.Write("*");
         }
         return pwd;
      }

      public static void CopyFolder(string sourceFolder, string destFolder, bool overwrite)
      {
         if (!Directory.Exists(destFolder))
            Directory.CreateDirectory(destFolder);

         string[] files = Directory.GetFiles(sourceFolder);
         foreach (string file in files)
         {
            string name = Path.GetFileName(file);
            string dest = Path.Combine(destFolder, name);
            File.Copy(file, dest, overwrite);
         }

         string[] folders = Directory.GetDirectories(sourceFolder);
         foreach (string folder in folders)
         {
            string name = Path.GetFileName(folder);
            string dest = Path.Combine(destFolder, name);
            CopyFolder(folder, dest, overwrite);
         }
      }

      public static string GetFullName()
      {
         string retVal = "";
         try
         {
            retVal = Assembly.GetCallingAssembly().GetName().FullName;
         }
         catch (Exception ex)
         {
            retVal = "GetFullName/EXCEPTION: " + ex.Message;
         }
         return retVal;
      }

      public static string GetVersion()
      {
         string retVal = "";
         try
         {
            retVal = Assembly.GetCallingAssembly().GetName().Version.ToString();
         }
         catch (Exception ex)
         {
            retVal = "GetVersion/EXCEPTION: " + ex.Message;
         }
         return retVal;
      }

      public static string ObfuscateString(string connect, string lookFor)
      {
         // string lookFor = "pwd=";
         string retVal = connect;

         if (! string.IsNullOrEmpty(lookFor))
         {
            int iPosPwd = connect.ToLower().IndexOf(lookFor.ToLower());
            if (iPosPwd > 0)
            {
               int iPosSemi = connect.Substring(iPosPwd).IndexOf(";");
               if (iPosSemi > 0)
               {
                  retVal = connect.Substring(0, iPosPwd + lookFor.Length) + "***" + connect.Substring(iPosPwd + iPosSemi);
               }
            }
         }
         return retVal;
      }

      public static TimeSpan UpTime
      {
         get
         {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
               uptime.NextValue();       //Call this an extra time before reading its value
               return TimeSpan.FromSeconds(uptime.NextValue());
            }
         }
      }

      public static string LocalIpv4Address
      {
         get
         {
            string retVal = "";
            try
            {
               var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
               var ip = (
                          from addr in hostEntry.AddressList
                          where addr.AddressFamily.ToString() == "InterNetwork"
                          select addr.ToString()
                   ).FirstOrDefault();
               retVal = ip;
            }
            finally
            {
               // Not much we can do here, we will just hope the calling program does a IsNullOrEmpty check. 
            }
            return retVal;
         }
      }
   }
}

