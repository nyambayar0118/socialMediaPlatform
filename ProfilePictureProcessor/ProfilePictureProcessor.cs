using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ProfilePictureProcessor;

public class ProfilePictureProcessor
{
    public const int MinimumSize = 512;
    public const int TargetSize = 512;

    public static ProcessResult Process(string inputPath, string outputPath) 
    { 
        if(!File.Exists(inputPath))
        {
            return ProcessResult.Fail($"Файл олдсонгүй: {inputPath}");
        }

        try
        {
            using var image = Image.FromFile(inputPath);
            return ProcessImage(image, outputPath);
        }
        catch (Exception e)
        {
            return ProcessResult.Fail($"Зураг унших үед алдаа гарлаа: {e.Message}");
        }
    }
    public static ProcessResult ProcessImage(Image image, string outputPath)
    {
        if (image.Width < MinimumSize || image.Height < MinimumSize)
        {
            return ProcessResult.Fail($"Зураг хамгийн багадаа {MinimumSize}x{MinimumSize} хэмжээтэй байх ёстой.");
        }

        using var croppedImg = CropToSquare(image);
        using var resizedImg = croppedImg.Width > TargetSize ? Resize(croppedImg, TargetSize, TargetSize) : new Bitmap(croppedImg);

        try
        {
            var directory = Path.GetDirectoryName(outputPath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            SaveImage(resizedImg, outputPath);
            return ProcessResult.Ok(outputPath);
        }
        catch (Exception e)
        {
            return ProcessResult.Fail($"Зураг хадгалах үед алдаа гарлаа: {e.Message}");
        }
    }

    public static void SaveImage(Bitmap bmp, string outputPath)
    {
        bmp.Save(outputPath, ImageFormat.Png);
    }
    public static Bitmap CropToSquare(Image image)
    {
        int size = Math.Min(image.Width, image.Height);
        int x = (image.Width - size) / 2;
        int y = (image.Height - size) / 2;

        var bmp = new Bitmap(size, size);
        using var graphic = Graphics.FromImage(bmp);
        graphic.DrawImage(image, new Rectangle(0, 0, size, size), new Rectangle(x, y, size, size), GraphicsUnit.Pixel);
        return bmp;
    }
    public static Bitmap Resize(Image image, int width, int height) 
    {
        var bmp = new Bitmap(width, height);
        using var graphic = Graphics.FromImage(bmp);

        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphic.DrawImage(image, 0, 0, width, height);

        return bmp;
    }
}
