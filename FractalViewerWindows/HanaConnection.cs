using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FractalModel;

namespace FractalViewerWindows
{
   /// <summary>
   /// This acts as a controller, and uses:
   /// _fractalManager to call GetFractal to kick off queued jobs to get fractal data
   /// _fractalViewer to do the rendering
   /// This controller subscribes to _fractalManager.OnResultSetChanged and calls _fractalManager.GetResultSet
   /// which it then passes to the viewer.
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
         //SetTextBoxDefaults(200, -1f, 0.4f, -0.11f, 0.89f);
         SetTextBoxDefaults(200, -1.5f, 0.5f, -1f, 1f);

         // Res 400 zoomed in all data
         //SetTextBoxDefaults(400, -1f, 1f, -1f, 1f);

         // Res 800 zoomed in all data
         //SetTextBoxDefaults(800, -1f, 1f, -1f, 1f);

         // Res 6000 zoomed in all data
         //SetTextBoxDefaults(6000, -0.60f, -0.51f, 0.53f, 0.59f);

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

      private void buttonGetData_Click(object sender, EventArgs e)
      {
         // Clear out viewer and manager ready to receive new data
         _fractalManager.Reset();
         _fractalViewer.Reset();
         
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

      private void HanaConnection_Paint(object sender, PaintEventArgs e)
      {
         // Show what we've received and are drawing
         labelResultSet.Text = "Count: " + _fractalViewer.ResultSetDraw.Count.ToString();
         labelRangeRecX.Text = _fractalViewer.GetResultMetaDataStringX();
         labelRangeRecY.Text = _fractalViewer.GetResultMetaDataStringY();

         // Read controls to get latest drawing settings, which are then set on the _fractalViewer before drawing
         // Brush shape
         if (comboBoxBrushShape.Text == "Circle")
            _fractalViewer.BrushShape = FractalBrushShape.Round;
         else
            _fractalViewer.BrushShape = FractalBrushShape.Square;
         // Brush width and alpha
         _fractalViewer.BrushWidth = Convert.ToInt32(comboBoxBrushWidth.Text);
         _fractalViewer.BrushAlpha = Convert.ToInt32(txtAlpha.Text);
         // Coloring algorithm
         if (comboBoxScheme.Text == "Red")
            _fractalViewer.ColorAlgorithm = FractalColorAlgorithm.LogBasedRed;
         else if (comboBoxScheme.Text == "Blue")
            _fractalViewer.ColorAlgorithm = FractalColorAlgorithm.LogBasedBlue;
         else if (comboBoxScheme.Text == "Orange")
            _fractalViewer.ColorAlgorithm = FractalColorAlgorithm.RangeBasedOrange;
         else
            _fractalViewer.ColorAlgorithm = FractalColorAlgorithm.LogBasedRed;

         // Draw result set and its axes
         Graphics g = e.Graphics;
         _fractalViewer.Draw(g);
      }

      private void buttonRefreshDisplay_Click(object sender, EventArgs e)
      {
         this.Invalidate();
      }
   }
}
