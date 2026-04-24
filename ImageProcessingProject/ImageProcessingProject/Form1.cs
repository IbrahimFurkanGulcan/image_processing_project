#nullable disable
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageProcessingProject
{
    public partial class Form1 : Form
    {
        static readonly Color BG = Color.FromArgb(14, 14, 17);
        static readonly Color SURFACE = Color.FromArgb(22, 22, 26);
        static readonly Color SURFACE2 = Color.FromArgb(30, 30, 36);
        static readonly Color SURFACE3 = Color.FromArgb(38, 38, 46);
        static readonly Color BORDER = Color.FromArgb(46, 46, 56);
        static readonly Color ACCENT = Color.FromArgb(124, 110, 247);
        static readonly Color ACCENT2 = Color.FromArgb(165, 148, 255);
        static readonly Color TEXT1 = Color.FromArgb(240, 240, 245);
        static readonly Color TEXT2 = Color.FromArgb(144, 144, 168);
        static readonly Color TEXT3 = Color.FromArgb(90, 90, 114);
        static readonly Color GREEN = Color.FromArgb(62, 207, 142);
        static readonly Color AMBER = Color.FromArgb(245, 166, 35);

        Panel topBar, sidebar, mainArea, canvasArea, bottomPanel, paramArea, navPanel;
        PictureBox picInput, picOutput;
        Bitmap imgInput = null, imgOutput = null;
        Button btnLoad1, btnLoad2, btnSave, btnApply, btnReset;
        Label lblOpTitle, lblOpCat, lblOutStatus;
        TextBox txtSearch;
        string currentOp = "gri";
        Bitmap secondImage = null;

        Panel pGri, pBinary, pKirp, pDondur, pZoom;
        Panel pAritm, pHist, pKontrast, pEsik, pNoise;
        Panel pMean, pMorfo, pUnsharp, pKenar, pRenk;
        Panel pEsikStatic, pEsikDynamic, pNoiseAdd, pNoiseRemove;

        TrackBar trkBinary, trkDondur, trkKontrast, trkEsik, trkNoise, trkZoom;
        Label lblBinaryVal, lblDondurVal, lblKontrastVal, lblEsikVal, lblNoiseVal, lblZoomVal;
        NumericUpDown numCropX, numCropY, numCropW, numCropH;
        ComboBox cmbDondurInterp, cmbZoomInterp, cmbAritm, cmbHist;
        ComboBox cmbEsikMatrix, cmbNoiseType, cmbNoiseRemove, cmbNoiseMatrix;
        ComboBox cmbMean, cmbMorfoType, cmbMorfoMatrix, cmbMorfoShape;
        ComboBox cmbUnsharp, cmbKenar, cmbRenk;
        RadioButton rbEsikStatic, rbEsikDynamic, rbNoiseAdd, rbNoiseRemove;

        public Form1()
        {
            InitializeComponent();
            this.Text = "VisuaLab — Görüntü İşleme Platformu";
            this.ClientSize = new Size(1400, 820);
            this.MinimumSize = new Size(1100, 700);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = BG;
            this.ForeColor = TEXT1;
            this.Font = new Font("Segoe UI", 9f);
            this.StartPosition = FormStartPosition.CenterScreen;
            BuildTopBar();
            BuildMainArea();
            BuildSidebar();
        }

        void BuildTopBar()
        {
            topBar = NewPanel(DockStyle.Top, 50, SURFACE);
            topBar.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 0, 49, topBar.Width, 49);

            var logo = new Panel { Size = new Size(28, 28), BackColor = ACCENT, Location = new Point(16, 11) };
            RoundCorners(logo, 8);
            logo.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var p = new Pen(Color.White, 1.5f);
                g.DrawRectangle(p, 3, 3, 7, 7);
                g.DrawRectangle(p, 15, 3, 7, 7);
                g.DrawRectangle(p, 3, 15, 7, 7);
                g.FillEllipse(Brushes.White, 16, 16, 6, 6);
            };

            var lblName = new Label { Text = "VisuaLab", Font = new Font("Segoe UI", 12f, FontStyle.Bold), ForeColor = TEXT1, AutoSize = true, Location = new Point(52, 14) };

            btnLoad1 = MakeTopBtn("Resim Yükle");
            btnLoad2 = MakeTopBtn("2. Resim Yükle");
            btnSave = MakeTopBtn("Çıktıyı Kaydet");
            btnSave.BackColor = ACCENT;
            btnSave.ForeColor = Color.White;
            btnSave.FlatAppearance.BorderColor = ACCENT;
            btnSave.FlatAppearance.MouseOverBackColor = ACCENT2;

            btnLoad1.Click += BtnLoad1_Click;
            btnLoad2.Click += BtnLoad2_Click;
            btnSave.Click += BtnSave_Click;

            topBar.SizeChanged += (s, e) =>
            {
                btnSave.Location = new Point(topBar.Width - 16 - 130, 11);
                btnLoad2.Location = new Point(topBar.Width - 16 - 270, 11);
                btnLoad1.Location = new Point(topBar.Width - 16 - 410, 11);
            };

            topBar.Controls.AddRange(new Control[] { logo, lblName, btnLoad1, btnLoad2, btnSave });
            this.Controls.Add(topBar);
        }

        Button MakeTopBtn(string text)
        {
            var b = new Button { Text = text, Size = new Size(130, 28), FlatStyle = FlatStyle.Flat, BackColor = SURFACE2, ForeColor = TEXT2, Font = new Font("Segoe UI", 8.5f), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderColor = BORDER;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.MouseOverBackColor = SURFACE3;
            RoundCorners(b, 7);
            return b;
        }

        void BuildSidebar()
        {
            sidebar = NewPanel(DockStyle.Left, 0, SURFACE);
            sidebar.Width = 215;
            sidebar.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 214, 0, 214, sidebar.Height);

            var searchWrap = NewPanel(DockStyle.Top, 48, SURFACE);
            searchWrap.Padding = new Padding(10, 9, 10, 9);
            searchWrap.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 0, 47, searchWrap.Width, 47);

            var searchBg = NewPanel(DockStyle.Fill, 0, SURFACE2);
            searchBg.Padding = new Padding(10, 0, 8, 0);
            RoundCorners(searchBg, 8);

            txtSearch = new TextBox { Dock = DockStyle.Fill, BackColor = SURFACE2, ForeColor = TEXT3, BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9f), Text = "İşlem ara..." };
            txtSearch.GotFocus += (s, e) => { if (txtSearch.Text == "İşlem ara...") { txtSearch.Text = ""; txtSearch.ForeColor = TEXT1; } };
            txtSearch.LostFocus += (s, e) => { if (txtSearch.Text == "") { txtSearch.Text = "İşlem ara..."; txtSearch.ForeColor = TEXT3; } };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            searchBg.Controls.Add(txtSearch);
            searchWrap.Controls.Add(searchBg);

            navPanel = NewPanel(DockStyle.Fill, 0, SURFACE);
            var navScroll = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, BackColor = SURFACE, Padding = new Padding(0, 6, 0, 16) };

            AddCat(navScroll, "TEMEL");
            AddNav(navScroll, "gri", "Gri Dönüşüm", true);
            AddNav(navScroll, "binary", "Binary Dönüşüm");
            AddCat(navScroll, "GEOMETRİK");
            AddNav(navScroll, "kirp", "Görüntü Kırpma");
            AddNav(navScroll, "dondur", "Görüntü Döndürme");
            AddNav(navScroll, "zoom", "Yaklaştırma / Uzaklaştırma");
            AddCat(navScroll, "GELİŞTİRME");
            AddNav(navScroll, "aritm", "Aritmetik İşlemler");
            AddNav(navScroll, "hist", "Histogram İşlemleri");
            AddNav(navScroll, "kontrast", "Kontrast Artırma");
            AddCat(navScroll, "FİLTRELER");
            AddNav(navScroll, "esik", "Eşikleme");
            AddNav(navScroll, "noise", "Gürültü Ekle / Temizle");
            AddNav(navScroll, "mean", "Konvolüsyon (Mean)");
            AddNav(navScroll, "morfo", "Morfolojik İşlemler");
            AddNav(navScroll, "unsharp", "Unsharp Filtre");
            AddNav(navScroll, "kenar", "Kenar Bulma (Prewitt)");
            AddCat(navScroll, "DİĞER");
            AddNav(navScroll, "renk", "Renk Uzayı Dönüşümleri");

            navPanel.Controls.Add(navScroll);
            sidebar.Controls.Add(navPanel);
            sidebar.Controls.Add(searchWrap);
            this.Controls.Add(sidebar);
        }

        void AddCat(FlowLayoutPanel parent, string text)
        {
            parent.Controls.Add(new Label { Text = text, ForeColor = TEXT3, Font = new Font("Segoe UI", 7.5f, FontStyle.Bold), AutoSize = false, Width = 210, Height = 28, Padding = new Padding(14, 12, 0, 0) });
        }

        void AddNav(FlowLayoutPanel parent, string key, string text, bool active = false)
        {
            var item = new Panel { Width = 203, Height = 32, Cursor = Cursors.Hand, Tag = key, BackColor = active ? Color.FromArgb(35, 124, 110, 247) : Color.Transparent, Margin = new Padding(6, 1, 6, 1) };
            RoundCorners(item, 8);
            var dot = new Panel { Size = new Size(5, 5), Location = new Point(12, 13), BackColor = active ? ACCENT : BORDER };
            RoundCorners(dot, 3);
            var lbl = new Label { Text = text, AutoSize = false, Location = new Point(25, 0), Size = new Size(172, 32), TextAlign = ContentAlignment.MiddleLeft, ForeColor = active ? ACCENT2 : TEXT2, Font = new Font("Segoe UI", 9f, active ? FontStyle.Bold : FontStyle.Regular) };

            Action activate = () => { item.BackColor = Color.FromArgb(35, 124, 110, 247); dot.BackColor = ACCENT; lbl.ForeColor = ACCENT2; lbl.Font = new Font("Segoe UI", 9f, FontStyle.Bold); };

            EventHandler click = (s, e) =>
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is Panel np && np.Tag is string)
                    {
                        np.BackColor = Color.Transparent;
                        foreach (Control ch in np.Controls)
                        {
                            if (ch is Panel d) d.BackColor = BORDER;
                            if (ch is Label l) { l.ForeColor = TEXT2; l.Font = new Font("Segoe UI", 9f, FontStyle.Regular); }
                        }
                    }
                }
                activate();
                NavSelect(key, text);
            };

            item.MouseEnter += (s, e) => { if (currentOp != key) item.BackColor = SURFACE2; };
            item.MouseLeave += (s, e) => { if (currentOp != key) item.BackColor = Color.Transparent; };
            item.Click += click; lbl.Click += click; dot.Click += click;
            lbl.MouseEnter += (s, e) => { if (currentOp != key) item.BackColor = SURFACE2; };
            lbl.MouseLeave += (s, e) => { if (currentOp != key) item.BackColor = Color.Transparent; };
            item.Controls.Add(dot);
            item.Controls.Add(lbl);
            parent.Controls.Add(item);
        }

        void NavSelect(string key, string text)
        {
            currentOp = key;
            lblOpTitle.Text = text;
            lblOpCat.Text = key switch
            {
                "gri" or "binary" => "TEMEL",
                "kirp" or "dondur" or "zoom" => "GEOMETRİK",
                "aritm" or "hist" or "kontrast" => "GELİŞTİRME",
                "esik" or "noise" or "mean" or "morfo" or "unsharp" or "kenar" => "FİLTRELER",
                _ => "DİĞER"
            };
            ShowParam(key);
            ResetApplyBtn();
            lblOutStatus.Text = "— bekleniyor";
        }

        void BuildMainArea()
        {
            mainArea = new Panel { Dock = DockStyle.Fill, BackColor = BG };

            bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 280, BackColor = SURFACE };
            bottomPanel.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 0, 0, bottomPanel.Width, 0);

            var opHead = new Panel { Dock = DockStyle.Top, Height = 44, BackColor = SURFACE };
            opHead.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 0, 43, opHead.Width, 43);

            var titleFlow = new FlowLayoutPanel { BackColor = Color.Transparent, Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, Padding = new Padding(18, 0, 18, 0) };
            lblOpTitle = new Label { Text = "Gri Dönüşüm", ForeColor = TEXT1, Font = new Font("Segoe UI", 12f, FontStyle.Bold), AutoSize = true, Margin = new Padding(0, 10, 10, 0) };
            lblOpCat = new Label { Text = "TEMEL", ForeColor = ACCENT2, Font = new Font("Segoe UI", 8f, FontStyle.Bold), AutoSize = true, BackColor = Color.FromArgb(40, 124, 110, 247), Padding = new Padding(8, 3, 8, 3), Margin = new Padding(0, 12, 0, 0) };
            RoundCorners(lblOpCat, 10);
            titleFlow.Controls.Add(lblOpTitle);
            titleFlow.Controls.Add(lblOpCat);
            opHead.Controls.Add(titleFlow);

            var footer = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = SURFACE };
            footer.Padding = new Padding(18, 9, 18, 9);
            footer.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 0, 0, footer.Width, 0);

            btnApply = new Button { Text = "İşlemi Uygula", Dock = DockStyle.Fill, BackColor = ACCENT, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10f, FontStyle.Bold), Cursor = Cursors.Hand };
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatAppearance.MouseOverBackColor = ACCENT2;
            btnApply.Click += BtnApply_Click;

            btnReset = new Button { Text = "Sıfırla", Dock = DockStyle.Right, Width = 90, BackColor = Color.Transparent, ForeColor = TEXT2, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f), Cursor = Cursors.Hand };
            btnReset.FlatAppearance.BorderColor = BORDER;
            btnReset.FlatAppearance.BorderSize = 1;
            btnReset.Click += (s, e) => { imgOutput = null; picOutput.Image = null; lblOutStatus.Text = "— bekleniyor"; ResetApplyBtn(); };
            footer.Controls.Add(btnApply);
            footer.Controls.Add(btnReset);

            paramArea = new Panel { Dock = DockStyle.Fill, BackColor = SURFACE, Padding = new Padding(18, 10, 18, 6) };
            BuildAllParams();
            paramArea.Controls.Add(pGri);

            bottomPanel.Controls.Add(paramArea);
            bottomPanel.Controls.Add(footer);
            bottomPanel.Controls.Add(opHead);

            canvasArea = new Panel { Dock = DockStyle.Fill, BackColor = BG, Padding = new Padding(8) };
            var leftPane = BuildImagePane("GİRİŞ", false);
            var rightPane = BuildImagePane("ÇIKTI", true);

            EventHandler layout = (s, e) =>
            {
                int pad = 8;
                int w = (canvasArea.Width - pad * 3) / 2;
                int h = canvasArea.Height - pad * 2;
                if (w > 10 && h > 10)
                {
                    leftPane.SetBounds(pad, pad, w, h);
                    rightPane.SetBounds(pad * 2 + w, pad, w, h);
                }
            };
            canvasArea.Resize += layout;
            this.Shown += layout;
            canvasArea.Controls.Add(leftPane);
            canvasArea.Controls.Add(rightPane);

            mainArea.Controls.Add(bottomPanel);
            mainArea.Controls.Add(canvasArea);
            this.Controls.Add(mainArea);
        }

        Panel BuildImagePane(string title, bool isOutput)
        {
            var outer = new Panel { BackColor = SURFACE2 };
            var header = new Panel { Dock = DockStyle.Top, Height = 34, BackColor = SURFACE2, Padding = new Padding(12, 0, 12, 0) };
            header.Paint += (s, e) => e.Graphics.DrawLine(new Pen(BORDER), 0, 33, header.Width, 33);
            var lblTitle = new Label { Text = title, ForeColor = isOutput ? ACCENT2 : TEXT2, Font = new Font("Segoe UI", 8f, FontStyle.Bold), Dock = DockStyle.Left, Width = 80, TextAlign = ContentAlignment.MiddleLeft };
            header.Controls.Add(lblTitle);
            if (isOutput)
            {
                lblOutStatus = new Label { Text = "— bekleniyor", ForeColor = TEXT3, Font = new Font("Segoe UI", 8f), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
                header.Controls.Add(lblOutStatus);
            }
            var pic = new PictureBox { Dock = DockStyle.Fill, BackColor = BG, SizeMode = PictureBoxSizeMode.Zoom };
            if (isOutput) picOutput = pic;
            else picInput = pic;
            outer.Controls.Add(pic);
            outer.Controls.Add(header);
            return outer;
        }

        // Sabit yükseklikli satır — FlowLayoutPanel TopDown içinde güvenli çalışır
        Panel MakeParamRow()
        {
            return new Panel { Height = 36, Dock = DockStyle.Top, BackColor = Color.Transparent };
        }

        // Parametre paneli için dikey FlowLayoutPanel
        Panel MakeParamContainer()
        {
            return new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
        }

        void BuildAllParams()
        {
            pGri = MakeParamContainer();
            pGri.Controls.Add(new Label { Text = "Bu işlem için parametre gerekmez.", ForeColor = TEXT3, Font = new Font("Segoe UI", 9f), AutoSize = true, Location = new Point(0, 8) });

            pBinary = MakeParamContainer();
            trkBinary = AddSlider(pBinary, "Eşik değeri", 0, 255, 128, 0, out lblBinaryVal);

            pKirp = MakeParamContainer();
            var r1 = MakeParamRow(); numCropX = AddNumInRow(r1, "X", 0, 0); numCropY = AddNumInRow(r1, "Y", 0, 160);
            var r2 = MakeParamRow(); numCropW = AddNumInRow(r2, "Genişlik", 200, 0); numCropH = AddNumInRow(r2, "Yükseklik", 200, 160);
            pKirp.Controls.Add(r2);
            pKirp.Controls.Add(r1);

            pDondur = MakeParamContainer();
            trkDondur = AddSlider(pDondur, "Açı (°)", 0, 360, 90, 0, out lblDondurVal);
            cmbDondurInterp = AddComboInPanel(pDondur, "Enterpolasyon", new[] { "Nearest Neighbor", "Bilinear", "Bicubic" }, 1, 36);

            pZoom = MakeParamContainer();
            trkZoom = AddSlider(pZoom, "Oran (x10)", 1, 50, 15, 0, out lblZoomVal);
            cmbZoomInterp = AddComboInPanel(pZoom, "Enterpolasyon", new[] { "Nearest Neighbor", "Bilinear", "Bicubic" }, 1, 36);

            pAritm = MakeParamContainer();
            cmbAritm = AddComboInPanel(pAritm, "İşlem türü", new[] { "Toplam", "Çıkarma", "Çarpma", "Bölme", "Mantıksal VE", "Mantıksal VEYA" }, 0, 0);
            var wt = new Label { Text = "2 resim gerekli", ForeColor = AMBER, BackColor = Color.FromArgb(30, 245, 166, 35), Font = new Font("Segoe UI", 8f, FontStyle.Bold), AutoSize = true, Padding = new Padding(8, 3, 8, 3), Location = new Point(0, 44) };
            RoundCorners(wt, 10);
            pAritm.Controls.Add(wt);

            pHist = MakeParamContainer();
            cmbHist = AddComboInPanel(pHist, "Mod", new[] { "Histogram Tablosu", "Germe", "Eşitleme" }, 0, 0);

            pKontrast = MakeParamContainer();
            trkKontrast = AddSlider(pKontrast, "Şiddet", -100, 100, 0, 0, out lblKontrastVal);

            // Eşikleme
            pEsik = MakeParamContainer();
            var esikModRow = MakeParamRow();
            esikModRow.Controls.Add(new Label { Text = "Mod", ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = false, Width = 50, Height = 26, Location = new Point(0, 5) });
            rbEsikStatic = new RadioButton { Text = "Statik", Checked = true, ForeColor = ACCENT2, Font = new Font("Segoe UI", 9f), AutoSize = true, BackColor = Color.Transparent, Location = new Point(55, 8) };
            rbEsikDynamic = new RadioButton { Text = "Dinamik", Checked = false, ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = true, BackColor = Color.Transparent, Location = new Point(130, 8) };
            rbEsikStatic.CheckedChanged += (s, e) => rbEsikStatic.ForeColor = rbEsikStatic.Checked ? ACCENT2 : TEXT2;
            rbEsikDynamic.CheckedChanged += (s, e) => rbEsikDynamic.ForeColor = rbEsikDynamic.Checked ? ACCENT2 : TEXT2;
            esikModRow.Controls.Add(rbEsikStatic);
            esikModRow.Controls.Add(rbEsikDynamic);
            pEsik.Controls.Add(esikModRow);

            pEsikStatic = MakeParamContainer();
            trkEsik = AddSlider(pEsikStatic, "Eşik değeri", 0, 255, 128, 0, out lblEsikVal);
            pEsikStatic.Dock = DockStyle.Fill;

            pEsikDynamic = MakeParamContainer();
            cmbEsikMatrix = AddComboInPanel(pEsikDynamic, "Matris boyutu", new[] { "3x3", "5x5", "7x7", "11x11", "15x15" }, 0, 0);
            pEsikDynamic.Dock = DockStyle.Fill;
            pEsikDynamic.Visible = false;

            pEsik.Controls.Add(pEsikDynamic);
            pEsik.Controls.Add(pEsikStatic);
            rbEsikStatic.CheckedChanged += (s, e) => { pEsikStatic.Visible = rbEsikStatic.Checked; pEsikDynamic.Visible = !rbEsikStatic.Checked; };

            // Gürültü
            pNoise = MakeParamContainer();
            var noiseModRow = MakeParamRow();
            noiseModRow.Controls.Add(new Label { Text = "Mod", ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = false, Width = 50, Height = 26, Location = new Point(0, 5) });
            rbNoiseAdd = new RadioButton { Text = "Ekle", Checked = true, ForeColor = ACCENT2, Font = new Font("Segoe UI", 9f), AutoSize = true, BackColor = Color.Transparent, Location = new Point(55, 8) };
            rbNoiseRemove = new RadioButton { Text = "Temizle", Checked = false, ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = true, BackColor = Color.Transparent, Location = new Point(130, 8) };
            rbNoiseAdd.CheckedChanged += (s, e) => rbNoiseAdd.ForeColor = rbNoiseAdd.Checked ? ACCENT2 : TEXT2;
            rbNoiseRemove.CheckedChanged += (s, e) => rbNoiseRemove.ForeColor = rbNoiseRemove.Checked ? ACCENT2 : TEXT2;
            noiseModRow.Controls.Add(rbNoiseAdd);
            noiseModRow.Controls.Add(rbNoiseRemove);
            pNoise.Controls.Add(noiseModRow);

            pNoiseAdd = MakeParamContainer();
            cmbNoiseType = AddComboInPanel(pNoiseAdd, "Gürültü türü", new[] { "Salt & Pepper", "Salt", "Pepper" }, 0, 36);
            trkNoise = AddSlider(pNoiseAdd, "Oran (%)", 0, 100, 10, 72, out lblNoiseVal);
            pNoiseAdd.Dock = DockStyle.Fill;

            pNoiseRemove = MakeParamContainer();
            cmbNoiseRemove = AddComboInPanel(pNoiseRemove, "Filtre türü", new[] { "Mean Filtresi", "Median Filtresi" }, 0, 36);
            cmbNoiseMatrix = AddComboInPanel(pNoiseRemove, "Matris boyutu", new[] { "3x3", "5x5", "7x7", "9x9" }, 0, 72);
            pNoiseRemove.Dock = DockStyle.Fill;
            pNoiseRemove.Visible = false;

            pNoise.Controls.Add(pNoiseRemove);
            pNoise.Controls.Add(pNoiseAdd);
            rbNoiseAdd.CheckedChanged += (s, e) => { pNoiseAdd.Visible = rbNoiseAdd.Checked; pNoiseRemove.Visible = !rbNoiseAdd.Checked; };

            pMean = MakeParamContainer();
            cmbMean = AddComboInPanel(pMean, "Matris boyutu", new[] { "3x3", "5x5", "7x7" }, 0, 0);

            pMorfo = MakeParamContainer();
            cmbMorfoType = AddComboInPanel(pMorfo, "İşlem türü", new[] { "Genişleme", "Aşınma", "Açma", "Kapama" }, 0, 0);
            cmbMorfoMatrix = AddComboInPanel(pMorfo, "Matris boyutu", new[] { "3x3", "5x5", "7x7" }, 0, 36);
            cmbMorfoShape = AddComboInPanel(pMorfo, "Şekil", new[] { "Kare", "Daire", "Haç", "Özel" }, 0, 72);

            pUnsharp = MakeParamContainer();
            cmbUnsharp = AddComboInPanel(pUnsharp, "Matris boyutu", new[] { "3x3", "5x5", "7x7" }, 0, 0);

            pKenar = MakeParamContainer();
            cmbKenar = AddComboInPanel(pKenar, "Yön", new[] { "Yatay (Horizontal)", "Dikey (Vertical)", "Her İkisi (Magnitude)" }, 0, 0);

            pRenk = MakeParamContainer();
            cmbRenk = AddComboInPanel(pRenk, "Hedef uzay", new[] { "RGB -> HSV", "RGB -> YCbCr", "RGB -> CMYK", "RGB -> Gri" }, 0, 0);
        }

        // Sabit konumlu slider — y offset ile
        TrackBar AddSlider(Panel parent, string label, int min, int max, int val, int y, out Label valLbl)
        {
            var lbl = new Label { Text = label, ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = false, Width = 100, Height = 26, Location = new Point(0, y + 5), TextAlign = ContentAlignment.MiddleLeft };
            var trk = new TrackBar { Minimum = min, Maximum = max, Value = val, Width = 180, Height = 26, TickFrequency = Math.Max(1, (max - min) / 10), BackColor = SURFACE, Location = new Point(105, y) };
            var vl = new Label { Text = val.ToString(), ForeColor = ACCENT2, Font = new Font("Segoe UI", 9f, FontStyle.Bold), AutoSize = false, Width = 40, Height = 26, Location = new Point(290, y + 5), TextAlign = ContentAlignment.MiddleLeft };
            trk.ValueChanged += (s, e) => vl.Text = trk.Value.ToString();
            parent.Controls.Add(lbl);
            parent.Controls.Add(trk);
            parent.Controls.Add(vl);
            valLbl = vl;
            return trk;
        }

        // Sabit konumlu combobox — y offset ile
        ComboBox AddComboInPanel(Panel parent, string label, string[] items, int sel, int y)
        {
            var lbl = new Label { Text = label, ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = false, Width = 110, Height = 26, Location = new Point(0, y + 5), TextAlign = ContentAlignment.MiddleLeft };
            var cmb = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, BackColor = SURFACE2, ForeColor = TEXT1, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9f), Width = 165, Location = new Point(115, y + 3) };
            cmb.Items.AddRange(items);
            cmb.SelectedIndex = sel;
            parent.Controls.Add(lbl);
            parent.Controls.Add(cmb);
            return cmb;
        }

        NumericUpDown AddNumInRow(Panel row, string label, int val, int x)
        {
            var lbl = new Label { Text = label, ForeColor = TEXT2, Font = new Font("Segoe UI", 9f), AutoSize = false, Width = 68, Height = 26, Location = new Point(x, 5), TextAlign = ContentAlignment.MiddleLeft };
            var num = new NumericUpDown { Minimum = 0, Maximum = 9999, Value = val, Width = 80, BackColor = SURFACE2, ForeColor = TEXT1, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9f), Location = new Point(x + 72, 5) };
            row.Controls.Add(lbl);
            row.Controls.Add(num);
            return num;
        }

        void ShowParam(string key)
        {
            paramArea.Controls.Clear();
            Control p = key switch
            {
                "gri" => pGri,
                "binary" => pBinary,
                "kirp" => pKirp,
                "dondur" => pDondur,
                "zoom" => pZoom,
                "aritm" => pAritm,
                "hist" => pHist,
                "kontrast" => pKontrast,
                "esik" => pEsik,
                "noise" => pNoise,
                "mean" => pMean,
                "morfo" => pMorfo,
                "unsharp" => pUnsharp,
                "kenar" => pKenar,
                "renk" => pRenk,
                _ => pGri
            };
            paramArea.Controls.Add(p);
        }

        void BtnLoad1_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Resim|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            imgInput?.Dispose();
            imgInput = LoadBitmapFull(ofd.FileName);
            picInput.Image = imgInput;
            btnLoad1.Text = "Yüklendi ✓";
            btnLoad1.ForeColor = GREEN;
            btnLoad1.FlatAppearance.BorderColor = GREEN;
        }

        void BtnLoad2_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Resim|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            secondImage?.Dispose();
            secondImage = LoadBitmapFull(ofd.FileName);
            btnLoad2.Text = "2. Yüklendi ✓";
            btnLoad2.ForeColor = GREEN;
            btnLoad2.FlatAppearance.BorderColor = GREEN;
        }

        static Bitmap LoadBitmapFull(string path)
        {
            using var src = new Bitmap(path);
            return src.Clone(new Rectangle(0, 0, src.Width, src.Height), PixelFormat.Format32bppArgb);
        }

        void BtnSave_Click(object sender, EventArgs e)
        {
            if (imgOutput == null) { MessageBox.Show("Kaydedilecek çıktı yok.", "VisuaLab"); return; }
            using var sfd = new SaveFileDialog { Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp", FileName = "visualab_output" };
            if (sfd.ShowDialog() == DialogResult.OK) imgOutput.Save(sfd.FileName);
        }

        void BtnApply_Click(object sender, EventArgs e)
        {
            if (imgInput == null) { MessageBox.Show("Önce bir resim yükleyin.", "VisuaLab"); return; }
            btnApply.Text = "İşleniyor...";
            btnApply.BackColor = SURFACE3;
            btnApply.Refresh();
            try
            {
                var src = new Bitmap(imgInput);
                Bitmap result = currentOp switch
                {
                    "gri" => ImageProcessor.ConvertToGrayscale(src),
                    "binary" => ImageProcessor.ConvertToBinary(src, trkBinary.Value),
                    "kirp" => ImageProcessor.CropImage(src, (int)numCropX.Value, (int)numCropY.Value, (int)numCropX.Value + (int)numCropW.Value, (int)numCropY.Value + (int)numCropH.Value),
                    "kenar" => ImageProcessor.ApplyPrewitt(src, cmbKenar.SelectedItem.ToString()),
                    "esik" => rbEsikStatic.Checked ? ImageProcessor.ApplyStaticThreshold(src, trkEsik.Value) : ImageProcessor.ApplyDynamicThreshold(src, int.Parse(cmbEsikMatrix.SelectedItem.ToString().Split('x')[0])),
                    "noise" => rbNoiseAdd.Checked ? ImageProcessor.AddNoise(src, trkNoise.Value, cmbNoiseType.SelectedItem.ToString()) : (cmbNoiseRemove.SelectedItem.ToString() == "Mean Filtresi" ? ImageProcessor.ApplyMeanFilter(src, int.Parse(cmbNoiseMatrix.SelectedItem.ToString().Split('x')[0])) : ImageProcessor.ApplyMedianFilter(src, int.Parse(cmbNoiseMatrix.SelectedItem.ToString().Split('x')[0]))),
                    "mean" => ImageProcessor.ApplyMeanFilter(src, int.Parse(cmbMean.SelectedItem.ToString().Split('x')[0])),
                    _ => null
                };

                if (result != null)
                {
                    imgOutput = result;
                    picOutput.Image = result;
                    lblOutStatus.Text = $"{result.Width} x {result.Height} px";
                    btnApply.Text = "Uygulandı ✓";
                    btnApply.BackColor = GREEN;
                }
                else
                {
                    MessageBox.Show("Bu işlem henüz kodlanmadı.", "VisuaLab");
                    ResetApplyBtn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "VisuaLab");
                ResetApplyBtn();
            }
        }

        void ResetApplyBtn()
        {
            btnApply.Text = "İşlemi Uygula";
            btnApply.BackColor = ACCENT;
        }

        void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string q = txtSearch.Text.ToLower();
            if (q == "i\u0307şlem ara...") return;
            foreach (Control c in GetNavScroll().Controls)
            {
                if (c is Panel item && item.Tag is string)
                {
                    string t = "";
                    foreach (Control ch in item.Controls)
                        if (ch is Label l) t = l.Text.ToLower();
                    item.Visible = string.IsNullOrEmpty(q) || t.Contains(q);
                }
            }
        }

        FlowLayoutPanel GetNavScroll()
        {
            foreach (Control c in navPanel.Controls)
                if (c is FlowLayoutPanel f) return f;
            return null;
        }

        Panel NewPanel(DockStyle dock, int height, Color bg)
        {
            var p = new Panel { Dock = dock, BackColor = bg };
            if (height > 0) p.Height = height;
            return p;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        static extern IntPtr CreateRoundRectRgn(int l, int t, int r, int b, int w, int h);

        void RoundCorners(Control ctrl, int r)
        {
            ctrl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width + 1, ctrl.Height + 1, r, r));
            ctrl.SizeChanged += (s, e) => ctrl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width + 1, ctrl.Height + 1, r, r));
        }
    }
}
