using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfilePictureProcessor;
using System;
using System.Drawing;
using System.IO;

namespace ProfilePictureProcessor.Tests
{
    /// <summary>
    /// Профайл зургийг боловсруулах сангийн нэгж тестүүд.
    /// 100% code coverage буюу кодыг 100% хамруулахыг хичээв.
    /// </summary>
    [TestClass]
    public class ProfilePictureProcessorTests
    {
        private string _testDirectory;
        private const string TestImageFormat = "png";

        [TestInitialize]
        public void Setup()
        {
            _testDirectory = Path.Combine(Path.GetTempPath(), "ProfilePictureProcessorTests");

            if (!Directory.Exists(_testDirectory))
            {
                Directory.CreateDirectory(_testDirectory);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                if (Directory.Exists(_testDirectory))
                {
                    foreach (var file in Directory.GetFiles(_testDirectory))
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(_testDirectory);
                }
            }
            catch { }
        }

        #region Helper Methods

        /// <summary>
        /// Заасан хэмжээтэй тест зураг үүсгэх метод.
        /// </summary>
        private string CreateTestImage(int width, int height, string filename = null)
        {
            filename = filename ?? $"test_{width}x{height}.png";
            string filePath = Path.Combine(_testDirectory, filename);

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Blue);
            }
            bitmap.Save(filePath);
            bitmap.Dispose();

