using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using static Mandelbrot.CoordinateSystem;
using static Mandelbrot.Form1;

namespace Mandelbrot
{
    internal static class Compute
    {
        static private int TotalPixelCount = (Form1.ComplexPlane.Scale + 1) * (Form1.ComplexPlane.Scale + 1);
        static private int QuadrantPixelCount = TotalPixelCount / 4;

        /* Thread Shared variables */
        static private Point[] BitmapPixels = new Point[TotalPixelCount];
        static private Complex[] ComplexCoordinates = new Complex[TotalPixelCount];
        static public MandelbrotPixel[] MandelbrotPixels = new MandelbrotPixel[TotalPixelCount];

        public struct Complex
        {
            public float Real;
            public float Imag;

            public Complex(float real, float imag)
            {
                Real = real;
                Imag = imag;
            }
        }

        public struct MandelbrotPixel {

            public Point Point;
            public int IterationCount;

            public MandelbrotPixel(Point point, int iterationCount) {
                Point = point;
                IterationCount = iterationCount;
            }
        }

        public static float GetVectorLengthSquareMax(CoordinateSystem coordSys) {

            float[] _vectorLengthSquares = new float[4];
            _vectorLengthSquares[0] = coordSys.Ymax + coordSys.Xmax;
            _vectorLengthSquares[1] = coordSys.Ymax + Math.Abs(coordSys.Xmin);
            _vectorLengthSquares[2] = Math.Abs(coordSys.Xmin) + Math.Abs(coordSys.Ymin);
            _vectorLengthSquares[3] = Math.Abs(coordSys.Ymin) + coordSys.Xmax;

            float _lengthmax = 0;
            for (int i = 0; i < _vectorLengthSquares.Length; i++)
            {
                if (_vectorLengthSquares[i] > _lengthmax) { _lengthmax = _vectorLengthSquares[i]; }
            }

            return _lengthmax;

        }

        public static int GetIterCount(Complex c, CoordinateSystem coordSys) {

            int _count = 0;
            int _max = 256;
            Complex z = new Complex(0, 0);
            float _temp;
            float _vectorLengthSquare = 0;
            float _vectorLengthSquareMax = Compute.GetVectorLengthSquareMax(coordSys);

            do
            {
                _temp = z.Real * z.Real - z.Imag * z.Imag + c.Real;
                z.Imag = 2 * z.Real * z.Imag + c.Imag;
                z.Real = _temp;
                _vectorLengthSquare = z.Real * z.Real + z.Imag * z.Imag;
                _count++;
            } while ((_vectorLengthSquare < _vectorLengthSquareMax) && (_count < _max));

            return _count;
        }
        /////////////////////////////////////////////////////////////////SEQUENTIAL///////////////////////////////////////////////////////////
        public static Point[] GetBasicCoordinates(CoordinateSystem cordSys) {

            int _totalPoints = (cordSys.Scale+1) * (cordSys.Scale+1);
            Point[] basicPoints = new Point[_totalPoints];

            int idx = 0;
            for (int x = 0; x <= cordSys.Scale; x++) {
                for (int y = 0; y <= cordSys.Scale; y++) {
                    basicPoints[idx] = new Point(x, y);
                    idx++;
                }

            }
            return basicPoints;
        }

        public static Complex[] GetMappedCoordinates(CoordinateSystem cordSys)
        {

            int _totalPoints = (cordSys.Scale + 1) * (cordSys.Scale + 1);
            Complex[] mappedPoints = new Complex[_totalPoints];

            int idx = 0;
            for (int x = 0; x <= cordSys.Scale; x++)
            {
                for (int y = 0; y <= cordSys.Scale; y++)
                {   
                    /* Y must be negated: different direction on Y axis between Bitmap and a Coordinate System in math */
                    float _mappedX =  (  (x - 0) * (cordSys.Xmax - cordSys.Xmin) / (cordSys.Scale - 0) + cordSys.Xmin  );
                    float _mappedY = -( (y - 0) * (cordSys.Ymax - cordSys.Ymin) / (cordSys.Scale - 0) + cordSys.Ymin   ); // direction changed (negation)

                    mappedPoints[idx] = new Complex(_mappedX, _mappedY);
                    idx++;
                }
            }

            return mappedPoints;
        }

