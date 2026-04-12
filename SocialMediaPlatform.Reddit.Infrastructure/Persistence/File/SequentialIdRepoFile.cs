using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.File
{
    /// <summary>
    /// Дараалсан ID-ийн файл дээр суурилсан Repository адаптер
    /// </summary>
    public class SequentialIdRepoFile : ISequentialIdRepoPort
    {
        /// <summary>
        /// Ажиллах файлын зам. Файлд ID төрлүүд болон сүүлийн ID-үүдийг хадгална.
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Байгуулагч. Файл замыг авна. Файл байхгүй бол автоматаар үүсгэнэ.
        /// </summary>
        /// <param name="filePath">Файлын зам</param>
        public SequentialIdRepoFile(string filePath)
        {
            _filePath = filePath;
            // Файлд 0 утгуудыг ID төрлүүдийн хамт бичиж үүсгэнэ
            if (!System.IO.File.Exists(_filePath))
            {
                var lines = Enum.GetValues<IdEntityType>()
                    .Select(e => $"{e}|0")
                    .ToArray();
                System.IO.File.WriteAllLines(_filePath, lines);
            }
        }

        /// <summary>Сүүлийн ID дугаарыг авах</summary>
        /// <param name="entityType">ID-тай entity төрлийг зааж өгөх</param>
        public uint GetLastId(IdEntityType entityType)
        {
            foreach (var line in System.IO.File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (Enum.Parse<IdEntityType>(parts[0]) == entityType)
                    return uint.Parse(parts[1]);
            }
            throw new KeyNotFoundException($"Entity type {entityType} not found");
        }

        /// <summary>Сүүлийн ID-ийг хадгалах</summary>
        /// <param name="entityType">ID-тай entity төрлийг зааж өгөх</param>
        /// <param name="value">Хадгалах утга</param>
        public void SaveLastId(IdEntityType entityType, uint value)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return Enum.Parse<IdEntityType>(parts[0]) == entityType
                        ? $"{entityType}|{value}"
                        : line;
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }
    }
}