            return filePath;
        }

        /// <summary>
        /// Санах ойд Bitmap тест зураг үүсгэх метод.
        /// </summary>
        private Bitmap CreateTestBitmap(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Green);
            }
            return bitmap;
        }

        /// <summary>
        /// Үлдсэн тестийн зургуудыг цэвэрлэх метод.
        /// </summary>
        private void CleanupTestImages()
        {
            try
            {
                foreach (var file in Directory.GetFiles(_testDirectory))
                {
                    File.Delete(file);
                }
            }
            catch { }
        }

        #endregion

        #region Process Method - File Tests

        [TestMethod]
        [Description("Файл байхгүй үед зураг боловсруулахыг оролдох тест")]
        public void Process_WithNonexistentFile_ReturnsFail()
        {
            var result = ProfilePictureProcessor.Process(
                "/nonexistent/path/image.png",
                Path.Combine(_testDirectory, "output.png"));

            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.ErrorMessage);
            Assert.IsTrue(result.ErrorMessage.Contains("Файл олдсонгүй"));
            Assert.IsNull(result.OutputPath);
        }

        [TestMethod]
        [Description("Анх ажиллуулахад зургийг хадгалах хавтас үүсгэх тест")]
        public void Process_CreatesDirectoryIfNotExists()
        {
            string inputPath = CreateTestImage(512, 512);
            string nestedDirectory = Path.Combine(_testDirectory, "nested", "output");
            string outputPath = Path.Combine(nestedDirectory, "result.png");

            var result = ProfilePictureProcessor.Process(inputPath, outputPath);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(Directory.Exists(nestedDirectory));
            Assert.IsTrue(File.Exists(outputPath));
        }

        [TestMethod]
        [Description("Файлыг амжилттай боловсруулах тест")]
        public void Process_WithValidFile_Succeeds()
        {
            string inputPath = CreateTestImage(512, 512);
            string outputPath = Path.Combine(_testDirectory, "output.png");

            var result = ProfilePictureProcessor.Process(inputPath, outputPath);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.OutputPath);
            Assert.IsTrue(File.Exists(result.OutputPath));
        }

        #endregion

        #region ProcessImage Method - Dimension Validation

        [TestMethod]
        [DataRow(128, 512, "Хэт жижиг болон буруу харьцаа")]
        [DataRow(100, 200, "Хамгийн бага өндөр нь 512 байх ёстой")]
        [DataRow(256, 256, "Квадрат боловч хэт жижиг")]
        [DataRow(511, 511, "Нэг пиксел нь хангалтгүй")]
        [DataRow(512, 100, "Өндөр хангалтгүй")]
        [DataRow(100, 512, "Урт хангалтгүй")]
        [Description("Хэт жижиг хэмжээтэй зураг дээрх боловсруулалт амжилтгүй болох тест")]
        public void ProcessImage_WithTooSmallDimensions_ReturnsFail(int width, int height, string scenario)
        {
            string inputPath = CreateTestImage(width, height, $"small_{width}x{height}.png");
            string outputPath = Path.Combine(_testDirectory, "output.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsFalse(result.Success, $"Хүн нэмэлтийн төлөв: {scenario}");
            Assert.IsTrue(result.ErrorMessage.Contains("хамгийн багадаа"));
            Assert.IsNull(result.OutputPath);
        }

        [TestMethod]
        [Description("Хамгийн бага заасан хэмжээтэй зургийг амжилттай боловсруулах тест")]
        public void ProcessImage_WithExactMinimumDimensions_Succeeds()
        {
            string inputPath = CreateTestImage(512, 512);
            string outputPath = Path.Combine(_testDirectory, "output_min.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.OutputPath);
            Assert.IsTrue(File.Exists(result.OutputPath));

            // Үр дүнгийн зургийн хэмжээг шалгах
            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        #endregion

        #region ProcessImage Method - Aspect Ratio Cropping

        [TestMethod]
        [DataRow(512, 768, "Өндөр нь илүүтэй - дээр доороос тайрах")]
        [DataRow(768, 512, "Урт нь илүүтэй - хоёр талаас тайрах")]
        [DataRow(1024, 768, "4:3 харьцаа")]
        [DataRow(600, 400, "3:2 харьцаа")]
        [DataRow(800, 600, "4:3 харьцаа")]
        [Description("Янз бүрийн харьцаатай зургийг дөрвөлжин болгож тайрах тест")]
        public void ProcessImage_WithWrongAspectRatio_CropsToSquare(int width, int height, string scenario)
        {
            string inputPath = CreateTestImage(width, height, $"aspect_{width}x{height}.png");
            string outputPath = Path.Combine(_testDirectory, $"cropped_{width}x{height}.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success, $"Алдаа төлөв: {scenario}");
            Assert.IsTrue(File.Exists(result.OutputPath));

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                // Дүнгийн зураг квадрат байх ёстой
                Assert.AreEqual(resultImage.Width, resultImage.Height);
                // Хэмжээ хамгийн ихдээ 512х512 байх ёстой
                Assert.IsTrue(resultImage.Width <= 512);
            }
        }

        [TestMethod]
        [Description("Заасан 512x768 утгаас 512x512 руу тайрах тест")]
        public void ProcessImage_512x768_CropsTo512x512()
        {
            string inputPath = CreateTestImage(512, 768);
            string outputPath = Path.Combine(_testDirectory, "output_512x768.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        [TestMethod]
        [Description("768x512 хэмжээтэй зургийг дөрвөлжин болгож тайрах тест")]
        public void ProcessImage_768x512_CropsToSquare()
        {
            string inputPath = CreateTestImage(768, 512);
            string outputPath = Path.Combine(_testDirectory, "output_768x512.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        #endregion

        #region ProcessImage Method - Resize Tests

        [TestMethod]
        [DataRow(1024, 1024, "Квадрат боловч хэт том")]
        [DataRow(2048, 2048, "Маш том квадрат")]
        [Description("Хэт том хэмжээтэй зургуудийг тайрч жижиг болгох тест")]
        public void ProcessImage_WithOversizedSquareImage_Resizes(int width, int height, string scenario)
        {
            string inputPath = CreateTestImage(width, height, $"large_{width}x{height}.png");
            string outputPath = Path.Combine(_testDirectory, $"resized_{width}x{height}.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success, $"Алдаа төлөв: {scenario}");
            Assert.IsTrue(File.Exists(result.OutputPath));

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        [TestMethod]
        [Description("4096x6000 хэмжээтэй зургийг тайрч 512x512 болгох тест")]
        public void ProcessImage_4096x6000_ResizesTo512x512()
        {
            string inputPath = CreateTestImage(4096, 6000);
            string outputPath = Path.Combine(_testDirectory, "output_4096x6000.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(File.Exists(result.OutputPath));

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        #endregion

        #region ProcessImage Method - Combined Scenarios

        [TestMethod]
        [Description("2048x4096 зургийг тайрч resize хийх тест")]
        public void ProcessImage_TallRectangleOversized_CropsThenResizes()
        {
            string inputPath = CreateTestImage(2048, 4096);
            string outputPath = Path.Combine(_testDirectory, "output_2048x4096.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        [TestMethod]
        [Description("4096x2048 зургийг тайрч resize хийх тест")]
        public void ProcessImage_WideRectangleOversized_CropsThenResizes()
        {
            string inputPath = CreateTestImage(4096, 2048);
            string outputPath = Path.Combine(_testDirectory, "output_4096x2048.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        [TestMethod]
        [Description("8192x8192 зургийг resize хийх тест")]
        public void ProcessImage_VeryLargeSquareImage_Resizes()
        {
            string inputPath = CreateTestImage(8192, 8192);
            string outputPath = Path.Combine(_testDirectory, "output_8192x8192.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        #endregion

        #region ProcessImage - Bitmap Object Tests

        [TestMethod]
        [Description("Bitmap объектыг процесс хийх тест")]
        public void ProcessImage_WithBitmapObject_Succeeds()
        {
            Bitmap bitmap = CreateTestBitmap(512, 512);
            string outputPath = Path.Combine(_testDirectory, "output_bitmap.png");

            var result = ProfilePictureProcessor.ProcessImage(bitmap, outputPath);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(File.Exists(result.OutputPath));

            bitmap.Dispose();
        }

        [TestMethod]
        [Description("Bitmap объектийн хэмжээ хангалтгүй бол алдаа буцаах тест")]
        public void ProcessImage_WithBitmapObject_TooSmall_ReturnsFail()
        {
            Bitmap bitmap = CreateTestBitmap(256, 256);
            string outputPath = Path.Combine(_testDirectory, "output_bitmap_small.png");

            var result = ProfilePictureProcessor.ProcessImage(bitmap, outputPath);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.OutputPath);

            bitmap.Dispose();
        }

        [TestMethod]
        [Description("Bitmap объект нь буруу харьцаатай бол тайрах тест")]
        public void ProcessImage_WithBitmapObject_WrongAspectRatio_Crops()
        {
            Bitmap bitmap = CreateTestBitmap(512, 768);
            string outputPath = Path.Combine(_testDirectory, "output_bitmap_crop.png");

            var result = ProfilePictureProcessor.ProcessImage(bitmap, outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }

            bitmap.Dispose();
        }

        #endregion

        #region CropToSquare Method Tests

        [TestMethod]
        [DataRow(512, 768, "Дээр доор урт")]
        [DataRow(768, 512, "Зүүн баруун урт")]
        [DataRow(1024, 512, "Хоёр дахь урт")]
        [Description("Квадрат болгож тайрах функцийн тест")]
        public void CropToSquare_WithRectangle_ReturnsSquare(int width, int height, string scenario)
        {
            Bitmap rectangle = CreateTestBitmap(width, height);

            Bitmap square = ProfilePictureProcessor.CropToSquare(rectangle);

            Assert.AreEqual(square.Width, square.Height, $"Алдаа төлөв: {scenario}");
            Assert.AreEqual(Math.Min(width, height), square.Width);

            rectangle.Dispose();
            square.Dispose();
        }

        [TestMethod]
        [Description("Аль хэдийн квадрат зургийг квадрат болгох тест")]
        public void CropToSquare_WithSquareImage_ReturnsSameSize()
        {
            Bitmap square = CreateTestBitmap(512, 512);

            Bitmap result = ProfilePictureProcessor.CropToSquare(square);

            Assert.AreEqual(512, result.Width);
            Assert.AreEqual(512, result.Height);

            square.Dispose();
            result.Dispose();
        }

        [TestMethod]
        [Description("Том тэгш өнцөг зургийг квадрат болгож тайрах тест")]
        public void CropToSquare_WithVeryLargeRectangle_CropsToSquare()
        {
            Bitmap large = CreateTestBitmap(4096, 6000);

            Bitmap square = ProfilePictureProcessor.CropToSquare(large);

            Assert.AreEqual(square.Width, square.Height);
            Assert.AreEqual(4096, square.Width);

            large.Dispose();
            square.Dispose();
        }

        #endregion

        #region Resize Method Tests

        [TestMethod]
        [DataRow(1024, 1024, 512, 512)]
        [DataRow(2048, 2048, 512, 512)]
        [DataRow(1024, 2048, 512, 512)]
        [Description("Зургийг resize хийх тест")]
        public void Resize_WithLargerImage_Resizes(int width, int height, int targetWidth, int targetHeight)
        {
            Bitmap original = CreateTestBitmap(width, height);

            Bitmap resized = ProfilePictureProcessor.Resize(
                original,
                targetWidth,
                targetHeight);

            Assert.AreEqual(targetWidth, resized.Width);
            Assert.AreEqual(targetHeight, resized.Height);

            original.Dispose();
            resized.Dispose();
        }

        [TestMethod]
        [Description("512x512 хэмжээтэй зургийг 512x512 болгох тест")]
        public void Resize_To512x512_CreatesCorrectSize()
        {
            Bitmap original = CreateTestBitmap(1024, 1024);

            Bitmap resized = ProfilePictureProcessor.Resize(
                original,
                512,
                512);

            Assert.AreEqual(512, resized.Width);
            Assert.AreEqual(512, resized.Height);

            original.Dispose();
            resized.Dispose();
        }

        [TestMethod]
        [Description("Янз бүрийн хэмжээнээс resize хийх тест")]
        public void Resize_WithVariousDimensions_Works()
        {
            Bitmap original = CreateTestBitmap(2048, 2048);

            Bitmap resized = ProfilePictureProcessor.Resize(
                original,
                256,
                256);

            Assert.AreEqual(256, resized.Width);
            Assert.AreEqual(256, resized.Height);

            original.Dispose();
            resized.Dispose();
        }

        #endregion

        #region SaveImage Method Tests

        [TestMethod]
        [Description("Зургийг PNG форматаар хадгалах тест")]
        public void SaveImage_SavesAsPng()
        {
            Bitmap bitmap = CreateTestBitmap(512, 512);
            string outputPath = Path.Combine(_testDirectory, "test_save.png");

            ProfilePictureProcessor.SaveImage(bitmap, outputPath);

            Assert.IsTrue(File.Exists(outputPath));

            // PNG форматаар хадгалагдсанаа шалгах
            using (var savedImage = Image.FromFile(outputPath))
            {
                Assert.AreEqual(512, savedImage.Width);
                Assert.AreEqual(512, savedImage.Height);
            }

            bitmap.Dispose();
        }

        [TestMethod]
        [Description("Байхгүй хавтсад зургийг хадгалах тест")]
        public void SaveImage_WithNestedPath_CreatesFile()
        {
            Bitmap bitmap = CreateTestBitmap(512, 512);
            string nestedPath = Path.Combine(_testDirectory, "nested", "deep", "folder", "image.png");

            // Хавтас өөрөө үүсгэгдэхгүй, энэ нь Process методын ажил
            string directory = Path.GetDirectoryName(nestedPath);
            Directory.CreateDirectory(directory);

            ProfilePictureProcessor.SaveImage(bitmap, nestedPath);

            Assert.IsTrue(File.Exists(nestedPath));

            bitmap.Dispose();
        }

        #endregion

        #region ProcessResult Record Tests

        [TestMethod]
        [Description("Амжилттай үр дүнг үүсгэж байгааг шалгах тест")]
        public void ProcessResult_Ok_CreatesSuccessResult()
        {
            string testPath = "/path/to/output.png";

            var result = ProcessResult.Ok(testPath);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(testPath, result.OutputPath);
            Assert.IsNull(result.ErrorMessage);
        }

        [TestMethod]
        [Description("Амжилтгүй үр дүнг үүсгэж байгааг шалгах тест")]
        public void ProcessResult_Fail_CreatesFailResult()
        {
            string errorMsg = "Test error message";

            var result = ProcessResult.Fail(errorMsg);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(errorMsg, result.ErrorMessage);
            Assert.IsNull(result.OutputPath);
        }

        [TestMethod]
        [Description("Янз бүрийн алдааны мессэжүүд дамжуулах тест")]
        public void ProcessResult_Fail_WithVariousMessages()
        {
            var errors = new[]
            {
                "Зураг олдохгүй байна",
                "Файл чийглээгүй байна",
                "Үл хүлээсэн алдаа",
                ""
            };

            foreach (var error in errors)
            {
                var result = ProcessResult.Fail(error);
                Assert.IsFalse(result.Success);
                Assert.AreEqual(error, result.ErrorMessage);
            }
        }

        #endregion

        #region Edge Cases and Boundary Tests

        [TestMethod]
        [DataRow(513, 512, "Арай илүү өргөн")]
        [DataRow(512, 513, "Арай илүү өндөр")]
        [DataRow(520, 510, "Оршом байршил")]
        [Description("Бараг квадрат хэмжээтэй зургуудыг боловсруулах тест")]
        public void ProcessImage_WithAlmostSquareImage_Accepts(int width, int height, string scenario)
        {
            string inputPath = CreateTestImage(width, height, $"almost_{width}x{height}.png");
            string outputPath = Path.Combine(_testDirectory, $"almost_result_{width}x{height}.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success, $"Алдаа төлөв: {scenario}");
            Assert.IsTrue(File.Exists(result.OutputPath));
        }

        [TestMethod]
        [Description("Маш том хэмжээтэй зургуудыг боловсруулах тест")]
        public void ProcessImage_WithExtremelyLargeDimensions_Processes()
        {
            string inputPath = CreateTestImage(16384, 16384);
            string outputPath = Path.Combine(_testDirectory, "output_16384x16384.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        [TestMethod]
        [Description("Хамгийн богино өргөн болон хамгийн урт хэмжээтэй өндөртэй зураг боловсруулах тест")]
        public void ProcessImage_MinimalWidthMaximalHeight_Crops()
        {
            string inputPath = CreateTestImage(512, 2048);
            string outputPath = Path.Combine(_testDirectory, "output_512x2048.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        [TestMethod]
        [Description("Хамгийн урт өргөн хамгийн бага өндрийн хэмжээтэй зураг боловсруулах тест")]
        public void ProcessImage_MaximalWidthMinimalHeight_Crops()
        {
            string inputPath = CreateTestImage(2048, 512);
            string outputPath = Path.Combine(_testDirectory, "output_2048x512.png");

            var result = ProfilePictureProcessor.ProcessImage(
                Image.FromFile(inputPath),
                outputPath);

            Assert.IsTrue(result.Success);

            using (var resultImage = Image.FromFile(result.OutputPath))
            {
                Assert.AreEqual(512, resultImage.Width);
                Assert.AreEqual(512, resultImage.Height);
            }
        }

        #endregion

        #region Constants Verification

        [TestMethod]
        [Description("Тогтмолуудыг шалгах тест")]
        public void Constants_AreCorrectlyDefined()
        {
            Assert.AreEqual(512, ProfilePictureProcessor.MinimumSize);
            Assert.AreEqual(512, ProfilePictureProcessor.TargetSize);
        }

        #endregion

        #region Error Handling and Exception Tests

        [TestMethod]
        [Description("Буруу форматтай зургийг таниж байгаа эсэхийг шалгах тест")]
        public void Process_WithInvalidImageFile_ReturnsFail()
        {
            string invalidImagePath = Path.Combine(_testDirectory, "invalid.png");

            // Сэтгэл санаанд зургийн файлийг үүсгэх эсэхийн оронд текст файл үүсгэх
            File.WriteAllText(invalidImagePath, "This is not an image");

            string outputPath = Path.Combine(_testDirectory, "output.png");
            var result = ProfilePictureProcessor.Process(invalidImagePath, outputPath);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorMessage.Contains("алдаа") || result.ErrorMessage.Contains("Зураг"));
        }

        [TestMethod]
        [Description("Хадгалах явцад үр дүнг хадгалах зам дээр алдаа гарах тест")]
        public void Process_WithInvalidOutputPath_ReturnsFail()
        {
            string inputPath = CreateTestImage(512, 512);

            // Windows-ийн хориглосон файлын нэр ашиглах
            string invalidOutputPath = "NUL";

            var result = ProfilePictureProcessor.Process(inputPath, invalidOutputPath);

            Assert.IsFalse(result.Success);
        }

        #endregion

        #region Integration Tests

        [TestMethod]
        [Description("Бүрэн боловсруулах шатны амжилттай болох тест")]
        public void FullWorkflow_SmallToLargeImages_WorksCorrectly()
        {
            var testCases = new (int width, int height, string description)[]
            {
                (512, 512, "Perfect square"),
                (512, 768, "Tall rectangle"),
                (768, 512, "Wide rectangle"),
                (1024, 1024, "Large square"),
                (4096, 6000, "Requirement example")
            };

            foreach (var (width, height, description) in testCases)
            {
                string inputPath = CreateTestImage(width, height, $"integration_{width}x{height}.png");
                string outputPath = Path.Combine(_testDirectory, $"output_{width}x{height}.png");

                var result = ProfilePictureProcessor.Process(inputPath, outputPath);

                Assert.IsTrue(result.Success, $"Failed for: {description} ({width}x{height})");
                Assert.IsTrue(File.Exists(result.OutputPath));

                using (var resultImage = Image.FromFile(result.OutputPath))
                {
                    Assert.AreEqual(512, resultImage.Width, $"Width mismatch for {description}");
                    Assert.AreEqual(512, resultImage.Height, $"Height mismatch for {description}");
                }
            }
        }

        #endregion
    }
}