using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mandelbrot
{
    internal class CoordinateSystem : Coordinate
    {

        public override int Scale { get; protected set; }

        public override float Xmin { get; protected set; }
        public override float Xmax { get; protected set; }

        public override float Ymin { get; protected set; }
        public override float Ymax { get; protected set; }

        public override Point[] Axes { get; protected set; }
        public override Point[] UpLimit { get; protected set; }
        public override Point[] DownLimit { get; protected set; }
        public override Point[] LeftLimit { get; protected set; }
        public override Point[] RightLimit { get; protected set; }

        public Quadrant FirstQuadrant  { get; protected set; }
        public Quadrant SecondQuadrant { get; protected set; }
        public Quadrant ThirdQuadrant  { get; protected set; }
        public Quadrant FourthQuadrant { get; protected set; }

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

        public CoordinateSystem(float xmin, float xmax, float ymin, float ymax, int scale) : base(xmin, xmax, ymin, ymax)
        {
            /* Initialize Necessaries */
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymax = ymax;
            Scale = scale;

            /* Initialize Axes */

            Point[] _axes =
             {
                new Point(0,Scale/2),
                new Point(Scale,Scale/2),
                new Point(Scale/2,Scale/2),
                new Point(Scale/2,Scale),
                new Point(Scale/2,Scale/2),
                new Point(Scale/2,0)

             };

            Axes = _axes;

            /* Initialize limits */

            Point[] _upLimit =
              {
                 new Point(Scale/2,0),
                 new Point((Scale/2+Scale/80),0),
                 new Point(Scale/2,0),
                 new Point((Scale/2-Scale/80),0),

             };

            UpLimit = _upLimit;


            Point[] _downLimit =
             {
                new Point(Scale / 2, Scale),
                new Point((Scale / 2 + Scale / 80), Scale),
                new Point(Scale / 2, Scale),
                new Point((Scale / 2 - Scale / 80), Scale),

             };

            DownLimit = _downLimit;

            Point[] _leftLimit =
             {
                new Point(0, Scale/2),
                new Point(0,(Scale / 2 + Scale / 80)),
                new Point(0, Scale/2),
                new Point(0,(Scale / 2 - Scale / 80)),

             };

            LeftLimit = _leftLimit;

            Point[] _rightLimit =
             {

                new Point(Scale, Scale/2),
                new Point(Scale, (Scale / 2 + Scale / 80)),
                new Point(Scale, Scale/2),
                new Point(Scale, (Scale / 2 - Scale / 80)),

             };

            RightLimit = _rightLimit;

            FirstQuadrant = new Quadrant   (    new Point(Scale / 2, 0)     ,   new Point(Scale, Scale / 2)  ,  0                             , ( (Scale / 2+1) * Scale / 2)     );
            SecondQuadrant = new Quadrant  (    new Point(0, 0)             ,   new Point(Scale/2, Scale/2)  ,  ((Scale/2+1) * Scale/2)       , ( (Scale / 2+1) * Scale / 2)*2   );
            ThirdQuadrant = new Quadrant   (    new Point(0, Scale/2)       ,   new Point(Scale/2, Scale)    ,  ((Scale / 2+1) * Scale / 2)*2 , ( (Scale / 2+1) * Scale / 2)*3   );
            FourthQuadrant = new Quadrant  (    new Point(Scale/2, Scale/2) ,   new Point(Scale, Scale)      ,  ((Scale / 2+1) * Scale / 2)*3 , ( (Scale / 2+1) * Scale / 2)*4   );

        }

        public void SetLimits(float xmin, float xmax, float ymin, float ymax) {
            Xmin = xmin;
            Xmax = Xmax;
            Ymin = ymin;
            Ymax = Ymax;
        }
        public void SetScale(int scale)
        {
            if (this is CoordinateSystem)
            {
                this.Scale = scale;

                Point[] _axes =
                 {
                new Point(0,Scale/2),
                new Point(Scale,Scale/2),
                new Point(Scale/2,Scale/2),
                new Point(Scale/2,Scale),
                new Point(Scale/2,Scale/2),
                new Point(Scale/2,0)

             };

                Axes = _axes;

                /* Initialize limits */

                Point[] _upLimit =
                  {
                 new Point(Scale/2,0),
                 new Point((Scale/2+Scale/80),0),
                 new Point(Scale/2,0),
                 new Point((Scale/2-Scale/80),0),

             };

                UpLimit = _upLimit;


                Point[] _downLimit =
                 {
                new Point(Scale / 2, Scale),
                new Point((Scale / 2 + Scale / 80), Scale),
                new Point(Scale / 2, Scale),
                new Point((Scale / 2 - Scale / 80), Scale),

             };

                DownLimit = _downLimit;

                Point[] _leftLimit =
                 {
                new Point(0, Scale/2),
                new Point(0,(Scale / 2 + Scale / 80)),
                new Point(0, Scale/2),
                new Point(0,(Scale / 2 - Scale / 80)),

             };

                LeftLimit = _leftLimit;

                Point[] _rightLimit =
                 {

                new Point(Scale, Scale/2),
                new Point(Scale, (Scale / 2 + Scale / 80)),
                new Point(Scale, Scale/2),
                new Point(Scale, (Scale / 2 - Scale / 80)),

             };

                RightLimit = _rightLimit;
            }

            FirstQuadrant = new Quadrant  (    new Point(Scale / 2, 0)         , new Point(Scale, Scale / 2)       , 0                                 , ((Scale / 2 + 1) * Scale / 2)        );
            SecondQuadrant = new Quadrant (    new Point(0, 0)                 , new Point(Scale / 2, Scale / 2)   , ((Scale / 2 + 1) * Scale / 2)     , ((Scale / 2 + 1) * Scale / 2) * 2    );
            ThirdQuadrant = new Quadrant  (    new Point(0, Scale / 2)         , new Point(Scale / 2, Scale)       , ((Scale / 2 + 1) * Scale / 2) * 2 , ((Scale / 2 + 1) * Scale / 2) * 3    );
            FourthQuadrant = new Quadrant (    new Point(Scale / 2, Scale / 2) , new Point(Scale, Scale)           , ((Scale / 2 + 1) * Scale / 2) * 3 , ((Scale / 2 + 1) * Scale / 2) * 4    );
        }

    }
}
