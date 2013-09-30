using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;
using Newtonsoft.Json.Linq;

namespace FractalModel
{
   /// <summary>
   /// Receives requests for fractal information via .GetFractalFromHana() and queues them up, processes them asynchronously
   /// and raises event OnResultSetChanged when new data arrives.  That data is then retrievable by calling .GetResultSet().
   /// Uses OData and Json to retreive HANA fractal data.
   /// </summary>
   public class FractalManager
   {
      public event EventHandler<EventArgs> OnResultSetChanged = delegate { };

      private readonly string _odataService_RootURL_Json = @"http://" + HanaServerDetails.ServerName + HanaServerDetails.ODataServiceName;
      private List<ResultSetHana> _resultSetHana = new List<ResultSetHana>(32);
      private Timer _timer;
      private object _lockObjectResultSetHana = new object();
      private int QueueSize { get { return _fractalCallQueue.Count; } }
      private List<FractalQueueItem> _fractalCallQueue = new List<FractalQueueItem>(32);
      private bool _isQueueAbleToBeProcessed = true;

      public FractalManager() { }

      public void Initialise()
      {
         _timer = new Timer();
         _timer.Interval = 1500; // The time per tick in milliseconds
         _timer.Elapsed += OnTimerTick;

         _timer.AutoReset = true;
         _timer.Enabled = false;
      }

      void OnTimerTick(object sender, ElapsedEventArgs e)
      {
         if (!_isQueueAbleToBeProcessed)
            return;

         //WriteLine("Tick");

         // Remove queue items that have been kicked off
         _fractalCallQueue.RemoveAll(item => item.IsProcessed == true);

         // Process next item in queue
         if (_fractalCallQueue.Count > 0)
         {
            FractalQueueItem fqi = _fractalCallQueue[0];
            fqi.IsProcessed = true;
            this.ProcessQueueItem(fqi);

            // We dont process any more till we get the callback
            _isQueueAbleToBeProcessed = false;
         }
         else
         {
            //WriteLine("Queue empty");
            _isQueueAbleToBeProcessed = true;
         }
      }

      /// <summary>
      /// Returns a copy of the result set held within the Fractal Manager
      /// </summary>      
      public List<ResultSetHana> GetResultSet()
      {
         List<ResultSetHana> resultSetCopy = new List<ResultSetHana>();

         foreach (var rsh in _resultSetHana)
         {
            resultSetCopy.Add(rsh);
         }
         return resultSetCopy;
      }

      /// <summary>
      /// Returns a result set meta (largest smallest x y)
      /// </summary>      
      public ResultSetHanaMetadata GetResultSetMetaData()
      {
         float xmax = -9999f, xmin = 9999f, ymax = -9999f, ymin = 9999f;

         foreach (ResultSetHana rsh in _resultSetHana)
         {
            xmax = Math.Max(rsh.X, xmax);
            xmin = Math.Min(rsh.X, xmin);
            ymax = Math.Max(rsh.Y, ymax);
            ymin = Math.Min(rsh.Y, ymin);
         }

         return new ResultSetHanaMetadata() { Xmax = xmax, Xmin = xmin, Ymax = ymax, Ymin = ymin };

         //WriteLine("xmax:{0} xmin:{1} ymax:{2} ymin:{3}", xmax, xmin, ymax, ymin);
      }

      /// <summary>      
      /// Requests fractal data be retieved from HANA
      /// All coordinates are in Mandlebrot space.
      /// </summary>
      public void GetFractalFromHana(float xStart, float xEnd, float yStart, float yEnd, int Resolution)
      {
         _fractalCallQueue.Add(new FractalQueueItem()
         {
            XStart = xStart,
            XEnd = xEnd,
            YStart = yStart,
            YEnd = yEnd,
            Resolution = Resolution,
            IsProcessed = false
         });

         _timer.Enabled = true;
      }

