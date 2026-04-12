using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Reactions;
using SocialMediaPlatform.Reddit.Core.Ports.Output;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.File
{
    /// <summary>
    /// Reaction-ий файл дээр суурилсан Repository адаптер
    /// </summary>
    public class ReactionRepoFile : IReactionRepoPort
    {
        /// <summary>
        /// Ажиллах файл зам. Файлд Reaction-ий мэдээллийг мөр болгон хадгална. Мөрийн формат: TargetId|TargetType|AuthorId|CreatedAt|ReactionType
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Байгуулагч. Файлын замыг авна. Файл байхгүй бол автоматаар үүсгэнэ.
        /// </summary>
        /// <param name="filePath">Файлын зам</param>
        public ReactionRepoFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>Reaction хадгалах</summary>
        /// <param name="reaction">Хадгалах Reaction объект</param>
        public void Save(Reaction reaction)
        {
            var line = Serialize(reaction);
            System.IO.File.AppendAllText(_filePath, line + Environment.NewLine);
        }

        /// <summary>Reaction устгах</summary>
        /// <param name="authorId">Reaction өгсөн хэрэглэгчийн Id</param>
        /// <param name="targetId">Reaction өгсөн объекты Id</param>
        public void Delete(uint targetId, UserId authorId)
        {
            var lines = System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line =>
                {
                    var parts = line.Split('|');
                    return !(uint.Parse(parts[0]) == targetId && uint.Parse(parts[2]) == authorId.Value);
                })
                .ToArray();
            System.IO.File.WriteAllLines(_filePath, lines);
        }

        /// <summary>Объектын Reaction-ий тоог авах</summary>
        /// <param name="targetId">Reaction өгсөн объекты Id</param>
        /// <param name="targetType">Reaction өгсөн объекты төрөл</param>
        public Dictionary<string, uint> CountByTarget(uint targetId, ReactionTargetType targetType)
        {
            return System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(Deserialize)
                .Where(r => r.TargetId == targetId && r.TargetType == targetType)
                .GroupBy(r => r is Upvote ? "Upvote" : "Downvote")
                .ToDictionary(g => g.Key, g => (uint)g.Count());
        }

        /// <summary>Хэрэглэгч тухайн объектод Reaction өгсөн эсэхийг шалгах</summary>
        /// <param name="userId">Хэрэглэгчийн Id</param>
        /// <param name="targetType">Reaction өгсөн объекты төрөл</param>
        /// <param name="targetId">Reaction өгсөн объекты Id</param>
        public bool ExistsByUserAndTarget(UserId userId, uint targetId, ReactionTargetType targetType)
        {
            return System.IO.File.ReadAllLines(_filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(Deserialize)
                .Any(r => r.AuthorId.Value == userId.Value
                       && r.TargetId == targetId
                       && r.TargetType == targetType);
        }

        /// <summary>Reaction объектыг мөр болгох</summary>
        /// <param name="reaction">Reaction объект</param>
        private static string Serialize(Reaction reaction)
        {
            var type = reaction is Upvote ? "Upvote" : "Downvote";
            return $"{reaction.TargetId}|{reaction.TargetType}|{reaction.AuthorId.Value}|{reaction.CreatedAt:O}|{type}";
        }

        /// <summary>Мөрийг Reaction объект болгох</summary>
        /// <param name="line">Объект болгох мөр</param>
        private static Reaction Deserialize(string line)
        {
            var parts = line.Split('|');
            var targetId = uint.Parse(parts[0]);
            var targetType = Enum.Parse<ReactionTargetType>(parts[1]);
            var authorId = new UserId { Value = uint.Parse(parts[2]) };
            var createdAt = DateTime.Parse(parts[3]);
            var type = parts[4];

            if (type == "Upvote")
                return new Upvote { TargetId = targetId, TargetType = targetType, AuthorId = authorId, CreatedAt = createdAt };

            if (type == "Downvote")
                return new Downvote { TargetId = targetId, TargetType = targetType, AuthorId = authorId, CreatedAt = createdAt };

            throw new ArgumentException($"Undefined Reaction type: {type}");
        }
    }
}