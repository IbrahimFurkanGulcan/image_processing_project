using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingProject
{
    public static class ImageProcessor
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
        // 1. GÜRÜLTÜ EKLEME (SALT, PEPPER VEYA İKİSİ)
        // ==========================================================
        public static Bitmap AddNoise(Bitmap original, int percentage, string noiseType)
        {
            Bitmap result = Get32BppImage(original);
            Random rnd = new Random();
            BitmapData data = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int totalPixels = result.Width * result.Height;
            int noisePixels = (totalPixels * percentage) / 100;

            unsafe
            {
                byte* ptrFirstPixel = (byte*)data.Scan0;

                for (int i = 0; i < noisePixels; i++)
                {
                    int x = rnd.Next(0, result.Width);
                    int y = rnd.Next(0, result.Height);
                    int xOffset = x * 4;
                    byte* currentLine = ptrFirstPixel + (y * data.Stride);

                    byte noiseValue = 0; // Varsayılan Siyah (Pepper)

                    if (noiseType == "Salt")
                    {
                        noiseValue = 255; // Sadece Beyaz
                    }
                    else if (noiseType == "Pepper")
                    {
                        noiseValue = 0; // Sadece Siyah
                    }
                    else // Salt & Pepper
                    {
                        noiseValue = (byte)(rnd.Next(0, 2) == 0 ? 0 : 255);
                    }

                    currentLine[xOffset] = noiseValue;
                    currentLine[xOffset + 1] = noiseValue;
                    currentLine[xOffset + 2] = noiseValue;
                }
            }

            result.UnlockBits(data);
            return result;
        }

        // ==========================================================
        // 2. GÜRÜLTÜ TEMİZLEME: DİNAMİK MEAN (ORTALAMA) FİLTRESİ
        // ==========================================================
        public static Bitmap ApplyMeanFilter(Bitmap original, int matrixSize)
        {
            Bitmap source = Get32BppImage(original);
            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* res = (byte*)resData.Scan0;
                int stride = srcData.Stride;
                int width = source.Width;
                int height = source.Height;

                int offset = matrixSize / 2;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int sumB = 0, sumG = 0, sumR = 0, count = 0;

                        // Dinamik boyutlu Kernel taraması ve kenar koruması
                        for (int ky = -offset; ky <= offset; ky++)
                        {
                            int ny = y + ky;
                            if (ny < 0 || ny >= height) continue; // Resmin dışına çıkmayı engelle

                            for (int kx = -offset; kx <= offset; kx++)
                            {
                                int nx = x + kx;
                                if (nx < 0 || nx >= width) continue; // Resmin dışına çıkmayı engelle

                                int idx = (ny * stride) + (nx * 4);
                                sumB += src[idx];
                                sumG += src[idx + 1];
                                sumR += src[idx + 2];
                                count++;
                            }
                        }

                        int resIdx = (y * stride) + (x * 4);
                        res[resIdx] = (byte)(sumB / count);
                        res[resIdx + 1] = (byte)(sumG / count);
                        res[resIdx + 2] = (byte)(sumR / count);
                        res[resIdx + 3] = 255;
                    }
                }
            }

            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

        // ==========================================================
        // 3. GÜRÜLTÜ TEMİZLEME: DİNAMİK MEDYAN (ORTANCA) FİLTRESİ
        // ==========================================================
        public static Bitmap ApplyMedianFilter(Bitmap original, int matrixSize)
        {
            Bitmap source = Get32BppImage(original);
            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* res = (byte*)resData.Scan0;
                int stride = srcData.Stride;
                int width = source.Width;
                int height = source.Height;

                int offset = matrixSize / 2;
                int maxNeighbors = matrixSize * matrixSize;

                // Belleği yormamak için dizileri döngü dışında bir kez oluşturuyoruz
                byte[] neighborsB = new byte[maxNeighbors];
                byte[] neighborsG = new byte[maxNeighbors];
                byte[] neighborsR = new byte[maxNeighbors];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int n = 0;

                        for (int ky = -offset; ky <= offset; ky++)
                        {
                            int ny = y + ky;
                            if (ny < 0 || ny >= height) continue;

                            for (int kx = -offset; kx <= offset; kx++)
                            {
                                int nx = x + kx;
                                if (nx < 0 || nx >= width) continue;

                                int idx = (ny * stride) + (nx * 4);
                                neighborsB[n] = src[idx];
                                neighborsG[n] = src[idx + 1];
                                neighborsR[n] = src[idx + 2];
                                n++;
                            }
                        }

                        // Sadece geçerli piksel sayısı kadar olan kısmı sıralıyoruz
                        Array.Sort(neighborsB, 0, n);
                        Array.Sort(neighborsG, 0, n);
                        Array.Sort(neighborsR, 0, n);

                        int medianIndex = n / 2;

                        int resIdx = (y * stride) + (x * 4);
                        res[resIdx] = neighborsB[medianIndex];
                        res[resIdx + 1] = neighborsG[medianIndex];
                        res[resIdx + 2] = neighborsR[medianIndex];
                        res[resIdx + 3] = 255;
                    }
                }
            }

            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

        // ==========================================================
        // 4. KENAR BULMA (PREWITT OPERATÖRÜ)
        // ==========================================================
        public static Bitmap ApplyPrewitt(Bitmap original, string direction)
        {
            Bitmap source = Get32BppImage(original);
            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* res = (byte*)resData.Scan0;
                int stride = srcData.Stride;
                int width = source.Width;
                int height = source.Height;

                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        // 3x3 grid koordinatları hesaplaması
                        int idx00 = ((y - 1) * stride) + ((x - 1) * 4);
                        int idx10 = ((y - 1) * stride) + (x * 4);
                        int idx20 = ((y - 1) * stride) + ((x + 1) * 4);

                        int idx01 = (y * stride) + ((x - 1) * 4);
                        int idx21 = (y * stride) + ((x + 1) * 4);

                        int idx02 = ((y + 1) * stride) + ((x - 1) * 4);
                        int idx12 = ((y + 1) * stride) + (x * 4);
                        int idx22 = ((y + 1) * stride) + ((x + 1) * 4);

                        // Piksellerin gri tonlarını (Luminance) hesaplıyoruz
                        int g00 = (src[idx00] + src[idx00 + 1] + src[idx00 + 2]) / 3;
                        int g10 = (src[idx10] + src[idx10 + 1] + src[idx10 + 2]) / 3;
                        int g20 = (src[idx20] + src[idx20 + 1] + src[idx20 + 2]) / 3;

                        int g01 = (src[idx01] + src[idx01 + 1] + src[idx01 + 2]) / 3;
                        int g21 = (src[idx21] + src[idx21 + 1] + src[idx21 + 2]) / 3;

                        int g02 = (src[idx02] + src[idx02 + 1] + src[idx02 + 2]) / 3;
                        int g12 = (src[idx12] + src[idx12 + 1] + src[idx12 + 2]) / 3;
                        int g22 = (src[idx22] + src[idx22 + 1] + src[idx22 + 2]) / 3;

                        // Prewitt Gx (Yatay Kenarlar)
                        int gx = (-1 * g00) + (1 * g20) +
                                 (-1 * g01) + (1 * g21) +
                                 (-1 * g02) + (1 * g22);

                        // Prewitt Gy (Dikey Kenarlar)
                        int gy = (1 * g00) + (1 * g10) + (1 * g20) +
                                 (-1 * g02) + (-1 * g12) + (-1 * g22);

                        int magnitude = 0;

                        if (direction == "Yatay (Horizontal)")
                            magnitude = Math.Abs(gx);
                        else if (direction == "Dikey (Vertical)")
                            magnitude = Math.Abs(gy);
                        else
                            magnitude = (int)Math.Sqrt((gx * gx) + (gy * gy)); // Her İkisi (Genlik)

                        if (magnitude > 255) magnitude = 255;
                        if (magnitude < 0) magnitude = 0;

                        int resIdx = (y * stride) + (x * 4);
                        res[resIdx] = (byte)magnitude;     // B
                        res[resIdx + 1] = (byte)magnitude; // G
                        res[resIdx + 2] = (byte)magnitude; // R
                        res[resIdx + 3] = 255;             // A
                    }
                }
            }

            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

        // ==========================================================
        // 5. STATİK EŞİKLEME (SABİT DEĞER)
        // ==========================================================
        public static Bitmap ApplyStaticThreshold(Bitmap original, int threshold)
        {
            Bitmap result = Get32BppImage(original);
            BitmapData data = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int bytes = Math.Abs(data.Stride) * result.Height;

                for (int i = 0; i < bytes; i += 4)
                {
                    int gray = (ptr[i] + ptr[i + 1] + ptr[i + 2]) / 3;
                    byte val = (byte)(gray >= threshold ? 255 : 0);
                    ptr[i] = val;
                    ptr[i + 1] = val;
                    ptr[i + 2] = val;
                }
            }

            result.UnlockBits(data);
            return result;
        }

        // ==========================================================
        // 6. DİNAMİK (ADAPTİF) EŞİKLEME (BÖLGESEL ORTALAMA)
        // ==========================================================
        public static Bitmap ApplyDynamicThreshold(Bitmap original, int matrixSize)
        {
            Bitmap source = Get32BppImage(original);
            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* res = (byte*)resData.Scan0;
                int stride = srcData.Stride;
                int width = source.Width;
                int height = source.Height;

                int offset = matrixSize / 2;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int sum = 0;
                        int count = 0;

                        // Seçilen matris boyutu kadar etraftaki piksellerin ortalamasını al
                        for (int ky = -offset; ky <= offset; ky++)
                        {
                            int ny = y + ky;
                            if (ny < 0 || ny >= height) continue;

                            for (int kx = -offset; kx <= offset; kx++)
                            {
                                int nx = x + kx;
                                if (nx < 0 || nx >= width) continue;

                                int idx = (ny * stride) + (nx * 4);
                                sum += (src[idx] + src[idx + 1] + src[idx + 2]) / 3;
                                count++;
                            }
                        }

                        int localMean = sum / count;

                        // Merkez pikseli kontrol et
                        int centerIdx = (y * stride) + (x * 4);
                        int centerGray = (src[centerIdx] + src[centerIdx + 1] + src[centerIdx + 2]) / 3;

                        // Bölgesel ortalamadan büyükse beyaz, küçükse siyah
                        byte val = (byte)(centerGray >= localMean ? 255 : 0);

                        res[centerIdx] = val;
                        res[centerIdx + 1] = val;
                        res[centerIdx + 2] = val;
                        res[centerIdx + 3] = 255;
                    }
                }
            }

            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }
    }
}