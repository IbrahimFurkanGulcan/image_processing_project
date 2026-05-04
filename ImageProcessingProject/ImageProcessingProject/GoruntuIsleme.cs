using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingProject
{
    public static class GoruntuIslem
    {


        // Tüm işlemlerde kullanacağımız, resmi 32bit formatına çeviren güvenlik kalkanımız
        // Bu sayede Stride (satır sonu boşlukları) hesaplamalarıyla uğraşmayız.
        private static Bitmap Get32BppImage(Bitmap original)
        {
            // Yeni resmi orijinalin boyutuyla oluşturuyoruz
            Bitmap bmp = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);

            // C#'ın resmi kendi kafasına göre büyütüp küçültmesini (DPI Bug) engelle
            bmp.SetResolution(original.HorizontalResolution, original.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Kaliteyi bozmadan ve pikselleri kaydırmadan 1'e 1 kopyalama (Interpolation kapatıldı)
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                // Kaynak ve hedef dikdörtgeni tam olarak eşitliyoruz                         
                g.DrawImage(original,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    new Rectangle(0, 0, original.Width, original.Height),
                    GraphicsUnit.Pixel);

                //Bitmap clone = original.Clone(
                //  new Rectangle(0, 0, original.Width, original.Height),
                //PixelFormat.Format32bppArgb);
                //daha kısa yol ama scale olursa çalışmaz
            }
            return bmp;
        }


        // ==========================================================
        // 1. ARİTMETİK İŞLEM: TOPLAMA
        // ==========================================================
        public static Bitmap ResimTopla(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height) return null;

            // Her iki resmi de aynı piksel formatına (32bpp) zorluyoruz
            Bitmap b1 = Get32BppImage(resim1);
            Bitmap b2 = Get32BppImage(resim2);
            Bitmap sonuc = new Bitmap(b1.Width, b1.Height, PixelFormat.Format32bppArgb);

            BitmapData data1 = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData data2 = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dataSonuc = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr1 = (byte*)data1.Scan0;
                byte* ptr2 = (byte*)data2.Scan0;
                byte* ptrSonuc = (byte*)dataSonuc.Scan0;

                int bytes = Math.Abs(data1.Stride) * b1.Height;

                for (int i = 0; i < bytes; i += 4)
                {
                    int b = ptr1[i] + ptr2[i];
                    int g = ptr1[i + 1] + ptr2[i + 1];
                    int r = ptr1[i + 2] + ptr2[i + 2];

                    // 255 sınırını (Clamping) aşıp aşmadığını kontrol et
                    ptrSonuc[i] = (byte)(b > 255 ? 255 : b);
                    ptrSonuc[i + 1] = (byte)(g > 255 ? 255 : g);
                    ptrSonuc[i + 2] = (byte)(r > 255 ? 255 : r);
                    ptrSonuc[i + 3] = 255; // Alpha kanalı (Opak)
                }
            }

            b1.UnlockBits(data1);
            b2.UnlockBits(data2);
            sonuc.UnlockBits(dataSonuc);

            return sonuc;
        }

        // ==========================================================
        // 2. ARİTMETİK İŞLEM: BÖLME
        // ==========================================================
        public static Bitmap ResimBol(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height) return null;

            Bitmap b1 = Get32BppImage(resim1);
            Bitmap b2 = Get32BppImage(resim2);
            Bitmap sonuc = new Bitmap(b1.Width, b1.Height, PixelFormat.Format32bppArgb);

            BitmapData data1 = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData data2 = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dataSonuc = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr1 = (byte*)data1.Scan0;
                byte* ptr2 = (byte*)data2.Scan0;
                byte* ptrSonuc = (byte*)dataSonuc.Scan0;

                int bytes = Math.Abs(data1.Stride) * b1.Height;

                for (int i = 0; i < bytes; i += 4)
                {
                    int b = (ptr1[i] * 255) / (ptr2[i] + 1);
                    int g = (ptr1[i + 1] * 255) / (ptr2[i + 1] + 1);
                    int r = (ptr1[i + 2] * 255) / (ptr2[i + 2] + 1);

                    ptrSonuc[i] = (byte)(b > 255 ? 255 : b);
                    ptrSonuc[i + 1] = (byte)(g > 255 ? 255 : g);
                    ptrSonuc[i + 2] = (byte)(r > 255 ? 255 : r);
                    ptrSonuc[i + 3] = 255;
                }
            }

            b1.UnlockBits(data1);
            b2.UnlockBits(data2);
            sonuc.UnlockBits(dataSonuc);

            return sonuc;
        }

        // ==========================================================
        // 3. ARİTMETİK İŞLEM: ÇIKARMA
        // ==========================================================
        public static Bitmap ResimCikar(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height) return null;

            Bitmap b1 = Get32BppImage(resim1);
            Bitmap b2 = Get32BppImage(resim2);
            Bitmap sonuc = new Bitmap(b1.Width, b1.Height, PixelFormat.Format32bppArgb);

            BitmapData data1 = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData data2 = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dataSonuc = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr1 = (byte*)data1.Scan0;
                byte* ptr2 = (byte*)data2.Scan0;
                byte* ptrSonuc = (byte*)dataSonuc.Scan0;

                int bytes = Math.Abs(data1.Stride) * b1.Height;

                for (int i = 0; i < bytes; i += 4)
                {
                    int b = ptr1[i] - ptr2[i];
                    int g = ptr1[i + 1] - ptr2[i + 1];
                    int r = ptr1[i + 2] - ptr2[i + 2];

                    // 0'ın altına inme kontrolü
                    ptrSonuc[i] = (byte)(b < 0 ? 0 : b);
                    ptrSonuc[i + 1] = (byte)(g < 0 ? 0 : g);
                    ptrSonuc[i + 2] = (byte)(r < 0 ? 0 : r);
                    ptrSonuc[i + 3] = 255;
                }
            }

            b1.UnlockBits(data1);
            b2.UnlockBits(data2);
            sonuc.UnlockBits(dataSonuc);

            return sonuc;
        }

        // ==========================================================
        // 4. ARİTMETİK İŞLEM: ÇARPMA
        // ==========================================================
        public static Bitmap ResimCarp(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height) return null;

            Bitmap b1 = Get32BppImage(resim1);
            Bitmap b2 = Get32BppImage(resim2);
            Bitmap sonuc = new Bitmap(b1.Width, b1.Height, PixelFormat.Format32bppArgb);

            BitmapData data1 = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData data2 = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dataSonuc = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr1 = (byte*)data1.Scan0;
                byte* ptr2 = (byte*)data2.Scan0;
                byte* ptrSonuc = (byte*)dataSonuc.Scan0;

                int bytes = Math.Abs(data1.Stride) * b1.Height;

                for (int i = 0; i < bytes; i += 4)
                {
                    int b = (ptr1[i] * ptr2[i]) / 255;
                    int g = (ptr1[i + 1] * ptr2[i + 1]) / 255;
                    int r = (ptr1[i + 2] * ptr2[i + 2]) / 255;

                    // Bölme yaptığımız için sınırı aşmaz, doğrudan cast edebiliriz
                    ptrSonuc[i] = (byte)b;
                    ptrSonuc[i + 1] = (byte)g;
                    ptrSonuc[i + 2] = (byte)r;
                    ptrSonuc[i + 3] = 255;
                }
            }

            b1.UnlockBits(data1);
            b2.UnlockBits(data2);
            sonuc.UnlockBits(dataSonuc);

            return sonuc;
        }

        // --- 3. MORFOLOJİK İŞLEM: GENİŞLEME (DILATION) ---
        // --- YARDIMCI FONKSİYON: Arayüzden Gelen Şekil ve Boyuta Göre Matris Üretici ---
        public static int[,] MatrisOlustur(int boyut, string sekil)
        {
            int[,] matris = new int[boyut, boyut];
            int merkez = boyut / 2;

            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    // 1. KARE KONTROLÜ
                    if (sekil.Contains("Kare"))
                    {
                        matris[i, j] = 1;
                    }
                    // 2. HAÇ (CROSS) / ARTI KONTROLÜ
                    else if (sekil.Contains("Haç") || sekil.Contains("Artı"))
                    {
                        if (i == merkez || j == merkez) matris[i, j] = 1;
                        else matris[i, j] = 0;
                    }
                    // 3. DAİRE (CIRCLE) KONTROLÜ
                    else if (sekil.Contains("Daire"))
                    {
                        double mesafe = Math.Sqrt(Math.Pow(i - merkez, 2) + Math.Pow(j - merkez, 2));
                        if (mesafe <= merkez) matris[i, j] = 1;
                        else matris[i, j] = 0;
                    }
                    // 4. ÖZEL (CUSTOM) KONTROLÜ
                    else if (sekil.Contains("Özel"))
                    {
                        // Arayüzde özel bir çizim alanı yoksa, siyah ekran vermesin diye 
                        // sadece merkezi 1 yapıyoruz (Görüntüyü değiştirmez, etkisiz elemandır).
                        if (i == merkez && j == merkez) matris[i, j] = 1;
                        else matris[i, j] = 0;
                    }
                }
            }
            return matris;
        }

        // --- 1. DİNAMİK VE HIZLI GENİŞLEME (DILATION) ---
        public static Bitmap Genisleme(Bitmap kaynakResim, int matrisBoyutu, string sekil)
        {
            // 1. Sonuç resmini 32-bit (ARGB) olarak oluşturuyoruz
            Bitmap sonuc = new Bitmap(kaynakResim.Width, kaynakResim.Height, PixelFormat.Format32bppArgb);

            int[,] matris = MatrisOlustur(matrisBoyutu, sekil);
            int offset = matrisBoyutu / 2;

            // 2. İki resmi de 32-bit formatında kilitliyoruz ki hafıza kayması yaşanmasın
            BitmapData srcData = kaynakResim.LockBits(new Rectangle(0, 0, kaynakResim.Width, kaynakResim.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;
                int stride = srcData.Stride; // İkisi de 32-bit olduğu için stride (satır uzunluğu) tamamen aynıdır

                // Döngüleri offset kadar içeriden başlatıyoruz ki taşma olmasın
                for (int y = offset; y < kaynakResim.Height - offset; y++)
                {
                    for (int x = offset; x < kaynakResim.Width - offset; x++)
                    {
                        byte maxB = 0, maxG = 0, maxR = 0;

                        // Arayüzden gelen matrise göre komşuluk taraması
                        for (int my = -offset; my <= offset; my++)
                        {
                            for (int mx = -offset; mx <= offset; mx++)
                            {
                                if (matris[my + offset, mx + offset] == 1)
                                {
                                    // DİKKAT: 32-bit olduğu için "* 3" yerine "* 4" kullanıyoruz!
                                    byte* piksel = srcPtr + ((y + my) * stride) + ((x + mx) * 4);

                                    if (piksel[0] > maxB) maxB = piksel[0];
                                    if (piksel[1] > maxG) maxG = piksel[1];
                                    if (piksel[2] > maxR) maxR = piksel[2];
                                }
                            }
                        }

                        // DİKKAT: 32-bit olduğu için "* 3" yerine "* 4" kullanıyoruz!
                        byte* sonucPiksel = dstPtr + (y * stride) + (x * 4);
                        sonucPiksel[0] = maxB;
                        sonucPiksel[1] = maxG;
                        sonucPiksel[2] = maxR;
                        sonucPiksel[3] = 255; // KRİTİK NOKTA: Alpha (Saydamlık) kanalını 255 yapıyoruz ki simsiyah olmasın!
                    }
                }
            }

            kaynakResim.UnlockBits(srcData);
            sonuc.UnlockBits(dstData);

            return sonuc;
        }

        // --- 2. DİNAMİK VE HIZLI AŞINMA (EROSION) ---
        public static Bitmap Asinma(Bitmap kaynakResim, int matrisBoyutu, string sekil)
        {
            // 1. Sonuç resmini 32-bit (ARGB) olarak oluşturuyoruz
            Bitmap sonuc = new Bitmap(kaynakResim.Width, kaynakResim.Height, PixelFormat.Format32bppArgb);

            int[,] matris = MatrisOlustur(matrisBoyutu, sekil);
            int offset = matrisBoyutu / 2;

            // 2. İki resmi de 32-bit formatında kilitliyoruz ki hafıza kayması yaşanmasın
            BitmapData srcData = kaynakResim.LockBits(new Rectangle(0, 0, kaynakResim.Width, kaynakResim.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;
                int stride = srcData.Stride;

                // Döngüleri offset kadar içeriden başlatıyoruz ki taşma olmasın
                for (int y = offset; y < kaynakResim.Height - offset; y++)
                {
                    for (int x = offset; x < kaynakResim.Width - offset; x++)
                    {
                        // Aşınma işlemi için başlangıç değerleri 255 (Beyaz) olmalı. (Mantığın harika!)
                        byte minB = 255, minG = 255, minR = 255;

                        for (int my = -offset; my <= offset; my++)
                        {
                            for (int mx = -offset; mx <= offset; mx++)
                            {
                                if (matris[my + offset, mx + offset] == 1)
                                {
                                    // DİKKAT: 32-bit olduğu için "* 3" yerine "* 4" kullanıyoruz!
                                    byte* piksel = srcPtr + ((y + my) * stride) + ((x + mx) * 4);

                                    if (piksel[0] < minB) minB = piksel[0];
                                    if (piksel[1] < minG) minG = piksel[1];
                                    if (piksel[2] < minR) minR = piksel[2];
                                }
                            }
                        }

                        // DİKKAT: 32-bit olduğu için "* 3" yerine "* 4" kullanıyoruz!
                        byte* sonucPiksel = dstPtr + (y * stride) + (x * 4);
                        sonucPiksel[0] = minB;
                        sonucPiksel[1] = minG;
                        sonucPiksel[2] = minR;
                        sonucPiksel[3] = 255; // KRİTİK NOKTA: Alpha (Saydamlık) kanalını 255 yapıyoruz!
                    }
                }
            }

            kaynakResim.UnlockBits(srcData);
            sonuc.UnlockBits(dstData);

            return sonuc;
        }

        // --- 3. AÇMA VE KAPAMA (Arayüzde Varsa) ---
        public static Bitmap Acma(Bitmap kaynakResim, int matrisBoyutu, string sekil)
        {
            // Aşınma + Genişleme
            Bitmap gecici = Asinma(kaynakResim, matrisBoyutu, sekil);
            return Genisleme(gecici, matrisBoyutu, sekil);
        }

        public static Bitmap Kapama(Bitmap kaynakResim, int matrisBoyutu, string sekil)
        {
            // Genişleme + Aşınma
            Bitmap gecici = Genisleme(kaynakResim, matrisBoyutu, sekil);
            return Asinma(gecici, matrisBoyutu, sekil);
        }

        // --- 7. FİLTRELEME: UNSHARP MASK (KESKİNLEŞTİRME) ---
        public static Bitmap UnsharpMask(Bitmap kaynakResim, double miktar = 1.5)
        {
            int genislik = kaynakResim.Width;
            int yukseklik = kaynakResim.Height;
            Bitmap sonuc = new(genislik, yukseklik);

            // 1. ADIM: Önce resmi bulanıklaştır (Box Blur mantığıyla)
            // Kenarlardan 1 piksel içeriden başlıyoruz
            for (int x = 1; x < genislik - 1; x++)
            {
                for (int y = 1; y < yukseklik - 1; y++)
                {
                    int toplamR = 0, toplamG = 0, toplamB = 0;

                    // 3x3 komşuluktaki pikselleri topla (Bulanıklaştırmak için)
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            Color komsu = kaynakResim.GetPixel(x + i, y + j);
                            toplamR += komsu.R;
                            toplamG += komsu.G;
                            toplamB += komsu.B;
                        }
                    }

                    // Ortalamasını alarak bulanık pikseli bul (9 piksel olduğu için 9'a bölüyoruz)
                    int bulanikR = toplamR / 9;
                    int bulanikG = toplamG / 9;
                    int bulanikB = toplamB / 9;

                    // 2. ADIM: Unsharp Mask formülünü uygula
                    Color orjinal = kaynakResim.GetPixel(x, y);

                    int yeniR = (int)(orjinal.R + miktar * (orjinal.R - bulanikR));
                    int yeniG = (int)(orjinal.G + miktar * (orjinal.G - bulanikG));
                    int yeniB = (int)(orjinal.B + miktar * (orjinal.B - bulanikB));

                    // Sınırları kontrol et (0-255 arası)
                    yeniR = Math.Max(0, Math.Min(255, yeniR));
                    yeniG = Math.Max(0, Math.Min(255, yeniG));
                    yeniB = Math.Max(0, Math.Min(255, yeniB));

                    sonuc.SetPixel(x, y, Color.FromArgb(yeniR, yeniG, yeniB));
                }
            }
            return sonuc;
        }
    }
}