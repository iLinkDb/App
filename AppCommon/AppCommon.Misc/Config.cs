using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace AppCommon
{
   public class Config
   {

      public static string ConnectString(string dbName, string settingPrefix)
      {
         string retVal = string.Format("server={0};user id={1};pwd={2};database={3};",
            DbServer(settingPrefix), DbUser(settingPrefix), DbPwd(settingPrefix), dbName);

         return retVal;
      }

      public static string DbServer(string settingPrefix)
      {
         string retVal = ConfigurationManager.AppSettings[GetPrefix(settingPrefix) + "_DbServer"];
         if (string.IsNullOrEmpty(retVal))
         { retVal = "awsinstance.c2qabeutlsje.us-east-1.rds.amazonaws.com"; }

         return retVal;
      }

      public static string DbUser(string settingPrefix)
      {
         string retVal = ConfigurationManager.AppSettings[GetPrefix(settingPrefix) + "_DbUser"];
         if (string.IsNullOrEmpty(retVal))
         { retVal = "awsAdmin"; }

         return retVal;
      }

      public static string DbPwd(string settingPrefix)
      {
         string retVal = ConfigurationManager.AppSettings[GetPrefix(settingPrefix) + "_DbPwd"];
         if (string.IsNullOrEmpty(retVal))
         { retVal = "fredfred"; }

         return retVal;
      }

      private static string GetPrefix(string settingPrefix)
      {
         string retVal = "";

         if (!string.IsNullOrEmpty(settingPrefix))
         {
            retVal = settingPrefix;
         }
         return retVal;
      }

      public static bool IsSoapWebService(string wsUrl)
      {
         bool retVal = wsUrl.ToUpper().Contains("TELELINK-USA.COM");

         return retVal;
      }

   }
}
