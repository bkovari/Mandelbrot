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
        private Graphics gfx;      
        private int resolution;
        private bool isColorful;
        private Bitmap drawingSheet;
     
        public Graphics GFX { get => gfx; set => gfx = value; }     
        public int Resolution { get => resolution; set => resolution = value; }
        public bool IsColorful { get => isColorful; set => isColorful = value; }
        public Bitmap DrawingSheet { get => drawingSheet; set => drawingSheet = value; }

        public CoordinateVisualize(CoordinateSystem coordSys): base(coordSys.Xmin,coordSys.Xmax,coordSys.Ymin,coordSys.Ymax,coordSys.Scale)
        {
            drawingSheet = new Bitmap(coordSys.Scale + 1, coordSys.Scale + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            gfx = Graphics.FromImage(drawingSheet);
        }

        public void DrawEmptyImage()
        {
            if(this is CoordinateSystem){
                gfx.Clear(SystemColors.Control);
            }
        }

        public void DrawCoordinateSystem()
        {
            if (this is CoordinateVisualize)
            {
                gfx.DrawLines(Pens.Black, Axes);
                gfx.DrawLines(Pens.Black, UpLimit);
                gfx.DrawLines(Pens.Black, DownLimit);
                gfx.DrawLines(Pens.Black, LeftLimit);
                gfx.DrawLines(Pens.Black, RightLimit);
            }
        }

        public void DrawMandelbrotFractal(MandelbrotPixel[] mandelbrotPixels)
        {
            if (this is CoordinateVisualize) {
                foreach (var pixel in mandelbrotPixels)
                {
                    drawingSheet.SetPixel(pixel.Point.X,pixel.Point.Y, ColorSelect(pixel.IterationCount, this.IsColorful));
                }
            }
        }

        static public Color ColorSelect(int iterationCount, bool iscolorful)
        {
            if (iterationCount >= 0 && iterationCount < 10) {
                return(iscolorful) ? Color.Purple : SystemColors.Control;
            }
            else if (iterationCount >= 10 && iterationCount < 20)
            {
                return(iscolorful) ? Color.PowderBlue : Color.LightSlateGray;
            }
            else if (iterationCount >= 20 && iterationCount < 40)
            {
                return(iscolorful) ? Color.Red : Color.Gray;
            }
            else if (iterationCount >= 40 && iterationCount < 60)
            {
                return(iscolorful)? Color.LightBlue : Color.DimGray;
            }
            else if (iterationCount >= 60 && iterationCount < 90)
            {
                return(iscolorful) ? Color.CadetBlue : Color.Gray;
            }
            else if (iterationCount >= 90 && iterationCount < 120)
            {
                return(iscolorful) ? Color.CornflowerBlue : Color.DarkGray;
            }
            else if (iterationCount >= 120)
            {
                return(iscolorful) ? Color.DarkBlue : Color.Black;
            }
           return Color.Black;
        }
    }
}


