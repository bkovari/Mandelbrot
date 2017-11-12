using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mandelbrot.Compute;
using System.Diagnostics;
using System.Threading;

namespace Mandelbrot
{
    public partial class MainForm : Form
    {
        /* Environment globals */
        static internal CoordinateSystem ComplexPlane = new CoordinateSystem(-2, 2, -2, 2, 800);
        static internal CoordinateVisualize ComplexImage = new CoordinateVisualize(ComplexPlane);
        static internal bool HighResolutionLaunch { get; set; } = false;

        private enum Resolutions { r600x600, r800x800 };
        private enum Coloring { BlacknWhite, Colored };
        public enum Priorities { Default, Highest, Lowest };
        public enum Granularity { Div1, Div2, Div5, Div10, Div50, Div100, Div200, DivHalfMax, DivMax }

        private const int LowResolutionLaunchHeightTreshold = 700;
        private const int HighResolutionLaunchHeightTreshold = 900;

        private struct Monitorsize
        {
            public readonly int Width;
            public readonly int Height;

            public Monitorsize(int width, int height) {
                Width = width;
                Height = height;
            }
        }

        static private Monitorsize GetMonitorSize()
        {
            int _screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int _screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Monitorsize actualResolution = new Monitorsize(_screenWidth, _screenHeight);

            return actualResolution;
        }

        private void SetAxeLabelPositions(CoordinateVisualize coordinateVisualize)
        {
            Label[] _axeLabels = new Label[] {
                lblYmax, lblYmin, lblXmax, lblXmin, lblImag, lblReal
            };

            Point[] _800x800LblPos = {
                new Point(440,26),
                new Point(440,809),
                new Point(808,393),
                new Point(27,395),
                new Point(392,26),
                new Point(798,442)
            };

            Point[] _600x600LblPos = {
                new Point(330,28),
                new Point(330,611),
                new Point(609,296),
                new Point(24,296),
                new Point(290,28),
                new Point(599,335)
            };

            Point[] currentLblPos = (ComplexImage.Scale == 800) ? _800x800LblPos : _600x600LblPos;
            int index = 0;
            foreach (var lblPos in currentLblPos)
            {
                _axeLabels[index].Location = lblPos;
                index++;
            }
        }

