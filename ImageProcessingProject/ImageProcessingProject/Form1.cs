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

            trkNoisePercentage.ValueChanged += TrkNoisePercentage_ValueChanged;
            numNoisePercentage.ValueChanged += NumNoisePercentage_ValueChanged;

            
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

        /// <summary>Designer/eski exe senkronu bozulmasın diye liste her açılışta kodla doldurulur.</summary>
        private void RenkUzayiComboDoldur()
        {
            string onceki = cmbColorSpace.SelectedItem?.ToString();
            cmbColorSpace.Items.Clear();
            cmbColorSpace.Items.AddRange(RenkUzayiSecenekleri);
            int idx = Array.IndexOf(RenkUzayiSecenekleri, onceki);
            cmbColorSpace.SelectedIndex = idx >= 0 ? idx : 0;
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
                picOutput.Dock = DockStyle.Fill;
                picOutput.SizeMode = PictureBoxSizeMode.Zoom;

                switch (treeView1.SelectedNode.Text)
                {
                    case "Gri Dönüşüm":
                        // ImageProcessor'daki ConvertToGrayscale metodunu tetikliyoruz
                        Bitmap kaynakGri = new Bitmap(picInput1.Image);
                        picOutput.Image = ImageProcessor.ConvertToGrayscale(kaynakGri);
                        break;

                    
                    case "Binary Dönüşüm":
                        // Arayüzdeki TrackBar (trkThreshold) üzerinden statik değeri alıyoruz
                        Bitmap kaynakBinary = new Bitmap(picInput1.Image);
                        int binaryEsik = trkThreshold.Value;
                        picOutput.Image = ImageProcessor.ConvertToBinary(kaynakBinary, binaryEsik);
                        break;

                    case "Görüntü Döndürme":

                        picOutput.Dock = DockStyle.None;
                        picOutput.SizeMode = PictureBoxSizeMode.AutoSize;

                        int aciDegeri = (int)numAngle.Value;
                        string yontem = cmbRotateInterpolation.Text;

                        Bitmap dondurulmusResim = ImageProcessor.GoruntuDondur((Bitmap)picInput1.Image, aciDegeri, yontem);

                        picOutput.Image = dondurulmusResim;


                        break;


                    case "Kontrast Artırma":
                        // 1. Asıl kontrast çubuğunun değerini alıyoruz
                        int secilenKontrast = trkContrast.Value;

                        // 2. Orijinal resmi bozmadan kopyalıyoruz
                        Bitmap kaynakKontrast = new Bitmap(picInput1.Image);

                        // 3. Değeri motora gönderip sonucu sağdaki kutuya basıyoruz
                        picOutput.Image = ImageProcessor.KontrastUygula(kaynakKontrast, secilenKontrast);

                        break;


                    case "Görüntü Yaklaştırma/Uzaklaştırma":

                        picOutput.Dock = DockStyle.None;
                        picOutput.SizeMode = PictureBoxSizeMode.AutoSize;

                        double secilenOran = Convert.ToDouble(numScale.Value);

                        // Arayüzdeki ComboBox'tan yöntemi okuyoruz (Eğer boşsa varsayılan Bilinear)
                        string intYontem = cmbScaleInterpolation.SelectedItem != null
                                        ? cmbScaleInterpolation.SelectedItem.ToString()
                                        : "Bilinear";

                        if (secilenOran > 0)
                        {
                            Bitmap kaynakOlcek = new Bitmap(picInput1.Image);

                            
                            Bitmap sonucOlcek = ImageProcessor.GoruntuOlcekle(kaynakOlcek, secilenOran, intYontem);

                            picOutput.Image = sonucOlcek;

                            // İLLÜZYONU KIRAN MESAJ: Resmin arka planda gerçekten büyüdüğünü/küçüldüğünü kanıtlar!
                            MessageBox.Show($"Orijinal Boyut: {kaynakOlcek.Width} x {kaynakOlcek.Height} piksel\n" +
                                            $"Yeni Boyut: {sonucOlcek.Width} x {sonucOlcek.Height} piksel\n" +
                                            $"Kullanılan Yöntem: {intYontem}",
                                            "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Lütfen 0'dan büyük bir ölçekleme oranı giriniz.", "Hatalı Veri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;

                    case "Görüntü Kırpma":
                        {
                            picOutput.Dock = DockStyle.None;
                            picOutput.SizeMode = PictureBoxSizeMode.AutoSize;

                            // 1. Arayüzden değerleri okuyoruz.
                            int baslangicX = (int)numCropX.Value;
                            int baslangicY = (int)numCropY.Value;
                            int genislik = (int)numCropHeight.Value;
                            int yukseklik = (int)numCropWidth.Value;

                            // 2. Güvenlik Kontrolü: Sıfır veya eksi değerle resim kırpılamaz.
                            if (genislik <= 0 || yukseklik <= 0)
                            {
                                MessageBox.Show("Lütfen kırpılacak alanın Genişlik ve Yükseklik değerlerini 0'dan büyük giriniz.", "Hatalı Parametre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                            // 3. Arka planın anladığı dile çeviri (Matematiksel Dönüşüm)
                            // x2 noktası = Başlangıç X noktası + Kırpılacak Genişlik
                            int x1 = baslangicX;
                            int y1 = baslangicY;
                            int x2 = baslangicX + genislik;
                            int y2 = baslangicY + yukseklik;

                            // 4. Orijinal resmi koruyarak kopyasını al ve kırpma metoduna gönder
                            Bitmap kaynakKirpma = new Bitmap(picInput1.Image);
                            picOutput.Image = ImageProcessor.CropImage(kaynakKirpma, x1, y1, x2, y2);

                            break;
                        }


                    case "Histogram İşlemleri (germe/genişletme)":
                        {
                            Bitmap srcH = new Bitmap(picInput1.Image);

                            // Once giris histogramini her durumda cizdirelim
                            int[] histOrj = PikselIslem.HistogramHesapla(srcH);
                            picHistogram.Image = PikselIslem.HistogramCiz(histOrj);

                            string secimHist = cmbHistogram.SelectedItem != null
                                ? cmbHistogram.SelectedItem.ToString()
                                : "Histogram Germe";

                            Bitmap cikisHist;
                            if (secimHist == "Histogram Genişletme")
                                cikisHist = PikselIslem.HistogramGenisletme(srcH);
                            else
                                cikisHist = PikselIslem.HistogramGerme(srcH);

                            picOutput.Image = cikisHist;

                            // Cikis histogramini da yan tuvale cizdirelim
                            int[] histYeni = PikselIslem.HistogramHesapla(new Bitmap(cikisHist));
                            picHistogramResult.Image = PikselIslem.HistogramCiz(histYeni);
                            break;
                        }

                    case "İki Resim Arasında Aritmetik İşlemler (ekleme, bölme)":

                        // 1. İkinci resim yüklü mü kontrolü
                        if (picInput2.Image == null)
                        {
                            MessageBox.Show("Bu işlem için 'İkinci Resmi Yükle' menüsünden 2. bir resim seçmelisiniz!", "Eksik Veri", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 2. ComboBox'tan işlem seçilmiş mi kontrolü
                        if (cmbAritmetik.SelectedItem == null)
                        {
                            MessageBox.Show("Lütfen yapılacak aritmetik işlemi seçiniz (Toplam, Çıkarma vb.).", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Bitmap resim1 = new Bitmap(picInput1.Image);
                        Bitmap resim2 = new Bitmap(picInput2.Image);
                        Bitmap sonucAritmetik = null;

                        string islem = cmbAritmetik.SelectedItem.ToString();

                        // Seçilen string değere göre ilgili statik metodu tetikliyoruz
                        if (islem == "Toplam")
                        {
                            sonucAritmetik = GoruntuIslem.ResimTopla(resim1, resim2);
                        }
                        else if (islem == "Çıkarma")
                        {
                            sonucAritmetik = GoruntuIslem.ResimCikar(resim1, resim2);
                        }
                        else if (islem == "Çarpma")
                        {
                            sonucAritmetik = GoruntuIslem.ResimCarp(resim1, resim2);
                        }
                        else if (islem == "Bölme")
                        {
                            sonucAritmetik = GoruntuIslem.ResimBol(resim1, resim2);
                        }

                        // Eğer metodlar null döndürmediyse (yani boyutlar eşleştiyse) resmi ekrana bas
                        if (sonucAritmetik != null)
                        {
                            picOutput.Image = sonucAritmetik;
                        }
                        else
                        {
                            MessageBox.Show("Hata: İşlem yapılacak resimlerin boyutları (Genişlik ve Yükseklik) birebir aynı olmalıdır!", "Boyut Uyuşmazlığı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;


                    case "Morfolojik İşlemler (Genişleme, Aşınma, Açma, Kapama)":
                        Bitmap srcMorph = new(picInput1.Image);
                        string islemTuru = cmbMorphologyType.SelectedItem.ToString();

                        // 1. Arayüzden değerleri dinamik olarak çekiyoruz
                        int mBoyut = int.Parse(cmbMorphMatrixSize.SelectedItem.ToString().Substring(0, 1));
                        string mSekil = cmbMorphShape.SelectedItem.ToString();

                        // 2. İşlemleri çağırırken bu 3 bilgiyi (resim, boyut, şekil) gönderiyoruz
                        if (islemTuru == "Genişleme (Dilation)")
                        {
                            picOutput.Image = GoruntuIslem.Genisleme(srcMorph, mBoyut, mSekil);
                        }
                        else if (islemTuru == "Aşınma (Erosion)")
                        {
                            picOutput.Image = GoruntuIslem.Asinma(srcMorph, mBoyut, mSekil);
                        }
                        else if (islemTuru == "Açma (Opening)")
                        {
                            picOutput.Image = GoruntuIslem.Acma(srcMorph, mBoyut, mSekil);
                        }
                        else if (islemTuru == "Kapama (Closing)")
                        {
                            picOutput.Image = GoruntuIslem.Kapama(srcMorph, mBoyut, mSekil);
                        }
                        break;

                    case "Görüntüye Filtre Uygulanması (Unsharp)":
                        if (picInput1.Image != null)
                        {
                            // 1. Resmi al
                            Bitmap girisResmi = new Bitmap(picInput1.Image);

                            // 2. Filtreyi uygula (1.5 keskinlik seviyesi - istersen değiştirebilirsin)
                            Bitmap sonucResmi = GoruntuIslem.UnsharpMask(girisResmi, 1.5);

                            // 3. Sonucu ekrana yansıt
                            picOutput.Image = sonucResmi;
                        }
                        else
                        {
                            MessageBox.Show("Lütfen işlemi uygulamak için bir resim yükleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;


                    case "Kenar Bulma Algoritmalarının Kullanımı (prewitt)":
                        Bitmap srcPrewitt = new Bitmap(picInput1.Image);
                        string secilenYon = cmbEdgeType.SelectedItem.ToString();
                        
                        picOutput.Image = ImageProcessor.ApplyPrewitt(srcPrewitt, secilenYon);
                        break;

                    case "Konvolüsyon İşlemi (mean)":
                        {
                            if (cmbMatrixSize.SelectedItem == null)
                            {
                                MessageBox.Show("Lütfen matris boyutunu seçiniz (3x3, 5x5, 7x7).", "Bilgi");
                                return;
                            }

                            Bitmap srcK = new Bitmap(picInput1.Image);
                            int boyutK = int.Parse(cmbMatrixSize.SelectedItem.ToString().Split('x')[0]);
                            picOutput.Image = PikselIslem.MeanKonvolusyon(srcK, boyutK);
                            break;
                        }

                    case "Renk Uzayı Dönüşümleri":
                        {
                            if (cmbColorSpace.SelectedItem == null)
                            {
                                MessageBox.Show("Lütfen bir renk uzayı dönüşümü seçiniz.", "Bilgi");
                                return;
                            }

                            Bitmap srcRu = new Bitmap(picInput1.Image);
                            string hedefUzay = cmbColorSpace.SelectedItem.ToString();

                            if (hedefUzay == "RGB -> HSV")
                                picOutput.Image = PikselIslem.RgbToHsv(srcRu);
                            else if (hedefUzay == "HSV -> RGB")
                                picOutput.Image = PikselIslem.HsvToRgb(srcRu);
                            else if (hedefUzay == "RGB -> YCbCr")
                                picOutput.Image = PikselIslem.RgbToYCbCr(srcRu);
                            else if (hedefUzay == "YCbCr -> RGB")
                                picOutput.Image = PikselIslem.YCbCrToRgb(srcRu);
                            else if (hedefUzay == "RGB -> CMYK")
                                picOutput.Image = PikselIslem.RgbToCmyk(srcRu);
                            else if (hedefUzay == "RGB -> Gri (Luminance)")
                                picOutput.Image = PikselIslem.RgbToGri(srcRu);
                            else
                                MessageBox.Show("Bu dönüşüm henüz desteklenmiyor: " + hedefUzay, "Bilgi");
                            break;
                        }

                    case "Gürültü Ekleme (Salt&Pepper)/Temizleme (mean, median)":
                        Bitmap kaynakResim = new Bitmap(picInput1.Image);

                        if (rbNoiseAdd.Checked) // Ekleme
                        {
                            if (cmbNoiseAdd.SelectedItem != null)
                            {
                                string gurultuTuru = cmbNoiseAdd.SelectedItem.ToString();
                                
                                int oran = trkNoisePercentage.Value; 
                                
                                picOutput.Image = ImageProcessor.AddNoise(kaynakResim, oran, gurultuTuru);
                            }
                        }
                        else // Temizleme
                        {
                            if (cmbNoiseRemove.SelectedItem != null && cmbNoiseMatrixSize.SelectedItem != null)
                            {
                                string filtreTuru = cmbNoiseRemove.SelectedItem.ToString();

                                // Matris box ındaki stringten değeri çekiyoruz parse ederek: 5x5->5
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
                            // Dinamik (Adaptif) eşikleme - Gürültü temizlemedeki gibi: 5x5->5
                            string matrisStr = cmbThresholdMatrix.SelectedItem.ToString(); 
                            int matrisBoyutu = int.Parse(matrisStr.Split('x')[0]); 

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
                trkNoisePercentage.Enabled = true; // Oran çubuğu aktif
                numNoisePercentage.Enabled = true; // Sayı kutusu aktif

                cmbNoiseRemove.Enabled = false;
                cmbNoiseMatrixSize.Enabled = false; // Eklemede matris kutusu kapalı
                cmbNoiseMatrixSize.Enabled = false;
            }
            else
            {
                cmbNoiseAdd.Enabled = false;
                trkNoisePercentage.Enabled = false; // Oran çubuğu pasif
                numNoisePercentage.Enabled = false; // Sayı kutusu pasif

                cmbNoiseRemove.Enabled = true;
                cmbNoiseMatrixSize.Enabled = true; // Temizlemede matris kutusu açık
                cmbNoiseMatrixSize.Enabled = true;
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
        // 1. TrackBar (Kaydırma Çubuğu) değiştiğinde
        private void TrkTreshold2_ValueChanged(object sender, EventArgs e)
        {
            // ÖNEMLİ: Eğer sayı kutusundaki değer zaten çubukla aynıysa işlem yapma!
            if (numThreshold.Value != (decimal)trkTreshold2.Value)
            {
                numThreshold.Value = (decimal)trkTreshold2.Value;
            }
        }

               

        // Sayı kutusuna rakam yazıldığında Trackbar'ı o hizaya kaydırır
        // 2. NumericUpDown (Sayı Kutusu) değiştiğinde
        private void NumThreshold_ValueChanged(object sender, EventArgs e)
        {
            // ÖNEMLİ: Eğer çubuktaki değer zaten sayı kutusuyla aynıysa işlem yapma!
            if (trkTreshold2.Value != (int)numThreshold.Value)
            {
                trkTreshold2.Value = (int)numThreshold.Value;
            }
        }

        private void TrkNoisePercentage_ValueChanged(object sender, EventArgs e)
        {
            if (numNoisePercentage.Value != trkNoisePercentage.Value)
                numNoisePercentage.Value = trkNoisePercentage.Value;
        }

        private void NumNoisePercentage_ValueChanged(object sender, EventArgs e)
        {
            if (trkNoisePercentage.Value != (int)numNoisePercentage.Value)
                trkNoisePercentage.Value = (int)numNoisePercentage.Value;
        }


    }
}