      /// <summary>
      /// Kick off an HTTP request asynchronously
      /// </summary>
      private void ProcessQueueItem(FractalQueueItem fqi)
      {
         try
         {
            string urlString = GetUrlString(fqi);
            HttpWebRequest request;
            request = (HttpWebRequest)HttpWebRequest.Create(urlString);
            request.Credentials = new System.Net.NetworkCredential(HanaServerDetails.UserName, HanaServerDetails.Password);
            TrackerObjectHttp trackerObjectHttp = new TrackerObjectHttp(request, "tag");

            request.BeginGetResponse(CallbackGetFractalFromHana_Json, trackerObjectHttp);
            WriteLine("--> GetFractalFromHana_Json called");
         }
         catch (Exception e)
         {
            WriteLine("ERROR: ProcessQueueItem failed exception: {0}", e.Message);
         }
         finally
         {
         }
      }

      private string GetUrlString(FractalQueueItem fqi)
      {
         string url = _odataService_RootURL_Json;

         // resolution
         url = url + @"?$filter=Resolution%20eq%20" + fqi.Resolution.ToString();

         // x interval
         int xmin = (int)(fqi.XStart * (float)fqi.Resolution);
         url = url + @"%20and%20XCoord%20ge%20" + xmin.ToString();
         int xmax = (int)(fqi.XEnd * (float)fqi.Resolution);
         url = url + @"%20and%20XCoord%20le%20" + xmax.ToString();

         // y interval
         int ymin = (int)(fqi.YStart * (float)fqi.Resolution);
         url = url + @"%20and%20YCoord%20ge%20" + ymin.ToString();
         int ymax = (int)(fqi.YEnd * (float)fqi.Resolution);
         url = url + @"%20and%20YCoord%20le%20" + ymax.ToString();

         // format
         url = url + @"&$format=json";

         WriteLine("--> GetUrlString gives URL: {0}", url);
         return url;
      }

      private void CallbackGetFractalFromHana_Json(IAsyncResult result)
      {
         WriteLine("<-- Callback received");

         TrackerObjectHttp trackerObjectHttp = result.AsyncState as TrackerObjectHttp;
         if (trackerObjectHttp == null)
         {
            WriteLine("ERROR: CallbackGetFractalFromHana_Json failed, trackerObjectHttp is null");
            return;
         }
         HttpWebRequest request = trackerObjectHttp.Request;

         lock (_lockObjectResultSetHana)
         {
            try
            {
               HttpWebResponse webresponse = request.EndGetResponse(result) as HttpWebResponse;

               // Get the response string
               Encoding enc = System.Text.Encoding.GetEncoding(1252);
               StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
               string res = loResponseStream.ReadToEnd();
               loResponseStream.Close();
               webresponse.Close();

               // Get Json data from the string
               JObject jObjectAll = JObject.Parse(res);
               JObject jObjectD = (JObject)jObjectAll["d"];
               JArray results = (JArray)jObjectD["results"];

               for (int i = 0; i < results.Count; i++)
               {
                  JObject jObjectEntry = (JObject)results[i];
                  int resolution = (int)jObjectEntry["Resolution"];
                  int xCoord = (int)jObjectEntry["XCoord"];
                  int yCoord = (int)jObjectEntry["YCoord"];
                  int escaped = (int)jObjectEntry["Escaped"];

                  ResultSetHana rsh = new ResultSetHana()
                  {
                     Id = i,
                     X = (float)xCoord / (float)resolution,
                     Y = (float)yCoord / (float)resolution,
                     Escaped = escaped
                  };

                  _resultSetHana.Add(rsh);
               }
               OnResultSetChangedRaised();
            }
            catch (Exception e)
            {
               WriteLine("ERROR: CallbackGetFractalFromHana_Json failed tag: {0} exception: {1}", trackerObjectHttp.Tag, e.Message);
            }
         }

         _isQueueAbleToBeProcessed = true;
      }

      private void OnResultSetChangedRaised()
      {
         OnResultSetChanged(this, EventArgs.Empty);
      }

      public void WriteLine(string format, params object[] arg)
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

      public void Reset()
      {
         // Avoid contention with queue that could be filling results
         lock (_lockObjectResultSetHana)
         {
            try
            {
               _resultSetHana.Clear();
            }
            catch (Exception e)
            {
               WriteLine("ERROR: Reset failed exception: {0}", e.Message);
            }
         }
      }
   }
}
