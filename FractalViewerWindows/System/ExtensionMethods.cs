using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FractalViewerWindows
{
   public static class ExtensionMethods
   {
      public static void Shuffle<T>(this IList<T> list)
      {
         Random rng = new Random();
         int n = list.Count;
         while (n > 1)
         {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
         }
      }

      /// <summary>
      /// Returns the largest Rectangle of given aspect ratio that can be selected from calling Rectangle.
      /// Calling rectangle must be at position 0,0 we only use its width and height
      /// </summary>
      /// <param name="sourceRect">Source rectangle</param>
      /// <param name="desiredAspectRatio">The aspect ratio of the rectangle to return</param>
      /// <param name="squashyFactor">Use 1f for no squashing. Allow squashing eg 1.3 would allow 30% squashing or stretching</param>
      /// <returns>Largest Rectangle of given aspect ratio that can be selected from within calling Rectangle sourceRect</returns>
      public static Rectangle GetLargestRectangle(this Rectangle sourceRect, float desiredAspectRatio, float squashyFactor)
      {
         Rectangle desiredRectangle = new Rectangle();

         float sourceAspectRatio = (float)sourceRect.Width / (float)sourceRect.Height;
         if (desiredAspectRatio < sourceAspectRatio)
         {
            // Height can be full height, we lose a bit of the left and right edges
            desiredRectangle.Height = sourceRect.Height;
            desiredRectangle.Y = 0;
            // Width
            desiredRectangle.Width = (int)((float)sourceRect.Height * desiredAspectRatio * squashyFactor);
            // Clamp width
            if (desiredRectangle.Width > sourceRect.Width)
            {
               desiredRectangle.Width = sourceRect.Width;
            }
            // Offset
            desiredRectangle.X = (int)(0.5f * (sourceRect.Width - desiredRectangle.Width));
            if (desiredRectangle.X < 0)
            {
               desiredRectangle.X = 0;
            }
         }
         else
         {
            // Width can be full width, we lose a bit of the top and bottom
            desiredRectangle.Width = sourceRect.Width;
            desiredRectangle.X = 0;
            // Height
            desiredRectangle.Height = (int)((float)sourceRect.Width / desiredAspectRatio * squashyFactor);
            // Clamp height
            if (desiredRectangle.Height > sourceRect.Height)
            {
               desiredRectangle.Height = sourceRect.Height;
            }
            // Offset
            desiredRectangle.Y = (int)(0.5f * (sourceRect.Height - desiredRectangle.Height));
            if (desiredRectangle.Y < 0)
            {
               desiredRectangle.Y = 0;
            }
         }

         //SourceRectFromDXT = new Rectangle(43, 0, 426, 256);
         //desiredRectangle = Dashboard.SourceRectFromDXT;
         return desiredRectangle;
      }
   }
}
