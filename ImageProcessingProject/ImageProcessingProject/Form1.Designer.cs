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
            pnlBinary = new Panel();
            esikdegeri = new Label();
            trkThreshold = new TrackBar();
            pnlThresholding = new Panel();
            esikdegerLabel = new Label();
            trkTreshold2 = new TrackBar();
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
            pnlHistogram = new Panel();
            histLabel = new Label();
            cmbHistogram = new ComboBox();
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
            pnlBinary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkThreshold).BeginInit();
            pnlThresholding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkTreshold2).BeginInit();
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
            pnlHistogram.SuspendLayout();
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
            treeView1.AfterSelect += treeView1_AfterSelect;
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
            resimYükleToolStripMenuItem1.Click += resimYükleToolStripMenuItem1_Click;
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
            pnlMasterParametre.Controls.Add(pnlThresholding);
            pnlMasterParametre.Controls.Add(pnlBinary);
            pnlMasterParametre.Controls.Add(pnlCrop);
            pnlMasterParametre.Controls.Add(pnlRotate);
            pnlMasterParametre.Controls.Add(pnlScale);
            pnlMasterParametre.Controls.Add(pnlArithmetic);
            pnlMasterParametre.Controls.Add(pnlContrast);
            pnlMasterParametre.Controls.Add(pnlHistogram);
            pnlMasterParametre.Dock = DockStyle.Fill;
            pnlMasterParametre.Location = new Point(14, 17);
            pnlMasterParametre.Margin = new Padding(4, 5, 4, 5);
            pnlMasterParametre.Name = "pnlMasterParametre";
            pnlMasterParametre.Size = new Size(1249, 441);
            pnlMasterParametre.TabIndex = 10;
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
            // pnlThresholding
            // 
            pnlThresholding.Controls.Add(esikdegerLabel);
            pnlThresholding.Controls.Add(trkTreshold2);
            pnlThresholding.Dock = DockStyle.Fill;
            pnlThresholding.Location = new Point(0, 0);
            pnlThresholding.Margin = new Padding(4, 5, 4, 5);
            pnlThresholding.Name = "pnlThresholding";
            pnlThresholding.Size = new Size(1249, 441);
            pnlThresholding.TabIndex = 15;
            pnlThresholding.Visible = false;
            // 
            // esikdegerLabel
            // 
            esikdegerLabel.AutoSize = true;
            esikdegerLabel.Location = new Point(39, 45);
            esikdegerLabel.Margin = new Padding(4, 0, 4, 0);
            esikdegerLabel.Name = "esikdegerLabel";
            esikdegerLabel.Size = new Size(109, 29);
            esikdegerLabel.TabIndex = 1;
            esikdegerLabel.Text = "Eşik Değeri";
            // 
            // trkTreshold2
            // 
            trkTreshold2.Location = new Point(157, 33);
            trkTreshold2.Margin = new Padding(4, 5, 4, 5);
            trkTreshold2.Maximum = 255;
            trkTreshold2.Name = "trkTreshold2";
            trkTreshold2.Size = new Size(429, 69);
            trkTreshold2.TabIndex = 0;
            trkTreshold2.Value = 128;
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
            cmbAritmetik.Items.AddRange(new object[] { "Toplam", "Çıkarma", "Çarpma", "Bölme", "Mantıksal VE", "Mantıksal VEYA" });
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
            // pnlHistogram
            // 
            pnlHistogram.Controls.Add(histLabel);
            pnlHistogram.Controls.Add(cmbHistogram);
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
            pnlBinary.ResumeLayout(false);
            pnlBinary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkThreshold).EndInit();
            pnlThresholding.ResumeLayout(false);
            pnlThresholding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkTreshold2).EndInit();
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
            pnlHistogram.ResumeLayout(false);
            pnlHistogram.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.TrackBar trkTreshold2;
        private System.Windows.Forms.Panel pnlHistogram;
        private System.Windows.Forms.Label histLabel;
        private System.Windows.Forms.ComboBox cmbHistogram;
        private System.Windows.Forms.Button btnUygula;
        private Label esikdegerLabel;
    }
}