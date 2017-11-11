using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mandelbrot
{
    internal class CoordinateSystem
    {
        private int scale;
        private float xmin;
        private float xmax;
        private float ymin;
        private float ymax;
        private Point[] axes;
        private Point[] upLimit;
        private Point[] downLimit;
        private Point[] leftLimit;
        private Point[] rightLimit;
        private Quadrant firstQuadrant;
        private Quadrant secondQuadrant;
        private Quadrant thirdQuadrant;
        private Quadrant fourthQuadrant;

        public int Scale { get => scale; set => scale = value; }
        public float Xmin { get => xmin; set => xmin = value; }
        public float Xmax { get => xmax; set => xmax = value; }
        public float Ymin { get => ymin; set => ymin = value; }
        public float Ymax { get => ymax; set => ymax = value; }
        public Point[] Axes { get => axes; set => axes = value; }
        public Point[] UpLimit { get => upLimit; set => upLimit = value; }
        public Point[] DownLimit { get => downLimit; set => downLimit = value; }
        public Point[] LeftLimit { get => leftLimit; set => leftLimit = value; }
        public Point[] RightLimit { get => rightLimit; set => rightLimit = value; }
        internal Quadrant FirstQuadrant { get => firstQuadrant; set => firstQuadrant = value; }
        internal Quadrant SecondQuadrant { get => secondQuadrant; set => secondQuadrant = value; }
        internal Quadrant ThirdQuadrant { get => thirdQuadrant; set => thirdQuadrant = value; }
        internal Quadrant FourthQuadrant { get => fourthQuadrant; set => fourthQuadrant = value; }

        public readonly int QuadrantCount = 4;

        public struct Quadrant
        {
            public readonly Point StartPoint;
            public readonly Point EndPoint;
            public int StartIndex;
            public int StopIndex;

            public Quadrant(Point startPoint, Point endPoint, int startindex, int stopindex)
            {
                StartPoint = startPoint;
                EndPoint = endPoint;
                StartIndex = startindex;
                StopIndex = stopindex;
            }

        }

        public CoordinateSystem(float xmin, float xmax, float ymin, float ymax, int scale)
        {
            /* Initialize min and max points */
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            this.scale = scale;

            /* Initialize Axes */
            Point[] _axes =
             {
                new Point(0,scale/2),
                new Point(scale,scale/2),
                new Point(scale/2,scale/2),
                new Point(scale/2,scale),
                new Point(scale/2,scale/2),
                new Point(scale/2,0)

             };

            Axes = _axes;

            /* Initialize limits */
            Point[] _upLimit =
              {
                 new Point(scale/2,0),
                 new Point((scale/2+scale/80),0),
                 new Point(scale/2,0),
                 new Point((scale/2-scale/80),0),

             };

            UpLimit = _upLimit;

            Point[] _downLimit =
             {
                new Point(scale / 2, scale),
                new Point((scale / 2 + scale / 80), scale),
                new Point(scale / 2, scale),
                new Point((scale / 2 - scale / 80), scale),

             };

            DownLimit = _downLimit;

            Point[] _leftLimit =
             {
                new Point(0, scale/2),
                new Point(0,(scale / 2 + scale / 80)),
                new Point(0, scale/2),
                new Point(0,(scale / 2 - scale / 80)),

             };

            LeftLimit = _leftLimit;

            Point[] _rightLimit =
             {

                new Point(scale, scale/2),
                new Point(scale, (scale / 2 + scale / 80)),
                new Point(scale, scale/2),
                new Point(scale, (scale / 2 - scale / 80)),

             };

            RightLimit = _rightLimit;

            /* Set quadrants */
            firstQuadrant = new Quadrant   (    new Point(scale / 2, 0)     ,   new Point(scale, scale / 2)  ,  0                             , ( (scale / 2+1) * scale / 2)     );
            secondQuadrant = new Quadrant  (    new Point(0, 0)             ,   new Point(scale/2, scale/2)  ,  ((scale/2+1) * scale/2)       , ( (scale / 2+1) * scale / 2)*2   );
            thirdQuadrant = new Quadrant   (    new Point(0, scale/2)       ,   new Point(scale/2, scale)    ,  ((scale / 2+1) * scale / 2)*2 , ( (scale / 2+1) * scale / 2)*3   );
            fourthQuadrant = new Quadrant  (    new Point(scale/2, scale/2) ,   new Point(scale, scale)      ,  ((scale / 2+1) * scale / 2)*3 , ( (scale / 2+1) * scale / 2)*4   );

        }

        public void SetLimits(float xmin, float xmax, float ymin, float ymax) {
            this.xmin = xmin;
            this.xmax = Xmax;
            this.ymin = ymin;
            this.ymax = Ymax;
        }
        public void SetScale(int userScale)
        {
            if (this is CoordinateSystem)
            {
                this.scale = userScale;

                Point[] _axes =
                {
                new Point(0,scale/2),
                new Point(scale,scale/2),
                new Point(scale/2,scale/2),
                new Point(scale/2,scale),
                new Point(scale/2,scale/2),
                new Point(scale/2,0)

                };

                Axes = _axes;

                /* Initialize limits */

                Point[] _upLimit =
                {
                 new Point(scale/2,0),
                 new Point((scale/2+scale/80),0),
                 new Point(scale/2,0),
                 new Point((scale/2-scale/80),0),

                };

                UpLimit = _upLimit;


                Point[] _downLimit =
                {
                new Point(scale / 2, scale),
                new Point((scale / 2 + scale / 80), scale),
                new Point(scale / 2, scale),
                new Point((scale / 2 - scale / 80), scale),

                };

                DownLimit = _downLimit;

                Point[] _leftLimit =
                {
                new Point(0, scale/2),
                new Point(0,(scale / 2 + scale / 80)),
                new Point(0, scale/2),
                new Point(0,(scale / 2 - scale / 80)),

                };

                LeftLimit = _leftLimit;

                Point[] _rightLimit =
                {

                new Point(scale, scale/2),
                new Point(scale, (scale / 2 + scale / 80)),
                new Point(scale, scale/2),
                new Point(scale, (scale / 2 - scale / 80)),

                };

                RightLimit = _rightLimit;
            }

            firstQuadrant = new Quadrant  (    new Point(scale / 2, 0)         , new Point(scale, scale / 2)       , 0                                 , ((scale / 2 + 1) * scale / 2)        );
            secondQuadrant = new Quadrant (    new Point(0, 0)                 , new Point(scale / 2, scale / 2)   , ((scale / 2 + 1) * scale / 2)     , ((scale / 2 + 1) * scale / 2) * 2    );
            thirdQuadrant = new Quadrant  (    new Point(0, scale / 2)         , new Point(scale / 2, scale)       , ((scale / 2 + 1) * scale / 2) * 2 , ((scale / 2 + 1) * scale / 2) * 3    );
            fourthQuadrant = new Quadrant (    new Point(scale / 2, scale / 2) , new Point(scale, scale)           , ((scale / 2 + 1) * scale / 2) * 3 , ((scale / 2 + 1) * scale / 2) * 4    );
        }

    }
}
