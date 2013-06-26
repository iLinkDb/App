using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IlinkDb.WebRole.UiTests
{
   public enum TestDataEnum
   {
      BarneyRubble,
      TomTuttle,
      Invalid
   }

   public class TestData
   {
       public const string SearchBox = "q";
       public const string DuckSubmit = "search_button_homepage";

      public string CardNumber { get; set; }
      public string CardHolderName { get; set; }
      public string CardMonth { get; set; }
      public string CardYear { get; set; }
      public string CardCode { get; set; }
      public string CardZip { get; set; }
      public string ClientName { get; set; }
      public string Inv1Number { get; set; }
      public string Inv1Amount { get; set; }
      public string Comment { get; set; }

      public TestData()
      {
         SetTestData(TestDataEnum.BarneyRubble);
      }

      public TestData(TestDataEnum testData)
      {
         SetTestData(testData);
      }

      private void SetTestData(TestDataEnum testData)
      {
         switch (testData)
         {
            case TestDataEnum.Invalid:
               {
                  CardNumber = "1";
                  CardHolderName = "";
                  CardMonth = "";
                  CardYear = "";
                  CardCode = "1";
                  CardZip = "1";
                  ClientName = "Betty Rubble";
                  Inv1Number = "1";
                  Inv1Amount = "0";
                  Comment = "";
               }
               break;

            case TestDataEnum.BarneyRubble:
               {
                  CardNumber = "4111111111111111";
                  CardHolderName = "Barney Rubble";
                  CardMonth = "5 - May";
                  CardYear = "2015";
                  CardCode = "8765";
                  CardZip = "12345-6789";
                  ClientName = "Betty Rubble";
                  Inv1Number = "A12345";
                  Inv1Amount = "1.03";
                  Comment = "Comment for Barney";
               }
               break;
            case TestDataEnum.TomTuttle:
               {
                  CardNumber = "4111111111111111";
                  CardHolderName = "Tom Tuttle";
                  CardMonth = "5 - May";
                  CardYear = "2015";
                  CardCode = "8765";
                  CardZip = "12345-6789";
                  ClientName = "Tommy's Boy";
                  Inv1Number = "A12345";
                  Inv1Amount = "1.03";
                  Comment = "Comment for Tom";
               }
               break;
         }
      }
   }
}
