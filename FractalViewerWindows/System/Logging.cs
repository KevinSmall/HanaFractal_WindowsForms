using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FractalViewerWindows
{
   public static class Logging
   {
      public static void WriteLine(string format, params object[] arg)
      {
         // Build basic string
         String str;
         if (arg.Length > 0)
         {
            str = string.Format(format, arg);
         }
         else
         {
            str = format;
         }
         Debug.WriteLine(str);
      }
   }
}
