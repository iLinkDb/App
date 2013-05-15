using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AppCommon
{
   public class Conversion
   {
      private static string DateTimeUrlFormat = "yyyyMMddHHmmss";


      public static long GetId(string idValue)
      {
         long retVal = 0;
         long.TryParse(idValue, out retVal);
         return retVal;
      }

      public static string FormatMySqlGuid(string guid)
      {
         string retVal = guid;
         if (!String.IsNullOrEmpty(guid))
         {
            if (guid.Contains("{"))
            {
               retVal = guid.Replace("{", "").Replace("}", "");
            }
         }
         return retVal;
      }

      public static string FormatPhone(string phone)
      {
         string retVal = "";
         if (!String.IsNullOrEmpty(phone))
         {
            string workPhone = phone.Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "").Replace(" ", "");

            int len = workPhone.Length;
            if (len < 5)
            {
               retVal = workPhone;
            }
            else if (len < 8)
            {
               retVal = String.Format("{0}-{1}", workPhone.Substring(0, len - 4), workPhone.Substring(len - 4, 4));
            }
            else if (len < 11)
            {
               retVal = String.Format("({0}) {1}-{2}",
                                      workPhone.Substring(0, len - 7),
                                      workPhone.Substring(len - 7, 3),
                                      workPhone.Substring(len - 4, 4));
            }
            else
            {
               retVal = String.Format("+{0} ({1}) {2}-{3}",
                                      workPhone.Substring(0, len - 10),
                                      workPhone.Substring(len - 10, 3),
                                      workPhone.Substring(len - 7, 3),
                                      workPhone.Substring(len - 4, 4));
            }
         }

         return retVal;
      }

       public static string FormatCreditCardFourDashFour(string creditCardNumber)
       {
           string retVal = creditCardNumber.Substring(0, 4) + "-" + creditCardNumber.Substring(creditCardNumber.Length - 4, 4);
           return retVal;
       }

       public static string DateTimeIso8601FormatToString(DateTime dateTime)
       {
           string retVal = dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

           return retVal;
       }

      public static string EncodeDateForUrl(DateTime workDate)
      {
         string retVal = workDate.ToString(DateTimeUrlFormat);
         return retVal;
      }

      public static DateTime DecodeDateFromUrl(string workDate)
      {
         CultureInfo culture = CultureInfo.InvariantCulture;
         DateTime retVal = DateTime.ParseExact(workDate, DateTimeUrlFormat, culture);

         return retVal;
      }
   }
}
