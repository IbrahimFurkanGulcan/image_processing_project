using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessingProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Arayüzü Form1.Designer.cs'den yükler
        }

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
                        picInput2.Visible = true; // Sadece bu işlemde 2. resmi göster!
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
                        break;
                    case "Gürültü Ekleme (Salt&Pepper)/Temizleme (mean, median)":
                        pnlNoise.Visible = true;
                        // ComboBox'lar boş gelmesin, varsayılan olarak ilk seçenekler (İşlem Yok) seçili olsun
                        if (cmbNoiseAdd.SelectedIndex == -1) cmbNoiseAdd.SelectedIndex = 0;
                        if (cmbNoiseRemove.SelectedIndex == -1) cmbNoiseRemove.SelectedIndex = 0;
                        break;
                    case "Konvolüsyon İşlemi (mean)":
                        pnlMatrixFilter.Visible = true;
                        if (cmbMatrixSize.SelectedIndex == -1) cmbMatrixSize.SelectedIndex = 0;
                        break;
                    case "Morfolojik İşlemler (Genişleme, Aşınma, Açma, Kapama)":
                        pnlMorphology.Visible = true;
                        if (cmbMorphologyType.SelectedIndex == -1) cmbMorphologyType.SelectedIndex = 0;
                        if (cmbMorphMatrixSize.SelectedIndex == -1) cmbMorphMatrixSize.SelectedIndex = 0;
                        break;
                }
            }
        }

        // --- 3. EKRAN TEMİZLEME YARDIMCISI ---
        private void PanelleriGizle()
        {
            picInput2.Visible = false; // 2. resmi varsayılan olarak gizle

            pnlBinary.Visible = false;
            pnlRotate.Visible = false;
            pnlCrop.Visible = false;
            pnlScale.Visible = false;
            pnlArithmetic.Visible = false;
            pnlHistogram.Visible = false;
            pnlContrast.Visible = false;
            pnlThresholding.Visible = false;
            pnlNoise.Visible = false;
            pnlMorphology.Visible = false;
            pnlMatrixFilter.Visible = false;
        }

        // --- 4. İŞLEMİ UYGULA BUTONU ---
        private void btnUygula_Click(object sender, EventArgs e)
        {
            if (picInput1.Image == null)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("Lütfen sol menüden bir işlem seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

                    case "Morfolojik İşlemler (Genişleme, Aşınma, Açma, Kapama)":
                        string islemTuru = cmbMorphologyType.SelectedItem.ToString();
                        string morphMatris = cmbMorphMatrixSize.SelectedItem.ToString();
                        MessageBox.Show($"Morfolojik işlem uygulanıyor...\nTür: {islemTuru}\nMatris: {morphMatris}");
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

        private void cmbHistogram_SelectedIndexChanged(object sender, EventArgs e)
        {
            // İleride histogram seçimi değiştiğinde anlık bir şey yapmak istersen burayı kullanabilirsin.
        }
    }
}