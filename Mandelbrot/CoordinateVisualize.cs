using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using static Mandelbrot.Compute;


namespace Mandelbrot
{
    class CoordinateVisualize : CoordinateSystem
    {
        object lockobj = new object();

        public readonly Bitmap DrawingSheet;
        public readonly Graphics GFX;

        public bool IsColorful { get; set; } = false;
        public int Resolution { get; set; } = 800;

        public CoordinateVisualize(CoordinateSystem coordSys): base(coordSys.Xmin,coordSys.Xmax,coordSys.Ymin,coordSys.Ymax,coordSys.Scale) {

            DrawingSheet = new Bitmap(coordSys.Scale + 1, coordSys.Scale + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GFX = Graphics.FromImage(DrawingSheet);

        }

        public void DrawEmptyImage() {
            if(this is CoordinateSystem){
                GFX.Clear(SystemColors.Control);
            }
        }

        public void DrawCoordinateSystem()
        {

            if (this is CoordinateVisualize)
            {

                GFX.DrawLines(Pens.Black, Axes);
                GFX.DrawLines(Pens.Black, UpLimit);
                GFX.DrawLines(Pens.Black, DownLimit);
                GFX.DrawLines(Pens.Black, LeftLimit);
                GFX.DrawLines(Pens.Black, RightLimit);
            }

        }

        public void DrawPoint(Point point, Color color) {

            DrawingSheet.SetPixel(point.X, point.Y, color);
        }

        public void DrawMandelbrotFractal(MandelbrotPixel[] mandelbrotPixels) {

            if (this is CoordinateVisualize) {
                foreach (var pixel in mandelbrotPixels)
                {
                    this.DrawPoint(pixel.Point, ColorSelect(pixel.IterationCount,this.IsColorful));
                }
            }
        }

        static public Color ColorSelect(int iterationCount, bool iscolorful) {

            if (iterationCount >= 0 && iterationCount < 10) {
                return(iscolorful) ? Color.Purple : SystemColors.Control;
            }

            if (iterationCount >= 10 && iterationCount < 20)
            {
                return(iscolorful) ? Color.PowderBlue : Color.LightSlateGray;
            }

            if (iterationCount >= 20 && iterationCount < 40)
            {
                return(iscolorful) ? Color.Red : Color.Gray;
            }

            if (iterationCount >= 40 && iterationCount < 60)
            {
                return(iscolorful)? Color.LightBlue : Color.DimGray;
            }

            if (iterationCount >= 60 && iterationCount < 90)
            {
                return(iscolorful) ? Color.CadetBlue : Color.Gray;
            }

            if (iterationCount >= 90 && iterationCount < 120)
            {
                return(iscolorful) ? Color.CornflowerBlue : Color.DarkGray;
            }

            if (iterationCount >= 120)
            {
                return(iscolorful) ? Color.DarkBlue : Color.Black;
            }

           return Color.Black;

        }

    }
}


