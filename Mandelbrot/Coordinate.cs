using System.Drawing;

namespace Mandelbrot
{
    abstract internal class Coordinate
    {
        abstract public int Scale { get; protected set; }

        abstract public float Xmin { get; protected set; }
        abstract public float Xmax { get; protected set; }

        abstract public float Ymin { get; protected set; }
        abstract public float Ymax { get; protected set; }

        abstract public Point[] Axes { get; protected set; }
        abstract public Point[] UpLimit { get; protected set; }
        abstract public Point[] DownLimit { get; protected set; }
        abstract public Point[] LeftLimit { get; protected set; }
        abstract public Point[] RightLimit { get; protected set; }

        public Coordinate(float xmin, float xmax, float ymin, float ymax) {
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymin = ymin;

        }

    }


}