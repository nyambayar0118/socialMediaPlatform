using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilePictureProcessor
{
    /// <summary>
    /// Зургийг боловсруулсны дараах үр дүнг илэрхийлэх record
    /// </summary>
    public record ProcessResult
    {
        /// <summary>
        /// Үр дүн амжилттай болсон эсэхийг заана. Амжилттай бол true, алдаа гарсан бол false байна.
        /// </summary>
        public bool Success { get; init; }
        /// <summary>
        /// Амжилтгүй болсон тохиолдолд алдааны мэдээллийг агуулна.
        /// </summary>
        public string? ErrorMessage { get; init; }
        /// <summary>
        /// Үр дүн амжилттай болсон тохиолдолд боловсруулсан зургийн замыг агуулна.
        /// </summary>
        public string? OutputPath { get; init; }

        /// <summary>
        /// Үр дүн амжилттай болсон тохиолдолд record үүсгэх метод.
        /// </summary>
        /// <param name="outputPath"> Зургийг боловсруулсны дараа хадгалах зам. </param>
        /// <returns> Үр дүнгийн record-ийг буцаана. </returns>
        public static ProcessResult Ok(string outputPath) => new() { Success = true, OutputPath = outputPath };
        /// <summary>
        /// Үр дүн амжилтгүй болсон тохиолдолд record үүсгэх метод.
        /// </summary>
        /// <param name="error"> Гарсан алдааны мессэж. </param>
        /// <returns> Үр дүнгийн record-ийг буцаана. </returns>
        public static ProcessResult Fail(string error) => new() { Success = false, ErrorMessage = error };
    }
}
