public static class AvatarHelper
{
    public static PictureBox Create(string? picturePath, string username, int size)
    {
        var pb = new PictureBox
        {
            Size = new Size(size, size),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        pb.Image = !string.IsNullOrEmpty(picturePath) && File.Exists(picturePath)
            ? MakeCircle(Image.FromFile(picturePath), size)
            : MakeInitialsCircle(username, size);

        return pb;
    }

    private static Bitmap MakeCircle(Image source, int size)
    {
        var bmp = new Bitmap(size, size);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // clip to circle before drawing the image
        using var path = new System.Drawing.Drawing2D.GraphicsPath();
        path.AddEllipse(0, 0, size, size);
        g.SetClip(path);

        g.DrawImage(source, 0, 0, size, size);
        return bmp;
    }

    private static Bitmap MakeInitialsCircle(string username, int size)
    {
        string initials = username.Length >= 2
            ? username.Substring(0, 2).ToUpper()
            : username.ToUpper();

        var bmp = new Bitmap(size, size);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.FillEllipse(new SolidBrush(Color.FromArgb(0xB5, 0xD4, 0xF4)), 0, 0, size, size);

        using var font = new Font("Segoe UI", size * 0.25f, FontStyle.Bold);
        var textSize = g.MeasureString(initials, font);
        g.DrawString(initials, font, Brushes.White,
            (size - textSize.Width) / 2,
            (size - textSize.Height) / 2);

        return bmp;
    }
}