        public static MandelbrotPixel[] CalculateSequential(CoordinateSystem coordinateSystem, CoordinateVisualize coordinateImage, Priorities threadPriority)
        {
            Thread.CurrentThread.Priority = PriorityManager(threadPriority, false)[0];

            /* Calculate the Mandelbrot Pixels sequentially and return a struct that shall be visualized */
            Point[] _bitmapPixels = GetBasicCoordinates(coordinateSystem);
            Complex[] _complexCoordinates = GetMappedCoordinates(coordinateSystem);
            MandelbrotPixel[] _mandelbrotPixel = new MandelbrotPixel[_bitmapPixels.Length];

            int idx = 0;
            foreach (var cpoint in _complexCoordinates)
            {
                int _iterCount = GetIterCount(cpoint, coordinateSystem);

                /* Return the corresponding bitmap pixel and iteration count based on complex points iteration */
                _mandelbrotPixel[idx] = new MandelbrotPixel(_bitmapPixels[idx], _iterCount);
                idx++;
            }

            return _mandelbrotPixel;
        }

        //////////////////////////////////////////////////////////////PARALLEL//////////////////////////////////////////////////////////////////////////////////////////////

        public static void GetBasicCoordinates(CoordinateSystem cordSys, ref Point[] pointTable, Quadrant quadrant)
        {
            int idx = quadrant.StartIndex;
            for (int x = quadrant.StartPoint.X; x <= quadrant.EndPoint.X; x++)
            {
                for (int y = quadrant.StartPoint.Y; y <= quadrant.EndPoint.Y; y++)
                {
                    pointTable[idx] = new Point(x, y);
                    idx++;
                    if( idx > Form1.ComplexPlane.FourthQuadrant.StopIndex) { break; }
                }

            }
        }

        public static void GetMappedCoordinates(CoordinateSystem cordSys, ref Complex[] pointTable, Quadrant quadrant)
        {
            int i = quadrant.StartIndex;
            for (int x = quadrant.StartPoint.X; x <= quadrant.EndPoint.X; x++)
            {
                for (int y = quadrant.StartPoint.Y; y <= quadrant.EndPoint.Y; y++)
                {
                    /* Y must be negated: different direction on Y axis between Bitmap and a Coordinate System in math */
                    float _mappedX =  (  (x - 0) * (cordSys.Xmax - cordSys.Xmin) / (cordSys.Scale - 0) + cordSys.Xmin  );
                    float _mappedY = -(  (y - 0) * (cordSys.Ymax - cordSys.Ymin) / (cordSys.Scale - 0) + cordSys.Ymin  ); // direction changed (negation)
                    pointTable[i] = new Complex(_mappedX, _mappedY);
                    i++;
                    if ( i > Form1.ComplexPlane.FourthQuadrant.StopIndex) { break; }
                }
            }

        }

        public static void GetMandelbrotData(CoordinateSystem cordSys, ref MandelbrotPixel[] pointTable, Quadrant quadrant) {

            for (int index = quadrant.StartIndex; index < quadrant.StopIndex; index++)
            {
                int _iterCount = GetIterCount(Compute.ComplexCoordinates[index], Form1.ComplexPlane);
                pointTable[index] = new MandelbrotPixel(BitmapPixels[index], _iterCount);
            }

        }

        public static void ThreadCalculationJob(object obj) {

            Quadrant _actualQuadrant = new Quadrant();

            switch ( (int)obj )
            {
                case 1:
                    _actualQuadrant = Form1.ComplexPlane.FirstQuadrant;
                    break;
                case 2:
                    _actualQuadrant = Form1.ComplexPlane.SecondQuadrant;
                    break;
                case 3:
                    _actualQuadrant = Form1.ComplexPlane.ThirdQuadrant;
                    break;
                case 4:
                    _actualQuadrant = Form1.ComplexPlane.FourthQuadrant;
                    break;
            }

            GetBasicCoordinates(Form1.ComplexPlane, ref BitmapPixels, _actualQuadrant);
            GetMappedCoordinates(Form1.ComplexPlane, ref ComplexCoordinates, _actualQuadrant);
            GetMandelbrotData(Form1.ComplexPlane, ref MandelbrotPixels, _actualQuadrant);
            
        }