        private void SetAxeLabelColors(CoordinateVisualize coordinateVisualize, bool resetRequest)
        {
            Label[] _axeLabels = new Label[] {
                lblYmax, lblYmin, lblXmax, lblXmin, lblImag, lblReal
            };

            Color actualColor = (coordinateVisualize.IsColorful) ? Color.Purple : SystemColors.Control;
            actualColor = (resetRequest) ? SystemColors.Control : actualColor;

            for (int i = 0; i < _axeLabels.Length; i++) {
                _axeLabels[i].BackColor = actualColor;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            /* Get user monitor size */
            Monitorsize Monitor = GetMonitorSize();
            HighResolutionLaunch = (Monitor.Width > this.Size.Width && Monitor.Height > this.Size.Height) ? true : false;
            
            /* Limit resolution to 600 px on small screen */
            if (!HighResolutionLaunch)
            {
                ComplexImage.Resolution = 600;
                ComplexImage.SetScale(600);
                ComplexPlane.SetScale(600);
                SetAxeLabelPositions(ComplexImage);
                SetAxeLabelColors(ComplexImage, false);
                pictureBox1.Refresh();
                cmBoxResolution.Items.Remove("800x800");
                cmBoxGranularity.Items.Add("300");
                cmBoxGranularity.Items.Add("600");
                this.Height = LowResolutionLaunchHeightTreshold;
            }
            else {
                ComplexImage.Resolution = 800;
                ComplexImage.SetScale(800);
                ComplexPlane.SetScale(800);
                SetAxeLabelPositions(ComplexImage);
                SetAxeLabelColors(ComplexImage, false);
                pictureBox1.Refresh();
                cmBoxResolution.Items.Remove("600x600");
                cmBoxGranularity.Items.Add("400");
                cmBoxGranularity.Items.Add("800");
                this.Height = HighResolutionLaunchHeightTreshold;
            }

            /* Initialize own form components */
            pictureBox1.Width = ComplexPlane.Scale + 1;
            pictureBox1.Height = ComplexPlane.Scale + 1;
            ComplexImage.DrawCoordinateSystem();
            pictureBox1.Image = ComplexImage.DrawingSheet;

            lblYmin.Text = ComplexImage.Ymin.ToString();
            lblYmax.Text = ComplexImage.Ymax.ToString();
            lblXmin.Text = ComplexImage.Xmin.ToString();
            lblXmax.Text = ComplexImage.Xmax.ToString();

            cmBoxResolution.Enabled = true;
            cmBoxGranularity.Enabled = false;
            cmBoxColoring.SelectedIndex = (int)Coloring.BlacknWhite;
            cmBoxResolution.SelectedIndex = (int)Resolutions.r600x600;
            cmBoxThreadPriority.SelectedIndex = (int)Priorities.Default;
            cmBoxGranularity.SelectedIndex = (int)Granularity.DivMax;
        }

        private void btnGenSequential_Click(object sender, EventArgs e)
        {
            /* Adjust user selected color */
            ComplexImage.IsColorful = (cmBoxColoring.SelectedIndex == (int)Coloring.Colored) ? true : false;
            
            /* Refresh drawing area */
            ClearDrawing();

            /* Create Stopwatch for time measuring */
            Stopwatch _sequentialCalculationTimer = new Stopwatch();
            Stopwatch _sequentialDisplayTimer = new Stopwatch();

            /* Measure pure calculation time */
            _sequentialCalculationTimer.Start();
            MandelbrotPixel[] Fractal = Compute.CalculateSequential(ComplexPlane, (Priorities)cmBoxThreadPriority.SelectedIndex);
            _sequentialCalculationTimer.Stop();

            /* Draw fractal */
            ComplexImage.DrawMandelbrotFractal(Fractal);

            /* Set label to calculation values */
            lblxCalculationTimeSequential.Text = _sequentialCalculationTimer.ElapsedMilliseconds.ToString() + " ms";

            /* Show image with coordinate system */
            ComplexImage.DrawCoordinateSystem();
            SetAxeLabelPositions(ComplexImage);
            SetAxeLabelColors(ComplexImage, false);
            pictureBox1.Image = ComplexImage.DrawingSheet;

            /* Enable Thread combobox */
            cmBoxThreadPriority.Enabled = true;
        }

        async private void btnGenerateParallel_Click(object sender, EventArgs e)
        {
            MandelbrotPixel[] Fractal;

            /* Adjust user selected color */
            ComplexImage.IsColorful = (cmBoxColoring.SelectedIndex == (int)Coloring.Colored) ? true : false;

            /* Refresh drawing area */
            ClearDrawing();

            /* Create timer for calculation time measurement */
            Stopwatch _parallelCalculationTimer = new Stopwatch();

            if (rdoBtnManualThreads.Checked == true)
            {
                /* MANUAL THREADS: Meausure pure calculation time */
                _parallelCalculationTimer.Reset();
                _parallelCalculationTimer.Start();
                Fractal = Compute.CalculateParallelThreads((Priorities)cmBoxThreadPriority.SelectedIndex);
                _parallelCalculationTimer.Stop();
            }
            else if (rdoBtnThreadPool.Checked == true) {

                /* THREADPOOL: Meausure pure calculation time */
                _parallelCalculationTimer.Reset();
                _parallelCalculationTimer.Start();
                int _selectedVal = int.Parse((string)cmBoxGranularity.SelectedItem);
                Fractal = Compute.CalculateParallelThreadPool(_selectedVal);
                _parallelCalculationTimer.Stop();
            }
            else
            {
                /* ASYNC TASKS: Measure pure calculation time */
                _parallelCalculationTimer.Reset();
                _parallelCalculationTimer.Start();
                Fractal = await Compute.CalculateParallelTasksAsync();
                _parallelCalculationTimer.Stop();
            }

            /* Draw fractal*/
            ComplexImage.DrawMandelbrotFractal(Fractal);

            /* Show elapsed time */
            if (_parallelCalculationTimer.ElapsedMilliseconds == 0) {
                double _timeval = ((double)_parallelCalculationTimer.ElapsedTicks / (double)10000);
                lblxCalculationTimeParallel.Text = _timeval.ToString("0.00") + " ms";
            }
            else
                lblxCalculationTimeParallel.Text = _parallelCalculationTimer.ElapsedMilliseconds.ToString() + " ms";

            /* Show image with coordinate system */
            ComplexImage.DrawCoordinateSystem();
            SetAxeLabelPositions(ComplexImage);
            SetAxeLabelColors(ComplexImage, false);
            pictureBox1.Image = ComplexImage.DrawingSheet;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDrawing();
        }

        private void btnAbout_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Home assignment for Software Development to Parallel Architectures" +
                            Environment.NewLine +
                            "University of Obuda John von Neumann Faculty of Informatics" +
                            Environment.NewLine +
                            "Made by Bence Kovari\n",
                            "About",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void ClearDrawing()
        {
            ComplexImage.DrawEmptyImage();
            ComplexImage.DrawCoordinateSystem();
            SetAxeLabelColors(ComplexImage, true);
            pictureBox1.Image = ComplexImage.DrawingSheet;
        }

        private void ParallelSettingsOptionManager()
        {
            if (rdoBtnManualThreads.Checked == true)
            {
                cmBoxThreadPriority.Enabled = true;
                cmBoxGranularity.Enabled = false;
            }

            if (rdoBtnTasks.Checked == true)
            {
                cmBoxThreadPriority.Enabled = false;
                cmBoxGranularity.Enabled = false;
            }

            if (rdoBtnThreadPool.Checked == true)
            {
                cmBoxThreadPriority.Enabled = false;
                cmBoxGranularity.Enabled = true;
            }
        }

        private void rdoBtnThreads_CheckedChanged(object sender, EventArgs e)
        {
            ParallelSettingsOptionManager();
        }

        private void rdoBtnTasks_CheckedChanged(object sender, EventArgs e)
        {
            ParallelSettingsOptionManager();
        }

        private void rdoBtnThreadPool_CheckedChanged(object sender, EventArgs e)
        {
            ParallelSettingsOptionManager();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void lblXmax_Click(object sender, EventArgs e)
        {
        }
        private void lblDownLimit_Click(object sender, EventArgs e)
        {
        }
        private void lblXmin_Click(object sender, EventArgs e)
        {
        }
        private void lblReal_Click(object sender, EventArgs e)
        {
        }
        private void grpBoxSequential_Enter(object sender, EventArgs e)
        {
        }
        private void grpBoxParallel_Enter(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
