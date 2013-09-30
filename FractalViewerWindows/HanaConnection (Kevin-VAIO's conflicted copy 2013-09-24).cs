using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FractalModel;

namespace FractalViewWin
{
   /// <summary>
   /// Requires FractalModel project to create a FractalManager
   /// Calls _fractalManager.GetFractal* to kick off queued jobs to get data.
   /// Subscribes to _fractalManager.OnResultSetChanged and then calls _fractalManager.GetResultSet
   /// to see data points to draw.
   /// </summary>
   public partial class HanaConnection : Form
   {
      private FractalManager _fractalManager;
      private FractalViewer _fractalViewer;

      public HanaConnection()
      {
         InitializeComponent();

         // Initialise text boxes to some useful defaults
         // Res 100
         //SetTextBoxDefaults(100, -1f, 0.40f, -0.11f, 0.89f);
         
         // Res 200 
         //SetTextBoxDefaults(200, -1f, -0.5f, -0.11f, 0.89f);
         
         // Res 400 zoomed in all data
         SetTextBoxDefaults(200, -1f, 1f, -1f, 1f);
         
         // Fractal manager
         _fractalManager = new FractalManager();
         _fractalManager.Initialise();
         _fractalManager.OnResultSetChanged += OnResultSetChanged;

         // Fractal viewer
         _fractalViewer = new FractalViewer(new Rectangle(70, 176, 560, 400));
         _fractalViewer.Initialise();         
      }

      private void SetTextBoxDefaults(int resolution, float xmin, float xmax, float ymin, float ymax)
      {
         textBoxXMin.Text = xmin.ToString();
         textBoxXMax.Text = xmax.ToString();
         textBoxYMin.Text = ymin.ToString();
         textBoxYMax.Text = ymax.ToString();
         comboBoxResolution.Text = resolution.ToString();
      }

      private void buttonODataHttp_Click(object sender, EventArgs e)
      {
         // get mandlebrot space intervals x and y that we want to draw
         float minX = (float)Convert.ToDouble(textBoxXMin.Text);
         float maxX = (float)Convert.ToDouble(textBoxXMax.Text);
         float minY = (float)Convert.ToDouble(textBoxYMin.Text);
         float maxY = (float)Convert.ToDouble(textBoxYMax.Text);
         int resolution = Convert.ToInt32(comboBoxResolution.Text);

         // request the result set asynchronously
         _fractalManager.GetFractalFromHana(minX, maxX, minY, maxY, resolution);
      }

      private void OnResultSetChanged(object sender, EventArgs args)
      {
         // Get copy of latest result set (Mandlebrot space)
         List<ResultSetHana> resultSetHana = _fractalManager.GetResultSet();
         ResultSetHanaMetadata meta = _fractalManager.GetResultSetMetaData();

         // Pass result set to viewer (converts Mandlebrot space to something drawable)
         _fractalViewer.BuildDrawingResultSet(resultSetHana, meta);

         // Force a repaint
         this.Invalidate();
      }

      private void btnGetDataOffine_Click(object sender, EventArgs e)
      {
         _fractalManager.GetFractalFromOffline();
      }

      private void HanaConnection_Paint(object sender, PaintEventArgs e)
      {
         //WriteLine("OnPaint");   
         // Show what we've received and are drawing
         labelResultSet.Text = "Count: " + _fractalViewer.ResultSetDraw.Count.ToString();
         labelResultMeta.Text = "Ranges: " + _fractalViewer.GetResultMetaDataString();
         Graphics g;
         g = e.Graphics;

         float xmax = 0f, xmin = 5000f, ymax = 0f, ymin = 5000f;

         foreach (ResultSetDraw rsd in _fractalViewer.ResultSetDraw)
         {
            if (cboBrushShape.Text == "Circle")
            {
               g.DrawEllipse(rsd.Pen, rsd.XDraw, rsd.YDraw, rsd.Pen.Width, rsd.Pen.Width);
            }
            else
            {
               g.DrawLine(rsd.Pen, rsd.XDraw, rsd.YDraw, rsd.XDraw + rsd.Pen.Width, rsd.YDraw);
            }

            if (rsd.XDraw > xmax)
               xmax = rsd.XDraw;

            if (rsd.XDraw < xmin)
               xmin = rsd.XDraw;

            if (rsd.YDraw > ymax)
               ymax = rsd.YDraw;

            if (rsd.YDraw < ymin)
               ymin = rsd.YDraw;
         }
         WriteLine("xDraw max:{0} xDraw min:{1} yDraw max:{2} yDraw min:{3}", xmax, xmin, ymax, ymin);
      }

      private void btnRefreshDisplay_Click(object sender, EventArgs e)
      {
         int brushWidth = Convert.ToInt32(cboBrushWidth.Text);
         int brushAlpha = Convert.ToInt32(txtAlpha.Text);
         _fractalViewer.RefreshResultSetPenWidthAndAlpha(brushWidth, brushAlpha);

         this.Invalidate();
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

      private void buttonRebuildColors_Click(object sender, EventArgs e)
      {
         // Change color scheme
         if (comboBoxScheme.Text == "Red")
            _fractalViewer.FractalColorScheme = FractalColorScheme.LogBasedRed;
         else if (comboBoxScheme.Text == "Blue")
            _fractalViewer.FractalColorScheme = FractalColorScheme.LogBasedBlue;
         else
            _fractalViewer.FractalColorScheme = FractalColorScheme.LogBasedRed;

         // Rebuild colors
         _fractalViewer.RebuildColors();
      }
   }
}
