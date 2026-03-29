using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace ProfilePictureProcessor;

/// <summary>
/// Зураг боловсруулах класс.
/// </summary>
public class ProfilePictureProcessor
{
    /// <summary>
    /// Зургийн хамгийн бага хэмжээ.
    /// </summary>
    public const int MinimumSize = 512;
    /// <summary>
    /// Зургийн байх ёстой хэмжээ.
    /// </summary>
    public const int TargetSize = 512;

    /// <summary>
    /// Зургийг боловсруулах метод-ийн wrapper метод.
    /// </summary>
    /// <param name="inputPath"> Боловсруулах зургийн зам. </param>
    /// <param name="outputPath"> Боловсруулсан зургийг хадгалах зам. </param>
    /// <returns> Үр дүнг илэрхийлэх record. </returns>
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
    /// <summary>
    /// Зургийг боловсруулах үндсэн метод. Оруулсан зургийг TargetSize хэмжээтэй квадрат зураг болгож хадгална.
    /// </summary>
    /// <param name="image"> Боловсруулах зургийн объект. </param>
    /// <param name="outputPath"> Боловсруулсан зургийг хадгалах зам. </param>
    /// <returns> Үр дүнг илэрхийлэх record. </returns>
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
    /// <summary>
    /// Боловсруулсан зургийг хадгалах метод. Зургийг PNG форматаар хадгална.
    /// </summary>
    /// <param name="bmp"> Хадгалах зургийн объект. </param>
    /// <param name="outputPath"> Боловсруулсан зургийг хадгалах зам. </param>
    public static void SaveImage(Bitmap bmp, string outputPath)
    {
        bmp.Save(outputPath, ImageFormat.Png);
    }
    /// <summary>
    /// Зургийг квадрат хэлбэрт оруулах метод.
    /// </summary>
    /// <param name="image"> Хэлбэрийг нь өөрчлөх зургийн объект. </param>
    /// <returns> Квадрат хэлбэртэй болгосон зургийн объект. </returns>
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
    /// <summary>
    /// Зургийг заасан хэмжээтэй болгох метод.
    /// </summary>
    /// <param name="image"> Хэмжээг өөрчлөх зургийн объект. </param>
    /// <param name="width"> Шинэ зургийн урт. </param>
    /// <param name="height"> Шинэ зургийн өндөр. </param>
    /// <returns> Хэмжээг нь өөрчилсөн зургийн объект. </returns>
    public static Bitmap Resize(Image image, int width, int height) 
    {
        var bmp = new Bitmap(width, height);
        using var graphic = Graphics.FromImage(bmp);

        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphic.DrawImage(image, 0, 0, width, height);

        return bmp;
    }
}
