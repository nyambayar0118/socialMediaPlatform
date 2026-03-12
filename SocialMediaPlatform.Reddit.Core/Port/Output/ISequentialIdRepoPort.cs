using SocialMediaPlatform.Core.Domain.Enum;

namespace SocialMediaPlatform.Reddit.Core.Ports.Output
{
    /// <summary>
    /// Дараалсан ID-ийн репозиторийн Output Port интерфейс
    /// </summary>
    public interface ISequentialIdRepoPort
    {
        /// <summary>Сүүлийн ID-ийг авах</summary>
        /// <param name="entityType">Объектын төрөл</param>
        /// <returns>Сүүлийн ID-ийн утга</returns>
        uint GetLastId(IdEntityType entityType);

        /// <summary>Сүүлийн ID-ийг хадгалах</summary>
        /// <param name="entityType">Объектын төрөл</param>
        /// <param name="value">Хадгалах ID-ийн утга</param>
        void SaveLastId(IdEntityType entityType, uint value);
    }
}