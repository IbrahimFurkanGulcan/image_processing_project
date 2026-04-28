using System;
using System.Drawing; // Eğer altı kırmızı çizilirse NuGet'ten System.Drawing.Common yükle

public class GoruntuIslem
{
    // --- 1. ARİTMETİK İŞLEM: TOPLAMA ---
    public Bitmap ResimTopla(Bitmap resim1, Bitmap resim2)
    {
        // Resimlerin boyutlarını kontrol ediyoruz
        if (resim1.Width != resim2.Width || resim1.Height != resim2.Height)
        {
            return null;
        }

        Bitmap sonuc = new Bitmap(resim1.Width, resim1.Height);

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
    public Bitmap ResimBol(Bitmap resim1, Bitmap resim2)
    {
        if (resim1.Width != resim2.Width || resim1.Height != resim2.Height)
        {
            return null;
        }

        Bitmap sonuc = new Bitmap(resim1.Width, resim1.Height);

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
    // --- 3. MORFOLOJİK İŞLEM: GENİŞLEME (DILATION) ---
    public Bitmap Genisleme(Bitmap kaynakResim)
    {
        Bitmap sonuc = new Bitmap(kaynakResim.Width, kaynakResim.Height);

        // Kenarlarda hata almamak için 1. pikselden başlıyoruz
        for (int x = 1; x < kaynakResim.Width - 1; x++)
        {
            for (int y = 1; y < kaynakResim.Height - 1; y++)
            {
                byte maxR = 0, maxG = 0, maxB = 0;

                // 3x3 pencere içinde geziniyoruz (Komşuları kontrol et)
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        Color komsuRenk = kaynakResim.GetPixel(x + i, y + j);

                        // En parlak değeri bul
                        if (komsuRenk.R > maxR) maxR = komsuRenk.R;
                        if (komsuRenk.G > maxG) maxG = komsuRenk.G;
                        if (komsuRenk.B > maxB) maxB = komsuRenk.B;
                    }
                }
                sonuc.SetPixel(x, y, Color.FromArgb(maxR, maxG, maxB));
            }
        }
        return sonuc;
    }

    // --- 4. MORFOLOJİK İŞLEM: AŞINMA (EROSION) ---
    public Bitmap Asinma(Bitmap kaynakResim)
    {
        Bitmap sonuc = new Bitmap(kaynakResim.Width, kaynakResim.Height);

        for (int x = 1; x < kaynakResim.Width - 1; x++)
        {
            for (int y = 1; y < kaynakResim.Height - 1; y++)
            {
                byte minR = 255, minG = 255, minB = 255;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        Color komsuRenk = kaynakResim.GetPixel(x + i, y + j);

                        // En karanlık değeri bul
                        if (komsuRenk.R < minR) minR = komsuRenk.R;
                        if (komsuRenk.G < minG) minG = komsuRenk.G;
                        if (komsuRenk.B < minB) minB = komsuRenk.B;
                    }
                }
                sonuc.SetPixel(x, y, Color.FromArgb(minR, minG, minB));
            }
        }
        return sonuc;
    }
    // Açma (Opening): Önce Aşınma -> Sonra Genişleme
    public Bitmap Acma(Bitmap kaynakResim)
    {
        Bitmap geciciResim = Asinma(kaynakResim);
        return Genisleme(geciciResim);
    }

    // Kapama (Closing): Önce Genişleme -> Sonra Aşınma
    public Bitmap Kapama(Bitmap kaynakResim)
    {
        Bitmap geciciResim = Genisleme(kaynakResim);
        return Asinma(geciciResim);
    }
}