namespace ImageProcessingProject
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            menuStrip1 = new MenuStrip();
            dosyaToolStripMenuItem = new ToolStripMenuItem();
            resimYükleToolStripMenuItem1 = new ToolStripMenuItem();
            ikinciResmiYükleToolStripMenuItem = new ToolStripMenuItem();
            splitContainer2 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            picInput1 = new PictureBox();
            picInput2 = new PictureBox();
            picOutput = new PictureBox();
            pnlMasterParametre = new Panel();
            pnlHistogram = new Panel();
            histLabel = new Label();
            cmbHistogram = new ComboBox();
            tblHistograms = new TableLayoutPanel();
            lblHistOriginal = new Label();
            lblHistResult = new Label();
            picHistogram = new PictureBox();
            picHistogramResult = new PictureBox();
            pnlMatrixFilter = new Panel();
            lblMatrixTitle = new Label();
            lblMatrixSize = new Label();
            cmbMatrixSize = new ComboBox();
            pnlNoise = new Panel();
            lblNoisePercentage = new Label();
            trkNoisePercentage = new TrackBar();
            numNoisePercentage = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(trkNoisePercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numNoisePercentage)).BeginInit();
            lblNoiseMatrixSize = new Label();
            cmbNoiseMatrixSize = new ComboBox();
            rbNoiseAdd = new RadioButton();
            rbNoiseRemove = new RadioButton();
            lblNoiseAdd = new Label();
            cmbNoiseAdd = new ComboBox();
            lblNoiseRemove = new Label();
            cmbNoiseRemove = new ComboBox();
            pnlThresholding = new Panel();
            numThreshold = new NumericUpDown();            
            ((System.ComponentModel.ISupportInitialize)(numThreshold)).BeginInit();
            rbStaticThreshold = new RadioButton();
            rbDynamicThreshold = new RadioButton();
            esikdegerLabel = new Label();
            trkTreshold2 = new TrackBar();
            lblThresholdMatrix = new Label();
            cmbThresholdMatrix = new ComboBox();
            pnlBinary = new Panel();
            esikdegeri = new Label();
            trkThreshold = new TrackBar();
            pnlCrop = new Panel();
            width = new Label();
            higth = new Label();
            y = new Label();
            x = new Label();
            numCropHeight = new NumericUpDown();
            numCropWidth = new NumericUpDown();
            numCropY = new NumericUpDown();
            numCropX = new NumericUpDown();
            pnlRotate = new Panel();
            interpoletionLabel = new Label();
            acıLabel = new Label();
            cmbRotateInterpolation = new ComboBox();
            numAngle = new NumericUpDown();
            pnlScale = new Panel();
            interpoletionLabelScale = new Label();
            cmbScaleInterpolation = new ComboBox();
            scaleLabel = new Label();
            numScale = new NumericUpDown();
            pnlArithmetic = new Panel();
            islemLabel = new Label();
            cmbAritmetik = new ComboBox();
            pnlContrast = new Panel();
            konstrastLabel = new Label();
            trkContrast = new TrackBar();
            pnlMorphology = new Panel();
            lblMorphologyType = new Label();
            cmbMorphologyType = new ComboBox();
            lblMorphMatrixSize = new Label();
            cmbMorphMatrixSize = new ComboBox();
            lblMorphShape = new Label();
            cmbMorphShape = new ComboBox();
            gbMorphMatrix = new GroupBox();
            pnlUnsharp = new Panel();
            lblUnsharpMatrix = new Label();
            cmbUnsharpMatrix = new ComboBox();
            pnlEdgeDetection = new Panel();
            lblEdgeType = new Label();
            cmbEdgeType = new ComboBox();
            gbEdgeMatrix = new GroupBox();
            tlpEdgeMatrix = new TableLayoutPanel();
            pnlColorSpace = new Panel();
            lblColorSpace = new Label();
            cmbColorSpace = new ComboBox();
            btnUygula = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picInput1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInput2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOutput).BeginInit();
            pnlMasterParametre.SuspendLayout();
            pnlHistogram.SuspendLayout();
            tblHistograms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picHistogramResult).BeginInit();
            pnlMatrixFilter.SuspendLayout();
            pnlNoise.SuspendLayout();
            pnlThresholding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkTreshold2).BeginInit();
            pnlBinary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkThreshold).BeginInit();
            pnlCrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCropHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCropWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCropY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCropX).BeginInit();
            pnlRotate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAngle).BeginInit();
            pnlScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScale).BeginInit();
            pnlArithmetic.SuspendLayout();
            pnlContrast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkContrast).BeginInit();
            pnlMorphology.SuspendLayout();
            pnlUnsharp.SuspendLayout();
            pnlEdgeDetection.SuspendLayout();
            gbEdgeMatrix.SuspendLayout();
            pnlColorSpace.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(4, 5, 4, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1.Controls.Add(menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1714, 1333);
            splitContainer1.SplitterDistance = 429;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 35);
            treeView1.Margin = new Padding(4, 5, 4, 5);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(427, 1296);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dosyaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(9, 3, 0, 3);
            menuStrip1.Size = new Size(427, 35);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            dosyaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resimYükleToolStripMenuItem1, ikinciResmiYükleToolStripMenuItem });
            dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            dosyaToolStripMenuItem.Size = new Size(78, 29);
            dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // resimYükleToolStripMenuItem1
            // 
            resimYükleToolStripMenuItem1.Name = "resimYükleToolStripMenuItem1";
            resimYükleToolStripMenuItem1.Size = new Size(253, 34);
            resimYükleToolStripMenuItem1.Text = "Resim Yükle";
            resimYükleToolStripMenuItem1.Click += ResimYükleToolStripMenuItem1_Click;
            // 
            // ikinciResmiYükleToolStripMenuItem
            // 
            ikinciResmiYükleToolStripMenuItem.Name = "ikinciResmiYükleToolStripMenuItem";
            ikinciResmiYükleToolStripMenuItem.Size = new Size(253, 34);
            ikinciResmiYükleToolStripMenuItem.Text = "İkinci Resmi Yükle";
            ikinciResmiYükleToolStripMenuItem.Click += ikinciResmiYükleToolStripMenuItem_Click;
            // 
            // splitContainer2
            // 
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(4, 5, 4, 5);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(pnlMasterParametre);
            splitContainer2.Panel2.Controls.Add(btnUygula);
            splitContainer2.Panel2.Padding = new Padding(14, 17, 14, 17);
            splitContainer2.Size = new Size(1279, 1333);
            splitContainer2.SplitterDistance = 749;
            splitContainer2.SplitterWidth = 7;
            splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            tableLayoutPanel1.Controls.Add(picInput1, 0, 0);
            tableLayoutPanel1.Controls.Add(picInput2, 1, 0);
            tableLayoutPanel1.Controls.Add(picOutput, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(21, 25, 21, 25);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1277, 747);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // picInput1
            // 
            picInput1.BorderStyle = BorderStyle.FixedSingle;
            picInput1.Dock = DockStyle.Fill;
            picInput1.Location = new Point(25, 30);
            picInput1.Margin = new Padding(4, 5, 4, 5);
            picInput1.Name = "picInput1";
            picInput1.Size = new Size(403, 687);
            picInput1.SizeMode = PictureBoxSizeMode.Zoom;
            picInput1.TabIndex = 0;
            picInput1.TabStop = false;
            // 
            // picInput2
            // 
            picInput2.BorderStyle = BorderStyle.FixedSingle;
            picInput2.Dock = DockStyle.Fill;
            picInput2.Location = new Point(436, 30);
            picInput2.Margin = new Padding(4, 5, 4, 5);
            picInput2.Name = "picInput2";
            picInput2.Size = new Size(403, 687);
            picInput2.SizeMode = PictureBoxSizeMode.Zoom;
            picInput2.TabIndex = 1;
            picInput2.TabStop = false;
            // 
            // picOutput
            // 
            picOutput.BorderStyle = BorderStyle.FixedSingle;
            picOutput.Dock = DockStyle.Fill;
            picOutput.Location = new Point(847, 30);
            picOutput.Margin = new Padding(4, 5, 4, 5);
            picOutput.Name = "picOutput";
            picOutput.Size = new Size(405, 687);
            picOutput.SizeMode = PictureBoxSizeMode.Zoom;
            picOutput.TabIndex = 2;
            picOutput.TabStop = false;
            // 
            // pnlMasterParametre
            // 
            pnlMasterParametre.Controls.Add(pnlHistogram);
            pnlMasterParametre.Controls.Add(pnlMatrixFilter);
            pnlMasterParametre.Controls.Add(pnlNoise);
            pnlMasterParametre.Controls.Add(pnlThresholding);
            pnlMasterParametre.Controls.Add(pnlBinary);
            pnlMasterParametre.Controls.Add(pnlCrop);
            pnlMasterParametre.Controls.Add(pnlRotate);
            pnlMasterParametre.Controls.Add(pnlScale);
            pnlMasterParametre.Controls.Add(pnlArithmetic);
            pnlMasterParametre.Controls.Add(pnlContrast);
            pnlMasterParametre.Controls.Add(pnlMorphology);
            pnlMasterParametre.Controls.Add(pnlUnsharp);
            pnlMasterParametre.Controls.Add(pnlEdgeDetection);
            pnlMasterParametre.Controls.Add(pnlColorSpace);
            pnlMasterParametre.Dock = DockStyle.Fill;
            pnlMasterParametre.Location = new Point(14, 17);
            pnlMasterParametre.Margin = new Padding(4, 5, 4, 5);
            pnlMasterParametre.Name = "pnlMasterParametre";
            pnlMasterParametre.Size = new Size(1249, 441);
            pnlMasterParametre.TabIndex = 10;
            // 
            // pnlHistogram
            // 
            pnlHistogram.Controls.Add(histLabel);
            pnlHistogram.Controls.Add(cmbHistogram);
            pnlHistogram.Controls.Add(tblHistograms);
            pnlHistogram.Dock = DockStyle.Fill;
            pnlHistogram.Location = new Point(0, 0);
            pnlHistogram.Margin = new Padding(4, 5, 4, 5);
            pnlHistogram.Name = "pnlHistogram";
            pnlHistogram.Size = new Size(1249, 441);
            pnlHistogram.TabIndex = 14;
            pnlHistogram.Visible = false;
            // 
            // histLabel
            // 
            histLabel.AutoSize = true;
            histLabel.Location = new Point(39, 45);
            histLabel.Margin = new Padding(4, 0, 4, 0);
            histLabel.Name = "histLabel";
            histLabel.Size = new Size(73, 25);
            histLabel.TabIndex = 1;
            histLabel.Text = "İşlemler";
            // 
            // cmbHistogram
            // 
            cmbHistogram.FormattingEnabled = true;
            cmbHistogram.Items.AddRange(new object[] { "Histogram Tablosu", "Histogram Germe", "Histogram Eşitleme" });
            cmbHistogram.Location = new Point(129, 40);
            cmbHistogram.Margin = new Padding(4, 5, 4, 5);
            cmbHistogram.Name = "cmbHistogram";
            cmbHistogram.Size = new Size(284, 33);
            cmbHistogram.TabIndex = 0;
            // 
            // tblHistograms
            // 
            tblHistograms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tblHistograms.ColumnCount = 2;
            tblHistograms.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblHistograms.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblHistograms.Controls.Add(lblHistOriginal, 0, 0);
            tblHistograms.Controls.Add(lblHistResult, 1, 0);
            tblHistograms.Controls.Add(picHistogram, 0, 1);
            tblHistograms.Controls.Add(picHistogramResult, 1, 1);
            tblHistograms.Location = new Point(39, 90);
            tblHistograms.Name = "tblHistograms";
            tblHistograms.RowCount = 2;
            tblHistograms.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tblHistograms.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblHistograms.Size = new Size(1170, 320);
            tblHistograms.TabIndex = 3;
            // 
            // lblHistOriginal
            // 
            lblHistOriginal.AutoSize = true;
            lblHistOriginal.Dock = DockStyle.Fill;
            lblHistOriginal.Location = new Point(3, 0);
            lblHistOriginal.Name = "lblHistOriginal";
            lblHistOriginal.Size = new Size(579, 30);
            lblHistOriginal.TabIndex = 3;
            lblHistOriginal.Text = "Orijinal Histogram";
            lblHistOriginal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHistResult
            // 
            lblHistResult.AutoSize = true;
            lblHistResult.Dock = DockStyle.Fill;
            lblHistResult.Location = new Point(588, 0);
            lblHistResult.Name = "lblHistResult";
            lblHistResult.Size = new Size(579, 30);
            lblHistResult.TabIndex = 4;
            lblHistResult.Text = "İşlenmiş Histogram";
            lblHistResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picHistogram
            // 
            picHistogram.BackColor = Color.White;
            picHistogram.BorderStyle = BorderStyle.FixedSingle;
            picHistogram.Dock = DockStyle.Fill;
            picHistogram.Location = new Point(4, 35);
            picHistogram.Margin = new Padding(4, 5, 4, 5);
            picHistogram.Name = "picHistogram";
            picHistogram.Size = new Size(577, 280);
            picHistogram.SizeMode = PictureBoxSizeMode.Zoom;
            picHistogram.TabIndex = 2;
            picHistogram.TabStop = false;
            // 
            // picHistogramResult
            // 
            picHistogramResult.BackColor = Color.White;
            picHistogramResult.BorderStyle = BorderStyle.FixedSingle;
            picHistogramResult.Dock = DockStyle.Fill;
            picHistogramResult.Location = new Point(589, 35);
            picHistogramResult.Margin = new Padding(4, 5, 4, 5);
            picHistogramResult.Name = "picHistogramResult";
            picHistogramResult.Size = new Size(577, 280);
            picHistogramResult.SizeMode = PictureBoxSizeMode.Zoom;
            picHistogramResult.TabIndex = 2;
            picHistogramResult.TabStop = false;
            // 
            // pnlMatrixFilter
            // 
            pnlMatrixFilter.Controls.Add(lblMatrixTitle);
            pnlMatrixFilter.Controls.Add(lblMatrixSize);
            pnlMatrixFilter.Controls.Add(cmbMatrixSize);
            pnlMatrixFilter.Dock = DockStyle.Fill;
            pnlMatrixFilter.Location = new Point(0, 0);
            pnlMatrixFilter.Margin = new Padding(4, 5, 4, 5);
            pnlMatrixFilter.Name = "pnlMatrixFilter";
            pnlMatrixFilter.Size = new Size(1249, 441);
            pnlMatrixFilter.TabIndex = 19;
            pnlMatrixFilter.Visible = false;
            // 
            // lblMatrixTitle
            // 
            lblMatrixTitle.AutoSize = true;
            lblMatrixTitle.Location = new Point(39, 45);
            lblMatrixTitle.Margin = new Padding(4, 0, 4, 0);
            lblMatrixTitle.Name = "lblMatrixTitle";
            lblMatrixTitle.Size = new Size(117, 25);
            lblMatrixTitle.TabIndex = 1;
            lblMatrixTitle.Text = "MEAN Filtresi";
            // 
            // lblMatrixSize
            // 
            lblMatrixSize.AutoSize = true;
            lblMatrixSize.Location = new Point(250, 45);
            lblMatrixSize.Margin = new Padding(4, 0, 4, 0);
            lblMatrixSize.Name = "lblMatrixSize";
            lblMatrixSize.Size = new Size(122, 25);
            lblMatrixSize.TabIndex = 3;
            lblMatrixSize.Text = "Matris Boyutu";
            // 
            // cmbMatrixSize
            // 
            cmbMatrixSize.FormattingEnabled = true;
            cmbMatrixSize.Items.AddRange(new object[] { "3x3", "5x5", "7x7" });
            cmbMatrixSize.Location = new Point(380, 40);
            cmbMatrixSize.Margin = new Padding(4, 5, 4, 5);
            cmbMatrixSize.Name = "cmbMatrixSize";
            cmbMatrixSize.Size = new Size(150, 33);
            cmbMatrixSize.TabIndex = 2;
            // 
            // pnlNoise
            // 
            pnlNoise.Controls.Add(rbNoiseAdd);
            pnlNoise.Controls.Add(rbNoiseRemove);
            pnlNoise.Controls.Add(lblNoiseAdd);
            pnlNoise.Controls.Add(cmbNoiseAdd);
            pnlNoise.Controls.Add(lblNoiseRemove);
            pnlNoise.Controls.Add(cmbNoiseRemove);
            pnlNoise.Controls.Add(lblNoiseMatrixSize);
            pnlNoise.Controls.Add(cmbNoiseMatrixSize);
            pnlNoise.Controls.Add(lblNoisePercentage);
            pnlNoise.Controls.Add(trkNoisePercentage);
            pnlNoise.Controls.Add(numNoisePercentage);
            pnlNoise.Dock = DockStyle.Fill;
            pnlNoise.Location = new Point(0, 0);
            pnlNoise.Margin = new Padding(4, 5, 4, 5);
            pnlNoise.Name = "pnlNoise";
            pnlNoise.Size = new Size(1249, 441);
            pnlNoise.TabIndex = 18;
            pnlNoise.Visible = false;
            // 
            // lblNoisePercentage
            // 
            lblNoisePercentage.AutoSize = true;
            lblNoisePercentage.Location = new Point(39, 135);
            lblNoisePercentage.Margin = new Padding(4, 0, 4, 0);
            lblNoisePercentage.Name = "lblNoisePercentage";
            lblNoisePercentage.Size = new Size(82, 25);
            lblNoisePercentage.TabIndex = 8;
            lblNoisePercentage.Text = "Oran (%)";
            // 
            // trkNoisePercentage
            // 
            trkNoisePercentage.Location = new Point(140, 125);
            trkNoisePercentage.Margin = new Padding(4, 5, 4, 5);
            trkNoisePercentage.Maximum = 100; // Yüzde olduğu için max 100
            trkNoisePercentage.Name = "trkNoisePercentage";
            trkNoisePercentage.Size = new Size(160, 69);
            trkNoisePercentage.TabIndex = 9;
            trkNoisePercentage.Value = 10; // Varsayılan %10
            // 
            // numNoisePercentage
            //                                
            numNoisePercentage.Location = new Point(300, 130);
            numNoisePercentage.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            numNoisePercentage.Name = "numNoisePercentage";
            numNoisePercentage.Size = new Size(60, 31);
            numNoisePercentage.TabIndex = 10;
            numNoisePercentage.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numNoisePercentage.BringToFront(); // Ezilmesini önler
            // 
            // rbNoiseAdd
            // 
            rbNoiseAdd.AutoSize = true;
            rbNoiseAdd.Checked = true;
            rbNoiseAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rbNoiseAdd.Location = new Point(39, 20);
            rbNoiseAdd.Name = "rbNoiseAdd";
            rbNoiseAdd.Size = new Size(142, 29);
            rbNoiseAdd.TabIndex = 4;
            rbNoiseAdd.TabStop = true;
            rbNoiseAdd.Text = "Gürültü Ekle";
            rbNoiseAdd.CheckedChanged += RbNoise_CheckedChanged;
            // 
            // rbNoiseRemove
            // 
            rbNoiseRemove.AutoSize = true;
            rbNoiseRemove.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rbNoiseRemove.Location = new Point(400, 20);
            rbNoiseRemove.Name = "rbNoiseRemove";
            rbNoiseRemove.Size = new Size(171, 29);
            rbNoiseRemove.TabIndex = 5;
            rbNoiseRemove.Text = "Gürültü Temizle";
            rbNoiseRemove.CheckedChanged += RbNoise_CheckedChanged;
            // 
            // lblNoiseAdd
            // 
            lblNoiseAdd.AutoSize = true;
            lblNoiseAdd.Location = new Point(39, 75);
            lblNoiseAdd.Margin = new Padding(4, 0, 4, 0);
            lblNoiseAdd.Name = "lblNoiseAdd";
            lblNoiseAdd.Size = new Size(106, 25);
            lblNoiseAdd.TabIndex = 1;
            lblNoiseAdd.Text = "Gürültü Ekle";
            // 
            // cmbNoiseAdd
            // 
            cmbNoiseAdd.FormattingEnabled = true;
            cmbNoiseAdd.Items.AddRange(new object[] { "Salt & Pepper", "Salt", "Pepper" });
            cmbNoiseAdd.Location = new Point(145, 70);
            cmbNoiseAdd.Margin = new Padding(4, 5, 4, 5);
            cmbNoiseAdd.Name = "cmbNoiseAdd";
            cmbNoiseAdd.Size = new Size(200, 33);
            cmbNoiseAdd.TabIndex = 0;
            // 
            // lblNoiseRemove
            // 
            lblNoiseRemove.AutoSize = true;
            lblNoiseRemove.Location = new Point(400, 75);
            lblNoiseRemove.Margin = new Padding(4, 0, 4, 0);
            lblNoiseRemove.Name = "lblNoiseRemove";
            lblNoiseRemove.Size = new Size(132, 25);
            lblNoiseRemove.TabIndex = 3;
            lblNoiseRemove.Text = "Gürültü Temizle";
            // 
            // cmbNoiseRemove
            // 
            cmbNoiseRemove.FormattingEnabled = true;
            cmbNoiseRemove.Items.AddRange(new object[] { "Mean Filtresi", "Median Filtresi" });
            cmbNoiseRemove.Location = new Point(540, 70);
            cmbNoiseRemove.Margin = new Padding(4, 5, 4, 5);
            cmbNoiseRemove.Name = "cmbNoiseRemove";
            cmbNoiseRemove.Size = new Size(200, 33);
            cmbNoiseRemove.TabIndex = 2;
            // 
            // lblNoiseMatrixSize
            // 
            lblNoiseMatrixSize.AutoSize = true;
            lblNoiseMatrixSize.Location = new Point(760, 75);
            lblNoiseMatrixSize.Margin = new Padding(4, 0, 4, 0);
            lblNoiseMatrixSize.Name = "lblNoiseMatrixSize";
            lblNoiseMatrixSize.Size = new Size(122, 25);
            lblNoiseMatrixSize.TabIndex = 6;
            lblNoiseMatrixSize.Text = "Matris Boyutu";
            // 
            // cmbNoiseMatrixSize
            // 
            cmbNoiseMatrixSize.FormattingEnabled = true;
            cmbNoiseMatrixSize.Items.AddRange(new object[] { "3x3", "5x5", "7x7", "9x9" });
            cmbNoiseMatrixSize.Location = new Point(890, 70);
            cmbNoiseMatrixSize.Margin = new Padding(4, 5, 4, 5);
            cmbNoiseMatrixSize.Name = "cmbNoiseMatrixSize";
            cmbNoiseMatrixSize.Size = new Size(120, 33);
            cmbNoiseMatrixSize.TabIndex = 7;
            // 
            // pnlThresholding
            // 
            pnlThresholding.Controls.Add(rbStaticThreshold);
            pnlThresholding.Controls.Add(rbDynamicThreshold);
            pnlThresholding.Controls.Add(esikdegerLabel);
            pnlThresholding.Controls.Add(trkTreshold2);
            pnlThresholding.Controls.Add(lblThresholdMatrix);
            pnlThresholding.Controls.Add(cmbThresholdMatrix);
            pnlThresholding.Controls.Add(numThreshold);
            pnlThresholding.Dock = DockStyle.Fill;
            pnlThresholding.Location = new Point(0, 0);
            pnlThresholding.Margin = new Padding(4, 5, 4, 5);
            pnlThresholding.Name = "pnlThresholding";
            pnlThresholding.Size = new Size(1249, 441);
            pnlThresholding.TabIndex = 15;
            pnlThresholding.Visible = false;
            // 
            // numThreshold
            // 
            numThreshold.Location = new Point(650, 70);
            numThreshold.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numThreshold.Name = "numThreshold";
            numThreshold.Size = new Size(70, 31);
            numThreshold.TabIndex = 6;
            numThreshold.Value = new decimal(new int[] { 128, 0, 0, 0 });
            numThreshold.ValueChanged += NumThreshold_ValueChanged; // Senkronizasyon Olayı
            // 
            // rbStaticThreshold
            // 
            rbStaticThreshold.AutoSize = true;
            rbStaticThreshold.Checked = true;
            rbStaticThreshold.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rbStaticThreshold.Location = new Point(39, 20);
            rbStaticThreshold.Name = "rbStaticThreshold";
            rbStaticThreshold.Size = new Size(165, 29);
            rbStaticThreshold.TabIndex = 2;
            rbStaticThreshold.TabStop = true;
            rbStaticThreshold.Text = "Statik Eşikleme";
            rbStaticThreshold.CheckedChanged += RbThreshold_CheckedChanged;
            // 
            // rbDynamicThreshold
            // 
            rbDynamicThreshold.AutoSize = true;
            rbDynamicThreshold.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rbDynamicThreshold.Location = new Point(800, 20);
            rbDynamicThreshold.Name = "rbDynamicThreshold";
            rbDynamicThreshold.Size = new Size(186, 29);
            rbDynamicThreshold.TabIndex = 3;
            rbDynamicThreshold.Text = "Dinamik Eşikleme";
            rbDynamicThreshold.CheckedChanged += RbThreshold_CheckedChanged;
            // 
            // esikdegerLabel
            // 
            esikdegerLabel.AutoSize = true;
            esikdegerLabel.Location = new Point(39, 75);
            esikdegerLabel.Margin = new Padding(4, 0, 4, 0);
            esikdegerLabel.Name = "esikdegerLabel";
            esikdegerLabel.Size = new Size(99, 25);
            esikdegerLabel.TabIndex = 1;
            esikdegerLabel.Text = "Eşik Değeri";
            // 
            // trkTreshold2
            // 
            trkTreshold2.Location = new Point(157, 63);
            trkTreshold2.Margin = new Padding(4, 5, 4, 5);
            trkTreshold2.Maximum = 255;
            trkTreshold2.Name = "trkTreshold2";
            trkTreshold2.Size = new Size(429, 69);
            trkTreshold2.TabIndex = 0;
            trkTreshold2.Value = 128;
            // 
            // lblThresholdMatrix
            // 
            lblThresholdMatrix.AutoSize = true;
            lblThresholdMatrix.Location = new Point(800, 75);
            lblThresholdMatrix.Margin = new Padding(4, 0, 4, 0);
            lblThresholdMatrix.Name = "lblThresholdMatrix";
            lblThresholdMatrix.Size = new Size(122, 25);
            lblThresholdMatrix.TabIndex = 4;
            lblThresholdMatrix.Text = "Matris Boyutu";
            // 
            // cmbThresholdMatrix
            // 
            cmbThresholdMatrix.FormattingEnabled = true;
            cmbThresholdMatrix.Items.AddRange(new object[] { "3x3", "5x5", "7x7", "11x11", "15x15", "21x21" });
            cmbThresholdMatrix.Location = new Point(950, 70);
            cmbThresholdMatrix.Margin = new Padding(4, 5, 4, 5);
            cmbThresholdMatrix.Name = "cmbThresholdMatrix";
            cmbThresholdMatrix.Size = new Size(150, 33);
            cmbThresholdMatrix.TabIndex = 5;
            // 
            // pnlBinary
            // 
            pnlBinary.Controls.Add(esikdegeri);
            pnlBinary.Controls.Add(trkThreshold);
            pnlBinary.Dock = DockStyle.Fill;
            pnlBinary.Location = new Point(0, 0);
            pnlBinary.Margin = new Padding(4, 5, 4, 5);
            pnlBinary.Name = "pnlBinary";
            pnlBinary.Size = new Size(1249, 441);
            pnlBinary.TabIndex = 3;
            pnlBinary.Visible = false;
            // 
            // esikdegeri
            // 
            esikdegeri.AutoSize = true;
            esikdegeri.Location = new Point(39, 45);
            esikdegeri.Margin = new Padding(4, 0, 4, 0);
            esikdegeri.Name = "esikdegeri";
            esikdegeri.Size = new Size(99, 25);
            esikdegeri.TabIndex = 1;
            esikdegeri.Text = "Eşik Değeri";
            // 
            // trkThreshold
            // 
            trkThreshold.Location = new Point(157, 33);
            trkThreshold.Margin = new Padding(4, 5, 4, 5);
            trkThreshold.Maximum = 255;
            trkThreshold.Name = "trkThreshold";
            trkThreshold.Size = new Size(429, 69);
            trkThreshold.TabIndex = 0;
            trkThreshold.Value = 128;
            // 
            // pnlCrop
            // 
            pnlCrop.Controls.Add(width);
            pnlCrop.Controls.Add(higth);
            pnlCrop.Controls.Add(y);
            pnlCrop.Controls.Add(x);
            pnlCrop.Controls.Add(numCropHeight);
            pnlCrop.Controls.Add(numCropWidth);
            pnlCrop.Controls.Add(numCropY);
            pnlCrop.Controls.Add(numCropX);
            pnlCrop.Dock = DockStyle.Fill;
            pnlCrop.Location = new Point(0, 0);
            pnlCrop.Margin = new Padding(4, 5, 4, 5);
            pnlCrop.Name = "pnlCrop";
            pnlCrop.Size = new Size(1249, 441);
            pnlCrop.TabIndex = 5;
            pnlCrop.Visible = false;
            // 
            // width
            // 
            width.AutoSize = true;
            width.Location = new Point(571, 45);
            width.Margin = new Padding(4, 0, 4, 0);
            width.Name = "width";
            width.Size = new Size(72, 25);
            width.TabIndex = 7;
            width.Text = "Genişlik";
            // 
            // higth
            // 
            higth.AutoSize = true;
            higth.Location = new Point(829, 45);
            higth.Margin = new Padding(4, 0, 4, 0);
            higth.Name = "higth";
            higth.Size = new Size(84, 25);
            higth.TabIndex = 6;
            higth.Text = "Yükseklik";
            // 
            // y
            // 
            y.AutoSize = true;
            y.Location = new Point(300, 45);
            y.Margin = new Padding(4, 0, 4, 0);
            y.Name = "y";
            y.Size = new Size(22, 25);
            y.TabIndex = 5;
            y.Text = "Y";
            // 
            // x
            // 
            x.AutoSize = true;
            x.Location = new Point(29, 45);
            x.Margin = new Padding(4, 0, 4, 0);
            x.Name = "x";
            x.Size = new Size(23, 25);
            x.TabIndex = 4;
            x.Text = "X";
            // 
            // numCropHeight
            // 
            numCropHeight.Location = new Point(914, 42);
            numCropHeight.Margin = new Padding(4, 5, 4, 5);
            numCropHeight.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numCropHeight.Name = "numCropHeight";
            numCropHeight.Size = new Size(143, 31);
            numCropHeight.TabIndex = 3;
            // 
            // numCropWidth
            // 
            numCropWidth.Location = new Point(657, 42);
            numCropWidth.Margin = new Padding(4, 5, 4, 5);
            numCropWidth.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numCropWidth.Name = "numCropWidth";
            numCropWidth.Size = new Size(143, 31);
            numCropWidth.TabIndex = 2;
            // 
            // numCropY
            // 
            numCropY.Location = new Point(329, 42);
            numCropY.Margin = new Padding(4, 5, 4, 5);
            numCropY.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numCropY.Name = "numCropY";
            numCropY.Size = new Size(143, 31);
            numCropY.TabIndex = 1;
            // 
            // numCropX
            // 
            numCropX.Location = new Point(57, 42);
            numCropX.Margin = new Padding(4, 5, 4, 5);
            numCropX.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numCropX.Name = "numCropX";
            numCropX.Size = new Size(143, 31);
            numCropX.TabIndex = 0;
            // 
            // pnlRotate
            // 
            pnlRotate.Controls.Add(interpoletionLabel);
            pnlRotate.Controls.Add(acıLabel);
            pnlRotate.Controls.Add(cmbRotateInterpolation);
            pnlRotate.Controls.Add(numAngle);
            pnlRotate.Dock = DockStyle.Fill;
            pnlRotate.Location = new Point(0, 0);
            pnlRotate.Margin = new Padding(4, 5, 4, 5);
            pnlRotate.Name = "pnlRotate";
            pnlRotate.Size = new Size(1249, 441);
            pnlRotate.TabIndex = 4;
            pnlRotate.Visible = false;
            // 
            // interpoletionLabel
            // 
            interpoletionLabel.AutoSize = true;
            interpoletionLabel.Location = new Point(357, 45);
            interpoletionLabel.Margin = new Padding(4, 0, 4, 0);
            interpoletionLabel.Name = "interpoletionLabel";
            interpoletionLabel.Size = new Size(125, 25);
            interpoletionLabel.TabIndex = 3;
            interpoletionLabel.Text = "Enterpolasyon";
            // 
            // acıLabel
            // 
            acıLabel.AutoSize = true;
            acıLabel.Location = new Point(39, 45);
            acıLabel.Margin = new Padding(4, 0, 4, 0);
            acıLabel.Name = "acıLabel";
            acıLabel.Size = new Size(36, 25);
            acıLabel.TabIndex = 2;
            acıLabel.Text = "Açı";
            // 
            // cmbRotateInterpolation
            // 
            cmbRotateInterpolation.FormattingEnabled = true;
            cmbRotateInterpolation.Items.AddRange(new object[] { "Nearest Neighbor", "Bilinear", "Bicubic" });
            cmbRotateInterpolation.Location = new Point(486, 40);
            cmbRotateInterpolation.Margin = new Padding(4, 5, 4, 5);
            cmbRotateInterpolation.Name = "cmbRotateInterpolation";
            cmbRotateInterpolation.Size = new Size(213, 33);
            cmbRotateInterpolation.TabIndex = 1;
            // 
            // numAngle
            // 
            numAngle.Location = new Point(100, 42);
            numAngle.Margin = new Padding(4, 5, 4, 5);
            numAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numAngle.Name = "numAngle";
            numAngle.Size = new Size(171, 31);
            numAngle.TabIndex = 0;
            // 
            // pnlScale
            // 
            pnlScale.Controls.Add(interpoletionLabelScale);
            pnlScale.Controls.Add(cmbScaleInterpolation);
            pnlScale.Controls.Add(scaleLabel);
            pnlScale.Controls.Add(numScale);
            pnlScale.Dock = DockStyle.Fill;
            pnlScale.Location = new Point(0, 0);
            pnlScale.Margin = new Padding(4, 5, 4, 5);
            pnlScale.Name = "pnlScale";
            pnlScale.Size = new Size(1249, 441);
            pnlScale.TabIndex = 10;
            pnlScale.Visible = false;
            // 
            // interpoletionLabelScale
            // 
            interpoletionLabelScale.AutoSize = true;
            interpoletionLabelScale.Location = new Point(357, 45);
            interpoletionLabelScale.Margin = new Padding(4, 0, 4, 0);
            interpoletionLabelScale.Name = "interpoletionLabelScale";
            interpoletionLabelScale.Size = new Size(125, 25);
            interpoletionLabelScale.TabIndex = 3;
            interpoletionLabelScale.Text = "Enterpolasyon";
            // 
            // cmbScaleInterpolation
            // 
            cmbScaleInterpolation.FormattingEnabled = true;
            cmbScaleInterpolation.Items.AddRange(new object[] { "Nearest Neighbor", "Bilinear", "Bicubic" });
            cmbScaleInterpolation.Location = new Point(486, 40);
            cmbScaleInterpolation.Margin = new Padding(4, 5, 4, 5);
            cmbScaleInterpolation.Name = "cmbScaleInterpolation";
            cmbScaleInterpolation.Size = new Size(213, 33);
            cmbScaleInterpolation.TabIndex = 2;
            // 
            // scaleLabel
            // 
            scaleLabel.AutoSize = true;
            scaleLabel.Location = new Point(39, 45);
            scaleLabel.Margin = new Padding(4, 0, 4, 0);
            scaleLabel.Name = "scaleLabel";
            scaleLabel.Size = new Size(51, 25);
            scaleLabel.TabIndex = 1;
            scaleLabel.Text = "Oran";
            // 
            // numScale
            // 
            numScale.DecimalPlaces = 1;
            numScale.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numScale.Location = new Point(114, 42);
            numScale.Margin = new Padding(4, 5, 4, 5);
            numScale.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numScale.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numScale.Name = "numScale";
            numScale.Size = new Size(171, 31);
            numScale.TabIndex = 0;
            numScale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pnlArithmetic
            // 
            pnlArithmetic.Controls.Add(islemLabel);
            pnlArithmetic.Controls.Add(cmbAritmetik);
            pnlArithmetic.Dock = DockStyle.Fill;
            pnlArithmetic.Location = new Point(0, 0);
            pnlArithmetic.Margin = new Padding(4, 5, 4, 5);
            pnlArithmetic.Name = "pnlArithmetic";
            pnlArithmetic.Size = new Size(1249, 441);
            pnlArithmetic.TabIndex = 17;
            pnlArithmetic.Visible = false;
            // 
            // islemLabel
            // 
            islemLabel.AutoSize = true;
            islemLabel.Location = new Point(39, 45);
            islemLabel.Margin = new Padding(4, 0, 4, 0);
            islemLabel.Name = "islemLabel";
            islemLabel.Size = new Size(73, 25);
            islemLabel.TabIndex = 1;
            islemLabel.Text = "İşlemler";
            // 
            // cmbAritmetik
            // 
            cmbAritmetik.FormattingEnabled = true;
            cmbAritmetik.Items.AddRange(new object[] { "Toplam", "Çıkarma", "Çarpma", "Bölme" });
            cmbAritmetik.Location = new Point(129, 40);
            cmbAritmetik.Margin = new Padding(4, 5, 4, 5);
            cmbAritmetik.Name = "cmbAritmetik";
            cmbAritmetik.Size = new Size(284, 33);
            cmbAritmetik.TabIndex = 0;
            // 
            // pnlContrast
            // 
            pnlContrast.Controls.Add(konstrastLabel);
            pnlContrast.Controls.Add(trkContrast);
            pnlContrast.Dock = DockStyle.Fill;
            pnlContrast.Location = new Point(0, 0);
            pnlContrast.Margin = new Padding(4, 5, 4, 5);
            pnlContrast.Name = "pnlContrast";
            pnlContrast.Size = new Size(1249, 441);
            pnlContrast.TabIndex = 16;
            pnlContrast.Visible = false;
            // 
            // konstrastLabel
            // 
            konstrastLabel.AutoSize = true;
            konstrastLabel.Location = new Point(39, 45);
            konstrastLabel.Margin = new Padding(4, 0, 4, 0);
            konstrastLabel.Name = "konstrastLabel";
            konstrastLabel.Size = new Size(63, 25);
            konstrastLabel.TabIndex = 1;
            konstrastLabel.Text = "Şiddet";
            // 
            // trkContrast
            // 
            trkContrast.Location = new Point(114, 33);
            trkContrast.Margin = new Padding(4, 5, 4, 5);
            trkContrast.Maximum = 100;
            trkContrast.Minimum = -100;
            trkContrast.Name = "trkContrast";
            trkContrast.Size = new Size(429, 69);
            trkContrast.TabIndex = 0;
            // 
            // pnlMorphology
            // 
            pnlMorphology.Controls.Add(lblMorphologyType);
            pnlMorphology.Controls.Add(cmbMorphologyType);
            pnlMorphology.Controls.Add(lblMorphMatrixSize);
            pnlMorphology.Controls.Add(cmbMorphMatrixSize);
            pnlMorphology.Controls.Add(lblMorphShape);
            pnlMorphology.Controls.Add(cmbMorphShape);
            pnlMorphology.Controls.Add(gbMorphMatrix);
            pnlMorphology.Dock = DockStyle.Fill;
            pnlMorphology.Location = new Point(0, 0);
            pnlMorphology.Margin = new Padding(4, 5, 4, 5);
            pnlMorphology.Name = "pnlMorphology";
            pnlMorphology.Size = new Size(1249, 441);
            pnlMorphology.TabIndex = 20;
            pnlMorphology.Visible = false;
            // 
            // lblMorphologyType
            // 
            lblMorphologyType.AutoSize = true;
            lblMorphologyType.Location = new Point(40, 45);
            lblMorphologyType.Margin = new Padding(4, 0, 4, 0);
            lblMorphologyType.Name = "lblMorphologyType";
            lblMorphologyType.Size = new Size(94, 25);
            lblMorphologyType.TabIndex = 1;
            lblMorphologyType.Text = "İşlem Türü";
            // 
            // cmbMorphologyType
            // 
            cmbMorphologyType.FormattingEnabled = true;
            cmbMorphologyType.Items.AddRange(new object[] { "Genişleme (Dilation)", "Aşınma (Erosion)", "Açma (Opening)", "Kapama (Closing)" });
            cmbMorphologyType.Location = new Point(145, 40);
            cmbMorphologyType.Margin = new Padding(4, 5, 4, 5);
            cmbMorphologyType.Name = "cmbMorphologyType";
            cmbMorphologyType.Size = new Size(220, 33);
            cmbMorphologyType.TabIndex = 0;
            // 
            // lblMorphMatrixSize
            // 
            lblMorphMatrixSize.AutoSize = true;
            lblMorphMatrixSize.Location = new Point(410, 45);
            lblMorphMatrixSize.Margin = new Padding(4, 0, 4, 0);
            lblMorphMatrixSize.Name = "lblMorphMatrixSize";
            lblMorphMatrixSize.Size = new Size(122, 25);
            lblMorphMatrixSize.TabIndex = 3;
            lblMorphMatrixSize.Text = "Matris Boyutu";
            // 
            // cmbMorphMatrixSize
            // 
            cmbMorphMatrixSize.FormattingEnabled = true;
            cmbMorphMatrixSize.Items.AddRange(new object[] { "3x3", "5x5", "7x7" });
            cmbMorphMatrixSize.Location = new Point(540, 40);
            cmbMorphMatrixSize.Margin = new Padding(4, 5, 4, 5);
            cmbMorphMatrixSize.Name = "cmbMorphMatrixSize";
            cmbMorphMatrixSize.Size = new Size(150, 33);
            cmbMorphMatrixSize.TabIndex = 2;
            cmbMorphMatrixSize.SelectedIndexChanged += UpdateMorphMatrix_Event;
            // 
            // lblMorphShape
            // 
            lblMorphShape.AutoSize = true;
            lblMorphShape.Location = new Point(770, 45);
            lblMorphShape.Margin = new Padding(4, 0, 4, 0);
            lblMorphShape.Name = "lblMorphShape";
            lblMorphShape.Size = new Size(106, 25);
            lblMorphShape.TabIndex = 5;
            lblMorphShape.Text = "Yapısal Şekil";
            // 
            // cmbMorphShape
            // 
            cmbMorphShape.FormattingEnabled = true;
            cmbMorphShape.Items.AddRange(new object[] { "Kare (Square)", "Daire (Circle)", "Haç (Cross)", "Özel (Custom)" });
            cmbMorphShape.Location = new Point(885, 40);
            cmbMorphShape.Margin = new Padding(4, 5, 4, 5);
            cmbMorphShape.Name = "cmbMorphShape";
            cmbMorphShape.Size = new Size(150, 33);
            cmbMorphShape.TabIndex = 4;
            cmbMorphShape.SelectedIndexChanged += UpdateMorphMatrix_Event;
            // 
            // gbMorphMatrix
            // 
            gbMorphMatrix.Location = new Point(39, 100);
            gbMorphMatrix.Name = "gbMorphMatrix";
            gbMorphMatrix.Size = new Size(400, 320);
            gbMorphMatrix.TabIndex = 6;
            gbMorphMatrix.TabStop = false;
            gbMorphMatrix.Text = "Yapısal Öğe (Structuring Element)";
            // 
            // pnlUnsharp
            // 
            pnlUnsharp.Controls.Add(lblUnsharpMatrix);
            pnlUnsharp.Controls.Add(cmbUnsharpMatrix);
            pnlUnsharp.Dock = DockStyle.Fill;
            pnlUnsharp.Location = new Point(0, 0);
            pnlUnsharp.Margin = new Padding(4, 5, 4, 5);
            pnlUnsharp.Name = "pnlUnsharp";
            pnlUnsharp.Size = new Size(1249, 441);
            pnlUnsharp.TabIndex = 21;
            pnlUnsharp.Visible = false;
            // 
            // lblUnsharpMatrix
            // 
            lblUnsharpMatrix.AutoSize = true;
            lblUnsharpMatrix.Location = new Point(39, 45);
            lblUnsharpMatrix.Margin = new Padding(4, 0, 4, 0);
            lblUnsharpMatrix.Name = "lblUnsharpMatrix";
            lblUnsharpMatrix.Size = new Size(178, 25);
            lblUnsharpMatrix.TabIndex = 1;
            lblUnsharpMatrix.Text = "Maske Matris Boyutu";
            // 
            // cmbUnsharpMatrix
            // 
            cmbUnsharpMatrix.FormattingEnabled = true;
            cmbUnsharpMatrix.Items.AddRange(new object[] { "3x3", "5x5", "7x7" });
            cmbUnsharpMatrix.Location = new Point(220, 40);
            cmbUnsharpMatrix.Margin = new Padding(4, 5, 4, 5);
            cmbUnsharpMatrix.Name = "cmbUnsharpMatrix";
            cmbUnsharpMatrix.Size = new Size(150, 33);
            cmbUnsharpMatrix.TabIndex = 0;
            // 
            // pnlEdgeDetection
            // 
            pnlEdgeDetection.Controls.Add(lblEdgeType);
            pnlEdgeDetection.Controls.Add(cmbEdgeType);
            pnlEdgeDetection.Controls.Add(gbEdgeMatrix);
            pnlEdgeDetection.Dock = DockStyle.Fill;
            pnlEdgeDetection.Location = new Point(0, 0);
            pnlEdgeDetection.Margin = new Padding(4, 5, 4, 5);
            pnlEdgeDetection.Name = "pnlEdgeDetection";
            pnlEdgeDetection.Size = new Size(1249, 441);
            pnlEdgeDetection.TabIndex = 22;
            pnlEdgeDetection.Visible = false;
            // 
            // lblEdgeType
            // 
            lblEdgeType.AutoSize = true;
            lblEdgeType.Location = new Point(39, 45);
            lblEdgeType.Margin = new Padding(4, 0, 4, 0);
            lblEdgeType.Name = "lblEdgeType";
            lblEdgeType.Size = new Size(100, 25);
            lblEdgeType.TabIndex = 1;
            lblEdgeType.Text = "Kenar Yönü";
            // 
            // cmbEdgeType
            // 
            cmbEdgeType.FormattingEnabled = true;
            cmbEdgeType.Items.AddRange(new object[] { "Yatay (Horizontal)", "Dikey (Vertical)", "Her İkisi (Magnitude)" });
            cmbEdgeType.Location = new Point(155, 40);
            cmbEdgeType.Margin = new Padding(4, 5, 4, 5);
            cmbEdgeType.Name = "cmbEdgeType";
            cmbEdgeType.Size = new Size(200, 33);
            cmbEdgeType.TabIndex = 0;
            cmbEdgeType.SelectedIndexChanged += UpdateEdgeMatrix_Event;
            // 
            // gbEdgeMatrix
            // 
            gbEdgeMatrix.Controls.Add(tlpEdgeMatrix);
            gbEdgeMatrix.Location = new Point(39, 100);
            gbEdgeMatrix.Name = "gbEdgeMatrix";
            gbEdgeMatrix.Size = new Size(300, 300);
            gbEdgeMatrix.TabIndex = 2;
            gbEdgeMatrix.TabStop = false;
            gbEdgeMatrix.Text = "Uygulanan Matris (Kernel)";
            // 
            // tlpEdgeMatrix
            // 
            tlpEdgeMatrix.ColumnCount = 3;
            tlpEdgeMatrix.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpEdgeMatrix.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpEdgeMatrix.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpEdgeMatrix.Dock = DockStyle.Fill;
            tlpEdgeMatrix.Location = new Point(3, 27);
            tlpEdgeMatrix.Name = "tlpEdgeMatrix";
            tlpEdgeMatrix.Padding = new Padding(10);
            tlpEdgeMatrix.RowCount = 3;
            tlpEdgeMatrix.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tlpEdgeMatrix.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tlpEdgeMatrix.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tlpEdgeMatrix.Size = new Size(294, 270);
            tlpEdgeMatrix.TabIndex = 0;
            // 
            // pnlColorSpace
            // 
            pnlColorSpace.Controls.Add(lblColorSpace);
            pnlColorSpace.Controls.Add(cmbColorSpace);
            pnlColorSpace.Dock = DockStyle.Fill;
            pnlColorSpace.Location = new Point(0, 0);
            pnlColorSpace.Margin = new Padding(4, 5, 4, 5);
            pnlColorSpace.Name = "pnlColorSpace";
            pnlColorSpace.Size = new Size(1249, 441);
            pnlColorSpace.TabIndex = 23;
            pnlColorSpace.Visible = false;
            // 
            // lblColorSpace
            // 
            lblColorSpace.AutoSize = true;
            lblColorSpace.Location = new Point(39, 45);
            lblColorSpace.Margin = new Padding(4, 0, 4, 0);
            lblColorSpace.Name = "lblColorSpace";
            lblColorSpace.Size = new Size(150, 25);
            lblColorSpace.TabIndex = 1;
            lblColorSpace.Text = "Hedef Renk Uzayı";
            // 
            // cmbColorSpace
            // 
            cmbColorSpace.FormattingEnabled = true;
            cmbColorSpace.Items.AddRange(new object[] { "RGB -> HSV", "RGB -> YCbCr", "RGB -> CMYK", "RGB -> Gri (Luminance)" });
            cmbColorSpace.Location = new Point(190, 40);
            cmbColorSpace.Margin = new Padding(4, 5, 4, 5);
            cmbColorSpace.Name = "cmbColorSpace";
            cmbColorSpace.Size = new Size(250, 33);
            cmbColorSpace.TabIndex = 0;
            // 
            // btnUygula
            // 
            btnUygula.Dock = DockStyle.Bottom;
            btnUygula.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUygula.Location = new Point(14, 458);
            btnUygula.Margin = new Padding(4, 5, 4, 5);
            btnUygula.Name = "btnUygula";
            btnUygula.Size = new Size(1249, 100);
            btnUygula.TabIndex = 9;
            btnUygula.Text = "İŞLEMİ UYGULA";
            btnUygula.UseVisualStyleBackColor = true;
            btnUygula.Click += BtnUygula_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1714, 1333);
            Controls.Add(splitContainer1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Görüntü İşleme Platformu";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picInput1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInput2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOutput).EndInit();
            pnlMasterParametre.ResumeLayout(false);
            pnlHistogram.ResumeLayout(false);
            pnlHistogram.PerformLayout();
            tblHistograms.ResumeLayout(false);
            tblHistograms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)picHistogramResult).EndInit();
            pnlMatrixFilter.ResumeLayout(false);
            pnlMatrixFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(trkNoisePercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numNoisePercentage)).EndInit();
            pnlNoise.ResumeLayout(false);
            pnlNoise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(numThreshold)).EndInit();
            pnlThresholding.ResumeLayout(false);
            pnlThresholding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkTreshold2).EndInit();
            pnlBinary.ResumeLayout(false);
            pnlBinary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkThreshold).EndInit();
            pnlCrop.ResumeLayout(false);
            pnlCrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCropHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCropWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCropY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCropX).EndInit();
            pnlRotate.ResumeLayout(false);
            pnlRotate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAngle).EndInit();
            pnlScale.ResumeLayout(false);
            pnlScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScale).EndInit();
            pnlArithmetic.ResumeLayout(false);
            pnlArithmetic.PerformLayout();
            pnlContrast.ResumeLayout(false);
            pnlContrast.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkContrast).EndInit();
            pnlMorphology.ResumeLayout(false);
            pnlMorphology.PerformLayout();
            pnlUnsharp.ResumeLayout(false);
            pnlUnsharp.PerformLayout();
            pnlEdgeDetection.ResumeLayout(false);
            pnlEdgeDetection.PerformLayout();
            gbEdgeMatrix.ResumeLayout(false);
            pnlColorSpace.ResumeLayout(false);
            pnlColorSpace.PerformLayout();
            ResumeLayout(false);


        }

        #endregion

        private System.Windows.Forms.RadioButton rbNoiseAdd;
        private System.Windows.Forms.RadioButton rbNoiseRemove;
        private System.Windows.Forms.Panel pnlColorSpace;
        private System.Windows.Forms.Label lblColorSpace;
        private System.Windows.Forms.ComboBox cmbColorSpace;
        private System.Windows.Forms.Panel pnlEdgeDetection;
        private System.Windows.Forms.GroupBox gbEdgeMatrix;
        private System.Windows.Forms.TableLayoutPanel tlpEdgeMatrix;
        private System.Windows.Forms.Label[,] edgeMatrixLabels; 
        private System.Windows.Forms.Label lblEdgeType;
        private System.Windows.Forms.ComboBox cmbEdgeType;
        private System.Windows.Forms.Panel pnlUnsharp;
        private System.Windows.Forms.Label lblUnsharpMatrix;
        private System.Windows.Forms.ComboBox cmbUnsharpMatrix;
        private System.Windows.Forms.Panel pnlMorphology;
        private System.Windows.Forms.Label lblMorphShape;
        private System.Windows.Forms.ComboBox cmbMorphShape;
        private System.Windows.Forms.GroupBox gbMorphMatrix;        
        private System.Windows.Forms.Label lblMorphologyType;
        private System.Windows.Forms.ComboBox cmbMorphologyType;
        private System.Windows.Forms.Label lblMorphMatrixSize;
        private System.Windows.Forms.ComboBox cmbMorphMatrixSize;
        private System.Windows.Forms.Panel pnlMatrixFilter;
        private System.Windows.Forms.Label lblMatrixTitle;
        private System.Windows.Forms.Label lblMatrixSize;
        private System.Windows.Forms.ComboBox cmbMatrixSize;
        private System.Windows.Forms.Panel pnlNoise;
        private System.Windows.Forms.Label lblNoisePercentage;
        private System.Windows.Forms.TrackBar trkNoisePercentage;
        private System.Windows.Forms.NumericUpDown numNoisePercentage;
        private System.Windows.Forms.Label lblNoiseMatrixSize;
        private System.Windows.Forms.ComboBox cmbNoiseMatrixSize;
        private System.Windows.Forms.Label lblNoiseAdd;
        private System.Windows.Forms.ComboBox cmbNoiseAdd;
        private System.Windows.Forms.Label lblNoiseRemove;
        private System.Windows.Forms.ComboBox cmbNoiseRemove;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resimYükleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ikinciResmiYükleToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picInput1;
        private System.Windows.Forms.PictureBox picInput2;
        private System.Windows.Forms.PictureBox picOutput;
        private System.Windows.Forms.Panel pnlMasterParametre;
        private System.Windows.Forms.Panel pnlBinary;
        private System.Windows.Forms.Label esikdegeri;
        private System.Windows.Forms.TrackBar trkThreshold;
        private System.Windows.Forms.Panel pnlRotate;
        private System.Windows.Forms.Label interpoletionLabel;
        private System.Windows.Forms.Label acıLabel;
        private System.Windows.Forms.ComboBox cmbRotateInterpolation;
        private System.Windows.Forms.NumericUpDown numAngle;
        private System.Windows.Forms.Panel pnlCrop;
        private System.Windows.Forms.Label width;
        private System.Windows.Forms.Label higth;
        private System.Windows.Forms.Label y;
        private System.Windows.Forms.Label x;
        private System.Windows.Forms.NumericUpDown numCropHeight;
        private System.Windows.Forms.NumericUpDown numCropWidth;
        private System.Windows.Forms.NumericUpDown numCropY;
        private System.Windows.Forms.NumericUpDown numCropX;
        private System.Windows.Forms.Panel pnlScale;
        private System.Windows.Forms.Label interpoletionLabelScale;
        private System.Windows.Forms.ComboBox cmbScaleInterpolation;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.NumericUpDown numScale;
        private System.Windows.Forms.Panel pnlArithmetic;
        private System.Windows.Forms.Label islemLabel;
        private System.Windows.Forms.ComboBox cmbAritmetik;
        private System.Windows.Forms.Panel pnlContrast;
        private System.Windows.Forms.Label konstrastLabel;
        private System.Windows.Forms.TrackBar trkContrast;
        private System.Windows.Forms.Panel pnlThresholding;
        private System.Windows.Forms.NumericUpDown numThreshold;
        private System.Windows.Forms.TrackBar trkTreshold2;
        private System.Windows.Forms.RadioButton rbStaticThreshold;
        private System.Windows.Forms.RadioButton rbDynamicThreshold;
        private System.Windows.Forms.Label lblThresholdMatrix;
        private System.Windows.Forms.ComboBox cmbThresholdMatrix;
        private System.Windows.Forms.Panel pnlHistogram;
        private System.Windows.Forms.PictureBox picHistogram;
        private System.Windows.Forms.PictureBox picHistogramResult;
        private System.Windows.Forms.Label lblHistOriginal;
        private System.Windows.Forms.TableLayoutPanel tblHistograms;
        private System.Windows.Forms.Label lblHistResult;
        private System.Windows.Forms.Label histLabel;
        private System.Windows.Forms.ComboBox cmbHistogram;
        private System.Windows.Forms.Button btnUygula;
        private System.Windows.Forms.Label esikdegerLabel;
    }
}