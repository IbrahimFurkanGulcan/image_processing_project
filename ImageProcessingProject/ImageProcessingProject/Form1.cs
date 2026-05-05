using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessingProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateEdgeMatrixGrid();
            trkTreshold2.ValueChanged += TrkTreshold2_ValueChanged;
            numThreshold.ValueChanged += NumThreshold_ValueChanged;
            trkNoisePercentage.ValueChanged += TrkNoisePercentage_ValueChanged;
            numNoisePercentage.ValueChanged += NumNoisePercentage_ValueChanged;
            trkThreshold.ValueChanged += (s, e) => esikdegeri.Text = "Esik Degeri: " + trkThreshold.Value;
            esikdegeri.Text = "Esik Degeri: " + trkThreshold.Value;
        }

        private System.Windows.Forms.TextBox[,] morphMatrixTextBoxes;

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            TreeNode nodeTemel = new TreeNode("Temel Islemler");
            nodeTemel.Nodes.Add("Gri Donusum");
            nodeTemel.Nodes.Add("Binary Donusum");
            treeView1.Nodes.Add(nodeTemel);

            TreeNode nodeGeometrik = new TreeNode("Geometrik Islemler");
            nodeGeometrik.Nodes.Add("Goruntu Dondurme");
            nodeGeometrik.Nodes.Add("Goruntu Kirpma");
            nodeGeometrik.Nodes.Add("Goruntu Yaklastirma/Uzaklastirma");
            treeView1.Nodes.Add(nodeGeometrik);

            TreeNode nodeGelistirme = new TreeNode("Gelistirme");
            nodeGelistirme.Nodes.Add("Iki Resim Arasinda Aritmetik Islemler (ekleme, bolme)");
            nodeGelistirme.Nodes.Add("Histogram Islemleri (germe/genisletme)");
            nodeGelistirme.Nodes.Add("Kontrast Artirma");
            treeView1.Nodes.Add(nodeGelistirme);

            TreeNode nodeFiltreler = new TreeNode("Filtreler");
            nodeFiltreler.Nodes.Add("Esikleme islemleri (Tek Esikleme)");
            nodeFiltreler.Nodes.Add("Gurultu Ekleme (Salt&Pepper)/Temizleme (mean, median)");
            nodeFiltreler.Nodes.Add("Konvolusyon Islemi (mean)");
            nodeFiltreler.Nodes.Add("Morfolojik Islemler (Genisleme, Asinma, Acma, Kapama)");
            nodeFiltreler.Nodes.Add("Goruntüye Filtre Uygulanmasi (Unsharp)");
            nodeFiltreler.Nodes.Add("Kenar Bulma Algoritmalarinin Kullanimi (prewitt)");
            treeView1.Nodes.Add(nodeFiltreler);

            TreeNode nodeDiger = new TreeNode("Diger Islemler");
            nodeDiger.Nodes.Add("Renk Uzayi Donusumleri");
            treeView1.Nodes.Add(nodeDiger);

            treeView1.ExpandAll();
            PanelleriGizle();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                PanelleriGizle();

                switch (e.Node.Text)
                {
                    case "Binary Donusum":
                        pnlBinary.Visible = true;
                        break;
                    case "Goruntu Dondurme":
                        pnlRotate.Visible = true;
                        if (cmbRotateInterpolation.SelectedIndex == -1) cmbRotateInterpolation.SelectedIndex = 1;
                        break;
                    case "Goruntu Kirpma":
                        pnlCrop.Visible = true;
                        break;
                    case "Goruntu Yaklastirma/Uzaklastirma":
                        pnlScale.Visible = true;
                        if (cmbScaleInterpolation.SelectedIndex == -1) cmbScaleInterpolation.SelectedIndex = 1;
                        break;
                    case "Iki Resim Arasinda Aritmetik Islemler (ekleme, bolme)":
                        pnlArithmetic.Visible = true;
                        picInput2.Visible = true;
                        tableLayoutPanel1.ColumnStyles[0].Width = 33.33f;
                        tableLayoutPanel1.ColumnStyles[1].Width = 33.33f;
                        tableLayoutPanel1.ColumnStyles[2].Width = 33.33f;
                        break;
                    case "Histogram Islemleri (germe/genisletme)":
                        pnlHistogram.Visible = true;
                        if (cmbHistogram.SelectedIndex == -1) cmbHistogram.SelectedIndex = 0;
                        break;
                    case "Kontrast Artirma":
                        pnlContrast.Visible = true;
                        break;
                    case "Esikleme islemleri (Tek Esikleme)":
                        pnlThresholding.Visible = true;
                        if (cmbThresholdMatrix.SelectedIndex == -1) cmbThresholdMatrix.SelectedIndex = 0;
                        rbStaticThreshold.Checked = true;
                        rbThreshold_CheckedChanged(null, null);
                        break;
                    case "Gurultu Ekleme (Salt&Pepper)/Temizleme (mean, median)":
                        pnlNoise.Visible = true;
                        if (cmbNoiseAdd.SelectedIndex == -1) cmbNoiseAdd.SelectedIndex = 0;
                        if (cmbNoiseRemove.SelectedIndex == -1) cmbNoiseRemove.SelectedIndex = 0;
                        if (cmbNoiseMatrixSize.SelectedIndex == -1) cmbNoiseMatrixSize.SelectedIndex = 0;
                        rbNoiseAdd.Checked = true;
                        rbNoise_CheckedChanged(null, null);
                        break;
                    case "Konvolusyon Islemi (mean)":
                        pnlMatrixFilter.Visible = true;
                        if (cmbMatrixSize.SelectedIndex == -1) cmbMatrixSize.SelectedIndex = 0;
                        break;
                    case "Morfolojik Islemler (Genisleme, Asinma, Acma, Kapama)":
                        pnlMorphology.Visible = true;
                        if (cmbMorphologyType.SelectedIndex == -1) cmbMorphologyType.SelectedIndex = 0;
                        if (cmbMorphMatrixSize.SelectedIndex == -1) cmbMorphMatrixSize.SelectedIndex = 0;
                        if (cmbMorphShape.SelectedIndex == -1) cmbMorphShape.SelectedIndex = 0;
                        GenerateMorphMatrix();
                        break;
                    case "Goruntüye Filtre Uygulanmasi (Unsharp)":
                        pnlUnsharp.Visible = true;
                        if (cmbUnsharpMatrix.SelectedIndex == -1) cmbUnsharpMatrix.SelectedIndex = 0;
                        break;
                    case "Kenar Bulma Algoritmalarinin Kullanimi (prewitt)":
                        pnlEdgeDetection.Visible = true;
                        if (cmbEdgeType.SelectedIndex == -1) cmbEdgeType.SelectedIndex = 0;
                        GenerateEdgeMatrix();
                        break;
                    case "Renk Uzayi Donusumleri":
                        pnlColorSpace.Visible = true;
                        RenkUzayiComboDoldur();
                        break;
                }
            }
        }

        private static readonly string[] RenkUzayiSecenekleri =
        {
            "RGB -> HSV",
            "HSV -> RGB",
            "RGB -> YCbCr",
            "YCbCr -> RGB",
            "RGB -> CMYK",
            "RGB -> Gri (Luminance)"
        };

        private void RenkUzayiComboDoldur()
        {
            string onceki = cmbColorSpace.SelectedItem?.ToString();
            cmbColorSpace.Items.Clear();
            cmbColorSpace.Items.AddRange(RenkUzayiSecenekleri);
            int idx = Array.IndexOf(RenkUzayiSecenekleri, onceki);
            cmbColorSpace.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void PanelleriGizle()
        {
            if (picInput2 != null) picInput2.Visible = false;
            tableLayoutPanel1.ColumnStyles[0].Width = 50f;
            tableLayoutPanel1.ColumnStyles[1].Width = 0f;
            tableLayoutPanel1.ColumnStyles[2].Width = 50f;
            if (pnlBinary != null) pnlBinary.Visible = false;
            if (pnlRotate != null) pnlRotate.Visible = false;
            if (pnlCrop != null) pnlCrop.Visible = false;
            if (pnlScale != null) pnlScale.Visible = false;
            if (pnlArithmetic != null) pnlArithmetic.Visible = false;
            if (pnlHistogram != null) pnlHistogram.Visible = false;
            if (pnlContrast != null) pnlContrast.Visible = false;
            if (pnlThresholding != null) pnlThresholding.Visible = false;
            if (pnlNoise != null) pnlNoise.Visible = false;
            if (pnlMatrixFilter != null) pnlMatrixFilter.Visible = false;
            if (pnlMorphology != null) pnlMorphology.Visible = false;
            if (pnlUnsharp != null) pnlUnsharp.Visible = false;
            if (pnlEdgeDetection != null) pnlEdgeDetection.Visible = false;
            if (pnlColorSpace != null) pnlColorSpace.Visible = false;
            if (picHistogram != null) picHistogram.Image = null;
            if (picHistogramResult != null) picHistogramResult.Image = null;
        }

        private void btnUygula_Click(object sender, EventArgs e)
        {
            if (picInput1.Image == null)
            {
                MessageBox.Show("HATA: Sol kutuda islenecek bir resim yok!", "Hata");
                return;
            }
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("HATA: Sol menuден hicbir islem secilmemis!", "Hata");
                return;
            }

            try
            {
                picOutput.Dock = DockStyle.Fill;
                picOutput.SizeMode = PictureBoxSizeMode.Zoom;

                switch (treeView1.SelectedNode.Text)
                {
                    case "Gri Donusum":
                        picOutput.Image = ImageProcessor.ConvertToGrayscale(new Bitmap(picInput1.Image));
                        break;
                    case "Binary Donusum":
                        picOutput.Image = ImageProcessor.ConvertToBinary(new Bitmap(picInput1.Image), trkThreshold.Value);
                        break;
                    case "Goruntu Dondurme":
                        picOutput.Dock = DockStyle.None;
                        picOutput.SizeMode = PictureBoxSizeMode.AutoSize;
                        picOutput.Image = ImageProcessor.GoruntuDondur((Bitmap)picInput1.Image, (int)numAngle.Value, cmbRotateInterpolation.Text);
                        break;
                    case "Kontrast Artirma":
                        picOutput.Image = ImageProcessor.KontrastUygula(new Bitmap(picInput1.Image), trkContrast.Value);
                        break;
                    case "Goruntu Yaklastirma/Uzaklastirma":
                        picOutput.Dock = DockStyle.None;
                        picOutput.SizeMode = PictureBoxSizeMode.AutoSize;
                        string intYontem = cmbScaleInterpolation.SelectedItem?.ToString() ?? "Bilinear";
                        double secilenOran = Convert.ToDouble(numScale.Value);
                        if (secilenOran > 0)
                        {
                            Bitmap kaynakOlcek = new Bitmap(picInput1.Image);
                            Bitmap sonucOlcek = ImageProcessor.GoruntuOlcekle(kaynakOlcek, secilenOran, intYontem);
                            picOutput.Image = sonucOlcek;
                            MessageBox.Show($"Orijinal: {kaynakOlcek.Width}x{kaynakOlcek.Height}\nYeni: {sonucOlcek.Width}x{sonucOlcek.Height}\nYontem: {intYontem}", "Basarili");
                        }
                        break;
                    case "Goruntu Kirpma":
                        picOutput.Dock = DockStyle.None;
                        picOutput.SizeMode = PictureBoxSizeMode.AutoSize;
                        int genislik = (int)numCropHeight.Value;
                        int yukseklik = (int)numCropWidth.Value;
                        if (genislik <= 0 || yukseklik <= 0) { MessageBox.Show("Genislik ve Yukseklik 0'dan buyuk olmali.", "Hata"); break; }
                        picOutput.Image = ImageProcessor.CropImage(new Bitmap(picInput1.Image), (int)numCropX.Value, (int)numCropY.Value, (int)numCropX.Value + genislik, (int)numCropY.Value + yukseklik);
                        break;
                    case "Histogram Islemleri (germe/genisletme)":
                        Bitmap srcH = new Bitmap(picInput1.Image);
                        picHistogram.Image = PikselIslem.HistogramCiz(PikselIslem.HistogramHesapla(srcH));
                        string secimHist = cmbHistogram.SelectedItem?.ToString() ?? "Histogram Germe";
                        Bitmap cikisHist = secimHist == "Histogram Genisletme" ? PikselIslem.HistogramGenisletme(srcH) : PikselIslem.HistogramGerme(srcH);
                        picOutput.Image = cikisHist;
                        picHistogramResult.Image = PikselIslem.HistogramCiz(PikselIslem.HistogramHesapla(new Bitmap(cikisHist)));
                        break;
                    case "Iki Resim Arasinda Aritmetik Islemler (ekleme, bolme)":
                        if (picInput2.Image == null) { MessageBox.Show("2. resim yukleyin!", "Hata"); return; }
                        if (cmbAritmetik.SelectedItem == null) { MessageBox.Show("Islem secin.", "Hata"); return; }
                        Bitmap r1 = new Bitmap(picInput1.Image), r2 = new Bitmap(picInput2.Image);
                        string islem = cmbAritmetik.SelectedItem.ToString();
                        Bitmap sonucA = islem == "Toplam" ? GoruntuIslem.ResimTopla(r1, r2) : islem == "Cikarma" ? GoruntuIslem.ResimCikar(r1, r2) : islem == "Carpma" ? GoruntuIslem.ResimCarp(r1, r2) : GoruntuIslem.ResimBol(r1, r2);
                        if (sonucA != null) picOutput.Image = sonucA;
                        else MessageBox.Show("Resim boyutlari esit olmali!", "Hata");
                        break;
                    case "Morfolojik Islemler (Genisleme, Asinma, Acma, Kapama)":
                        string islemTuru = cmbMorphologyType.SelectedItem.ToString();
                        int mBoyut = int.Parse(cmbMorphMatrixSize.SelectedItem.ToString().Substring(0, 1));
                        string mSekil = cmbMorphShape.SelectedItem.ToString();
                        if (islemTuru == "Genisleme (Dilation)") picOutput.Image = GoruntuIslem.Genisleme(new Bitmap(picInput1.Image), mBoyut, mSekil);
                        else if (islemTuru == "Asinma (Erosion)") picOutput.Image = GoruntuIslem.Asinma(new Bitmap(picInput1.Image), mBoyut, mSekil);
                        else if (islemTuru == "Acma (Opening)") picOutput.Image = GoruntuIslem.Acma(new Bitmap(picInput1.Image), mBoyut, mSekil);
                        else if (islemTuru == "Kapama (Closing)") picOutput.Image = GoruntuIslem.Kapama(new Bitmap(picInput1.Image), mBoyut, mSekil);
                        break;
                    case "Goruntüye Filtre Uygulanmasi (Unsharp)":
                        picOutput.Image = GoruntuIslem.UnsharpMask(new Bitmap(picInput1.Image), 1.5);
                        break;
                    case "Kenar Bulma Algoritmalarinin Kullanimi (prewitt)":
                        picOutput.Image = ImageProcessor.ApplyPrewitt(new Bitmap(picInput1.Image), cmbEdgeType.SelectedItem.ToString());
                        break;
                    case "Konvolusyon Islemi (mean)":
                        if (cmbMatrixSize.SelectedItem == null) { MessageBox.Show("Matris boyutu secin.", "Hata"); return; }
                        picOutput.Image = PikselIslem.MeanKonvolusyon(new Bitmap(picInput1.Image), int.Parse(cmbMatrixSize.SelectedItem.ToString().Split('x')[0]));
                        break;
                    case "Renk Uzayi Donusumleri":
                        if (cmbColorSpace.SelectedItem == null) { MessageBox.Show("Renk uzayi secin.", "Hata"); return; }
                        string hedefUzay = cmbColorSpace.SelectedItem.ToString();
                        Bitmap srcRu = new Bitmap(picInput1.Image);
                        if (hedefUzay == "RGB -> HSV") picOutput.Image = PikselIslem.RgbToHsv(srcRu);
                        else if (hedefUzay == "HSV -> RGB") picOutput.Image = PikselIslem.HsvToRgb(srcRu);
                        else if (hedefUzay == "RGB -> YCbCr") picOutput.Image = PikselIslem.RgbToYCbCr(srcRu);
                        else if (hedefUzay == "YCbCr -> RGB") picOutput.Image = PikselIslem.YCbCrToRgb(srcRu);
                        else if (hedefUzay == "RGB -> CMYK") picOutput.Image = PikselIslem.RgbToCmyk(srcRu);
                        else if (hedefUzay == "RGB -> Gri (Luminance)") picOutput.Image = PikselIslem.RgbToGri(srcRu);
                        break;
                    case "Gurultu Ekleme (Salt&Pepper)/Temizleme (mean, median)":
                        Bitmap kaynakResim = new Bitmap(picInput1.Image);
                        if (rbNoiseAdd.Checked)
                        {
                            if (cmbNoiseAdd.SelectedItem != null)
                                picOutput.Image = ImageProcessor.AddNoise(kaynakResim, trkNoisePercentage.Value, cmbNoiseAdd.SelectedItem.ToString());
                        }
                        else
                        {
                            if (cmbNoiseRemove.SelectedItem != null && cmbNoiseMatrixSize.SelectedItem != null)
                            {
                                int matrisBoyutu = int.Parse(cmbNoiseMatrixSize.SelectedItem.ToString().Split('x')[0]);
                                picOutput.Image = cmbNoiseRemove.SelectedItem.ToString() == "Mean Filtresi" ? ImageProcessor.ApplyMeanFilter(kaynakResim, matrisBoyutu) : ImageProcessor.ApplyMedianFilter(kaynakResim, matrisBoyutu);
                            }
                        }
                        break;
                    case "Esikleme islemleri (Tek Esikleme)":
                        Bitmap srcThreshold = new Bitmap(picInput1.Image);
                        if (rbStaticThreshold.Checked)
                            picOutput.Image = ImageProcessor.ApplyStaticThreshold(srcThreshold, trkTreshold2.Value);
                        else
                            picOutput.Image = ImageProcessor.ApplyDynamicThreshold(srcThreshold, int.Parse(cmbThresholdMatrix.SelectedItem.ToString().Split('x')[0]));
                        break;
                    default:
                        MessageBox.Show("Bu islem henuz kodlanmadi.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata");
            }
        }

        private void resimYükleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Resim|*.jpg;*.jpeg;*.png;*.bmp|Tum Dosyalar|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try { picInput1.BackColor = Color.White; picInput1.Image = new Bitmap(Image.FromFile(ofd.FileName)); }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
        }

        private void ikinciResmiYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Resim|*.jpg;*.jpeg;*.png;*.bmp|Tum Dosyalar|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try { picInput2.BackColor = Color.White; picInput2.Image = new Bitmap(Image.FromFile(ofd.FileName)); }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
        }

        private void rbNoise_CheckedChanged(object sender, EventArgs e)
        {
            cmbNoiseAdd.Enabled = rbNoiseAdd.Checked;
            trkNoisePercentage.Enabled = rbNoiseAdd.Checked;
            numNoisePercentage.Enabled = rbNoiseAdd.Checked;
            cmbNoiseRemove.Enabled = !rbNoiseAdd.Checked;
            cmbNoiseMatrixSize.Enabled = !rbNoiseAdd.Checked;
        }

        private void rbThreshold_CheckedChanged(object sender, EventArgs e)
        {
            trkTreshold2.Enabled = rbStaticThreshold.Checked;
            numThreshold.Enabled = rbStaticThreshold.Checked;
            cmbThresholdMatrix.Enabled = !rbStaticThreshold.Checked;
        }

        private void UpdateMorphMatrix_Event(object sender, EventArgs e) { GenerateMorphMatrix(); }

        private void GenerateMorphMatrix()
        {
            if (cmbMorphMatrixSize.SelectedIndex == -1 || cmbMorphShape.SelectedIndex == -1) return;
            gbMorphMatrix.Controls.Clear();
            int size = cmbMorphMatrixSize.SelectedIndex == 1 ? 5 : cmbMorphMatrixSize.SelectedIndex == 2 ? 7 : 3;
            string shape = cmbMorphShape.SelectedItem.ToString();
            bool isCustom = shape == "Ozel (Custom)";
            morphMatrixTextBoxes = new System.Windows.Forms.TextBox[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox
                    {
                        Size = new System.Drawing.Size(35, 35),
                        Location = new System.Drawing.Point(20 + j * 40, 35 + i * 40),
                        TextAlign = HorizontalAlignment.Center,
                        MaxLength = 1,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        ReadOnly = !isCustom
                    };
                    if (shape == "Kare (Square)") txt.Text = "1";
                    else if (shape == "Hac (Cross)") txt.Text = (i == size / 2 || j == size / 2) ? "1" : "0";
                    else if (shape == "Daire (Circle)") { double c = size / 2.0; txt.Text = Math.Sqrt(Math.Pow(i - c, 2) + Math.Pow(j - c, 2)) <= c ? "1" : "0"; }
                    else txt.Text = "0";
                    txt.BackColor = txt.Text == "0" ? Color.LightGray : Color.White;
                    morphMatrixTextBoxes[i, j] = txt;
                    gbMorphMatrix.Controls.Add(txt);
                }
            }
        }

        private void UpdateEdgeMatrix_Event(object sender, EventArgs e) { GenerateEdgeMatrix(); }

        private void GenerateEdgeMatrix()
        {
            if (cmbEdgeType.SelectedIndex == -1) return;
            if (cmbEdgeType.SelectedIndex == 0)
            {
                edgeMatrixLabels[0, 0].Text = "-1"; edgeMatrixLabels[0, 1].Text = "0"; edgeMatrixLabels[0, 2].Text = "1";
                edgeMatrixLabels[1, 0].Text = "-1"; edgeMatrixLabels[1, 1].Text = "0"; edgeMatrixLabels[1, 2].Text = "1";
                edgeMatrixLabels[2, 0].Text = "-1"; edgeMatrixLabels[2, 1].Text = "0"; edgeMatrixLabels[2, 2].Text = "1";
            }
            else if (cmbEdgeType.SelectedIndex == 1)
            {
                edgeMatrixLabels[0, 0].Text = "1"; edgeMatrixLabels[0, 1].Text = "1"; edgeMatrixLabels[0, 2].Text = "1";
                edgeMatrixLabels[1, 0].Text = "0"; edgeMatrixLabels[1, 1].Text = "0"; edgeMatrixLabels[1, 2].Text = "0";
                edgeMatrixLabels[2, 0].Text = "-1"; edgeMatrixLabels[2, 1].Text = "-1"; edgeMatrixLabels[2, 2].Text = "-1";
            }
            else
            {
                for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++) edgeMatrixLabels[i, j].Text = "";
                edgeMatrixLabels[1, 1].Text = "Gx+Gy";
            }
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    edgeMatrixLabels[i, j].BackColor = edgeMatrixLabels[i, j].Text == "0" ? Color.LightGray : Color.White;
        }

        private void CreateEdgeMatrixGrid()
        {
            edgeMatrixLabels = new Label[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    edgeMatrixLabels[i, j] = new Label { Text = "0", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 12F, FontStyle.Bold) };
                    tlpEdgeMatrix.Controls.Add(edgeMatrixLabels[i, j], j, i);
                }
        }

        private void cmbHistogram_SelectedIndexChanged(object sender, EventArgs e) { }

        private void TrkTreshold2_ValueChanged(object sender, EventArgs e)
        {
            if (numThreshold.Value != (decimal)trkTreshold2.Value) numThreshold.Value = (decimal)trkTreshold2.Value;
        }

        private void NumThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (trkTreshold2.Value != (int)numThreshold.Value) trkTreshold2.Value = (int)numThreshold.Value;
        }

        private void TrkNoisePercentage_ValueChanged(object sender, EventArgs e)
        {
            if (numNoisePercentage.Value != trkNoisePercentage.Value) numNoisePercentage.Value = trkNoisePercentage.Value;
        }

        private void NumNoisePercentage_ValueChanged(object sender, EventArgs e)
        {
            if (trkNoisePercentage.Value != (int)numNoisePercentage.Value) trkNoisePercentage.Value = (int)numNoisePercentage.Value;
        }
    }
}