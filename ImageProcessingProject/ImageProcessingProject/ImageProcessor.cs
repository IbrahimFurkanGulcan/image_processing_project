using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessingProject
{
    public static class ImageProcessor
    {
        private static Bitmap Get32BppImage(Bitmap original)
        {
            Bitmap bmp = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
            bmp.SetResolution(original.HorizontalResolution, original.VerticalResolution);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(original,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    new Rectangle(0, 0, original.Width, original.Height),
                    GraphicsUnit.Pixel);
            }
            return bmp;
        }

        public static Bitmap ConvertToGrayscale(Bitmap original)
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
                for (int y = 0; y < source.Height; y++)
                {
                    for (int x = 0; x < source.Width; x++)
                    {
                        int idx = (y * stride) + (x * 4);
                        byte gray = (byte)(0.299 * src[idx + 2] + 0.587 * src[idx + 1] + 0.114 * src[idx]);
                        res[idx] = gray; res[idx + 1] = gray; res[idx + 2] = gray; res[idx + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

        public static Bitmap ConvertToBinary(Bitmap original, int threshold)
        {
            Bitmap gray = ConvertToGrayscale(original);
            Bitmap source = Get32BppImage(gray);
            Bitmap result = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* res = (byte*)resData.Scan0;
                int stride = srcData.Stride;
                for (int y = 0; y < source.Height; y++)
                {
                    for (int x = 0; x < source.Width; x++)
                    {
                        int idx = (y * stride) + (x * 4);
                        byte val = src[idx] > threshold ? (byte)255 : (byte)0;
                        res[idx] = val; res[idx + 1] = val; res[idx + 2] = val; res[idx + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

        public static Bitmap CropImage(Bitmap original, int x1, int y1, int x2, int y2)
        {
            Bitmap source = Get32BppImage(original);
            x1 = Math.Max(0, x1); y1 = Math.Max(0, y1);
            x2 = Math.Min(source.Width, x2); y2 = Math.Min(source.Height, y2);
            int cropW = x2 - x1, cropH = y2 - y1;
            if (cropW <= 0 || cropH <= 0) return source;
            Bitmap result = new Bitmap(cropW, cropH, PixelFormat.Format32bppArgb);
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* src = (byte*)srcData.Scan0;
                byte* res = (byte*)resData.Scan0;
                int srcStride = srcData.Stride, resStride = resData.Stride;
                for (int y = 0; y < cropH; y++)
                {
                    for (int x = 0; x < cropW; x++)
                    {
                        int si = ((y + y1) * srcStride) + ((x + x1) * 4);
                        int ri = (y * resStride) + (x * 4);
                        res[ri] = src[si]; res[ri + 1] = src[si + 1]; res[ri + 2] = src[si + 2]; res[ri + 3] = src[si + 3];
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

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
                    byte noiseValue;
                    if (noiseType == "Salt") noiseValue = 255;
                    else if (noiseType == "Pepper") noiseValue = 0;
                    else noiseValue = (byte)(rnd.Next(0, 2) == 0 ? 0 : 255);
                    currentLine[xOffset] = noiseValue;
                    currentLine[xOffset + 1] = noiseValue;
                    currentLine[xOffset + 2] = noiseValue;
                }
            }
            result.UnlockBits(data);
            return result;
        }

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
                int stride = srcData.Stride, width = source.Width, height = source.Height;
                int offset = matrixSize / 2;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int sumB = 0, sumG = 0, sumR = 0, count = 0;
                        for (int ky = -offset; ky <= offset; ky++)
                        {
                            int ny = y + ky;
                            if (ny < 0 || ny >= height) continue;
                            for (int kx = -offset; kx <= offset; kx++)
                            {
                                int nx = x + kx;
                                if (nx < 0 || nx >= width) continue;
                                int idx = (ny * stride) + (nx * 4);
                                sumB += src[idx]; sumG += src[idx + 1]; sumR += src[idx + 2]; count++;
                            }
                        }
                        int ri = (y * stride) + (x * 4);
                        res[ri] = (byte)(sumB / count); res[ri + 1] = (byte)(sumG / count); res[ri + 2] = (byte)(sumR / count); res[ri + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

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
                int stride = srcData.Stride, width = source.Width, height = source.Height;
                int offset = matrixSize / 2, maxN = matrixSize * matrixSize;
                byte[] nB = new byte[maxN], nG = new byte[maxN], nR = new byte[maxN];
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
                                nB[n] = src[idx]; nG[n] = src[idx + 1]; nR[n] = src[idx + 2]; n++;
                            }
                        }
                        Array.Sort(nB, 0, n); Array.Sort(nG, 0, n); Array.Sort(nR, 0, n);
                        int mi = n / 2;
                        int ri = (y * stride) + (x * 4);
                        res[ri] = nB[mi]; res[ri + 1] = nG[mi]; res[ri + 2] = nR[mi]; res[ri + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

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
                int stride = srcData.Stride, width = source.Width, height = source.Height;
                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int idx00 = ((y - 1) * stride) + ((x - 1) * 4), idx10 = ((y - 1) * stride) + (x * 4), idx20 = ((y - 1) * stride) + ((x + 1) * 4);
                        int idx01 = (y * stride) + ((x - 1) * 4), idx21 = (y * stride) + ((x + 1) * 4);
                        int idx02 = ((y + 1) * stride) + ((x - 1) * 4), idx12 = ((y + 1) * stride) + (x * 4), idx22 = ((y + 1) * stride) + ((x + 1) * 4);
                        int g00 = (src[idx00] + src[idx00 + 1] + src[idx00 + 2]) / 3, g10 = (src[idx10] + src[idx10 + 1] + src[idx10 + 2]) / 3, g20 = (src[idx20] + src[idx20 + 1] + src[idx20 + 2]) / 3;
                        int g01 = (src[idx01] + src[idx01 + 1] + src[idx01 + 2]) / 3, g21 = (src[idx21] + src[idx21 + 1] + src[idx21 + 2]) / 3;
                        int g02 = (src[idx02] + src[idx02 + 1] + src[idx02 + 2]) / 3, g12 = (src[idx12] + src[idx12 + 1] + src[idx12 + 2]) / 3, g22 = (src[idx22] + src[idx22 + 1] + src[idx22 + 2]) / 3;
                        int gx = (-g00) + (g20) + (-g01) + (g21) + (-g02) + (g22);
                        int gy = (g00) + (g10) + (g20) + (-g02) + (-g12) + (-g22);
                        int mag = direction == "Yatay (Horizontal)" ? Math.Abs(gx) : direction == "Dikey (Vertical)" ? Math.Abs(gy) : (int)Math.Sqrt(gx * gx + gy * gy);
                        mag = Math.Min(255, Math.Max(0, mag));
                        int ri = (y * stride) + (x * 4);
                        res[ri] = (byte)mag; res[ri + 1] = (byte)mag; res[ri + 2] = (byte)mag; res[ri + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }

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
                    ptr[i] = val; ptr[i + 1] = val; ptr[i + 2] = val;
                }
            }
            result.UnlockBits(data);
            return result;
        }

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
                int stride = srcData.Stride, width = source.Width, height = source.Height;
                int offset = matrixSize / 2;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int sum = 0, count = 0;
                        for (int ky = -offset; ky <= offset; ky++)
                        {
                            int ny = y + ky; if (ny < 0 || ny >= height) continue;
                            for (int kx = -offset; kx <= offset; kx++)
                            {
                                int nx = x + kx; if (nx < 0 || nx >= width) continue;
                                int idx = (ny * stride) + (nx * 4);
                                sum += (src[idx] + src[idx + 1] + src[idx + 2]) / 3; count++;
                            }
                        }
                        int localMean = sum / count;
                        int ci = (y * stride) + (x * 4);
                        int cg = (src[ci] + src[ci + 1] + src[ci + 2]) / 3;
                        byte val = (byte)(cg >= localMean ? 255 : 0);
                        res[ci] = val; res[ci + 1] = val; res[ci + 2] = val; res[ci + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            result.UnlockBits(resData);
            return result;
        }
    }
}