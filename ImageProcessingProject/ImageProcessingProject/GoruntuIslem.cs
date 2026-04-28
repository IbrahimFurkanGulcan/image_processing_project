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
}