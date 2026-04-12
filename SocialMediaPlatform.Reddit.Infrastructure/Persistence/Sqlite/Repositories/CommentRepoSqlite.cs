using SocialMediaPlatform.Core.Domain.Entity;
using SocialMediaPlatform.Core.Domain.IdWrapper;
using SocialMediaPlatform.Reddit.Core.Ports.Output;
using Microsoft.Data.Sqlite;
using SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Reddit.Infrastructure.Persistence.Sqlite.Repositories
{
    /// <summary>
    /// Comment service-ийн SQLite Repository-той ажиллах адаптер
    /// </summary>
    public class CommentRepoSqlite : ICommentRepoPort
    {
        /// <summary>
        /// SQLite холболтын объект
        /// </summary>
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Байгуулагч. SQLite холболтыг dependency injection-аар авна.
        /// </summary>
        /// <param name="connection">SQLite холболт</param>
        public CommentRepoSqlite(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        /// <summary>Сэтгэгдэл устгана</summary>
        /// <param name="commentId">Устгах сэтгэгдлийн ID дугаар</param>
        public void Delete(CommentId commentId)
        {
            const string deleteQuery = @"
                DELETE FROM Comments
                WHERE Id = @CommentId";

            using var command = _connection.CreateCommand();
            command.CommandText = deleteQuery;
            command.Parameters.AddWithValue("@CommentId", commentId.Value);

            command.ExecuteNonQuery();
        }

        /// <summary>ID дугаараар сэтгэгдэл хайна</summary>
        /// <param name="commentId">Сэтгэгдлийн ID дугаар</param>
        public Comment FindById(CommentId commentId)
        {
            const string query = @"
                SELECT Id, PostId, AuthorId, Content, CreatedAt
                FROM Comments
                WHERE CommentId = @CommentId";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@CommentId", commentId.Value);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return CommentMapper.ToDomain(reader);
            }

            throw new KeyNotFoundException($"Comment with ID {commentId.Value} not found.");
        }

        /// <summary>Постын ID дугаараар сэтгэгдэл хайна</summary>
        /// <param name="postId">Постын ID дугаар</param>
        public List<Comment> FindByPost(PostId postId)
        {
            const string query = @"
                SELECT Id, PostId, AuthorId, Content, CreatedAt
                FROM Comments
                WHERE PostId = @PostId";

            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@PostId", postId.Value);

            using var reader = command.ExecuteReader();
            var comments = new List<Comment>();

            while (reader.Read())
            {
                comments.Add(CommentMapper.ToDomain(reader));
            }
            return comments;
        }

        /// <summary>Шинэ сэтгэгдэл хадгална</summary>
        /// <param name="comment">Хадгалах сэтгэгдлийн объект</param>
        public void Save(Comment comment)
        {
            const string insertQuery = @"
                INSERT INTO Comments (Id, PostId, AuthorId, Content, CreatedAt)
                VALUES (@Id, @PostId, @AuthorId, @Content, @CreatedAt)";

            using var command = _connection.CreateCommand();
            command.CommandText = insertQuery;
            command.Parameters.AddWithValue("@Id", comment.Id.Value);
            command.Parameters.AddWithValue("@PostId", comment.PostId.Value);
            command.Parameters.AddWithValue("@AuthorId", comment.AuthorId.Value);
            command.Parameters.AddWithValue("@Content", comment.Content);
            command.Parameters.AddWithValue("@CreatedAt", comment.CreatedAt.ToString("O"));

            command.ExecuteNonQuery();
        }
    }
}
