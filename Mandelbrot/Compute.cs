using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using static Mandelbrot.MainForm;
using static Mandelbrot.CoordinateSystem;

namespace Mandelbrot
{
    internal static class Compute
    {
        static private int totalPixelCount = (ComplexPlane.Scale + 1) * (ComplexPlane.Scale + 1);
        static private int quadrantPixelCount = totalPixelCount / 4;
        static private int threadPoolTaskCount = ComplexPlane.Scale;
        static private int threadPoolGranularity = 1;

        static private Point[] BitmapPixels = new Point[totalPixelCount];
        static private Complex[] ComplexCoordinates = new Complex[totalPixelCount];
        static private MandelbrotPixel[] MandelbrotPixels = new MandelbrotPixel[totalPixelCount];
        static private List<MandelbrotPixel> MandelbrotPixelsList = new List<MandelbrotPixel>(totalPixelCount);

        static ManualResetEvent signalThreadPoolJobReady = new ManualResetEvent(false);

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

        public struct MandelbrotPixel
        {
            public Point Point;
            public int IterationCount;

            public MandelbrotPixel(Point point, int iterationCount)
            {
                Point = point;
                IterationCount = iterationCount;
            }
        }

        /////////////////////////////////////////////////////////////////CALCULATION/////////////////////////////////////////////////////////
        public static float GetVectorLengthSquareMax(CoordinateSystem coordSys)
        {
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

        private static int GetIterCount(Complex c, CoordinateSystem coordSys)
        {
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
        public static Point[] GetBasicCoordinates(CoordinateSystem cordSys)
        {
            Point[] basicPoints = new Point[totalPixelCount];

            int idx = 0;
            for (int x = 0; x <= cordSys.Scale; x++)
            {
                for (int y = 0; y <= cordSys.Scale; y++)
                {
                    basicPoints[idx] = new Point(x, y);
                    idx++;
                }

            }
            return basicPoints;
        }

        private static Complex[] GetMappedCoordinates(CoordinateSystem cordSys)
        {
            Complex[] mappedPoints = new Complex[totalPixelCount];

            int idx = 0;
            for (int x = 0; x <= cordSys.Scale; x++)
            {
                for (int y = 0; y <= cordSys.Scale; y++)
                {
                    /* Y must be negated: different direction on Y axis between Bitmap and a Coordinate System in math */
                    float _mappedX = ((x - 0) * (cordSys.Xmax - cordSys.Xmin) / (cordSys.Scale - 0) + cordSys.Xmin);
                    float _mappedY = -((y - 0) * (cordSys.Ymax - cordSys.Ymin) / (cordSys.Scale - 0) + cordSys.Ymin); // Direction change

                    mappedPoints[idx] = new Complex(_mappedX, _mappedY);
                    idx++;
                }
            }
            return mappedPoints;
        }

        public static MandelbrotPixel[] CalculateSequential(CoordinateSystem coordinateSystem, Priorities threadPriority)
        {
            Thread.CurrentThread.Priority = PriorityManager(threadPriority, false)[0]; // Set chosen priority

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

        //////////////////////////////////////////////////////////////PARALLEL///////////////////////////////////////////////////////////////
        public static void GetBasicCoordinates(CoordinateSystem cordSys, Point[] pointTable, Quadrant quadrant)
        {
            int idx = quadrant.StartIndex;
            for (int x = quadrant.StartPoint.X; x <= quadrant.EndPoint.X; x++)
            {
                for (int y = quadrant.StartPoint.Y; y <= quadrant.EndPoint.Y; y++)
                {
                    pointTable[idx] = new Point(x, y);
                    idx++;
                    if (idx > ComplexPlane.FourthQuadrant.StopIndex) { break; }
                }
            }
        }

        public static void GetBasicCoordinates(CoordinateSystem cordSys, bool sharedResult)
        {
            int idx = 0;
            for (int x = 0; x <= cordSys.Scale; x++)
            {
                for (int y = 0; y <= cordSys.Scale; y++)
                {
                    BitmapPixels[idx] = new Point(x, y);
                    idx++;
                }

            }
        }

        private static void GetMappedCoordinates(CoordinateSystem cordSys, Complex[] pointTable, Quadrant quadrant)
        {
            int i = quadrant.StartIndex;
            for (int x = quadrant.StartPoint.X; x <= quadrant.EndPoint.X; x++)
            {
                for (int y = quadrant.StartPoint.Y; y <= quadrant.EndPoint.Y; y++)
                {
                    /* Y must be negated: different direction on Y axis between Bitmap and a Coordinate System in math */
                    float _mappedX = ((x - 0) * (cordSys.Xmax - cordSys.Xmin) / (cordSys.Scale - 0) + cordSys.Xmin);
                    float _mappedY = -((y - 0) * (cordSys.Ymax - cordSys.Ymin) / (cordSys.Scale - 0) + cordSys.Ymin); // Direction change
                    pointTable[i] = new Complex(_mappedX, _mappedY);
                    i++;
                    if (i > ComplexPlane.FourthQuadrant.StopIndex) { break; }
                }
            }

        }

        public static void GetMandelbrotData(CoordinateSystem cordSys, MandelbrotPixel[] pointTable, Quadrant quadrant)
        {
            for (int index = quadrant.StartIndex; index < quadrant.StopIndex; index++)
            {
                int _iterCount = GetIterCount(Compute.ComplexCoordinates[index], ComplexPlane);
                pointTable[index] = new MandelbrotPixel(BitmapPixels[index], _iterCount);
            }
        }

        public static Point[] GetBasicCoordinates(int columnIdx, int columnCount)
        {
            Point[] _basicPoints = new Point[columnCount * ComplexPlane.Scale + columnCount];

            int idx = 0;
            for (int x = columnIdx; x < columnIdx + columnCount; x++)
            {
                for (int y = 0; y <= ComplexPlane.Scale; y++)
                {
                    _basicPoints[idx] = new Point(x, y);
                    idx++;
                }
            }
            return _basicPoints;
        }

        public static Complex[] GetMappedCoordinates(Point[] basicPoints)
        {
            Complex[] _mappedPoints = new Complex[basicPoints.Length];

            int idx = 0;
            foreach (Point point in basicPoints)
            {
                float _mappedX = ((point.X - 0) * (ComplexPlane.Xmax - ComplexPlane.Xmin) / (ComplexPlane.Scale - 0) + ComplexPlane.Xmin);
                float _mappedY = -((point.Y - 0) * (ComplexPlane.Ymax - ComplexPlane.Ymin) / (ComplexPlane.Scale - 0) + ComplexPlane.Ymin);
                _mappedPoints[idx] = new Complex(_mappedX, _mappedY);
                idx++;
            }
            return _mappedPoints;
        }

        public static Quadrant QuadrantSelector(int quadrant)
        {
            Quadrant _selectedQuadrant = new Quadrant();

            switch (quadrant)
            {
                case 1:
                    _selectedQuadrant = ComplexPlane.FirstQuadrant;
                    break;
                case 2:
                    _selectedQuadrant = ComplexPlane.SecondQuadrant;
                    break;
                case 3:
                    _selectedQuadrant = ComplexPlane.ThirdQuadrant;
                    break;
                case 4:
                    _selectedQuadrant = ComplexPlane.FourthQuadrant;
                    break;
            }
            return _selectedQuadrant;
        }

        public static void ThreadCalculationJob(object obj)
        {
            Quadrant _actualQuadrant = QuadrantSelector((int)obj);

            GetBasicCoordinates(ComplexPlane, BitmapPixels, _actualQuadrant);
            GetMappedCoordinates(ComplexPlane, ComplexCoordinates, _actualQuadrant);
            GetMandelbrotData(ComplexPlane, MandelbrotPixels, _actualQuadrant);

        }

        public static ThreadPriority[] PriorityManager(Priorities threadPriority, bool multiThreading)
        {
            if (!multiThreading)
            {

                ThreadPriority[] _tSinglePriority = new ThreadPriority[1];

                switch (threadPriority)
                {
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

        public static MandelbrotPixel[] CalculateParallelThreads(Priorities threadPriority)
        {
            /* Working threads: one for each quadrant */
            Thread[] _tQuadrantThreads = new Thread[4];
            ThreadPriority[] _priorities = PriorityManager(threadPriority, true);

            for (int i = 0; i < ComplexPlane.QuadrantCount; i++)
            {
                /* Create and start working thread with the chosen priority */
                _tQuadrantThreads[i] = new Thread(ThreadCalculationJob)
                {
                    Priority = _priorities[i]
                };
                _tQuadrantThreads[i].Start(i + 1);
            }

            /* Wait threads to finish job */
            _tQuadrantThreads[0].Join(); _tQuadrantThreads[1].Join(); _tQuadrantThreads[2].Join(); _tQuadrantThreads[3].Join();

            return MandelbrotPixels;
        }

        async public static Task<MandelbrotPixel[]> CalculateParallelTasksAsync()
        {
            /* Create calculation task list */
            List<Task<int>> BasicCoordinatesTasks = new List<Task<int>>();
            List<Task<int>> MappedCoordinatesTasks = new List<Task<int>>();
            List<Task<int>> MandelbrotDataTasks = new List<Task<int>>();

            /* Add calculation tasks to the lists */
            int basic_idx = 0;
            int complex_idx = 0;
            int data_idx = 0;
            for (int i = 1; i <= ComplexPlane.QuadrantCount; i++)
            {
                Quadrant _actualQuadrant = QuadrantSelector(i);

                BasicCoordinatesTasks.Add(new Task<int>(() => {
                    GetBasicCoordinates(ComplexPlane, BitmapPixels, _actualQuadrant);
                    return basic_idx++;
                }, TaskCreationOptions.None));

                MappedCoordinatesTasks.Add(new Task<int>(() => {
                    GetMappedCoordinates(ComplexPlane, ComplexCoordinates, _actualQuadrant);
                    return complex_idx++;
                }, TaskCreationOptions.None));

                MandelbrotDataTasks.Add(new Task<int>(() => {
                    GetMandelbrotData(ComplexPlane, MandelbrotPixels, _actualQuadrant);
                    return data_idx++;
                }, TaskCreationOptions.None));
            }

            /* Start first session taks */
            foreach (Task t in BasicCoordinatesTasks)
            {
                t.Start();
            }

            /* Fire second session task if any first session task is ready */
            while (BasicCoordinatesTasks.Any())
            {
                Task<int> finishedTaskFirstSession = await Task.WhenAny(BasicCoordinatesTasks);
                int _quadrantReadyFirstSession = finishedTaskFirstSession.Result;
                Trace.WriteLine("First session finished Quadrant ID: " + _quadrantReadyFirstSession.ToString());
                MappedCoordinatesTasks[_quadrantReadyFirstSession].Start();
                BasicCoordinatesTasks.Remove(finishedTaskFirstSession);
            }

            /* Fire third session task if any second session task is ready */
            while (MappedCoordinatesTasks.Any())
            {
                Task<int> finishedTaskSecondSession = await Task.WhenAny(MappedCoordinatesTasks);
                int _quadrantReadySecondSession = finishedTaskSecondSession.Result;
                Trace.WriteLine("Second session finished Quadrant ID: " + _quadrantReadySecondSession.ToString());
                MandelbrotDataTasks[_quadrantReadySecondSession].Start();
                MappedCoordinatesTasks.Remove(finishedTaskSecondSession);
            }

            /* Get third session ready */
            await Task.WhenAll(MandelbrotDataTasks.ToArray());

            return MandelbrotPixels;
        }

        public static void ThreadPoolCalculationJob(object Idx)
        {
            Point[] _bitmapPixels = GetBasicCoordinates((int)Idx, threadPoolGranularity);
            Complex[] _complexPoints = GetMappedCoordinates(_bitmapPixels);
            List<MandelbrotPixel> _threadCalculatedMandelbrotPixels = new List<MandelbrotPixel>();

            int _pointidx = 0;
            foreach (var cpoint in _complexPoints)
            {
                int _iterCount = GetIterCount(cpoint, ComplexPlane);
                _threadCalculatedMandelbrotPixels.Add(new MandelbrotPixel(_bitmapPixels[_pointidx++], _iterCount));
            }

            bool lockTaken = false;
            try
            {
                /* When thread calculation is ready add result to the shared list */
                Monitor.Enter(MandelbrotPixels, ref lockTaken);
                MandelbrotPixelsList.AddRange(_threadCalculatedMandelbrotPixels);

                /* When all job done, signal to the boss */
                if (Interlocked.Decrement(ref threadPoolTaskCount) == 0)
                {
                    signalThreadPoolJobReady.Set();
                }
            }
            finally { if (lockTaken) Monitor.Exit(MandelbrotPixels); }
        }

        public static MandelbrotPixel[] CalculateParallelThreadPool(int selectedGranularity)
        {
            /* Get available max threads and set */
            ThreadPool.GetMaxThreads(out int _workerthreads, out int _completionthreads);
            ThreadPool.SetMaxThreads(_workerthreads, _completionthreads);
            Trace.WriteLine("Workerthreads: " + _workerthreads.ToString() + ", Completionthreads: " + _completionthreads.ToString());

            /* Reset thread shared variables */
            MandelbrotPixelsList.Clear();
            signalThreadPoolJobReady.Reset();
            if (selectedGranularity == 1 || selectedGranularity == ComplexPlane.Scale) {
                threadPoolGranularity = (ComplexPlane.Scale + 1) - selectedGranularity;
            }else
                threadPoolGranularity = (ComplexPlane.Scale  / selectedGranularity);
            threadPoolTaskCount = ComplexPlane.Scale / threadPoolGranularity;

            for (int i = 0; i < ComplexPlane.Scale; i += threadPoolGranularity)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolCalculationJob), (i));
            }

            /* Wait for ThreadPool finished job event */
            signalThreadPoolJobReady.WaitOne();

            return MandelbrotPixelsList.ToArray();
        }
    }
}
