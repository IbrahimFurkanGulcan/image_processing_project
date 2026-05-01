using System;
using System.Drawing;

namespace ImageProcessingProject
{
    
    //  - Histogram (hesap, cizim, germe, genisletme, esitleme)
    //  - Renk uzayi donusumleri (RGB <-> HSV, RGB <-> YCbCr, RGB -> Gri)
    //  - Konvolusyon (mean) ve genel kernel uygulayici
    //
    // Kural: hazir filtre/donusum cagirilmayacak, her sey piksel piksel.
    // GetPixel/SetPixel kullaniyorum, mantik daha kolay oturuyor. Hizli olmasi
    // gerekirse sonra LockBits'e tasiriz.  
    public static class PikselIslem
    {
        // -------- HISTOGRAM --------

        // Gri-seviye histogram. Her pikselin gri tonu (R+G+B)/3 olarak alinir, sayilir.
        public static int[] HistogramHesapla(Bitmap img)
        {
            int[] h = new int[256];
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color c = img.GetPixel(x, y);
                    int gri = (c.R + c.G + c.B) / 3;
                    h[gri]++;
                }
            }
            return h;
        }

        // R, G, B kanali icin ayri histogram. Renkli histogram cizimi icin lazim oluyor.
        public static (int[] r, int[] g, int[] b) HistogramHesaplaRGB(Bitmap img)
        {
            int[] hr = new int[256];
            int[] hg = new int[256];
            int[] hb = new int[256];

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color c = img.GetPixel(x, y);
                    hr[c.R]++;
                    hg[c.G]++;
                    hb[c.B]++;
                }
            }
            return (hr, hg, hb);
        }

        // Histogrami cubuk grafik gibi cizip Bitmap olarak verir.
        // (Bu sadece gorsellestirme, algoritma degil; matplotlib.bar gibi dusunulebilir.)
        public static Bitmap HistogramCiz(int[] hist, int W = 512, int H = 300)
        {
            Bitmap bmp = new Bitmap(W, H);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                int max = 0;
                for (int i = 0; i < 256; i++) if (hist[i] > max) max = hist[i];
                if (max == 0) return bmp;

                float kg = (float)W / 256f;
                for (int i = 0; i < 256; i++)
                {
                    float yk = (hist[i] / (float)max) * (H - 8);
                    g.FillRectangle(Brushes.Black, i * kg, H - yk, kg, yk);
                }
                g.DrawRectangle(Pens.Gray, 0, 0, W - 1, H - 1);
            }
            return bmp;
        }

        // R, G, B kanal histogramlarini ust uste yari saydam ciziyor (R kirmizi, G yesil, B mavi).
        public static Bitmap HistogramCizRGB(int[] hr, int[] hg, int[] hb, int W = 512, int H = 300)
        {
            Bitmap bmp = new Bitmap(W, H);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                int max = 0;
                for (int i = 0; i < 256; i++)
                {
                    if (hr[i] > max) max = hr[i];
                    if (hg[i] > max) max = hg[i];
                    if (hb[i] > max) max = hb[i];
                }
                if (max == 0) return bmp;

                float kg = (float)W / 256f;
                using (Brush bR = new SolidBrush(Color.FromArgb(140, 220, 30, 30)))
                using (Brush bG = new SolidBrush(Color.FromArgb(140, 30, 180, 30)))
                using (Brush bB = new SolidBrush(Color.FromArgb(140, 30, 60, 220)))
                {
                    for (int i = 0; i < 256; i++)
                    {
                        float yR = (hr[i] / (float)max) * (H - 8);
                        float yG = (hg[i] / (float)max) * (H - 8);
                        float yB = (hb[i] / (float)max) * (H - 8);
                        g.FillRectangle(bR, i * kg, H - yR, kg, yR);
                        g.FillRectangle(bG, i * kg, H - yG, kg, yG);
                        g.FillRectangle(bB, i * kg, H - yB, kg, yB);
                    }
                }
                g.DrawRectangle(Pens.Gray, 0, 0, W - 1, H - 1);
            }
            return bmp;
        }

        // Klasik histogram germe.
        // En koyu pikseli 0'a, en aciktakini 255'e tasiyip aralari linear yayiyoruz.
        // Formul: yeni = (eski - min) * 255 / (max - min)
        // Renkli resimde her kanalin kendi min/max'ini buluyorum, boylece renk dengesi cok bozulmaz.
        public static Bitmap HistogramGerme(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;

            int rMin = 255, rMax = 0;
            int gMin = 255, gMax = 0;
            int bMin = 255, bMax = 0;

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    if (c.R < rMin) rMin = c.R; if (c.R > rMax) rMax = c.R;
                    if (c.G < gMin) gMin = c.G; if (c.G > gMax) gMax = c.G;
                    if (c.B < bMin) bMin = c.B; if (c.B > bMax) bMax = c.B;
                }
            }

            int rRange = rMax - rMin; if (rRange == 0) rRange = 1;
            int gRange = gMax - gMin; if (gRange == 0) gRange = 1;
            int bRange = bMax - bMin; if (bRange == 0) bRange = 1;

            Bitmap son = new Bitmap(W, H);
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);

                    int r = ((c.R - rMin) * 255) / rRange;
                    int g = ((c.G - gMin) * 255) / gRange;
                    int b = ((c.B - bMin) * 255) / bRange;

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    son.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return son;
        }

        // Histogram genisletme: aslinda germenin daha "saglam" hali.
        // Tek bir uc pikselin yuzunden butun streche cuvallamasin diye en koyu/en acik
        // pikseli direkt almak yerine yuzdelik (ornegin %1 ile %99) noktalarini kullaniyoruz.
        // Ondan sonrasi yine aynisi: linear olarak 0..255'e yay.
        public static Bitmap HistogramGenisletme(Bitmap img, double altYuzde = 1.0, double ustYuzde = 99.0)
        {
            int W = img.Width;
            int H = img.Height;
            int toplam = W * H;

            (int[] hr, int[] hg, int[] hb) = HistogramHesaplaRGB(img);

            int rLo = YuzdelikBul(hr, toplam, altYuzde);
            int rHi = YuzdelikBul(hr, toplam, ustYuzde);
            int gLo = YuzdelikBul(hg, toplam, altYuzde);
            int gHi = YuzdelikBul(hg, toplam, ustYuzde);
            int bLo = YuzdelikBul(hb, toplam, altYuzde);
            int bHi = YuzdelikBul(hb, toplam, ustYuzde);

            int rRange = rHi - rLo; if (rRange <= 0) rRange = 1;
            int gRange = gHi - gLo; if (gRange <= 0) gRange = 1;
            int bRange = bHi - bLo; if (bRange <= 0) bRange = 1;

            Bitmap son = new Bitmap(W, H);
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);

                    int r = ((c.R - rLo) * 255) / rRange;
                    int g = ((c.G - gLo) * 255) / gRange;
                    int b = ((c.B - bLo) * 255) / bRange;

                    if (r < 0) r = 0; else if (r > 255) r = 255;
                    if (g < 0) g = 0; else if (g > 255) g = 255;
                    if (b < 0) b = 0; else if (b > 255) b = 255;

                    son.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return son;
        }

        // Histogram dizisinde verilen yuzdeligi (mesela %1 ya da %99) bulur.
        // Toplam pikselin yuzdesi kadar piksel toplanasiya kadar gri seviyeleri yukseltiyoruz.
        private static int YuzdelikBul(int[] hist, int toplam, double yuzde)
        {
            int hedef = (int)(toplam * yuzde / 100.0);
            int kumul = 0;
            for (int i = 0; i < 256; i++)
            {
                kumul += hist[i];
                if (kumul >= hedef) return i;
            }
            return 255;
        }

        // Histogram esitleme. Kumulatif dagilim (CDF) ile her gri tonu yeniden esliyoruz.
        // Formul:  T(i) = round( (CDF(i) - CDFmin) / (W*H - CDFmin) * 255 )
        // Renkli resimde her kanal icin ayri LUT cikarip kanali ona gore ceviriyorum.
        public static Bitmap HistogramEsitleme(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            int toplam = W * H;

            (int[] hr, int[] hg, int[] hb) = HistogramHesaplaRGB(img);

            byte[] lutR = LutUret(hr, toplam);
            byte[] lutG = LutUret(hg, toplam);
            byte[] lutB = LutUret(hb, toplam);

            Bitmap son = new Bitmap(W, H);
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    son.SetPixel(x, y, Color.FromArgb(lutR[c.R], lutG[c.G], lutB[c.B]));
                }
            }
            return son;
        }

        // Tek kanalin histogramindan CDF cikarip 0..255 arasinda haritalama tablosu uretir.
        private static byte[] LutUret(int[] hist, int toplamPiksel)
        {
            int[] cdf = new int[256];
            cdf[0] = hist[0];
            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + hist[i];
            }

            int cdfMin = 0;
            for (int i = 0; i < 256; i++)
            {
                if (cdf[i] != 0) { cdfMin = cdf[i]; break; }
            }

            byte[] lut = new byte[256];
            int payda = toplamPiksel - cdfMin;

            // Tek tonlu resimde anlamli esitleme yok, identity ver
            if (payda <= 0)
            {
                for (int i = 0; i < 256; i++) lut[i] = (byte)i;
                return lut;
            }

            for (int i = 0; i < 256; i++)
            {
                double t = (cdf[i] - cdfMin) * 255.0 / payda;
                int v = (int)(t + 0.5);
                if (v < 0) v = 0; else if (v > 255) v = 255;
                lut[i] = (byte)v;
            }
            return lut;
        }

        // -------- RENK UZAYI --------
        //
        // C# Bitmap her zaman R, G, B kanali tutuyor; bu yuzden HSV ve YCbCr sonuclari
        // direkt gosterilemez. Onlari da R/G/B yuvalarina paketliyorum:
        //   HSV   -> R = H/360 * 255,  G = S * 255,  B = V * 255
        //   YCbCr -> R = Y, G = Cb, B = Cr
        // Geri donusumler de ayni paketlemeyi varsayiyor.

        public static Bitmap RgbToHsv(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    double r = c.R / 255.0;
                    double g = c.G / 255.0;
                    double b = c.B / 255.0;

                    double mx = r;
                    if (g > mx) mx = g;
                    if (b > mx) mx = b;

                    double mn = r;
                    if (g < mn) mn = g;
                    if (b < mn) mn = b;

                    double delta = mx - mn;

                    double V = mx;
                    double S = (mx == 0) ? 0 : delta / mx;
                    double Hh;

                    // Renksiz (gri) durumda H tanimsiz; sifir verdik.
                    if (delta < 1e-9)
                    {
                        Hh = 0;
                    }
                    else if (mx == r)
                    {
                        Hh = 60.0 * ((g - b) / delta);
                    }
                    else if (mx == g)
                    {
                        Hh = 60.0 * (((b - r) / delta) + 2.0);
                    }
                    else
                    {
                        Hh = 60.0 * (((r - g) / delta) + 4.0);
                    }
                    if (Hh < 0) Hh += 360.0;

                    int hb_ = (int)(Hh / 360.0 * 255.0 + 0.5);
                    int sb_ = (int)(S * 255.0 + 0.5);
                    int vb_ = (int)(V * 255.0 + 0.5);

                    if (hb_ < 0) hb_ = 0; else if (hb_ > 255) hb_ = 255;
                    if (sb_ < 0) sb_ = 0; else if (sb_ > 255) sb_ = 255;
                    if (vb_ < 0) vb_ = 0; else if (vb_ > 255) vb_ = 255;

                    son.SetPixel(x, y, Color.FromArgb(hb_, sb_, vb_));
                }
            }
            return son;
        }

        // Girdi RgbToHsv'in formatinda olmali (R=H, G=S, B=V).
        public static Bitmap HsvToRgb(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    double Hh = (c.R / 255.0) * 360.0;
                    double S = c.G / 255.0;
                    double V = c.B / 255.0;

                    double C = V * S;
                    double m = V - C;

                    // X = C * (1 - |((H/60) mod 2) - 1|)
                    // C# ' nin '%' operatoru negatif sonuc verebildigi icin
                    // mod 2'yi Floor ile elden hesapliyorum.
                    double hp = Hh / 60.0;
                    double mod2 = hp - 2.0 * Math.Floor(hp / 2.0);
                    double X = C * (1.0 - Math.Abs(mod2 - 1.0));

                    double rp, gp, bp;
                    if (Hh < 60)       { rp = C; gp = X; bp = 0; }
                    else if (Hh < 120) { rp = X; gp = C; bp = 0; }
                    else if (Hh < 180) { rp = 0; gp = C; bp = X; }
                    else if (Hh < 240) { rp = 0; gp = X; bp = C; }
                    else if (Hh < 300) { rp = X; gp = 0; bp = C; }
                    else               { rp = C; gp = 0; bp = X; }

                    int R = (int)((rp + m) * 255.0 + 0.5);
                    int G = (int)((gp + m) * 255.0 + 0.5);
                    int B = (int)((bp + m) * 255.0 + 0.5);

                    if (R < 0) R = 0; else if (R > 255) R = 255;
                    if (G < 0) G = 0; else if (G > 255) G = 255;
                    if (B < 0) B = 0; else if (B > 255) B = 255;

                    son.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return son;
        }

        // RGB -> YCbCr (BT.601 katsayilari). Sonuc R<-Y, G<-Cb, B<-Cr seklinde paketlenir.
        public static Bitmap RgbToYCbCr(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    double R = c.R, G = c.G, B = c.B;

                    double Y  = 0.299 * R + 0.587 * G + 0.114 * B;
                    double Cb = 128.0 - 0.168736 * R - 0.331264 * G + 0.5 * B;
                    double Cr = 128.0 + 0.5 * R - 0.418688 * G - 0.081312 * B;

                    int yy  = (int)(Y  + 0.5);
                    int cbi = (int)(Cb + 0.5);
                    int cri = (int)(Cr + 0.5);

                    if (yy < 0) yy = 0; else if (yy > 255) yy = 255;
                    if (cbi < 0) cbi = 0; else if (cbi > 255) cbi = 255;
                    if (cri < 0) cri = 0; else if (cri > 255) cri = 255;

                    son.SetPixel(x, y, Color.FromArgb(yy, cbi, cri));
                }
            }
            return son;
        }

        // Girdi RgbToYCbCr formatinda olmali (R=Y, G=Cb, B=Cr).
        public static Bitmap YCbCrToRgb(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    double Y  = c.R;
                    double Cb = c.G - 128.0;
                    double Cr = c.B - 128.0;

                    double R = Y + 1.402 * Cr;
                    double G = Y - 0.344136 * Cb - 0.714136 * Cr;
                    double B = Y + 1.772 * Cb;

                    int Ri = (int)(R + 0.5);
                    int Gi = (int)(G + 0.5);
                    int Bi = (int)(B + 0.5);

                    if (Ri < 0) Ri = 0; else if (Ri > 255) Ri = 255;
                    if (Gi < 0) Gi = 0; else if (Gi > 255) Gi = 255;
                    if (Bi < 0) Bi = 0; else if (Bi > 255) Bi = 255;

                    son.SetPixel(x, y, Color.FromArgb(Ri, Gi, Bi));
                }
            }
            return son;
        }

        // RGB -> CMYK. Standart formul:
        //   K = 1 - max(R,G,B)/255
        //   C = (1 - R/255 - K) / (1 - K)
        //   M = (1 - G/255 - K) / (1 - K)
        //   Y = (1 - B/255 - K) / (1 - K)
        // Bitmap 3 kanal tuttugu icin C/M/Y'yi R/G/B yuvalarina, K'yi de Alpha'ya
        // paketliyorum. Boylece K bilgisi kaybolmuyor, gorsel olarak da CMY kismi gozukuyor.
        public static Bitmap RgbToCmyk(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    double R = c.R / 255.0;
                    double G = c.G / 255.0;
                    double B = c.B / 255.0;

                    double mx = R;
                    if (G > mx) mx = G;
                    if (B > mx) mx = B;

                    double K = 1.0 - mx;
                    double C, M, Y;

                    // Tamamen siyah pikselde K=1 oluyor, bolme patlamasin diye kontrol
                    if (K >= 0.999)
                    {
                        C = 0; M = 0; Y = 0;
                    }
                    else
                    {
                        double payda = 1.0 - K;
                        C = (1.0 - R - K) / payda;
                        M = (1.0 - G - K) / payda;
                        Y = (1.0 - B - K) / payda;
                    }

                    int Ci = (int)(C * 255.0 + 0.5);
                    int Mi = (int)(M * 255.0 + 0.5);
                    int Yi = (int)(Y * 255.0 + 0.5);
                    int Ki = (int)(K * 255.0 + 0.5);

                    if (Ci < 0) Ci = 0; else if (Ci > 255) Ci = 255;
                    if (Mi < 0) Mi = 0; else if (Mi > 255) Mi = 255;
                    if (Yi < 0) Yi = 0; else if (Yi > 255) Yi = 255;
                    if (Ki < 0) Ki = 0; else if (Ki > 255) Ki = 255;

                    // ARGB: A=K (255-Ki dersek "ne kadar siyah" tersine olur, sade tutuyorum: A = K)
                    son.SetPixel(x, y, Color.FromArgb(Ki, Ci, Mi, Yi));
                }
            }
            return son;
        }

        // RGB -> Gri. Standart luminance agirligi (BT.601). (R+G+B)/3 ortalamasindan
        // gozumuze daha dogru bir gri verir; uygulamasi da ayni kolaylikta.
        public static Bitmap RgbToGri(Bitmap img)
        {
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    Color c = img.GetPixel(x, y);
                    int gri = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B + 0.5);
                    if (gri < 0) gri = 0; else if (gri > 255) gri = 255;
                    son.SetPixel(x, y, Color.FromArgb(gri, gri, gri));
                }
            }
            return son;
        }

        // -------- KONVOLUSYON --------

        // NxN kare kernel ile genel konvolusyon. Kenarlarda en yakin gecerli pikseli
        // kopyaliyoruz (clamp), boylece dis cerceve karaya donmuyor.
        // Formul:  O(x,y) = sum_{ky,kx}  K(ky,kx) * I(x+kx, y+ky)
        public static Bitmap Konvolusyon(Bitmap img, double[,] kernel)
        {
            int kSize = kernel.GetLength(0);
            if (kernel.GetLength(1) != kSize)
                throw new ArgumentException("Kernel kare olmali (NxN).");
            if (kSize % 2 == 0)
                throw new ArgumentException("Kernel boyutu tek olmali (3, 5, 7 ...).");

            int off = kSize / 2;
            int W = img.Width;
            int H = img.Height;
            Bitmap son = new Bitmap(W, H);

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    double sR = 0, sG = 0, sB = 0;

                    for (int ky = -off; ky <= off; ky++)
                    {
                        int ny = y + ky;
                        if (ny < 0) ny = 0;
                        else if (ny >= H) ny = H - 1;

                        for (int kx = -off; kx <= off; kx++)
                        {
                            int nx = x + kx;
                            if (nx < 0) nx = 0;
                            else if (nx >= W) nx = W - 1;

                            Color c = img.GetPixel(nx, ny);
                            double k = kernel[ky + off, kx + off];

                            sR += c.R * k;
                            sG += c.G * k;
                            sB += c.B * k;
                        }
                    }

                    int r = (int)(sR + 0.5); if (r < 0) r = 0; else if (r > 255) r = 255;
                    int g = (int)(sG + 0.5); if (g < 0) g = 0; else if (g > 255) g = 255;
                    int b = (int)(sB + 0.5); if (b < 0) b = 0; else if (b > 255) b = 255;

                    son.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return son;
        }

        // Mean (ortalama) konvolusyon. NxN kernelin tum hucreleri 1/(N*N) oluyor.
        public static Bitmap MeanKonvolusyon(Bitmap img, int boyut)
        {
            if (boyut < 3 || boyut % 2 == 0)
                throw new ArgumentException("Boyut 3, 5, 7 ... gibi tek sayi olmali.");

            double k = 1.0 / (boyut * boyut);
            double[,] kernel = new double[boyut, boyut];
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    kernel[i, j] = k;
                }
            }
            return Konvolusyon(img, kernel);
        }
    }
}
