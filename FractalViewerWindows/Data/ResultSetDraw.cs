using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace FractalModel
{
   /// <summary>
   /// Must be a struct so copying will work
   /// </summary>
   [DebuggerDisplay("RawX: {XRaw}, RawY: {YRaw}, XDraw: {XDraw}, YDraw: {YDraw}, RGB: {Pen.Color.R}-{Pen.Color.G}-{Pen.Color.B}, Esc: {EscapedRaw}")]
   public struct ResultSetDraw
   {
      public int Id;
      public float XRaw;
      public float YRaw;
      public int EscapedRaw;

      public float XDraw;
      public float YDraw;
      public Pen Pen;
   }
}
