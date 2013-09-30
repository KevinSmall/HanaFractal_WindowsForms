using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FractalModel
{
   /// <summary>
   /// Tracker object to pass back and forth to asynch calls to the HTTP methods   
   /// </summary>
   internal class TrackerObjectHttp
   {
      public HttpWebRequest Request;
      public string Tag;
      public Guid Guid;

      public TrackerObjectHttp(HttpWebRequest request, string tag)
      {
         Request = request;
         Tag = tag;
         Guid = Guid.NewGuid();
      }
   }
}
