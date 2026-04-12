using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Domain.Posts;
using SocialMediaPlatform.Reddit.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories
{
    /// <summary>
    /// Post service-ийн SQLite Repository-той ажиллах адаптер
    /// </summary>
    public class PostRepoSqlite : IPostRepoPort
    {
        /// <summary>
        /// SQLite холболтын объект
        /// </summary>
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Байгуулагч. SQLite холболтыг dependency injection-аар авна.
        /// </summary>
        /// <param name="connection">SQLite холболт</param>
        public PostRepoSqlite(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>Шинэ пост хадгална</summary>
        /// <param name="post">Хадгалах постын объект</param>
        public void Save(Post post)
        {
            const string insertQuery = @"
                INSERT INTO Posts (Id, AuthorId, Title, Content, Visibility, CreatedAt)
                VALUES (@Id, @AuthorId, @Title, @Content, @Visibility, @CreatedAt)";

            using var command = _connection.CreateCommand();
            command.CommandText = insertQuery;
            command.Parameters.AddWithValue("@Id", post.Id.Value);
            command.Parameters.AddWithValue("@AuthorId", post.AuthorId.Value);
            command.Parameters.AddWithValue("@Title", (post as TimelinePost)?.Title ?? "");
            command.Parameters.AddWithValue("@Content", (post as TimelinePost)?.Content ?? "");
            command.Parameters.AddWithValue("@Visibility", post.Visibility.ToString());
            command.Parameters.AddWithValue("@CreatedAt", post.CreatedAt.ToString("O"));

            command.ExecuteNonQuery();
        }

        /// <summary>ID дугаараар пост хайна</summary>
        /// <param name="postId">Постын ID дугаар</param>
        public Post FindById(PostId postId)
        {
            const string query = @"
                SELECT Id, AuthorId, Title, Content, Visibility, CreatedAt
                FROM Posts
                WHERE Id = @Id";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@Id", postId.Value);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return PostMapper.ToDomain(reader);
            }

            throw new KeyNotFoundException($"Post ID {postId.Value} not found");
        }

        /// <summary>Бүх постыг жагсаалт байдаар авна</summary>
        public List<Post> FindAll()
        {
            const string query = @"
                SELECT Id, AuthorId, Title, Content, Visibility, CreatedAt
                FROM Posts
                ORDER BY CreatedAt DESC";

            var posts = new List<Post>();

            using var command = _connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                posts.Add(PostMapper.ToDomain(reader));
            }

            return posts;
        }

        /// <summary>Постын мэдээлэл шинэчлэнэ</summary>
        /// <param name="post">Шинэчлэх постын объект</param>
        public void Update(Post post)
        {
            const string updateQuery = @"
                UPDATE Posts
                SET AuthorId = @AuthorId,
                    Title = @Title,
                    Content = @Content,
                    Visibility = @Visibility
                WHERE Id = @Id";

            using var command = _connection.CreateCommand();
            command.CommandText = updateQuery;
            command.Parameters.AddWithValue("@Id", post.Id.Value);
            command.Parameters.AddWithValue("@AuthorId", post.AuthorId.Value);
            command.Parameters.AddWithValue("@Title", (post as TimelinePost)?.Title ?? "");
            command.Parameters.AddWithValue("@Content", (post as TimelinePost)?.Content ?? "");
            command.Parameters.AddWithValue("@Visibility", post.Visibility.ToString());

            command.ExecuteNonQuery();
        }

        /// <summary>Пост устгана</summary>
        /// <param name="postId">Устгах постын ID дугаар</param>
        public void Delete(PostId postId)
        {
            const string deleteQuery = "DELETE FROM Posts WHERE Id = @Id";

            using var command = _connection.CreateCommand();
            command.CommandText = deleteQuery;
            command.Parameters.AddWithValue("@Id", postId.Value);

            command.ExecuteNonQuery();
        }
    }
}