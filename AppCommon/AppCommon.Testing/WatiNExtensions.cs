using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Native;

namespace AppCommon
{
   public static class WatiNExtensions
   {
      //
      // ForceChange
      //
      // Ensures proper testing of jQuery .change() events.
      //
      // http://stackoverflow.com/questions/3712825/unable-to-fire-jquery-change-event-on-selectlist-from-watin
      //
      public static void ForceChange(this Element e)
      {
         e.DomContainer.Eval(string.Format("$('#{0}').change();", e.Id));
         e.WaitForComplete();

         e.DomContainer.Eval(string.Format("$('[name=\"{0}\"]').change();", e.Id));
         e.WaitForComplete();
      }
   }
}
