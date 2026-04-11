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
            CreateEdgeMatrixGrid();// Arayüzü Form1.Designer.cs'den yükler
                                   // TrackBar kaydığında sayıyı güncellemesi için:
            trkTreshold2.ValueChanged += TrkTreshold2_ValueChanged;

            // Sayı değiştiğinde TrackBar'ı kaydırması için:
            numThreshold.ValueChanged += NumThreshold_ValueChanged;
        }

        private TextBox[,] morphMatrixTextBoxes;

        // --- 1. FORM YÜKLENDİĞİNDE (MENÜYÜ DOLDUR) ---
        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            TreeNode nodeTemel = new TreeNode("Temel İşlemler");
            nodeTemel.Nodes.Add("Gri Dönüşüm");
            nodeTemel.Nodes.Add("Binary Dönüşüm");
            treeView1.Nodes.Add(nodeTemel);

            TreeNode nodeGeometrik = new TreeNode("Geometrik İşlemler");
            nodeGeometrik.Nodes.Add("Görüntü Döndürme");
            nodeGeometrik.Nodes.Add("Görüntü Kırpma");
            nodeGeometrik.Nodes.Add("Görüntü Yaklaştırma/Uzaklaştırma");
            treeView1.Nodes.Add(nodeGeometrik);

            TreeNode nodeGelistirme = new TreeNode("Geliştirme");
            nodeGelistirme.Nodes.Add("İki Resim Arasında Aritmetik İşlemler (ekleme, bölme)");
            nodeGelistirme.Nodes.Add("Histogram İşlemleri (germe/genişletme)");
            nodeGelistirme.Nodes.Add("Kontrast Artırma");
            treeView1.Nodes.Add(nodeGelistirme);

            TreeNode nodeFiltreler = new TreeNode("Filtreler");
            nodeFiltreler.Nodes.Add("Eşikleme işlemleri (Tek Eşikleme)");
            nodeFiltreler.Nodes.Add("Gürültü Ekleme (Salt&Pepper)/Temizleme (mean, median)");
            nodeFiltreler.Nodes.Add("Konvolüsyon İşlemi (mean)");
            nodeFiltreler.Nodes.Add("Morfolojik İşlemler (Genişleme, Aşınma, Açma, Kapama)");
            nodeFiltreler.Nodes.Add("Görüntüye Filtre Uygulanması (Unsharp)");
            nodeFiltreler.Nodes.Add("Kenar Bulma Algoritmalarının Kullanımı (prewitt)");
            treeView1.Nodes.Add(nodeFiltreler);

            TreeNode nodeDiger = new TreeNode("Diğer İşlemler");
            nodeDiger.Nodes.Add("Renk Uzayı Dönüşümleri");
            treeView1.Nodes.Add(nodeDiger);

            treeView1.ExpandAll();

            // Program ilk açıldığında sağ alttaki panelleri gizle
            PanelleriGizle();
        }

        // --- 2. MENÜDEN İŞLEM SEÇİLDİĞİNDE ---
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0) // Sadece tıklanabilir alt başlıklarsa
            {
                PanelleriGizle(); // Önce ekranı temizle

                switch (e.Node.Text)
                {
                    case "Binary Dönüşüm":
                        pnlBinary.Visible = true;
                        break;
                    case "Görüntü Döndürme":
                        pnlRotate.Visible = true;
                        // ComboBox boş gelmesin
                        if (cmbRotateInterpolation.SelectedIndex == -1) cmbRotateInterpolation.SelectedIndex = 1;
                        break;
                    case "Görüntü Kırpma":
                        pnlCrop.Visible = true;
                        break;
                    case "Görüntü Yaklaştırma/Uzaklaştırma":
                        pnlScale.Visible = true;
                        if (cmbScaleInterpolation.SelectedIndex == -1) cmbScaleInterpolation.SelectedIndex = 1;
                        break;
                    case "İki Resim Arasında Aritmetik İşlemler (ekleme, bölme)":
                        pnlArithmetic.Visible = true;
                        picInput2.Visible = true;
                        // Tabloyu 3 eşit parçaya böl (%33)
                        tableLayoutPanel1.ColumnStyles[0].Width = 33.33f;
                        tableLayoutPanel1.ColumnStyles[1].Width = 33.33f;
                        tableLayoutPanel1.ColumnStyles[2].Width = 33.33f;
                        break;
                    case "Histogram İşlemleri (germe/genişletme)":
                        pnlHistogram.Visible = true;
                        if (cmbHistogram.SelectedIndex == -1) cmbHistogram.SelectedIndex = 0;
                        break;
                    case "Kontrast Artırma":
                        pnlContrast.Visible = true;
                        break;
                    case "Eşikleme işlemleri (Tek Eşikleme)":
                        pnlThresholding.Visible = true;
                        if (cmbThresholdMatrix.SelectedIndex == -1) cmbThresholdMatrix.SelectedIndex = 0;

                        // Varsayılan ayarları tetikle
                        rbStaticThreshold.Checked = true;
                        rbThreshold_CheckedChanged(null, null);
                        break;
                    case "Gürültü Ekleme (Salt&Pepper)/Temizleme (mean, median)":
                        pnlNoise.Visible = true;
                        if (cmbNoiseAdd.SelectedIndex == -1) cmbNoiseAdd.SelectedIndex = 0;
                        if (cmbNoiseRemove.SelectedIndex == -1) cmbNoiseRemove.SelectedIndex = 0;
                        if (cmbNoiseMatrixSize.SelectedIndex == -1) cmbNoiseMatrixSize.SelectedIndex = 0;

                        // Varsayılan olarak ekleme seçili gelsin ve event tetiklensin
                        rbNoiseAdd.Checked = true;
                        rbNoise_CheckedChanged(null, null);
                        break;
                    case "Konvolüsyon İşlemi (mean)":
                        pnlMatrixFilter.Visible = true;
                        if (cmbMatrixSize.SelectedIndex == -1) cmbMatrixSize.SelectedIndex = 0;
                        break;
                    case "Morfolojik İşlemler (Genişleme, Aşınma, Açma, Kapama)":
                        pnlMorphology.Visible = true;
                        if (cmbMorphologyType.SelectedIndex == -1) cmbMorphologyType.SelectedIndex = 0;
                        if (cmbMorphMatrixSize.SelectedIndex == -1) cmbMorphMatrixSize.SelectedIndex = 0;
                        if (cmbMorphShape.SelectedIndex == -1) cmbMorphShape.SelectedIndex = 0; // Varsayılan Kare

                        GenerateMorphMatrix(); // Ekran açılır açılmaz matrisi çiz
                        break;
                    case "Görüntüye Filtre Uygulanması (Unsharp)":
                        pnlUnsharp.Visible = true;
                        if (cmbUnsharpMatrix.SelectedIndex == -1) cmbUnsharpMatrix.SelectedIndex = 0;
                        break;
                    case "Kenar Bulma Algoritmalarının Kullanımı (prewitt)":
                        pnlEdgeDetection.Visible = true;
                        if (cmbEdgeType.SelectedIndex == -1) cmbEdgeType.SelectedIndex = 0; // Varsayılan Yatay

                        GenerateEdgeMatrix(); // Ekran açılır açılmaz matrisi çiz
                        break;
                    case "Renk Uzayı Dönüşümleri":
                        pnlColorSpace.Visible = true;
                        if (cmbColorSpace.SelectedIndex == -1) cmbColorSpace.SelectedIndex = 0; // Varsayılan: RGB -> HSV
                        break;
                }
            }
        }

        // --- 3. EKRAN TEMİZLEME YARDIMCISI ---
        private void PanelleriGizle()
        {
            // Ortadaki 2. resim kutusunu gizle
            if (picInput2 != null) picInput2.Visible = false;

            // TABLOYU DÜZELT: Ortadaki sütunu (Column 1) yok et, Giriş ve Çıkış'ı %50-%50 yay.
            tableLayoutPanel1.ColumnStyles[0].Width = 50f; // 1. Resim %50 alan kaplasın
            tableLayoutPanel1.ColumnStyles[1].Width = 0f;  // 2. Resim alanını tamamen sıfırla (Gizle)
            tableLayoutPanel1.ColumnStyles[2].Width = 50f; // Çıktı Resmi %50 alan kaplasın

            // Panelleri gizle
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

            // Histogram tuvalini temizle
            if (picHistogram != null) picHistogram.Image = null;
            if (picHistogramResult != null) picHistogramResult.Image = null;
        }

        // --- 4. İŞLEMİ UYGULA BUTONU ---

        private void btnUygula_Click(object sender, EventArgs e)
        {


            // 1. KONTROL: Resim var mı?
            if (picInput1.Image == null)
            {
                MessageBox.Show("HATA: Sol kutuda işlenecek bir resim yok!", "Detektif 1");
                return;
            }

            // 2. KONTROL: Menüden bir şey seçilmiş mi?
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("HATA: Sol menüden hiçbir işlem seçilmemiş!", "Detektif 2");
                return;
            }

            // Seçilen menü isminin başındaki ve sonundaki görünmez boşlukları temizleyelim (Trim)
            string secilenIslem = treeView1.SelectedNode.Text.Trim();

            try
            {
                switch (treeView1.SelectedNode.Text)
                {
                    case "Gri Dönüşüm":
                        MessageBox.Show("Griye çevirme işlemi başlatılıyor...");
                        break;

                    case "Binary Dönüşüm":
                        int esikDegeri = trkThreshold.Value;
                        MessageBox.Show($"Resim {esikDegeri} eşik değeri ile Siyah-Beyaz yapılıyor...");
                        break;

                    case "Görüntü Döndürme":
                        int aci = (int)numAngle.Value;
                        string enterpolasyon = cmbRotateInterpolation.SelectedItem != null ? cmbRotateInterpolation.SelectedItem.ToString() : "Bilinear";
                        MessageBox.Show($"Resim {aci} derece döndürülüyor. Yöntem: {enterpolasyon}");
                        break;

                    case "Histogram İşlemleri (germe/genişletme)":

                        if (secilenIslem == "Histogram Tablosu")
                        {
                            MessageBox.Show("Histogram hesaplanıp aşağıdaki beyaz tuvale çizilecek!");
                            // TODO: picHistogram üzerine Graphics ile çizim kodları buraya gelecek
                        }
                        else
                        {
                            MessageBox.Show($"{secilenIslem} işlemi üstteki resme uygulanıyor...");
                            // TODO: Germe/Eşitleme algoritmaları buraya gelecek
                        }
                        break;

                    case "Morfolojik İşlemler (Genişleme, Aşınma, Açma, Kapama)":
                        string islemTuru = cmbMorphologyType.SelectedItem.ToString();
                        string morphMatris = cmbMorphMatrixSize.SelectedItem.ToString();
                        MessageBox.Show($"Morfolojik işlem uygulanıyor...\nTür: {islemTuru}\nMatris: {morphMatris}");
                        break;

                    case "Kenar Bulma Algoritmalarının Kullanımı (prewitt)":
                        Bitmap srcPrewitt = new Bitmap(picInput1.Image);
                        string secilenYon = cmbEdgeType.SelectedItem.ToString();

                        // Kenar bulma filtresini uygula
                        picOutput.Image = ImageProcessor.ApplyPrewitt(srcPrewitt, secilenYon);
                        break;

                    case "Renk Uzayı Dönüşümleri":
                        string hedefUzay = cmbColorSpace.SelectedItem.ToString();
                        MessageBox.Show($"Renk dönüşümü başlatılıyor...\nSeçilen Dönüşüm: {hedefUzay}");
                        break;

                    case "Gürültü Ekleme (Salt&Pepper)/Temizleme (mean, median)":
                        Bitmap kaynakResim = new Bitmap(picInput1.Image);

                        if (rbNoiseAdd.Checked) // EKLEME
                        {
                            if (cmbNoiseAdd.SelectedItem != null)
                            {
                                string gurultuTuru = cmbNoiseAdd.SelectedItem.ToString();
                                // Türü doğrudan metoda gönderiyoruz (Salt, Pepper veya Salt & Pepper)
                                picOutput.Image = ImageProcessor.AddNoise(kaynakResim, 10, gurultuTuru);
                            }
                        }
                        else // TEMİZLEME
                        {
                            if (cmbNoiseRemove.SelectedItem != null && cmbNoiseMatrixSize.SelectedItem != null)
                            {
                                string filtreTuru = cmbNoiseRemove.SelectedItem.ToString();

                                // "5x5" gibi bir string'den '5' rakamını alıyoruz
                                int matrisBoyutu = int.Parse(cmbNoiseMatrixSize.SelectedItem.ToString().Split('x')[0]);

                                if (filtreTuru == "Mean Filtresi")
                                {
                                    picOutput.Image = ImageProcessor.ApplyMeanFilter(kaynakResim, matrisBoyutu);
                                }
                                else if (filtreTuru == "Median Filtresi")
                                {
                                    picOutput.Image = ImageProcessor.ApplyMedianFilter(kaynakResim, matrisBoyutu);
                                }
                            }
                        }
                        break;

                    case "Eşikleme işlemleri (Tek Eşikleme)":
                        Bitmap srcThreshold = new Bitmap(picInput1.Image);

                        if (rbStaticThreshold.Checked)
                        {
                            // Statik eşikleme
                            int esik = trkTreshold2.Value;
                            picOutput.Image = ImageProcessor.ApplyStaticThreshold(srcThreshold, esik);
                        }
                        else
                        {
                            // Dinamik (Adaptif) eşikleme - Combobox'tan boyutu sayı olarak ayıklıyoruz
                            string matrisStr = cmbThresholdMatrix.SelectedItem.ToString(); // Örn: "5x5"
                            int matrisBoyutu = int.Parse(matrisStr.Split('x')[0]); // "5" rakamını alır

                            picOutput.Image = ImageProcessor.ApplyDynamicThreshold(srcThreshold, matrisBoyutu);
                        }
                        break;

                    default:
                        MessageBox.Show("Bu işlem henüz kodlanmadı.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 5. RESİM YÜKLEME KODLARI ---
        private void resimYükleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var tempImage = Image.FromFile(ofd.FileName))
                    {
                        picInput1.BackColor = Color.White;
                        picInput1.Image = new Bitmap(tempImage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Birinci resim yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ikinciResmiYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var tempImage = Image.FromFile(ofd.FileName))
                    {
                        picInput2.BackColor = Color.White;
                        picInput2.Image = new Bitmap(tempImage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İkinci resim yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rbNoise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoiseAdd.Checked)
            {
                cmbNoiseAdd.Enabled = true;
                cmbNoiseRemove.Enabled = false;
                cmbNoiseMatrixSize.Enabled = false; // Eklemede matris kutusu kapalı
            }
            else
            {
                cmbNoiseAdd.Enabled = false;
                cmbNoiseRemove.Enabled = true;
                cmbNoiseMatrixSize.Enabled = true; // Temizlemede matris kutusu açık
            }
        }

        private void rbThreshold_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStaticThreshold.Checked)
            {
                trkTreshold2.Enabled = true;
                numThreshold.Enabled = true; // Sayı kutusu aktif
                cmbThresholdMatrix.Enabled = false;
            }
            else
            {
                trkTreshold2.Enabled = false;
                numThreshold.Enabled = false; // Sayı kutusu pasif
                cmbThresholdMatrix.Enabled = true;
            }
        }

        // Seçimlerden herhangi biri değiştiğinde matrisi yeniden çizecek olay
        private void UpdateMorphMatrix_Event(object sender, EventArgs e)
        {
            GenerateMorphMatrix();
        }

        // Matrisi dinamik olarak ekrana çizen asıl beyin
        private void GenerateMorphMatrix()
        {
            if (cmbMorphMatrixSize.SelectedIndex == -1 || cmbMorphShape.SelectedIndex == -1) return;

            gbMorphMatrix.Controls.Clear(); // Eski matrisi temizle

            // Boyutu belirle
            int size = 3;
            if (cmbMorphMatrixSize.SelectedIndex == 1) size = 5;
            else if (cmbMorphMatrixSize.SelectedIndex == 2) size = 7;

            string shape = cmbMorphShape.SelectedItem.ToString();
            bool isCustom = (shape == "Özel (Custom)"); // Sadece özel seçiliyse dışarıdan veri girilebilir

            morphMatrixTextBoxes = new TextBox[size, size];

            int cellSize = 35; // Her bir kutunun boyutu
            int startX = 20;   // Çerçeveden iç boşluk
            int startY = 35;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TextBox txt = new TextBox();
                    txt.Size = new Size(cellSize, cellSize);
                    txt.Location = new Point(startX + j * (cellSize + 5), startY + i * (cellSize + 5));
                    txt.TextAlign = HorizontalAlignment.Center;
                    txt.MaxLength = 1; // Sadece 1 karakter (0 veya 1)
                    txt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    txt.ReadOnly = !isCustom; // Özel değilse kutuları kilitle

                    // Şekle göre 0 veya 1 atama mantığı
                    if (shape == "Kare (Square)")
                    {
                        txt.Text = "1";
                    }
                    else if (shape == "Haç (Cross)")
                    {
                        // Sadece tam ortadaki satır veya sütun 1 olur
                        txt.Text = (i == size / 2 || j == size / 2) ? "1" : "0";
                    }
                    else if (shape == "Daire (Circle)")
                    {
                        // Merkeze olan uzaklığı hesaplayıp basit bir çember çıkarıyoruz
                        double center = size / 2.0;
                        double dist = Math.Sqrt(Math.Pow(i - (int)center, 2) + Math.Pow(j - (int)center, 2));
                        txt.Text = dist <= center ? "1" : "0";
                    }
                    else
                    {
                        txt.Text = "0"; // Custom seçilirse hepsi 0 gelsin, kullanıcı kendi 1 yapsın
                    }

                    // Eğer değer 0 ise arka planı hafif gri yap ki şekil gözle net anlaşılsın
                    if (txt.Text == "0") txt.BackColor = Color.LightGray;
                    else txt.BackColor = Color.White;

                    morphMatrixTextBoxes[i, j] = txt;
                    gbMorphMatrix.Controls.Add(txt);
                }
            }
        }

        // Seçim değiştiğinde matrisi güncelleyecek olay
        private void UpdateEdgeMatrix_Event(object sender, EventArgs e)
        {
            GenerateEdgeMatrix();
        }

        // Matrisi seçilen yöne göre dolduran beyin
        private void GenerateEdgeMatrix()
        {
            if (cmbEdgeType.SelectedIndex == -1) return;

            if (cmbEdgeType.SelectedIndex == 0) // Yatay (Horizontal)
            {
                // Prewitt Yatay Kernel
                edgeMatrixLabels[0, 0].Text = "-1"; edgeMatrixLabels[0, 1].Text = "0"; edgeMatrixLabels[0, 2].Text = "1";
                edgeMatrixLabels[1, 0].Text = "-1"; edgeMatrixLabels[1, 1].Text = "0"; edgeMatrixLabels[1, 2].Text = "1";
                edgeMatrixLabels[2, 0].Text = "-1"; edgeMatrixLabels[2, 1].Text = "0"; edgeMatrixLabels[2, 2].Text = "1";
            }
            else if (cmbEdgeType.SelectedIndex == 1) // Dikey (Vertical)
            {
                // Prewitt Dikey Kernel
                edgeMatrixLabels[0, 0].Text = "1"; edgeMatrixLabels[0, 1].Text = "1"; edgeMatrixLabels[0, 2].Text = "1";
                edgeMatrixLabels[1, 0].Text = "0"; edgeMatrixLabels[1, 1].Text = "0"; edgeMatrixLabels[1, 2].Text = "0";
                edgeMatrixLabels[2, 0].Text = "-1"; edgeMatrixLabels[2, 1].Text = "-1"; edgeMatrixLabels[2, 2].Text = "-1";
            }
            else if (cmbEdgeType.SelectedIndex == 2) // Her İkisi (Magnitude)
            {
                // İki matrisin birleştirileceğini sembolik olarak gösterelim
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        edgeMatrixLabels[i, j].Text = ""; // Önce temizle
                    }
                }

                // Tam ortaya formülü yaz
                edgeMatrixLabels[1, 1].Text = "Gx + Gy\n(Genlik)";
            }

            // Renklendirme mantığı (Opsiyonel, şekli netleştirmek için): 0'ları gri, diğerlerini beyaz yap
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (edgeMatrixLabels[i, j].Text == "0") edgeMatrixLabels[i, j].BackColor = Color.LightGray;
                    else if (string.IsNullOrEmpty(edgeMatrixLabels[i, j].Text)) edgeMatrixLabels[i, j].BackColor = Color.White; // Genlik durumu
                    else edgeMatrixLabels[i, j].BackColor = Color.White;
                }
            }
        }

        private void CreateEdgeMatrixGrid()
        {
            edgeMatrixLabels = new Label[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    edgeMatrixLabels[i, j] = new Label();
                    edgeMatrixLabels[i, j].Text = "0";
                    edgeMatrixLabels[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    edgeMatrixLabels[i, j].Dock = DockStyle.Fill;
                    edgeMatrixLabels[i, j].BorderStyle = BorderStyle.FixedSingle;
                    edgeMatrixLabels[i, j].Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    tlpEdgeMatrix.Controls.Add(edgeMatrixLabels[i, j], j, i);
                }
            }
        }

        private void cmbHistogram_SelectedIndexChanged(object sender, EventArgs e)
        {
            // İleride histogram seçimi değiştiğinde anlık bir şey yapmak istersen burayı kullanabilirsin.
        }

        // Trackbar kaydırıldığında sayıyı değiştirir
        private void TrkTreshold2_ValueChanged(object sender, EventArgs e)
        {
            if (numThreshold.Value != trkTreshold2.Value)
            {
                numThreshold.Value = trkTreshold2.Value;
            }
        }

        // Sayı kutusuna rakam yazıldığında Trackbar'ı o hizaya kaydırır
        private void NumThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (trkTreshold2.Value != (int)numThreshold.Value)
            {
                trkTreshold2.Value = (int)numThreshold.Value;
            }
        }


    }
}