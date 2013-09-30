using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FractalModel;
using System.Drawing;

namespace FractalViewerWindows
{
   class FractalViewer
   {
      // Properties controlling fractal coloring algorithm and brush
      public FractalColorAlgorithm ColorAlgorithm = FractalColorAlgorithm.LogBasedRed;
      public FractalBrushShape BrushShape = FractalBrushShape.Round;
      public int BrushWidth = 4;
      public int BrushAlpha = 127;

      public List<ResultSetDraw> ResultSetDraw { get { return _resultSetDraw; } }
      private List<ResultSetDraw> _resultSetDraw = new List<ResultSetDraw>(32);
      private ResultSetHanaMetadata _resultSetHanaMeta;
      private Rectangle _viewport;

      public FractalViewer(Rectangle viewport)
      {
         _viewport = viewport;
      }

      internal void Initialise() { }

      internal string GetResultMetaDataStringX()
      {
         return "x:{" + _resultSetHanaMeta.Xmin.ToString() + ", " + _resultSetHanaMeta.Xmax.ToString() + "}";
      }

      internal string GetResultMetaDataStringY()
      {
         return "y:{" + _resultSetHanaMeta.Ymin.ToString() + ", " + _resultSetHanaMeta.Ymax.ToString() + "}";
      }

      /// <summary>
      /// Receives a Hana result set (and its meta data) and builds a drawing result set, including the coloring of points
      /// </summary>
      internal void BuildDrawingResultSet(List<ResultSetHana> resultSetHanaList, ResultSetHanaMetadata meta)
      {
         _resultSetHanaMeta = meta;

         // Calculate the largest rectangle with aspect ratio of the result set, that can fit in the viewport 
         float widthMandle = Math.Abs(_resultSetHanaMeta.Xmax - _resultSetHanaMeta.Xmin);
         float heightMandle = Math.Abs(_resultSetHanaMeta.Ymax - _resultSetHanaMeta.Ymin);
         float aspectRatioMandle = widthMandle / heightMandle;
         Rectangle viewportAtOrigin = new Rectangle(0, 0, _viewport.Width, _viewport.Height);
         Rectangle biggestRectangleThatCanFitInViewport = viewportAtOrigin.GetLargestRectangle(aspectRatioMandle, 1f);

         // The calculation above is just to give us the right scale and coordinates to fit in the viewport
         float scale = biggestRectangleThatCanFitInViewport.Width / widthMandle;
         float xStart = _viewport.X - (scale * _resultSetHanaMeta.Xmin);
         float yStart = _viewport.Y - (scale * _resultSetHanaMeta.Ymin);

         foreach (ResultSetHana rs in resultSetHanaList)
         {
            // Color
            int a = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            ConvertEscapedToColor(this.ColorAlgorithm, rs.Escaped, out a, out r, out g, out b);

            ResultSetDraw rsd = new ResultSetDraw()
            {
               Id = rs.Id,
               XRaw = rs.X,
               YRaw = rs.Y,
               EscapedRaw = rs.Escaped,
               XDraw = xStart + (rs.X * scale),
               YDraw = yStart + (rs.Y * scale),
               Pen = new Pen(Color.FromArgb(a, r, g, b), BrushWidth),
            };
            _resultSetDraw.Add(rsd);
         }
      }

      public void RefreshResultSetPenWidthAndAlpha(int brushWidth, int brushAlpha)
      {
         BrushAlpha = brushAlpha;

         if (this.ResultSetDraw == null)
            return;

         foreach (ResultSetDraw rsd in this.ResultSetDraw)
         {
            rsd.Pen.Width = brushWidth;
            rsd.Pen.Color = Color.FromArgb(BrushAlpha, rsd.Pen.Color.R, rsd.Pen.Color.G, rsd.Pen.Color.B);
         }
      }

      public void RebuildColors()
      {
         if (this.ResultSetDraw == null)
            return;

         foreach (ResultSetDraw rsd in this.ResultSetDraw)
         {
            // Color
            int a = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            ConvertEscapedToColor(this.ColorAlgorithm, rsd.EscapedRaw, out a, out r, out g, out b);
            rsd.Pen.Color = Color.FromArgb(a, r, g, b);
         }
      }

