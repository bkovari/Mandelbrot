namespace Mandelbrot
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGenerateSequential = new System.Windows.Forms.Button();
            this.lblYmax = new System.Windows.Forms.Label();
            this.lblYmin = new System.Windows.Forms.Label();
            this.lblXmin = new System.Windows.Forms.Label();
            this.lblXmax = new System.Windows.Forms.Label();
            this.lblReal = new System.Windows.Forms.Label();
            this.lblImag = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.grpBoxSequential = new System.Windows.Forms.GroupBox();
            this.lblxCalculationTimeSequential = new System.Windows.Forms.Label();
            this.lblCalculationTimeSequential = new System.Windows.Forms.Label();
            this.grpBoxParallel = new System.Windows.Forms.GroupBox();
            this.rdoBtnTasks = new System.Windows.Forms.RadioButton();
            this.rdoBtnManualThreads = new System.Windows.Forms.RadioButton();
            this.lblxCalculationTimeParallel = new System.Windows.Forms.Label();
            this.lblCalculationTimeParallel = new System.Windows.Forms.Label();
            this.btnGenerateParallel = new System.Windows.Forms.Button();
            this.cmBoxResolution = new System.Windows.Forms.ComboBox();
            this.cmBoxColoring = new System.Windows.Forms.ComboBox();
            this.grpBoxSettings = new System.Windows.Forms.GroupBox();
            this.lblThreadPriority = new System.Windows.Forms.Label();
            this.cmBoxThreadPriority = new System.Windows.Forms.ComboBox();
            this.lblColoring = new System.Windows.Forms.Label();
            this.lblResolution = new System.Windows.Forms.Label();
            this.rdoBtnThreadPool = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpBoxSequential.SuspendLayout();
            this.grpBoxParallel.SuspendLayout();
            this.grpBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(23, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 800);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnGenerateSequential
            // 
            this.btnGenerateSequential.Location = new System.Drawing.Point(29, 29);
            this.btnGenerateSequential.Name = "btnGenerateSequential";
            this.btnGenerateSequential.Size = new System.Drawing.Size(96, 31);
            this.btnGenerateSequential.TabIndex = 1;
            this.btnGenerateSequential.Text = "Generate";
            this.btnGenerateSequential.UseVisualStyleBackColor = true;
            this.btnGenerateSequential.Click += new System.EventHandler(this.btnGenSequential_Click);
            // 
            // lblYmax
            // 
            this.lblYmax.AutoSize = true;
            this.lblYmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblYmax.Location = new System.Drawing.Point(440, 26);
            this.lblYmax.Name = "lblYmax";
            this.lblYmax.Size = new System.Drawing.Size(15, 15);
            this.lblYmax.TabIndex = 2;
            this.lblYmax.Text = "2";
            // 
            // lblYmin
            // 
            this.lblYmin.AutoSize = true;
            this.lblYmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblYmin.Location = new System.Drawing.Point(440, 809);
            this.lblYmin.Name = "lblYmin";
            this.lblYmin.Size = new System.Drawing.Size(20, 15);
            this.lblYmin.TabIndex = 3;
            this.lblYmin.Text = "-2";
            this.lblYmin.Click += new System.EventHandler(this.lblDownLimit_Click);
            // 
            // lblXmin
            // 
            this.lblXmin.AutoSize = true;
            this.lblXmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblXmin.Location = new System.Drawing.Point(27, 395);
            this.lblXmin.Name = "lblXmin";
            this.lblXmin.Size = new System.Drawing.Size(20, 15);
            this.lblXmin.TabIndex = 4;
            this.lblXmin.Text = "-2";
            this.lblXmin.Click += new System.EventHandler(this.lblXmin_Click);
            // 
            // lblXmax
            // 
            this.lblXmax.AutoSize = true;
            this.lblXmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblXmax.Location = new System.Drawing.Point(808, 393);
            this.lblXmax.Name = "lblXmax";
            this.lblXmax.Size = new System.Drawing.Size(15, 15);
            this.lblXmax.TabIndex = 5;
            this.lblXmax.Text = "2";
            this.lblXmax.Click += new System.EventHandler(this.lblXmax_Click);
            // 
            // lblReal
            // 
            this.lblReal.AutoSize = true;
            this.lblReal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblReal.Location = new System.Drawing.Point(798, 442);
            this.lblReal.Name = "lblReal";
            this.lblReal.Size = new System.Drawing.Size(25, 15);
            this.lblReal.TabIndex = 6;
            this.lblReal.Text = "Re";
            this.lblReal.Click += new System.EventHandler(this.lblReal_Click);
            // 
            // lblImag
            // 
            this.lblImag.AutoSize = true;
            this.lblImag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblImag.Location = new System.Drawing.Point(392, 26);
            this.lblImag.Name = "lblImag";
            this.lblImag.Size = new System.Drawing.Size(23, 15);
            this.lblImag.TabIndex = 7;
            this.lblImag.Text = "Im";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(877, 578);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 27);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(876, 611);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(96, 27);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click_1);
            // 
            // grpBoxSequential
            // 
            this.grpBoxSequential.Controls.Add(this.lblxCalculationTimeSequential);
            this.grpBoxSequential.Controls.Add(this.lblCalculationTimeSequential);
            this.grpBoxSequential.Controls.Add(this.btnGenerateSequential);
            this.grpBoxSequential.Location = new System.Drawing.Point(848, 26);
            this.grpBoxSequential.Name = "grpBoxSequential";
            this.grpBoxSequential.Size = new System.Drawing.Size(150, 137);
            this.grpBoxSequential.TabIndex = 10;
            this.grpBoxSequential.TabStop = false;
            this.grpBoxSequential.Text = "Sequential Method";
            this.grpBoxSequential.Enter += new System.EventHandler(this.grpBoxSequential_Enter);
            // 
            // lblxCalculationTimeSequential
            // 
            this.lblxCalculationTimeSequential.AutoSize = true;
            this.lblxCalculationTimeSequential.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblxCalculationTimeSequential.ForeColor = System.Drawing.Color.Blue;
            this.lblxCalculationTimeSequential.Location = new System.Drawing.Point(69, 108);
            this.lblxCalculationTimeSequential.Name = "lblxCalculationTimeSequential";
            this.lblxCalculationTimeSequential.Size = new System.Drawing.Size(14, 17);
            this.lblxCalculationTimeSequential.TabIndex = 3;
            this.lblxCalculationTimeSequential.Text = "-";
            // 
            // lblCalculationTimeSequential
            // 
            this.lblCalculationTimeSequential.AutoSize = true;
            this.lblCalculationTimeSequential.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCalculationTimeSequential.Location = new System.Drawing.Point(24, 83);
            this.lblCalculationTimeSequential.Name = "lblCalculationTimeSequential";
            this.lblCalculationTimeSequential.Size = new System.Drawing.Size(84, 13);
            this.lblCalculationTimeSequential.TabIndex = 2;
            this.lblCalculationTimeSequential.Text = "Calculation time:";
            // 
            // grpBoxParallel
            // 
            this.grpBoxParallel.Controls.Add(this.rdoBtnThreadPool);
            this.grpBoxParallel.Controls.Add(this.rdoBtnTasks);
            this.grpBoxParallel.Controls.Add(this.rdoBtnManualThreads);
            this.grpBoxParallel.Controls.Add(this.lblxCalculationTimeParallel);
            this.grpBoxParallel.Controls.Add(this.lblCalculationTimeParallel);
            this.grpBoxParallel.Controls.Add(this.btnGenerateParallel);
            this.grpBoxParallel.Location = new System.Drawing.Point(848, 169);
            this.grpBoxParallel.Name = "grpBoxParallel";
            this.grpBoxParallel.Size = new System.Drawing.Size(150, 203);
            this.grpBoxParallel.TabIndex = 11;
            this.grpBoxParallel.TabStop = false;
            this.grpBoxParallel.Text = "Parallel Methods";
            this.grpBoxParallel.Enter += new System.EventHandler(this.grpBoxParallel_Enter);
            // 
            // rdoBtnTasks
            // 
            this.rdoBtnTasks.AutoSize = true;
            this.rdoBtnTasks.Location = new System.Drawing.Point(27, 54);
            this.rdoBtnTasks.Name = "rdoBtnTasks";
            this.rdoBtnTasks.Size = new System.Drawing.Size(86, 17);
            this.rdoBtnTasks.TabIndex = 16;
            this.rdoBtnTasks.TabStop = true;
            this.rdoBtnTasks.Text = "Async Tasks";
            this.rdoBtnTasks.UseVisualStyleBackColor = true;
            this.rdoBtnTasks.CheckedChanged += new System.EventHandler(this.rdoBtnTasks_CheckedChanged);
            // 
            // rdoBtnManualThreads
            // 
            this.rdoBtnManualThreads.AutoSize = true;
            this.rdoBtnManualThreads.Checked = true;
            this.rdoBtnManualThreads.Location = new System.Drawing.Point(27, 31);
            this.rdoBtnManualThreads.Name = "rdoBtnManualThreads";
            this.rdoBtnManualThreads.Size = new System.Drawing.Size(102, 17);
            this.rdoBtnManualThreads.TabIndex = 15;
            this.rdoBtnManualThreads.TabStop = true;
            this.rdoBtnManualThreads.Text = "Manual Threads";
            this.rdoBtnManualThreads.UseVisualStyleBackColor = true;
            this.rdoBtnManualThreads.CheckedChanged += new System.EventHandler(this.rdoBtnThreads_CheckedChanged);
            // 
            // lblxCalculationTimeParallel
            // 
            this.lblxCalculationTimeParallel.AutoSize = true;
            this.lblxCalculationTimeParallel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblxCalculationTimeParallel.ForeColor = System.Drawing.Color.Blue;
            this.lblxCalculationTimeParallel.Location = new System.Drawing.Point(69, 171);
            this.lblxCalculationTimeParallel.Name = "lblxCalculationTimeParallel";
            this.lblxCalculationTimeParallel.Size = new System.Drawing.Size(14, 17);
            this.lblxCalculationTimeParallel.TabIndex = 14;
            this.lblxCalculationTimeParallel.Text = "-";
            // 
            // lblCalculationTimeParallel
            // 
            this.lblCalculationTimeParallel.AutoSize = true;
            this.lblCalculationTimeParallel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCalculationTimeParallel.Location = new System.Drawing.Point(29, 147);
            this.lblCalculationTimeParallel.Name = "lblCalculationTimeParallel";
            this.lblCalculationTimeParallel.Size = new System.Drawing.Size(84, 13);
            this.lblCalculationTimeParallel.TabIndex = 13;
            this.lblCalculationTimeParallel.Text = "Calculation time:";
            // 
            // btnGenerateParallel
            // 
            this.btnGenerateParallel.Location = new System.Drawing.Point(29, 107);
            this.btnGenerateParallel.Name = "btnGenerateParallel";
            this.btnGenerateParallel.Size = new System.Drawing.Size(96, 32);
            this.btnGenerateParallel.TabIndex = 12;
            this.btnGenerateParallel.Text = "Generate";
            this.btnGenerateParallel.UseVisualStyleBackColor = true;
            this.btnGenerateParallel.Click += new System.EventHandler(this.btnGenerateParallel_Click);
            // 
            // cmBoxResolution
            // 
            this.cmBoxResolution.FormattingEnabled = true;
            this.cmBoxResolution.Items.AddRange(new object[] {
            "800x800",
            "600x600"});
            this.cmBoxResolution.Location = new System.Drawing.Point(28, 46);
            this.cmBoxResolution.Name = "cmBoxResolution";
            this.cmBoxResolution.Size = new System.Drawing.Size(95, 21);
            this.cmBoxResolution.TabIndex = 12;
            // 
            // cmBoxColoring
            // 
            this.cmBoxColoring.DisplayMember = "Colored";
            this.cmBoxColoring.FormattingEnabled = true;
            this.cmBoxColoring.Items.AddRange(new object[] {
            "Black & White",
            "Colored"});
            this.cmBoxColoring.Location = new System.Drawing.Point(28, 97);
            this.cmBoxColoring.Name = "cmBoxColoring";
            this.cmBoxColoring.Size = new System.Drawing.Size(96, 21);
            this.cmBoxColoring.TabIndex = 13;
            this.cmBoxColoring.ValueMember = "Colored";
            // 
            // grpBoxSettings
            // 
            this.grpBoxSettings.Controls.Add(this.lblThreadPriority);
            this.grpBoxSettings.Controls.Add(this.cmBoxThreadPriority);
            this.grpBoxSettings.Controls.Add(this.lblColoring);
            this.grpBoxSettings.Controls.Add(this.lblResolution);
            this.grpBoxSettings.Controls.Add(this.cmBoxResolution);
            this.grpBoxSettings.Controls.Add(this.cmBoxColoring);
            this.grpBoxSettings.Location = new System.Drawing.Point(848, 378);
            this.grpBoxSettings.Name = "grpBoxSettings";
            this.grpBoxSettings.Size = new System.Drawing.Size(149, 194);
            this.grpBoxSettings.TabIndex = 14;
            this.grpBoxSettings.TabStop = false;
            this.grpBoxSettings.Text = "Settings";
            // 
            // lblThreadPriority
            // 
            this.lblThreadPriority.AutoSize = true;
            this.lblThreadPriority.Location = new System.Drawing.Point(26, 132);
            this.lblThreadPriority.Name = "lblThreadPriority";
            this.lblThreadPriority.Size = new System.Drawing.Size(75, 13);
            this.lblThreadPriority.TabIndex = 17;
            this.lblThreadPriority.Text = "Thread Priority";
            this.lblThreadPriority.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmBoxThreadPriority
            // 
            this.cmBoxThreadPriority.FormattingEnabled = true;
            this.cmBoxThreadPriority.Items.AddRange(new object[] {
            "Default",
            "Highest",
            "Lowest"});
            this.cmBoxThreadPriority.Location = new System.Drawing.Point(28, 148);
            this.cmBoxThreadPriority.Name = "cmBoxThreadPriority";
            this.cmBoxThreadPriority.Size = new System.Drawing.Size(94, 21);
            this.cmBoxThreadPriority.TabIndex = 16;
            // 
            // lblColoring
            // 
            this.lblColoring.AutoSize = true;
            this.lblColoring.Location = new System.Drawing.Point(26, 81);
            this.lblColoring.Name = "lblColoring";
            this.lblColoring.Size = new System.Drawing.Size(45, 13);
            this.lblColoring.TabIndex = 15;
            this.lblColoring.Text = "Coloring";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(26, 30);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(57, 13);
            this.lblResolution.TabIndex = 14;
            this.lblResolution.Text = "Resolution";
            // 
            // rdoBtnThreadPool
            // 
            this.rdoBtnThreadPool.AutoSize = true;
            this.rdoBtnThreadPool.Location = new System.Drawing.Point(27, 77);
            this.rdoBtnThreadPool.Name = "rdoBtnThreadPool";
            this.rdoBtnThreadPool.Size = new System.Drawing.Size(80, 17);
            this.rdoBtnThreadPool.TabIndex = 17;
            this.rdoBtnThreadPool.TabStop = true;
            this.rdoBtnThreadPool.Text = "ThreadPool";
            this.rdoBtnThreadPool.UseVisualStyleBackColor = true;
            this.rdoBtnThreadPool.CheckedChanged += new System.EventHandler(this.rdoBtnThreadPool_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 861);
            this.Controls.Add(this.grpBoxSettings);
            this.Controls.Add(this.grpBoxParallel);
            this.Controls.Add(this.grpBoxSequential);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblImag);
            this.Controls.Add(this.lblReal);
            this.Controls.Add(this.lblXmax);
            this.Controls.Add(this.lblXmin);
            this.Controls.Add(this.lblYmin);
            this.Controls.Add(this.lblYmax);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mandelbrot Fractal Representation";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpBoxSequential.ResumeLayout(false);
            this.grpBoxSequential.PerformLayout();
            this.grpBoxParallel.ResumeLayout(false);
            this.grpBoxParallel.PerformLayout();
            this.grpBoxSettings.ResumeLayout(false);
            this.grpBoxSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGenerateSequential;
        private System.Windows.Forms.Label lblYmax;
        private System.Windows.Forms.Label lblYmin;
        private System.Windows.Forms.Label lblXmin;
        private System.Windows.Forms.Label lblXmax;
        private System.Windows.Forms.Label lblReal;
        private System.Windows.Forms.Label lblImag;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox grpBoxSequential;
        private System.Windows.Forms.GroupBox grpBoxParallel;
        private System.Windows.Forms.Button btnGenerateParallel;
        private System.Windows.Forms.Label lblxCalculationTimeSequential;
        private System.Windows.Forms.Label lblCalculationTimeSequential;
        private System.Windows.Forms.Label lblxCalculationTimeParallel;
        private System.Windows.Forms.Label lblCalculationTimeParallel;
        private System.Windows.Forms.ComboBox cmBoxResolution;
        private System.Windows.Forms.ComboBox cmBoxColoring;
        private System.Windows.Forms.GroupBox grpBoxSettings;
        private System.Windows.Forms.Label lblColoring;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.Label lblThreadPriority;
        private System.Windows.Forms.ComboBox cmBoxThreadPriority;
        private System.Windows.Forms.RadioButton rdoBtnManualThreads;
        private System.Windows.Forms.RadioButton rdoBtnTasks;
        private System.Windows.Forms.RadioButton rdoBtnThreadPool;
    }
}