        public static ThreadPriority[] PriorityManager(Priorities threadPriority, bool multiThreading) {

            if (!multiThreading) {

                ThreadPriority[] _tSinglePriority = new ThreadPriority[1];

                switch (threadPriority) {
                    case Priorities.Default:
                        _tSinglePriority[0] = ThreadPriority.Normal;
                        break;
                    case Priorities.Highest:
                        _tSinglePriority[0] = ThreadPriority.Highest;
                        break;
                    case Priorities.Lowest:
                        _tSinglePriority[0] = ThreadPriority.Lowest;
                        break;
                }

                return _tSinglePriority;
            }

            else
            {
                ThreadPriority[] _tMultiPriority = new ThreadPriority[4];

                switch (threadPriority)
                {
                    case Priorities.Default:

                        /* Quadrant 1 */
                        _tMultiPriority[0] = ThreadPriority.Normal;

                        /* Quadrant 2 */
                        _tMultiPriority[1] = ThreadPriority.Highest;

                        /* Quadrant 3 */
                        _tMultiPriority[2] = ThreadPriority.Normal;

                        /* Quadrant 4 */
                        _tMultiPriority[3] = ThreadPriority.Normal;

                        break;

                    case Priorities.Highest:
                        _tMultiPriority[0] = ThreadPriority.Highest;
                        _tMultiPriority[1] = ThreadPriority.Highest;
                        _tMultiPriority[2] = ThreadPriority.Highest;
                        _tMultiPriority[3] = ThreadPriority.Highest;
                        break;
                    case Priorities.Lowest:
                        _tMultiPriority[0] = ThreadPriority.Lowest;
                        _tMultiPriority[1] = ThreadPriority.Lowest;
                        _tMultiPriority[2] = ThreadPriority.Lowest;
                        _tMultiPriority[3] = ThreadPriority.Lowest;
                        break;
                }

                return _tMultiPriority;

            }


        }
        public static MandelbrotPixel[] CalculateParallel(Priorities threadPriority)
        {

            /* Create working threads */
            Thread[] _tQuadrantThreads = new Thread[4];
            _tQuadrantThreads[0] = new Thread(ThreadCalculationJob);
            _tQuadrantThreads[1] = new Thread(ThreadCalculationJob);
            _tQuadrantThreads[2] = new Thread(ThreadCalculationJob);
            _tQuadrantThreads[3] = new Thread(ThreadCalculationJob);

            /* Set user selected thread priority */
            ThreadPriority[] _priorities = PriorityManager(threadPriority, true);
            _tQuadrantThreads[0].Priority = _priorities[0];
            _tQuadrantThreads[1].Priority = _priorities[1];
            _tQuadrantThreads[2].Priority = _priorities[2];
            _tQuadrantThreads[3].Priority = _priorities[3];

            /* Trace priority */
            Trace.WriteLine("T1 Priority" + _tQuadrantThreads[0].Priority.ToString());
            Trace.WriteLine("T2 Priority" + _tQuadrantThreads[1].Priority.ToString());
            Trace.WriteLine("T3 Priority" + _tQuadrantThreads[2].Priority.ToString());
            Trace.WriteLine("T4 Priority" + _tQuadrantThreads[3].Priority.ToString());

            /* Start threads */
            _tQuadrantThreads[0].Start(1); _tQuadrantThreads[1].Start(2); _tQuadrantThreads[2].Start(3); _tQuadrantThreads[3].Start(4);

            /* Wait finishing jobs */
            _tQuadrantThreads[0].Join(); _tQuadrantThreads[1].Join(); _tQuadrantThreads[2].Join(); _tQuadrantThreads[3].Join();

            return MandelbrotPixels;

        }


    }
}
