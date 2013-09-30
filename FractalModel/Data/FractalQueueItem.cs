using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FractalModel
{
   [DebuggerDisplay("Xs: {XStart}, Ys: {YStart}, Xe: {XEnd}, Ye: {YEnd}, Res: {Resolution}, IsProc: {IsProcessed}")]
   internal class FractalQueueItem
   {
      public float XStart;
      public float YStart;
      public float XEnd;
      public float YEnd;
      public int Resolution;
      public bool IsProcessed;
   }
}
