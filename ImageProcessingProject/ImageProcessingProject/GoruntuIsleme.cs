using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingProject
{
    public static class GoruntuIslem
    {
        // --- 1. ARİTMETİK İŞLEM: TOPLAMA ---
        public static Bitmap ResimTopla(Bitmap resim1, Bitmap resim2)
        {
            // Resimlerin boyutlarını kontrol ediyoruz
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height)
            {
                return null;
            }

            Bitmap sonuc = new(resim1.Width, resim1.Height);

            for (int x = 0; x < resim1.Width; x++)
            {
                for (int y = 0; y < resim1.Height; y++)
                {
                    Color renk1 = resim1.GetPixel(x, y);
                    Color renk2 = resim2.GetPixel(x, y);

                    // Toplama işlemi
                    int r = renk1.R + renk2.R;
                    int g = renk1.G + renk2.G;
                    int b = renk1.B + renk2.B;

                    // 255'i aşma kontrolü (Clamping)
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;

                    sonuc.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return sonuc;
        }

        // --- 2. ARİTMETİK İŞLEM: BÖLME ---
        public static Bitmap ResimBol(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height)
            {
                return null;
            }

            Bitmap sonuc = new(resim1.Width, resim1.Height);

            for (int x = 0; x < resim1.Width; x++)
            {
                for (int y = 0; y < resim1.Height; y++)
                {
                    Color renk1 = resim1.GetPixel(x, y);
                    Color renk2 = resim2.GetPixel(x, y);

                    // Bölme işlemi (+1 sıfıra bölme hatasını önler)
                    int r = (renk1.R * 255) / (renk2.R + 1);
                    int g = (renk1.G * 255) / (renk2.G + 1);
                    int b = (renk1.B * 255) / (renk2.B + 1);

                    // Sınırları kontrol et
                    if (r > 255) r = 255; if (r < 0) r = 0;
                    if (g > 255) g = 255; if (g < 0) g = 0;
                    if (b > 255) b = 255; if (b < 0) b = 0;

                    sonuc.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return sonuc;
        }

        // --- ARİTMETİK İŞLEM: ÇIKARMA ---
        public static Bitmap ResimCikar(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height) return null;

            Bitmap sonuc = new(resim1.Width, resim1.Height);

            for (int x = 0; x < resim1.Width; x++)
            {
                for (int y = 0; y < resim1.Height; y++)
                {
                    Color renk1 = resim1.GetPixel(x, y);
                    Color renk2 = resim2.GetPixel(x, y);

                    // Çıkarma işlemi
                    int r = renk1.R - renk2.R;
                    int g = renk1.G - renk2.G;
                    int b = renk1.B - renk2.B;

                    // 0'ın altına inme kontrolü (Clamping)
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;

                    sonuc.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return sonuc;
        }

        // --- ARİTMETİK İŞLEM: ÇARPMA ---
        public static Bitmap ResimCarp(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height) return null;

            Bitmap sonuc = new(resim1.Width, resim1.Height);

            for (int x = 0; x < resim1.Width; x++)
            {
                for (int y = 0; y < resim1.Height; y++)
                {
                    Color renk1 = resim1.GetPixel(x, y);
                    Color renk2 = resim2.GetPixel(x, y);

                    // DİKKAT: Çarpma işleminde renkler çok hızlı 255'i aşar ve resim bembeyaz olur.
                    // Bu yüzden Photoshop'taki "Multiply" (Çoğalt) mantığıyla 255'e bölerek normalize ediyoruz.
                    int r = (renk1.R * renk2.R) / 255;
                    int g = (renk1.G * renk2.G) / 255;
                    int b = (renk1.B * renk2.B) / 255;

                    sonuc.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
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
                    if (sekil == "Kare")
                    {
                        matris[i, j] = 1; // Karenin her yeri doludur
                    }
                    else if (sekil == "Artı")
                    {
                        // Artı şeklinde sadece merkez satır ve merkez sütun doludur
                        if (i == merkez || j == merkez) matris[i, j] = 1;
                        else matris[i, j] = 0;
                    }
                    // İleride arayüze "Daire" veya "Çapraz" eklenirse buraya kolayca eklenebilir
                }
            }
            return matris;
        }

        // --- 1. DİNAMİK VE HIZLI GENİŞLEME (DILATION) ---
        public static Bitmap Genisleme(Bitmap kaynakResim, int matrisBoyutu, string sekil)
        {
            Bitmap sonuc = new(kaynakResim.Width, kaynakResim.Height, PixelFormat.Format24bppRgb);

            int[,] matris = MatrisOlustur(matrisBoyutu, sekil);
            int offset = matrisBoyutu / 2; // Kenar boşlukları boyuta göre dinamik hesaplanır

            BitmapData srcData = kaynakResim.LockBits(new Rectangle(0, 0, kaynakResim.Width, kaynakResim.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

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
                        byte maxB = 0, maxG = 0, maxR = 0;

                        // Arayüzden gelen özel matris boyutuna göre komşuluk taraması
                        for (int my = -offset; my <= offset; my++)
                        {
                            for (int mx = -offset; mx <= offset; mx++)
                            {
                                // Sadece matrisin "1" (dolu) olan yerlerini işleme dahil et
                                if (matris[my + offset, mx + offset] == 1)
                                {
                                    byte* piksel = srcPtr + ((y + my) * stride) + ((x + mx) * 3);
                                    if (piksel[0] > maxB) maxB = piksel[0];
                                    if (piksel[1] > maxG) maxG = piksel[1];
                                    if (piksel[2] > maxR) maxR = piksel[2];
                                }
                            }
                        }

                        byte* sonucPiksel = dstPtr + (y * stride) + (x * 3);
                        sonucPiksel[0] = maxB;
                        sonucPiksel[1] = maxG;
                        sonucPiksel[2] = maxR;
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
            Bitmap sonuc = new(kaynakResim.Width, kaynakResim.Height, PixelFormat.Format24bppRgb);

            int[,] matris = MatrisOlustur(matrisBoyutu, sekil);
            int offset = matrisBoyutu / 2;

            BitmapData srcData = kaynakResim.LockBits(new Rectangle(0, 0, kaynakResim.Width, kaynakResim.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = sonuc.LockBits(new Rectangle(0, 0, sonuc.Width, sonuc.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;
                int stride = srcData.Stride;

                for (int y = offset; y < kaynakResim.Height - offset; y++)
                {
                    for (int x = offset; x < kaynakResim.Width - offset; x++)
                    {
                        byte minB = 255, minG = 255, minR = 255;

                        for (int my = -offset; my <= offset; my++)
                        {
                            for (int mx = -offset; mx <= offset; mx++)
                            {
                                if (matris[my + offset, mx + offset] == 1)
                                {
                                    byte* piksel = srcPtr + ((y + my) * stride) + ((x + mx) * 3);
                                    if (piksel[0] < minB) minB = piksel[0];
                                    if (piksel[1] < minG) minG = piksel[1];
                                    if (piksel[2] < minR) minR = piksel[2];
                                }
                            }
                        }

                        byte* sonucPiksel = dstPtr + (y * stride) + (x * 3);
                        sonucPiksel[0] = minB;
                        sonucPiksel[1] = minG;
                        sonucPiksel[2] = minR;
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