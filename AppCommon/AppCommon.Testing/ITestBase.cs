using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCommon
{
   public interface ITestBase
   {
      void Begin(HtmlLogFile htmlLogFile);
      void Begin(HtmlLogFile htmlLogFile, string siteUrl);
   }
}
