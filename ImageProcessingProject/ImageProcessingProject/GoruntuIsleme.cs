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
        public static unsafe Bitmap UnsharpMask(Bitmap kaynakResim, double miktar = 1.5)
        {
            int genislik = kaynakResim.Width;
            int yukseklik = kaynakResim.Height;

            // Sonuç için yeni bir bitmap oluşturuyoruz (24 bit hız için en iyisidir)
            Bitmap sonuc = new Bitmap(genislik, yukseklik, PixelFormat.Format24bppRgb);

            // Her iki resmi de hafızaya kilitliyoruz (LockBits)
            BitmapData srcData = kaynakResim.LockBits(new Rectangle(0, 0, genislik, yukseklik), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstData = sonuc.LockBits(new Rectangle(0, 0, genislik, yukseklik), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            byte* srcPtr = (byte*)srcData.Scan0;
            byte* dstPtr = (byte*)dstData.Scan0;
            int stride = srcData.Stride;

            // Resmin kenarlarından 1 piksel içeriden başlıyoruz (3x3 kernel için hata almamak adına)
            for (int y = 1; y < yukseklik - 1; y++)
            {
                for (int x = 1; x < genislik - 1; x++)
                {
                    int toplamR = 0, toplamG = 0, toplamB = 0;

                    // 1. ADIM: 3x3 alanda Mean (Ortalama) hesapla (Bulanıklaştırma)
                    for (int my = -1; my <= 1; my++)
                    {
                        for (int mx = -1; mx <= 1; mx++)
                        {
                            // Komşu piksele pointer ile erişim
                            byte* komsu = srcPtr + ((y + my) * stride) + ((x + mx) * 3);
                            toplamB += komsu[0]; // Mavi
                            toplamG += komsu[1]; // Yeşil
                            toplamR += komsu[2]; // Kırmızı
                        }
                    }

                    int meanB = toplamB / 9;
                    int meanG = toplamG / 9;
                    int meanR = toplamR / 9;

                    // 2. ADIM: Orijinal piksel değerini al
                    byte* orj = srcPtr + (y * stride) + (x * 3);

                    // 3. ADIM: Unsharp Mask formülünü uygula: Orj + miktar * (Orj - Mean)
                    int yeniB = (int)(orj[0] + miktar * (orj[0] - meanB));
                    int yeniG = (int)(orj[1] + miktar * (orj[1] - meanG));
                    int yeniR = (int)(orj[2] + miktar * (orj[2] - meanR));

                    // Renk değerlerini 0-255 arasında sınırla (Clamping)
                    byte* hedef = dstPtr + (y * stride) + (x * 3);
                    hedef[0] = (byte)Math.Max(0, Math.Min(255, yeniB));
                    hedef[1] = (byte)Math.Max(0, Math.Min(255, yeniG));
                    hedef[2] = (byte)Math.Max(0, Math.Min(255, yeniR));
                }
            }

            // Hafıza kilidini kaldırıyoruz
            kaynakResim.UnlockBits(srcData);
            sonuc.UnlockBits(dstData);

            return sonuc;
        }
    }
}