      /// <summary>
      /// Converts a raw Escaped value (1 to 1000) into argb color values
      /// </summary>
      private void ConvertEscapedToColor(FractalColorAlgorithm fractalColorScheme, int esc, out int a, out int r, out int g, out int b)
      {
         if (fractalColorScheme == FractalColorAlgorithm.LogBasedBlue || fractalColorScheme == FractalColorAlgorithm.LogBasedRed)
         {
            #region Log based calcs
            // init 
            a = BrushAlpha;
            r = 0;
            g = 0;
            b = 0;

            float c = 3f * (float)(Math.Log((double)esc) / Math.Log((double)(1000f - 1f)));
            if (c < 1)
            {
               r = (int)(255 * c);
               g = 0;
               b = 0;
            }
            else if (c < 2)
            {
               r = 255;
               g = (int)(255 * (c - 1));
               b = 0;
            }
            else
            {
               r = 255;
               g = 255;
               b = (int)(255 * (c - 2));
            }

            // special case
            if (esc == 1000)
            {
               r = 0;
               g = 0;
               b = 0;
            }

            // Swap red to be blue
            if (fractalColorScheme == FractalColorAlgorithm.LogBasedBlue)
            {
               int temp = 0;
               temp = r;
               r = b;
               b = temp;
            }
            #endregion
         }
         else
         {
            #region Range based calcs
            // init 
            a = BrushAlpha;
            r = 0;
            g = 0;
            b = 0;

            // scale my 0...1000 to this guys 0..4096
            int scaledEsc = esc * 4;
            if (scaledEsc == 4000) /* In the set. Assign black. */
            {
               r = 0;
               g = 0;
               b = 0;
            }
            else if (scaledEsc < 64) /* red*/
            {
               r = scaledEsc * 2;
               g = 0;
               b = 0;
            }
            else if (scaledEsc < 128)
            {
               r = (((scaledEsc - 64) * 128) / 126) + 128;
               g = 0;
               b = 0;
            }
            else if (scaledEsc < 256)
            {
               r = (((scaledEsc - 128) * 62) / 127) + 193;
               g = 0;
               b = 0;
            }
            else if (scaledEsc < 512)
            {
               r = 255;
               g = (((scaledEsc - 256) * 62) / 255) + 1;
               b = 0;
            }
            else if (scaledEsc < 1024)
            {
               r = 255;
               g = (((scaledEsc - 512) * 63) / 511) + 64;
               b = 0;
            }
            else if (scaledEsc < 2048)
            {
               r = 255;
               g = (((scaledEsc - 1024) * 63) / 1023) + 128;
               b = 0;
            }
            //else if (scaledEsc < 4096)
            else if (scaledEsc < 3900)
            {
               r = 255;
               g = (((scaledEsc - 2048) * 63) / 2047) + 192;
               b = 0;
            }
            else
            {
               r = 255;
               g = 255;
               b = 0;
            }
            #endregion
         }
      }

      internal void Reset()
      {
         _resultSetDraw.Clear();
         _resultSetHanaMeta = new ResultSetHanaMetadata();
      }

      internal void Draw(Graphics g)
      {
         // Probably can combine these rebuilds?
         // Rebuild pen width and alpha
         RefreshResultSetPenWidthAndAlpha(BrushWidth, BrushAlpha);
         // Rebuild colors
         RebuildColors();

         float xmax = -9999f, xmin = 9999f, ymax = -9999f, ymin = 9999f;
         float xrawmax = -9999f, xrawmin = 9999f, yrawmax = -9999f, yrawmin = 9999f;

         foreach (ResultSetDraw rsd in this._resultSetDraw)
         {
            if (BrushShape == FractalBrushShape.Round)
            {
               g.DrawEllipse(rsd.Pen, rsd.XDraw, rsd.YDraw, rsd.Pen.Width, rsd.Pen.Width);
            }
            else
            {
               g.DrawLine(rsd.Pen, rsd.XDraw, rsd.YDraw, rsd.XDraw + rsd.Pen.Width, rsd.YDraw);
            }

            xmax = Math.Max(rsd.XDraw, xmax);
            xmin = Math.Min(rsd.XDraw, xmin);
            ymax = Math.Max(rsd.YDraw, ymax);
            ymin = Math.Min(rsd.YDraw, ymin);
            xrawmax = Math.Max(rsd.XRaw, xrawmax);
            xrawmin = Math.Min(rsd.XRaw, xrawmin);
            yrawmax = Math.Max(rsd.YRaw, yrawmax);
            yrawmin = Math.Min(rsd.YRaw, yrawmin);

         }
         Logging.WriteLine("xDraw min:{0} xDraw max:{1} yDraw min:{2} yDraw max:{3}", xmin, xmax, ymin, ymax);
         Logging.WriteLine("xRaw min:{0} xRaw max:{1} yRaw min:{2} yRaw max:{3}", xrawmin, xrawmax, yrawmin, yrawmax);

         // Draw axes
         Font font = new Font(FontFamily.GenericSansSerif, 10);
         Brush brush = new SolidBrush(Color.Black);
         float offset = 24;

         // The numbers showing the limits on the axes
         Point xleft = new Point((int)(xmin), (int)(ymin - offset));
         g.DrawString(xrawmin.ToString(), font, brush, xleft);
         Point xright = new Point((int)(xmax - offset), (int)(ymin - offset));
         g.DrawString(xrawmax.ToString(), font, brush, xright);
         Point ytop = new Point((int)(xmax + offset), (int)(ymin));
         g.DrawString(yrawmin.ToString(), font, brush, ytop);
         Point ybottom = new Point((int)(xmax + offset), (int)(ymax - offset));
         g.DrawString(yrawmax.ToString(), font, brush, ybottom);

         // The axes lines
         Pen pen = new Pen(Color.Black, 1);
         g.DrawLine(pen, xleft, xright);
         g.DrawLine(pen, ytop, ybottom);
      }
   }

   public enum FractalColorAlgorithm
   {
      LogBasedBlue,
      LogBasedRed,
      RangeBasedOrange
   }
   public enum FractalBrushShape
   {
      Round,
      Square
   }
}
